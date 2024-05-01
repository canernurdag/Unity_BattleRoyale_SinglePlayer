using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction;
using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction.WeaponInteractionProjectiles;
using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction.WeaponInteractionThrowable;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Zenject.Installers.LevelContext.Level_MemoryPool_Installer
{
    public class Level_MemoryPool_Controller : MonoBehaviour
    {
        public MemoryPoolController<WeaponInteractionProjectiles_AssaultRifle> MemoryPoolController_WeaponProjecttiles_AssaultRifle { get; private set; }
        public MemoryPoolController<WeaponInteractionProjectiles_Bazooka> MemoryPoolController_WeaponProjecttiles_Bazooka { get; private set; }
        public MemoryPoolController<WeaponInteractionProjectiles_ChemicalGun> MemoryPoolController_WeaponProjecttiles_ChemicalGun { get; private set; }
        public MemoryPoolController<WeaponInteractionProjectiles_CrossBow> MemoryPoolController_WeaponProjecttiles_CrossBow { get; private set; }
        public MemoryPoolController<WeaponInteractionProjectiles_ElectricGun> MemoryPoolController_WeaponProjecttiles_ElectricGun { get; private set; }
        public MemoryPoolController<WeaponInteractionProjectiles_GravityGun> MemoryPoolController_WeaponProjecttiles_GravityGun { get; private set; }
        public MemoryPoolController<WeaponInteractionThrowable_Grenade> MemoryPoolController_WeaponInteractionThrowable_Grenade { get; private set; }
        public MemoryPoolController<WeaponInteractionProjectiles_HMG> MemoryPoolController_WeaponProjecttiles_HMG { get; private set; }
        public MemoryPoolController<WeaponInteractionProjectiles_HuntingRifle> MemoryPoolController_WeaponProjecttiles_HuntingRifle { get; private set; }
        public MemoryPoolController<WeaponInteractionProjectiles_Icegun> MemoryPoolController_WeaponProjecttiles_Icegun { get; private set; }
        public MemoryPoolController<WeaponInteractionProjectiles_Minigun> MemoryPoolController_WeaponProjecttiles_Minigun { get; private set; }
        public MemoryPoolController<WeaponInteractionProjectiles_Pistol> MemoryPoolController_WeaponProjecttiles_Pistol { get; private set; }
        public MemoryPoolController<WeaponInteractionProjectiles_Railgun> MemoryPoolController_WeaponProjecttiles_Railgun { get; private set; }
        public MemoryPoolController<WeaponInteractionProjectiles_Shotgun> MemoryPoolController_WeaponProjecttiles_Shotgun { get; private set; }
        public MemoryPoolController<WeaponInteractionProjectiles_SMG> MemoryPoolController_WeaponProjecttiles_SMG { get; private set; }
        public MemoryPoolController<WeaponInteractionThrowable_SmokeGrenade> MemoryPoolController_WeaponInteractionThrowable_SmokeGrenade { get; private set; }
        public MemoryPoolController<WeaponInteractionProjectiles_Sniper> MemoryPoolController_WeaponProjecttiles_Sniper { get; private set; }

        [Inject]
        public void Construct(
        MemoryPoolController<WeaponInteractionProjectiles_AssaultRifle> memoryPoolController_WeaponProjecttiles_AssaultRifle,
        MemoryPoolController<WeaponInteractionProjectiles_Bazooka> memoryPoolController_WeaponProjecttiles_Bazooka,
        MemoryPoolController<WeaponInteractionProjectiles_ChemicalGun> memoryPoolController_WeaponProjecttiles_ChemicalGun,
        MemoryPoolController<WeaponInteractionProjectiles_CrossBow> memoryPoolController_WeaponProjecttiles_CrossBow,
        MemoryPoolController<WeaponInteractionProjectiles_ElectricGun> memoryPoolController_WeaponProjecttiles_ElectricGun,
        MemoryPoolController<WeaponInteractionProjectiles_GravityGun> memoryPoolController_WeaponProjecttiles_GravityGun,
        MemoryPoolController<WeaponInteractionThrowable_Grenade> memoryPoolController_WeaponInteractionThrowable_Grenade,
        MemoryPoolController<WeaponInteractionProjectiles_HMG> memoryPoolController_WeaponProjecttiles_HMG,
        MemoryPoolController<WeaponInteractionProjectiles_HuntingRifle> memoryPoolController_WeaponProjecttiles_HuntingRifle,
        MemoryPoolController<WeaponInteractionProjectiles_Icegun> memoryPoolController_WeaponProjecttiles_Icegun,
        MemoryPoolController<WeaponInteractionProjectiles_Minigun> memoryPoolController_WeaponProjecttiles_Minigun,
        MemoryPoolController<WeaponInteractionProjectiles_Pistol> memoryPoolController_WeaponProjecttiles_Pistol,
        MemoryPoolController<WeaponInteractionProjectiles_Railgun> memoryPoolController_WeaponProjecttiles_Railgun,
        MemoryPoolController<WeaponInteractionProjectiles_Shotgun> memoryPoolController_WeaponProjecttiles_Shotgun,
        MemoryPoolController<WeaponInteractionProjectiles_SMG> memoryPoolController_WeaponProjecttiles_SMG,
        MemoryPoolController<WeaponInteractionThrowable_SmokeGrenade> memoryPoolController_WeaponInteractionThrowable_SmokeGrenade,
        MemoryPoolController<WeaponInteractionProjectiles_Sniper> memoryPoolController_WeaponProjecttiles_Sniper
        )
        {
            MemoryPoolController_WeaponProjecttiles_AssaultRifle = memoryPoolController_WeaponProjecttiles_AssaultRifle;
            MemoryPoolController_WeaponProjecttiles_Bazooka = memoryPoolController_WeaponProjecttiles_Bazooka;
            MemoryPoolController_WeaponProjecttiles_ChemicalGun = memoryPoolController_WeaponProjecttiles_ChemicalGun;
            MemoryPoolController_WeaponProjecttiles_CrossBow = memoryPoolController_WeaponProjecttiles_CrossBow;
            MemoryPoolController_WeaponProjecttiles_ElectricGun = memoryPoolController_WeaponProjecttiles_ElectricGun;
            MemoryPoolController_WeaponProjecttiles_GravityGun = memoryPoolController_WeaponProjecttiles_GravityGun;
            MemoryPoolController_WeaponInteractionThrowable_Grenade = memoryPoolController_WeaponInteractionThrowable_Grenade;
            MemoryPoolController_WeaponProjecttiles_HMG = memoryPoolController_WeaponProjecttiles_HMG;
            MemoryPoolController_WeaponProjecttiles_HuntingRifle = memoryPoolController_WeaponProjecttiles_HuntingRifle;
            MemoryPoolController_WeaponProjecttiles_Icegun = memoryPoolController_WeaponProjecttiles_Icegun;
            MemoryPoolController_WeaponProjecttiles_Minigun = memoryPoolController_WeaponProjecttiles_Minigun;
            MemoryPoolController_WeaponProjecttiles_Pistol = memoryPoolController_WeaponProjecttiles_Pistol;
            MemoryPoolController_WeaponProjecttiles_Railgun = memoryPoolController_WeaponProjecttiles_Railgun;
            MemoryPoolController_WeaponProjecttiles_Shotgun = memoryPoolController_WeaponProjecttiles_Shotgun;
            MemoryPoolController_WeaponProjecttiles_SMG = memoryPoolController_WeaponProjecttiles_SMG;
            MemoryPoolController_WeaponInteractionThrowable_SmokeGrenade = memoryPoolController_WeaponInteractionThrowable_SmokeGrenade;
            MemoryPoolController_WeaponProjecttiles_Sniper = memoryPoolController_WeaponProjecttiles_Sniper;
        }

    }

    [Serializable]
    public class MemoryPoolController<T> where T : WeaponInteractionPoolable<T>
    {
        public WeaponInteractionPoolable<T>.Pool Pool { get; private set; }

        public MemoryPoolController(WeaponInteractionPoolable<T>.Pool pool)
        {
            Pool = pool;
        }


        public WeaponInteractionPoolable<T> Spawn()
        {
            return Pool.Spawn();
        }

        public void Despawn(T despawnObject)
        {
            Pool.Despawn(despawnObject);
        }
    }
}