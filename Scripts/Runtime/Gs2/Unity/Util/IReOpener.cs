using System.Collections;
using Gs2.Core;
using Gs2.Core.Net;
using Gs2.Core.Result;
using UnityEngine.Events;

namespace Gs2.Unity.Util
{
    public interface IReopener
    {
        IEnumerator ReOpen(Gs2WebSocketSession session, UnityAction<AsyncResult<OpenResult>> callback);
    }
}