using Assets.Scripts.Runtime.Core.Actors.GrassActor;
using Assets.Scripts.Runtime.Systems.CinemachineSystem;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Zenject.Installers.LevelContext
{
    public class Level_Other_Installer : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<MeshRenderer>()
                .FromComponentsSibling()
                .AsTransient()
                .WhenInjectedInto<Grass>();

            //CINEMACHINE
            Container.Bind<CinemachineStateDrivenCamera>()
                .FromComponentInHierarchy()
                .AsCached()
                .WhenInjectedInto<CinemachineSystem>();

            Container.Bind<ControllerCinemachineCameraShake>()
                .FromComponentsSibling()
                .AsCached()
                .WhenInjectedInto<CinemachineSystem>();
        }
    }
}