using System.Collections;
using System.Collections.Generic;
#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
#endif
using Gs2.Core;
using Gs2.Core.Domain;
using Gs2.Gs2Datastore.Request;
using Gs2.Unity.Gs2Datastore.Model;
using Gs2.Unity.Gs2Datastore.Result;
using UnityEngine.Events;

namespace Gs2.Unity.Gs2Datastore.Domain.Model
{

    public partial class EzUserDomain {

#if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Gs2Datastore.Domain.Model.DataObjectDomain> UploadAsync(
#else
        public Gs2Future<Gs2.Gs2Datastore.Domain.Model.DataObjectDomain> Upload(
#endif
            string scope,
            List<string> allowUserIds,
            byte[] data,
            string name=null,
            bool? updateIfExists=null
        )
        {
#if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                null,
                async () =>
                {
                    return await _domain.Upload(
                        scope,
                        allowUserIds,
                        data,
                        name,
                        updateIfExists
                    );
                }
            );
            return result;
#else

            IEnumerator Impl(Gs2Future<Gs2.Gs2Datastore.Domain.Model.DataObjectDomain> self)
            {
                var future = _domain.Upload(
                    scope,
                    allowUserIds,
                    data,
                    name,
                    updateIfExists
                );
                yield return _profile.RunFuture(
                    null,
                    future,
                    () => {
                        return future = _domain.Upload(
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
                self.OnComplete(future.Result);
            }
            return new Gs2InlineFuture<Gs2.Gs2Datastore.Domain.Model.DataObjectDomain>(Impl);
#endif
        }
    }
}