using System;
using Assets.Scripts.Runtime.Systems.UiSystem.Systems;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Managers.Manager_Ui
{
    /// <summary>
    /// This class controls Ui Joystick via "UiJoystickSystem"
    /// </summary>
    public class ControllerUiJoystickCanvasPlayerMovement : ControllerUiJoystickCanvas
    {


        [Inject]
        public override void Construct(UiJoystickSystem uiJoystickSystem)
        {
            base.Construct(uiJoystickSystem);

        }


        public override void OnPointerDown(PointerEventData eventData)
        {

            if (!IsTouchInsideOfTheCorrectArea(eventData)) return;

            base.OnPointerDown(eventData);
        }

        public override void OnDrag(PointerEventData eventData)
        {


            if (IsTouchInsideOfTheCorrectArea(eventData))
            {
                base.OnDrag(eventData);
            }

        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (IsTouchInsideOfTheCorrectArea(eventData))
            {
                base.OnPointerUp(eventData);
            }

        }

        private bool IsTouchInsideOfTheCorrectArea(PointerEventData pointerEventData)
        {
            return pointerEventData.position.x < Screen.width / 2;
        }
    }
}

