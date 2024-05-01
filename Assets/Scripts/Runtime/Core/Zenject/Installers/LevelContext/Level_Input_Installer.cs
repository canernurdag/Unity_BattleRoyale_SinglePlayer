using Assets.Scripts.Runtime.Core.Managers.Manager_Input;
using Assets.Scripts.Runtime.SystemAdapters.JoystickAdapters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Zenject.Installers.LevelContext
{
    public class Level_Input_Installer : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IInputAdapter>()
                .WithId("PlayerMovement")
                .To<JoystickInputAdaptorPlayerMovement>()
                .FromNewComponentOn(FindObjectOfType<ManagerInput>().gameObject)
                .AsCached()
                .WhenInjectedInto<ManagerInput>();

            Container.Bind<IInputAdapter>()
                .WithId("PlayerAttack")
                .To<JoystickInputAdaptorPlayerAttack>()
                .FromNewComponentOn(FindObjectOfType<ManagerInput>().gameObject)
                .AsCached()
                .WhenInjectedInto<ManagerInput>();
        }
    }
}