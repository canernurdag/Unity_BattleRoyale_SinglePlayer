using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction;
using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction.WeaponInteractionProjectiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponAttacks.Variants
{
    public class WeaponAttack_RailGun : WeaponAttack_Projectile<WeaponInteractionProjectiles_Railgun>
    {

        public override void DespawnProjectile(WeaponInteractionProjectile<WeaponInteractionProjectiles_Railgun> projectile)
        {
            _level_MemoryPool_Controller.MemoryPoolController_WeaponProjecttiles_Railgun.Despawn(projectile as WeaponInteractionProjectiles_Railgun);
        }

        public override WeaponInteractionProjectile<WeaponInteractionProjectiles_Railgun> SpawnProjectile()
        {
            return _level_MemoryPool_Controller.MemoryPoolController_WeaponProjecttiles_Railgun.Spawn() as WeaponInteractionProjectile<WeaponInteractionProjectiles_Railgun>;
        }
    }
}