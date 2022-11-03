using System;
using Core;
using Core.LevelSystem;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Models
{
    public class PathPoint : MonoBehaviour, IPathPoint
    {
        [SerializeField] private DragAndDrop _dragAndDrop;
        private bool _isLock;
        
        public Vector2Int PositionInLevel { get; private set; }
        public Vector3 Position => transform.position;
        
        public event Func<IPathPoint, Vector2Int> ReadyToAllign;

        public void Init(ITimeProvider timeProvider)
        {
            _dragAndDrop.Init(timeProvider);
        }

        public void SetLock(bool isLock = true)
        {
            _isLock = isLock;
            _dragAndDrop.SetLock(isLock);
            if (_dragAndDrop.IsMovement)
            {
                var result = ReadyToAllign?.Invoke(this);
                if (result.HasValue)
                {
                    PositionInLevel = result.Value;
                    transform.position = new Vector3(PositionInLevel.x, 0, PositionInLevel.y);
                }
            }
        }

        public void SetPosition(Vector2Int position)
        {
            PositionInLevel = position;
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }

        private void OnMouseUp()
        {
            if (_isLock)
            {
                return;
            }
            var result =  ReadyToAllign?.Invoke(this);
            if (result.HasValue)
            {
                PositionInLevel = result.Value;
                transform.position = new Vector3(PositionInLevel.x, 0, PositionInLevel.y);
            }
        }
    }
}