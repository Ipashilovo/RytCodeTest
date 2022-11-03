using System;
using UnityEngine;

namespace Core.LevelSystem
{
    public struct LevelCell : IEquatable<LevelCell>
    {
        public readonly bool IsFull;
        public readonly Vector2Int Position;

        public LevelCell(bool isFull, Vector2Int position)
        {
            IsFull = isFull;
            Position = position;
        }

        public bool Equals(LevelCell other)
        {
            return Position.Equals(other.Position);
        }

        public override bool Equals(object obj)
        {
            return obj is LevelCell other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode();
        }
    }
}