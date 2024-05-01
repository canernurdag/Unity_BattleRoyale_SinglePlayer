using Assets.Scripts.Runtime.Core.Managers.Manager_Ui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Ui
{
    public class UiSlot_LobbyRoom : MonoBehaviour
    {
        public class UiSlotLobbyRoomFactory : PlaceholderFactory<UiSlot_LobbyRoom>
        {

        }

        [SerializeField] private Button _button;

        private ControllerUiLobbyCanvas _controllerUiLobbyCanvas;

        [Inject]
        public void Construct(ControllerUiLobbyCanvas controllerUiLobbyCanvas)
        {
            _controllerUiLobbyCanvas = controllerUiLobbyCanvas;
        }



        private void Start()
        {
            _button.onClick.AddListener(() =>
            {
                _controllerUiLobbyCanvas.SetSelectedUiLobbyRoom(this);
            });
        }
    }
}