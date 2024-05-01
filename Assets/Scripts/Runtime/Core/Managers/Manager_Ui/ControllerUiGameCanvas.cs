using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Runtime.Core.Ui;
using Assets.Scripts.Runtime.Core.User_;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Managers.Manager_Ui
{
    /// <summary>
    /// This class controls Ui objects such as levelNo, levelProgress etc which reflects data related to current level
    /// </summary>
    public class ControllerUiGameCanvas : MonoBehaviour
    {
        #region DIRECT REF
        [SerializeField] private Transform _parentTimer;
        [SerializeField] private TMP_Text _textTimer;
        [SerializeField] private UiKillIndicator _ownerTeamUiKillIndicator;
        [SerializeField] private UiKillIndicator _counterTeamUiKillIndicator;
        [SerializeField] private TMP_Text _scoreTextOwnerTeam, _scoreTextCounterTeam;
        #endregion


        #region DI REF
        private CharacterVisualSettings _characterVisualSettings;
        #endregion

        private Tween _ownerTeamIndicatorTween, _counterTeamIndicatorTween;
        private Tween _ownerHelperTween, _counterHelperTween;
        private Tween _parentTimerScaleTween;
        public bool IsParenTimerScaleOn { get; private set; } = false;


        private Vector3 _initPosOwnerTeamIndicator, _initPosCounterTeamIndicator;
        private Vector3 _startPosOwnerTeamIndicator, _startPosCounterTeamIndicator;



        [Inject]
        public void Construct(CharacterVisualSettings characterVisualSettings)
        {
            _characterVisualSettings = characterVisualSettings;
        }

        private void Awake()
        {
            _initPosOwnerTeamIndicator = _ownerTeamUiKillIndicator.transform.position;
            _initPosCounterTeamIndicator = _counterTeamUiKillIndicator.transform.position;

            _startPosOwnerTeamIndicator = _ownerTeamUiKillIndicator.transform.position - Vector3.right * 500;
            _startPosCounterTeamIndicator = _counterTeamUiKillIndicator.transform.position + Vector3.right * 500;

            _ownerTeamUiKillIndicator.gameObject.SetActive(false);
            _counterTeamUiKillIndicator.gameObject.SetActive(false);
        }

        public void SetTimerText(string timerText)
        {
            _textTimer.text = timerText;
        }

        public void SetTimerTextColor(Color color)
        {
            _textTimer.color = color;
        }

        public void StartTimerParentScaleTween()
        {
            _parentTimerScaleTween?.Kill();
            _parentTimerScaleTween = _parentTimer.DOScale(1.2f, 0.6f)
                .SetLoops(-1, LoopType.Yoyo);
            IsParenTimerScaleOn = true;
        }

        public void ResetTimerParent()
        {
            _parentTimerScaleTween?.Kill();
            _parentTimer.transform.localScale = Vector3.one;
            IsParenTimerScaleOn = false;
        }


        public string ConvertSecondsToTimerFormat(float timer)
        {
            float minutes = Mathf.Floor(timer / 60);
            float seconds = Mathf.RoundToInt(timer % 60);

            string minute = minutes.ToString();
            string second = seconds.ToString();


            if (minutes < 10)
            {
                minute = "0" + minutes.ToString();
            }
            if (seconds < 10)
            {
                second = "0" + Mathf.RoundToInt(seconds).ToString();
            }

            return minute + ":" + second;
        }

        public void ShowUiKillIndicator(bool isOwnerTeam, User killerUser, User killedUser)
        {
            UiKillIndicator selectedUiKillIndicator = null;

            if (isOwnerTeam)
            {
                selectedUiKillIndicator = _ownerTeamUiKillIndicator;
            }
            else if (!isOwnerTeam)
            {
                selectedUiKillIndicator = _counterTeamUiKillIndicator;
            }

            Sprite killlerIcon = null;
            Sprite killedIcon = null;


            bool isKillerIconExist = _characterVisualSettings.CharacterVisualDatas.Any(x => x.CharacterVisualType == killerUser.Userdata.CharacterVisualType);

            if (isKillerIconExist)
            {
                killlerIcon = _characterVisualSettings.CharacterVisualDatas.First(x => x.CharacterVisualType == killerUser.Userdata.CharacterVisualType).CharacterIcon;
            }
            else if (!isKillerIconExist)
            {
                Debug.Log("Character Data Does Not Exist In SO");
            }

            bool isKilledIconExist = _characterVisualSettings.CharacterVisualDatas.Any(x => x.CharacterVisualType == killedUser.Userdata.CharacterVisualType);

            if (isKilledIconExist)
            {
                killedIcon = _characterVisualSettings.CharacterVisualDatas.First(x => x.CharacterVisualType == killedUser.Userdata.CharacterVisualType).CharacterIcon;
            }
            else if (!isKilledIconExist)
            {
                Debug.Log("Character Data Does Not Exist In SO");
            }


            selectedUiKillIndicator.SetUiKillIndicator(killlerIcon, killedIcon, killerUser.Userdata.UserName, killedUser.Userdata.UserName);

            if (isOwnerTeam)
            {
                selectedUiKillIndicator.gameObject.SetActive(true);

                _ownerTeamIndicatorTween?.Kill();

                selectedUiKillIndicator.transform.position = _startPosOwnerTeamIndicator;
                _ownerTeamIndicatorTween = selectedUiKillIndicator.transform.DOMove(_initPosOwnerTeamIndicator, 0.4f)
                    .OnComplete(() =>
                    {
                        _ownerHelperTween?.Kill();
                        _ownerHelperTween = DOVirtual.DelayedCall(1f, () =>
                        {
                            selectedUiKillIndicator.gameObject.SetActive(false);
                        });
                    });
            }
            else if (!isOwnerTeam)
            {
                selectedUiKillIndicator.gameObject.SetActive(true);

                _counterTeamIndicatorTween?.Kill();

                selectedUiKillIndicator.transform.position = _startPosCounterTeamIndicator;
                _counterTeamIndicatorTween = selectedUiKillIndicator.transform.DOMove(_initPosCounterTeamIndicator, 0.4f)
                    .OnComplete(() =>
                    {
                        _counterHelperTween?.Kill();
                        _counterHelperTween = DOVirtual.DelayedCall(1f, () =>
                        {
                            selectedUiKillIndicator.gameObject.SetActive(false);
                        });
                    });
            }
        }

        public void SetScoreTextOwnerTeam(int score)
        {
            _scoreTextOwnerTeam.text = score.ToString();
        }

        public void SetScoreTextCounterTeam(int score)
        {
            _scoreTextCounterTeam.text = score.ToString();
        }
    }
}

