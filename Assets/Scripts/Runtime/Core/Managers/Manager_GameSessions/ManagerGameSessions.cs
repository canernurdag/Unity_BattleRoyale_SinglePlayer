using Assets.Scripts.Runtime.Core.User_;
using Assets.Scripts.Runtime.Core.Zenject.Signals.User_;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Managers.Manager_GameSessions
{
    public class ManagerGameSessions : MonoBehaviour
    {
        #region DI REFERENCES
        private GameSessionSettings _gameSessionSettings;
        #endregion


        #region VAR
        private GameSession _activeGameSession = null;
        public GameSession ActiveGameSession => _activeGameSession;

        #endregion

        public IObservable<bool> ObservableIfTeamsAreReady { get; private set; }

        [Inject]
        public void Construct(GameSessionSettings gameSessionSettings)
        {
            _gameSessionSettings = gameSessionSettings;
        }

        private void Awake()
        {
            SetActiveGameSession(null);


            ObservableIfTeamsAreReady = this.UpdateAsObservable()
                .Where(_ => ActiveGameSession != null)
                .Select(_ => ActiveGameSession.Teams.All(x => x.Users.Count == ActiveGameSession.UserCountPerTeam));
        }

        public GameSession CreateGameSession(GameSession.GameType selectedGameType, GameSession.MapType selectedMapType)
        {
            GameSession gameSession = new GameSession();
            gameSession.SetSelectedGameType(selectedGameType);
            gameSession.SetSelectedMapType(selectedMapType);
            return gameSession;

        }

        public void SyncLobbyRooms()
        {
            //TBF for multiplayer solution
        }

        #region SET FUNCTIONS


        public void SetActiveGameSession(GameSession gameSession)
        {
            _activeGameSession = gameSession;
        }

        public void JoinUserToGameSession(SignalUserJoinToTheGameSession signalUserJoinToTheGameSession)
        {
            JoinUserToGameSession(signalUserJoinToTheGameSession.GameSession, signalUserJoinToTheGameSession.User);
        }

        public void LeaveUserFromGameSession(SignalUserLeaveFromTheGameSession signalUserLeaveFromTheGameSession)
        {
            LeaveUserFromGameSession(signalUserLeaveFromTheGameSession.GameSession, signalUserLeaveFromTheGameSession.User);
        }

        private void JoinUserToGameSession(GameSession gameSession, User user)
        {
            gameSession.AddUserToJoinedUsers(user);
            gameSession.PlaceUserToATeamRandomly(user);
        }

        private void LeaveUserFromGameSession(GameSession gameSession, User user)
        {
            gameSession.RemoveUserFromJoinedUsers(user);
            gameSession.RemoveUserFromTeams(user);

        }



        #endregion






    }
}