using Assets.Scripts.Runtime.Core.Managers.Manager_Sound;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Managers.Manager_Ui
{
    public class ControllerUiCountdownCanvas : MonoBehaviour
    {
        #region DIRECT REF
        [SerializeField] private TMP_Text _indicatorText;
        [SerializeField] private AudioClip _countDownAudioClip;
        #endregion

        private Vector3 _initScale;
        private Vector3 _bigScale;
        private Tween _scaleTween;

        #region DI REF
        private ManagerSound _managerSound;
        #endregion


        [Inject]
        public void Construct(ManagerSound managerSound)
        {
            _managerSound = managerSound;
        }

        private void Awake()
        {
            _indicatorText.gameObject.SetActive(false);
            _initScale = _indicatorText.transform.localScale;
            _bigScale = _initScale * 1.5f;
        }

        public void SetIndicatorText(string textValue)
        {
            _indicatorText.text = textValue;
        }

        public void StartCountDownSequence(Action onCompleteCallback)
        {
            _indicatorText.gameObject.SetActive(true);
            SetIndicatorText("3");
            ScaleTween(() =>
            {
                _managerSound.PlayAudioClipInPoint(_countDownAudioClip, Vector3.zero, volumeMultiplier: 3);
                SetIndicatorText("2");
                ScaleTween(() =>
                {
                    _managerSound.PlayAudioClipInPoint(_countDownAudioClip, Vector3.zero, volumeMultiplier: 3);
                    SetIndicatorText("1");
                    ScaleTween(() =>
                    {
                        _managerSound.PlayAudioClipInPoint(_countDownAudioClip, Vector3.zero, volumeMultiplier: 3);
                        SetIndicatorText("GO");
                        ScaleTween(() =>
                        {
                            _managerSound.PlayAudioClipInPoint(_countDownAudioClip, Vector3.zero, volumeMultiplier: 3);
                            onCompleteCallback();
                        });
                    });
                });
            });


        }

        private void ScaleTween(Action onComplete)
        {
            _indicatorText.transform.localScale = _bigScale;
            _scaleTween?.Kill();
            _scaleTween?.Kill();
            _scaleTween = _indicatorText.transform.DOScale(_initScale, 0.4f)
                .SetDelay(0.3f)
                .OnComplete(() =>
                {
                    onComplete();
                });
        }
    }
}