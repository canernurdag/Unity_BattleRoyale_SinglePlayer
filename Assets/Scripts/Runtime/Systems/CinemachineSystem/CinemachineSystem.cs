using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Systems.CinemachineSystem
{
    [RequireComponent(typeof(ControllerCinemachineCameraShake))]
    public class CinemachineSystem : MonoBehaviour
    {

        #region DIRECT REF
        [SerializeField] private Animator _cinemachineAnimator;
        #endregion

        #region DI REF
        public CinemachineStateDrivenCamera CinemachineStateDrivenCamera { get; private set; }
        public ControllerCinemachineCameraShake ControllerCinemachineCameraShake { get; private set; }
        #endregion


        [Inject]
        public void Construct(CinemachineStateDrivenCamera cinemachineStateDrivenCamera,
            ControllerCinemachineCameraShake controllerCinemachineCameraShake)
        {
            CinemachineStateDrivenCamera = cinemachineStateDrivenCamera;
            ControllerCinemachineCameraShake = controllerCinemachineCameraShake;
        }



        /// <summary>
        /// In order to change the active virtual camera
        /// </summary>
        /// <param name="targetAnimationParameter"></param>
        public void SetActiveVcamByUsingAnimator(string targetAnimationParameter)
        {
            _cinemachineAnimator.SetTrigger(targetAnimationParameter);
        }

        public CinemachineVirtualCamera GetActiveVCAM()
        {
            return (CinemachineVirtualCamera)CinemachineStateDrivenCamera.LiveChild;
        }
    }

}
