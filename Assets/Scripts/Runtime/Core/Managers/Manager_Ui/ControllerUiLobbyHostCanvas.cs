using Assets.Scripts.Runtime.Core.Managers.Manager_GameSessions;
using Assets.Scripts.Runtime.Core.Managers.Manager_Save;
using Assets.Scripts.Runtime.Core.Managers.Manager_Sound;
using Assets.Scripts.Runtime.Core.User_;
using Assets.Scripts.Runtime.Core.Zenject.Signals.User_;
using Assets.Scripts.Runtime.Systems.UiSystem.Enum;
using Assets.Scripts.Runtime.Systems.UiSystem.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Managers.Manager_Ui
{
    public class ControllerUiLobbyHostCanvas : MonoBehaviour
    {
        #region DIRECT REF
        [SerializeField] private Button _exitButton, _createButton;
        [SerializeField] private Button _previousButtonGameType, _nextButtonGameType;
        [SerializeField] private Button _previousButtonMapType, _nextButtonMapType;

        [SerializeField] private TMP_Text _gameTypeText, _mapTypeText;
        [SerializeField] private TMP_InputField _lobbyName;

        private GameSession.GameType _currentGameType;
        private GameSession.MapType _currentMapType;
        [SerializeField] private AudioClip _buttonClickSFX, _createButtonClickSFX;
        #endregion

        #region DI REF
        private UiCanvasSystem _uiCanvasSystem;
        private ManagerGameSessions _managerGameSessions;
        private GameSessionSettings _gameSessionSettings;
        private ManagerSave _managerSave;
        private SignalBus _signalBus;
        private ManagerSound _managerSound;
        #endregion

        private List<GameSession.GameType> GameTypes = new();
        private List<GameSession.MapType> MapTypes = new();

        [Inject]
        public void Construct(UiCanvasSystem uiCanvasSystem, ManagerGameSessions managerGameSessions, GameSessionSettings gameSessionSettings,
            ManagerSave managerSave, SignalBus signalBus, ManagerSound managerSound)
        {
            _uiCanvasSystem = uiCanvasSystem;
            _managerGameSessions = managerGameSessions;
            _gameSessionSettings = gameSessionSettings;
            _managerSave = managerSave;
            _signalBus = signalBus;
            _managerSound = managerSound;
        }


        private void Awake()
        {
            GameTypes = Enum.GetValues(typeof(GameSession.GameType)).Cast<GameSession.GameType>().ToList();
            MapTypes = Enum.GetValues(typeof(GameSession.MapType)).Cast<GameSession.MapType>().ToList();

        }

        private void OnEnable()
        {
            _currentGameType = GameTypes[0];
            _currentMapType = MapTypes[0];

            SetGameTypeText(_currentGameType.ToString());
            SetMapTypeText(_currentMapType.ToString());
        }

        private void Start()
        {
            _exitButton.onClick.AddListener(() =>
            {
                _managerGameSessions.SetActiveGameSession(null);

                _uiCanvasSystem.SetActivenessOfAllCanvases(false);
                _uiCanvasSystem.SetActivenessOfSpecificCanvas(CanvasType.CanvasLobby, true);
                _managerSound.PlayAudioClipInPoint(_buttonClickSFX, Vector3.zero);
            });

            _createButton.onClick.AddListener(() =>
            {
                bool isGameSessionDataExistInSO = _gameSessionSettings.GameSessionDatas.Any(x => x.GameType == _currentGameType);

                if (isGameSessionDataExistInSO)
                {
                    //GET DATA FROM SO
                    var gameSessionDataFromSO = _gameSessionSettings.GameSessionDatas.First(x => x.GameType == _currentGameType);

                    // CREATE GAME SESSION
                    GameSession createdGameSession = _managerGameSessions.CreateGameSession(_currentGameType, _currentMapType);
                    createdGameSession.SetSelectedGameType(_currentGameType);
                    createdGameSession.SetSelectedMapType(_currentMapType);
                    createdGameSession.SetDuration(gameSessionDataFromSO.Duration);
                    createdGameSession.SetDurationForCharacterRivive(gameSessionDataFromSO.DurationForCharacterRivive);
                    createdGameSession.SetUserCountPerTeam(gameSessionDataFromSO.UserCountPerTeam);
                    _managerGameSessions.SetActiveGameSession(createdGameSession);

                    //CREATE HOST DATA
                    var hostUserData = new UserData();
                    hostUserData.UserId = (int)_managerSave.SaveSystem.SaveState.UserId;
                    hostUserData.UserName = _managerSave.SaveSystem.SaveState.UserName;
                    hostUserData.CharacterVisualType = (Actors.Characters.Behaviours.CharacterVisual.CharacterVisual.Type)_managerSave.SaveSystem.SaveState.SelectedCharacterVisualType;
                    hostUserData.WeaponType = (Actors.Weapons.Weapon.Type)_managerSave.SaveSystem.SaveState.SelectedWeaponType;
                    hostUserData.IsBot = false;
                    var hostUser = new User(hostUserData);

                    //ASSIGN HOST
                    _signalBus.Fire(new SignalUserJoinToTheGameSession(createdGameSession, hostUser));


                    //UI CHANGE
                    _uiCanvasSystem.SetActivenessOfAllCanvases(false);
                    _uiCanvasSystem.SetActivenessOfSpecificCanvas(CanvasType.CanvasMatchMaking, true);

                    //SFX
                    _managerSound.PlayAudioClipInPoint(_createButtonClickSFX, Vector3.zero);
                }
                else if (!isGameSessionDataExistInSO)
                {
                    Debug.Log("Data does not exit in SO");
                }


            });

            _previousButtonGameType.onClick.AddListener(() =>
            {

                var currentIndex = GameTypes.IndexOf(_currentGameType);
                int previousIndex = 0;
                if (currentIndex == 0)
                {
                    previousIndex = GameTypes.Count - 1;
                }
                else
                {
                    previousIndex = currentIndex - 1;
                }

                _currentGameType = GameTypes[previousIndex];
                SetGameTypeText(_currentGameType.ToString());

                _managerSound.PlayAudioClipInPoint(_buttonClickSFX, Vector3.zero);

            });

            _nextButtonGameType.onClick.AddListener(() =>
            {

                var currentIndex = GameTypes.IndexOf(_currentGameType);
                int nextIndex = 0;
                if (currentIndex == GameTypes.Count - 1)
                {
                    nextIndex = 0;
                }
                else
                {
                    nextIndex = currentIndex + 1;
                }

                _currentGameType = GameTypes[nextIndex];
                SetGameTypeText(_currentGameType.ToString());

                _managerSound.PlayAudioClipInPoint(_buttonClickSFX, Vector3.zero);
            });

            _previousButtonMapType.onClick.AddListener(() =>
            {

                var currentIndex = MapTypes.IndexOf(_currentMapType);
                int previousIndex = 0;
                if (currentIndex == 0)
                {
                    previousIndex = MapTypes.Count - 1;
                }
                else
                {
                    previousIndex = currentIndex - 1;
                }

                _currentMapType = MapTypes[previousIndex];
                SetMapTypeText(_currentMapType.ToString());

                _managerSound.PlayAudioClipInPoint(_buttonClickSFX, Vector3.zero);
            });

            _nextButtonMapType.onClick.AddListener(() =>
            {

                var currentIndex = MapTypes.IndexOf(_currentMapType);
                int nextIndex = 0;
                if (currentIndex == MapTypes.Count - 1)
                {
                    nextIndex = 0;
                }
                else
                {
                    nextIndex = currentIndex + 1;
                }

                _currentMapType = MapTypes[nextIndex];
                SetMapTypeText(_currentMapType.ToString());

                _managerSound.PlayAudioClipInPoint(_buttonClickSFX, Vector3.zero);
            });
        }

        private void SetGameTypeText(string gameTypeString)
        {
            _gameTypeText.text = gameTypeString;
        }

        private void SetMapTypeText(string mapTypeString)
        {
            _mapTypeText.text = mapTypeString;
        }
    }
}