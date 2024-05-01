using Assets.Scripts.Runtime.Core.Managers.Manager_Game;
using Assets.Scripts.Runtime.Core.Managers.Manager_Save;
using Assets.Scripts.Runtime.Core.Ui.ControllerCanvasButton.Base;
using Assets.Scripts.Runtime.Systems.Sound_System;
using Assets.Scripts.Runtime.Systems.UiSystem;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Ui.ControllerCanvasButton
{
    [DefaultExecutionOrder(50)]
    public class ControllerUiButtonSettings : ControllerCanvasButtons
    {
        private CanvasUi _canvasUi;
        // private ControllerOpenCloseSettingCanvas _controllerOpenCloseSettingCanvas;
        private SoundSystem _soundSystem;

        [SerializeField] private UiToggle _soundToggle, _vibrationToggle;

        private ManagerSave _managerSave;
        private ManagerGame _managerGame;
        private void Awake()
        {
            _canvasUi = GetComponent<CanvasUi>();
            // _controllerOpenCloseSettingCanvas = GetComponent<ControllerOpenCloseSettingCanvas>();
        }

        private void Start()
        {
            // _managerSave = ManagerServiceLocator.Instance.ServiceLocatorSystem.GetService<ManagerSave>();
            //_soundToggle.SetUiToggleVisual(_managerSave.SaveSystem.SaveState.IsSoundOn);
            //_vibrationToggle.SetUiToggleVisual(_managerSave.SaveSystem.SaveState.IsVibrationOn );
            // _soundSystem = ManagerServiceLocator.Instance.ServiceLocatorSystem.GetService<ManagerSound>().SoundSystem;
        }

        public void SoundToggleButtonFunction()
        {
            //bool isSoundOn = _managerSave.SaveSystem.SaveState.IsSoundOn;

            //_managerSave.SaveSystem.SaveState.IsSoundOn = !isSoundOn;
            //_managerSave.SaveSystem.Save();

            //_soundToggle.SetUiToggleVisual(_managerSave.SaveSystem.SaveState.IsSoundOn );
        }

        public void VibrationToggleButtonFunction()
        {
            // _soundSystem.PlaySoundAtPoint(Vector3.zero, _soundsUiDataSo.ButtonToggleSFX, 1);  

            bool isVibrationOn = (bool)_managerSave.SaveSystem.SaveState.IsVibrationOn;

            _managerSave.SaveSystem.SaveState.IsVibrationOn = !isVibrationOn;
            _managerSave.SaveSystem.Save();

            _vibrationToggle.SetUiToggleVisual(_managerSave.SaveSystem.SaveState.IsVibrationOn);
        }

        public void ExitButtonFunction()
        {
            // if (_controllerOpenCloseSettingCanvas)
            // {
            //    _controllerOpenCloseSettingCanvas.CloseUiCanvas(_canvasUi.CanvasUiParts);
            //    _managerGame.SetCurrentGameStateType(GameStateType.LevelOpened);
            //    
            //    // _soundSystem.PlaySoundAtPoint(Vector3.zero, _soundsUiDataSo.ButtonExitSFX, 1);
            // }
        }

        public void PrivacyPolicyButtonFunction()
        {
            Application.OpenURL("http://github.com/canernurdag");
        }
    }

}
