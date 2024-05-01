using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction;
using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction.WeaponInteractionProjectiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponAttacks.Variants
{
    public class WeaponAttack_HuntingRifle : WeaponAttack_Projectile<WeaponInteractionProjectiles_HuntingRifle>
    {

        public override void DespawnProjectile(WeaponInteractionProjectile<WeaponInteractionProjectiles_HuntingRifle> projectile)
        {
            _level_MemoryPool_Controller.MemoryPoolController_WeaponProjecttiles_HuntingRifle.Despawn(projectile as WeaponInteractionProjectiles_HuntingRifle);
        }

        public override WeaponInteractionProjectile<WeaponInteractionProjectiles_HuntingRifle> SpawnProjectile()
        {
            return _level_MemoryPool_Controller.MemoryPoolController_WeaponProjecttiles_HuntingRifle.Spawn() as WeaponInteractionProjectile<WeaponInteractionProjectiles_HuntingRifle>;
        }
    }
}