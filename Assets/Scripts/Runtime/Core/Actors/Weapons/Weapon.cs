using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Actors.Weapons
{
    public class Weapon : MonoBehaviour
    {
        #region DIRECT REF
        [field: SerializeField] public Type WeaponType { get; private set; }
        [field: SerializeField] public Vector3 LocalPosition { get; private set; } = new Vector3(0.075f, -0.031f, -0.011f);
        [field: SerializeField] public Vector3 LocalRotationAngle { get; private set; } = new Vector3(0.153f, 89.795f, 89.79f);
        #endregion

        #region DI REF
        private WeaponSettings _weaponSettings;
        public List<MeshRenderer> MeshRenderers { get; private set; } = new();
        public List<SkinnedMeshRenderer> SkinnedMeshRenderers { get; private set; } = new();
        #endregion



        [Inject]
        public void Construct(WeaponSettings weaponSettings, MeshRenderer[] meshRenderers,
            SkinnedMeshRenderer[] skinnedMeshRenderers)
        {
            _weaponSettings = weaponSettings;
            MeshRenderers = meshRenderers.ToList();
            SkinnedMeshRenderers = skinnedMeshRenderers.ToList();
        }


        public void SetMeshRendererMaterial(Material material)
        {
            if (MeshRenderers == null) return;
            if (MeshRenderers.Count == 0) return;

            MeshRenderers.ForEach(x => x.material = material);
        }
        public void SetSkinnedMeshRendererMaterial(Material material)
        {
            if (SkinnedMeshRenderers == null) return;
            if (SkinnedMeshRenderers.Count == 0) return;

            SkinnedMeshRenderers.ForEach(x => x.material = material);
        }

        public WeaponSettings.WeaponData GetWeaponData(Type weaponType)
        {
            bool isDataExist = _weaponSettings.WeaponDatas.Any(x => x.WeaponType == weaponType);

            if (isDataExist)
            {
                return _weaponSettings.WeaponDatas.First(x => x.WeaponType == weaponType);
            }
            else if (!isDataExist)
            {
                Debug.Log("The Data does not exist");
            }

            return null;
        }

        public enum Type
        {
            AssaultRifle = 0,
            FlameThrower = 2,
            GravityGun = 3,
            Pistol = 4,
            HMG = 5,
            ElectricGun = 6,
            SMG = 7,
            ChemiclaGun = 8,
            SniperRifle = 9,
            RailGun = 10,
            IceGun = 11,
            CrossBow = 12,
            HuntingRifle = 13,
            Shotgun = 14,
            Bazooka = 15,
            MiniGun = 16,
            Knife = 17,
            Grenade = 18,
            SmokeGrenade = 19,
        }


    }
}