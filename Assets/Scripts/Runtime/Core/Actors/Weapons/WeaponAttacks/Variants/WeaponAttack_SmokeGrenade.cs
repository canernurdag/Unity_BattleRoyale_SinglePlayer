using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction;
using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction.WeaponInteractionThrowable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponAttacks.Variants
{
    public class WeaponAttack_SmokeGrenade : WeaponAttack_Throwable<WeaponInteractionThrowable_SmokeGrenade>
    {

        public override void DespawnThrowable(WeaponInteractionThrowable<WeaponInteractionThrowable_SmokeGrenade> throwable)
        {
            _level_MemoryPool_Controller.MemoryPoolController_WeaponInteractionThrowable_SmokeGrenade.Despawn(throwable as WeaponInteractionThrowable_SmokeGrenade);
        }

        public override WeaponInteractionThrowable<WeaponInteractionThrowable_SmokeGrenade> SpawnThrowable()
        {
            return _level_MemoryPool_Controller.MemoryPoolController_WeaponInteractionThrowable_SmokeGrenade.Spawn() as WeaponInteractionThrowable<WeaponInteractionThrowable_SmokeGrenade>;
        }
    }
}