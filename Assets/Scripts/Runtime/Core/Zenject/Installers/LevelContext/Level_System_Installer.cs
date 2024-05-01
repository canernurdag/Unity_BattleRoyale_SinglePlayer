using Assets.Scripts.Runtime.Systems.CinemachineSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Zenject.Installers.LevelContext
{
    public class Level_System_Installer : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<CinemachineSystem>()
                .FromComponentInHierarchy()
                .AsCached();
        }
    }
}