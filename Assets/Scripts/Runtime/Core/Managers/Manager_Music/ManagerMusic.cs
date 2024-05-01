using Assets.Scripts.Runtime.Core.Managers.Manager_Save;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Managers.Manager_Music
{
    [RequireComponent(typeof(AudioSource))]
    public class ManagerMusic : MonoBehaviour
    {
        #region DI REF
        private ManagerSave _managerSave;
        #endregion

        #region DIRECT REF
        [SerializeField] private AudioSource _audioSource;
        #endregion


        [Inject]
        public void Construct(ManagerSave managerSave)
        {
            _managerSave = managerSave;
        }

        public void PlayMusic(AudioClip audioClip, bool isLooping)
        {
            _audioSource.clip = audioClip;
            _audioSource.volume = (float)_managerSave.SaveSystem.SaveState.MusicLevel;
            _audioSource.loop = isLooping;
            _audioSource.Play();
        }
    }
}