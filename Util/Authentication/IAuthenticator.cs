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
        public bool NeedReAuthentication { get; protected set; }

#if GS2_ENABLE_UNITASK
        internal abstract UniTask<AccessToken> AuthenticationAsync(
            Gs2Connection connection,
            string userId,
            string password
        );
#endif
        internal abstract Gs2Future<AccessToken> AuthenticationFuture(
            Gs2Connection connection,
            string userId,
            string password
        );
    }
}