using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Assets.Scripts.Runtime.Systems.CinemachineSystem
{
    public class ControllerCinemachineCameraShake : MonoBehaviour
    {
        private float shakeTimer;
        private float shakeTimerTotal;
        private float startingIntensity;

        public IEnumerator ShakeCamera(CinemachineVirtualCamera targetVirtualCamera, float intensity, float time)
        {
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                targetVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;

            startingIntensity = intensity;
            shakeTimerTotal = time;
            shakeTimer = time;

            while (shakeTimer > 0)
            {
                shakeTimer -= Time.deltaTime;
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain =
                    Mathf.Lerp(startingIntensity, 0f, 1 - shakeTimer / shakeTimerTotal);

                yield return null;
            }
        }

    }

}
