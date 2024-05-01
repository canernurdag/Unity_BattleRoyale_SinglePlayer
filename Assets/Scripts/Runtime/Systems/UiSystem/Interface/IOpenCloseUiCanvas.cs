using Assets.Scripts.Runtime.Systems.UiSystem;
using System.Collections.Generic;

namespace Assets.Scripts.Runtime.Systems.UiSystem.Interface
{
    public interface IOpenCloseUiCanvas
    {
        void OpenUiCanvas(List<CanvasUiPart> canvasUiParts);
        void CloseUiCanvas(List<CanvasUiPart> canvasUiParts);

    }
}

