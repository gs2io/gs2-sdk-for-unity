/*
 * Copyright 2016 Game Server Services, Inc. or its affiliates. All Rights
 * Reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * or in the "license" file accompanying this file. This file is distributed
 * on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
 * express or implied. See the License for the specific language governing
 * permissions and limitations under the License.
 */
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantUsingDirective
// ReSharper disable CheckNamespace
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UseObjectOrCollectionInitializer
// ReSharper disable ArrangeThisQualifier
// ReSharper disable NotAccessedField.Local

#pragma warning disable 1998

using System;
using System.Linq;
using System.Text.RegularExpressions;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Gs2Stamina.Domain.Iterator;
using Gs2.Gs2Stamina.Request;
using Gs2.Gs2Stamina.Result;
using Gs2.Gs2Auth.Model;
using Gs2.Util.LitJson;
using Gs2.Core;
using Gs2.Core.Domain;
using Gs2.Core.Util;
using UnityEngine.Scripting;
using System.Collections;
#if GS2_ENABLE_UNITASK
using Cysharp.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using System.Collections.Generic;
#endif

namespace Gs2.Unity.Gs2Stamina.Domain.Model
{

    public partial class EzStaminaGameSessionDomain {
        private readonly Gs2.Gs2Stamina.Domain.Model.StaminaAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public int? OverflowValue => _domain.OverflowValue;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;
        public string StaminaName => _domain?.StaminaName;

        public EzStaminaGameSessionDomain(
            Gs2.Gs2Stamina.Domain.Model.StaminaAccessTokenDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        [Obsolete("The name has been changed to ConsumeFuture.")]
        public IFuture<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> Consume(
            int consumeValue
        )
        {
            return ConsumeFuture(
                consumeValue
            );
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> ConsumeFuture(
            int consumeValue
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> self)
            {
                var future = this._domain.ConsumeFuture(
                    new ConsumeStaminaRequest()
                        .WithConsumeValue(consumeValue)
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain(future.Result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> ConsumeAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> ConsumeFuture(
        #endif
            int consumeValue
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.ConsumeAsync(
                        new ConsumeStaminaRequest()
                            .WithConsumeValue(consumeValue)
                            .WithAccessToken(_domain.AccessToken.Token)
                    );
                }
            );
            return new Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> self)
            {
                var future = _domain.ConsumeFuture(
                    new ConsumeStaminaRequest()
                        .WithConsumeValue(consumeValue)
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return _profile.RunFuture(
                    _domain.AccessToken,
                    future,
                    () =>
        			{
                		return future = _domain.ConsumeFuture(
                    		new ConsumeStaminaRequest()
                	        .WithConsumeValue(consumeValue)
                    	    .WithAccessToken(_domain.AccessToken.Token)
        		        );
        			}
                );
                if (future.Error != null)
                {
                    self.OnError(future.Error);
                    yield break;
                }
                var result = future.Result;
                self.OnComplete(new Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain(result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain>(Impl);
        #endif
        }

        [Obsolete("The name has been changed to SetMaxValueFuture.")]
        public IFuture<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> SetMaxValue(
            string keyId,
            string signedStatusBody,
            string signedStatusSignature
        )
        {
            return SetMaxValueFuture(
                keyId,
                signedStatusBody,
                signedStatusSignature
            );
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> SetMaxValueFuture(
            string keyId,
            string signedStatusBody,
            string signedStatusSignature
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> self)
            {
                var future = this._domain.SetMaxValueByStatusFuture(
                    new SetMaxValueByStatusRequest()
                        .WithKeyId(keyId)
                        .WithSignedStatusBody(signedStatusBody)
                        .WithSignedStatusSignature(signedStatusSignature)
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain(future.Result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> SetMaxValueAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> SetMaxValueFuture(
        #endif
            string keyId,
            string signedStatusBody,
            string signedStatusSignature
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.SetMaxValueByStatusAsync(
                        new SetMaxValueByStatusRequest()
                            .WithKeyId(keyId)
                            .WithSignedStatusBody(signedStatusBody)
                            .WithSignedStatusSignature(signedStatusSignature)
                            .WithAccessToken(_domain.AccessToken.Token)
                    );
                }
            );
            return new Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> self)
            {
                var future = _domain.SetMaxValueByStatusFuture(
                    new SetMaxValueByStatusRequest()
                        .WithKeyId(keyId)
                        .WithSignedStatusBody(signedStatusBody)
                        .WithSignedStatusSignature(signedStatusSignature)
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return _profile.RunFuture(
                    _domain.AccessToken,
                    future,
                    () =>
        			{
                		return future = _domain.SetMaxValueByStatusFuture(
                    		new SetMaxValueByStatusRequest()
                	        .WithKeyId(keyId)
                	        .WithSignedStatusBody(signedStatusBody)
                	        .WithSignedStatusSignature(signedStatusSignature)
                    	    .WithAccessToken(_domain.AccessToken.Token)
        		        );
        			}
                );
                if (future.Error != null)
                {
                    self.OnError(future.Error);
                    yield break;
                }
                var result = future.Result;
                self.OnComplete(new Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain(result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain>(Impl);
        #endif
        }

        [Obsolete("The name has been changed to SetRecoverIntervalFuture.")]
        public IFuture<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> SetRecoverInterval(
            string keyId,
            string signedStatusBody,
            string signedStatusSignature
        )
        {
            return SetRecoverIntervalFuture(
                keyId,
                signedStatusBody,
                signedStatusSignature
            );
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> SetRecoverIntervalFuture(
            string keyId,
            string signedStatusBody,
            string signedStatusSignature
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> self)
            {
                var future = this._domain.SetRecoverIntervalByStatusFuture(
                    new SetRecoverIntervalByStatusRequest()
                        .WithKeyId(keyId)
                        .WithSignedStatusBody(signedStatusBody)
                        .WithSignedStatusSignature(signedStatusSignature)
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain(future.Result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> SetRecoverIntervalAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> SetRecoverIntervalFuture(
        #endif
            string keyId,
            string signedStatusBody,
            string signedStatusSignature
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.SetRecoverIntervalByStatusAsync(
                        new SetRecoverIntervalByStatusRequest()
                            .WithKeyId(keyId)
                            .WithSignedStatusBody(signedStatusBody)
                            .WithSignedStatusSignature(signedStatusSignature)
                            .WithAccessToken(_domain.AccessToken.Token)
                    );
                }
            );
            return new Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> self)
            {
                var future = _domain.SetRecoverIntervalByStatusFuture(
                    new SetRecoverIntervalByStatusRequest()
                        .WithKeyId(keyId)
                        .WithSignedStatusBody(signedStatusBody)
                        .WithSignedStatusSignature(signedStatusSignature)
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return _profile.RunFuture(
                    _domain.AccessToken,
                    future,
                    () =>
        			{
                		return future = _domain.SetRecoverIntervalByStatusFuture(
                    		new SetRecoverIntervalByStatusRequest()
                	        .WithKeyId(keyId)
                	        .WithSignedStatusBody(signedStatusBody)
                	        .WithSignedStatusSignature(signedStatusSignature)
                    	    .WithAccessToken(_domain.AccessToken.Token)
        		        );
        			}
                );
                if (future.Error != null)
                {
                    self.OnError(future.Error);
                    yield break;
                }
                var result = future.Result;
                self.OnComplete(new Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain(result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain>(Impl);
        #endif
        }

        [Obsolete("The name has been changed to SetRecoverValueFuture.")]
        public IFuture<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> SetRecoverValue(
            string keyId,
            string signedStatusBody,
            string signedStatusSignature
        )
        {
            return SetRecoverValueFuture(
                keyId,
                signedStatusBody,
                signedStatusSignature
            );
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> SetRecoverValueFuture(
            string keyId,
            string signedStatusBody,
            string signedStatusSignature
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> self)
            {
                var future = this._domain.SetRecoverValueByStatusFuture(
                    new SetRecoverValueByStatusRequest()
                        .WithKeyId(keyId)
                        .WithSignedStatusBody(signedStatusBody)
                        .WithSignedStatusSignature(signedStatusSignature)
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain(future.Result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> SetRecoverValueAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> SetRecoverValueFuture(
        #endif
            string keyId,
            string signedStatusBody,
            string signedStatusSignature
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.SetRecoverValueByStatusAsync(
                        new SetRecoverValueByStatusRequest()
                            .WithKeyId(keyId)
                            .WithSignedStatusBody(signedStatusBody)
                            .WithSignedStatusSignature(signedStatusSignature)
                            .WithAccessToken(_domain.AccessToken.Token)
                    );
                }
            );
            return new Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> self)
            {
                var future = _domain.SetRecoverValueByStatusFuture(
                    new SetRecoverValueByStatusRequest()
                        .WithKeyId(keyId)
                        .WithSignedStatusBody(signedStatusBody)
                        .WithSignedStatusSignature(signedStatusSignature)
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return _profile.RunFuture(
                    _domain.AccessToken,
                    future,
                    () =>
        			{
                		return future = _domain.SetRecoverValueByStatusFuture(
                    		new SetRecoverValueByStatusRequest()
                	        .WithKeyId(keyId)
                	        .WithSignedStatusBody(signedStatusBody)
                	        .WithSignedStatusSignature(signedStatusSignature)
                    	    .WithAccessToken(_domain.AccessToken.Token)
        		        );
        			}
                );
                if (future.Error != null)
                {
                    self.OnError(future.Error);
                    yield break;
                }
                var result = future.Result;
                self.OnComplete(new Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain(result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain>(Impl);
        #endif
        }

        [Obsolete("The name has been changed to ModelFuture.")]
        public IFuture<Gs2.Unity.Gs2Stamina.Model.EzStamina> Model()
        {
            return ModelFuture();
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Stamina.Model.EzStamina> ModelFuture()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Stamina.Model.EzStamina> self)
            {
                yield return ModelAsync().ToCoroutine(
                    self.OnComplete,
                    e =>
                    {
                        if (e is Gs2.Core.Exception.Gs2Exception e2) {
                            self.OnError(e2);
                        }
                        else {
                            UnityEngine.Debug.LogError(e.Message);
                            self.OnError(new Gs2.Core.Exception.UnknownException(e.Message));
                        }
                    }
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Stamina.Model.EzStamina>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Stamina.Model.EzStamina> ModelAsync()
        {
            var item = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.ModelAsync();
                }
            );
            if (item == null) {
                return null;
            }
            return Gs2.Unity.Gs2Stamina.Model.EzStamina.FromModel(
                item
            );
        }
        #else
        public IFuture<Gs2.Unity.Gs2Stamina.Model.EzStamina> ModelFuture()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Stamina.Model.EzStamina> self)
            {
                var future = _domain.ModelFuture();
                yield return _profile.RunFuture(
                    _domain.AccessToken,
                    future,
                    () => {
                    	return future = _domain.ModelFuture();
                    }
                );
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                var item = future.Result;
                if (item == null) {
                    self.OnComplete(null);
                    yield break;
                }
                self.OnComplete(Gs2.Unity.Gs2Stamina.Model.EzStamina.FromModel(
                    item
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Stamina.Model.EzStamina>(Impl);
        }
        #endif

        public ulong Subscribe(Action<Gs2.Unity.Gs2Stamina.Model.EzStamina> callback)
        {
            return this._domain.Subscribe(item => {
                callback.Invoke(Gs2.Unity.Gs2Stamina.Model.EzStamina.FromModel(
                    item
                ));
            });
        }

        public void Unsubscribe(ulong callbackId)
        {
            this._domain.Unsubscribe(callbackId);
        }

    }
}
