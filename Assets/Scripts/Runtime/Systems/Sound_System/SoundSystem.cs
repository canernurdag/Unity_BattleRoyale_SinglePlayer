using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Systems.Sound_System
{
    public class SoundSystem : MonoBehaviour
    {
        #region REF
        public bool PreventSound { get; private set; } = false;
        #endregion

        /// <summary>
        /// Play sound for gameobject already has AudioSource component
        /// </summary>
        /// <param name="targetAudioSource"></param>
        /// <param name="clipToBePlayed"></param>
        /// <param name="volume"></param>
        public void PlaySound(AudioSource targetAudioSource, AudioClip clipToBePlayed, float volume)
        {
            if (PreventSound) return;
            targetAudioSource.PlayOneShot(clipToBePlayed, volume);
        }

        /// <summary>
        /// Play sound for gameobject does not have AudioSource component
        /// </summary>
        /// <param name="position"></param>
        /// <param name="clipToBePlayed"></param>
        /// <param name="volume"></param>
        public void PlaySoundAtPoint(Vector3 position, AudioClip clipToBePlayed, float volume)
        {
            if (PreventSound) return;
            AudioSource.PlayClipAtPoint(clipToBePlayed, position, volume);
        }

        public void SetPreventSound(bool value)
        {
            PreventSound = value;
        }
    }
}

