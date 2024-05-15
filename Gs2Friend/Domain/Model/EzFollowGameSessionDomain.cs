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

    public partial class EzFollowGameSessionDomain {
        private readonly Gs2.Gs2Friend.Domain.Model.FollowAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.IGameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string? NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;
        public bool? WithProfile => _domain?.WithProfile;

        public EzFollowGameSessionDomain(
            Gs2.Gs2Friend.Domain.Model.FollowAccessTokenDomain domain,
            Gs2.Unity.Util.IGameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }

        [Obsolete("The name has been changed to FollowFuture.")]
        public IFuture<Gs2.Unity.Gs2Friend.Domain.Model.EzFollowUserGameSessionDomain> Follow(
            string targetUserId
        )
        {
            return FollowFuture(
                targetUserId
            );
        }

        public IFuture<Gs2.Unity.Gs2Friend.Domain.Model.EzFollowUserGameSessionDomain> FollowFuture(
            string targetUserId
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Friend.Domain.Model.EzFollowUserGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.FollowFuture(
                        new FollowRequest()
                            .WithTargetUserId(targetUserId)
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Friend.Domain.Model.EzFollowUserGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Friend.Domain.Model.EzFollowUserGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Friend.Domain.Model.EzFollowUserGameSessionDomain> FollowAsync(
            string targetUserId
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.FollowAsync(
                    new FollowRequest()
                        .WithTargetUserId(targetUserId)
                )
            );
            return new Gs2.Unity.Gs2Friend.Domain.Model.EzFollowUserGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        public Gs2Iterator<Gs2.Unity.Gs2Friend.Model.EzFollowUser> Follows(
        )
        {
            return new Gs2.Unity.Gs2Friend.Domain.Iterator.EzDescribeFollowUsersIterator(
                this._domain,
                this._gameSession,
                this._connection
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Friend.Model.EzFollowUser> FollowsAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Friend.Model.EzFollowUser>(async (writer, token) =>
            {
                var it = _domain.FollowsAsync(
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        this._gameSession,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.FollowsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Friend.Model.EzFollowUser.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeFollows(
            Action<Gs2.Unity.Gs2Friend.Model.EzFollowUser[]> callback
        ) {
            return this._domain.SubscribeFollows(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Friend.Model.EzFollowUser.FromModel).ToArray());
                }
            );
        }

        public void UnsubscribeFollows(
            ulong callbackId
        ) {
            this._domain.UnsubscribeFollows(
                callbackId
            );
        }

        public Gs2.Unity.Gs2Friend.Domain.Model.EzFollowUserGameSessionDomain FollowUser(
            string targetUserId
        ) {
            return new Gs2.Unity.Gs2Friend.Domain.Model.EzFollowUserGameSessionDomain(
                _domain.FollowUser(
                    targetUserId
                ),
                this._gameSession,
                this._connection
            );
        }

    }
}
