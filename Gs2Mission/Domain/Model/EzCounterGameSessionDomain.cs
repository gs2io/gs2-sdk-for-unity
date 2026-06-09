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
using Gs2.Gs2Mission.Domain.Iterator;
using Gs2.Gs2Mission.Request;
using Gs2.Gs2Mission.Result;
using Gs2.Gs2Auth.Model;
using Gs2.Util.LitJson;
using Gs2.Core;
using Gs2.Core.Domain;
using Gs2.Core.Util;
#if UNITY_2017_1_OR_NEWER
using UnityEngine.Scripting;
using System.Collections;
    #if GS2_ENABLE_UNITASK
using Cysharp.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using System.Collections.Generic;
    #endif
#else
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Gs2.Unity.Gs2Mission.Domain.Model
{

    public partial class EzCounterGameSessionDomain {
        private readonly Gs2.Gs2Mission.Domain.Model.CounterAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.IGameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public Gs2.Unity.Gs2Mission.Model.EzComplete[] ChangedCompletes => _domain.ChangedCompletes.Select(Gs2.Unity.Gs2Mission.Model.EzComplete.FromModel).ToArray();
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;
        public string CounterName => _domain?.CounterName;

        public EzCounterGameSessionDomain(
            Gs2.Gs2Mission.Domain.Model.CounterAccessTokenDomain domain,
            Gs2.Unity.Util.IGameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }

        #if UNITY_2017_1_OR_NEWER
        [Obsolete("The name has been changed to DecreaseCounterFuture.")]
        public IFuture<Gs2.Unity.Gs2Mission.Domain.Model.EzCounterGameSessionDomain> DecreaseCounter(
            long value
        )
        {
            return DecreaseCounterFuture(
                value
            );
        }

        public IFuture<Gs2.Unity.Gs2Mission.Domain.Model.EzCounterGameSessionDomain> DecreaseCounterFuture(
            long value
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Mission.Domain.Model.EzCounterGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.DecreaseFuture(
                        new DecreaseCounterRequest()
                            .WithValue(value)
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Mission.Domain.Model.EzCounterGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Mission.Domain.Model.EzCounterGameSessionDomain>(Impl);
        }
        #endif

        #if !UNITY_2017_1_OR_NEWER || GS2_ENABLE_UNITASK
            #if UNITY_2017_1_OR_NEWER
        public async UniTask<Gs2.Unity.Gs2Mission.Domain.Model.EzCounterGameSessionDomain> DecreaseCounterAsync(
            #else
        public async Task<Gs2.Unity.Gs2Mission.Domain.Model.EzCounterGameSessionDomain> DecreaseCounterAsync(
            #endif
            long value
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.DecreaseAsync(
                    new DecreaseCounterRequest()
                        .WithValue(value)
                )
            );
            return new Gs2.Unity.Gs2Mission.Domain.Model.EzCounterGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        #if UNITY_2017_1_OR_NEWER
        [Obsolete("The name has been changed to ResetCounterFuture.")]
        public IFuture<Gs2.Unity.Gs2Mission.Domain.Model.EzCounterGameSessionDomain> ResetCounter(
            Gs2.Unity.Gs2Mission.Model.EzScopedValue[] scopes
        )
        {
            return ResetCounterFuture(
                scopes
            );
        }

        public IFuture<Gs2.Unity.Gs2Mission.Domain.Model.EzCounterGameSessionDomain> ResetCounterFuture(
            Gs2.Unity.Gs2Mission.Model.EzScopedValue[] scopes
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Mission.Domain.Model.EzCounterGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.ResetFuture(
                        new ResetCounterRequest()
                            .WithScopes(scopes?.Select(v => v.ToModel()).ToArray())
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Mission.Domain.Model.EzCounterGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Mission.Domain.Model.EzCounterGameSessionDomain>(Impl);
        }
        #endif

        #if !UNITY_2017_1_OR_NEWER || GS2_ENABLE_UNITASK
            #if UNITY_2017_1_OR_NEWER
        public async UniTask<Gs2.Unity.Gs2Mission.Domain.Model.EzCounterGameSessionDomain> ResetCounterAsync(
            #else
        public async Task<Gs2.Unity.Gs2Mission.Domain.Model.EzCounterGameSessionDomain> ResetCounterAsync(
            #endif
            Gs2.Unity.Gs2Mission.Model.EzScopedValue[] scopes
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.ResetAsync(
                    new ResetCounterRequest()
                        .WithScopes(scopes?.Select(v => v.ToModel()).ToArray())
                )
            );
            return new Gs2.Unity.Gs2Mission.Domain.Model.EzCounterGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        #if UNITY_2017_1_OR_NEWER
        [Obsolete("The name has been changed to DeleteCounterFuture.")]
        public IFuture<Gs2.Unity.Gs2Mission.Domain.Model.EzCounterGameSessionDomain> DeleteCounter(
        )
        {
            return DeleteCounterFuture(
            );
        }

        public IFuture<Gs2.Unity.Gs2Mission.Domain.Model.EzCounterGameSessionDomain> DeleteCounterFuture(
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Mission.Domain.Model.EzCounterGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.DeleteFuture(
                        new DeleteCounterRequest()
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Mission.Domain.Model.EzCounterGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Mission.Domain.Model.EzCounterGameSessionDomain>(Impl);
        }
        #endif

        #if !UNITY_2017_1_OR_NEWER || GS2_ENABLE_UNITASK
            #if UNITY_2017_1_OR_NEWER
        public async UniTask<Gs2.Unity.Gs2Mission.Domain.Model.EzCounterGameSessionDomain> DeleteCounterAsync(
            #else
        public async Task<Gs2.Unity.Gs2Mission.Domain.Model.EzCounterGameSessionDomain> DeleteCounterAsync(
            #endif
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.DeleteAsync(
                    new DeleteCounterRequest()
                )
            );
            return new Gs2.Unity.Gs2Mission.Domain.Model.EzCounterGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        #if UNITY_2017_1_OR_NEWER
        [Obsolete("The name has been changed to ModelFuture.")]
        public IFuture<Gs2.Unity.Gs2Mission.Model.EzCounter> Model()
        {
            return ModelFuture();
        }
        #endif

        #if !UNITY_2017_1_OR_NEWER || GS2_ENABLE_UNITASK
            #if UNITY_2017_1_OR_NEWER
        public async UniTask<Gs2.Unity.Gs2Mission.Model.EzCounter> ModelAsync()
            #else
        public async Task<Gs2.Unity.Gs2Mission.Model.EzCounter> ModelAsync()
            #endif
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
            return Gs2.Unity.Gs2Mission.Model.EzCounter.FromModel(
                item
            );
        }
        #endif

        #if UNITY_2017_1_OR_NEWER
        public IFuture<Gs2.Unity.Gs2Mission.Model.EzCounter> ModelFuture()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Mission.Model.EzCounter> self)
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
                self.OnComplete(Gs2.Unity.Gs2Mission.Model.EzCounter.FromModel(
                    item
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Mission.Model.EzCounter>(Impl);
        }
        #endif

        public void Invalidate()
        {
            this._domain.Invalidate();
        }

        public ulong Subscribe(Action<Gs2.Unity.Gs2Mission.Model.EzCounter> callback)
        {
            return this._domain.Subscribe(item => {
                callback.Invoke(item == null ? null : Gs2.Unity.Gs2Mission.Model.EzCounter.FromModel(
                    item
                ));
            });
        }

        public void Unsubscribe(ulong callbackId)
        {
            this._domain.Unsubscribe(callbackId);
        }

        #if UNITY_2017_1_OR_NEWER
        public Gs2Future<ulong> SubscribeWithInitialCallFuture(Action<Gs2.Unity.Gs2Mission.Model.EzCounter> callback)
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
        public async UniTask<ulong> SubscribeWithInitialCallAsync(Action<Gs2.Unity.Gs2Mission.Model.EzCounter> callback)
            #else
        public async Task<ulong> SubscribeWithInitialCallAsync(Action<Gs2.Unity.Gs2Mission.Model.EzCounter> callback)
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
