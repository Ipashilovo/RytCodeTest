using System;
using System.Collections.Generic;
using Core.LevelSystem;
using UnityEngine;

namespace Core
{
    public class Path
    {
        public bool ReachFinish { get; }
        public Vector2Int[] Points { get; }

        public Path(Vector2Int[] points, bool reachFinish)
        {
            Points = points;
            ReachFinish = reachFinish;
        }
    }
}