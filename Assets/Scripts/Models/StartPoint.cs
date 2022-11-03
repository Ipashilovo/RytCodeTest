using Core.LevelSystem;
using UnityEngine;

namespace Models
{
    public class StartPoint : MonoBehaviour, IStartPoint
    {
        public Vector2Int Position { get; private set; }

        public void Init(Vector2Int position)
        {
            Position = position;
            transform.position = new Vector3(position.x, 0, position.y);
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}