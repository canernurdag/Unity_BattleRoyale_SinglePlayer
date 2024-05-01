using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction;
using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction.WeaponInteractionProjectiles;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponAttacks.Variants
{
    public class WeaponAttack_AssaultRifle : WeaponAttack_Projectile<WeaponInteractionProjectiles_AssaultRifle>
    {
        public override void DespawnProjectile(WeaponInteractionProjectile<WeaponInteractionProjectiles_AssaultRifle> projectile)
        {
            _level_MemoryPool_Controller.MemoryPoolController_WeaponProjecttiles_AssaultRifle.Despawn(projectile as WeaponInteractionProjectiles_AssaultRifle);
        }

        public override WeaponInteractionProjectile<WeaponInteractionProjectiles_AssaultRifle> SpawnProjectile()
        {
            return _level_MemoryPool_Controller.MemoryPoolController_WeaponProjecttiles_AssaultRifle.Spawn() as WeaponInteractionProjectile<WeaponInteractionProjectiles_AssaultRifle>;
        }
    }
}