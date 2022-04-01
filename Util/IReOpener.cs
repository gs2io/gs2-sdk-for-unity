#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
#endif
using Gs2.Core.Net;
using Gs2.Core.Result;
using Gs2.Core;
using System.Collections;
using UnityEngine.Events;

namespace Gs2.Unity.Util
{
    public abstract class IReopener
    {
#if GS2_ENABLE_UNITASK
        public abstract UniTask<OpenResult> ReOpenAsync(Gs2WebSocketSession session, Gs2RestSession restSession);
#endif
        public UnityAction<AsyncResult<OpenResult>> Callback { get; set; }

        public abstract IEnumerator ReOpen(Gs2WebSocketSession session, Gs2RestSession restSession, UnityAction<AsyncResult<OpenResult>> callback);
    }
}