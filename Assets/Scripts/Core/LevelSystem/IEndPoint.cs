using System;
using UnityEngine;

namespace Core.LevelSystem
{
    public interface IEndPoint : IDisposable
    {
        public Vector2Int Position { get; }
    }
}