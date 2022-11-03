using Core;
using Core.LevelSystem;
using UnityEngine;

namespace Models
{
    public class PointFactory : IPointFactory
    {
        private readonly PathPoint _point;
        private readonly TimeProvider _timeProvider;
        private StartPoint _startPoint;
        private EndPoint _endPoint;

        public PointFactory(StartPoint startPoint, EndPoint endPoint, PathPoint point, TimeProvider timeProvider)
        {
            _startPoint = startPoint;
            _endPoint = endPoint;
            _point = point;
            _timeProvider = timeProvider;
        }
        
        public IStartPoint GetStartPoint(Vector2Int freeCellPosition)
        {
            var point =  Instantiate(_startPoint, freeCellPosition);
            point.Init(freeCellPosition);
            return point;
        }

        public IEndPoint GetEndPoint(Vector2Int freeCellPosition)
        {
            var point =  Instantiate(_endPoint, freeCellPosition);
            point.Init(freeCellPosition);
            return point;
        }

        public IPathPoint GetPathPoint(Vector2Int freeCellPosition)
        {
            var point =  Instantiate(_point, freeCellPosition);
            point.SetPosition(freeCellPosition);
            point.Init(_timeProvider);
            return point;
        }

        private T Instantiate<T>(T prefab, Vector2Int freeCellPosition) where T : MonoBehaviour
        {
            return Object.Instantiate(prefab, new Vector3(freeCellPosition.x, 0, freeCellPosition.y), Quaternion.identity);
        }
    }
}