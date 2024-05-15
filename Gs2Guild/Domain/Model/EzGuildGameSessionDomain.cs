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
#pragma warning disable CS0169, CS0168

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

    public partial class EzGuildGameSessionDomain {
        private readonly Gs2.Gs2Guild.Domain.Model.GuildAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.GuildGameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string NamespaceName => _domain?.NamespaceName;
        public string GuildModelName => _domain?.GuildModelName;
        public string GuildName => _domain?.GuildName;

        public EzGuildGameSessionDomain(
            Gs2.Gs2Guild.Domain.Model.GuildAccessTokenDomain domain,
            Gs2.Unity.Util.GuildGameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }

        public Gs2.Unity.Gs2Guild.Domain.Model.EzReceiveMemberRequestGameSessionDomain ReceiveMemberRequest(
            string fromUserId
        ) {
            return new Gs2.Unity.Gs2Guild.Domain.Model.EzReceiveMemberRequestGameSessionDomain(
                _domain.ReceiveMemberRequest(
                    fromUserId
                ),
                this._gameSession,
                this._connection
            );
        }

        public Gs2Iterator<Gs2.Unity.Gs2Guild.Model.EzReceiveMemberRequest> ReceiveMemberRequests(
        )
        {
            return new Gs2.Unity.Gs2Guild.Domain.Iterator.EzListReceiveRequestsIterator(
                this._domain,
                this._gameSession,
                this._connection
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Guild.Model.EzReceiveMemberRequest> ReceiveMemberRequestsAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Guild.Model.EzReceiveMemberRequest>(async (writer, token) =>
            {
                var it = _domain.ReceiveRequestsAsync(
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        this._gameSession,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.ReceiveRequestsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Guild.Model.EzReceiveMemberRequest.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeReceiveRequests(
            Action<Gs2.Unity.Gs2Guild.Model.EzReceiveMemberRequest[]> callback
        ) {
            return this._domain.SubscribeReceiveRequests(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Guild.Model.EzReceiveMemberRequest.FromModel).ToArray());
                }
            );
        }

        public void UnsubscribeReceiveRequests(
            ulong callbackId
        ) {
            this._domain.UnsubscribeReceiveRequests(
                callbackId
            );
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Guild.Domain.Model.EzGuildGameSessionDomain> UpdateAsync(
            string displayName,
            int? attribute1,
            int? attribute2,
            int? attribute3,
            int? attribute4,
            int? attribute5,
            string joinPolicy,
            Gs2.Gs2Guild.Model.RoleModel[] customRoles = null,
            string guildMemberDefaultRole = null
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.UpdateAsync(
                    new UpdateGuildRequest()
                        .WithDisplayName(displayName)
                        .WithAttribute1(attribute1)
                        .WithAttribute2(attribute2)
                        .WithAttribute3(attribute3)
                        .WithAttribute4(attribute4)
                        .WithAttribute5(attribute5)
                        .WithJoinPolicy(joinPolicy)
                        .WithCustomRoles(customRoles)
                        .WithGuildMemberDefaultRole(guildMemberDefaultRole)
                )
            );
            return new Gs2.Unity.Gs2Guild.Domain.Model.EzGuildGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        public IFuture<Gs2.Unity.Gs2Guild.Domain.Model.EzGuildGameSessionDomain> UpdateFuture(
            string displayName,
            int? attribute1,
            int? attribute2,
            int? attribute3,
            int? attribute4,
            int? attribute5,
            string joinPolicy,
            Gs2.Gs2Guild.Model.RoleModel[] customRoles = null,
            string guildMemberDefaultRole = null
        ) {
            IEnumerator Impl(IFuture<Gs2.Unity.Gs2Guild.Domain.Model.EzGuildGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.UpdateFuture(
                        new UpdateGuildRequest()
                            .WithDisplayName(displayName)
                            .WithAttribute1(attribute1)
                            .WithAttribute2(attribute2)
                            .WithAttribute3(attribute3)
                            .WithAttribute4(attribute4)
                            .WithAttribute5(attribute5)
                            .WithJoinPolicy(joinPolicy)
                            .WithCustomRoles(customRoles)
                            .WithGuildMemberDefaultRole(guildMemberDefaultRole)
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Guild.Domain.Model.EzGuildGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Guild.Domain.Model.EzGuildGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Guild.Domain.Model.EzGuildGameSessionDomain> DeleteMemberAsync(
            string targetUserId
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.DeleteMemberAsync(
                    new DeleteMemberRequest()
                        .WithTargetUserId(targetUserId)
                )
            );
            return new Gs2.Unity.Gs2Guild.Domain.Model.EzGuildGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        public IFuture<Gs2.Unity.Gs2Guild.Domain.Model.EzGuildGameSessionDomain> DeleteMemberFuture(
            string targetUserId
        ) {
            IEnumerator Impl(IFuture<Gs2.Unity.Gs2Guild.Domain.Model.EzGuildGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.DeleteMemberFuture(
                        new DeleteMemberRequest()
                            .WithTargetUserId(targetUserId)
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Guild.Domain.Model.EzGuildGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Guild.Domain.Model.EzGuildGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Guild.Domain.Model.EzGuildGameSessionDomain> UpdateMemberRoleAsync(
            string targetUserId,
            string roleName
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.UpdateMemberRoleAsync(
                    new UpdateMemberRoleRequest()
                        .WithTargetUserId(targetUserId)
                        .WithRoleName(roleName)
                )
            );
            return new Gs2.Unity.Gs2Guild.Domain.Model.EzGuildGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        public IFuture<Gs2.Unity.Gs2Guild.Domain.Model.EzGuildGameSessionDomain> UpdateMemberRoleFuture(
            string targetUserId,
            string roleName
        ) {
            IEnumerator Impl(IFuture<Gs2.Unity.Gs2Guild.Domain.Model.EzGuildGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.UpdateMemberRoleFuture(
                        new UpdateMemberRoleRequest()
                            .WithTargetUserId(targetUserId)
                            .WithRoleName(roleName)
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Guild.Domain.Model.EzGuildGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Guild.Domain.Model.EzGuildGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Guild.Domain.Model.EzGuildGameSessionDomain> DeleteAsync(
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.DeleteAsync(
                    new DeleteGuildRequest()
                )
            );
            return new Gs2.Unity.Gs2Guild.Domain.Model.EzGuildGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        public IFuture<Gs2.Unity.Gs2Guild.Domain.Model.EzGuildGameSessionDomain> DeleteFuture(
        ) {
            IEnumerator Impl(IFuture<Gs2.Unity.Gs2Guild.Domain.Model.EzGuildGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.DeleteFuture(
                        new DeleteGuildRequest()
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Guild.Domain.Model.EzGuildGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Guild.Domain.Model.EzGuildGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Guild.Model.EzGuild> ModelAsync()
        {
            var item = await this._connection.RunAsync(
                null,
                async () =>
                {
                    return await _domain.ModelAsync();
                }
            );
            if (item == null) {
                return null;
            }
            return Gs2.Unity.Gs2Guild.Model.EzGuild.FromModel(
                item
            );
        }
        #endif

        public IFuture<Gs2.Unity.Gs2Guild.Model.EzGuild> ModelFuture()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Guild.Model.EzGuild> self)
            {
                var future = this._connection.RunFuture(
                    null,
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
                self.OnComplete(Gs2.Unity.Gs2Guild.Model.EzGuild.FromModel(
                    item
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Guild.Model.EzGuild>(Impl);
        }

        public void Invalidate()
        {
            this._domain.Invalidate();
        }

        public ulong Subscribe(Action<Gs2.Unity.Gs2Guild.Model.EzGuild> callback)
        {
            return this._domain.Subscribe(item => {
                callback.Invoke(item == null ? null : Gs2.Unity.Gs2Guild.Model.EzGuild.FromModel(
                    item
                ));
            });
        }

        public void Unsubscribe(ulong callbackId)
        {
            this._domain.Unsubscribe(callbackId);
        }

        #if UNITY_2017_1_OR_NEWER
        public Gs2Future<ulong> SubscribeWithInitialCallFuture(Action<Gs2.Unity.Gs2Guild.Model.EzGuild> callback)
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
        public async UniTask<ulong> SubscribeWithInitialCallAsync(Action<Gs2.Unity.Gs2Guild.Model.EzGuild> callback)
            #else
        public async Task<ulong> SubscribeWithInitialCallAsync(Action<Gs2.Unity.Gs2Guild.Model.EzGuild> callback)
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
