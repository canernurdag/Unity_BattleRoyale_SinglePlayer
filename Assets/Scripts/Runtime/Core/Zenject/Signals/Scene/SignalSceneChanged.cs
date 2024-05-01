using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Zenject.Signals.Scene
{
    public class SignalSceneChanged
    {
        public int SceneIndex;

        public SignalSceneChanged(int sceneIndex)
        {
            SceneIndex = sceneIndex;
        }
    }
}