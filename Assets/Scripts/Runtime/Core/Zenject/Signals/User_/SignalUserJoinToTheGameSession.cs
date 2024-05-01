using Assets.Scripts.Runtime.Core.Managers.Manager_GameSessions;
using Assets.Scripts.Runtime.Core.User_;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Zenject.Signals.User_
{
    public class SignalUserJoinToTheGameSession
    {
        public GameSession GameSession;
        public User User;
        public SignalUserJoinToTheGameSession(GameSession gameSession, User user)
        {
            GameSession = gameSession;
            User = user;
        }
    }
}