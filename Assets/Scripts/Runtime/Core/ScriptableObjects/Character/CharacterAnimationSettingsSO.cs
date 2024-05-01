using Assets.Scripts.Runtime.Core.Actors.Weapons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "CharacterAnimationSettingsSO", menuName = "CanerNurdag/Character/CharacterAnimationSettingsSO")]
public class CharacterAnimationSettingsSO : ScriptableObjectInstaller<CharacterAnimationSettingsSO>
{
    public CharacterAnimationSettings characterAnimationSettings = new();

    public override void InstallBindings()
    {
        Container.BindInstance(characterAnimationSettings);
    }
}

[Serializable]
public class CharacterAnimationSettings
{

    public string Victory;
    public string PowerUp;
    public string GetHit0;
    public string GetHit1;
    public string KnockDown;
    public string GetUp;
    public string Dizzy;
    public string Die;

    public List<CharacterAnimationData> CharacterAnimationDatas = new();

    [Serializable]
    public class CharacterAnimationData
    {
        public Weapon.Type WeaponType;
        public string Idle;
        public string Shoot;
        public string Reload;
    }

}