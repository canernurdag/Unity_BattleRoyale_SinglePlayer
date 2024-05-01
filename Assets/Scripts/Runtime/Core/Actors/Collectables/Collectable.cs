using Assets.Scripts.Runtime.Core.Actors.Characters.Character_.GamePlay;
using Assets.Scripts.Runtime.Core.Managers.Manager_Game;
using Assets.Scripts.Runtime.Core.Managers.Manager_Sound;
using Assets.Scripts.Runtime.Core.Managers.Manager_Vibration;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Actors.Collectables
{
    public abstract class Collectable : MonoBehaviour
    {
        #region DIRECT REF
        [SerializeField] protected Transform _visualTransform;
        [SerializeField] protected float _durationToReactive;
        [SerializeField] protected AudioClip _collectAudioClip;
        #endregion

        #region DI REF
        protected ManagerGame _managerGame;
        protected ManagerSound _managerSound;
        protected ManagerVibration _managerVibration;
        #endregion

        #region VAR
        public bool IsActive { get; protected set; } = true;
        protected Tween _visualRotateTween;
        #endregion

        [Inject]
        public void Construct(ManagerGame managerGame, ManagerSound managerSound, ManagerVibration managerVibration)
        {
            _managerGame = managerGame;
            _managerSound = managerSound;
            _managerVibration = managerVibration;
        }
        protected virtual void Start()
        {
            _visualRotateTween = _visualTransform.DORotate(new Vector3(0, 360, 0), 5, RotateMode.WorldAxisAdd)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart);
        }


        protected void OnTriggerEnter(Collider other)
        {
            if (!IsActive) return;

            CharacterGameplay characterGameplay = other.gameObject.GetComponent<CharacterGameplay>();
            if (!characterGameplay) return;

            SetIsActive(false);
            Effect(characterGameplay);

            //SFX
            if (_collectAudioClip)
            {
                _managerSound.PlayAudioClipInPoint(_collectAudioClip, transform.position);
            }



            DOVirtual.DelayedCall(_durationToReactive, () =>
            {
                if (_managerGame.CurrentGameStateType != GameStateType.GameStarted) return;

                SetIsActive(true);
            });

        }

        protected abstract void Effect(CharacterGameplay characterGameplay);

        protected void SetIsActive(bool isActive)
        {
            IsActive = isActive;

            _visualTransform.gameObject.SetActive(isActive);
        }
    }
}