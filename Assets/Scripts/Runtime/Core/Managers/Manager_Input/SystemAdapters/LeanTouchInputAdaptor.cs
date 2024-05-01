using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.Runtime.SystemAdapters
{
    public class LeanTouchInputAdaptor : MonoBehaviour, IInputAdapter
    {
        // TO BE UPDATED

        public Vector2 GetCurrentMobileInput()
        {
            return Vector2.zero;
        }

        public bool IsPointerOverCorrectUiElements()
        {
            return true;
        }

        public bool GetOnDownValue()
        {
            return true;
        }


        public Transform GetTransform()
        {
            return transform;
        }

        public void ExcludeMoreThanOneTouch()
        {
            throw new System.NotImplementedException();
        }

        public float GetTapDuration()
        {
            return 0;
        }

        public bool IsInputExist()
        {
            return true;
        }


    }
}