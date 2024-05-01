using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction;
using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction.WeaponInteractionProjectiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponAttacks.Variants
{
    public class WeaponAttack_CrossBow : WeaponAttack_Projectile<WeaponInteractionProjectiles_CrossBow>
    {

        public override void DespawnProjectile(WeaponInteractionProjectile<WeaponInteractionProjectiles_CrossBow> projectile)
        {
            _level_MemoryPool_Controller.MemoryPoolController_WeaponProjecttiles_CrossBow.Despawn(projectile as WeaponInteractionProjectiles_CrossBow);
        }

        public override WeaponInteractionProjectile<WeaponInteractionProjectiles_CrossBow> SpawnProjectile()
        {
            return _level_MemoryPool_Controller.MemoryPoolController_WeaponProjecttiles_CrossBow.Spawn() as WeaponInteractionProjectile<WeaponInteractionProjectiles_CrossBow>;
        }
    }
}