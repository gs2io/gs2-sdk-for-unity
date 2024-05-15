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
 *
 * deny overwrite
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
using Gs2.Gs2Idle.Domain.Iterator;
using Gs2.Gs2Idle.Request;
using Gs2.Gs2Idle.Result;
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

namespace Gs2.Unity.Gs2Idle.Domain.Model
{

    public partial class EzStatusGameSessionDomain {
        private readonly Gs2.Gs2Idle.Domain.Model.StatusAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.IGameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string? TransactionId => _domain.TransactionId;
        public bool? AutoRunStampSheet => _domain.AutoRunStampSheet;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;
        public string CategoryName => _domain?.CategoryName;

        public EzStatusGameSessionDomain(
            Gs2.Gs2Idle.Domain.Model.StatusAccessTokenDomain domain,
            Gs2.Unity.Util.IGameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }

        [Obsolete("The name has been changed to PredictionFuture.")]
        public IFuture<Gs2.Unity.Core.Model.EzAcquireAction[]> Prediction(
        )
        {
            return PredictionFuture(
            );
        }

        public IFuture<Gs2.Unity.Core.Model.EzAcquireAction[]> PredictionFuture(
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Core.Model.EzAcquireAction[]> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.PredictionFuture(
                        new PredictionRequest()
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(future.Result.Select(v => new Gs2.Unity.Core.Model.EzAcquireAction {
                    Action = v.Action,
                    Request = v.Request
                }).ToArray());
            }
            return new Gs2InlineFuture<Gs2.Unity.Core.Model.EzAcquireAction[]>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Core.Model.EzAcquireAction[]> PredictionAsync(
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.PredictionAsync(
                    new PredictionRequest()
                )
            );
            return result.Select(v => new Gs2.Unity.Core.Model.EzAcquireAction {
                Action = v.Action,
                Request = v.Request
            }).ToArray();
        }
        #endif

        [Obsolete("The name has been changed to ReceiveFuture.")]
        public IFuture<Gs2.Unity.Core.Domain.EzTransactionDomain> Receive(
            bool speculativeExecute = true
        )
        {
            return ReceiveFuture(
                speculativeExecute
            );
        }

        public IFuture<Gs2.Unity.Core.Domain.EzTransactionDomain> ReceiveFuture(
            bool speculativeExecute = true
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Core.Domain.EzTransactionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.ReceiveFuture(
                        new ReceiveRequest(),
                        speculativeExecute
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(future.Result == null ? null : new Gs2.Unity.Core.Domain.EzTransactionDomain(future.Result));
            }
            return new Gs2InlineFuture<Gs2.Unity.Core.Domain.EzTransactionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Core.Domain.EzTransactionDomain> ReceiveAsync(
            bool speculativeExecute = true
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.ReceiveAsync(
                    new ReceiveRequest(),
                    speculativeExecute
                )
            );
            return result == null ? null : new Gs2.Unity.Core.Domain.EzTransactionDomain(result);
        }
        #endif

        [Obsolete("The name has been changed to ModelFuture.")]
        public IFuture<Gs2.Unity.Gs2Idle.Model.EzStatus> Model()
        {
            return ModelFuture();
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Idle.Model.EzStatus> ModelAsync()
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
            return Gs2.Unity.Gs2Idle.Model.EzStatus.FromModel(
                item
            );
        }
        #endif

        public IFuture<Gs2.Unity.Gs2Idle.Model.EzStatus> ModelFuture()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Idle.Model.EzStatus> self)
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
                self.OnComplete(Gs2.Unity.Gs2Idle.Model.EzStatus.FromModel(
                    item
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Idle.Model.EzStatus>(Impl);
        }

        public void Invalidate()
        {
            this._domain.Invalidate();
        }

        public ulong Subscribe(Action<Gs2.Unity.Gs2Idle.Model.EzStatus> callback)
        {
            return this._domain.Subscribe(item => {
                callback.Invoke(item == null ? null : Gs2.Unity.Gs2Idle.Model.EzStatus.FromModel(
                    item
                ));
            });
        }

        public void Unsubscribe(ulong callbackId)
        {
            this._domain.Unsubscribe(callbackId);
        }

        #if UNITY_2017_1_OR_NEWER
        public Gs2Future<ulong> SubscribeWithInitialCallFuture(Action<Gs2.Unity.Gs2Idle.Model.EzStatus> callback)
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
        public async UniTask<ulong> SubscribeWithInitialCallAsync(Action<Gs2.Unity.Gs2Idle.Model.EzStatus> callback)
            #else
        public async Task<ulong> SubscribeWithInitialCallAsync(Action<Gs2.Unity.Gs2Idle.Model.EzStatus> callback)
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
