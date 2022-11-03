using Balance.Data;
using UnityEngine;

namespace Balance.Configs
{
    [CreateAssetMenu(fileName = "PathPointConfig", menuName = "ScriptableObject/PathPointConfig", order = 0)]
    public class PathPointConfig : ScriptableObject
    {
        [SerializeField] private int area = 1;
        
        public PathPointData Get()
        {
            return new PathPointData(new Vector2Int(area, area));
        }
    }
}