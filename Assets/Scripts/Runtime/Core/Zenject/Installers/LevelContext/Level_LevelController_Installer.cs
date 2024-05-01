using Assets.Scripts.Runtime.Core.Controllers.Level;
using Assets.Scripts.Runtime.Core.Zenject.Installers.LevelContext.Level_MemoryPool_Installer;
using Assets.Scripts.Runtime.Systems.CinemachineSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Zenject.Installers.LevelContext
{
    public class Level_LevelController_Installer : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ControllerMatch>()
                .FromComponentInHierarchy()
                .AsCached();

            Container.Bind<ControllerCollectables>()
                .FromComponentInHierarchy()
                .AsCached();

            Container.Bind<ControllerCinemachineCameraShake>()
                .FromComponentInHierarchy()
                .AsCached();

            Container.Bind<Level_MemoryPool_Controller>()
                .FromComponentInHierarchy()
                .AsCached();

            Container.Bind<GhostObjectForCinemachineTarget>()
                .FromComponentInHierarchy()
                .AsCached();



        }
    }
}