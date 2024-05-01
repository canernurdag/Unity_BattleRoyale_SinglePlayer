using Assets.Scripts.Runtime.Core.Managers.Manager_Save;
using Assets.Scripts.Runtime.Systems.UiSystem.Enum;
using Assets.Scripts.Runtime.Systems.UiSystem.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Managers.Manager_Ui
{
    public class ControllerUiSettingsCanvas : MonoBehaviour
    {
        #region DIRECT REF
        [SerializeField] private Button _exitButton;
        [SerializeField] private Slider _sfxSlider, _musicSlider;
        #endregion

        #region DI REF
        private UiCanvasSystem _uiCanvasSystem;
        private ManagerSave _managerSave;
        private SignalBus _signalBus;
        #endregion


        [Inject]
        public void Construct(ManagerSave managerSave, UiCanvasSystem uiCanvasSystem, SignalBus signalBus)
        {
            _managerSave = managerSave;
            _uiCanvasSystem = uiCanvasSystem;
            _signalBus = signalBus;
        }


        private void Start()
        {
            InitFromSaveSystem();


            _exitButton.onClick.AddListener(() =>
            {
                _uiCanvasSystem.SetActivenessOfAllCanvases(false);
                _uiCanvasSystem.SetActivenessOfSpecificCanvas(CanvasType.CanvasHome, true);
            });

            _sfxSlider.onValueChanged.AddListener(value =>
            {
                _managerSave.SaveSystem.SaveState.SFXLevel = value;
            });

            _musicSlider.onValueChanged.AddListener(value =>
            {
                _managerSave.SaveSystem.SaveState.MusicLevel = value;
                _signalBus.Fire(new SignalUserMusicLevelChanged(value));
            });
        }

        private void InitFromSaveSystem()
        {
            _sfxSlider.value = (float)_managerSave.SaveSystem.SaveState.SFXLevel;
            _musicSlider.value = (float)_managerSave.SaveSystem.SaveState.MusicLevel;
        }
    }
}