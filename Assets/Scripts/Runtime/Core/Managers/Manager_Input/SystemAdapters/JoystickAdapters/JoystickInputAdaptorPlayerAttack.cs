using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Runtime.Core.Actors.Weapons;
using Assets.Scripts.Runtime.Core.Controllers.Level;
using Assets.Scripts.Runtime.Core.Managers.Manager_Ui;
using Assets.Scripts.Runtime.Core.Zenject.Signals.GamePlay;
using Assets.Scripts.Runtime.SystemAdapters.JoystickAdapters.Base;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Assets.Scripts.Runtime.SystemAdapters.JoystickAdapters
{
    public class JoystickInputAdaptorPlayerAttack : JoystickInputAdapter
    {
        private SignalBus _signalBus;
        private ControllerMatch _controllerMatch;

        [Inject]
        public void Construct(ControllerUiJoystickCanvasPlayerAttack controllerUiJoystickCanvasPlayerAttack, SignalBus signalBus, ControllerMatch controllerMatch)
        {
            _controllerUiJoystickCanvas = controllerUiJoystickCanvasPlayerAttack;
            _signalBus = signalBus;
            _controllerMatch = controllerMatch;
        }


        protected override void UpdateActiveTouches()
        {
            for (int i = 0; i < Input.touches.Length; i++)
            {
                var touch = Input.touches[i];

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if (!IsTouchInsideTheCorrectArea(touch)) continue;

                        if (_activeTouchId == -1)
                        {
                            _lastActiveTouchId = touch.fingerId;
                            _activeTouchId = touch.fingerId;
                            _touchBeganTime = Time.time;
                            _controllerMatch.OwnerCharacterGameplay.CharacterWeaponAttack.GetActiveWeaponAimIndicator().gameObject.SetActive(true);
                            if (_controllerMatch.OwnerCharacterGameplay.CharacterWeaponAttack.GetActiveWeaponAttack().WeaponAttackType == WeaponAttack.Type.Throwable)
                            {
                                _controllerMatch.OwnerCharacterGameplay.CharacterLineRenderer.SetActivenessOfLineRenderer(true);
                                _controllerMatch.OwnerCharacterGameplay.CharacterLineRenderer.SetTargetTranform(_controllerMatch.OwnerCharacterGameplay.CharacterWeaponAttack.GetActiveWeaponAimIndicator().transform);
                            }

                        }
                        else if (_activeTouchId != -1)
                        {

                        }

                        break;
                    case TouchPhase.Moved:
                        break;
                    case TouchPhase.Stationary:
                        break;
                    case TouchPhase.Ended:
                        if (_activeTouchId == touch.fingerId)
                        {
                            _touchEndedTime = Time.time;

                            float tapDuration = GetTapDuration();
                            _signalBus.Fire(new SignalCharacterAttack(_controllerMatch.OwnerCharacterGameplay, tapDuration));
                            _controllerMatch.OwnerCharacterGameplay.CharacterWeaponAttack.GetActiveWeaponAimIndicator().gameObject.SetActive(false);
                            _controllerMatch.OwnerCharacterGameplay.MovableAndRotatable.SetPreventOnlyRotation(true);
                            _controllerMatch.OwnerCharacterGameplay.CharacterLineRenderer.SetActivenessOfLineRenderer(false);


                            _activeTouchId = -1;



                        }

                        break;
                    case TouchPhase.Canceled:
                        if (_activeTouchId == touch.fingerId)
                        {
                            _touchEndedTime = Time.time;

                            float tapDuration = GetTapDuration();
                            _signalBus.Fire(new SignalCharacterAttack(_controllerMatch.OwnerCharacterGameplay, tapDuration));
                            _controllerMatch.OwnerCharacterGameplay.CharacterWeaponAttack.GetActiveWeaponAimIndicator().gameObject.SetActive(false);
                            _controllerMatch.OwnerCharacterGameplay.MovableAndRotatable.SetPreventOnlyRotation(true);
                            _controllerMatch.OwnerCharacterGameplay.CharacterLineRenderer.SetActivenessOfLineRenderer(false);


                            _activeTouchId = -1;



                        }

                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        protected override bool IsTouchInsideTheCorrectArea(Touch touch)
        {
            bool isInside = touch.position.x > (float)Screen.width / 2;
            return isInside;
        }


    }
}