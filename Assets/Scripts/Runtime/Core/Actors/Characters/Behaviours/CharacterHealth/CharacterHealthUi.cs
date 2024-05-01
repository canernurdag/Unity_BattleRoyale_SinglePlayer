using Assets.Scripts.Runtime.Core.Actors.Characters.Character_.GamePlay;
using Assets.Scripts.Runtime.Core.Controllers.Level;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterHealth
{
    public class CharacterHealthUi : MonoBehaviour
    {
        #region DIRECT REF
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private Image _healthSliderFillImage;
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private TMP_Text _healthChangeText;

        [field: SerializeField] public Sprite Normal { get; private set; }
        [field: SerializeField] public Sprite Beware { get; private set; }
        [field: SerializeField] public Sprite Danger { get; private set; }
        [field: SerializeField] public Sprite TheSameTeam { get; private set; }
        [field: SerializeField] public Sprite TheEnemyTeam { get; private set; }
        #endregion

        #region DI REF
        private CharacterGameplay _characterGameplay;
        private ControllerMatch _controllerMatch;
        #endregion

        private Vector3 _initScaleHealthChangeText;
        private Sequence _healthChangeSequence;
        private Tween _sliderTween;
        private Sequence _flashEffectSequence;



        [Inject]
        public void Construct(CharacterGameplay characterGameplay, ControllerMatch controllerMatch)
        {
            _characterGameplay = characterGameplay;
            _controllerMatch = controllerMatch;
        }

        private IEnumerator Start()
        {
            _initScaleHealthChangeText = _healthChangeText.transform.localScale;
            _healthChangeText.transform.localScale = Vector3.zero;
            _healthChangeSequence = DOTween.Sequence();
            _flashEffectSequence = DOTween.Sequence();

            yield return new WaitForEndOfFrame();
            var ownerCharacterTeamScene = _controllerMatch.OwnerCharacterGameplay.TeamScene;
            var characterTeamScene = _characterGameplay.TeamScene;

        }

        public void DoFlashEffect(Sprite sprite1, Sprite sprite2)
        {
            _flashEffectSequence?.Kill();
            _flashEffectSequence = DOTween.Sequence();
            SetFillImageSprite(sprite1);
            _flashEffectSequence.Append(DOVirtual.DelayedCall(0.4f, () => { SetFillImageSprite(sprite2); }));
            _flashEffectSequence.Append(DOVirtual.DelayedCall(0.4f, () => { SetFillImageSprite(sprite1); }));

            _flashEffectSequence.SetLoops(-1, LoopType.Restart);

        }

        public void SetFillImageSprite(Sprite sprite)
        {
            _healthSliderFillImage.sprite = sprite;
        }

        public void SetHealthSlider(float sliderValue)
        {
            _sliderTween?.Kill();
            _sliderTween = DOTween.To(
                () => _healthSlider.value,
                x => _healthSlider.value = x,
                sliderValue,
                0.3f);


            if (_characterGameplay.IsOwner)
            {
                if (sliderValue >= 0.5f)
                {
                    _flashEffectSequence?.Kill();

                    SetFillImageSprite(Normal);
                }
                else if (sliderValue < 0.5f && sliderValue >= 0.25f)
                {
                    DoFlashEffect(Normal, Beware);
                }
                else if (sliderValue < 0.25f)
                {
                    DoFlashEffect(Normal, Danger);
                }
            }
            else if (!_characterGameplay.IsOwner)
            {
                if (_characterGameplay.IsSameTeamWithOwner)
                {
                    SetFillImageSprite(TheSameTeam);
                }
                else if (!_characterGameplay.IsSameTeamWithOwner)
                {
                    SetFillImageSprite(TheEnemyTeam);
                }
            }
        }

        public void SetHealthText(string health)
        {
            _healthText.text = health;
        }

        public void SetHealthChangeSeq(int healthChange)
        {
            //SET COLOR
            if (!_characterGameplay.IsOwner)
            {
                SetHealthChangeTextColor(Color.white);
            }
            else if (_characterGameplay.IsOwner)
            {
                if (healthChange > 0)
                {
                    SetHealthChangeTextColor(Color.green);
                }
                else if (healthChange < 0)
                {
                    SetHealthChangeTextColor(Color.red);
                }
            }

            //SET TEXT
            SetHealthChangeText(Mathf.Abs(healthChange).ToString());

            //SET TWEEN
            _healthChangeText.transform.localScale = Vector3.zero;

            _healthChangeSequence?.Kill();
            _healthChangeSequence = DOTween.Sequence();

            _healthChangeSequence.Append(_healthChangeText.transform.DOScale(_initScaleHealthChangeText, 0.5f));
            _healthChangeSequence.Append(_healthChangeText.transform.DOScale(0, 0.3f).SetDelay(0.6f));
        }
        private void SetHealthChangeText(string healthChange)
        {
            _healthChangeText.text = healthChange;
        }

        private void SetHealthChangeTextColor(Color color)
        {
            _healthChangeText.color = color;
        }
    }
}