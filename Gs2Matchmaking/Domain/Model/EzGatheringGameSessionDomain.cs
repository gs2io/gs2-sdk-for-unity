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
using Gs2.Gs2Matchmaking.Domain.Iterator;
using Gs2.Gs2Matchmaking.Request;
using Gs2.Gs2Matchmaking.Result;
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

namespace Gs2.Unity.Gs2Matchmaking.Domain.Model
{

    public partial class EzGatheringGameSessionDomain {
        private readonly Gs2.Gs2Matchmaking.Domain.Model.GatheringAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.GameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;
        public string GatheringName => _domain?.GatheringName;

        public EzGatheringGameSessionDomain(
            Gs2.Gs2Matchmaking.Domain.Model.GatheringAccessTokenDomain domain,
            Gs2.Unity.Util.GameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }

        [Obsolete("The name has been changed to UpdateGatheringFuture.")]
        public IFuture<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzGatheringGameSessionDomain> UpdateGathering(
            Gs2.Unity.Gs2Matchmaking.Model.EzAttributeRange[] attributeRanges = null
        )
        {
            return UpdateGatheringFuture(
                attributeRanges
            );
        }

        public IFuture<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzGatheringGameSessionDomain> UpdateGatheringFuture(
            Gs2.Unity.Gs2Matchmaking.Model.EzAttributeRange[] attributeRanges = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzGatheringGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.UpdateFuture(
                        new UpdateGatheringRequest()
                            .WithAttributeRanges(attributeRanges?.Select(v => v.ToModel()).ToArray())
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Matchmaking.Domain.Model.EzGatheringGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzGatheringGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzGatheringGameSessionDomain> UpdateGatheringAsync(
            Gs2.Unity.Gs2Matchmaking.Model.EzAttributeRange[] attributeRanges = null
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.UpdateAsync(
                    new UpdateGatheringRequest()
                        .WithAttributeRanges(attributeRanges?.Select(v => v.ToModel()).ToArray())
                )
            );
            return new Gs2.Unity.Gs2Matchmaking.Domain.Model.EzGatheringGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        [Obsolete("The name has been changed to CancelMatchmakingFuture.")]
        public IFuture<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzGatheringGameSessionDomain> CancelMatchmaking(
        )
        {
            return CancelMatchmakingFuture(
            );
        }

        public IFuture<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzGatheringGameSessionDomain> CancelMatchmakingFuture(
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzGatheringGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.CancelMatchmakingFuture(
                        new CancelMatchmakingRequest()
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Matchmaking.Domain.Model.EzGatheringGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzGatheringGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzGatheringGameSessionDomain> CancelMatchmakingAsync(
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.CancelMatchmakingAsync(
                    new CancelMatchmakingRequest()
                )
            );
            return new Gs2.Unity.Gs2Matchmaking.Domain.Model.EzGatheringGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        [Obsolete("The name has been changed to ModelFuture.")]
        public IFuture<Gs2.Unity.Gs2Matchmaking.Model.EzGathering> Model()
        {
            return ModelFuture();
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Matchmaking.Model.EzGathering> ModelAsync()
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
            return Gs2.Unity.Gs2Matchmaking.Model.EzGathering.FromModel(
                item
            );
        }
        #endif

        public IFuture<Gs2.Unity.Gs2Matchmaking.Model.EzGathering> ModelFuture()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Matchmaking.Model.EzGathering> self)
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
                self.OnComplete(Gs2.Unity.Gs2Matchmaking.Model.EzGathering.FromModel(
                    item
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Matchmaking.Model.EzGathering>(Impl);
        }

        public void Invalidate()
        {
            this._domain.Invalidate();
        }

        public ulong Subscribe(Action<Gs2.Unity.Gs2Matchmaking.Model.EzGathering> callback)
        {
            return this._domain.Subscribe(item => {
                callback.Invoke(item == null ? null : Gs2.Unity.Gs2Matchmaking.Model.EzGathering.FromModel(
                    item
                ));
            });
        }

        public void Unsubscribe(ulong callbackId)
        {
            this._domain.Unsubscribe(callbackId);
        }

        #if UNITY_2017_1_OR_NEWER
        public Gs2Future<ulong> SubscribeWithInitialCallFuture(Action<Gs2.Unity.Gs2Matchmaking.Model.EzGathering> callback)
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
        public async UniTask<ulong> SubscribeWithInitialCallAsync(Action<Gs2.Unity.Gs2Matchmaking.Model.EzGathering> callback)
            #else
        public async Task<ulong> SubscribeWithInitialCallAsync(Action<Gs2.Unity.Gs2Matchmaking.Model.EzGathering> callback)
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
