using Assets.Scripts.Runtime.Core.Actors.Characters.Character_.GamePlay;
using Assets.Scripts.Runtime.Core.Actors.Weapons;
using Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponAttacks
{
    public abstract class WeaponAttack_Projectile<T> : WeaponAttack where T : WeaponInteractionProjectile<T>
    {
        [SerializeField] protected float _delayBetweenShots;
        protected CharacterGameplay _characterGameplay;

        private void Start()
        {
            _characterGameplay = Character as CharacterGameplay;
        }
        public override void Attack()
        {
            var targets = _characterGameplay.CharacterWeaponAttack.GetActiveWeaponAimIndicator().WeaponTargets;
            SetIsWeaponAttackFinished(false);

            for (int i = 0; i < targets.Count; i++)
            {
                int index = i;
                var target = targets[i];
                var weaponData = Weapon.GetWeaponData(Weapon.WeaponType);
                var delay = index * _delayBetweenShots;

                DOVirtual.DelayedCall(delay, () =>
                {
                    WeaponInteractionProjectile<T> projectile = SpawnProjectile();
                    projectile.UsingWeaponAttack = this;
                    projectile.DespawnCallback = () => { if(projectile.gameObject.activeInHierarchy) DespawnProjectile(projectile); };
                    projectile.WeaponData = weaponData;
                    projectile.CounterCharacterGameplays = _characterGameplay.CounterTeamScene.TeamCharacterGameplays;
                    projectile.transform.position = _muzzle.transform.position;
                    Vector3 direction = (target.transform.position - _muzzle.transform.position).normalized;
                    projectile.transform.rotation = Quaternion.LookRotation(direction);

                    if (projectile.TriggerAudioClip != null)
                    {
                        _managerSound.PlayAudioClipInPoint(projectile.TriggerAudioClip, transform.position);
                    }


                    projectile.Move(target.transform.position, 50, 0, () =>
                    {
                        if (projectile.HitParticleSystem != null)
                        {
                            projectile.HitParticleSystem.Play();
                        }

                        if (_characterGameplay.IsOwner)
                        {
                            if (projectile.HitAudioClip != null)
                            {
                                _managerSound.PlayAudioClipInPoint(projectile.HitAudioClip, transform.position);
                            }

                            _managerVibration.Vibrate();
                        }

                        projectile.ActiveTransform.gameObject.SetActive(false);


                        DOVirtual.DelayedCall(1f, () =>
                        {
                            if (projectile.gameObject.activeInHierarchy)
                                DespawnProjectile(projectile);

                        });


                    });

                    if (index == targets.Count - 1)
                    {
                        SetIsWeaponAttackFinished(true);
                        if (Character.GetType() == typeof(CharacterGameplayPlayer))
                        {
                            _characterGameplay.MovableAndRotatable.SetPreventOnlyRotation(false);
                        }
                    }
                });


            }
        }

        public abstract void DespawnProjectile(WeaponInteractionProjectile<T> projectile);

        public abstract WeaponInteractionProjectile<T> SpawnProjectile();
    }
}