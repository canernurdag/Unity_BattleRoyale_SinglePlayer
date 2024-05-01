using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Runtime.Core.Managers.Manager_Game;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Zenject.Signals.Game
{
    public class SignalGameStateChanged
    {
        public GameStateType GameStateType;

        public SignalGameStateChanged(GameStateType gameStateType)
        {
            GameStateType = gameStateType;
        }
    }
}