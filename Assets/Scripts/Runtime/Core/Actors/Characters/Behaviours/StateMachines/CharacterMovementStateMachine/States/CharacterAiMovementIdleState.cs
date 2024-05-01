using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Runtime.Systems.StateMachineSystem;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.StateMachines.CharacterMovementStateMachine.States
{
    public class CharacterAiMovementIdleState : CharacterMovementIdleState
    {
        private CharacterAiMovementStateMachine _characterAiMovementStateMachine;
        public CharacterAiMovementIdleState(BaseStateMachineSystem stateMachineSystem) : base(stateMachineSystem)
        {
            _characterAiMovementStateMachine = stateMachineSystem as CharacterAiMovementStateMachine;
        }

        public override IEnumerator EnterState()
        {
            while (_characterAiMovementStateMachine == null || _characterAiMovementStateMachine.NavMeshAgent == null)
            {
                yield return null;
            }


            if (_characterAiMovementStateMachine.NavMeshAgent.isActiveAndEnabled)
            {
                _characterAiMovementStateMachine.NavMeshAgent.isStopped = true;
            }

        }


    }
}