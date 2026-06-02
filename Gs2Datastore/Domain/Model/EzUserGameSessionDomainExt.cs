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

    public partial class EzUserGameSessionDomain {

#if UNITY_2017_1_OR_NEWER
        public Gs2Future<EzDataObject> UploadFuture(
            string scope,
            List<string> allowUserIds,
            byte[] data,
            string name=null,
            bool? updateIfExists=null
        )
        {
            IEnumerator Impl(Gs2Future<EzDataObject> self) {
                var future = _connection.RunFuture(
                    this._gameSession,
                    () =>
                    {
                        return _domain.UploadFuture(
                            scope,
                            allowUserIds,
                            data,
                            name,
                            updateIfExists
                        );
                    }
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
        public async UniTask<EzDataObject> UploadAsync(
            string scope,
            List<string> allowUserIds,
            byte[] data,
            string name=null,
            bool? updateIfExists=null
        )
    #else
        public async Task<EzDataObject> UploadAsync(
            string scope,
            List<string> allowUserIds,
            byte[] data,
            string name=null,
            bool? updateIfExists=null
        )
    #endif
        {
            var result = await _connection.RunAsync(
                this._gameSession,
                async () =>
                {
                    return await _domain.UploadAsync(
                        scope,
                        allowUserIds,
                        data,
                        name,
                        updateIfExists
                    );
                }
            );
            return EzDataObject.FromModel(result);
        }
#endif
#if UNITY_2017_1_OR_NEWER
        [Obsolete("The name has been changed to UploadFuture.")]
        public IFuture<EzDataObject> Upload(
            string scope,
            List<string> allowUserIds,
            byte[] data,
            string name=null,
            bool? updateIfExists=null
        )
        {
            return UploadFuture(
                scope,
                allowUserIds,
                data,
                name,
                updateIfExists
            );
        }

        public Gs2Future<byte[]> DownloadFuture(
            string dataObjectId
        )
        {
            IEnumerator Impl(Gs2Future<byte[]> self)
            {
                var future = this._domain.DownloadFuture(
                    dataObjectId
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
        public async UniTask<byte[]> DownloadAsync(
            string dataObjectId
        )
    #else
        public async Task<byte[]> DownloadAsync(
            string dataObjectId
        )
    #endif
        {
            return await this._domain.DownloadAsync(dataObjectId);
        }
#endif
#if UNITY_2017_1_OR_NEWER
        [Obsolete("The name has been changed to DownloadFuture.")]
        public IFuture<byte[]> Download(
            string dataObjectId
        )
        {
            return DownloadFuture(
                dataObjectId
            );
        }
#endif
    }
}