using System;
using System.Collections;
using Gs2.Core;
using Gs2.Core.Domain;
using Gs2.Core.Exception;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Core.Result;
using UnityEngine.Events;
#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
#endif

namespace Gs2.Unity.Util
{
    public class Gs2Connection
    {
        public Gs2RestSession RestSession { get; internal set; }
        public Gs2WebSocketSession WebSocketSession { get; internal set; }

        public Region Region => RestSession.Region;

        public Gs2Connection(
            IGs2Credential credential,
            Region region
        ) {
            RestSession = new Gs2RestSession(credential, region);
            WebSocketSession = new Gs2WebSocketSession(credential, region);
        }

        public Gs2Future<OpenResult> ConnectFuture() {
            IEnumerator Impl(Gs2Future<OpenResult> self)
            {
                {
                    var future = this.RestSession.ReOpenFuture();
                    yield return future;
                    if (future.Error != null) {
                        self.OnError(future.Error);
                        yield break;
                    }
                }
                {
                    var future = this.WebSocketSession.ReOpenFuture();
                    yield return future;
                    if (future.Error != null) {
                        self.OnError(future.Error);
                        yield break;
                    }
                    self.OnComplete(
                        future.Result
                    );
                }
            }

            return new Gs2InlineFuture<OpenResult>(Impl);
        }
        
#if GS2_ENABLE_UNITASK
        public async UniTask<OpenResult> ConnectAsync() {
            await this.RestSession.ReOpenAsync();
            await this.WebSocketSession.ReOpenAsync();
            return new OpenResult();
        }
#endif

        public Gs2Future DisconnectFuture() {
            IEnumerator Impl(Gs2Future self)
            {
                {
                    var future = this.RestSession.CloseFuture();
                    yield return future;
                    if (future.Error != null) {
                        self.OnError(future.Error);
                        yield break;
                    }
                }
                {
                    var future = this.WebSocketSession.CloseFuture();
                    yield return future;
                    if (future.Error != null) {
                        self.OnError(future.Error);
                        yield break;
                    }
                    self.OnComplete(
                        future.Result
                    );
                }
            }

            return new Gs2InlineFuture(Impl);
        }
        
#if GS2_ENABLE_UNITASK
        public async UniTask DisconnectAsync() {
            await this.RestSession.CloseAsync();
            await this.WebSocketSession.CloseAsync();
        }
#endif
        
        public delegate IFuture<T> RetryAction<T>();
        
        public Gs2Future<T> RunFuture<T>(
            IGameSession gameSession,
            Func<IFuture<T>> requestAction
        )
        {
            IEnumerator Impl(Gs2Future<T> self)
            {
                var isReopenTried = false;
                var isAuthenticationTried = false;

                RETRY:
                
                if (gameSession != null && !isAuthenticationTried) {
                    var future = gameSession.RefreshIfNeedRefreshFuture();
                    yield return future;
                    if (future.Error != null) {
                        self.OnError(future.Error);
                        yield break;
                    }
                    if (future.Result) {
                        isAuthenticationTried = true;
                        goto RETRY;
                    }
                }

                var requestFuture = requestAction();
                yield return requestFuture;
                
                if (requestFuture.Error is SessionNotOpenException && !isReopenTried)
                {
                    isAuthenticationTried = false;
                    isReopenTried = true;
                    var future = ConnectFuture();
                    yield return future;
                    if (future.Error != null) {
                        self.OnError(future.Error);
                        yield break;
                    }
                    goto RETRY;
                }

                if (gameSession != null && requestFuture.Error is UnauthorizedException && !isAuthenticationTried)
                {
                    isAuthenticationTried = true;

                    var future = gameSession.RefreshFuture();
                    yield return future;
                    if (future.Error != null) {
                        self.OnError(future.Error);
                        yield break;
                    }
                    goto RETRY;
                }

                if (requestFuture.Error != null) {
                    self.OnError(requestFuture.Error);
                    yield break;
                }

                self.OnComplete(requestFuture.Result);
            }

            return new Gs2InlineFuture<T>(Impl);
        }

        public delegate IEnumerator RequestAction<T>(UnityAction<AsyncResult<T>> callback);
        
        public IEnumerator Run<T>(
            UnityAction<AsyncResult<T>> callback,
            IGameSession gameSession,
            RequestAction<T> requestAction)
        {
            bool isReopenTried = false;
            bool isAuthenticationTried = false;

            AsyncResult<T> asyncResult = null;

            RETRY:

            if (gameSession != null && !isAuthenticationTried) {
                var future = gameSession.RefreshIfNeedRefreshFuture();
                yield return future;
                if (future.Error != null) {
                    callback.Invoke(new AsyncResult<T>(
                        default,
                        future.Error
                    ));
                    yield break;
                }
                if (future.Result) {
                    isAuthenticationTried = true;
                    goto RETRY;
                }
            }
            
            yield return requestAction.Invoke(ar => asyncResult = ar);

            if (asyncResult.Error is SessionNotOpenException && !isReopenTried)
            {
                isAuthenticationTried = false;
                isReopenTried = true;
                var future = ConnectFuture();
                yield return future;
                if (future.Error != null)
                {
                    callback.Invoke(new AsyncResult<T>(
                        default,
                        future.Error
                    ));
                    yield break;
                }
                goto RETRY;
            }

            if (gameSession != null && asyncResult.Error is UnauthorizedException && !isAuthenticationTried)
            {
                isAuthenticationTried = true;

                var future = gameSession.RefreshFuture();
                yield return future;
                if (future.Error != null) {
                    callback.Invoke(new AsyncResult<T>(
                        default,
                        future.Error
                    ));
                    yield break;
                }
                goto RETRY;
            }
            
            callback.Invoke(asyncResult);
        }

#if GS2_ENABLE_UNITASK

        public async UniTask<T> RunAsync<T>(
            IGameSession gameSession,
            Func<UniTask<T>> requestActionAsync
        )
        {
            var isReopenTried = false;
            var isAuthenticationTried = false;

            RETRY:

            try
            {
                if (gameSession != null && !isAuthenticationTried) {
                    if (await gameSession.RefreshIfNeedRefreshAsync()) {
                        isAuthenticationTried = true;
                        goto RETRY;
                    }
                }
                return await requestActionAsync.Invoke();
            }
            catch (UnauthorizedException) {
                if (gameSession != null && !isAuthenticationTried)
                {
                    isAuthenticationTried = true;
                    await gameSession.RefreshAsync();
                    goto RETRY;
                }
                throw;
            }
            catch (SessionNotOpenException)
            {
                if (!isReopenTried) {
                    isAuthenticationTried = false;
                    isReopenTried = true;
                    await ConnectAsync();
                    goto RETRY;
                }
                throw;
            }
        }

#endif
        
        public delegate Gs2Iterator<T> RetryIterator<T>();

        public IEnumerator RunIterator<T>(
            IGameSession gameSession,
            Gs2Iterator<T> requestIterator,
            RetryIterator<T> retryIterator)
        {
            var isReopenTried = false;
            var isAuthenticationTried = false;

            RETRY:
            
            if (gameSession != null && !isAuthenticationTried) {
                var future = gameSession.RefreshIfNeedRefreshFuture();
                yield return future;
                if (future.Error != null)
                {
                    yield break;
                }
                if (future.Result)
                {
                    isAuthenticationTried = true;
                    goto RETRY;
                }
            }

            yield return requestIterator.Next();

            if (requestIterator.Error is SessionNotOpenException && !isReopenTried)
            {
                isAuthenticationTried = false;
                isReopenTried = true;
                var future = ConnectFuture();
                yield return future;
                if (future.Error != null)
                {
                    yield break;
                }
                requestIterator = retryIterator?.Invoke();
                goto RETRY;
            }

            if (gameSession != null && requestIterator.Error is UnauthorizedException && !isAuthenticationTried)
            {
                isAuthenticationTried = true;

                var future = gameSession.RefreshFuture();
                yield return future;
                if (future.Error != null)
                {
                    yield break;
                }
                requestIterator = retryIterator?.Invoke();
                goto RETRY;
            }
        }

#if GS2_ENABLE_UNITASK

        public delegate void RetryAction();
        
        public async UniTask<bool> RunIteratorAsync(
            IGameSession gameSession,
            Func<UniTask<bool>> requestActionAsync,
            RetryAction retryAction)
        {
            var isReopenTried = false;
            var isAuthenticationTried = false;

            RETRY:

            try
            {
                if (gameSession != null && !isAuthenticationTried) {
                    if (await gameSession.RefreshIfNeedRefreshAsync()) {
                        isAuthenticationTried = true;
                        goto RETRY;
                    }
                }
                return await requestActionAsync.Invoke();
            }
            catch (UnauthorizedException) {
                if (gameSession != null && !isAuthenticationTried)
                {
                    isAuthenticationTried = true;
                    await gameSession.RefreshAsync();
                    retryAction?.Invoke();
                    goto RETRY;
                }
                throw;
            }
            catch (SessionNotOpenException) {
                if (!isReopenTried)
                {
                    isAuthenticationTried = false;
                    isReopenTried = true;
                    await ConnectAsync();
                    retryAction?.Invoke();
                    goto RETRY;
                }
                throw;
            }
        }
#endif
        public void UpdateProjectToken(string projectToken) {
            RestSession.Credential.ProjectToken = projectToken;
            WebSocketSession.Credential.ProjectToken = projectToken;
        }
        
        public bool IsDisconnected() {
            if (RestSession.IsDisconnected()) {
                return true;
            } 
            if (WebSocketSession.IsDisconnected()) {
                return true;
            }
            return false;
        }
    }
}
