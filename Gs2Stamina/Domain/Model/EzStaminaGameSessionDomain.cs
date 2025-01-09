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
        private readonly Gs2.Unity.Util.IGameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public int? OverflowValue => _domain.OverflowValue;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;
        public string StaminaName => _domain?.StaminaName;

        public EzStaminaGameSessionDomain(
            Gs2.Gs2Stamina.Domain.Model.StaminaAccessTokenDomain domain,
            Gs2.Unity.Util.IGameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
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

        public IFuture<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> ConsumeFuture(
            int consumeValue
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.ConsumeFuture(
                        new ConsumeStaminaRequest()
                            .WithConsumeValue(consumeValue)
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> ConsumeAsync(
            int consumeValue
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.ConsumeAsync(
                    new ConsumeStaminaRequest()
                        .WithConsumeValue(consumeValue)
                )
            );
            return new Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        [Obsolete("The name has been changed to ApplyFuture.")]
        public IFuture<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> Apply(
        )
        {
            return ApplyFuture(
            );
        }

        public IFuture<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> ApplyFuture(
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.ApplyFuture(
                        new ApplyStaminaRequest()
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> ApplyAsync(
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.ApplyAsync(
                    new ApplyStaminaRequest()
                )
            );
            return new Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        [Obsolete("The name has been changed to SetMaxValueFuture.")]
        public IFuture<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> SetMaxValue(
            string signedStatusBody,
            string signedStatusSignature,
            string keyId = null
        )
        {
            return SetMaxValueFuture(
                signedStatusBody,
                signedStatusSignature,
                keyId
            );
        }

        public IFuture<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> SetMaxValueFuture(
            string signedStatusBody,
            string signedStatusSignature,
            string keyId = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.SetMaxValueByStatusFuture(
                        new SetMaxValueByStatusRequest()
                            .WithKeyId(keyId)
                            .WithSignedStatusBody(signedStatusBody)
                            .WithSignedStatusSignature(signedStatusSignature)
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> SetMaxValueAsync(
            string signedStatusBody,
            string signedStatusSignature,
            string keyId = null
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.SetMaxValueByStatusAsync(
                    new SetMaxValueByStatusRequest()
                        .WithKeyId(keyId)
                        .WithSignedStatusBody(signedStatusBody)
                        .WithSignedStatusSignature(signedStatusSignature)
                )
            );
            return new Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        [Obsolete("The name has been changed to SetRecoverIntervalFuture.")]
        public IFuture<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> SetRecoverInterval(
            string signedStatusBody,
            string signedStatusSignature,
            string keyId = null
        )
        {
            return SetRecoverIntervalFuture(
                signedStatusBody,
                signedStatusSignature,
                keyId
            );
        }

        public IFuture<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> SetRecoverIntervalFuture(
            string signedStatusBody,
            string signedStatusSignature,
            string keyId = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.SetRecoverIntervalByStatusFuture(
                        new SetRecoverIntervalByStatusRequest()
                            .WithKeyId(keyId)
                            .WithSignedStatusBody(signedStatusBody)
                            .WithSignedStatusSignature(signedStatusSignature)
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> SetRecoverIntervalAsync(
            string signedStatusBody,
            string signedStatusSignature,
            string keyId = null
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.SetRecoverIntervalByStatusAsync(
                    new SetRecoverIntervalByStatusRequest()
                        .WithKeyId(keyId)
                        .WithSignedStatusBody(signedStatusBody)
                        .WithSignedStatusSignature(signedStatusSignature)
                )
            );
            return new Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        [Obsolete("The name has been changed to SetRecoverValueFuture.")]
        public IFuture<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> SetRecoverValue(
            string signedStatusBody,
            string signedStatusSignature,
            string keyId = null
        )
        {
            return SetRecoverValueFuture(
                signedStatusBody,
                signedStatusSignature,
                keyId
            );
        }

        public IFuture<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> SetRecoverValueFuture(
            string signedStatusBody,
            string signedStatusSignature,
            string keyId = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.SetRecoverValueByStatusFuture(
                        new SetRecoverValueByStatusRequest()
                            .WithKeyId(keyId)
                            .WithSignedStatusBody(signedStatusBody)
                            .WithSignedStatusSignature(signedStatusSignature)
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain> SetRecoverValueAsync(
            string signedStatusBody,
            string signedStatusSignature,
            string keyId = null
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.SetRecoverValueByStatusAsync(
                    new SetRecoverValueByStatusRequest()
                        .WithKeyId(keyId)
                        .WithSignedStatusBody(signedStatusBody)
                        .WithSignedStatusSignature(signedStatusSignature)
                )
            );
            return new Gs2.Unity.Gs2Stamina.Domain.Model.EzStaminaGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        [Obsolete("The name has been changed to ModelFuture.")]
        public IFuture<Gs2.Unity.Gs2Stamina.Model.EzStamina> Model()
        {
            return ModelFuture();
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Stamina.Model.EzStamina> ModelAsync()
        {
            var item = await this._connection.RunAsync(
                this._gameSession,
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
        #endif

        public IFuture<Gs2.Unity.Gs2Stamina.Model.EzStamina> ModelFuture()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Stamina.Model.EzStamina> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => {
                    	return _domain.ModelFuture();
                    }
                );
                yield return future;
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

        public void Invalidate()
        {
            this._domain.Invalidate();
        }

        public ulong Subscribe(Action<Gs2.Unity.Gs2Stamina.Model.EzStamina> callback)
        {
            return this._domain.Subscribe(item => {
                callback.Invoke(item == null ? null : Gs2.Unity.Gs2Stamina.Model.EzStamina.FromModel(
                    item
                ));
            });
        }

        public void Unsubscribe(ulong callbackId)
        {
            this._domain.Unsubscribe(callbackId);
        }

        #if UNITY_2017_1_OR_NEWER
        public Gs2Future<ulong> SubscribeWithInitialCallFuture(Action<Gs2.Unity.Gs2Stamina.Model.EzStamina> callback)
        {
            IEnumerator Impl(IFuture<ulong> self)
            {
                var future = ModelFuture();
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                var item = future.Result;
                var callbackId = Subscribe(callback);
                callback.Invoke(item);
                self.OnComplete(callbackId);
            }
            return new Gs2InlineFuture<ulong>(Impl);
        }
        #endif

        #if !UNITY_2017_1_OR_NEWER || GS2_ENABLE_UNITASK
            #if UNITY_2017_1_OR_NEWER
        public async UniTask<ulong> SubscribeWithInitialCallAsync(Action<Gs2.Unity.Gs2Stamina.Model.EzStamina> callback)
            #else
        public async Task<ulong> SubscribeWithInitialCallAsync(Action<Gs2.Unity.Gs2Stamina.Model.EzStamina> callback)
            #endif
        {
            var item = await ModelAsync();
            var callbackId = Subscribe(callback);
            callback.Invoke(item);
            return callbackId;
        }
        #endif

    }
}
