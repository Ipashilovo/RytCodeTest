using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.LevelSystem
{
    public class Level : IDisposable
    {
        private readonly LevelCell[,] _cells;
        private readonly List<IPathPoint> _points;
        private Dictionary<LevelCell, IPathPoint> _pathPoints = new Dictionary<LevelCell, IPathPoint>();

        public IStartPoint StartPoint {get; }
        public IEndPoint EndPoint {get; }

        public Level(LevelCell[,] cells, IStartPoint startPoint, IEndPoint endPoint, List<IPathPoint> points)
        {
            _cells = cells;
            StartPoint = startPoint;
            EndPoint = endPoint;
            _points = points;
            foreach (var point in points)
            {
                point.ReadyToAllign += AllignPoint;
                _pathPoints[cells[point.PositionInLevel.x, point.PositionInLevel.y]] = point;
            }
        }

        public bool TryGetPoint(out IPathPoint point, Vector3 position)
        {
            if (CheckOutOfLevel(position))
            {
                point = null;
                return false;
            }
            var positionInLevel = GetPositionInLevel(position);
            var cell = _cells[positionInLevel.x, positionInLevel.y];
            if (_pathPoints.TryGetValue(cell, out point))
            {
                return true;
            }

            return false;
        }

        public void Clear()
        {
            StartPoint.Dispose();
            EndPoint.Dispose();
            foreach (var point in _points)
            {
                point.ReadyToAllign -= AllignPoint;
                point.Dispose();
            }
        }

        public IEnumerable<IPathPoint> GetNeighbours(IPathPoint pathPoint)
        {
            var position = pathPoint.PositionInLevel;
            List<LevelCell> heighbours = new List<LevelCell>();
            if (position.x > 0)
            {
                heighbours.Add(_cells[position.x - 1, position.y]);
            }

            if (position.x < _cells.GetLength(0) - 1)
            {
                heighbours.Add(_cells[position.x + 1, position.y]);
            }

            if (position.y > 0)
            {
                heighbours.Add(_cells[position.x, position.y - 1]);
            }

            if (position.y < _cells.GetLength(1) - 1)
            {
                heighbours.Add(_cells[position.x, position.y + 1]);
            }

            List<IPathPoint> points = new List<IPathPoint>();
            foreach (var heighbour in heighbours)
            {
                if (_pathPoints.TryGetValue(heighbour, out var point))
                {
                    points.Add(point);
                }
            }

            return points;
        }

        public void Lock()
        {
            foreach (var point in _points)
            {
                point.SetLock();
            }
        }

        public void Dispose()
        {
            foreach (var point in _points)
            {
                point.ReadyToAllign -= AllignPoint;
            }
        }

        private Vector2Int GetPositionInLevel(Vector3 pointPosition)
        {
            int allingX = (int)Math.Round(pointPosition.x, MidpointRounding.AwayFromZero);
            int allingY = (int)Math.Round(pointPosition.z, MidpointRounding.AwayFromZero);
            allingY = Math.Clamp(allingY, 0, _cells.GetLength(1) - 1);
            allingX = Math.Clamp(allingX, 0, _cells.GetLength(0) - 1);
            return  new Vector2Int(allingX, allingY);
        }

        private bool CheckOutOfLevel(Vector3 position)
        {
            var roundZ = Math.Round(position.z, MidpointRounding.AwayFromZero);
            var roundX = Math.Round(position.x, MidpointRounding.AwayFromZero);
            if (roundX < 0 || roundZ < 0) return true;
            if (roundX > _cells.GetLength(1) - 1) return true;
            if (roundZ > _cells.GetLength(0) - 1) return true;
            return false;
        }

        private Vector2Int AllignPoint(IPathPoint point)
        {
            var pointPosition = point.Position;
            Vector2Int positionInLevel = GetPositionInLevel(pointPosition);
            if (_cells[positionInLevel.x, positionInLevel.y].IsFull)
            {
                return point.PositionInLevel;
            }

            var oldCell = _cells[point.PositionInLevel.x, point.PositionInLevel.y];
            _pathPoints.Remove(oldCell);
            _cells[point.PositionInLevel.x, point.PositionInLevel.y] = new LevelCell(false, point.PositionInLevel);
            var newCell = new LevelCell(true, positionInLevel);
            _cells[positionInLevel.x, positionInLevel.y] = newCell;
            _pathPoints[newCell] = point;
            return positionInLevel;
        }
    }
}