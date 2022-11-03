using System;
using System.Collections.Generic;
using Balance.Configs;
using Core;
using Core.LevelSystem;
using UI;
using UnityEngine;

namespace Initialize
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private UiManager _uiManager;
        private Level _level;
        private PlayerModule _playerModule;
        private ScoreHandler _scoreHandler;

        private void OnDestroy()
        {
            _playerModule.Dispose();
            _level.Dispose();
            _scoreHandler.Dispose();
        }

        private void Start()
        {
            var balanceConfig = Resources.Load<BalanceConfig>("BalanceConfig");
            var balance = balanceConfig.Get();
            Resources.UnloadAsset(balanceConfig);
            TimeProvider timeProvider = new TimeProvider();
            _level = new LevelInitializer(balance.LevelGeneratorData, timeProvider).Get();
            PathCreator pathCreator = new PathCreator(_level);
            _playerModule = new PlayerLoader(_level, timeProvider, balance.PlayerData, pathCreator).Get(out IEnumerable<IPlayer> players);
            _scoreHandler = new ScoreHandler(balance.ScoreData, players);
            EndGameProvider endGameProvider = new EndGameProvider();
            _uiManager.Bind(_playerModule, timeProvider, _scoreHandler, endGameProvider);
        }
    }
}