using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction;
using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction.WeaponInteractionProjectiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponAttacks.Variants
{
    public class WeaponAttack_IceGun : WeaponAttack_Projectile<WeaponInteractionProjectiles_Icegun>
    {

        public override void DespawnProjectile(WeaponInteractionProjectile<WeaponInteractionProjectiles_Icegun> projectile)
        {
            _level_MemoryPool_Controller.MemoryPoolController_WeaponProjecttiles_Icegun.Despawn(projectile as WeaponInteractionProjectiles_Icegun);
        }

        public override WeaponInteractionProjectile<WeaponInteractionProjectiles_Icegun> SpawnProjectile()
        {
            return _level_MemoryPool_Controller.MemoryPoolController_WeaponProjecttiles_Icegun.Spawn() as WeaponInteractionProjectile<WeaponInteractionProjectiles_Icegun>;
        }
    }
}