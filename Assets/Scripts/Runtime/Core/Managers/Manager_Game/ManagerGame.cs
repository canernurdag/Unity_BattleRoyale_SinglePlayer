using Assets.Scripts.Runtime.Core.Zenject.Signals.Game;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Managers.Manager_Game
{
    public sealed class ManagerGame : MonoBehaviour
    {
        private SignalBus _signalBus;

        /// <summary>
        /// The one of the most important variable in the system. The most logics depends on this.
        /// </summary>
        private GameStateType _currentGameStateType;
        public GameStateType CurrentGameStateType => _currentGameStateType;


        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }



        private void Awake()
        {
            SetQualityAndFPS();
        }

        /// <summary>
        /// This function can be extended by giving specific values according to target platform.
        /// Besides, this function can be linked to "Ui Settings" and Save System
        /// </summary>
        private void SetQualityAndFPS()
        {
#if UNITY_ANDROID || UNITY_IOS
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
#endif
        }


        /// <summary>
        /// Set the CurrentGameStateType here.
        /// In addition, this function rises "OnGameStateChange" event to notify all systems in the games.
        /// </summary>
        /// <param name="gameStateType"></param>
        public void SetCurrentGameStateType(GameStateType gameStateType)
        {
            _currentGameStateType = gameStateType;
            _signalBus.Fire(new SignalGameStateChanged(gameStateType));

        }


    }

}

