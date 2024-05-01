using DG.Tweening;
using UnityEngine;
using Assets.Scripts.Runtime.Core.Managers.Manager_Scene;
using Assets.Scripts.Runtime.Core.Ui.ControllerCanvasButton.Base;
using Assets.Scripts.Runtime.Systems.Sound_System;

namespace Assets.Scripts.Runtime.Core.Ui.ControllerCanvasButton
{
    public class ControllerUiButtonSuccess : ControllerCanvasButtons
    {
        // [SerializeField] private SoundsUiDataSO _soundsUiDataSo;
        private SoundSystem _soundSystem;
        private ManagerScene _managerLevel;

        private void Start()
        {
            // _soundSystem = ManagerServiceLocator.Instance.ServiceLocatorSystem.GetService<ManagerSound>().SoundSystem;
        }

        public void NextButtonFunction()
        {
            // _soundSystem.PlaySoundAtPoint(Vector3.zero, _soundsUiDataSo.ButtonSelectSFX, 1);
            //DOVirtual.DelayedCall(0.5f, () => _managerLevel.OpenNextLevelScene(true));

        }
    }
}

