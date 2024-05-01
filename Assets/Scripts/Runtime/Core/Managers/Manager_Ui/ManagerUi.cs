using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Managers.Manager_Ui
{
    /// <summary>
    /// This is the main class that includes all ui control classes, except for "World Space Canvases".
    /// </summary>
    public class ManagerUi : MonoBehaviour
    {
        #region REFERENCES
        public ControllerUiCanvases ControllerUiCanvases { get; protected set; }
        public ControllerUiJoystickCanvasPlayerMovement ControllerUiJoystickCanvasPlayerMovement { get; protected set; }
        public ControllerUiGameCanvas ControllerUiLevelCanvas { get; protected set; }
        public ControllerUiLoadingCanvas ControllerUiLoadingCanvas { get; protected set; }
        public ControllerUiCountdownCanvas ControllerUiCountdownCanvas { get; protected set; }
        #endregion


        [Inject]
        public void Construct(ControllerUiCanvases controllerUiCanvases, ControllerUiJoystickCanvasPlayerMovement controllerUiJoystickCanvasPlayerMovement,
            ControllerUiGameCanvas controllerUiLevelCanvas, ControllerUiLoadingCanvas controllerUiLoadingCanvas, ControllerUiCountdownCanvas controllerUiCountdownCanvas)
        {
            ControllerUiCanvases = controllerUiCanvases;
            ControllerUiJoystickCanvasPlayerMovement = controllerUiJoystickCanvasPlayerMovement;
            ControllerUiLevelCanvas = controllerUiLevelCanvas;
            ControllerUiLoadingCanvas = controllerUiLoadingCanvas;
            ControllerUiCountdownCanvas = controllerUiCountdownCanvas;

        }


    }


}


