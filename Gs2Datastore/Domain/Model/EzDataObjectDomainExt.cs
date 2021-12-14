using System.Collections;
using System.Collections.Generic;
#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
#endif
using Gs2.Core;
using Gs2.Gs2Datastore.Request;
using Gs2.Unity.Gs2Datastore.Model;
using Gs2.Unity.Gs2Datastore.Result;
using UnityEngine.Events;

namespace Gs2.Unity.Gs2Datastore.Domain.Model
{

    public partial class EzDataObjectDomain {

        public async UniTask<byte[]> Download(
        )
        {
            return await _domain.Download();
        }

        public async UniTask<EzDataObjectDomain> ReUpload(
            byte[] data
        )
        {
            return new EzDataObjectDomain(
                await _domain.ReUpload(data)
            );
        }

    }
}