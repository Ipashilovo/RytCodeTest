using Balance.Data;
using UnityEngine;

namespace Balance.Configs
{
    [CreateAssetMenu(fileName = "LevelGeneratorConfig", menuName = "ScriptableObject/LevelGeneratorConfig", order = 0)]
    public class LevelGeneratorConfig : ScriptableObject
    {
        [SerializeField] private PathPointConfig _pathPointConfig;
        [SerializeField] private int _height;
        [SerializeField] private int _width;
        public LevelGeneratorData Get()
        {
            return new LevelGeneratorData(_pathPointConfig.Get(), new Vector2Int(_height, _width));
        }
    }
}