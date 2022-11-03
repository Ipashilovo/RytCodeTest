using System;
using Balance.Data;
using Core;
using Core.LevelSystem;
using UnityEngine;

namespace Models
{
    public class Player : MonoBehaviour, IPlayer
    {
        [SerializeField] private DragAndDrop _dragAndDrop;

        private PlayerData _playerData;
        private Path _path;
        private Vector2Int _target;
        private ITimeProvider _timeProvider;

        public bool CanStart
        {
            get
            {
                return _dragAndDrop.IsMovement == false && TargetPoint != null;
            }
        }

        public bool Complete { get; private set; }

        public IPathPoint TargetPoint { get; private set; }

        public Vector3 Position => transform.position;
        public event Action ReachPoint;
        public event Action<bool> ReachLastPoint;

        public event Action<IPlayer> NeedToAllign;

        private void Update()
        {
            if (_path == null || Complete || _timeProvider.IsPause)
            {
                return;
            }

            var targetPosition = new Vector3(_target.x, transform.position.y, _target.y);
            transform.position = Vector3.MoveTowards(transform.position,
                targetPosition, _playerData.Speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                if (TryGetNextPoint(_target, out var nextPoint) == false)
                {
                    ReachLastPoint?.Invoke(_path.ReachFinish);
                    Complete = true;
                    return;
                }
                ReachPoint?.Invoke();
                _target = nextPoint;
            }
        }

        private bool TryGetNextPoint(Vector2Int previousPoint, out Vector2Int nextPoint)
        {
            var index = Array.IndexOf(_path.Points, previousPoint);
            if (index == _path.Points.Length - 1)
            {
                nextPoint = new Vector2Int();
                return false;
            }

            nextPoint = _path.Points[index + 1];
            return true;
        }

        public void Init(PlayerData playerData, ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
            _playerData = playerData;
            _dragAndDrop.Init(timeProvider);
        }

        public void Lock(bool isLock = false)
        {
            _dragAndDrop.SetLock(isLock);
        }

        public void SetPath(Path path)
        {
            _path = path;
            _target = _path.Points[0];
        }

        public void DropTarget()
        {
            DropPoinLock();
            TargetPoint = null;
        }

        public void SetTarget(IPathPoint point)
        {
            DropPoinLock();
            _dragAndDrop.Drop();
            TargetPoint = point;
            TargetPoint.SetLock();
            var endPosition = point.Position;
            endPosition.y += _playerData.Offset;
            transform.position = endPosition;
        }

        private void DropPoinLock()
        {
            if (TargetPoint != null)
            {
                TargetPoint.SetLock(false);
            }
        }

        private void OnMouseUp()
        {
            NeedToAllign?.Invoke(this);
        }
    }
}