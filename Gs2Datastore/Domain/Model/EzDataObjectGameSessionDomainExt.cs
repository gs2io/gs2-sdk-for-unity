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

    public partial class EzDataObjectGameSessionDomain {

#if GS2_ENABLE_UNITASK
        public async UniTask<byte[]> DownloadAsync(
#else
        public Gs2Future<byte[]> Download(
#endif
        )
        {
#if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.Download(
                    );
                }
            );
            return result;
#else

            IEnumerator Impl(Gs2Future<byte[]> self)
            {
                var future = _domain.Download(
                );
                yield return _profile.RunFuture(
                    _domain.AccessToken,
                    future,
                    () => {
                        return future = _domain.Download(
                        );
                    }
                );
                if (future.Error != null)
                {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(future.Result);
            }
            return new Gs2InlineFuture<byte[]>(Impl);
#endif
        }
        
#if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> ReUploadAsync(
#else
        public Gs2Future<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> ReUpload(
#endif
            byte[] data
        )
        {
#if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.ReUpload(
                        data
                    );
                }
            );
            return new Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain(
                result, _profile
            );
#else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> self)
            {
                var future = _domain.ReUpload(
                    data
                );
                yield return _profile.RunFuture(
                    null,
                    future,
                    () => {
                        return future = _domain.ReUpload(
                            data
                        );
                    }
                );
                if (future.Error != null)
                {
                    self.OnError(future.Error);
                    yield break;
                }
                var result = future.Result;
                self.OnComplete(new Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain(result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain>(Impl);
#endif
        }

    }
}