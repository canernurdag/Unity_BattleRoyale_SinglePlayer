using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Runtime.Core.Actors.Characters.Character_.Lobby;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Controllers.Lobby
{
    public class ControllerLobbyObjects : MonoBehaviour
    {
        CharacterLobby _characterPlayerLobby;

        [Inject]
        public void Construct(CharacterLobby characterPlayerLobby)
        {
            _characterPlayerLobby = characterPlayerLobby;
        }

        private IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            _characterPlayerLobby.transform.position = Vector3.zero;
            _characterPlayerLobby.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));

        }
    }
}