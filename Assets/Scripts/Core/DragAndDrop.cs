using System;
using UnityEngine;

namespace Core
{
    public class DragAndDrop : MonoBehaviour
    {
        private Vector3 _mouseOffset;
        private bool _isLock;
        private ITimeProvider _timeProvider;

        public bool IsMovement { get; private set; }

        public void Init(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        private void OnMouseDown()
        {
            if (_timeProvider.IsPause)
            {
                return;
            }
            IsMovement = true;
            _mouseOffset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        }

        private void OnMouseDrag()
        {
            if (_timeProvider.IsPause || _isLock)
            {
                return;
            }
            
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _mouseOffset;
        }

        private void OnMouseUp()
        {
            IsMovement = false;
        }

        public void SetLock(bool isLock = true)
        {
            _isLock = isLock;
        }

        public void Drop()
        {
            IsMovement = false;
        }
    }
}