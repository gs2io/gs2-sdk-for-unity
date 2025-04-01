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
using Gs2.Gs2Guild.Domain.Iterator;
using Gs2.Gs2Guild.Request;
using Gs2.Gs2Guild.Result;
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

namespace Gs2.Unity.Gs2Guild.Domain.Model
{

    public partial class EzJoinedGuildGameSessionDomain {
        private readonly Gs2.Gs2Guild.Domain.Model.JoinedGuildAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.IGameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;
        public string GuildModelName => _domain?.GuildModelName;
        public string GuildName => _domain?.GuildName;

        public EzJoinedGuildGameSessionDomain(
            Gs2.Gs2Guild.Domain.Model.JoinedGuildAccessTokenDomain domain,
            Gs2.Unity.Util.IGameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }

        [Obsolete("The name has been changed to UpdateMemberMetadataFuture.")]
        public IFuture<Gs2.Unity.Gs2Guild.Domain.Model.EzGuildDomain> UpdateMemberMetadata(
            string? metadata = null
        )
        {
            return UpdateMemberMetadataFuture(
                metadata
            );
        }

        public IFuture<Gs2.Unity.Gs2Guild.Domain.Model.EzGuildDomain> UpdateMemberMetadataFuture(
            string? metadata = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Guild.Domain.Model.EzGuildDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.UpdateMemberMetadataFuture(
                        new UpdateMemberMetadataRequest()
                            .WithMetadata(metadata)
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Guild.Domain.Model.EzGuildDomain(
                    future.Result,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Guild.Domain.Model.EzGuildDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Guild.Domain.Model.EzGuildDomain> UpdateMemberMetadataAsync(
            string? metadata = null
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.UpdateMemberMetadataAsync(
                    new UpdateMemberMetadataRequest()
                        .WithMetadata(metadata)
                )
            );
            return new Gs2.Unity.Gs2Guild.Domain.Model.EzGuildDomain(
                result,
                this._connection
            );
        }
        #endif

        [Obsolete("The name has been changed to WithdrawGuildFuture.")]
        public IFuture<Gs2.Unity.Gs2Guild.Domain.Model.EzJoinedGuildGameSessionDomain> WithdrawGuild(
        )
        {
            return WithdrawGuildFuture(
            );
        }

        public IFuture<Gs2.Unity.Gs2Guild.Domain.Model.EzJoinedGuildGameSessionDomain> WithdrawGuildFuture(
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Guild.Domain.Model.EzJoinedGuildGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.WithdrawalFuture(
                        new WithdrawalRequest()
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Guild.Domain.Model.EzJoinedGuildGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Guild.Domain.Model.EzJoinedGuildGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Guild.Domain.Model.EzJoinedGuildGameSessionDomain> WithdrawGuildAsync(
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.WithdrawalAsync(
                    new WithdrawalRequest()
                )
            );
            return new Gs2.Unity.Gs2Guild.Domain.Model.EzJoinedGuildGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        [Obsolete("The name has been changed to ModelFuture.")]
        public IFuture<Gs2.Unity.Gs2Guild.Model.EzJoinedGuild> Model()
        {
            return ModelFuture();
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Guild.Model.EzJoinedGuild> ModelAsync()
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
            return Gs2.Unity.Gs2Guild.Model.EzJoinedGuild.FromModel(
                item
            );
        }
        #endif

        public IFuture<Gs2.Unity.Gs2Guild.Model.EzJoinedGuild> ModelFuture()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Guild.Model.EzJoinedGuild> self)
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
                self.OnComplete(Gs2.Unity.Gs2Guild.Model.EzJoinedGuild.FromModel(
                    item
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Guild.Model.EzJoinedGuild>(Impl);
        }

        public void Invalidate()
        {
            this._domain.Invalidate();
        }

        public ulong Subscribe(Action<Gs2.Unity.Gs2Guild.Model.EzJoinedGuild> callback)
        {
            return this._domain.Subscribe(item => {
                callback.Invoke(item == null ? null : Gs2.Unity.Gs2Guild.Model.EzJoinedGuild.FromModel(
                    item
                ));
            });
        }

        public void Unsubscribe(ulong callbackId)
        {
            this._domain.Unsubscribe(callbackId);
        }

        #if UNITY_2017_1_OR_NEWER
        public Gs2Future<ulong> SubscribeWithInitialCallFuture(Action<Gs2.Unity.Gs2Guild.Model.EzJoinedGuild> callback)
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
        public async UniTask<ulong> SubscribeWithInitialCallAsync(Action<Gs2.Unity.Gs2Guild.Model.EzJoinedGuild> callback)
            #else
        public async Task<ulong> SubscribeWithInitialCallAsync(Action<Gs2.Unity.Gs2Guild.Model.EzJoinedGuild> callback)
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
