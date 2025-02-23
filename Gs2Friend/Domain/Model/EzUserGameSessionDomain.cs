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
using Gs2.Gs2Friend.Domain.Iterator;
using Gs2.Gs2Friend.Request;
using Gs2.Gs2Friend.Result;
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

namespace Gs2.Unity.Gs2Friend.Domain.Model
{

    public partial class EzUserGameSessionDomain {
        private readonly Gs2.Gs2Friend.Domain.Model.UserAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.IGameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string? NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserGameSessionDomain(
            Gs2.Gs2Friend.Domain.Model.UserAccessTokenDomain domain,
            Gs2.Unity.Util.IGameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }

        [Obsolete("The name has been changed to SendRequestFuture.")]
        public IFuture<Gs2.Unity.Gs2Friend.Domain.Model.EzSendFriendRequestGameSessionDomain> SendRequest(
            string targetUserId
        )
        {
            return SendRequestFuture(
                targetUserId
            );
        }

        public IFuture<Gs2.Unity.Gs2Friend.Domain.Model.EzSendFriendRequestGameSessionDomain> SendRequestFuture(
            string targetUserId
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Friend.Domain.Model.EzSendFriendRequestGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.SendRequestFuture(
                        new SendRequestRequest()
                            .WithTargetUserId(targetUserId)
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Friend.Domain.Model.EzSendFriendRequestGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Friend.Domain.Model.EzSendFriendRequestGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Friend.Domain.Model.EzSendFriendRequestGameSessionDomain> SendRequestAsync(
            string targetUserId
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.SendRequestAsync(
                    new SendRequestRequest()
                        .WithTargetUserId(targetUserId)
                )
            );
            return new Gs2.Unity.Gs2Friend.Domain.Model.EzSendFriendRequestGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        public Gs2Iterator<Gs2.Unity.Gs2Friend.Model.EzFriendUser> Friends(
            bool? withProfile = null
        )
        {
            return new Gs2.Unity.Gs2Friend.Domain.Iterator.EzDescribeFriendsIterator(
                this._domain,
                this._gameSession,
                this._connection,
                withProfile
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Friend.Model.EzFriendUser> FriendsAsync(
              bool? withProfile = null
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Friend.Model.EzFriendUser>(async (writer, token) =>
            {
                var it = _domain.FriendsAsync(
                    withProfile
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        this._gameSession,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.FriendsAsync(
                                withProfile
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Friend.Model.EzFriendUser.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeFriends(
            Action<Gs2.Unity.Gs2Friend.Model.EzFriendUser[]> callback,
            bool? withProfile = null
        ) {
            return this._domain.SubscribeFriends(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Friend.Model.EzFriendUser.FromModel).ToArray());
                },
                withProfile
            );
        }

        public void UnsubscribeFriends(
            ulong callbackId,
            bool? withProfile = null
        ) {
            this._domain.UnsubscribeFriends(
                callbackId,
                withProfile
            );
        }

        public void InvalidateFriends(
            bool? withProfile = null
        ) {
            this._domain.InvalidateFriends(
                withProfile
            );
        }

        public Gs2Iterator<Gs2.Unity.Gs2Friend.Model.EzFriendRequest> SendRequests(
        )
        {
            return new Gs2.Unity.Gs2Friend.Domain.Iterator.EzDescribeSendRequestsIterator(
                this._domain,
                this._gameSession,
                this._connection
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Friend.Model.EzFriendRequest> SendRequestsAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Friend.Model.EzFriendRequest>(async (writer, token) =>
            {
                var it = _domain.SendRequestsAsync(
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
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Friend.Model.EzFriendRequest.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeSendRequests(
            Action<Gs2.Unity.Gs2Friend.Model.EzFriendRequest[]> callback
        ) {
            return this._domain.SubscribeSendRequests(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Friend.Model.EzFriendRequest.FromModel).ToArray());
                }
            );
        }

        public void UnsubscribeSendRequests(
            ulong callbackId
        ) {
            this._domain.UnsubscribeSendRequests(
                callbackId
            );
        }

        public void InvalidateSendRequests(
        ) {
            this._domain.InvalidateSendRequests(
            );
        }

        public Gs2Iterator<Gs2.Unity.Gs2Friend.Model.EzFriendRequest> ReceiveRequests(
        )
        {
            return new Gs2.Unity.Gs2Friend.Domain.Iterator.EzDescribeReceiveRequestsIterator(
                this._domain,
                this._gameSession,
                this._connection
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Friend.Model.EzFriendRequest> ReceiveRequestsAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Friend.Model.EzFriendRequest>(async (writer, token) =>
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
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Friend.Model.EzFriendRequest.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeReceiveRequests(
            Action<Gs2.Unity.Gs2Friend.Model.EzFriendRequest[]> callback
        ) {
            return this._domain.SubscribeReceiveRequests(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Friend.Model.EzFriendRequest.FromModel).ToArray());
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

        public Gs2Iterator<string> BlackListUsers(
        ) {
            return new Gs2.Unity.Gs2Friend.Domain.Iterator.EzBlackListUsersIterator(
                this._domain,
                this._gameSession,
                this._connection
            );
        }
        
#if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<string> BlackListUsersAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<string>(async (writer, token) =>
            {
                var it = _domain.BlackListUsersAsync(
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        this._gameSession,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.BlackListUsersAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : it.Current);
                }
            });
        }
#endif

        public void InvalidateReceiveRequests(
        ) {
            this._domain.InvalidateReceiveRequests(
            );
        }

        public Gs2.Unity.Gs2Friend.Domain.Model.EzProfileGameSessionDomain Profile(
        ) {
            return new Gs2.Unity.Gs2Friend.Domain.Model.EzProfileGameSessionDomain(
                _domain.Profile(
                ),
                this._gameSession,
                this._connection
            );
        }

        public Gs2.Unity.Gs2Friend.Domain.Model.EzBlackListGameSessionDomain BlackList(
        ) {
            return new Gs2.Unity.Gs2Friend.Domain.Model.EzBlackListGameSessionDomain(
                _domain.BlackList(
                ),
                this._gameSession,
                this._connection
            );
        }

        public Gs2.Unity.Gs2Friend.Domain.Model.EzFollowGameSessionDomain Follow(
            bool withProfile
        ) {
            return new Gs2.Unity.Gs2Friend.Domain.Model.EzFollowGameSessionDomain(
                _domain.Follow(
                    withProfile
                ),
                this._gameSession,
                this._connection
            );
        }

        public Gs2.Unity.Gs2Friend.Domain.Model.EzFriendGameSessionDomain Friend(
            bool withProfile
        ) {
            return new Gs2.Unity.Gs2Friend.Domain.Model.EzFriendGameSessionDomain(
                _domain.Friend(
                    withProfile
                ),
                this._gameSession,
                this._connection
            );
        }

        public Gs2.Unity.Gs2Friend.Domain.Model.EzSendFriendRequestGameSessionDomain SendFriendRequest(
            string targetUserId
        ) {
            return new Gs2.Unity.Gs2Friend.Domain.Model.EzSendFriendRequestGameSessionDomain(
                _domain.SendFriendRequest(
                    targetUserId
                ),
                this._gameSession,
                this._connection
            );
        }

        public Gs2.Unity.Gs2Friend.Domain.Model.EzReceiveFriendRequestGameSessionDomain ReceiveFriendRequest(
            string fromUserId
        ) {
            return new Gs2.Unity.Gs2Friend.Domain.Model.EzReceiveFriendRequestGameSessionDomain(
                _domain.ReceiveFriendRequest(
                    fromUserId
                ),
                this._gameSession,
                this._connection
            );
        }

    }
}
