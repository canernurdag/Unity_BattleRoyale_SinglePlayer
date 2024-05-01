using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction
{
    public abstract class WeaponInteractionPoolable<T> : WeaponInteraction where T : WeaponInteractionPoolable<T>
    {

        public Tween MoveTween;
        public Tween JumpTween;

        public Action DespawnCallback;

        public class Pool : MemoryPool<T>
        {
            Transform InitParent;
            protected override void OnCreated(T t)
            {
                InitParent = t.transform.parent;
                t.gameObject.SetActive(false);
            }

            protected override void OnDestroyed(T t)
            {
                Destroy(t.gameObject);
            }

            protected override void OnSpawned(T t)
            {
                ResetObject(t);



                t.gameObject.SetActive(true);
            }

            protected override void OnDespawned(T t)
            {
                t.gameObject.SetActive(false);
                t.transform.SetParent(InitParent);
            }

            protected override void Reinitialize(T t)
            {
                ResetObject(t);

            }

            private void ResetObject(T t)
            {
                t.UsingWeaponAttack = null;
                t.DespawnCallback = null;
                t.MoveTween?.Kill();

                if (t.HitParticleSystem != null) t.HitParticleSystem.Stop();
                t.ActiveTransform.gameObject.SetActive(true);


            }
        }

    }
}