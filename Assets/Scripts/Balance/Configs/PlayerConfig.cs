using Balance.Data;
using UnityEngine;

namespace Balance.Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "ScriptableObject/PlayerConfig", order = 0)]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private int _playerCount;
        [SerializeField] private int _offset;
        [SerializeField] private float _speed;


        public int PlayerCount => _playerCount;

        public PlayerData Get()
        {
            return new PlayerData(_offset, _speed, _playerCount);
        }
    }
}