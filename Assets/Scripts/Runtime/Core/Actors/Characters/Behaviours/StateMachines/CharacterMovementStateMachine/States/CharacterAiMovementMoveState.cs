using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Runtime.Systems.StateMachineSystem;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.StateMachines.CharacterMovementStateMachine.States
{
    public class CharacterAiMovementMoveState : CharacterMovementMoveState
    {
        private CharacterAiMovementStateMachine _characterAiMovementStateMachine;

        public CharacterAiMovementMoveState(BaseStateMachineSystem stateMachineSystem) : base(stateMachineSystem)
        {
            _characterAiMovementStateMachine = stateMachineSystem as CharacterAiMovementStateMachine;
        }

        public override IEnumerator EnterState()
        {
            yield return null;
            _characterAiMovementStateMachine.NavMeshAgent.isStopped = false;
        }


    }
}