using System.Collections;
#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
#endif
using Gs2.Core;
using Gs2.Core.Domain;
using Gs2.Gs2Auth.Model;
using UnityEngine.Events;

namespace Gs2.Unity.Util
{
    public abstract class IAuthenticator
    {
        public UnityAction<AsyncResult<AccessToken>> Callback { get; set; }
        public bool NeedReAuthentication { get; protected set; }

#if GS2_ENABLE_UNITASK
        public abstract UniTask<AccessToken> AuthenticationAsync();
#endif
        public abstract Gs2Future<AccessToken> AuthenticationFuture();
        public abstract IEnumerator Authentication(UnityAction<AsyncResult<AccessToken>> callback);
    }
}