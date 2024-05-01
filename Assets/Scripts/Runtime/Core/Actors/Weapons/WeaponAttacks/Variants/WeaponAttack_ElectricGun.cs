using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction;
using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction.WeaponInteractionProjectiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponAttacks.Variants
{
    public class WeaponAttack_ElectricGun : WeaponAttack_Projectile<WeaponInteractionProjectiles_ElectricGun>
    {

        public override void DespawnProjectile(WeaponInteractionProjectile<WeaponInteractionProjectiles_ElectricGun> projectile)
        {
            _level_MemoryPool_Controller.MemoryPoolController_WeaponProjecttiles_ElectricGun.Despawn(projectile as WeaponInteractionProjectiles_ElectricGun);
        }

        public override WeaponInteractionProjectile<WeaponInteractionProjectiles_ElectricGun> SpawnProjectile()
        {
            return _level_MemoryPool_Controller.MemoryPoolController_WeaponProjecttiles_ElectricGun.Spawn() as WeaponInteractionProjectile<WeaponInteractionProjectiles_ElectricGun>;
        }
    }
}