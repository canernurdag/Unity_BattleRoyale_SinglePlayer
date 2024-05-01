using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterAttack_.Base;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.StateMachines.CharacterAttackStateMachine.States;
using Assets.Scripts.Runtime.Core.Zenject.Signals.GamePlay;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterAttack
{
    public class CharacterAttackAi : CharacterAttackBase
    {
        public override void SetAttackState(SignalCharacterAttack signalCharacterAttack)
        {
            if (signalCharacterAttack.Character != Character) return;

            var nextReadyAmmo = GetNextReadyAmmo();
            if (nextReadyAmmo == null) return;

            if (_characterAttackStateMachine.GetCurrentState().GetType() == typeof(CharacterAttackAttackState)) return;

            nextReadyAmmo.Slider.value = 0;
            UpdateCharacterAttackAmmos();

            _characterAttackStateMachine.SetCurrentState(new CharacterAttackAttackState(_characterAttackStateMachine));
        }
    }
}