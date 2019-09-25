using System.Collections;
using Gs2.Core;
using Gs2.Core.Net;
using Gs2.Core.Result;
using Gs2.Gs2Account;
using Gs2.Gs2Account.Request;
using Gs2.Gs2Auth;
using Gs2.Gs2Auth.Model;
using Gs2.Gs2Auth.Request;
using UnityEngine.Events;

namespace Gs2.Unity.Util
{
    public class Gs2BasicReopener : IReopener
    {
        public IEnumerator ReOpen(Gs2WebSocketSession session, UnityAction<AsyncResult<OpenResult>> callback)
        {
            yield return session.Open(callback);
        }
    }
}