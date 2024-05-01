using Assets.Scripts.Runtime.Core.Managers.Manager_Camera;
using Assets.Scripts.Runtime.Core.Managers.Manager_Cinemachine;
using Assets.Scripts.Runtime.Core.Managers.Manager_Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Zenject.Installers.LevelContext
{
    public class Level_Manager_Installer : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ManagerCinemachine>()
                .FromComponentInHierarchy()
                .AsCached();

            Container.Bind<ManagerInput>()
                .FromComponentInHierarchy()
                .AsCached();

            Container.Bind<ManagerCamera>()
                .FromComponentInHierarchy()
                .AsCached();
        }
    }
}