using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Runtime.Core.Managers.Manager_Ui;
using Assets.Scripts.Runtime.Core.Ui.UiDropDown;
using Assets.Scripts.Runtime.Systems.UiSystem.Systems;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Zenject.Installers.ProjectContext
{
    public class Project_Ui_Installer : MonoInstaller
    {

        public override void InstallBindings()
        {

            Container.Bind<UiJoystickSystem>()
                .FromNew()
                .AsCached()
                .WhenInjectedInto<ControllerUiJoystickCanvasPlayerMovement>();

            Container.Bind<UiJoystickSystem>()
                .FromNew()
                .AsCached()
                .WhenInjectedInto<ControllerUiJoystickCanvasPlayerAttack>();

            Container.Bind<ControllerUiJoystickCanvasPlayerMovement>()
                .FromComponentInHierarchy()
                .AsCached();

            Container.Bind<ControllerUiJoystickCanvasPlayerAttack>()
                .FromComponentInHierarchy()
                .AsCached();

            Container.Bind<ControllerUiCountdownCanvas>()
                .FromComponentInHierarchy()
                .AsCached();

            Container.Bind<UiCanvasSystem>()
                .FromComponentInHierarchy()
                .AsCached();


            Container.Bind<ControllerUiCanvases>()
                .FromComponentInHierarchy()
                .AsCached();

            Container.Bind<ControllerUiGameCanvas>()
                .FromComponentInHierarchy()
                .AsCached();

            Container.Bind<ControllerUiLoadingCanvas>()
                .FromComponentInHierarchy()
                .AsCached()
                .WhenInjectedInto<ManagerUi>();

            Container.Bind<ControllerUiHomeCanvas>()
                .FromComponentInHierarchy()
                .AsCached();

            Container.Bind<ControllerUiCharacterListCanvas>()
                .FromComponentInHierarchy()
                .AsCached();

            Container.Bind<ControllerUiCharacterListCanvasButtons>()
                .FromComponentInHierarchy()
                .AsCached();

            Container.Bind<ControllerUiMatchmakingCanvas>()
                .FromComponentInHierarchy()
                .AsCached();


            Container.Bind<UiDropdown_GameType>()
                .FromComponentInChildren()
                .AsTransient();

            Container.Bind<ControllerUiGameEndingCanvas>()
                .FromComponentInHierarchy()
                .AsCached();

        }

    }
}