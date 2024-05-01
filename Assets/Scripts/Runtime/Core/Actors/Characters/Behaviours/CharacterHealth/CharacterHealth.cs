using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterParticle;
using Assets.Scripts.Runtime.Core.Controllers.Level;
using Assets.Scripts.Runtime.Core.Interfaces.Character_;
using Assets.Scripts.Runtime.Core.Managers.Manager_Cinemachine;
using Assets.Scripts.Runtime.Core.Managers.Manager_GameSessions;
using Assets.Scripts.Runtime.Core.Managers.Manager_Ui;
using Assets.Scripts.Runtime.Core.Actors.Characters.Character_.GamePlay;
using Assets.Scripts.Runtime.Core.Interfaces.Character;

namespace Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterHealth
{
    public class CharacterHealth : MonoBehaviour, IDamagable
    {

        #region DI REF
        private ManagerGameSessions _managerGameSessions;
        private ICharacterGameplay _characterGameplay;
        private CharacterHealthUi _characterHealthUi;
        private CharacterParticles _characterParticles;
        private ControllerUiGameCanvas _controllerUiGameCanvas;
        private ControllerMatch _controllerMatch;
        private ManagerCinemachine _managerCinemachine;
        #endregion

        [SerializeField] protected int _initHealth = 1000;
        protected int _currentHealth;


        [Inject]
        public void Construct(ManagerGameSessions managerGamerSession, CharacterGameplay characterGameplay, CharacterHealthUi characterHealthUi, CharacterParticles characterParticles,
            ControllerUiGameCanvas controllerUiGameCanvas, ControllerMatch controllerMatch,
            ManagerCinemachine managerCinemachine)
        {
            _managerGameSessions = managerGamerSession;
            _characterGameplay = characterGameplay;
            _characterHealthUi = characterHealthUi;
            _characterParticles = characterParticles;
            _controllerUiGameCanvas = controllerUiGameCanvas;
            _controllerMatch = controllerMatch;
            _managerCinemachine = managerCinemachine;
        }

        private void Awake()
        {
            _currentHealth = _initHealth;
        }

        public int GetInitHealth()
        {
            return _initHealth;
        }

        public int GetHealth()
        {
            return _currentHealth;
        }

        public Transform GetTransform()
        {
            return transform;
        }



        public virtual void SetHealth(int newHealth, ICharacterGameplay attackerCharacterGameplay)
        {
            if (!_characterGameplay.GetIsAlive()) return;

            var currentHealth = _currentHealth;

            if (newHealth < 0) newHealth = 0;
            if (newHealth > _initHealth) newHealth = _initHealth;

            _currentHealth = newHealth;

            if (_currentHealth == 0)
            {
                //INCREASE SCORE 
                var attackerTeam = attackerCharacterGameplay.GetTeam();
                attackerTeam.SetScore(attackerTeam.Score + 1);

                //SHOW KILL UI
                bool isTheSameTeamWithOwner = _controllerMatch.OwnerCharacterGameplay.TeamScene == attackerCharacterGameplay.GetTeamScene();
                _controllerUiGameCanvas.ShowUiKillIndicator(isTheSameTeamWithOwner, attackerCharacterGameplay.GetUser(), _characterGameplay.GetUser());

                //SET SCORE UI
                if (isTheSameTeamWithOwner)
                {
                    _controllerUiGameCanvas.SetScoreTextOwnerTeam(attackerTeam.Score);
                }
                else if (!isTheSameTeamWithOwner)
                {
                    _controllerUiGameCanvas.SetScoreTextCounterTeam(attackerTeam.Score);
                }

                //KILL CHARACTER
                _characterGameplay.SetIsAlive(false);
                _characterParticles.DeathParticle.Play();

                DOVirtual.DelayedCall(_managerGameSessions.ActiveGameSession.DurationForCharacterRivive, () =>
                {
                    _characterGameplay.Revive();
                });
            }


            //WORLD CANVAS UI
            if(_characterHealthUi != null)
            {
                _characterHealthUi.SetHealthSlider(GetCurrentHealthRatio());
                _characterHealthUi.SetHealthText(_currentHealth.ToString());
            }
  

            //CINEMACHINE CAMERA SHAKE
            if (_characterGameplay.GetIsOwner())
            {
                if (newHealth < currentHealth)
                {
                    var activeVCAM = _managerCinemachine.CinemachineSystem.GetActiveVCAM();
                    StartCoroutine(_managerCinemachine.ControllerCinemachineCameraShake.ShakeCamera(activeVCAM, 15, 0.8f));
                }
            }


        }


        public float GetCurrentHealthRatio()
        {
            return _currentHealth / (float)_initHealth;
        }

        public void SetHealthChange(int healthChangeAmount)
        {
            _characterHealthUi.SetHealthChangeSeq(healthChangeAmount);
        }


        #region TEST HELPERS
        public void Test_SetCurrentHealth(int currentHealh)
        {
            _currentHealth = currentHealh;
        }

        public int Test_GetCurrentHealth()
        {
            return _currentHealth;
        }

        public void Test_SetCharacterGameplay(ICharacterGameplay characterGameplay)
        {
            _characterGameplay = characterGameplay;
        }
        #endregion

    }
}

