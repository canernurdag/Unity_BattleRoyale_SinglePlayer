using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.StateMachines.CharacterMovementStateMachine;
using Assets.Scripts.Runtime.Core.Interfaces.Character_;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterMovement
{
    public class CharacterMovementAi : CharacterMovement, IProvideVelocityValue
    {
        private CharacterAiMovementStateMachine _characterAiMovementStateMachine;
        private NavMeshAgent _navMeshAgent;

        public Transform _targetTransform { get; private set; }
        public Vector3 _targetPosition { get; private set; }



        [Inject]
        public void Construct(CharacterAiMovementStateMachine characterAiMovementStateMachine, NavMeshAgent navMeshAgent)
        {
            _characterAiMovementStateMachine = characterAiMovementStateMachine;
            _navMeshAgent = navMeshAgent;
        }


        public override void Move(Vector3 target)
        {
            base.Move(target);
            if (PreventMovementAndRotation) return;
            _navMeshAgent.SetDestination(target);
        }

        public override void Rotate(Vector3 target)
        {
            base.Rotate(target);
            if (PreventMovementAndRotation) return;
        }

        public Vector3 GetCurrentVelocity()
        {
            return _navMeshAgent.velocity;
        }

        public float GetMaxVelocity()
        {
            return _characterControllerSettings.MaxMovementSpeedEnemyAi;
        }

        public void SetTargetTransform(Transform targetTransform)
        {

        }

        public void SetTargetPosition(Vector3 targetPosition)
        {

        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public override void SetPreventMovementAndRotation(bool isPreventMovementAndRotation)
        {
            PreventMovementAndRotation = isPreventMovementAndRotation;
            _navMeshAgent.enabled = !isPreventMovementAndRotation;
        }

        public NavMeshAgent GetNavMeshAgent()
        {
            return _navMeshAgent;
        }
    }
}

