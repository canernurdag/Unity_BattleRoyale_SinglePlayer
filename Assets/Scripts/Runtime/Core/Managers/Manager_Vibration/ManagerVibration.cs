using Assets.Scripts.Runtime.Core.Managers.Manager_Save;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Runtime.Core.Managers.Manager_Vibration
{
    public class ManagerVibration : MonoBehaviour
    {
        #region DI REF
        private ManagerSave _managerSave;
        #endregion


        [Inject]
        public void Construct(ManagerSave managerSave)
        {
            _managerSave = managerSave;
        }

        public void Vibrate()
        {
            if ((bool)!_managerSave.SaveSystem.SaveState.IsVibrationOn) return;

            //Vibrate will be implemented with 3rd party plugin
        }
    }
}