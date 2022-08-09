#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Gs2.Gs2Inbox.Model;
using Gs2.Gs2Inbox.Request;
using Gs2.Unity.Core;
using Gs2.Unity.Gs2Inbox.Model;
using Gs2.Unity.Util;
using Namespace = Gs2.Unity.Gs2Inbox.ScriptableObject.Namespace;

namespace Gs2.Unity.Express.Gs2Inbox
{
    public static class NamespaceExt
    {
        public static async UniTask<EzMessage[]> ListMessages(
            this Namespace @namespace,
            Gs2Domain gs2 = null
        )
        {
            return (await (gs2 ?? Gs2ClientHolder.Instance.Gs2).Inbox.Namespace(
                @namespace.namespaceName
            ).Me(
                Gs2GameSessionHolder.Instance.GameSession
            ).MessagesAsync(
            ).ToListAsync()).ToArray();
        }

        public static async UniTask<EzMessage> SendMessage(
            this Namespace @namespace,
            string userId,
            string metadata,
            AcquireAction[] acquireActions,
            Gs2Domain gs2 = null
        )
        {
            return EzMessage.FromModel(
                await (
                    await (gs2 ?? Gs2ClientHolder.Instance.Gs2).Super.Inbox.Namespace(
                        @namespace.namespaceName
                    ).User(
                        userId
                    ).SendMessageAsync(
                        new SendMessageByUserIdRequest()
                            .WithMetadata(metadata)
                            .WithReadAcquireActions(
                                acquireActions
                            )
                    )
                ).Model()
            );
        }
    }
}
#endif