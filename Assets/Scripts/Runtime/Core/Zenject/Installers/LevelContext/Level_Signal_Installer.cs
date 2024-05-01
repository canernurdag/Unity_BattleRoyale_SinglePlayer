using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterAttack;
using Assets.Scripts.Runtime.Core.Controllers.Level;
using Assets.Scripts.Runtime.Core.Zenject.Signals.Game;
using Assets.Scripts.Runtime.Core.Zenject.Signals.GamePlay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Zenject.Installers.LevelContext
{
    public class Level_Signal_Installer : MonoInstaller
    {
        public override void InstallBindings()
        {

            Container.DeclareSignal<SignalCharacterAttack>();


            Container.BindSignal<SignalCharacterAttack>()
                .ToMethod<CharacterAttackPlayer>(x => x.SetAttackState)
                .FromResolve();


            Container.BindSignal<SignalGameStateChanged>()
                .ToMethod<ControllerMatch>(x => x.StartCountdown)
                .FromResolve();




        }
    }
}