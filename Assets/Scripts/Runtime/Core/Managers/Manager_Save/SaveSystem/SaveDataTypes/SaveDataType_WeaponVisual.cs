using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scripts.Runtime.Core.Actors.Weapons;

namespace Assets.Scripts.Runtime.Systems.SaveSystem.SaveDataTypes
{
    [Serializable]
    public class SaveDataType_WeaponVisual
    {
        public Weapon.Type WeaponType;
        public bool IsActive;
    }
}