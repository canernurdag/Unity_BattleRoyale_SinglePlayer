using Assets.Scripts.Runtime.Core.Actors.Weapons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "WeaponSettingsSO", menuName = "CanerNurdag/WeaponSettingsSO")]
public class WeaponSettingsSO : ScriptableObjectInstaller<WeaponSettingsSO>
{
    public WeaponSettings weaponSettings = new();
    public override void InstallBindings()
    {
        Container.BindInstance(weaponSettings);
    }
}

[Serializable]
public class WeaponSettings
{

    public List<WeaponData> WeaponDatas = new();

    [Serializable]
    public class WeaponData
    {
        public Weapon.Type WeaponType;
        public int Price;
        public string Description;

        public int DamageAmount;
        public bool IsRadiusDamage;
        public float DamageRadius;
        public float DurationOfMeleeWeaponActiveness;

    }


}