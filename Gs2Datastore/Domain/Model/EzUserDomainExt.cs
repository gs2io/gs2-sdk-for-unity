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

    public partial class EzUserDomain {

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

#if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Datastore.Domain.Model.DataObjectDomain> UploadAsync(
            string scope,
            List<string> allowUserIds,
            byte[] data,
            string name=null,
            bool? updateIfExists=null
        )
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
    }
}