using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction;
using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction.WeaponInteractionProjectiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponAttacks.Variants
{
    public class WeaponAttack_Shotgun : WeaponAttack_Projectile<WeaponInteractionProjectiles_Shotgun>
    {
        public override void DespawnProjectile(WeaponInteractionProjectile<WeaponInteractionProjectiles_Shotgun> projectile)
        {
            _level_MemoryPool_Controller.MemoryPoolController_WeaponProjecttiles_Shotgun.Despawn(projectile as WeaponInteractionProjectiles_Shotgun);
        }

        public override WeaponInteractionProjectile<WeaponInteractionProjectiles_Shotgun> SpawnProjectile()
        {
            return _level_MemoryPool_Controller.MemoryPoolController_WeaponProjecttiles_Shotgun.Spawn() as WeaponInteractionProjectile<WeaponInteractionProjectiles_Shotgun>;
        }
    }
}