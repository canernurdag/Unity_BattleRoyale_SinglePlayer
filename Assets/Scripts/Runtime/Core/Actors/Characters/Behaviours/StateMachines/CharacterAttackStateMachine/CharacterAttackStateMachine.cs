using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterAnimators;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterAttack_.Base;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.StateMachines.CharacterAttackStateMachine.States;
using Assets.Scripts.Runtime.Core.Actors.Characters.Character_.GamePlay;
using Assets.Scripts.Runtime.Systems.StateMachineSystem;
using UniRx.Triggers;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.StateMachines.CharacterAttackStateMachine
{
    public abstract class CharacterAttackStateMachine : BaseStateMachineSystem
    {
        public CharacterGameplay CharacterGameplay { get; protected set; }

        public CharacterAttackBase CharacterAttack { get; protected set; }
        protected CharacterAnimator _characterAnimator;
        public CharacterAnimator CharacterAnimator => _characterAnimator;




        [Inject]
        public virtual void Construct(CharacterAttackBase characterAttack, CharacterAnimator characterAnimator, CharacterGameplay characterGameplay)
        {

            CharacterAttack = characterAttack;
            _characterAnimator = characterAnimator;

            CharacterGameplay = characterGameplay;
        }




        public override BaseState GetInitialState()
        {
            var initState = new CharacterAttackIdleState(this);
            return initState;
        }
    }

}

