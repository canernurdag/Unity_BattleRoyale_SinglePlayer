using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterVisual;
using Assets.Scripts.Runtime.Core.Actors.Weapons;
using Assets.Scripts.Runtime.Core.Managers.Manager_GameSessions;
using Assets.Scripts.Runtime.Core.Managers.Manager_Network;
using Assets.Scripts.Runtime.Core.Managers.Manager_Scene;
using Assets.Scripts.Runtime.Core.Managers.Manager_Sound;
using Assets.Scripts.Runtime.Core.Ui;
using Assets.Scripts.Runtime.Core.User_;
using Assets.Scripts.Runtime.Core.Zenject.Signals.User_;
using Assets.Scripts.Runtime.Systems.UiSystem.Enum;
using Assets.Scripts.Runtime.Systems.UiSystem.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Managers.Manager_Ui
{
    public class ControllerUiMatchmakingCanvas : MonoBehaviour
    {

        #region DIRECT REFERNCE
        [SerializeField] private Button _exitButton, _battleButton, _addBotButton;
        [SerializeField] private Transform _parentTransformTeam0, _parentTransformTeam1;
        [SerializeField] private AudioClip _buttonClickSFX, _battleButtonClickSFX;
        #endregion

        #region DEPENDENCY REFERENCES
        private ManagerGameSessions _managerMatchmaking;
        private UiCanvasSystem _uiCanvasSystem;
        private SignalBus _signalBus;
        private ManagerScene _managerScene;
        private ManagerNetwork _managerNetwork;
        private UiSlot_CharacterForMatchMaking.UiSlotCharacterForMatchMakingFactory _uiSlotCharacterForMatchMakingFactory;
        private ManagerSound _managerSound;
        #endregion


        private List<Weapon.Type> _weaponTypes = new();
        private List<CharacterVisual.Type> _characterVisualTypes = new();



        [Inject]
        public void Construct(ManagerGameSessions managerMatchmaking, UiCanvasSystem uiCanvasSystem,
            SignalBus signalBus, ManagerScene managerScene,
            UiSlot_CharacterForMatchMaking.UiSlotCharacterForMatchMakingFactory uiSlotCharacterForMatchMakingFactory,
            ManagerNetwork managerNetwork, ManagerSound managerSound)
        {
            _managerMatchmaking = managerMatchmaking;
            _uiCanvasSystem = uiCanvasSystem;
            _signalBus = signalBus;
            _uiSlotCharacterForMatchMakingFactory = uiSlotCharacterForMatchMakingFactory;
            _managerScene = managerScene;
            _managerNetwork = managerNetwork;
            _managerSound = managerSound;

        }

        private void OnEnable()
        {
            UpdateUiSlots();
        }

        private void Start()
        {
            _weaponTypes = Enum.GetValues(typeof(Weapon.Type)).Cast<Weapon.Type>().ToList();
            _characterVisualTypes = Enum.GetValues(typeof(CharacterVisual.Type)).Cast<CharacterVisual.Type>().ToList();

            UpdateUiSlots();

            SetButtonInteractability_RX();
            SetButtonFunctions();
        }

        public void UpdateUiSlots(SignalUserJoinToTheGameSession signalUserJoinToTheGameSession)
        {
            UpdateUiSlots();
        }

        public void UpdateUiSlots(SignalUserLeaveFromTheGameSession signalUserLeaveFromTheGameSession)
        {
            UpdateUiSlots();
        }
        private void UpdateUiSlots()
        {
            for (int i = 0; i < _parentTransformTeam0.childCount; i++)
            {
                var child = _parentTransformTeam0.GetChild(i);
                Destroy(child.gameObject);
            }

            for (int i = 0; i < _parentTransformTeam1.childCount; i++)
            {
                var child = _parentTransformTeam1.GetChild(i);
                Destroy(child.gameObject);
            }

            var activeGameSession = _managerMatchmaking.ActiveGameSession;
            for (int i = 0; i < activeGameSession.Teams.Count; i++)
            {
                var team = activeGameSession.Teams[i];
                for (int j = 0; j < team.Users.Count; j++)
                {
                    var user = team.Users[j];

                    var slot = _uiSlotCharacterForMatchMakingFactory.Create();
                    if (i == 0)
                    {
                        slot.transform.SetParent(_parentTransformTeam0);
                    }
                    else if (i == 1)
                    {
                        slot.transform.SetParent(_parentTransformTeam1);
                    }


                    slot.SetUser(user);
                }
            }
        }

        private void SetButtonInteractability_RX()
        {
            _managerMatchmaking.ObservableIfTeamsAreReady
                .Subscribe(isFull =>
                {
                    _battleButton.interactable = isFull;
                    _addBotButton.interactable = !isFull;
                });
        }

        private void SetButtonFunctions()
        {
            _exitButton.onClick.AddListener(() =>
            {
                _uiCanvasSystem.SetActivenessOfAllCanvases(false);
                _uiCanvasSystem.SetActivenessOfSpecificCanvas(CanvasType.CanvasLobby, true);
                _managerSound.PlayAudioClipInPoint(_buttonClickSFX, Vector3.zero);
            });

            _battleButton.onClick.AddListener(() =>
            {
                _uiCanvasSystem.SetActivenessOfAllCanvases(false);
                _managerScene.OpenTargetScene(true, 2);
                _managerSound.PlayAudioClipInPoint(_battleButtonClickSFX, Vector3.zero);
            });

            _addBotButton.onClick.AddListener(() =>
            {
                UserData userData = new UserData();
                userData.UserId = _managerNetwork.GetNewUniqueUserId();
                userData.UserName = $"BOT_{UnityEngine.Random.Range(0, 100)}";
                userData.CharacterVisualType = _characterVisualTypes[UnityEngine.Random.Range(0, _characterVisualTypes.Count)];
                userData.WeaponType = _weaponTypes[UnityEngine.Random.Range(0, _weaponTypes.Count)];
                userData.IsBot = true;

                User user = new User(userData);

                _signalBus.Fire(new SignalUserJoinToTheGameSession(_managerMatchmaking.ActiveGameSession, user));

                _managerSound.PlayAudioClipInPoint(_buttonClickSFX, Vector3.zero);

            });
        }

        #region SET FUNCTIONS

        #endregion
    }
}