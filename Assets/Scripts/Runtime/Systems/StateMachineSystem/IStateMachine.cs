using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Systems.StateMachineSystem
{
    public interface IStateMachine
    {
        void SetCurrentState(BaseState nextState);
        BaseState GetCurrentState();
    }
}