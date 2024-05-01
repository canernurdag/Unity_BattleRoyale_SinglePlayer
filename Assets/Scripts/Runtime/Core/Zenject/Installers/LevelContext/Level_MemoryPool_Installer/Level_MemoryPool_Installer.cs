using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction.WeaponInteractionProjectiles;
using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction.WeaponInteractionThrowable;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Zenject.Installers.LevelContext.Level_MemoryPool_Installer
{
    public class Level_MemoryPool_Installer : MonoInstaller
    {
        #region PREFABS
        [SerializeField] private WeaponInteractionProjectiles_AssaultRifle _prefab_WeaponProjecttiles_AssaultRifle;
        [SerializeField] private WeaponInteractionProjectiles_Bazooka _prefab_WeaponProjecttiles_Bazooka;
        [SerializeField] private WeaponInteractionProjectiles_ChemicalGun _prefab_WeaponProjecttiles_ChemicalGun;
        [SerializeField] private WeaponInteractionProjectiles_CrossBow _prefab_WeaponProjecttiles_CrossBow;
        [SerializeField] private WeaponInteractionProjectiles_ElectricGun _prefab_WeaponProjecttiles_ElectricGun;
        [SerializeField] private WeaponInteractionProjectiles_GravityGun _prefab_WeaponProjecttiles_GravityGun;
        [SerializeField] private WeaponInteractionThrowable_Grenade _prefab_WeaponProjecttiles_Grenade;
        [SerializeField] private WeaponInteractionProjectiles_HMG _prefab_WeaponProjecttiles_HMG;
        [SerializeField] private WeaponInteractionProjectiles_HuntingRifle _prefab_WeaponProjecttiles_HuntingRifle;
        [SerializeField] private WeaponInteractionProjectiles_Icegun _prefab_WeaponProjecttiles_Icegun;
        [SerializeField] private WeaponInteractionProjectiles_Minigun _prefab_WeaponProjecttiles_Minigun;
        [SerializeField] private WeaponInteractionProjectiles_Pistol _prefab_WeaponProjecttiles_Pistol;
        [SerializeField] private WeaponInteractionProjectiles_Railgun _prefab_WeaponProjecttiles_Railgun;
        [SerializeField] private WeaponInteractionProjectiles_Shotgun _prefab_WeaponProjecttiles_Shotgun;
        [SerializeField] private WeaponInteractionProjectiles_SMG _prefab_WeaponProjecttiles_SMG;
        [SerializeField] private WeaponInteractionThrowable_SmokeGrenade _prefab_WeaponProjecttiles_SmokeGrenade;
        [SerializeField] private WeaponInteractionProjectiles_Sniper _prefab_WeaponProjecttiles_Sniper;
        #endregion

        #region PARENTS
        [Space(60)]
        [SerializeField] private Transform _parent_WeaponProjectiles_AssaultRifle;
        [SerializeField] private Transform _parent_WeaponProjectiles_Bazooka;
        [SerializeField] private Transform _parent_WeaponProjectiles_ChemicalGun;
        [SerializeField] private Transform _parent_WeaponProjectiles_CrossBow;
        [SerializeField] private Transform _parent_WeaponProjectiles_ElectricGun;
        [SerializeField] private Transform _parent_WeaponProjectiles_FlameThrower;
        [SerializeField] private Transform _parent_WeaponProjectiles_GravityGun;
        [SerializeField] private Transform _parent_WeaponProjectiles_Grenade;
        [SerializeField] private Transform _parent_WeaponProjectiles_HMG;
        [SerializeField] private Transform _parent_WeaponProjectiles_HuntingRifle;
        [SerializeField] private Transform _parent_WeaponProjectiles_Icegun;
        [SerializeField] private Transform _parent_WeaponProjectiles_Knife;
        [SerializeField] private Transform _parent_WeaponProjectiles_Minigun;
        [SerializeField] private Transform _parent_WeaponProjectiles_Pistol;
        [SerializeField] private Transform _parent_WeaponProjectiles_Railgun;
        [SerializeField] private Transform _parent_WeaponProjectiles_Shotgun;
        [SerializeField] private Transform _parent_WeaponProjectiles_SMG;
        [SerializeField] private Transform _parent_WeaponProjectiles_SmokeGrenade;
        [SerializeField] private Transform _parent_WeaponProjectiles_Sniper;

        #endregion

        public override void InstallBindings()
        {
            InitMemoryPools();
            MemoryPoolControllers();
        }

        private void MemoryPoolControllers()
        {
            Container.Bind<MemoryPoolController<WeaponInteractionProjectiles_AssaultRifle>>()
                .AsCached();
            Container.Bind<MemoryPoolController<WeaponInteractionProjectiles_Bazooka>>()
                .AsCached();
            Container.Bind<MemoryPoolController<WeaponInteractionProjectiles_ChemicalGun>>()
                .AsCached();
            Container.Bind<MemoryPoolController<WeaponInteractionProjectiles_CrossBow>>()
                .AsCached();
            Container.Bind<MemoryPoolController<WeaponInteractionProjectiles_ElectricGun>>()
                .AsCached();
            Container.Bind<MemoryPoolController<WeaponInteractionProjectiles_GravityGun>>()
                .AsCached();
            Container.Bind<MemoryPoolController<WeaponInteractionThrowable_Grenade>>()
                .AsCached();
            Container.Bind<MemoryPoolController<WeaponInteractionProjectiles_HMG>>()
                .AsCached();
            Container.Bind<MemoryPoolController<WeaponInteractionProjectiles_HuntingRifle>>()
                .AsCached();
            Container.Bind<MemoryPoolController<WeaponInteractionProjectiles_Icegun>>()
                .AsCached();
            Container.Bind<MemoryPoolController<WeaponInteractionProjectiles_Minigun>>()
                .AsCached();
            Container.Bind<MemoryPoolController<WeaponInteractionProjectiles_Pistol>>()
                .AsCached();
            Container.Bind<MemoryPoolController<WeaponInteractionProjectiles_Railgun>>()
                .AsCached();
            Container.Bind<MemoryPoolController<WeaponInteractionProjectiles_Shotgun>>()
                .AsCached();
            Container.Bind<MemoryPoolController<WeaponInteractionProjectiles_SMG>>()
                .AsCached();
            Container.Bind<MemoryPoolController<WeaponInteractionThrowable_SmokeGrenade>>()
                .AsCached();
            Container.Bind<MemoryPoolController<WeaponInteractionProjectiles_Sniper>>()
                .AsCached();

        }

        private void InitMemoryPools()
        {
            Container.BindMemoryPool<WeaponInteractionProjectiles_AssaultRifle, WeaponInteractionProjectiles_AssaultRifle.Pool>()
                .WithInitialSize(0)
                .FromComponentInNewPrefab(_prefab_WeaponProjecttiles_AssaultRifle)
                .UnderTransform(_parent_WeaponProjectiles_AssaultRifle);

            Container.BindMemoryPool<WeaponInteractionProjectiles_Bazooka, WeaponInteractionProjectiles_Bazooka.Pool>()
                .WithInitialSize(0)
                .FromComponentInNewPrefab(_prefab_WeaponProjecttiles_Bazooka)
                .UnderTransform(_parent_WeaponProjectiles_Bazooka);

            Container.BindMemoryPool<WeaponInteractionProjectiles_ChemicalGun, WeaponInteractionProjectiles_ChemicalGun.Pool>()
                .WithInitialSize(0)
                .FromComponentInNewPrefab(_prefab_WeaponProjecttiles_ChemicalGun)
                .UnderTransform(_parent_WeaponProjectiles_ChemicalGun);

            Container.BindMemoryPool<WeaponInteractionProjectiles_CrossBow, WeaponInteractionProjectiles_CrossBow.Pool>()
                .WithInitialSize(0)
                .FromComponentInNewPrefab(_prefab_WeaponProjecttiles_CrossBow)
                .UnderTransform(_parent_WeaponProjectiles_CrossBow);

            Container.BindMemoryPool<WeaponInteractionProjectiles_ElectricGun, WeaponInteractionProjectiles_ElectricGun.Pool>()
                .WithInitialSize(0)
                .FromComponentInNewPrefab(_prefab_WeaponProjecttiles_ElectricGun)
                .UnderTransform(_parent_WeaponProjectiles_ElectricGun);


            Container.BindMemoryPool<WeaponInteractionProjectiles_GravityGun, WeaponInteractionProjectiles_GravityGun.Pool>()
                .WithInitialSize(0)
                .FromComponentInNewPrefab(_prefab_WeaponProjecttiles_GravityGun)
                .UnderTransform(_parent_WeaponProjectiles_GravityGun);

            Container.BindMemoryPool<WeaponInteractionThrowable_Grenade, WeaponInteractionThrowable_Grenade.Pool>()
                .WithInitialSize(0)
                .FromComponentInNewPrefab(_prefab_WeaponProjecttiles_Grenade)
                .UnderTransform(_parent_WeaponProjectiles_Grenade);

            Container.BindMemoryPool<WeaponInteractionProjectiles_HMG, WeaponInteractionProjectiles_HMG.Pool>()
                .WithInitialSize(0)
                .FromComponentInNewPrefab(_prefab_WeaponProjecttiles_HMG)
                .UnderTransform(_parent_WeaponProjectiles_HMG);

            Container.BindMemoryPool<WeaponInteractionProjectiles_HuntingRifle, WeaponInteractionProjectiles_HuntingRifle.Pool>()
                .WithInitialSize(0)
                .FromComponentInNewPrefab(_prefab_WeaponProjecttiles_HuntingRifle)
                .UnderTransform(_parent_WeaponProjectiles_HuntingRifle);

            Container.BindMemoryPool<WeaponInteractionProjectiles_Icegun, WeaponInteractionProjectiles_Icegun.Pool>()
                .WithInitialSize(0)
                .FromComponentInNewPrefab(_prefab_WeaponProjecttiles_Icegun)
                .UnderTransform(_parent_WeaponProjectiles_Icegun);


            Container.BindMemoryPool<WeaponInteractionProjectiles_Minigun, WeaponInteractionProjectiles_Minigun.Pool>()
                .WithInitialSize(0)
                .FromComponentInNewPrefab(_prefab_WeaponProjecttiles_Minigun)
                .UnderTransform(_parent_WeaponProjectiles_Minigun);

            Container.BindMemoryPool<WeaponInteractionProjectiles_Pistol, WeaponInteractionProjectiles_Pistol.Pool>()
                .WithInitialSize(0)
                .FromComponentInNewPrefab(_prefab_WeaponProjecttiles_Pistol)
                .UnderTransform(_parent_WeaponProjectiles_Pistol);

            Container.BindMemoryPool<WeaponInteractionProjectiles_Railgun, WeaponInteractionProjectiles_Railgun.Pool>()
                .WithInitialSize(0)
                .FromComponentInNewPrefab(_prefab_WeaponProjecttiles_Railgun)
                .UnderTransform(_parent_WeaponProjectiles_Railgun);

            Container.BindMemoryPool<WeaponInteractionProjectiles_Shotgun, WeaponInteractionProjectiles_Shotgun.Pool>()
                .WithInitialSize(0)
                .FromComponentInNewPrefab(_prefab_WeaponProjecttiles_Shotgun)
                .UnderTransform(_parent_WeaponProjectiles_Shotgun);

            Container.BindMemoryPool<WeaponInteractionProjectiles_SMG, WeaponInteractionProjectiles_SMG.Pool>()
                .WithInitialSize(0)
                .FromComponentInNewPrefab(_prefab_WeaponProjecttiles_SMG)
                .UnderTransform(_parent_WeaponProjectiles_SMG);

            Container.BindMemoryPool<WeaponInteractionThrowable_SmokeGrenade, WeaponInteractionThrowable_SmokeGrenade.Pool>()
                .WithInitialSize(0)
                .FromComponentInNewPrefab(_prefab_WeaponProjecttiles_SmokeGrenade)
                .UnderTransform(_parent_WeaponProjectiles_SmokeGrenade);

            Container.BindMemoryPool<WeaponInteractionProjectiles_Sniper, WeaponInteractionProjectiles_Sniper.Pool>()
                .WithInitialSize(0)
                .FromComponentInNewPrefab(_prefab_WeaponProjecttiles_Sniper)
                .UnderTransform(_parent_WeaponProjectiles_Sniper);
        }
    }
}