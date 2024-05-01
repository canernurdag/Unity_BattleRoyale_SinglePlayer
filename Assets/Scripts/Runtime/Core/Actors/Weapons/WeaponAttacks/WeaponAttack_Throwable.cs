using Assets.Scripts.Runtime.Core.Actors.Characters.Character_.GamePlay;
using Assets.Scripts.Runtime.Core.Actors.Weapons;
using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponAttacks
{
    public abstract class WeaponAttack_Throwable<T> : WeaponAttack where T : WeaponInteractionThrowable<T>
    {
        protected CharacterGameplay _characterGameplay;

        private void Start()
        {
            _characterGameplay = Character as CharacterGameplay;
        }
        public override void Attack()
        {
            var target = _characterGameplay.CharacterWeaponAttack.GetActiveWeaponAimIndicator().WeaponTargets[0];
            var weaponData = Weapon.GetWeaponData(Weapon.WeaponType);

            WeaponInteractionThrowable<T> throwable = SpawnThrowable();
            throwable.UsingWeaponAttack = this;
            throwable.DespawnCallback = () => { if(throwable.gameObject.activeInHierarchy)DespawnThrowable(throwable); };
            throwable.WeaponData = weaponData;
            throwable.CounterCharacterGameplays = _characterGameplay.CounterTeamScene.TeamCharacterGameplays;
            throwable.transform.position = _muzzle.transform.position;
            throwable.transform.forward = _muzzle.transform.forward;

            throwable.Jump(target.transform.position, 20, 1f, 0, () =>
            {
                if (throwable.HitParticleSystem != null)
                {
                    throwable.HitParticleSystem.Play();
                }
                throwable.ActiveTransform.gameObject.SetActive(false);


                DOVirtual.DelayedCall(1f, () =>
                {
                    if (throwable.gameObject.activeInHierarchy)
                        DespawnThrowable(throwable);

                });

                SetIsWeaponAttackFinished(true);
                if (Character.GetType() == typeof(CharacterGameplayPlayer))
                {
                    _characterGameplay.MovableAndRotatable.SetPreventOnlyRotation(false);
                }
            });

        }

        public abstract void DespawnThrowable(WeaponInteractionThrowable<T> throwable);
        public abstract WeaponInteractionThrowable<T> SpawnThrowable();
    }
}