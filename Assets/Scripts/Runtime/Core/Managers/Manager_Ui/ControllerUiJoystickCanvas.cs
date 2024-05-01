using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Runtime.Systems.InputSystem.Enums;
using Assets.Scripts.Runtime.Systems.UiSystem.Systems;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Managers.Manager_Ui
{
    public abstract class ControllerUiJoystickCanvas : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        #region REFERENCES
        [SerializeField] protected RectTransform _joystickImageBackground = null;
        [SerializeField] protected RectTransform _joystickImageHandle = null;
        [SerializeField] protected RectTransform _panelRecttransform;
        [SerializeField] protected Canvas _joystickCanvas;
        #endregion

        #region Cache
        protected UiJoystickSystem _uiJoystickSystem;
        #endregion

        #region Internal variables
        [SerializeField] protected JoystickType _activeJoystickType;
        [SerializeField] protected float _deadZone = 0;
        [SerializeField] protected float _handleRange = 1;
        protected Vector2 _joystickImageBackgroundRadiusVector;
        #endregion

        #region Output
        public Vector2 JoystickInputVector { get; private set; }
        #endregion

        #region Dynamic Joystick
        [Header("Dynamic Joystick")]
        [SerializeField] protected float _moveTreshold = 1;
        #endregion

        [Inject]
        public virtual void Construct(UiJoystickSystem uiJoystickSystem)
        {
            _uiJoystickSystem = uiJoystickSystem;
        }

        protected virtual void Start()
        {

            SetJoystickVisualActiveness();
            Vector2 center = new Vector2(0.5f, 0.5f);
            _joystickImageBackground.pivot = center;
            _joystickImageHandle.pivot = center;
            _joystickImageHandle.anchorMin = center;
            _joystickImageHandle.anchorMax = center;

            _joystickImageBackgroundRadiusVector = _joystickImageBackground.sizeDelta / 2;

        }


        public virtual void OnPointerDown(PointerEventData eventData)
        {
            switch (_activeJoystickType)
            {
                case JoystickType.Static:
                    break;
                case JoystickType.Floating:
                    _joystickImageBackground.anchoredPosition = _uiJoystickSystem.GetAnchoredPositionFromScreenPoint(eventData.position, _panelRecttransform, _joystickImageBackground);
                    _joystickImageBackground.gameObject.SetActive(true);
                    break;
                case JoystickType.Dynamic:
                    _joystickImageBackground.anchoredPosition = _uiJoystickSystem.GetAnchoredPositionFromScreenPoint(eventData.position, _panelRecttransform, _joystickImageBackground);
                    _joystickImageBackground.gameObject.SetActive(true);
                    break;
            }
            OnDrag(eventData);
        }

        public virtual void OnDrag(PointerEventData eventData)
        {

            Vector2 screenPositionOfJoystickImageBackground = RectTransformUtility.WorldToScreenPoint(null, _joystickImageBackground.position);
            JoystickInputVector = (eventData.position - screenPositionOfJoystickImageBackground) / (_joystickImageBackgroundRadiusVector * _joystickCanvas.scaleFactor);
            HandleInput(JoystickInputVector);
            _joystickImageHandle.anchoredPosition = JoystickInputVector * _joystickImageBackgroundRadiusVector * _handleRange;

        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            switch (_activeJoystickType)
            {
                case JoystickType.Static:
                    break;
                case JoystickType.Floating:
                    _joystickImageBackground.gameObject.SetActive(false);
                    break;
                case JoystickType.Dynamic:
                    _joystickImageBackground.gameObject.SetActive(false);
                    break;
            }

            JoystickInputVector = Vector2.zero;
            _joystickImageHandle.anchoredPosition = Vector2.zero;
        }


        protected virtual void HandleInput(Vector2 input)
        {
            switch (_activeJoystickType)
            {
                case JoystickType.Static:
                    break;
                case JoystickType.Floating:
                    break;

                case JoystickType.Dynamic:
                    if (input.magnitude > _moveTreshold)
                    {
                        Vector2 differenceVector = input.normalized * (input.magnitude - _moveTreshold) *
                                             _joystickImageBackgroundRadiusVector;
                        _joystickImageBackground.anchoredPosition += differenceVector;
                    }
                    break;
            }

            if (input.magnitude > _deadZone)
            {
                if (input.magnitude > 1) JoystickInputVector = input.normalized;
                else JoystickInputVector = input;
            }
            else
                JoystickInputVector = Vector2.zero;
        }

        #region Set Functions
        protected virtual void SetJoystickVisualActiveness()
        {
            switch (_activeJoystickType)
            {
                case JoystickType.Static:
                    break;
                case JoystickType.Floating:
                    _joystickImageBackground.gameObject.SetActive(false);
                    break;
                case JoystickType.Dynamic:
                    _joystickImageBackground.gameObject.SetActive(false);
                    break;
            }
        }
        #endregion

        public void ResetJoystick()
        {
            _joystickImageBackground.gameObject.SetActive(false);
            JoystickInputVector = Vector2.zero;
            _joystickImageHandle.anchoredPosition = Vector2.zero;
        }
    }
}
