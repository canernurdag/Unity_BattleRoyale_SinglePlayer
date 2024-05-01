using Assets.Scripts.Runtime.Core.Managers.Manager_Save;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Controllers
{
    public class ControllerMusic : MonoBehaviour
    {
        #region DIRECT REF
        [SerializeField] protected AudioSource _audioSource;
        [SerializeField] protected AudioClip _sceneMusic;
        #endregion

        #region DI REF
        protected ManagerSave _managerSave;
        #endregion

        [Inject]
        public void Construct(ManagerSave managerSave)
        {
            _managerSave = managerSave;
        }

        protected IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            PlayMusic(_sceneMusic);
        }

        public void PlayMusic(AudioClip musicAudioClip)
        {
            _audioSource.volume = (float)_managerSave.SaveSystem.SaveState.MusicLevel;

            _audioSource.clip = musicAudioClip;
            _audioSource.Play();
        }

        public void StopMusic()
        {
            _audioSource.Stop();
        }

        public void AdjustAudioSourceVolume(SignalUserMusicLevelChanged signalUserMusicLevelChanged)
        {
            _audioSource.volume = signalUserMusicLevelChanged.MusicLevel;
        }
    }
}