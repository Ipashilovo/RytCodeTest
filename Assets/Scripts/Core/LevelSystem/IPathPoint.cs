using System;
using UnityEngine;

namespace Core.LevelSystem
{
    public interface IPathPoint : IDisposable
    {
        public Vector2Int PositionInLevel { get; }
        public Vector3 Position { get; }
        public event Func<IPathPoint, Vector2Int> ReadyToAllign;
        
        public void SetLock(bool isLock = true);
    }
}