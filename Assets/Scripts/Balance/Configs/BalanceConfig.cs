using Balance.Data;
using UnityEngine;

namespace Balance.Configs
{
    [CreateAssetMenu(fileName = "BalanceConfig", menuName = "ScriptableObject/BalanceConfig", order = 0)]
    public class BalanceConfig : ScriptableObject
    {
        [SerializeField] private LevelGeneratorConfig _levelGeneratorConfig;
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private ScoreConfig _scoreConfig;
        
        public BalanceData Get()
        {
            return new BalanceData(_scoreConfig.Get(), _levelGeneratorConfig.Get(), _playerConfig.Get());
        }
    }
}