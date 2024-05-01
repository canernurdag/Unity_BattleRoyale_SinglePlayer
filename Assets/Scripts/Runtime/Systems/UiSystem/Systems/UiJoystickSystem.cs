using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Systems.UiSystem.Systems
{
    [Serializable]
    public class UiJoystickSystem
    {
        public Vector2 GetAnchoredPositionFromScreenPoint(Vector2 screenPosition, RectTransform panelRectTransform, RectTransform joystickImageBackground)
        {
            Vector2 localPoint = Vector2.zero;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRectTransform, screenPosition,
                    null, out localPoint))
            {
                Vector2 pivotOffset = panelRectTransform.pivot * panelRectTransform.sizeDelta;
                return localPoint - joystickImageBackground.anchorMax * panelRectTransform.sizeDelta + pivotOffset;
            }
            return Vector2.zero;
        }
    }

}
