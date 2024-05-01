using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.StateMachines.CharacterAttackStateMachine;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.StateMachines.CharacterMovementStateMachine;
using Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.StateMachines.CharacterMovementStateMachine.States;
using Assets.Scripts.Runtime.Core.Actors.Characters.Character_.GamePlay;
using Assets.Scripts.Runtime.Core.Controllers.Level;
using Assets.Scripts.Runtime.Core.Interfaces.Character_;
using Assets.Scripts.Runtime.Core.Managers.Manager_Game;
using Assets.Scripts.Runtime.Core.Managers.Manager_Input;
using UniRx;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Actors.Characters.Behaviours.CharacterMovement
{
    public class CharacterMovementPlayer : CharacterMovement, IProvideVelocityValue
    {
        #region DI REF
        private ControllerMatch _controllerMatch;
        private ManagerInput _managerInput;
        private Rigidbody _rigidbody;
        private CharacterPlayerMovementStateMachine _characterPlayerMovementStateMachine;
        private CharacterPlayerAttackStateMachine _characterPlayerAttackStateMachine;
        #endregion

        private List<CharacterGameplay> _counterCharactersGameplay = new();

        private float _tapTrashold = 0.3f;

        #region Init Variables Rotation

        private Quaternion _currentRot;
        private Vector3 _targetRotAngle;
        #endregion

        [Inject]
        public void Construct(ManagerInput managerInput, Rigidbody rigidbody,
            CharacterPlayerMovementStateMachine characterPlayerMovementStateMachine,
            ControllerMatch controllerMatch,
            CharacterPlayerAttackStateMachine characterPlayerAttackStateMachine)
        {
            _managerInput = managerInput;
            _rigidbody = rigidbody;
            _characterPlayerMovementStateMachine = characterPlayerMovementStateMachine;
            _controllerMatch = controllerMatch;
            _characterPlayerAttackStateMachine = characterPlayerAttackStateMachine;
        }

        private void Start()
        {
            Movement_RX();

            SetCounterCharacters();
        }

        private void SetCounterCharacters()
        {
            if (!_controllerMatch) return;

            var teamScene = _controllerMatch.OwnerCharacterGameplay.TeamScene;
            var counterTeamScene = _controllerMatch.TeamScenes.First(x => x != teamScene);
            _counterCharactersGameplay = counterTeamScene.TeamCharacterGameplays;
        }

        private void Movement_RX()
        {
            if (_managerInput == null) return;

            _managerInput.ObservableInputForPlayerMovement
                .Where(_ => _managerGame.CurrentGameStateType == GameStateType.GameStarted)
                .Where(_ => _characterGameplay.IsAlive)
                .Subscribe(input =>
                {
                    //In order to satisfy abstract class parameter, we cast Vector2 to Vector3. Z Value is 0.
                    Vector3 inputV3 = (Vector3)input;

                    if (input == Vector2.zero)
                    {
                        if (IsMoving)
                        {
                            SetIsMoving(false);
                            _characterPlayerMovementStateMachine.SetCurrentState(new CharacterMovementIdleState(_characterPlayerMovementStateMachine));
                        }

                    }
                    else if (input != Vector2.zero)
                    {
                        if (!IsMoving)
                        {
                            SetIsMoving(true);
                            _characterPlayerMovementStateMachine.SetCurrentState(new CharacterMovementMoveState(_characterPlayerMovementStateMachine));
                        }
                        Move(inputV3);
                    }


                    if (!_managerInput.InputProviderForPlayerAttack.IsInputExist())
                    {
                        if (_characterWeaponAttack.GetActiveWeaponAttack().IsWeaponAttackFinished)
                        {
                            Rotate(inputV3);
                        }
                    }

                });

            _managerInput.ObservableInputForPlayerAttack
                .Where(_ => _managerGame.CurrentGameStateType == GameStateType.GameStarted)
                .Where(x => x != Vector2.zero)
                .Where(_ => _characterGameplay.IsAlive)
                .Subscribe(input =>
                {
                    RotateForAttack(input);

                });
        }

        public override void Move(Vector3 target)
        {
            base.Move(target);
            if (PreventMovementAndRotation) return;

            if (target == Vector3.zero) return;
            _rigidbody.AddForce(new Vector3(target.x, 0, target.y) * _characterControllerSettings.SensitivityForPlayerMovement);

        }

        public override void Rotate(Vector3 target)
        {
            base.Rotate(target);
            if (PreventMovementAndRotation) return;
            if (PreventOnlyRotation) return;
            if (target == Vector3.zero) return;


            _currentRot = transform.rotation;
            _targetRotAngle = new Vector3(target.x, _rigidbody.velocity.y, target.y)
                .normalized;
            if (_targetRotAngle == Vector3.zero) _targetRotAngle = new Vector3(0, 0.001f, 0);
            Quaternion lookRotation = Quaternion.LookRotation(_targetRotAngle, Vector3.up);
            lookRotation.x = 0f;
            lookRotation.z = 0f;
            transform.rotation = Quaternion.Lerp(_currentRot, lookRotation,
                Time.fixedDeltaTime * _characterControllerSettings.SensitivityForPlayerRotation);

        }

        public void RotateForAttack(Vector2 input)
        {
            Quaternion targetRotation = Quaternion.identity;
            Quaternion currentRotation = transform.rotation;

            Vector3 direction = new Vector3(input.x, 0, input.y);
            targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, Time.deltaTime * 3f);
        }

        public Vector3 GetCurrentVelocity()
        {
            return _rigidbody.velocity;
        }

        public float GetMaxVelocity()
        {
            return _characterControllerSettings.MaxMovementSpeedPlayer;
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        #region TEST HELPERS
        public void Test_SetRigidbody(Rigidbody rigidbody)
        {
            _rigidbody = rigidbody;
        }


        public void Test_SetMockCharacterControllerSettings()
        {
            CharacterControllerSettings characterControllerSettings = new();
            characterControllerSettings.SensitivityForPlayerMovement = 1200;
            characterControllerSettings.SensitivityForPlayerRotation = 10;
            _characterControllerSettings = characterControllerSettings;
        }
        #endregion

    }

}
