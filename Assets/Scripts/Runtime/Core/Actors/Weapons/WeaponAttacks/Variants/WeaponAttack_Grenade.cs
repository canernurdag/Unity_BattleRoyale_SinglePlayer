using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction;
using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction.WeaponInteractionThrowable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponAttacks.Variants
{
    public class WeaponAttack_Grenade : WeaponAttack_Throwable<WeaponInteractionThrowable_Grenade>
    {
        public override void DespawnThrowable(WeaponInteractionThrowable<WeaponInteractionThrowable_Grenade> throwable)
        {
            _level_MemoryPool_Controller.MemoryPoolController_WeaponInteractionThrowable_Grenade.Despawn(throwable as WeaponInteractionThrowable_Grenade);
        }

        public override WeaponInteractionThrowable<WeaponInteractionThrowable_Grenade> SpawnThrowable()
        {
            return _level_MemoryPool_Controller.MemoryPoolController_WeaponInteractionThrowable_Grenade.Spawn() as WeaponInteractionThrowable<WeaponInteractionThrowable_Grenade>;
        }
    }
}