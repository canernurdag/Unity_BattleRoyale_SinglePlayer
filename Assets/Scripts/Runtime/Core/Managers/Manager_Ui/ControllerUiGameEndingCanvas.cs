using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Managers.Manager_Ui
{
    public class ControllerUiGameEndingCanvas : MonoBehaviour
    {
        #region DIRECT REF
        [SerializeField] private TMP_Text _resultText;
        #endregion

        private Tween _scaleTween;


        public void GameEndingSequence(string result, Color resultTextColor, Action callback)
        {
            SetResultText(result, resultTextColor);
            _resultText.transform.localScale = Vector3.zero;

            _scaleTween?.Kill();
            _scaleTween = _resultText.transform.DOScale(1, 0.4f)
                .OnComplete(() =>
                {
                    if (callback != null)
                    {
                        DOVirtual.DelayedCall(2f, () => callback());
                    }
                });
        }

        public void SetResultText(string result, Color resultTextColor)
        {
            _resultText.text = result;
            _resultText.color = resultTextColor;
        }
    }
}