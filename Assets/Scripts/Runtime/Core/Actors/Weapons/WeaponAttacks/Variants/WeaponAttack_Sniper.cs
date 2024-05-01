using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction;
using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction.WeaponInteractionProjectiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponAttacks.Variants
{
    public class WeaponAttack_Sniper : WeaponAttack_Projectile<WeaponInteractionProjectiles_Sniper>
    {

        public override void DespawnProjectile(WeaponInteractionProjectile<WeaponInteractionProjectiles_Sniper> projectile)
        {
            _level_MemoryPool_Controller.MemoryPoolController_WeaponProjecttiles_Sniper.Despawn(projectile as WeaponInteractionProjectiles_Sniper);
        }

        public override WeaponInteractionProjectile<WeaponInteractionProjectiles_Sniper> SpawnProjectile()
        {
            return _level_MemoryPool_Controller.MemoryPoolController_WeaponProjecttiles_Sniper.Spawn() as WeaponInteractionProjectile<WeaponInteractionProjectiles_Sniper>;
        }
    }
}