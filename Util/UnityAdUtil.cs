#if GS2_ENABLE_UNITY_ADS

using System;
using System.Collections;
using Gs2.Core;
using Gs2.Core.Domain;
using Gs2.Core.Exception;
using Gs2.Core.Model;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;

#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
#endif

namespace Gs2.Unity.Util
{
    public class UnityAdUtil
    {
        public static IEnumerator Initialize(
            UnityAction<AsyncResult<object>> callback,
            string gameId
        ) {
            var future = InitializeFuture(
                gameId
            );
            yield return future;
            callback.Invoke(new AsyncResult<object>(
                future.Result,
                future.Error
            ));
        }
        
        public static Gs2Future<object> InitializeFuture(
            string gameId
        )
        {
            IEnumerator Impl(Gs2Future<object> result) {
                bool exit = false;
                var listener = new UnityAdsInitializeListener {
                    Error = reason =>
                    {
                        result.OnError(new BadGatewayException(
                            new[] {
                                new RequestError("ads", reason),
                            }
                        ));
                        exit = true;
                    },
                    Finish = () =>
                    {
                        exit = true;
                    },
                };
#if GS2_ENABLE_UNITY_ADS_TEST_MODE
                Advertisement.Initialize(gameId, true, listener);
#else
                Advertisement.Initialize(gameId, false, listener);
#endif 
                yield return new WaitUntil(() => exit);
            }

            return new Gs2InlineFuture<object>(Impl);
        }
        
#if GS2_ENABLE_UNITASK
        
        public static async UniTask InitializeAsync(
            string gameId
        ) {
            var future = InitializeFuture(
                gameId
            );
            await future;
            if (future.Error != null) {
                throw future.Error;
            }
        }
#endif
        
        public static IEnumerator View(
            UnityAction<AsyncResult<object>> callback,
            string placement,
            GameSession session
        ) {
            var future = ViewFuture(
                placement, 
                session
            );
            yield return future;
            callback.Invoke(new AsyncResult<object>(
                future.Result,
                future.Error
            ));
        }
        
        public static Gs2Future<object> ViewFuture(
            string placement,
            GameSession session
        )
        {
            IEnumerator Impl(Gs2Future<object> result) {
                {
                    bool error = false;
                    bool exit = false;
                    var listener = new UnityAdsLoadListener {
                        Error = reason =>
                        {
                            result.OnError(new BadGatewayException(
                                new[] {
                                    new RequestError("ads", reason),
                                }
                            ));
                            error = true;
                            exit = true;
                        },
                        Finish = () =>
                        {
                            result.OnComplete(null);
                            exit = true;
                        },
                    };
                    Advertisement.Load(placement, listener);
                    yield return new WaitUntil(() => exit);

                    if (error) {
                        yield break;
                    }
                }
                {
                    var options = new ShowOptions();
                    options.gamerSid = session.AccessToken.UserId;
                    bool error = false;
                    bool exit = false;
                    var listener = new UnityAdsShowListener {
                        Error = reason =>
                        {
                            result.OnError(new BadGatewayException(
                                new[] {
                                    new RequestError("ads", reason),
                                }
                            ));
                            error = true;
                            exit = true;
                        },
                        Finish = () =>
                        {
                            result.OnComplete(null);
                            exit = true;
                        },
                    };
                    Advertisement.Show(placement, options, listener);
                    yield return new WaitUntil(() => exit);

                    if (error) {
                        yield break;
                    }
                }
            }

            return new Gs2InlineFuture<object>(Impl);
        }
        
#if GS2_ENABLE_UNITASK
        
        public static async UniTask ViewAsync(
            string placement,
            GameSession session
        ) {
            var future = ViewFuture(
                placement, 
                session
            );
            await future;
            if (future.Error != null) {
                throw future.Error;
            }
        }
#endif
    }

    public class UnityAdsInitializeListener : IUnityAdsInitializationListener
    {
        public UnityAction Finish;
        public UnityAction<string> Error;

        public void OnInitializationComplete() {
            this.Finish.Invoke();
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message) {
            this.Error.Invoke(error + ":" + message);
        }
    }

    public class UnityAdsLoadListener : IUnityAdsLoadListener
    {
        public UnityAction Finish;
        public UnityAction<string> Error;

        public void OnUnityAdsAdLoaded(string placementId) {
            this.Finish.Invoke();
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message) {
            this.Error.Invoke(error + ":" + message);
        }
    }

    public class UnityAdsShowListener : IUnityAdsShowListener
    {
        public UnityAction<string> Ready;
        public UnityAction Finish;
        public UnityAction<string> Error;

        public void OnUnityAdsShowClick(string placementId) {
            this.Ready.Invoke(placementId);
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) {
            this.Error.Invoke(error + ":" + message);
        }

        public void OnUnityAdsShowStart(string placementId) {
            
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState) {
            switch (showCompletionState) {
                case UnityAdsShowCompletionState.UNKNOWN:
                    this.Error.Invoke("failed");
                    break;
                case UnityAdsShowCompletionState.SKIPPED:
                    this.Error.Invoke("skip");
                    break;
                case UnityAdsShowCompletionState.COMPLETED:
                    this.Finish.Invoke();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(showCompletionState), showCompletionState, null);
            }
        }
    }
}

#endif
