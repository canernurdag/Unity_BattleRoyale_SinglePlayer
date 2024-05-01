using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction
{
    public class WeaponInteractionThrowable<T> : WeaponInteractionPoolable<T> where T : WeaponInteractionThrowable<T>
    {
        protected override void OnTriggerEnter(Collider other)
        {

        }

        public void Jump(Vector3 targetPos, float jumpPower, float duration, float delay, Action callback)
        {
            JumpTween?.Kill();
            JumpTween = transform.DOJump(targetPos, jumpPower, 1, duration)
                .SetEase(Ease.Linear)
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