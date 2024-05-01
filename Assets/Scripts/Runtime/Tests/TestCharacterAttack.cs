using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterAttack;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.StateMachines.CharacterAttackStateMachine;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.StateMachines.CharacterAttackStateMachine.States;
using Assets.Scripts.Runtime.Core.Interfaces.Character;
using Assets.Scripts.Runtime.Core.Zenject.Signals.GamePlay;
using Assets.Scripts.Runtime.Systems.StateMachineSystem;
using NSubstitute;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

namespace Assets.Scripts.Runtime.Tests
{
    public class TestCharacterAttack
    {
        [Test]
        public void CharacteAttackPlayer_WhenNoAmmo_ExpectAttackFails()
        {
            //ARRANGE 
            GameObject playerGO = new GameObject("Player");
            CharacterAttackPlayer characterAttackPlayer = playerGO.AddComponent<CharacterAttackPlayer>();
            characterAttackPlayer.TestSetAllAmmosEmpty();

            ICharacter character = Substitute.For<ICharacter>();
            characterAttackPlayer.TestSetCharacter(character);

            CharacterPlayerAttackStateMachine characterPlayerAttackStateMachine = playerGO.AddComponent<CharacterPlayerAttackStateMachine>();
            characterPlayerAttackStateMachine.SetCurrentState(new CharacterAttackIdleState(characterPlayerAttackStateMachine));

            //ACT
            var signalCharacterAttack = new SignalCharacterAttack(character, 0.1f);
            characterAttackPlayer.SetAttackState(signalCharacterAttack);

            //ASSERT
            Assert.AreNotEqual(characterPlayerAttackStateMachine.CurrentState.GetType(), typeof(CharacterAttackAttackState));
        }


    }
}