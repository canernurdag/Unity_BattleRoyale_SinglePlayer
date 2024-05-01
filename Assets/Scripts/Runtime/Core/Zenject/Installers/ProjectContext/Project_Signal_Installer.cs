using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Runtime.Core.Managers.Manager_GameSessions;
using Assets.Scripts.Runtime.Core.Managers.Manager_Save;
using Assets.Scripts.Runtime.Core.Managers.Manager_Ui;
using Assets.Scripts.Runtime.Core.Zenject.Signals.Game;
using Assets.Scripts.Runtime.Core.Zenject.Signals.Player;
using Assets.Scripts.Runtime.Core.Zenject.Signals.Scene;
using Assets.Scripts.Runtime.Core.Zenject.Signals.User_;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Zenject.Installers.ProjectContext
{
    public class Project_Signal_Installer : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            //SIGNAL DECLARATION
            Container.DeclareSignal<SignalGameStateChanged>();
            Container.DeclareSignal<SignalSceneChanged>();

            Container.DeclareSignal<SignalPlayerNameSet>();
            Container.DeclareSignal<SignalPlayerMoneyChanged>();

            Container.DeclareSignal<SignalPlayerActiveVisualChanged>();
            Container.DeclareSignal<SignalPlayerActiveWeaponChanged>();

            Container.DeclareSignal<SignalPlayerCustomizationCharacterVisualChanged>();
            Container.DeclareSignal<SignalPlayerCustomizationWeaponChanged>();

            Container.DeclareSignal<SignalUserJoinToTheGameSession>();
            Container.DeclareSignal<SignalUserLeaveFromTheGameSession>();

            Container.DeclareSignal<SignalUserSfxLevelChanged>();
            Container.DeclareSignal<SignalUserMusicLevelChanged>();




            //SIGNAL BINDINGS
            Container.BindSignal<SignalPlayerNameSet>()
                .ToMethod<ControllerUiHomeCanvas>(x => x.SetPlayerNameInUi)
                .FromResolve();

            Container.BindSignal<SignalPlayerNameSet>()
                .ToMethod<ControllerUiCharacterListCanvas>(x => x.SetPlayerNameInUi)
                .FromResolve();


            Container.BindSignal<SignalPlayerActiveVisualChanged>()
                .ToMethod<ControllerUiHomeCanvas>(x => x.SetPlayerIconInUi)
                .FromResolve();

            Container.BindSignal<SignalPlayerActiveVisualChanged>()
                .ToMethod<ControllerUiCharacterListCanvas>(x => x.SetPlayerIconInUi)
                .FromResolve();

            Container.BindSignal<SignalSceneChanged>()
                .ToMethod<ControllerUiCanvases>(x => x.SetCanvasesAsSceneOpening)
                .FromResolve();


            Container.BindSignal<SignalPlayerMoneyChanged>()
                .ToMethod<ControllerUiCharacterListCanvas>(x => x.SetPlayerMoneyAmountInUi)
                .FromResolve();



            Container.BindSignal<SignalPlayerMoneyChanged>()
                .ToMethod<ControllerUiHomeCanvas>(x => x.SetPlayerMoneyAmountInUi)
                .FromResolve();



            Container.BindSignal<SignalUserJoinToTheGameSession>()
                .ToMethod<ManagerGameSessions>(x => x.JoinUserToGameSession)
                .FromResolve();


            Container.BindSignal<SignalUserLeaveFromTheGameSession>()
                .ToMethod<ManagerGameSessions>(x => x.LeaveUserFromGameSession)
                .FromResolve();

            Container.BindSignal<SignalUserJoinToTheGameSession>()
                .ToMethod<ControllerUiMatchmakingCanvas>(x => x.UpdateUiSlots)
                .FromResolve();


            Container.BindSignal<SignalUserLeaveFromTheGameSession>()
                .ToMethod<ControllerUiMatchmakingCanvas>(x => x.UpdateUiSlots)
                .FromResolve();


            Container.BindSignal<SignalUserSfxLevelChanged>()
                .ToMethod<ManagerSave>(x => x.SetSfxLevel)
                .FromResolve();


            Container.BindSignal<SignalUserMusicLevelChanged>()
                .ToMethod<ManagerSave>(x => x.SetMusicLevel)
                .FromResolve();
        }

    }
}