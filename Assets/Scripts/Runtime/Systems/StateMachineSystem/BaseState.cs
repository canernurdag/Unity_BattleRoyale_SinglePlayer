using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Runtime.Systems.StateMachineSystem
{
    [Serializable]
    public abstract class BaseState
    {
        protected readonly IStateMachine _stateMachineSystem;

        protected BaseState(IStateMachine stateMachineSystem)
        {
            _stateMachineSystem = stateMachineSystem;
        }

        public abstract IEnumerator EnterState();
        public abstract void ExecuteUpdate();
        public abstract void ExecuteFixedUpdate();
        public abstract void ExecuteLateUpdate();
        public abstract void ExitState();

    }
}



