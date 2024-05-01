using Assets.Scripts.Runtime.Core.Actors.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Zenject.Signals.Player
{
    public class SignalPlayerCustomizationWeaponChanged
    {
        public Weapon.Type WeaponType;
        public bool IsActive;

        public SignalPlayerCustomizationWeaponChanged(Weapon.Type weaponType, bool isActive)
        {
            WeaponType = weaponType;
            IsActive = isActive;
        }
    }
}