using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterVisual;
using Assets.Scripts.Runtime.Core.Actors.Weapons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.User_
{
    [Serializable]
    public class UserData
    {
        public int UserId;
        public string UserName;
        public CharacterVisual.Type CharacterVisualType;
        public Weapon.Type WeaponType;
        public bool IsBot;

    }
}