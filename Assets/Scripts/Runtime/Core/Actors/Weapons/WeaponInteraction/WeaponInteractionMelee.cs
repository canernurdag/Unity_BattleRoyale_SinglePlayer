using Assets.Scripts.Runtime.Core.Actors.Weapons;
using Assets.Scripts.Runtime.Core.Interfaces.Character_;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction
{
    public class WeaponInteractionMelee : WeaponInteraction
    {
        [Inject]
        public void Construct(WeaponAttack weaponAttack)
        {
            UsingWeaponAttack = weaponAttack;

        }

        private void Start()
        {
            WeaponData = UsingWeaponAttack.Weapon.GetWeaponData(UsingWeaponAttack.WeaponType);
        }

        protected override void OnTriggerEnter(Collider other)
        {
            var usingCharacterGameplay = UsingWeaponAttack.Character;

            if (other.gameObject == usingCharacterGameplay.gameObject) return;

            IDamagable damagable = other.GetComponent<IDamagable>();
            bool isObstacle = other.gameObject.CompareTag("Obstacle");


            if (damagable != null || isObstacle)
            {
                if (HitParticleSystem != null)
                {
                    HitParticleSystem.Play();
                }
            }


            if (damagable != null)
            {
                Damage(damagable);
            }


        }
    }
}