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

    public partial class EzUserDomain {

#if UNITY_2017_1_OR_NEWER
        public Gs2Future<Gs2.Gs2Datastore.Domain.Model.DataObjectDomain> UploadFuture(
            string scope,
            List<string> allowUserIds,
            byte[] data,
            string name = null,
            bool? updateIfExists = null
        ) {
            IEnumerator Impl(Gs2Future<Gs2.Gs2Datastore.Domain.Model.DataObjectDomain> self) {
                var future = _connection.RunFuture(
                    null,
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
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(future.Result);
            }

            return new Gs2InlineFuture<Gs2.Gs2Datastore.Domain.Model.DataObjectDomain>(Impl);
        }
#endif

#if !UNITY_2017_1_OR_NEWER || GS2_ENABLE_UNITASK
    #if UNITY_2017_1_OR_NEWER
        public async UniTask<Gs2.Gs2Datastore.Domain.Model.DataObjectDomain> UploadAsync(
            string scope,
            List<string> allowUserIds,
            byte[] data,
            string name=null,
            bool? updateIfExists=null
        )
    #else
        public async Task<Gs2.Gs2Datastore.Domain.Model.DataObjectDomain> UploadAsync(
            string scope,
            List<string> allowUserIds,
            byte[] data,
            string name=null,
            bool? updateIfExists=null
        )
    #endif
        {
            var result = await this._connection.RunAsync(
                null,
                async () =>
                {
                    return await this._domain.UploadAsync(
                        scope,
                        allowUserIds,
                        data,
                        name,
                        updateIfExists
                    );
                }
            );
            return result;
        }
#endif
#if UNITY_2017_1_OR_NEWER
        [Obsolete("The name has been changed to UploadFuture.")]
        public IFuture<Gs2.Gs2Datastore.Domain.Model.DataObjectDomain> Upload(
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
#endif
    }
}