using Assets.Scripts.Runtime.Core.Managers.Manager_Game;
using Assets.Scripts.Runtime.Core.Managers.Manager_Ui;
using Assets.Scripts.Runtime.Core.Ui.ControllerCanvasButton.Base;
using Assets.Scripts.Runtime.Systems.Sound_System;
using Assets.Scripts.Runtime.Systems.UiSystem.Enum;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Ui.ControllerCanvasButton
{
    public class ControllerUiButtonInGameButtons : ControllerCanvasButtons
    {
        // [SerializeField] private SoundsUiDataSO _soundsUiDataSo;
        private SoundSystem _soundSystem;
        private ManagerUi _managerUi;
        private ManagerGame _managerGame;

        private void Start()
        {
            // _soundSystem = ManagerServiceLocator.Instance.ServiceLocatorSystem.GetService<ManagerSound>().SoundSystem;
        }
        public void SettingsButtonFunction()
        {
            _managerUi.ControllerUiCanvases.UiCanvasSystem.SetActivenessOfSpecificCanvas(CanvasType.CanvasSettings, true);

            _managerGame.SetCurrentGameStateType(GameStateType.GamePaused);

            // _soundSystem.PlaySoundAtPoint(Vector3.zero, _soundsUiDataSo.PopUpSFX, 1);
        }

    }
}

