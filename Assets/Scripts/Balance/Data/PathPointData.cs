using System.Numerics;
using UnityEngine;

namespace Balance.Data
{
    public class PathPointData
    {
        public PathPointData(Vector2Int area)
        {
            Area = area;
        }

        public Vector2Int Area { get; }
    }
}