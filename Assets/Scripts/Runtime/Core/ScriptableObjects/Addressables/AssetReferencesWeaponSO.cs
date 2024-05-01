using Assets.Scripts.Runtime.Core.Actors.Weapons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

[CreateAssetMenu(fileName = "AssetReferencesWeaponSO", menuName = "CanerNurdag/Addressables/AssetReferencesWeaponSO")]
public class AssetReferencesWeaponSO : ScriptableObjectInstaller<AssetReferencesWeaponSO>
{
    public AssetReferencesWeaponDatas AssetReferencesWeaponDatas = new();
    public override void InstallBindings()
    {
        Container.BindInstance(AssetReferencesWeaponDatas);
    }
}

[Serializable]
public class AssetReferencesWeaponDatas
{

    public List<AssetReferencesWeaponData> AssetReferencesWeaponDataList = new();

    [Serializable]
    public class AssetReferencesWeaponData
    {
        public Weapon.Type WeaponType;
        public AssetReference AssetReference;

    }


}