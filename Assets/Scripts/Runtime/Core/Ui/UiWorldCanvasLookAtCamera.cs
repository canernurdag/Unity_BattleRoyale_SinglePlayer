using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Ui
{
    [DefaultExecutionOrder(10)]
    public class UiWorldCanvasLookAtCamera : MonoBehaviour
    {

        private Transform _mainCameraTransform;

        private void Start()
        {
            _mainCameraTransform = Camera.main.transform;
        }

        private void LateUpdate()
        {
            transform.forward = _mainCameraTransform.forward;
        }
    }
}