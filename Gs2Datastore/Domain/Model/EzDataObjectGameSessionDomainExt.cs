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

    public partial class EzDataObjectGameSessionDomain {

#if UNITY_2017_1_OR_NEWER
        public Gs2Future<byte[]> DownloadFuture(
        )
        {
            IEnumerator Impl(Gs2Future<byte[]> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => {
                        return _domain.DownloadFuture(
                        );
                    }
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
        )
    #else
        public async Task<byte[]> DownloadAsync(
        )
    #endif
        {
            var result = await this._connection.RunAsync(
                this._gameSession,
                async () =>
                {
                    return await _domain.DownloadAsync();
                }
            );
            return result;
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
        public Gs2Future<byte[]> DownloadOwnFuture(
        )
        {
            IEnumerator Impl(Gs2Future<byte[]> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => {
                        return _domain.DownloadOwnFuture(
                        );
                    }
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
        public async UniTask<byte[]> DownloadOwnAsync(
        )
    #else
        public async Task<byte[]> DownloadOwnAsync(
        )
    #endif
        {
            var result = await this._connection.RunAsync(
                this._gameSession,
                async () =>
                {
                    return await _domain.DownloadOwnAsync();
                }
            );
            return result;
        }
#endif
#if UNITY_2017_1_OR_NEWER
        [Obsolete("The name has been changed to DownloadOwn.")]
        public IFuture<byte[]> DownloadOwn()
        {
            return DownloadOwnFuture();
        }
#endif

#if UNITY_2017_1_OR_NEWER
        public Gs2Future<EzDataObjectGameSessionDomain> ReUploadFuture(
            byte[] data
        )
        {
            IEnumerator Impl(Gs2Future<EzDataObjectGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => {
                        return _domain.ReUploadFuture(
                            data
                        );
                    }
                );
                yield return future;
                if (future.Error != null)
                {
                    self.OnError(future.Error);
                    yield break;
                }
                var result = future.Result;
                self.OnComplete(new EzDataObjectGameSessionDomain(
                    result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<EzDataObjectGameSessionDomain>(Impl);
        }
#endif

#if !UNITY_2017_1_OR_NEWER || GS2_ENABLE_UNITASK
    #if UNITY_2017_1_OR_NEWER
        public async UniTask<EzDataObjectGameSessionDomain> ReUploadAsync(
            byte[] data
        )
    #else
        public async Task<EzDataObjectGameSessionDomain> ReUploadAsync(
            byte[] data
        )
    #endif
        {
            var result = await this._connection.RunAsync(
                this._gameSession,
                async () =>
                {
                    return await _domain.ReUploadAsync(
                        data
                    );
                }
            );
            return new EzDataObjectGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
#endif
#if UNITY_2017_1_OR_NEWER
        [Obsolete("The name has been changed to ReUploadFuture.")]
        public IFuture<EzDataObjectGameSessionDomain> ReUpload(
            byte[] data
        )
        {
            return ReUploadFuture(data);
        }
#endif
    }
}