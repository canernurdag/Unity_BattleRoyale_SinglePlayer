using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction;
using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction.WeaponInteractionProjectiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponAttacks.Variants
{
    public class WeaponAttack_Bazooka : WeaponAttack_Projectile<WeaponInteractionProjectiles_Bazooka>
    {

        public override void DespawnProjectile(WeaponInteractionProjectile<WeaponInteractionProjectiles_Bazooka> projectile)
        {
            _level_MemoryPool_Controller.MemoryPoolController_WeaponProjecttiles_Bazooka.Despawn(projectile as WeaponInteractionProjectiles_Bazooka);
        }

        public override WeaponInteractionProjectile<WeaponInteractionProjectiles_Bazooka> SpawnProjectile()
        {
            return _level_MemoryPool_Controller.MemoryPoolController_WeaponProjecttiles_Bazooka.Spawn() as WeaponInteractionProjectile<WeaponInteractionProjectiles_Bazooka>;
        }
    }
}