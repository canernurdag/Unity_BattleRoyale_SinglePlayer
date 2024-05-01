using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;
using Assets.Scripts.Runtime.Core.Managers.Manager_Scene;

namespace Assets.Scripts.Runtime.Core._Helpers
{
    public class GameStarter : MonoBehaviour
    {
        private ManagerScene _managerLevel;

        [Inject]
        public void Construct(ManagerScene managerLevel)
        {
            _managerLevel = managerLevel;
        }

        private void Start()
        {
            DOVirtual.DelayedCall(2f, () =>
            {
                _managerLevel.OpenTargetScene(true, 1);
            });
        }
    }
}