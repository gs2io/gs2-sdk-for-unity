using Gs2.Core.Domain;
using Gs2.Gs2Auth.Model;
#if UNITY_2017_1_OR_NEWER && GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
#elif !UNITY_2017_1_OR_NEWER
using System.Threading.Tasks;
#endif

namespace Gs2.Unity.Util
{
    public interface IAuthenticator
    {
        public bool NeedReAuthentication { get; }

#if !UNITY_2017_1_OR_NEWER || GS2_ENABLE_UNITASK
    #if UNITY_2017_1_OR_NEWER
        public UniTask<AccessToken> AuthenticationAsync(
            Gs2Connection connection,
            string userId,
            string password
        );
    #else
        public Task<AccessToken> AuthenticationAsync(
            Gs2Connection connection,
            string userId,
            string password
        );
    #endif
#endif
        public Gs2Future<AccessToken> AuthenticationFuture(
            Gs2Connection connection,
            string userId,
            string password
        );
    }
}