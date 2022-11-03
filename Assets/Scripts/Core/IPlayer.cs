using System;
using Core.LevelSystem;
using UnityEngine;

namespace Core
{
    public interface IPlayer
    {
        public bool CanStart { get; }
        public bool Complete { get; }
        public IPathPoint TargetPoint { get; }
        public Vector3 Position { get; }

        public event Action ReachPoint;
        public event Action<bool> ReachLastPoint;
        
        public event Action<IPlayer> NeedToAllign;

        public void Lock(bool isLock = true);

        public void DropTarget();

        public void SetTarget(IPathPoint point);
        void SetPath(Path path);
    }
}