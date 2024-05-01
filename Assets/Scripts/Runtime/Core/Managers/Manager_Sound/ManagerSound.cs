using Assets.Scripts.Runtime.Core.Managers.Manager_Save;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Managers.Manager_Sound
{
    public class ManagerSound : MonoBehaviour
    {
        #region DI REF
        private ManagerSave _managerSave;
        #endregion


        [Inject]
        public void Construct(ManagerSave managerSave)
        {
            _managerSave = managerSave;
        }

        public void PlayAudioClip(AudioSource audioSource, AudioClip audioClip)
        {
            audioSource.PlayOneShot(audioClip, (float)_managerSave.SaveSystem.SaveState.SFXLevel);
        }

        public void PlayAudioClipInPoint(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1)
        {
            AudioSource.PlayClipAtPoint(audioClip, position, (float)(_managerSave.SaveSystem.SaveState.SFXLevel * volumeMultiplier));
        }


    }
}