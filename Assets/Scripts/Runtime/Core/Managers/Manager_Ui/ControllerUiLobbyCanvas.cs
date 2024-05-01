using Assets.Scripts.Runtime.Core.Managers.Manager_GameSessions;
using Assets.Scripts.Runtime.Core.Managers.Manager_Sound;
using Assets.Scripts.Runtime.Core.Ui;
using Assets.Scripts.Runtime.Systems.UiSystem.Enum;
using Assets.Scripts.Runtime.Systems.UiSystem.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Managers.Manager_Ui
{
    public class ControllerUiLobbyCanvas : MonoBehaviour
    {
        #region DIRECT REF
        [SerializeField] private Button _exitButton, _createButton, _joinButton;
        [SerializeField] private AudioClip _buttonClickSFX, _createButtonClickSFX, _joinButtonClickSFX;
        #endregion

        #region DI REF
        private UiCanvasSystem _uiCanvasSystem;
        private ManagerGameSessions _managerGameSession;
        private ManagerSound _managerSound;
        #endregion

        public UiSlot_LobbyRoom SelectedUiLobbyRoom { get; private set; }

        [Inject]
        public void Construct(UiCanvasSystem uiCanvasSystem, ManagerGameSessions managerGameSessions,
            ManagerSound managerSound)
        {
            _uiCanvasSystem = uiCanvasSystem;
            _managerGameSession = managerGameSessions;
            _managerSound = managerSound;
        }

        private void OnEnable()
        {
            _managerGameSession.SyncLobbyRooms();
        }
        private void Start()
        {

            _joinButton.interactable = false;

            _exitButton.onClick.AddListener(() =>
            {
                _uiCanvasSystem.SetActivenessOfAllCanvases(false);
                _uiCanvasSystem.SetActivenessOfSpecificCanvas(CanvasType.CanvasHome, true);
                _managerSound.PlayAudioClipInPoint(_buttonClickSFX, Vector3.zero);
            });

            _createButton.onClick.AddListener(() =>
            {
                _uiCanvasSystem.SetActivenessOfAllCanvases(false);
                _uiCanvasSystem.SetActivenessOfSpecificCanvas(CanvasType.CanvasLobbyHost, true);
                _managerSound.PlayAudioClipInPoint(_createButtonClickSFX, Vector3.zero);
            });

            _joinButton.onClick.AddListener(() =>
            {
                if (!SelectedUiLobbyRoom) return;

                //GameSession createdGameSession = _managerGameSession.CreateGameSession(_currentGameType, _currentMapType);
                //_managerGameSession.SetActiveGameSession(createdGameSession);

                _uiCanvasSystem.SetActivenessOfAllCanvases(false);
                _uiCanvasSystem.SetActivenessOfSpecificCanvas(CanvasType.CanvasMatchMaking, true);
                _managerSound.PlayAudioClipInPoint(_joinButtonClickSFX, Vector3.zero);
            });
        }

        public void SetSelectedUiLobbyRoom(UiSlot_LobbyRoom uiLobbyRoom)
        {
            SelectedUiLobbyRoom = uiLobbyRoom;
            _joinButton.interactable = true;
        }

    }
}