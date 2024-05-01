using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Runtime.Core.Managers.Manager_Scene;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Zenject.Installers
{
    public class StarterSceneInstaller : MonoInstaller
    {

        public override void InstallBindings()
        {
            Container.Bind<ManagerScene>().FromInstance(FindObjectOfType<ManagerScene>()).AsSingle();
        }
    }
}