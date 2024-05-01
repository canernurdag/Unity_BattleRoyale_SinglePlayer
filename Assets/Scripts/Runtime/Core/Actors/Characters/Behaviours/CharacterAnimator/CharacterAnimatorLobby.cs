using Assets.Scripts.Runtime.Core.Zenject.Signals.Player;
using Assets.Scripts.Runtime.Systems.AnimatorSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterAnimators
{
    public class CharacterAnimatorLobby : MonoBehaviour
    {
        protected AnimatorSystem _animatorSystem;
        protected Animator _animator;
        protected CharacterAnimationSettings _characterAnimationSettings;
        [Inject]
        public void Construct(Animator animator, AnimatorSystem animatorSystem, CharacterAnimationSettings characterAnimationSettings)
        {
            _animatorSystem = animatorSystem;
            _animator = animator;

            _animatorSystem.Animator = _animator;
            _characterAnimationSettings = characterAnimationSettings;

        }



        public void SetIdleAnimationAccordingToWeaponType(SignalPlayerCustomizationWeaponChanged signalUserCustomizationWeaponChanged)
        {
            var weaponType = signalUserCustomizationWeaponChanged.WeaponType;
            bool isWeaponTypeExistInScriptableObject = _characterAnimationSettings.CharacterAnimationDatas
                .Any(x => x.WeaponType == weaponType);

            if (isWeaponTypeExistInScriptableObject)
            {
                var weaponData = _characterAnimationSettings.CharacterAnimationDatas
                .First(x => x.WeaponType == weaponType);

                var idleAnimParam = weaponData.Idle;
                _animatorSystem.SetCurrentAnimatorState(idleAnimParam);
            }
            else if (!isWeaponTypeExistInScriptableObject)
            {
                Debug.Log("Weapon Type Does Not Exist In Scriptable Object");
            }
        }
    }
}