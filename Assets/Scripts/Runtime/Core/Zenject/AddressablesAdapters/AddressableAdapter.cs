using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Core.Zenject.AddressablesAdapters
{
    public abstract class AddressableAdapter : MonoBehaviour
    {
        public bool IsAsyncOn { get; private set; }

        protected abstract void ReleaseInstances();
        public void SetIsAsyncOn(bool isAsyncOn)
        {
            IsAsyncOn = isAsyncOn;
        }
    }
}