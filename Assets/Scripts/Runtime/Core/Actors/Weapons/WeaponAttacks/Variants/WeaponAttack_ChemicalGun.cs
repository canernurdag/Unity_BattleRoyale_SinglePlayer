using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction;
using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction.WeaponInteractionProjectiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponAttacks.Variants
{
    public class WeaponAttack_ChemicalGun : WeaponAttack_Projectile<WeaponInteractionProjectiles_ChemicalGun>
    {

        public override void DespawnProjectile(WeaponInteractionProjectile<WeaponInteractionProjectiles_ChemicalGun> projectile)
        {
            _level_MemoryPool_Controller.MemoryPoolController_WeaponProjecttiles_ChemicalGun.Despawn(projectile as WeaponInteractionProjectiles_ChemicalGun);
        }

        public override WeaponInteractionProjectile<WeaponInteractionProjectiles_ChemicalGun> SpawnProjectile()
        {
            return _level_MemoryPool_Controller.MemoryPoolController_WeaponProjecttiles_ChemicalGun.Spawn() as WeaponInteractionProjectile<WeaponInteractionProjectiles_ChemicalGun>;
        }
    }
}