#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Gs2.Unity.Gs2Inbox.Model;
using Gs2.Unity.Gs2Inbox.ScriptableObject;
using Gs2.Unity.Util;

namespace Gs2.Unity.Express.Gs2Inbox
{
    public static class NamespaceExt
    {
        public static async UniTask<EzMessage[]> ListMessages(
            this Namespace @namespace
        )
        {
            return (await Gs2ClientHolder.Instance.Gs2.Inbox.Namespace(
                @namespace.namespaceName
            ).Me(
                Gs2GameSessionHolder.Instance.GameSession
            ).MessagesAsync(
            ).ToListAsync()).ToArray();
        }
    }
}
#endif