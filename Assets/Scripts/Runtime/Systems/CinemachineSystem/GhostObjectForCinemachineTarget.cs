using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Systems.CinemachineSystem
{
    public class GhostObjectForCinemachineTarget : MonoBehaviour
    {
        private Transform _targetTransform;

        private void LateUpdate()
        {
            if (_targetTransform)
            {
                transform.position = _targetTransform.transform.position;
            }
        }

        public void SetTargetTransform(Transform targetTransform)
        {
            _targetTransform = targetTransform;
        }
    }
}

