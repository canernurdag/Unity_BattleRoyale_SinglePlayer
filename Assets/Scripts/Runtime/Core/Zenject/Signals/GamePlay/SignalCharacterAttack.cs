using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Runtime.Core.Actors.Characters.Character_;
using Assets.Scripts.Runtime.Core.Interfaces.Character;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Zenject.Signals.GamePlay
{
    public class SignalCharacterAttack
    {
        public ICharacter Character;
        public float TapDuration;

        public SignalCharacterAttack(ICharacter character, float tapDuration)
        {
            Character = character;
            TapDuration = tapDuration;
        }
    }
}