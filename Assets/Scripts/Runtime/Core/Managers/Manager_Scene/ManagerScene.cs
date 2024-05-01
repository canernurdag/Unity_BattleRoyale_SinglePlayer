using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using System.Linq;
using DG.Tweening;
using Assets.Scripts.Runtime.Core.Managers.Manager_Save;
using Assets.Scripts.Runtime.Core.Managers.Manager_Ui;
using Assets.Scripts.Runtime.Core.Managers.Manager_Game;
using Assets.Scripts.Runtime.Systems.UiSystem.Enum;
using Assets.Scripts.Runtime.Core.Zenject.Signals.Scene;

namespace Assets.Scripts.Runtime.Core.Managers.Manager_Scene
{
    /// <summary>
    /// This class controls level transactions with Unity's SceneManagement.
    /// This classes can be extended with "Addressables" and "Additive Scenes" according to needs
    /// </summary>
    public class ManagerScene : MonoBehaviour
    {

        public List<SceneData> SceneDatas = new List<SceneData>();

        #region REFERENCES
        private ManagerSave _managerSave;
        private ManagerUi _managerUi;
        private ManagerGame _managerGame;
        private SignalBus _signalBus;
        #endregion

        [Inject]
        public void Construct(ManagerSave managerSave, ManagerUi managerUi, ManagerGame managerGame, SignalBus signalBus)
        {
            _managerUi = managerUi;
            _managerSave = managerSave;
            _managerGame = managerGame;
            _signalBus = signalBus;
        }

        public void OpenTheSameScene(bool isAsync)
        {
            ExecuteSceneOpening(isAsync, SceneManager.GetActiveScene().buildIndex);
        }

        /// <summary>
        /// This function can be called to open the next level.
        /// </summary>
        /// <param name="isAsync"></param>
        public void OpenTargetScene(bool isAsync, int sceneIndex)
        {

            ExecuteSceneOpening(isAsync, sceneIndex);

        }

        /// <summary>
        /// The main function that handles level changing process
        /// </summary>
        /// <param name="isAsync"></param>
        /// <param name="levelIndex"></param>
        private void ExecuteSceneOpening(bool isAsync, int levelIndex)
        {
            if (isAsync)
            {
                OpenSceneAsync(levelIndex).Forget();
            }
            else if (!isAsync)
            {
                OpenSceneSync(levelIndex);
            }
        }

        private void OpenSceneSync(int levelIndex)
        {
            DOTween.KillAll();

            SceneManager.LoadScene(levelIndex);

            var targetLevel = SceneDatas[levelIndex];
            _signalBus.Fire(new SignalSceneChanged(levelIndex));
            _managerGame.SetCurrentGameStateType(targetLevel.GameStateType);


        }


        private async UniTask OpenSceneAsync(int levelIndex)
        {
            DOTween.KillAll();

            var loadSceneAsync = SceneManager.LoadSceneAsync(levelIndex);

            _managerUi.ControllerUiCanvases.UiCanvasSystem.SetActivenessOfAllCanvases(false);
            _managerUi.ControllerUiCanvases.UiCanvasSystem.SetActivenessOfSpecificCanvas(CanvasType.CanvasLoading, true);

            _managerGame.SetCurrentGameStateType(GameStateType.Loading);

            while (!loadSceneAsync.isDone)
            {
                _managerUi.ControllerUiLoadingCanvas.SetSliderValue(loadSceneAsync.progress);
                await UniTask.Yield(this.GetCancellationTokenOnDestroy());
            }


            var sceneData = SceneDatas[levelIndex];
            _signalBus.Fire(new SignalSceneChanged(levelIndex));
            _managerGame.SetCurrentGameStateType(sceneData.GameStateType);


        }

        public SceneData GetCurrentSceneData()
        {
            return SceneDatas[SceneManager.GetActiveScene().buildIndex];
        }

        public SceneData GetSceneData(int sceneIndex)
        {
            bool isSceneDataExist = SceneDatas.Any(x => x.SceneIndex == sceneIndex);
            if (isSceneDataExist)
            {
                return SceneDatas.First(x => x.SceneIndex == sceneIndex);
            }

            return null;

        }

    }
}


