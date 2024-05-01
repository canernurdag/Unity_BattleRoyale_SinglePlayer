using Assets.Scripts.Runtime.Core.Actors.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Interfaces.Character_
{
    public interface ICharacterWeapon
    {

        Weapon GetActiveWeapon();
        void SetActiveWeapon(Weapon.Type weaponType);
    }
}