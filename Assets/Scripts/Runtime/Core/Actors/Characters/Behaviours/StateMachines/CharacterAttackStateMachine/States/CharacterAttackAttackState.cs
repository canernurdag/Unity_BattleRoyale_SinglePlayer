using Assets.Scripts.Runtime.Systems.StateMachineSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.StateMachines.CharacterAttackStateMachine.States
{
    public class CharacterAttackAttackState : BaseState
    {
        private CharacterAttackStateMachine _characterAttackStateMachine;

        public CharacterAttackAttackState(IStateMachine stateMachineSystem) : base(stateMachineSystem)
        {
            _characterAttackStateMachine = stateMachineSystem as CharacterAttackStateMachine;

        }



        public override IEnumerator EnterState()
        {
            while (_characterAttackStateMachine.CharacterGameplay.CharacterWeapon.GetActiveWeapon() == null)
            {
                yield return null;
            }
            var activeWeapon = _characterAttackStateMachine.CharacterGameplay.CharacterWeapon.GetActiveWeapon();

            string shootAnimParam = _characterAttackStateMachine.CharacterGameplay.AnimatorUser.GetCharacterAnimationData(activeWeapon.WeaponType).Shoot;
            _characterAttackStateMachine.CharacterAnimator.AnimatorSystem.SetCurrentAnimatorState(shootAnimParam);

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