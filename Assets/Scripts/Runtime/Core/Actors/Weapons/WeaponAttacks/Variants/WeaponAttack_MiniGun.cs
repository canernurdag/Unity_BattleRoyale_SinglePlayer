using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction;
using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction.WeaponInteractionProjectiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponAttacks.Variants
{
    public class WeaponAttack_MiniGun : WeaponAttack_Projectile<WeaponInteractionProjectiles_Minigun>
    {

        public override void DespawnProjectile(WeaponInteractionProjectile<WeaponInteractionProjectiles_Minigun> projectile)
        {
            _level_MemoryPool_Controller.MemoryPoolController_WeaponProjecttiles_Minigun.Despawn(projectile as WeaponInteractionProjectiles_Minigun);
        }

        public override WeaponInteractionProjectile<WeaponInteractionProjectiles_Minigun> SpawnProjectile()
        {
            return _level_MemoryPool_Controller.MemoryPoolController_WeaponProjecttiles_Minigun.Spawn() as WeaponInteractionProjectile<WeaponInteractionProjectiles_Minigun>;
        }
    }
}