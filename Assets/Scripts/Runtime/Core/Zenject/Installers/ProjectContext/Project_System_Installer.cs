using Assets.Scripts.Runtime.Systems.AnimatorSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Zenject.Installers.ProjectContext
{
    public class Project_System_Installer : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<AnimatorSystem>().FromNew().AsTransient();
        }
    }
}