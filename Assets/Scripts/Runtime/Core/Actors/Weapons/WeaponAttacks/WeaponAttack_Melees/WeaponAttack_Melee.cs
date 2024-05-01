using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponAttacks.WeaponAttack_Melees
{
    public class WeaponAttack_Melee : WeaponAttack
    {
        #region DIRECT REF
        [SerializeField] protected WeaponInteractionMelee _meleeWeaponObject;
        [SerializeField] protected ParticleSystem _particleSystem;
        #endregion

        protected WeaponSettings.WeaponData _weaponData;

        protected virtual void Start()
        {
            ResetWeaponAttackMelee();
        }
        public override void Attack()
        {
            SetIsWeaponAttackFinished(false);
            _meleeWeaponObject.gameObject.SetActive(true);

            if (_particleSystem) _particleSystem.Play();

            var duration = Weapon.GetWeaponData(WeaponType).DurationOfMeleeWeaponActiveness;
            DOVirtual.DelayedCall(duration, () =>
            {
                ResetWeaponAttackMelee();
            });
        }

        private void ResetWeaponAttackMelee()
        {
            SetIsWeaponAttackFinished(true);
            if (_meleeWeaponObject) _meleeWeaponObject.gameObject.SetActive(false);
            if (_particleSystem) _particleSystem.Stop();
        }
    }
}