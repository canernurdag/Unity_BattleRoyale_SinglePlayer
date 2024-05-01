using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterWeapon_;
using Assets.Scripts.Runtime.Core.Actors.Weapons;
using Assets.Scripts.Runtime.Core.Managers.Manager_Addressables;
using Assets.Scripts.Runtime.Core.Zenject.Installers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Zenject.AddressablesAdapters
{
    public class AddressableAdapterWeapon : AddressableAdapter
    {
        #region DI REF
        private CharacterWeapon _characterWeapon;
        private ManagerAddressables _managerAddressables;
        private AssetReferencesWeaponDatas _assetReferencesWeaponDatas;
        #endregion

        #region VAR
        public GameObject ActiveWeaponGameObject { get; private set; } = null;

        #endregion

        [Inject]
        public void Construct(ManagerAddressables managerAddressables, AssetReferencesWeaponDatas assetReferencesWeaponDatas, CharacterWeapon characterWeapon)
        {
            _managerAddressables = managerAddressables;
            _assetReferencesWeaponDatas = assetReferencesWeaponDatas;
            _characterWeapon = characterWeapon;
        }

        private AssetReference GetWeaponAssetReference(Weapon.Type weaponType)
        {
            bool isWeaponAssetReferenceDataExist = _assetReferencesWeaponDatas.AssetReferencesWeaponDataList.Any(x => x.WeaponType == weaponType);
            if (isWeaponAssetReferenceDataExist)
            {
                var assetReference = _assetReferencesWeaponDatas.AssetReferencesWeaponDataList.First(x => x.WeaponType == weaponType).AssetReference;
                return assetReference;
            }
            else if (!isWeaponAssetReferenceDataExist)
            {
                Debug.Log("Data does not exist in SO");
            }

            return null;
        }

        public async void SetActiveWeapon(Weapon.Type weaponType, Action callback)
        {
            if (IsAsyncOn) return;
            ReleaseInstances();

            var weaponAssetReference = GetWeaponAssetReference(weaponType);
            if (weaponAssetReference != null)
            {
                SetIsAsyncOn(true);
                ActiveWeaponGameObject = await _managerAddressables.GetAndInstantiateGameObjectAsync(weaponAssetReference);
                ActiveWeaponGameObject.SetActive(true);
                ActiveWeaponGameObject.transform.SetParent(transform);


                var characterInstaller = FindObjectOfType<CharacterInstaller>();
                var container = characterInstaller.GetContainer();
                container.InjectGameObject(ActiveWeaponGameObject);

                var activeWeapon = ActiveWeaponGameObject.GetComponent<Weapon>();
                _characterWeapon.SetActiveWeapon(activeWeapon);
                ActiveWeaponGameObject.transform.localPosition = activeWeapon.LocalPosition;
                ActiveWeaponGameObject.transform.localRotation = Quaternion.Euler(activeWeapon.LocalRotationAngle);
                ActiveWeaponGameObject.transform.localScale = Vector3.one;

                SetIsAsyncOn(false);
                if (callback != null) callback();
            }
        }

        protected override void ReleaseInstances()
        {
            if (ActiveWeaponGameObject == null) return;

            _managerAddressables.ReleaseAndDestroy(ActiveWeaponGameObject);
        }
    }
}