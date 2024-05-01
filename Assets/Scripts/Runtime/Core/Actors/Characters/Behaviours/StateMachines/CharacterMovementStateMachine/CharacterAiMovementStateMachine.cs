using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterMovement;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.StateMachines.CharacterMovementStateMachine
{
    public class CharacterAiMovementStateMachine : CharacterMovementStateMachine
    {
        private CharacterMovementAi _characterMovementAi;
        public CharacterMovementAi CharacterMovementAi => _characterMovementAi;
        private NavMeshAgent _navMeshAgent;
        public NavMeshAgent NavMeshAgent => _navMeshAgent;


        [Inject]
        public void Construct(NavMeshAgent navMeshAgent, CharacterMovementAi characterMovementAi)
        {
            _navMeshAgent = navMeshAgent;
            _characterMovementAi = characterMovementAi;
        }



    }
}

