using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.StateMachines.CharacterAttackStateMachine;
using Assets.Scripts.Runtime.Core.Actors.Weapons;
using Assets.Scripts.Runtime.Core.Interfaces.Character_;
using Assets.Scripts.Runtime.Systems.AnimatorSystem;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterAnimators
{
    public abstract class CharacterAnimator : MonoBehaviour, IAnimatorUser
    {
        protected AnimatorSystem _animatorSystem;
        public AnimatorSystem AnimatorSystem => _animatorSystem;
        protected Animator _animator;
        protected CharacterAttackStateMachine _characterAttackStateMachine;
        protected IProvideVelocityValue _provideVelocityValue;

        #region Locomotion
        protected Vector3 _direction;
        protected float _signedAngle;
        protected Quaternion _targetRotation;
        protected Vector3 _targetDirection;

        protected float _forwardVelocity;
        protected float _rightVelocity;

        protected CharacterAnimationSettings _characterAnimationSettings;
        #endregion

        [Inject]
        public void Construct(Animator animator, IProvideVelocityValue provideVelocityValue, AnimatorSystem animatorSystem, CharacterAnimationSettings characterAnimationSettings)
        {
            _animatorSystem = animatorSystem;
            _animator = animator;
            _provideVelocityValue = provideVelocityValue;
            _characterAnimationSettings = characterAnimationSettings;
            _animatorSystem.Animator = _animator;

        }

        protected virtual void Start()
        {
            SetVelocitiesRX();


            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    _animatorSystem.SetFloatParameter("MovementHorizontal", _rightVelocity / _provideVelocityValue.GetMaxVelocity());
                    _animatorSystem.SetFloatParameter("MovementVertical", _forwardVelocity / _provideVelocityValue.GetMaxVelocity());

                });


        }

        protected abstract void SetVelocitiesRX();




        public Transform GetTransform()
        {
            return transform;
        }

        public CharacterAnimationSettings.CharacterAnimationData GetCharacterAnimationData(Weapon.Type weaponType)
        {
            bool isDataExist = _characterAnimationSettings.CharacterAnimationDatas.Any(x => x.WeaponType == weaponType);
            if (isDataExist)
            {
                return _characterAnimationSettings.CharacterAnimationDatas.First(x => x.WeaponType == weaponType);
            }
            else if (!isDataExist)
            {
                Debug.Log("Data does not exist");
            }

            return null;
        }

        public AnimatorSystem GetAnimatorSystem()
        {
            return _animatorSystem;
        }
    }

}

