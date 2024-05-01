using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterWeaponAttack_;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.StateMachines.CharacterAttackStateMachine;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.StateMachines.CharacterAttackStateMachine.States;
using Assets.Scripts.Runtime.Core.Managers.Manager_Game;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterAttack
{
    public class CharacterAttackAnimationEvents : MonoBehaviour
    {
        protected ManagerGame _managerGame;
        protected CharacterWeaponAttack _characterWeaponAttack;
        protected CharacterAttackStateMachine _characterAttackStateMachine;

        [Inject]
        public void Construct(CharacterWeaponAttack characterWeaponAttack, CharacterAttackStateMachine characterAttackStateMachine)
        {
            _characterWeaponAttack = characterWeaponAttack;
            _characterAttackStateMachine = characterAttackStateMachine;
        }
        public void OnShoot()
        {
            _characterWeaponAttack.ActiveWeaponAttack.Attack();

        }

        public void OnShootAnimCompleted()
        {
            _characterAttackStateMachine.SetCurrentState(new CharacterAttackIdleState(_characterAttackStateMachine));

        }

        public void OnReloadCompleted()
        {
            _characterAttackStateMachine.SetCurrentState(new CharacterAttackIdleState(_characterAttackStateMachine));
        }
    }

}
