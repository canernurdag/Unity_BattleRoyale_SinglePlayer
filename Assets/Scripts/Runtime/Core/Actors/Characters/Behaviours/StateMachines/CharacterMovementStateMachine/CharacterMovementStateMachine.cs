using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.StateMachines.CharacterMovementStateMachine.States;
using Assets.Scripts.Runtime.Systems.StateMachineSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.StateMachines.CharacterMovementStateMachine
{
    public class CharacterMovementStateMachine : BaseStateMachineSystem
    {
        public override BaseState GetInitialState()
        {
            var initState = new CharacterAiMovementIdleState(this);
            return initState;
        }
    }
}