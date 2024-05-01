using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Ui
{
    [RequireComponent(typeof(RectTransform))]
    public class UiPointer : MonoBehaviour
    {
        #region Caches
        private Transform _referenceTransform;
        private Transform _targetTransform;
        private RectTransform _rectTransform;
        private Camera _mainCamera;
        #endregion

        #region Buffers
        private Vector3 _direction = Vector3.zero;
        private float _angleForUiPointerRotation;
        private Vector3 _screenPositionOfTargetTransform;
        private float _border = 30f;

        private bool _isOffScreen = false;
        #endregion

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (_targetTransform == null) return;
            if (_referenceTransform == null) return;
            if (_mainCamera == null) return;

            SetRotation();
            SetPosition();
        }

        #region Set Functions

        private void SetRotation()
        {
            _direction = (_targetTransform.position - _referenceTransform.position).normalized;
            _angleForUiPointerRotation = Mathf.Atan2(_direction.z, _direction.x) * Mathf.Rad2Deg;

            _rectTransform.localEulerAngles = new Vector3(0, 0, _angleForUiPointerRotation);
        }

        private void SetPosition()
        {
            _screenPositionOfTargetTransform = _mainCamera.WorldToScreenPoint(_targetTransform.position);
            _isOffScreen = _screenPositionOfTargetTransform.x <= _border
                           || _screenPositionOfTargetTransform.x >= Screen.width - _border
                           || _screenPositionOfTargetTransform.y <= _border
                           || _screenPositionOfTargetTransform.y >= Screen.height - _border;

            if (_isOffScreen)
            {
                if (_screenPositionOfTargetTransform.x <= _border) _screenPositionOfTargetTransform.x = _border;

                if (_screenPositionOfTargetTransform.x >= Screen.width - _border) _screenPositionOfTargetTransform.x = Screen.width - _border;

                if (_screenPositionOfTargetTransform.y <= _border) _screenPositionOfTargetTransform.y = _border;

                if (_screenPositionOfTargetTransform.y >= Screen.height - _border) _screenPositionOfTargetTransform.y = Screen.height - _border;
            }

            _rectTransform.position =
                new Vector3(_screenPositionOfTargetTransform.x, _screenPositionOfTargetTransform.y, 0);
        }

        public void SetTargetTransform(Transform targetTransform)
        {
            _targetTransform = targetTransform;
        }

        public void SetReferenceTransform(Transform referenceTransform)
        {
            _referenceTransform = referenceTransform;
        }
        #endregion

    }
}

