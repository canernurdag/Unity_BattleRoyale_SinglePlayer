using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterAttack_.Base;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterAttack
{
    public class CharacterAttackAmmo : MonoBehaviour
    {

        #region DI REF
        private CharacterAttackBase _characterAttack;
        #endregion

        #region DIRECT REF
        [field: SerializeField] public Slider Slider { get; private set; }
        [SerializeField] private Image _fillImage;

        public Color FullColor;
        public Color NotFullColor;
        #endregion

        private Tween _fillTween;

        [Inject]
        public void Construct(CharacterAttackBase characterAttack)
        {
            _characterAttack = characterAttack;
        }


        public void SetSliderValue(float value)
        {
            _fillTween?.Kill();
            Slider.value = value;

            if (value == 1)
            {
                SetFillColor(FullColor);
            }
            else if (value < 1)
            {
                SetFillColor(NotFullColor);
            }
          
        }

        public void SetSliderValue(float value, float speed)
        {
            _fillTween?.Kill();

            _fillTween = DOTween.To(
                () => Slider.value,
                x=>Slider.value = x,
                value,
                speed)
                .SetEase(Ease.Linear)
                .SetSpeedBased()
                .OnComplete(() =>
                {
                    if (value == 1)
                    {
                        SetFillColor(FullColor);
                        _characterAttack.FillTheNextAmmo();
                    }
                    else if (value < 1)
                    {
                        SetFillColor(NotFullColor);
                    }
                });


            
        }

        public void SetFillColor(Color color)
        {
            _fillImage.color = color;
        }
    }
}