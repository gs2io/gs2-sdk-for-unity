using System;
using System.Collections;
using System.Linq;
#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
#endif
using Gs2.Core;
using Gs2.Core.Domain;
using Gs2.Core.Exception;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Core.Result;
using Gs2.Gs2Account;
using Gs2.Gs2Account.Request;
using Gs2.Gs2Auth;
using Gs2.Gs2Auth.Model;
using Gs2.Gs2Auth.Request;
using Gs2.Gs2Gateway;
using Gs2.Gs2Gateway.Request;
using Gs2.Gs2Version;
using Gs2.Gs2Version.Request;
using Gs2.Unity.Gs2Version.Model;
using UnityEngine.Events;

namespace Gs2.Unity.Util
{
    public class ReOpenEvent : UnityEvent
    {
        
    }

    public class DetectVersionUpEvent : UnityEvent<EzStatus[]>
    {
        
    }

    [Obsolete("")]
    public class Gs2BasicReopener : IReopener
    {
        public ReOpenEvent onReOpen = new ReOpenEvent();

#if GS2_ENABLE_UNITASK

        public override async UniTask<OpenResult> ReOpenAsync(
            Gs2WebSocketSession session, 
            Gs2RestSession restSession
        )
        {
            await session.ReOpenAsync();
            var result = await restSession.ReOpenAsync();
            
            onReOpen.Invoke();

            return result;
        }

#endif
        
        public override Gs2Future<OpenResult> ReOpenFuture(
            Gs2WebSocketSession session, 
            Gs2RestSession restSession
        )
        {
            IEnumerator Impl(Gs2Future<OpenResult> result) {
                yield return session.ReOpenFuture();
                yield return restSession.ReOpenFuture();

                onReOpen.Invoke();
            }

            return new Gs2InlineFuture<OpenResult>(Impl);
        }
        
        public override IEnumerator ReOpen(
            Gs2WebSocketSession session, 
            Gs2RestSession restSession, 
            UnityAction<AsyncResult<OpenResult>> callback
        ) {
            var future = ReOpenFuture(session, restSession);
            yield return future;
            callback.Invoke(new AsyncResult<OpenResult>(
                future.Result,
                future.Error
            ));
        }
    }
}