using Assets.Scripts.Runtime.Core.Ui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Zenject.Installers.ProjectContext
{
    public class Project_Factory_Installer : MonoInstaller
    {
        [SerializeField] private UiSlot_CharacterForMatchMaking _prefabUiSlot_CharacterForMatchmaking;
        [SerializeField] private UiSlot_LobbyRoom _prefabUiSlot_LobbyRoom;
        public override void InstallBindings()
        {
            Container.BindFactory<UiSlot_CharacterForMatchMaking, UiSlot_CharacterForMatchMaking.UiSlotCharacterForMatchMakingFactory>()
            .FromComponentInNewPrefab(_prefabUiSlot_CharacterForMatchmaking);

            Container.BindFactory<UiSlot_LobbyRoom, UiSlot_LobbyRoom.UiSlotLobbyRoomFactory>()
            .FromComponentInNewPrefab(_prefabUiSlot_LobbyRoom);

        }
    }
}