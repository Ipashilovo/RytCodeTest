using System;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _playButton;
        [SerializeField] private ScoreScreen _scoreScreen;
        [SerializeField] private Button _restartButton;
        private TimeProvider _timeProvider;
        private PlayerModule _playerModule;
        private EndGameProvider _endGameProvider;

        private void Start()
        {
            _playButton.onClick.AddListener(() => _timeProvider?.SetPause(false));
            _pauseButton.onClick.AddListener(() => _timeProvider?.SetPause(true));
            _restartButton.onClick.AddListener(() => _endGameProvider?.Restart());
        }

        private void OnDestroy()
        {
            if (_playerModule != null)
            {
                _playerModule.Complete -= OnPlayerModuleComplete;
            }
        }

        public void Bind(PlayerModule playerModule, TimeProvider timeProvider, ScoreHandler scoreHandler, EndGameProvider endGameProvider)
        {
            _endGameProvider = endGameProvider;
            _playerModule = playerModule;
            _timeProvider = timeProvider;
            _scoreScreen.Bind(scoreHandler, playerModule);
            playerModule.Complete += OnPlayerModuleComplete;
            gameObject.SetActive(true);
        }

        private void OnPlayerModuleComplete()
        {
            _restartButton.gameObject.SetActive(true);
        }
    }
}