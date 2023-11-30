#if GS2_ENABLE_ADMOB

using System.Collections;
using System.Collections.Generic;
using Gs2.Core;
using Gs2.Core.Domain;
using Gs2.Core.Exception;
using Gs2.Core.Model;
using UnityEngine;
using UnityEngine.Events;
using GoogleMobileAds.Api;
#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
#endif

namespace Gs2.Unity.Util
{
    public class AdMobUtil
    {
        public static IEnumerator Initialize(
            UnityAction<AsyncResult<object>> callback,
            RequestConfiguration configuration
        ) {
            var future = InitializeFuture(
                configuration
            );
            yield return future;
            callback.Invoke(new AsyncResult<object>(
                future.Result,
                future.Error
            ));
        }
        
        public static Gs2Future<object> InitializeFuture(
            RequestConfiguration configuration
        )
        {
            IEnumerator Impl(Gs2Future<object> result) {
                bool exit = false;
                MobileAds.SetRequestConfiguration(configuration);
                MobileAds.Initialize(initStatus =>
                {
                    result.OnComplete(null);
                    exit = true;
                });
                yield return new WaitUntil(() => exit);
            }

            return new Gs2InlineFuture<object>(Impl);
        }
        
#if GS2_ENABLE_UNITASK
        
        public static async UniTask InitializeAsync(
            RequestConfiguration configuration
        ) {
            var future = InitializeFuture(
                configuration
            );
            await future;
            if (future.Error != null) {
                throw future.Error;
            }
        }
#endif
        
        public static IEnumerator View(
            UnityAction<AsyncResult<object>> callback,
            string adUnitId,
            GameSession session
        ) {
            var future = ViewFuture(
                adUnitId, 
                session
            );
            yield return future;
            callback.Invoke(new AsyncResult<object>(
                future.Result,
                future.Error
            ));
        }
        
        public static Gs2Future<object> ViewFuture(
            string adUnitId,
            GameSession session
        )
        {
            IEnumerator Impl(Gs2Future<object> result) {
                {
                    bool paid = false;
                    bool exit = false;
                    RewardedAd runningAd = null;
                    RewardedAd.Load(adUnitId, new AdRequest(),
                        (RewardedAd ad, LoadAdError error) =>
                        {
                            if (error != null || ad == null)
                            {
                                result.OnError(new BadGatewayException(
                                    new[] {
                                        new RequestError("ads", error?.ToString()),
                                    }
                                ));
                                exit = true;
                            }
                            else {
                                ad.SetServerSideVerificationOptions(new ServerSideVerificationOptions {
                                    UserId = session.AccessToken.UserId,
                                });
                                ad.Show(_ =>
                                {
                                    result.OnComplete(null);
                                    exit = true;
                                });
                                runningAd = ad;
                            }
                        });
                    yield return new WaitUntil(() => exit);

                    if (runningAd != null) {
                        runningAd.Destroy();
                    }
                }
            }

            return new Gs2InlineFuture<object>(Impl);
        }
        
#if GS2_ENABLE_UNITASK
        
        public static async UniTask ViewAsync(
            string adUnitId,
            GameSession session
        ) {
            var future = ViewFuture(
                adUnitId, 
                session
            );
            await future;
            if (future.Error != null) {
                throw future.Error;
            }
        }
#endif
    }
}

#endif
