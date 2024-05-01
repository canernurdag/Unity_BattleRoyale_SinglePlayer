using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Assets.Scripts.Runtime.Core.Actors.Characters.Character_.GamePlay;
using Assets.Scripts.Runtime.Core.Managers.Manager_Cinemachine;
using Assets.Scripts.Runtime.Core.Managers.Manager_Game;
using Assets.Scripts.Runtime.Core.Managers.Manager_GameSessions;
using Assets.Scripts.Runtime.Core.Managers.Manager_Input;
using Assets.Scripts.Runtime.Core.Managers.Manager_Network;
using Assets.Scripts.Runtime.Core.Managers.Manager_Save;
using Assets.Scripts.Runtime.Core.Managers.Manager_Scene;
using Assets.Scripts.Runtime.Core.Managers.Manager_Sound;
using Assets.Scripts.Runtime.Core.Managers.Manager_Ui;
using Assets.Scripts.Runtime.Core.Team_;
using Assets.Scripts.Runtime.Core.Zenject.Signals.Game;
using Assets.Scripts.Runtime.Systems.CinemachineSystem;
using Assets.Scripts.Runtime.Systems.UiSystem.Enum;
using Assets.Scripts.Runtime.Systems.UiSystem.Systems;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEditor;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Runtime.Core.Controllers.Level
{
    public class ControllerMatch : MonoBehaviour
    {
        #region DI REF
        private ManagerGame _managerGame;
        private ManagerGameSessions _managerGameSessions;
        private CharacterGameplayAi.CharacterGameplayAiFactory _characterAiGameplayFactory;
        private CharacterGameplayPlayer.CharacterGameplayPlayerFactory _characterGameplayPlayerFactory;
        public List<TeamScene> TeamScenes { get; private set; } = new();
        private ControllerUiCountdownCanvas _controllerUiCountdownCanvas;
        private UiCanvasSystem _uiCanvasSystem;
        private ControllerUiGameCanvas _controllerUiGameCanvas;
        private ManagerNetwork _managerNetwork;
        private ManagerSave _managerSave;
        private ControllerUiGameEndingCanvas _controllerUiGameEnding;
        private ManagerScene _managerScene;
        private ManagerCinemachine _managerCinemachine;
        private ManagerInput _managerInput;
        private ManagerSound _managerSound;
        private GhostObjectForCinemachineTarget _ghostObjectForCinemachineTarget;
        #endregion

        public CharacterGameplay OwnerCharacterGameplay { get; private set; }

        #region DIRECT REF
        public List<Transform> Waypoints = new();
        #endregion


        #region COUNTDOWN
        private float _initDuration;
        private float _matchDuration;
        private Tween _countDownTween;
        #endregion

        [Inject]
        public void Construct(ManagerGame managerGame,
            CharacterGameplayAi.CharacterGameplayAiFactory characterAiFactory, CharacterGameplayPlayer.CharacterGameplayPlayerFactory characterPlayerFactory,
            TeamScene[] teamScenes, ManagerGameSessions managerGameSessions,
            ControllerUiCountdownCanvas controllerUiCountdownCanvas, UiCanvasSystem uiCanvasSystem, ControllerUiGameCanvas controllerUiGameCanvas,
            ManagerNetwork managerNetwork, ManagerSave managerSave, ControllerUiGameEndingCanvas controllerUiGameEnding,
            ManagerScene managerScene, ManagerCinemachine managerCinemachine, ManagerInput managerInput, ManagerSound managerSound,
            GhostObjectForCinemachineTarget ghostObjectForCinemachineTarget)
        {
            _managerGame = managerGame;
            _managerGameSessions = managerGameSessions;
            _characterAiGameplayFactory = characterAiFactory;
            _characterGameplayPlayerFactory = characterPlayerFactory;
            TeamScenes = teamScenes.ToList();
            _controllerUiCountdownCanvas = controllerUiCountdownCanvas;
            _uiCanvasSystem = uiCanvasSystem;
            _controllerUiGameCanvas = controllerUiGameCanvas;
            _managerNetwork = managerNetwork;
            _managerSave = managerSave;
            _controllerUiGameEnding = controllerUiGameEnding;
            _managerScene = managerScene;
            _managerCinemachine = managerCinemachine;
            _managerInput = managerInput;
            _managerSound = managerSound;
            _ghostObjectForCinemachineTarget = ghostObjectForCinemachineTarget;
        }

        private void Awake()
        {
            InitTeamsToTheScene();
            InitCharacters();
            InitCinemachine();
            InitInputCorrection();
            InitUi();
   
        }

        private void InitInputCorrection()
        {
            var teamSceneOwner = OwnerCharacterGameplay.TeamScene;
            var correctionValue = teamSceneOwner.InputCorrectionValue;
            _managerInput.SetInputCorrectionAccordingToCameraAngle(correctionValue);
        }

        private void InitCinemachine()
        {
            _managerCinemachine.AdjustCameraPositionsAndRotations(OwnerCharacterGameplay.TeamScene);
             gameObject.UpdateAsObservable()
             .Subscribe(_ =>
             {
                 if (OwnerCharacterGameplay != null)
                 {
                     _ghostObjectForCinemachineTarget.SetTargetTransform(OwnerCharacterGameplay.transform);
                 }
             });
        }

        private void InitUi()
        {
            _controllerUiGameCanvas.SetScoreTextOwnerTeam(0);
            _controllerUiGameCanvas.SetScoreTextCounterTeam(0);

            _initDuration = _managerGameSessions.ActiveGameSession.Duration;
            _matchDuration = _initDuration;

            var timerValue = _controllerUiGameCanvas.ConvertSecondsToTimerFormat(_matchDuration);
            _controllerUiGameCanvas.SetTimerText(timerValue);
        }

        private void OnDisable()
        {
            //DISPOSE BOTS FROM ACTIVE USERS
            var joinedUsers = _managerGameSessions.ActiveGameSession.JoinedUsers;
            for (int i = 0; i < joinedUsers.Count; i++)
            {
                var user = joinedUsers[i];
                if (user.Userdata.UserId == 1) continue;

                _managerNetwork.RemoveActiveUser(user.Userdata.UserId);
            }

            //DISPOSE THE GAMESESSION
            _managerGameSessions.SetActiveGameSession(null);
        }

        private IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            MatchStartSequence();
        }

        private void MatchStartSequence()
        {
            _uiCanvasSystem.SetActivenessOfSpecificCanvas(CanvasType.CanvasCountDown, true);
            _controllerUiCountdownCanvas.StartCountDownSequence(() =>
            {
                _managerGame.SetCurrentGameStateType(GameStateType.GameStarted);
                _uiCanvasSystem.SetActivenessOfSpecificCanvas(CanvasType.CanvasCountDown, false);
            });
        }

        public void StartCountdown(SignalGameStateChanged signalGameStateChanged)
        {
            if (signalGameStateChanged.GameStateType != GameStateType.GameStarted) return;
            _countDownTween?.Kill();
            _countDownTween = DOTween.To(
                () => _matchDuration,
                x => _matchDuration = x,
                0
                , _initDuration)

                .SetDelay(1.5f)
                .OnUpdate(() =>
                {
                    if (Time.frameCount % 30 == 0)
                    {
                        var timerValue = _controllerUiGameCanvas.ConvertSecondsToTimerFormat(_matchDuration);
                        _controllerUiGameCanvas.SetTimerText(timerValue);

                        if (_matchDuration < 10)
                        {
                            if (!_controllerUiGameCanvas.IsParenTimerScaleOn)
                            {
                                _controllerUiGameCanvas.StartTimerParentScaleTween();
                            }

                            var matchDuration = Mathf.RoundToInt(_matchDuration);
                            if (matchDuration % 2 == 0)
                            {
                                _controllerUiGameCanvas.SetTimerTextColor(Color.white);
                            }
                            else if (matchDuration % 2 == 1)
                            {
                                _controllerUiGameCanvas.SetTimerTextColor(Color.red);
                            }
                        }
                    }

                })
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {

                    //RESET UI
                    _controllerUiGameCanvas.SetTimerTextColor(Color.white);
                    _controllerUiGameCanvas.ResetTimerParent();

                    _managerGame.SetCurrentGameStateType(GameStateType.GameFinished);

                    // END MATCH HERE
                    var ownerTeamScore = OwnerCharacterGameplay.Team.Score;

                    var counterTeam = TeamScenes.First(x => x.Team != OwnerCharacterGameplay.Team).Team;
                    var counterTeamScore = counterTeam.Score;

                    _uiCanvasSystem.SetActivenessOfSpecificCanvas(CanvasType.CanvasGameEnding, true);
                    if (ownerTeamScore > counterTeamScore)
                    {
                        //WIN HERE
                        _controllerUiGameEnding.GameEndingSequence("WIN", Color.blue, () =>
                        {
                            _managerScene.OpenTargetScene(true, 1);
                        });
                    }
                    else if (ownerTeamScore < counterTeamScore)
                    {
                        //LOSE HERE
                        _controllerUiGameEnding.GameEndingSequence("DEFEAT", Color.red, () =>
                        {
                            _managerScene.OpenTargetScene(true, 1);
                        });
                    }
                    else if (ownerTeamScore == counterTeamScore)
                    {
                        //DRAW HERE
                        _controllerUiGameEnding.GameEndingSequence("DEFEAT", Color.white, () =>
                        {
                            _managerScene.OpenTargetScene(true, 1);
                        });
                    }


                });

        }

        private void InitTeamsToTheScene()
        {
            var gameSession = _managerGameSessions.ActiveGameSession;

            for (int i = 0; i < gameSession.Teams.Count; i++)
            {
                var team = gameSession.Teams[i];
                var teamScene = TeamScenes[i];
                var counterTeamScene = TeamScenes.First(x => x != teamScene);

                teamScene.SetTeam(team);

                for (int j = 0; j < team.Users.Count; j++)
                {
                    var user = team.Users[j];
                    bool isPlayer = !user.Userdata.IsBot;

                    CharacterGameplay characterGameplay = null;

                    if (isPlayer)
                    {
                        var characterPlayer = _characterGameplayPlayerFactory.Create();
                        characterGameplay = characterPlayer;

                        if (user.Userdata.UserId == _managerSave.SaveSystem.SaveState.UserId)
                        {
                            OwnerCharacterGameplay = characterGameplay;
                        }


                    }
                    else if (!isPlayer)
                    {
                        var characterAi = _characterAiGameplayFactory.Create();
                        characterGameplay = characterAi;
                    }

                    characterGameplay.SetUser(user);
                    characterGameplay.SetTeam(team);
                    characterGameplay.SetTeamScene(teamScene);
                    characterGameplay.SetCounterTeamScene(counterTeamScene);
                    characterGameplay.transform.SetParent(teamScene.ParentTransformForCharacters);

                    teamScene.TeamCharacterGameplays.Add(characterGameplay);

                    var spawnPoint = teamScene.GetUnusedSpawnPointRandomly();
                    if (spawnPoint)
                    {
                        spawnPoint.UsingCharacter = characterGameplay;
                        characterGameplay.SetTeamSpawnPoint(spawnPoint);
                        characterGameplay.transform.position = spawnPoint.transform.position;
                        characterGameplay.transform.rotation = Quaternion.Euler(spawnPoint.ForwardRotationAngle);
                    }
                    else if (!spawnPoint)
                    {
                        Debug.Log("Not Enough Scene Object - bug");
                    }

                }
            }
        }

        private void InitCharacters()
        {

            for (int i = 0; i < TeamScenes.Count; i++)
            {
                var teamScene = TeamScenes[i];

                for (int j = 0; j < teamScene.TeamCharacterGameplays.Count; j++)
                {
                    var character = teamScene.TeamCharacterGameplays[j];
                    character.InitCharacter();
                }

            }
        }

        public Transform GetRandomWaypoint()
        {
            return Waypoints[Random.Range(0, Waypoints.Count)];
        }


    }

}
