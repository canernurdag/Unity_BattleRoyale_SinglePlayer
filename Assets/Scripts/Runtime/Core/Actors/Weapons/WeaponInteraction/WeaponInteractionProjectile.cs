using Assets.Scripts.Runtime.Core.Actors.Characters.Character_.GamePlay;
using Assets.Scripts.Runtime.Core.Interfaces.Character_;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction
{
    public class WeaponInteractionProjectile<T> : WeaponInteractionPoolable<T> where T : WeaponInteractionProjectile<T>
    {

        protected override void OnTriggerEnter(Collider other)
        {
            var usingCharacterGameplay = UsingWeaponAttack.Character as CharacterGameplay;

            if (other.gameObject == usingCharacterGameplay.gameObject) return;

            IDamagable damagable = other.GetComponent<IDamagable>();
            bool isObstacle = other.gameObject.CompareTag("Obstacle");


            if (damagable != null || isObstacle)
            {

                MoveTween?.Kill();
                if (HitParticleSystem != null)
                {
                    HitParticleSystem.Play();
                }

                ActiveTransform.gameObject.SetActive(false);

                if (DespawnCallback != null)
                {
                    DespawnCallback();
                }
            }


            if (damagable != null)
            {
                Damage(damagable);
            }

            if (usingCharacterGameplay.IsOwner)
            {
                if (HitAudioClip != null)
                {
                    _managerSound.PlayAudioClipInPoint(HitAudioClip, transform.position);
                }

                _managerVibration.Vibrate();
            }

        }

        public void Move(Vector3 targetPos, float speed, float delay, Action callback)
        {
            MoveTween?.Kill();
            MoveTween = transform.DOMove(targetPos, speed)
                .SetEase(Ease.Linear)
                .SetSpeedBased()
                .SetDelay(delay)
                .OnComplete(() =>
                {
                    if (HitParticleSystem != null)
                    {
                        HitParticleSystem.Play();
                    }

                    Damage(null);

                    if (callback != null)
                        callback();
                });
        }





    }
}