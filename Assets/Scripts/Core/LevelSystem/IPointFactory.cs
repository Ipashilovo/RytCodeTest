using UnityEngine;

namespace Core.LevelSystem
{
    public interface IPointFactory
    {
        public IStartPoint GetStartPoint(Vector2Int freeCellPosition);
        public IEndPoint GetEndPoint(Vector2Int freeCellPosition);
        public IPathPoint GetPathPoint(Vector2Int freeCellPosition);
    }
}