using Assets.Scripts.Runtime.Core.Zenject.Signals.GamePlay;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Interfaces.Character_
{
    public interface ICharacterAttack
    {
        void SetAttackState(SignalCharacterAttack signalCharacterAttack);
        void ResetAmmo();
        Transform GetTransform();
        IDamagable GetClosestDamagableInRange();
    }
}