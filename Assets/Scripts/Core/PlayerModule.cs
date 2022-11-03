using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.LevelSystem;
using UnityEngine;

namespace Core
{
    public class PlayerModule : IDisposable
    {
        private readonly IEnumerable<IPlayer> _players;
        private readonly Level _level;
        private readonly PathCreator _pathCreator;

        public bool CanStart { get; private set; }

        public event Action CanStartIsChange;
        public event Action Complete;

        public PlayerModule(IEnumerable<IPlayer> players, Level level, PathCreator pathCreator)
        {
            _players = players;
            _level = level;
            _pathCreator = pathCreator;
            foreach (var player in _players)
            {
                player.NeedToAllign += AllignPlayer;
            }
        }

        public async Task Start()
        {
            if (CanStart == false)
            {
                return;
            }

            CanStart = false;
            _level.Lock();
            foreach (var player in _players)
            {
                player.Lock();
            }
            
            _pathCreator.SetPath(_players);

            bool allFinish = false;
            while (allFinish == false)
            {
                foreach (var player in _players)
                {
                    if (player.Complete == false)
                    {
                        allFinish = false;
                        break;
                    }

                    allFinish = true;
                }

                await Task.Yield();
            }
            Complete?.Invoke();
        }

        private void AllignPlayer(IPlayer player)
        {
            if (_level.TryGetPoint(out var point, player.Position))
            {
                if (Math.Abs(Vector2Int.Distance(_level.StartPoint.Position, point.PositionInLevel) - 1) < 0.1f)
                {
                    player.SetTarget(point);
                    SetCanStart();
                    return;
                }
            }
            player.DropTarget();
            SetCanStart();
        }

        private void SetCanStart()
        {
            foreach (var player in _players)
            {
                if (player.CanStart == false)
                {
                    CanStart = false;
                    CanStartIsChange?.Invoke();
                    return;
                }
            }
            
            CanStart = true;
            CanStartIsChange?.Invoke();
        }

        public void Dispose()
        {
            foreach (var player in _players)
            {
                player.NeedToAllign -= AllignPlayer;
            }
        }
    }
}