using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Managers.Manager_Camera
{
    public class ManagerCamera : MonoBehaviour
    {
        #region DIRECT REF
        [SerializeField] private Camera _mainCamera;
        [field: SerializeField] public Camera WorldCanvasCamera { get; private set; }

        private Transform _mainCameraTransfrom;
        private Transform _worldCanvasCameraTransfrom;
        #endregion

        private void Awake()
        {
            _mainCameraTransfrom = _mainCamera.transform;
            _worldCanvasCameraTransfrom = WorldCanvasCamera.transform;
        }

        private void LateUpdate()
        {
            _mainCamera.ResetWorldToCameraMatrix();
        }

    }
}