using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Runtime.Systems.StateMachineSystem;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.StateMachines.CharacterAttackStateMachine.States
{
    public class CharacterAttackIdleState : BaseState
    {
        private CharacterAttackStateMachine _characterAttackStateMachine;
        public CharacterAttackIdleState(BaseStateMachineSystem stateMachineSystem) : base(stateMachineSystem)
        {
            _characterAttackStateMachine = stateMachineSystem as CharacterAttackStateMachine;
        }

        public override IEnumerator EnterState()
        {
            if (_characterAttackStateMachine == null) yield break;
            if (_characterAttackStateMachine.CharacterGameplay == null) yield break;
            if (_characterAttackStateMachine.CharacterGameplay.CharacterWeapon == null) yield break;

            while (_characterAttackStateMachine.CharacterGameplay.CharacterWeapon.GetActiveWeapon() == null)
            {
                yield return null;
            }
            var activeWeapon = _characterAttackStateMachine.CharacterGameplay.CharacterWeapon.GetActiveWeapon();

            string idleAnimParam = _characterAttackStateMachine.CharacterGameplay.AnimatorUser.GetCharacterAnimationData(activeWeapon.WeaponType).Idle;
            _characterAttackStateMachine.CharacterAnimator.AnimatorSystem.SetCurrentAnimatorState(idleAnimParam);

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