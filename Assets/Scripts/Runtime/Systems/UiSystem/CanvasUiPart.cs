using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Runtime.Systems.UiSystem
{
    public class CanvasUiPart : MonoBehaviour
    {
        private Vector3 _initScale;
        public Vector3 InitScale => _initScale;

        private Tween _scaleTween;

        private void Awake()
        {
            _initScale = transform.localScale;
        }

        public void SetScale(Vector3 targetScale)
        {
            _scaleTween?.Kill();
            transform.localScale = targetScale;
        }

        public void SetScale(Vector3 targetScale, float duration)
        {
            _scaleTween?.Kill();
            _scaleTween = transform.DOScale(targetScale, duration);
        }

        public void SetScale(Vector3 targetScale, float duration, Action callback)
        {
            _scaleTween?.Kill();
            _scaleTween = transform.DOScale(targetScale, duration)
                .OnComplete(() => callback());
        }

    }
}

