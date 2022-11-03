using System;
using System.Collections.Generic;
using Balance.Data;

namespace Core
{
    public class ScoreHandler : IDisposable
    {
        private readonly ScoreData _scoreData;
        private readonly IEnumerable<IPlayer> _players;
        public int Score { get; private set; }

        public event Action ScoreChanged;

        public ScoreHandler(ScoreData scoreData, IEnumerable<IPlayer> players)
        {
            _scoreData = scoreData;
            _players = players;
            foreach (var player in _players)
            {
                player.ReachPoint += OnReachPoint;
                player.ReachLastPoint += OnReachLastPoint;
            }
        }

        private void OnReachLastPoint(bool obj)
        {
            if (obj)
            {
                Score += _scoreData.ScoreOnFinish;
                ScoreChanged?.Invoke();
            }
            else
            {
                OnReachPoint();
            }
        }

        private void OnReachPoint()
        {
            Score += _scoreData.ScoreOnPoint;
            ScoreChanged?.Invoke();
        }

        public void Dispose()
        {
            foreach (var player in _players)
            {
                player.ReachPoint -= OnReachPoint;
                player.ReachLastPoint -= OnReachLastPoint;
            }
        }
    }
}