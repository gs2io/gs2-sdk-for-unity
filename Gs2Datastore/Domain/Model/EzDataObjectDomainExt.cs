using System;
using System.Collections;
using System.Collections.Generic;
#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
#endif
using Gs2.Core;
using Gs2.Core.Domain;
using Gs2.Core.Net;
using Gs2.Gs2Datastore.Request;
using Gs2.Unity.Gs2Datastore.Model;
using Gs2.Unity.Gs2Datastore.Result;
using UnityEngine.Events;

namespace Gs2.Unity.Gs2Datastore.Domain.Model
{

    public partial class EzDataObjectDomain {

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

#if GS2_ENABLE_UNITASK
        public async UniTask<byte[]> DownloadAsync()
        {
            return await this._domain.DownloadAsync();
        }
#endif
        [Obsolete("The name has been changed to DownloadFuture.")]
        public IFuture<byte[]> Download()
        {
            return DownloadFuture();
        }

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

#if GS2_ENABLE_UNITASK
        public async UniTask<EzDataObject> ReUploadAsync(
            byte[] data
        )
        {
            return EzDataObject.FromModel(
                await this._domain.ReUploadAsync(data)
            );
        }
#endif
        [Obsolete("The name has been changed to ReUploadFuture.")]
        public IFuture<EzDataObject> ReUpload(
            byte[] data
        )
        {
            return ReUploadFuture(data);
        }
    }
}