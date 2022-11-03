using System.Numerics;
using UnityEngine;

namespace Balance.Data
{
    public class LevelGeneratorData
    {
        public Vector2Int MaxDistance { get; }
        public PathPointData PathPointData { get; }

        public LevelGeneratorData(PathPointData pathPointData, Vector2Int maxDistance)
        {
            PathPointData = pathPointData;
            MaxDistance = maxDistance;
        }
    }
}