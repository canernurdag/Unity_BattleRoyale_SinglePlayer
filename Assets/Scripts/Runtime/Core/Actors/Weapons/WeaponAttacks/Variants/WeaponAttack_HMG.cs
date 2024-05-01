using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction;
using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction.WeaponInteractionProjectiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponAttacks.Variants
{
    public class WeaponAttack_HMG : WeaponAttack_Projectile<WeaponInteractionProjectiles_HMG>
    {

        public override void DespawnProjectile(WeaponInteractionProjectile<WeaponInteractionProjectiles_HMG> projectile)
        {
            _level_MemoryPool_Controller.MemoryPoolController_WeaponProjecttiles_HMG.Despawn(projectile as WeaponInteractionProjectiles_HMG);
        }

        public override WeaponInteractionProjectile<WeaponInteractionProjectiles_HMG> SpawnProjectile()
        {
            return _level_MemoryPool_Controller.MemoryPoolController_WeaponProjecttiles_HMG.Spawn() as WeaponInteractionProjectile<WeaponInteractionProjectiles_HMG>;
        }
    }
}