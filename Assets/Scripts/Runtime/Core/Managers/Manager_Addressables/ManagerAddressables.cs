using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System;
using Cysharp.Threading.Tasks;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement;

namespace Assets.Scripts.Runtime.Core.Managers.Manager_Addressables
{
    public class ManagerAddressables : MonoBehaviour
    {
        #region OBJECT FUNCTIONS

        public async UniTask<GameObject> GetAndInstantiateGameObjectAsync(AssetReference assetReference)
        {
            var asyncOperationHandle = assetReference.InstantiateAsync().WithCancellation(this.GetCancellationTokenOnDestroy());
            var result = await asyncOperationHandle;

            return result;
        }

        public async UniTask<AudioClip> GetAudioClipAsync(AssetReferenceAudioClip assetReferenceAudioClip)
        {
            var asyncOperationHandle = assetReferenceAudioClip.LoadAssetAsync<AudioClip>().WithCancellation(this.GetCancellationTokenOnDestroy());
            var result = await asyncOperationHandle;

            return result;
        }

        public void ReleaseAndDestroy(GameObject addressedGameObject)
        {
            if (!Addressables.ReleaseInstance(addressedGameObject))
                Destroy(addressedGameObject);
        }

        #endregion
    }

    [Serializable]
    public class AssetReferenceAudioClip : AssetReferenceT<AudioClip>
    {
        public AssetReferenceAudioClip(string guid) : base(guid)
        {
        }
    }
}