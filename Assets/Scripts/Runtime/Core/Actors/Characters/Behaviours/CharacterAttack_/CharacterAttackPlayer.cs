using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterAttack_.Base;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.StateMachines.CharacterAttackStateMachine.States;
using Assets.Scripts.Runtime.Core.Zenject.Signals.GamePlay;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterAttack
{
    public class CharacterAttackPlayer : CharacterAttackBase
    {

        private float _tapTrashold = 0.1f;

        public override void SetAttackState(SignalCharacterAttack signalCharacterAttack)
        {
            if (signalCharacterAttack.Character != Character) return;

            var nextReadyAmmo = GetNextReadyAmmo();
            if (nextReadyAmmo == null) return;

            if (!_characterWeaponAttack.ActiveWeaponAttack.IsWeaponAttackFinished) return;

            if (_characterAttackStateMachine.GetCurrentState().GetType() == typeof(CharacterAttackAttackState)) return;

            nextReadyAmmo.Slider.value = 0;
            UpdateCharacterAttackAmmos();

            _characterAttackStateMachine.SetCurrentState(new CharacterAttackAttackState(_characterAttackStateMachine));

        }


    }
}