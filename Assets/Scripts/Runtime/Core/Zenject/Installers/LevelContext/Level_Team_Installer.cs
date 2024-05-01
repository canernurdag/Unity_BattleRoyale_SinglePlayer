using Assets.Scripts.Runtime.Core.Controllers.Level;
using Assets.Scripts.Runtime.Core.Team_;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Zenject.Installers.LevelContext
{
    public class Level_Team_Installer : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<TeamSpawnPoint>()
                .FromComponentsInChildren()
                .AsTransient();

            Container.Bind<TeamScene>()
                .FromComponentsInHierarchy()
                .AsCached()
                .WhenInjectedInto<ControllerMatch>();
        }
    }
}