
using Assets.Scripts.Runtime.Core.Managers.Manager_Scene;
using Assets.Scripts.Runtime.Core.Zenject.Signals.Scene;
using Assets.Scripts.Runtime.Systems.UiSystem.Systems;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Managers.Manager_Ui
{
    /// <summary>
    /// This class controls opening and closing canvases via "UiCanvasSystem"
    /// </summary>
    [RequireComponent(typeof(UiCanvasSystem))]
    public class ControllerUiCanvases : MonoBehaviour
    {
        #region REFERENCES
        private SignalBus _signalBus;
        public UiCanvasSystem UiCanvasSystem { get; private set; }
        private ManagerScene _managerScene;
        #endregion


        [Inject]
        public void Construct(SignalBus signalBus, UiCanvasSystem uiCanvasSystem, ManagerScene managerScene)
        {
            _signalBus = signalBus;
            UiCanvasSystem = uiCanvasSystem;
            _managerScene = managerScene;
        }


        public void SetCanvasesAsSceneOpening(SignalSceneChanged signalSceneChanged)
        {
            UiCanvasSystem.SetActivenessOfAllCanvases(false);

            var targetLevel = signalSceneChanged.SceneIndex;
            var sceneData = _managerScene.GetSceneData(targetLevel);

            for (int i = 0; i < sceneData.OpeningCanvasTypes.Count; i++)
            {
                UiCanvasSystem.SetActivenessOfSpecificCanvas(sceneData.OpeningCanvasTypes[i], true);
            }
        }
    }

}
