#if GS2_ENABLE_APPLOVIN_MAX

using System;
using System.Collections;
using Gs2.Core;
using Gs2.Core.Domain;
using Gs2.Core.Exception;
using Gs2.Core.Model;
using UnityEngine;
using UnityEngine.Events;

#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
#endif

namespace Gs2.Unity.Util
{
    public class AppLovinMaxUtil
    {
        public static IEnumerator View(
            UnityAction<AsyncResult<object>> callback,
            string sdkKey,
            string adUnitId,
            IGameSession session
        ) {
            var future = ViewFuture(
                sdkKey,
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
            string sdkKey,
            string adUnitId,
            IGameSession session
        )
        {
            IEnumerator Impl(Gs2Future<object> result) {
                MaxSdk.SetSdkKey(sdkKey);
                MaxSdk.SetUserId(session.UserId);

                MaxSdkBase.ErrorInfo error = null;
                bool showed = false;
                
                void OnRewardedAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
                {
                    // Rewarded ad failed to load
                    // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds).
                    error = errorInfo;
                }

                void OnRewardedAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo) {
                    error = errorInfo;
                }

                void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
                {
                    // The rewarded ad displayed and the user should receive the reward.
                    showed = true;
                }

                MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdLoadFailedEvent;
                MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailedToDisplayEvent;
                MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;
                try {
                    MaxSdk.InitializeSdk();
                    yield return new WaitUntil(() => error != null || MaxSdk.IsInitialized());
                    if (error != null) {
                        result.OnError(new ServiceUnavailableException(
                            new RequestError[] {
                                new (
                                    "appLovinMax",
                                    error.Message
                                ),
                            }
                        ));
                        yield break;
                    }
                    
                    MaxSdk.LoadRewardedAd(adUnitId);
                    yield return new WaitUntil(() => error != null || MaxSdk.IsRewardedAdReady(adUnitId));
                    if (error != null) {
                        result.OnError(new ServiceUnavailableException(
                            new RequestError[] {
                                new (
                                    "appLovinMax",
                                    error.Message
                                ),
                            }
                        ));
                        yield break;
                    }
                    
                    MaxSdk.ShowRewardedAd(adUnitId);
                    yield return new WaitUntil(() => error != null || showed);
                    if (error != null) {
                        result.OnError(new ServiceUnavailableException(
                            new RequestError[] {
                                new (
                                    "appLovinMax",
                                    error.Message
                                ),
                            }
                        ));
                        yield break;
                    }
                } finally
                {
                    MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent -= OnRewardedAdLoadFailedEvent;
                    MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent -= OnRewardedAdFailedToDisplayEvent;
                    MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent -= OnRewardedAdReceivedRewardEvent;
                }
            }

            return new Gs2InlineFuture<object>(Impl);
        }
        
#if GS2_ENABLE_UNITASK
        
        public static async UniTask ViewAsync(
            string sdkKey,
            string adUnitId,
            IGameSession session
        ) {
            var future = ViewFuture(
                sdkKey, 
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
