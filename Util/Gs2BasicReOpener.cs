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
    public class ReOpenEvent : UnityEvent
    {
        
    }
    
    public class Gs2BasicReopener : IReopener
    {
        public ReOpenEvent onReOpen = new ReOpenEvent();

        public override IEnumerator ReOpen(
            Gs2WebSocketSession session, 
            Gs2RestSession restSession, 
            UnityAction<AsyncResult<OpenResult>> callback
        )
        {
            yield return session.Open(callback);
            yield return restSession.Open(callback);
            
            onReOpen.Invoke();
        }
    }
}