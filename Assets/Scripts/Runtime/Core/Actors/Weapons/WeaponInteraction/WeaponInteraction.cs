using Assets.Scripts.Runtime.Core.Actors.Characters.Character_.GamePlay;
using Assets.Scripts.Runtime.Core.Actors.Weapons;
using Assets.Scripts.Runtime.Core.Interfaces.Character_;
using Assets.Scripts.Runtime.Core.Managers.Manager_Addressables;
using Assets.Scripts.Runtime.Core.Managers.Manager_Sound;
using Assets.Scripts.Runtime.Core.Managers.Manager_Vibration;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Actors.Weapons.WeaponInteraction
{
    public abstract class WeaponInteraction : MonoBehaviour
    {
        #region DI REF
        protected ManagerAddressables _managerAddressables;
        protected ManagerSound _managerSound;
        protected ManagerVibration _managerVibration;
        #endregion

        public WeaponAttack UsingWeaponAttack;
        public WeaponSettings.WeaponData WeaponData;
        public List<CharacterGameplay> CounterCharacterGameplays = new();

        [field: SerializeField] public ParticleSystem HitParticleSystem { get; protected set; }
        [field: SerializeField] public AssetReferenceAudioClip AR_TriggerAudioClip { get; protected set; }
        public AudioClip TriggerAudioClip { get; protected set; }
        [field: SerializeField] public AssetReferenceAudioClip AR_HitAudioClip { get; protected set; }
        public AudioClip HitAudioClip { get; protected set; }

        public Transform ActiveTransform;
        public Transform InactiveTransform;

        [Inject]
        public void Construct(ManagerSound managerSound, ManagerVibration managerVibration, ManagerAddressables managerAddressables)
        {
            _managerSound = managerSound;
            _managerVibration = managerVibration;
            _managerAddressables = managerAddressables;
        }
        protected virtual void Awake()
        {
           
            _ = SetTriggerAudioClip();
            _ = SetHitAudioClip();
            
        }

        protected async UniTaskVoid SetTriggerAudioClip()
        {
            if (AR_TriggerAudioClip == null) return;
            if(_managerAddressables == null) return;

            TriggerAudioClip = await _managerAddressables.GetAudioClipAsync(AR_TriggerAudioClip);
        }

        protected async UniTaskVoid SetHitAudioClip()
        {
            if (AR_HitAudioClip == null) return;
            if (_managerAddressables == null) return;

            HitAudioClip = await _managerAddressables.GetAudioClipAsync(AR_HitAudioClip);
        }
        protected abstract void OnTriggerEnter(Collider other);

        protected void Damage(IDamagable hitDamagable)
        {
            if (WeaponData != null)
            {
                if (WeaponData.IsRadiusDamage)
                {
                    for (int i = 0; i < CounterCharacterGameplays.Count; i++)
                    {
                        var character = CounterCharacterGameplays[i];
                        bool isCharacterInside = Vector3.Distance(transform.position, character.transform.position) < WeaponData.DamageRadius;
                        if (isCharacterInside)
                        {
                            int newHealth = character.Damagable.GetHealth() - WeaponData.DamageAmount;
                            character.Damagable.SetHealth(newHealth, UsingWeaponAttack.Character as CharacterGameplay);
                            character.Damagable.SetHealthChange(-WeaponData.DamageAmount);

                            KnockBackAnimSeq(character);
                        }
                    }
                }
                else if (!WeaponData.IsRadiusDamage)
                {
                    if (hitDamagable != null)
                    {
                        var hitCharacterTransform = hitDamagable.GetTransform();
                        if (hitCharacterTransform != null)
                        {
                            var hitCharacterGamePlay = hitCharacterTransform.GetComponent<CharacterGameplay>();
                            if (hitCharacterGamePlay != null)
                            {
                                //TO IMPLEMENT FRIENDLY FIRE AS AN OPTION
                                bool isFriendlyFireOn = false;
                                if (!isFriendlyFireOn)
                                {
                                    if (hitCharacterGamePlay.TeamScene == ((CharacterGameplay)UsingWeaponAttack.Character).TeamScene)
                                    {
                                        return;
                                    }
                                }

                                KnockBackAnimSeq(hitCharacterGamePlay);
                            }
                        }




                        int newHealth = hitDamagable.GetHealth() - WeaponData.DamageAmount;
                        hitDamagable.SetHealth(newHealth, UsingWeaponAttack.Character as CharacterGameplay);
                        hitDamagable.SetHealthChange(-WeaponData.DamageAmount);


                    }
                }
            }
            else if (WeaponData == null)
            {
                Debug.Log("Weapon data does not exist");
            }
        }

        protected void KnockBackAnimSeq(CharacterGameplay character)
        {
            var animatorSystem = character.AnimatorUser.GetAnimatorSystem();
            animatorSystem.SetAnimatorLayerWeight(1, 0);
            animatorSystem.SetCurrentAnimatorState("GetHit2");

            var characterMovement = character.MovableAndRotatable;
            characterMovement.SetPreventMovementAndRotation(true);

            DOVirtual.DelayedCall(1.5f, () =>
            {
                if (animatorSystem != null)
                {
                    animatorSystem.SetAnimatorLayerWeight(1, 1);

                    var characterStateMachine = character.CharacterAttackStateMachine;
                    var initialState = characterStateMachine.GetInitialState();
                    characterStateMachine.SetCurrentState(initialState);

                }
                if (characterMovement != null)
                {
                    characterMovement.SetPreventMovementAndRotation(false);
                }
            });
        }

        protected void OnDrawGizmosSelected()
        {
            if (WeaponData == null) return;
            Gizmos.DrawWireSphere(transform.position, WeaponData.DamageRadius);
        }


    }
}