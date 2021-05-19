using System.Collections;
using Gs2.Core;
using Gs2.Core.Net;
using Gs2.Core.Result;
using UnityEngine.Events;

namespace Gs2.Unity.Util
{
    public abstract class IReopener
    {
        public UnityAction<AsyncResult<OpenResult>> Callback { get; set; }

        public abstract IEnumerator ReOpen(Gs2WebSocketSession session, Gs2RestSession restSession, UnityAction<AsyncResult<OpenResult>> callback);
    }
}