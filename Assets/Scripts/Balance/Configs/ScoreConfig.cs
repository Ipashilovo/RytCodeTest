using Balance.Data;
using UnityEngine;

namespace Balance.Configs
{
    [CreateAssetMenu(fileName = "ScoreConfig", menuName = "ScriptableObject/ScoreConfig", order = 0)]
    public class ScoreConfig : ScriptableObject
    {
        [SerializeField] private int _scoreOnFinish;
        [SerializeField] private int _scoreOnPoint;

        public ScoreData Get()
        {
            return new ScoreData(_scoreOnPoint, _scoreOnFinish);
        }
    }
}