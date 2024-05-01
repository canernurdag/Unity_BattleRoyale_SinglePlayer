using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Systems.AnimatorSystem
{
    public class AnimatorSystem
    {
        public Animator Animator;

        protected Tween _parameterTween;
        protected Tween _layerTween;


        #region Set Functions

        public virtual void SetAnimatorActiveness(bool value)
        {
            Animator.enabled = value;
        }

        public virtual void SetCurrentAnimatorStateAndPlayImmediately(string stateName, int layerNo, float normalizedTime)
        {
            Animator.Play(stateName, layerNo, normalizedTime);
        }

        public virtual void SetCurrentAnimatorState(string stateName)
        {
            Animator.SetTrigger(stateName);
        }
        public virtual void SetCurrentAnimatorState(string stateName, bool boolValue)
        {
            Animator.SetBool(stateName, boolValue);
        }

        public virtual void SetAnimatorLayerWeight(int layerIndex, float layerWeight)
        {
            Animator.SetLayerWeight(layerIndex, layerWeight);
        }

        public virtual void SetAnimatorLayerWeight(int layerIndex, float layerWeight, float duration)
        {
            _layerTween?.Kill();
            _layerTween = DOTween.To(() => Animator.GetLayerWeight(layerIndex), x => Animator.SetLayerWeight(layerIndex, x), layerWeight,
                duration);
        }

        public virtual void SetFloatParameter(string floatName, float value)
        {
            Animator.SetFloat(floatName, value);
        }

        public virtual void SetFloatParameter(string floatName, float value, float duration)
        {
            _parameterTween?.Kill();
            _parameterTween = DOTween.To(() => Animator.GetFloat(floatName), x => Animator.SetFloat(floatName, x), value, duration);
        }

        #endregion
    }
}