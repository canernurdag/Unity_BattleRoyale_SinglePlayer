using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Assets.Scripts.Runtime.Systems.UiSystem;
using Assets.Scripts.Runtime.Systems.UiSystem.Interface;

namespace Assets.Scripts.Runtime.Core.Ui.ControllerCanvasOpenClose
{
    /// <summary>
    /// This class handles how the canvas should be opened and closed. Each canvas can have a unique behaviour by overriding functions.
    /// </summary>
    public class ControllerCanvasOpenCloseBase : MonoBehaviour, IOpenCloseUiCanvas
    {
        // [SerializeField] protected SoundsUiDataSO _soundsUiDataSo;
        [SerializeField] protected float _durationBetweenCanvasUiParts = 0.15f;
        public virtual void OpenUiCanvas(List<CanvasUiPart> canvasUiParts)
        {
            for (int i = 0; i < canvasUiParts.Count; i++)
            {
                CanvasUiPart canvasUiPart = canvasUiParts[i];
                int no = i;
                canvasUiPart.SetScale(Vector3.zero);

                DOVirtual.DelayedCall(no * _durationBetweenCanvasUiParts, () =>
                {
                    canvasUiPart.SetScale(Vector3.one, 0.3f);
                });

            }
        }

        public virtual void CloseUiCanvas(List<CanvasUiPart> canvasUiParts)
        {
            for (int i = 0; i < canvasUiParts.Count; i++)
            {
                CanvasUiPart canvasUiPart = canvasUiParts[i];
                int no = i;

                DOVirtual.DelayedCall(no * _durationBetweenCanvasUiParts, () =>
                {
                    canvasUiPart.SetScale(Vector3.zero, 0.3f);
                    if (no == canvasUiParts.Count - 1)
                    {
                        gameObject.SetActive(false);
                    }
                });

            }

            if (canvasUiParts.Count == 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
