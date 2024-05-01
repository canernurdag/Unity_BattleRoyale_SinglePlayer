using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Runtime.Core.Managers.Manager_Game;
using Assets.Scripts.Runtime.Systems.UiSystem.Enum;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Managers.Manager_Scene
{
    [System.Serializable]
    public class SceneData
    {
        public int SceneIndex;
        public GameStateType GameStateType;
        public List<CanvasType> OpeningCanvasTypes;

        public SceneData(int sceneIndex, GameStateType gameStateType)
        {
            SceneIndex = sceneIndex;
            GameStateType = gameStateType;
        }


    }

}
