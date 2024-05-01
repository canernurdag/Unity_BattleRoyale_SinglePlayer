using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction;
using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction.WeaponInteractionProjectiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponAttacks.Variants
{
    public class WeaponAttack_Pistol : WeaponAttack_Projectile<WeaponInteractionProjectiles_Pistol>
    {

        public override void DespawnProjectile(WeaponInteractionProjectile<WeaponInteractionProjectiles_Pistol> projectile)
        {
            _level_MemoryPool_Controller.MemoryPoolController_WeaponProjecttiles_Pistol.Despawn(projectile as WeaponInteractionProjectiles_Pistol);
        }

        public override WeaponInteractionProjectile<WeaponInteractionProjectiles_Pistol> SpawnProjectile()
        {
            return _level_MemoryPool_Controller.MemoryPoolController_WeaponProjecttiles_Pistol.Spawn() as WeaponInteractionProjectile<WeaponInteractionProjectiles_Pistol>;
        }
    }
}