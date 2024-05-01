using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;
using Assets.Scripts.Runtime.Core.Actors.Weapons;
using Assets.Scripts.Runtime.Core.Interfaces.Character_;
using Assets.Scripts.Runtime.Core.Zenject.AddressablesAdapters;
using Assets.Scripts.Runtime.Core.Zenject.Signals.Player;

namespace Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterWeapon_
{
    public class CharacterWeapon : MonoBehaviour, ICharacterWeapon
    {
        #region DIRECT REF
        [field: SerializeField] public Material ActiveMaterial { get; private set; }
        [field: SerializeField] public Material InActiveMaterial { get; private set; }
        #endregion

        #region DI REF
        public AddressableAdapterWeapon AddressableAdapterWeapon { get; private set; }

        #endregion

        public Weapon ActiveWeapon { get; private set; }



        [Inject]
        public void Construct(AddressableAdapterWeapon addressableAdapterWeapona)
        {
            AddressableAdapterWeapon = addressableAdapterWeapona;


        }


        public void SetActiveWeapon(Weapon.Type weaponType)
        {
            AddressableAdapterWeapon.SetActiveWeapon(weaponType, null);
        }

        public void SetActiveWeapon(Weapon weapon)
        {
            ActiveWeapon = weapon;
        }


        public void SetActiveWeapon(SignalPlayerActiveWeaponChanged signalPlayerActiveWeaponChanged)
        {
            AddressableAdapterWeapon.SetActiveWeapon(signalPlayerActiveWeaponChanged.WeaponType,
                () =>
            {
                if (signalPlayerActiveWeaponChanged.IsActive)
                {
                    ActiveWeapon.SetMeshRendererMaterial(ActiveMaterial);
                    ActiveWeapon.SetSkinnedMeshRendererMaterial(ActiveMaterial);
                }
                else if (!signalPlayerActiveWeaponChanged.IsActive)
                {
                    ActiveWeapon.SetMeshRendererMaterial(InActiveMaterial);
                    ActiveWeapon.SetSkinnedMeshRendererMaterial(InActiveMaterial);
                }
            });


        }

        public void SetWeaponMaterialAccordingToWeaponActiveness(SignalPlayerCustomizationWeaponChanged signalUserCustomizationWeaponChanged)
        {
            AddressableAdapterWeapon.SetActiveWeapon(signalUserCustomizationWeaponChanged.WeaponType, () =>
            {
                if (signalUserCustomizationWeaponChanged.IsActive)
                {
                    ActiveWeapon.SetMeshRendererMaterial(ActiveMaterial);
                    ActiveWeapon.SetSkinnedMeshRendererMaterial(ActiveMaterial);
                }
                else if (!signalUserCustomizationWeaponChanged.IsActive)
                {
                    ActiveWeapon.SetMeshRendererMaterial(InActiveMaterial);
                    ActiveWeapon.SetSkinnedMeshRendererMaterial(InActiveMaterial);
                }
            });


        }



        public Weapon GetActiveWeapon()
        {
            return ActiveWeapon;
        }


    }
}