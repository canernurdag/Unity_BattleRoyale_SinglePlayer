using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Assets.Scripts.Runtime.Core.Managers.Manager_Save;
using Assets.Scripts.Runtime.Systems.UiSystem.Systems;
using Assets.Scripts.Runtime.Systems.UiSystem.Enum;
using Assets.Scripts.Runtime.Core.Zenject.Signals.Player;

namespace Assets.Scripts.Runtime.Core.Managers.Manager_Ui
{
    public class ControllerUiPlayerChangeName : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _newUserNameInputField;
        [SerializeField] private Button _newUsernameButton, _exitButton;

        private UiCanvasSystem _uiCanvasSystem;
        private SignalBus _signalBus;
        private ManagerSave _managerSave;

        [Inject]
        public void Construct(UiCanvasSystem uiCanvasSystem, SignalBus signalBus, ManagerSave managerSave)
        {
            _uiCanvasSystem = uiCanvasSystem;
            _signalBus = signalBus;
            _managerSave = managerSave;
        }

        private void Start()
        {
            SetButtonFunctions();
        }

        private void SetButtonFunctions()
        {
            _exitButton.onClick.AddListener(() =>
            {
                _uiCanvasSystem.SetActivenessOfAllCanvases(false);
                _uiCanvasSystem.SetActivenessOfSpecificCanvas(CanvasType.CanvasCharacterList, true);
            });

            _newUsernameButton.onClick.AddListener(() =>
            {
                string newUserNameString = _newUserNameInputField.text;
                if (newUserNameString.Length == 0) return;
                if (newUserNameString.Length > 20) return;

                SetPlayerName(newUserNameString);

            });
        }

        public void SetPlayerName(string newUserName)
        {
            _managerSave.SaveSystem.SaveState.UserName = newUserName;
            _signalBus.Fire(new SignalPlayerNameSet(newUserName));
        }
    }
}