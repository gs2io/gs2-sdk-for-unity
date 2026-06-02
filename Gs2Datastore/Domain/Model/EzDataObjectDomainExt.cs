using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_2017_1_OR_NEWER && GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
#elif !UNITY_2017_1_OR_NEWER
using System.Threading.Tasks;
#endif
using Gs2.Core;
using Gs2.Core.Domain;
using Gs2.Core.Net;
using Gs2.Gs2Datastore.Request;
using Gs2.Unity.Gs2Datastore.Model;
using Gs2.Unity.Gs2Datastore.Result;
#if UNITY_2017_1_OR_NEWER
using UnityEngine.Events;
#endif

namespace Gs2.Unity.Gs2Datastore.Domain.Model
{

    public partial class EzDataObjectDomain {

#if UNITY_2017_1_OR_NEWER
        public Gs2Future<byte[]> DownloadFuture()
        {
            IEnumerator Impl(Gs2Future<byte[]> self)
            {
                var future = this._domain.DownloadFuture(
                );
                yield return future;
                if (future.Error != null)
                {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(future.Result);
            }
            return new Gs2InlineFuture<byte[]>(Impl);
        }
#endif

#if !UNITY_2017_1_OR_NEWER || GS2_ENABLE_UNITASK
    #if UNITY_2017_1_OR_NEWER
        public async UniTask<byte[]> DownloadAsync()
    #else
        public async Task<byte[]> DownloadAsync()
    #endif
        {
            return await this._domain.DownloadAsync();
        }
#endif
#if UNITY_2017_1_OR_NEWER
        [Obsolete("The name has been changed to DownloadFuture.")]
        public IFuture<byte[]> Download()
        {
            return DownloadFuture();
        }
#endif

#if UNITY_2017_1_OR_NEWER
        public Gs2Future<byte[]> DownloadByUserIdAndDataObjectNameFuture()
        {
            IEnumerator Impl(Gs2Future<byte[]> self)
            {
                var future = this._domain.DownloadByUserIdAndDataObjectNameFuture(
                );
                yield return future;
                if (future.Error != null)
                {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(future.Result);
            }
            return new Gs2InlineFuture<byte[]>(Impl);
        }
#endif

#if !UNITY_2017_1_OR_NEWER || GS2_ENABLE_UNITASK
    #if UNITY_2017_1_OR_NEWER
        public async UniTask<byte[]> DownloadByUserIdAndDataObjectNameAsync()
    #else
        public async Task<byte[]> DownloadByUserIdAndDataObjectNameAsync()
    #endif
        {
            return await this._domain.DownloadByUserIdAndDataObjectNameAsync();
        }
#endif
        
#if UNITY_2017_1_OR_NEWER
        public Gs2Future<EzDataObject> ReUploadFuture(
            byte[] data
        )
        {
            IEnumerator Impl(Gs2Future<EzDataObject> self)
            {
                var future = this._domain.ReUploadFuture(
                    data
                );
                yield return future;
                if (future.Error != null)
                {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(EzDataObject.FromModel(future.Result));
            }
            return new Gs2InlineFuture<EzDataObject>(Impl);
        }
#endif

#if !UNITY_2017_1_OR_NEWER || GS2_ENABLE_UNITASK
    #if UNITY_2017_1_OR_NEWER
        public async UniTask<EzDataObject> ReUploadAsync(
            byte[] data
        )
    #else
        public async Task<EzDataObject> ReUploadAsync(
            byte[] data
        )
    #endif
        {
            return EzDataObject.FromModel(
                await this._domain.ReUploadAsync(data)
            );
        }
#endif
#if UNITY_2017_1_OR_NEWER
        [Obsolete("The name has been changed to ReUploadFuture.")]
        public IFuture<EzDataObject> ReUpload(
            byte[] data
        )
        {
            return ReUploadFuture(data);
        }
#endif
    }
}