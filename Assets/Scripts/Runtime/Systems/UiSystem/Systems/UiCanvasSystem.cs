using Assets.Scripts.Runtime.Systems.UiSystem;
using Assets.Scripts.Runtime.Systems.UiSystem.Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Runtime.Systems.UiSystem.Systems
{
    public class UiCanvasSystem : MonoBehaviour
    {
        private List<CanvasUi> _canvasUis = new();

        private void Awake()
        {
            _canvasUis = GetComponentsInChildren<CanvasUi>(true).ToList();
        }

        public void SetActivenessOfAllCanvases(bool value)
        {
            for (int i = 0; i < _canvasUis.Count; i++)
            {
                _canvasUis[i].gameObject.SetActive(value);
            }
        }

        /// <summary>
        /// This function sets activeness of canvases.
        /// In order to improve memory-control, this function can be extended by "ManagerAddressables"
        /// </summary>
        /// <param name="canvasType"></param>
        /// <param name="activenessValue"></param>
        public void SetActivenessOfSpecificCanvas(CanvasType canvasType, bool activenessValue)
        {
            bool isCanvasExist = _canvasUis.Any(x => x.CanvasType == canvasType);
            if (!isCanvasExist) return;

            var canvasUi = _canvasUis.Find(x => x.CanvasType == canvasType);
            if (activenessValue)
            {
                canvasUi.gameObject.SetActive(true);
            }
            canvasUi.SetCanvasActiveness(activenessValue);
        }
    }
}

