using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Runtime.Core.Managers.Manager_Ui;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.SystemAdapters.JoystickAdapters.Base
{
    public abstract class JoystickInputAdapter : MonoBehaviour, IInputAdapter
    {


        protected ControllerUiJoystickCanvas _controllerUiJoystickCanvas;
        protected int _lastActiveTouchId;

        protected int _activeTouchId = -1;
        protected float _touchBeganTime = 0;
        protected float _touchEndedTime = 0;

        protected virtual void Start()
        {
            this.UpdateAsObservable()
                .Subscribe(_ => UpdateActiveTouches());
        }

        protected virtual void UpdateActiveTouches()
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
                            _activeTouchId = -1;
                            _touchEndedTime = Time.time;
                        }

                        break;
                    case TouchPhase.Canceled:
                        if (_activeTouchId == touch.fingerId)
                        {
                            _activeTouchId = -1;
                            _touchEndedTime = Time.time;
                        }

                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public virtual Vector2 GetCurrentMobileInput()
        {
            if (_controllerUiJoystickCanvas == null) { throw new NullReferenceException(); }

            return _controllerUiJoystickCanvas.JoystickInputVector;
        }


        public Transform GetTransform()
        {
            return transform;
        }


        protected abstract bool IsTouchInsideTheCorrectArea(Touch touch);

        public float GetTapDuration()
        {
            return _touchEndedTime - _touchBeganTime;
        }

        public bool IsInputExist()
        {
            for (int i = 0; i < Input.touches.Length; i++)
            {
                var touch = Input.touches[i];
                if (touch.fingerId == _activeTouchId)
                {
                    return true;
                }
            }

            return false;
        }


    }
}