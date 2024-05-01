using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Assets.Scripts.Runtime.Core.Managers.Manager_Scene;
using Assets.Scripts.Runtime.Core.Ui.ControllerCanvasButton.Base;
using Assets.Scripts.Runtime.Systems.Sound_System;

namespace Assets.Scripts.Runtime.Core.Ui.ControllerCanvasButton
{
    public class ControllerUiButtonFail : ControllerCanvasButtons
    {
        // [SerializeField] private SoundsUiDataSO _soundsUiDataSo;
        private SoundSystem _soundSystem;
        private ManagerScene _managerLevel;


        public void ReplayButtonFunction()
        {

            // _soundSystem.PlaySoundAtPoint(Vector3.zero, _soundsUiDataSo.ButtonSelectSFX, 1);
            DOVirtual.DelayedCall(0.5f, () => _managerLevel.OpenTheSameScene(true));

        }
    }

}

