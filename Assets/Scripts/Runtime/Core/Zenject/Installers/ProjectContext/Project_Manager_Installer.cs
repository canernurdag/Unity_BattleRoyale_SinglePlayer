using Assets.Scripts.Runtime.Core.Managers.Manager_Addressables;
using Assets.Scripts.Runtime.Core.Managers.Manager_Game;
using Assets.Scripts.Runtime.Core.Managers.Manager_GameSessions;
using Assets.Scripts.Runtime.Core.Managers.Manager_Music;
using Assets.Scripts.Runtime.Core.Managers.Manager_Network;
using Assets.Scripts.Runtime.Core.Managers.Manager_Save;
using Assets.Scripts.Runtime.Core.Managers.Manager_Scene;
using Assets.Scripts.Runtime.Core.Managers.Manager_Sound;
using Assets.Scripts.Runtime.Core.Managers.Manager_Ui;
using Assets.Scripts.Runtime.Core.Managers.Manager_Vibration;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Zenject.Installers.ProjectContext
{
    public class Project_Manager_Installer : MonoInstaller
    {

        public override void InstallBindings()
        {
            Container.Bind<ManagerGame>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ManagerUi>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ManagerSave>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ManagerScene>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ManagerGameSessions>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ManagerNetwork>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ManagerSound>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ManagerMusic>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ManagerVibration>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ManagerAddressables>().FromComponentInHierarchy().AsSingle();
        }
    }
}