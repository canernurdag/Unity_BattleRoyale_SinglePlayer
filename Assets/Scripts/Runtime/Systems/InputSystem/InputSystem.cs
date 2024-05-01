using Assets.Scripts.Runtime.Systems.InputSystem.Enums;
using Lean.Touch;
using UnityEngine;

namespace Assets.Scripts.Runtime.Systems.InputSystem
{
    /// <summary>
    /// This class handles the logic of input system
    /// </summary>
    public class InputSystem : MonoBehaviour
    {
        [field: SerializeField] public InputType InputType { get; private set; }
        [field: SerializeField] public JoystickType JoystickType { get; private set; }

        /// <summary>
        /// Current Vector2 of finger/mouse movement 
        /// </summary>
        public static Vector2 DeltaInputVector { get; private set; } = Vector2.zero;

        public bool PreventInput { get; private set; }


        private void OnEnable()
        {
            if (InputType == InputType.ContinuousDeltaFrame)
            {
                LeanTouch.OnFingerDown += OnFingerDown;
                LeanTouch.OnFingerUpdate += OnFingerUpdate;
                LeanTouch.OnFingerUp += OnFingerUp;
            }
        }

        protected void OnDisable()
        {
            if (InputType == InputType.ContinuousDeltaFrame)
            {
                LeanTouch.OnFingerDown -= OnFingerDown;
                LeanTouch.OnFingerUpdate -= OnFingerUpdate;
                LeanTouch.OnFingerUp -= OnFingerUp;
            }
        }

        private void OnFingerDown(LeanFinger finger)
        {
            if (PreventInput)
            {
                SetDeltaInputVector(Vector2.zero);
            }
            else
            {
                SetDeltaInputVector(finger.ScaledDelta);
            }
        }

        private void OnFingerUpdate(LeanFinger finger)
        {
            if (PreventInput)
            {
                SetDeltaInputVector(Vector2.zero);
            }
            else
            {
                SetDeltaInputVector(finger.ScaledDelta);
            }
        }

        private void OnFingerUp(LeanFinger finger)
        {
            SetDeltaInputVector(Vector2.zero);
        }

        public void SetDeltaInputVector(Vector2 targetVector)
        {
            DeltaInputVector = targetVector;
        }

        public void SetPreventInput(bool value)
        {
            PreventInput = value;
        }

        public bool IsInputExist()
        {
            return Input.GetMouseButton(0) || Input.touchCount > 0;
        }

    }

}
