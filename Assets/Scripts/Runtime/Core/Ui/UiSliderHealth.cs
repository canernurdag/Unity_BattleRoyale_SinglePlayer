using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Ui
{
    public class UiSliderHealth : MonoBehaviour
    {
        public RectTransform Green;
        public RectTransform Yellow;

        public TMP_Text _TMPText;

        private Tween _sliderTween;

        private void Awake()
        {
            _TMPText.gameObject.SetActive(false);
        }

        private void Start()
        {
            transform.localScale = Vector3.one * 0.045f;
        }

        public void SetSliderValue(float sliderValue, float duration)
        {
            sliderValue = Mathf.Clamp01(sliderValue);
            Green.localScale = new Vector3(sliderValue, 1, 1);
            _sliderTween?.Kill();
            _sliderTween = DOTween.To(() => Yellow.localScale.x,
                x => Yellow.localScale = new Vector3(x, 1, 1),
                sliderValue,
                duration)
                .OnUpdate(() => Yellow.anchorMax = new Vector2(0, 0.5f));
        }

        public void SetHealthText(string health)
        {
            _TMPText.text = health;
        }

    }
}