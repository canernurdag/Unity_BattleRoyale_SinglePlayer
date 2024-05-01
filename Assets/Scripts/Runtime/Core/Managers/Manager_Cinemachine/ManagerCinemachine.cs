using Assets.Scripts.Runtime.Core.Team_;
using Assets.Scripts.Runtime.Systems.CinemachineSystem;
using Cinemachine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Managers.Manager_Cinemachine
{
    /// <summary>
    /// This class handles all Cinemachine related operations via "CinemachineSystem"
    /// </summary>

    [RequireComponent(typeof(CinemachineSystem))]
    public class ManagerCinemachine : MonoBehaviour
    {

        #region DI REF
        public CinemachineSystem CinemachineSystem { get; private set; }
        public ControllerCinemachineCameraShake ControllerCinemachineCameraShake { get; private set; }
        #endregion




        public List<CameraAdjustmentData> CameraAdjustDatas = new();

        [Inject]
        public void Construct(CinemachineSystem cinemachineSystem, ControllerCinemachineCameraShake controllerCinemachineCameraShake)
        {
            CinemachineSystem = cinemachineSystem;
            ControllerCinemachineCameraShake = controllerCinemachineCameraShake;
        }



        public void AdjustCameraPositionsAndRotations(TeamScene ownerTeamScene)
        {
            if (CameraAdjustDatas.Count == 0) return;

            var selectedCameraData = CameraAdjustDatas
                .Where(x => x.TeamScene == ownerTeamScene)
                .ToList();

            if (selectedCameraData.Count == 0) return;

            selectedCameraData.ForEach(x =>
            {
                x.VirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = x.Position;
                x.VirtualCamera.transform.rotation = Quaternion.Euler(x.RotationAngle);
            });
        }
    }

}
