using System;
using Assets.Scripts.Runtime.Core.Managers.Manager_Ui;
using Assets.Scripts.Runtime.SystemAdapters.JoystickAdapters.Base;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Assets.Scripts.Runtime.SystemAdapters.JoystickAdapters
{
    public class JoystickInputAdaptorPlayerMovement : JoystickInputAdapter
    {


        [Inject]
        public void Construct(ControllerUiJoystickCanvasPlayerMovement controllerUiJoystickCanvasPlayerMovement)
        {
            _controllerUiJoystickCanvas = controllerUiJoystickCanvasPlayerMovement;
        }



        protected override bool IsTouchInsideTheCorrectArea(Touch touch)
        {
            return touch.position.x < (float)Screen.width / 2;
        }
    }
}