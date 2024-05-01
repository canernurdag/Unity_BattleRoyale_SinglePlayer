using Assets.Scripts.Runtime.Systems.StateMachineSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.StateMachines.CharacterMovementStateMachine.States
{
    public class CharacterMovementIdleState : BaseState
    {
        private CharacterMovementStateMachine _characterMovementStateMachine;
        public CharacterMovementIdleState(BaseStateMachineSystem stateMachineSystem) : base(stateMachineSystem)
        {
            _characterMovementStateMachine = stateMachineSystem as CharacterMovementStateMachine;
        }

        public override IEnumerator EnterState()
        {
            yield return null;
        }

        public override void ExecuteUpdate()
        {

        }

        public override void ExecuteFixedUpdate()
        {

        }

        public override void ExecuteLateUpdate()
        {

        }

        public override void ExitState()
        {

        }
    }
}