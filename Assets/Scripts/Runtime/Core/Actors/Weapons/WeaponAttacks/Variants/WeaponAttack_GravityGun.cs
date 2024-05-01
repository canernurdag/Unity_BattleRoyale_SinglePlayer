using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction;
using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction.WeaponInteractionProjectiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponAttacks.Variants
{
    public class WeaponAttack_GravityGun : WeaponAttack_Projectile<WeaponInteractionProjectiles_GravityGun>
    {

        public override void DespawnProjectile(WeaponInteractionProjectile<WeaponInteractionProjectiles_GravityGun> projectile)
        {
            _level_MemoryPool_Controller.MemoryPoolController_WeaponProjecttiles_GravityGun.Despawn(projectile as WeaponInteractionProjectiles_GravityGun);
        }

        public override WeaponInteractionProjectile<WeaponInteractionProjectiles_GravityGun> SpawnProjectile()
        {
            return _level_MemoryPool_Controller.MemoryPoolController_WeaponProjecttiles_GravityGun.Spawn() as WeaponInteractionProjectile<WeaponInteractionProjectiles_GravityGun>;
        }
    }
}