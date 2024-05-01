using Assets.Scripts.Runtime.Core.Actors.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Interfaces.Character_
{
    public interface ICharacterWeaponAttack
    {
        void InitChararcterWeaponAttack();
        WeaponAttack GetActiveWeaponAttack();
        WeaponAimIndicator GetActiveWeaponAimIndicator();
        void SetActiveWeaponAttack(WeaponAttack weaponAttack);
    }
}