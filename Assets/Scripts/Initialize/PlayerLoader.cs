using System;
using System.Collections.Generic;
using Balance.Configs;
using Balance.Data;
using Core;
using Core.LevelSystem;
using Models;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Initialize
{
    public class PlayerLoader
    {
        private readonly Level _level;
        private readonly ITimeProvider _timeProvider;
        private readonly PlayerData _data;
        private readonly PathCreator _pathCreator;

        public PlayerLoader(Level level, ITimeProvider timeProvider, PlayerData data, PathCreator pathCreator)
        {
            _level = level;
            _timeProvider = timeProvider;
            _data = data;
            _pathCreator = pathCreator;
        }

        public PlayerModule Get(out IEnumerable<IPlayer> players)
        {
            var playerPrefab = Resources.Load<Player>("Player");
            List<Player> newPlayers = new List<Player>();
            Vector3 position = new Vector3(_level.StartPoint.Position.x, _data.Offset, _level.StartPoint.Position.x);
            for (int i = 0; i < _data.Count; i++)
            {
                position.x += i;
                var player = Object.Instantiate(playerPrefab, position, Quaternion.identity);
                player.Init(_data, _timeProvider);
                newPlayers.Add(player);
            }

            players = newPlayers;

            return new PlayerModule(newPlayers, _level, _pathCreator);
        }
    }
}