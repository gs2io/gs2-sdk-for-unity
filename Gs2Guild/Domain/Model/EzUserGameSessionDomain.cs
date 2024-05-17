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
using Gs2.Unity.Util;
#if GS2_ENABLE_UNITASK
using Cysharp.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using System.Collections.Generic;
#endif

namespace Gs2.Unity.Gs2Guild.Domain.Model
{

    public partial class EzUserGameSessionDomain {
        private readonly Gs2.Gs2Guild.Domain.Model.UserAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.IGameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public Gs2.Unity.Util.IGameSession GameSession => this._gameSession;
        public Gs2.Unity.Util.Gs2Connection Connection => this._connection;

        public EzUserGameSessionDomain(
            Gs2.Gs2Guild.Domain.Model.UserAccessTokenDomain domain,
            Gs2.Unity.Util.IGameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }

        [Obsolete("The name has been changed to CreateGuildFuture.")]
        public IFuture<Gs2.Unity.Gs2Guild.Domain.Model.EzGuildDomain> CreateGuild(
            string guildModelName,
            string displayName,
            string joinPolicy,
            int? attribute1 = null,
            int? attribute2 = null,
            int? attribute3 = null,
            int? attribute4 = null,
            int? attribute5 = null,
            Gs2.Unity.Gs2Guild.Model.EzRoleModel[] customRoles = null,
            string? guildMemberDefaultRole = null
        )
        {
            return CreateGuildFuture(
                guildModelName,
                displayName,
                joinPolicy,
                attribute1,
                attribute2,
                attribute3,
                attribute4,
                attribute5,
                customRoles,
                guildMemberDefaultRole
            );
        }

        public IFuture<Gs2.Unity.Gs2Guild.Domain.Model.EzGuildDomain> CreateGuildFuture(
            string guildModelName,
            string displayName,
            string joinPolicy,
            int? attribute1 = null,
            int? attribute2 = null,
            int? attribute3 = null,
            int? attribute4 = null,
            int? attribute5 = null,
            Gs2.Unity.Gs2Guild.Model.EzRoleModel[] customRoles = null,
            string? guildMemberDefaultRole = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Guild.Domain.Model.EzGuildDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.CreateGuildFuture(
                        new CreateGuildRequest()
                            .WithGuildModelName(guildModelName)
                            .WithDisplayName(displayName)
                            .WithAttribute1(attribute1)
                            .WithAttribute2(attribute2)
                            .WithAttribute3(attribute3)
                            .WithAttribute4(attribute4)
                            .WithAttribute5(attribute5)
                            .WithJoinPolicy(joinPolicy)
                            .WithCustomRoles(customRoles?.Select(v => v.ToModel()).ToArray())
                            .WithGuildMemberDefaultRole(guildMemberDefaultRole)
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
        public async UniTask<Gs2.Unity.Gs2Guild.Domain.Model.EzGuildDomain> CreateGuildAsync(
            string guildModelName,
            string displayName,
            string joinPolicy,
            int? attribute1 = null,
            int? attribute2 = null,
            int? attribute3 = null,
            int? attribute4 = null,
            int? attribute5 = null,
            Gs2.Unity.Gs2Guild.Model.EzRoleModel[] customRoles = null,
            string? guildMemberDefaultRole = null
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.CreateGuildAsync(
                    new CreateGuildRequest()
                        .WithGuildModelName(guildModelName)
                        .WithDisplayName(displayName)
                        .WithAttribute1(attribute1)
                        .WithAttribute2(attribute2)
                        .WithAttribute3(attribute3)
                        .WithAttribute4(attribute4)
                        .WithAttribute5(attribute5)
                        .WithJoinPolicy(joinPolicy)
                        .WithCustomRoles(customRoles?.Select(v => v.ToModel()).ToArray())
                        .WithGuildMemberDefaultRole(guildMemberDefaultRole)
                )
            );
            return new Gs2.Unity.Gs2Guild.Domain.Model.EzGuildDomain(
                result,
                this._connection
            );
        }
        #endif

        public Gs2Iterator<Gs2.Unity.Gs2Guild.Model.EzGuild> SearchGuilds(
            string guildModelName,
            string? displayName = null,
            int[]? attributes1 = null,
            int[]? attributes2 = null,
            int[]? attributes3 = null,
            int[]? attributes4 = null,
            int[]? attributes5 = null,
            string[]? joinPolicies = null,
            bool? includeFullMembersGuild = null
        )
        {
            return new Gs2.Unity.Gs2Guild.Domain.Iterator.EzListGuildsIterator(
                this._domain,
                this._gameSession,
                this._connection,
                guildModelName,
                displayName,
                attributes1,
                attributes2,
                attributes3,
                attributes4,
                attributes5, 
                joinPolicies, 
                includeFullMembersGuild
            );
        }

#if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Guild.Model.EzGuild> SearchGuildsAsync(
            string guildModelName,
            string? displayName = null,
            int[]? attributes1 = null,
            int[]? attributes2 = null,
            int[]? attributes3 = null,
            int[]? attributes4 = null,
            int[]? attributes5 = null,
            string[]? joinPolicies = null,
            bool? includeFullMembersGuild = null
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Guild.Model.EzGuild>(async (writer, token) =>
            {
                var it = _domain.SearchGuildsAsync(
                    guildModelName,
                    displayName,
                    attributes1,
                    attributes2,
                    attributes3,
                    attributes4,
                    attributes5, 
                    joinPolicies, 
                    includeFullMembersGuild
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        this._gameSession,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.SearchGuildsAsync(
                                guildModelName
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Guild.Model.EzGuild.FromModel(it.Current));
                }
            });
        }
#endif

        [Obsolete("The name has been changed to SendRequestFuture.")]
        public IFuture<Gs2.Unity.Gs2Guild.Domain.Model.EzGuildDomain> SendRequest(
            string guildModelName,
            string targetGuildName
        )
        {
            return SendRequestFuture(
                guildModelName,
                targetGuildName
            );
        }

        public IFuture<Gs2.Unity.Gs2Guild.Domain.Model.EzGuildDomain> SendRequestFuture(
            string guildModelName,
            string targetGuildName
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Guild.Domain.Model.EzGuildDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.SendRequestFuture(
                        new SendRequestRequest()
                            .WithGuildModelName(guildModelName)
                            .WithTargetGuildName(targetGuildName)
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
        public async UniTask<Gs2.Unity.Gs2Guild.Domain.Model.EzGuildDomain> SendRequestAsync(
            string guildModelName,
            string targetGuildName
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.SendRequestAsync(
                    new SendRequestRequest()
                        .WithGuildModelName(guildModelName)
                        .WithTargetGuildName(targetGuildName)
                )
            );
            return new Gs2.Unity.Gs2Guild.Domain.Model.EzGuildDomain(
                result,
                this._connection
            );
        }
        #endif

        [Obsolete("The name has been changed to CancelRequestFuture.")]
        public IFuture<Gs2.Unity.Gs2Guild.Domain.Model.EzSendMemberRequestGameSessionDomain> CancelRequest(
            string guildModelName,
            string targetGuildName
        )
        {
            return CancelRequestFuture(
                guildModelName,
                targetGuildName
            );
        }

        public IFuture<Gs2.Unity.Gs2Guild.Domain.Model.EzSendMemberRequestGameSessionDomain> CancelRequestFuture(
            string guildModelName,
            string targetGuildName
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Guild.Domain.Model.EzSendMemberRequestGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.DeleteFuture(
                        new DeleteRequestRequest()
                            .WithGuildModelName(guildModelName)
                            .WithTargetGuildName(targetGuildName)
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Guild.Domain.Model.EzSendMemberRequestGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Guild.Domain.Model.EzSendMemberRequestGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Guild.Domain.Model.EzSendMemberRequestGameSessionDomain> CancelRequestAsync(
            string guildModelName,
            string targetGuildName
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.DeleteAsync(
                    new DeleteRequestRequest()
                        .WithGuildModelName(guildModelName)
                        .WithTargetGuildName(targetGuildName)
                )
            );
            return new Gs2.Unity.Gs2Guild.Domain.Model.EzSendMemberRequestGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        public Gs2Iterator<Gs2.Unity.Gs2Guild.Model.EzSendMemberRequest> SendRequests(
            string guildModelName
        )
        {
            return new Gs2.Unity.Gs2Guild.Domain.Iterator.EzListSendRequestsIterator(
                this._domain,
                this._gameSession,
                this._connection,
                guildModelName
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Guild.Model.EzSendMemberRequest> SendRequestsAsync(
              string guildModelName
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Guild.Model.EzSendMemberRequest>(async (writer, token) =>
            {
                var it = _domain.SendRequestsAsync(
                    guildModelName
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        this._gameSession,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.SendRequestsAsync(
                                guildModelName
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Guild.Model.EzSendMemberRequest.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeSendRequests(
            Action<Gs2.Unity.Gs2Guild.Model.EzSendMemberRequest[]> callback,
            string guildModelName
        ) {
            return this._domain.SubscribeSendRequests(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Guild.Model.EzSendMemberRequest.FromModel).ToArray());
                },
                guildModelName
            );
        }

        public void UnsubscribeSendRequests(
            ulong callbackId,
            string guildModelName
        ) {
            this._domain.UnsubscribeSendRequests(
                callbackId,
                guildModelName
            );
        }

        public Gs2Iterator<Gs2.Unity.Gs2Guild.Model.EzJoinedGuild> JoinedGuilds(
            string? guildModelName = null
        )
        {
            return new Gs2.Unity.Gs2Guild.Domain.Iterator.EzListJoinedGuildsIterator(
                this._domain,
                this._gameSession,
                this._connection,
                guildModelName
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Guild.Model.EzJoinedGuild> JoinedGuildsAsync(
              string? guildModelName = null
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Guild.Model.EzJoinedGuild>(async (writer, token) =>
            {
                var it = _domain.JoinedGuildsAsync(
                    guildModelName
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        this._gameSession,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.JoinedGuildsAsync(
                                guildModelName
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Guild.Model.EzJoinedGuild.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeJoinedGuilds(
            Action<Gs2.Unity.Gs2Guild.Model.EzJoinedGuild[]> callback,
            string? guildModelName = null
        ) {
            return this._domain.SubscribeJoinedGuilds(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Guild.Model.EzJoinedGuild.FromModel).ToArray());
                },
                guildModelName
            );
        }

        public void UnsubscribeJoinedGuilds(
            ulong callbackId,
            string? guildModelName = null
        ) {
            this._domain.UnsubscribeJoinedGuilds(
                callbackId,
                guildModelName
            );
        }

        public Gs2.Unity.Gs2Guild.Domain.Model.EzSendMemberRequestGameSessionDomain SendMemberRequest(
            string guildModelName,
            string guildName
        ) {
            return new Gs2.Unity.Gs2Guild.Domain.Model.EzSendMemberRequestGameSessionDomain(
                _domain.SendMemberRequest(
                    guildModelName,
                    guildName
                ),
                this._gameSession,
                this._connection
            );
        }

        public Gs2.Unity.Gs2Guild.Domain.Model.EzJoinedGuildGameSessionDomain JoinedGuild(
            string guildModelName,
            string guildName
        ) {
            return new Gs2.Unity.Gs2Guild.Domain.Model.EzJoinedGuildGameSessionDomain(
                _domain.JoinedGuild(
                    guildModelName,
                    guildName
                ),
                this._gameSession,
                this._connection
            );
        }

    }
}
