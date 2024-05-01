using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction;
using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction.WeaponInteractionProjectiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponAttacks.Variants
{
    public class WeaponAttack_SMG : WeaponAttack_Projectile<WeaponInteractionProjectiles_SMG>
    {
        public override void DespawnProjectile(WeaponInteractionProjectile<WeaponInteractionProjectiles_SMG> projectile)
        {
            _level_MemoryPool_Controller.MemoryPoolController_WeaponProjecttiles_SMG.Despawn(projectile as WeaponInteractionProjectiles_SMG);
        }

        public override WeaponInteractionProjectile<WeaponInteractionProjectiles_SMG> SpawnProjectile()
        {
            return _level_MemoryPool_Controller.MemoryPoolController_WeaponProjecttiles_SMG.Spawn() as WeaponInteractionProjectile<WeaponInteractionProjectiles_SMG>;
        }
    }
}