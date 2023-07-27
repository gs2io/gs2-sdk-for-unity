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
using UnityEngine.Events;

namespace Gs2.Unity.Util
{
    public class ReOpenEvent : UnityEvent
    {
        
    }

    public class DetectVersionUpEvent : UnityEvent
    {
        
    }

    public class Gs2BasicReopener : IReopener
    {
        private IAuthenticator _authenticator;
        private string _userId;
        private string _password;
        private GameSession _gameSession;
        
        public ReOpenEvent onReOpen = new ReOpenEvent();

        public override void SetAuthenticator(
            IAuthenticator authenticator,
            string userId,
            string password,
            GameSession gameSession
        ) {
            this._authenticator = authenticator;
            this._userId = userId;
            this._password = password;
            this._gameSession = gameSession;
        }

#if GS2_ENABLE_UNITASK

        public override async UniTask<OpenResult> ReOpenAsync(
            Gs2WebSocketSession session, 
            Gs2RestSession restSession
        )
        {
            await session.ReOpenAsync();
            var result = await restSession.ReOpenAsync();
            
            if (this._authenticator != null && this._userId != null && this._password != null) {
                var accessToken = await this._authenticator.AuthenticationAsync();
                this._gameSession.AccessToken = accessToken;
            }
            
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

                if (this._authenticator != null && this._userId != null && this._password != null) {
                    var future = this._authenticator.AuthenticationFuture();
                    yield return future;
                    if (future.Error != null) {
                        result.OnError(future.Error);
                        yield break;
                    }
                    this._gameSession.AccessToken = future.Result;
                }

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