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

    public partial class EzUserDomain {

        public async UniTask<EzDataObjectDomain> Upload(
            string scope,
            List<string> allowUserIds,
            byte[] data,
            string name=null,
            bool? updateIfExists=null
        )
        {
            return new EzDataObjectDomain(
                await _domain.Upload(
                    scope,
                    allowUserIds,
                    data,
                    name,
                    updateIfExists
                )
            );
        }
        
    }
}