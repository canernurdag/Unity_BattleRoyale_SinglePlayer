using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterAttack;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterMovement;
using Assets.Scripts.Runtime.Core.Actors.Characters.Character_.GamePlay;
using Assets.Scripts.Runtime.Core.Actors.Collectables;
using Assets.Scripts.Runtime.Core.Controllers.Level;
using Assets.Scripts.Runtime.Core.Interfaces.Character_;
using Assets.Scripts.Runtime.Core.Managers.Manager_Game;
using Assets.Scripts.Runtime.Core.Zenject.Signals.GamePlay;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterAiLogic
{
    public class CharacterAiLogic : MonoBehaviour
    {
        #region DI REF
        private CharacterGameplayAi _characterGameplayAi;
        private IDamagable _damagable;
        private ControllerMatch _controllerMatch;
        private ControllerCollectables _controllerCollectables;
        private CharacterMovementAi _chararcterMovementAi;
        private ManagerGame _managerGame;
        private ICharacterWeaponAttack _characterWeaponAttack;
        private SignalBus _signalBus;
        private CharacterAttackAi _characterAttackAi;
        #endregion

        private Transform _targetWaypoint;
        private CollectableHealUp _targetCollectableHealUp;
        private CharacterGameplay _targetCharacterGameplay;
        private List<CharacterGameplay> _counterCharactersGameplay = new();

        [Inject]
        public void Construct(IDamagable damagable, ControllerMatch controllerMatch, CharacterGameplayAi characterGameplayAi,
            ControllerCollectables controllerCollectables, CharacterMovementAi characterMovementAi, ManagerGame managerGame,
            ICharacterWeaponAttack characterWeaponAttack, SignalBus signalBus, CharacterAttackAi characterAttackAi)
        {
            _damagable = damagable;
            _controllerMatch = controllerMatch;
            _characterGameplayAi = characterGameplayAi;
            _controllerCollectables = controllerCollectables;
            _chararcterMovementAi = characterMovementAi;
            _managerGame = managerGame;
            _characterWeaponAttack = characterWeaponAttack;
            _signalBus = signalBus;
            _characterAttackAi = characterAttackAi;
        }

        private void Start()
        {
            var teamScene = _characterGameplayAi.TeamScene;
            var counterTeamScene = _controllerMatch.TeamScenes.First(x => x != teamScene);
            _counterCharactersGameplay = counterTeamScene.TeamCharacterGameplays;
        }

        private void Update()
        {
            if (_managerGame.CurrentGameStateType != GameStateType.GameStarted) return;

            if (Time.frameCount % 20 != 0) return;

            if (_characterGameplayAi.IsAlive)
            {
                var currentHealthRatio = _damagable.GetCurrentHealthRatio();

                //MOVEMENT AI

                // IS LOW HEALTH
                if (currentHealthRatio < 0.3f)
                {
                    //IS TARGET HEALUP EXIST
                    if (_targetCollectableHealUp)
                    {
                        //IS TARGET HEALUP ACTIVE
                        if (_targetCollectableHealUp.IsActive)
                        {
                            _chararcterMovementAi.Move(_targetCollectableHealUp.transform.position);
                        }
                        else if (!_targetCollectableHealUp.IsActive)
                        {
                            if (_targetWaypoint)
                            {
                                _chararcterMovementAi.Move(_targetWaypoint.transform.position);
                            }
                            else if (!_targetWaypoint)
                            {
                                _targetWaypoint = _controllerMatch.GetRandomWaypoint();
                            }
                        }

                    }
                    else if (!_targetCollectableHealUp)
                    {
                        var targetRandomCollectable = _controllerCollectables.GetRandomActiveCollectableHealUp();
                        if (targetRandomCollectable)
                        {
                            _targetCollectableHealUp = targetRandomCollectable;
                        }
                        else if (!targetRandomCollectable)
                        {
                            if (_targetWaypoint)
                            {
                                _chararcterMovementAi.Move(_targetWaypoint.transform.position);
                            }
                            else if (!_targetWaypoint)
                            {
                                _targetWaypoint = _controllerMatch.GetRandomWaypoint();
                            }
                        }
                    }


                }
                else if (currentHealthRatio >= 0.3f)
                {
                    //IS TARGET CHARACTERGAMEPLAY EXIST
                    if (_targetCharacterGameplay)
                    {
                        float distance = Vector3.Distance(transform.position, _targetCharacterGameplay.transform.position);
                        if (distance >= _characterWeaponAttack.GetActiveWeaponAttack().Range / 2)
                        {
                            _chararcterMovementAi.Move(_targetCharacterGameplay.transform.position);
                        }
                        else if (distance < _characterWeaponAttack.GetActiveWeaponAttack().Range / 2)
                        {
                            if (_targetWaypoint)
                            {
                                _chararcterMovementAi.Move(_targetWaypoint.transform.position);
                            }
                            else if (!_targetWaypoint)
                            {
                                _targetWaypoint = _controllerMatch.GetRandomWaypoint();
                            }
                        }

                    }
                    else if (!_targetCharacterGameplay)
                    {
                        //GET NEW TARGET CHARACTER HERE
                        var targetRandomCharacterGameplay = GetRandomAliveCounterCharacterGameplay();
                        if (targetRandomCharacterGameplay)
                        {
                            _targetCharacterGameplay = targetRandomCharacterGameplay;
                        }
                        else if (!targetRandomCharacterGameplay)
                        {
                            if (_targetWaypoint)
                            {
                                _chararcterMovementAi.Move(_targetWaypoint.transform.position);
                            }
                            else if (!_targetWaypoint)
                            {
                                _targetWaypoint = _controllerMatch.GetRandomWaypoint();
                            }
                        }

                    }

                }

                //ATTACK AI

                var nextReadyAmmo = _characterAttackAi.GetNextReadyAmmo();
                if (nextReadyAmmo)
                {
                    if (!_characterWeaponAttack.GetActiveWeaponAttack().IsWeaponAttackFinished) return;

                    for (int i = 0; i < _counterCharactersGameplay.Count; i++)
                    {
                        var counterCharacter = _counterCharactersGameplay[i];
                        float distance = Vector3.Distance(transform.position, counterCharacter.transform.position);
                        if (distance <= _characterWeaponAttack.GetActiveWeaponAttack().Range * 0.75f)
                        {
                            transform.LookAt(counterCharacter.transform);
                            _characterAttackAi.SetAttackState(new SignalCharacterAttack(_characterGameplayAi, 0));
                            _targetCharacterGameplay = null;
                        }
                    }
                }
            }
            else if (!_characterGameplayAi.IsAlive)
            {
                return;
            }



        }



        private CharacterGameplay GetRandomAliveCounterCharacterGameplay()
        {
            bool isThereAnyAliveCounterCharacterGameplay = _counterCharactersGameplay.Any(x => x.IsAlive);

            if (isThereAnyAliveCounterCharacterGameplay)
            {
                var availableCharactersGameplay = _counterCharactersGameplay.Where(x => x.IsAlive).ToList(); ;

                return availableCharactersGameplay[Random.Range(0, availableCharactersGameplay.Count)];
            }

            return null;
        }
    }
}