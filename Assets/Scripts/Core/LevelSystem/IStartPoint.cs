using System;
using UnityEngine;

namespace Core.LevelSystem
{
    public interface IStartPoint : IDisposable
    {
        public Vector2Int Position { get; }
    }
}