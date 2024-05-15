using Gs2.Core.Domain;
using Gs2.Gs2Auth.Model;
#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
#endif

namespace Gs2.Unity.Util
{
    public interface IAuthenticator
    {
        public bool NeedReAuthentication { get; }

#if GS2_ENABLE_UNITASK
        public UniTask<AccessToken> AuthenticationAsync(
            Gs2Connection connection,
            string userId,
            string password
        );
#endif
        public Gs2Future<AccessToken> AuthenticationFuture(
            Gs2Connection connection,
            string userId,
            string password
        );
    }
}