using Assets.Scripts.Runtime.Core.Managers.Manager_Save;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Ui
{
    public class UiToggle : MonoBehaviour
    {
        #region DIRECT REF
        [SerializeField] private GameObject _openVisual, _closeVisual;
        [SerializeField] private Button _toggleButton;
        #endregion

        #region DI REF
        private ManagerSave _managerSave;
        #endregion

        [Inject]
        public void Construct(ManagerSave managerSave)
        {
            _managerSave = managerSave;
        }

        private void Start()
        {
            InitToggleFromSaveSystem();

            _toggleButton.onClick.AddListener(() =>
            {
                Switch();
            });
            SetUiToggleVisual((bool)_managerSave.SaveSystem.SaveState.IsVibrationOn);

        }

        private void InitToggleFromSaveSystem()
        {
            SetUiToggleVisual((bool)_managerSave.SaveSystem.SaveState.IsVibrationOn);
        }

        public void Switch()
        {
            _managerSave.SaveSystem.SaveState.IsVibrationOn = !_managerSave.SaveSystem.SaveState.IsVibrationOn;

            SetUiToggleVisual((bool)_managerSave.SaveSystem.SaveState.IsVibrationOn);
        }
        public void SetUiToggleVisual(bool isOpen)
        {
            _openVisual.SetActive(isOpen);
            _closeVisual.SetActive(!isOpen);
        }

        internal void SetUiToggleVisual(bool? isVibrationOn)
        {
            throw new NotImplementedException();
        }
    }

}
