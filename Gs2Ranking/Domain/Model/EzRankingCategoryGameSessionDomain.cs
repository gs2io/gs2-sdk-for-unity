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
using Gs2.Gs2Ranking.Domain.Iterator;
using Gs2.Gs2Ranking.Request;
using Gs2.Gs2Ranking.Result;
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

namespace Gs2.Unity.Gs2Ranking.Domain.Model
{

    public partial class EzRankingCategoryGameSessionDomain {
        private readonly Gs2.Gs2Ranking.Domain.Model.RankingCategoryAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.IGameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string? NextPageToken => _domain.NextPageToken;
        public bool? Processing => _domain.Processing;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;
        public string CategoryName => _domain?.CategoryName;
        public string AdditionalScopeName => _domain?.AdditionalScopeName;

        public EzRankingCategoryGameSessionDomain(
            Gs2.Gs2Ranking.Domain.Model.RankingCategoryAccessTokenDomain domain,
            Gs2.Unity.Util.IGameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }

        [Obsolete("The name has been changed to SubscribeFuture.")]
        public IFuture<Gs2.Unity.Gs2Ranking.Domain.Model.EzSubscribeUserGameSessionDomain> Subscribe(
            string targetUserId
        )
        {
            return SubscribeFuture(
                targetUserId
            );
        }

        public IFuture<Gs2.Unity.Gs2Ranking.Domain.Model.EzSubscribeUserGameSessionDomain> SubscribeFuture(
            string targetUserId
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Ranking.Domain.Model.EzSubscribeUserGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.SubscribeFuture(
                        new SubscribeRequest()
                            .WithTargetUserId(targetUserId)
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Ranking.Domain.Model.EzSubscribeUserGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Ranking.Domain.Model.EzSubscribeUserGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Ranking.Domain.Model.EzSubscribeUserGameSessionDomain> SubscribeAsync(
            string targetUserId
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.SubscribeAsync(
                    new SubscribeRequest()
                        .WithTargetUserId(targetUserId)
                )
            );
            return new Gs2.Unity.Gs2Ranking.Domain.Model.EzSubscribeUserGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        [Obsolete("The name has been changed to PutScoreFuture.")]
        public IFuture<Gs2.Unity.Gs2Ranking.Domain.Model.EzScoreGameSessionDomain> PutScore(
            long score,
            string? metadata = null
        )
        {
            return PutScoreFuture(
                score,
                metadata
            );
        }

        public IFuture<Gs2.Unity.Gs2Ranking.Domain.Model.EzScoreGameSessionDomain> PutScoreFuture(
            long score,
            string? metadata = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Ranking.Domain.Model.EzScoreGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.PutScoreFuture(
                        new PutScoreRequest()
                            .WithScore(score)
                            .WithMetadata(metadata)
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Ranking.Domain.Model.EzScoreGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Ranking.Domain.Model.EzScoreGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Ranking.Domain.Model.EzScoreGameSessionDomain> PutScoreAsync(
            long score,
            string? metadata = null
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.PutScoreAsync(
                    new PutScoreRequest()
                        .WithScore(score)
                        .WithMetadata(metadata)
                )
            );
            return new Gs2.Unity.Gs2Ranking.Domain.Model.EzScoreGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        public Gs2Iterator<Gs2.Unity.Gs2Ranking.Model.EzSubscribeUser> SubscribeUsers(
        )
        {
            return new Gs2.Unity.Gs2Ranking.Domain.Iterator.EzListSubscribesIterator(
                this._domain,
                this._gameSession,
                this._connection
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Ranking.Model.EzSubscribeUser> SubscribeUsersAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Ranking.Model.EzSubscribeUser>(async (writer, token) =>
            {
                var it = _domain.SubscribeUsersAsync(
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        this._gameSession,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.SubscribeUsersAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Ranking.Model.EzSubscribeUser.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeSubscribeUsers(
            Action<Gs2.Unity.Gs2Ranking.Model.EzSubscribeUser[]> callback
        ) {
            return this._domain.SubscribeSubscribeUsers(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Ranking.Model.EzSubscribeUser.FromModel).ToArray());
                }
            );
        }

        public void UnsubscribeSubscribeUsers(
            ulong callbackId
        ) {
            this._domain.UnsubscribeSubscribeUsers(
                callbackId
            );
        }

        public Gs2Iterator<Gs2.Unity.Gs2Ranking.Model.EzRanking> Rankings(
        )
        {
            return new Gs2.Unity.Gs2Ranking.Domain.Iterator.EzGetRankingIterator(
                this._domain,
                this._gameSession,
                this._connection
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Ranking.Model.EzRanking> RankingsAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Ranking.Model.EzRanking>(async (writer, token) =>
            {
                var it = _domain.RankingsAsync(
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        this._gameSession,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.RankingsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Ranking.Model.EzRanking.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeRankings(
            Action<Gs2.Unity.Gs2Ranking.Model.EzRanking[]> callback
        ) {
            return this._domain.SubscribeRankings(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Ranking.Model.EzRanking.FromModel).ToArray());
                }
            );
        }

        public void UnsubscribeRankings(
            ulong callbackId
        ) {
            this._domain.UnsubscribeRankings(
                callbackId
            );
        }

        public Gs2.Unity.Gs2Ranking.Domain.Model.EzSubscribeUserGameSessionDomain SubscribeUser(
            string targetUserId
        ) {
            return new Gs2.Unity.Gs2Ranking.Domain.Model.EzSubscribeUserGameSessionDomain(
                _domain.SubscribeUser(
                    targetUserId
                ),
                this._gameSession,
                this._connection
            );
        }

        public Gs2.Unity.Gs2Ranking.Domain.Model.EzRankingGameSessionDomain Ranking(
            string scorerUserId = null,
            long? index = null
        ) {
            return new Gs2.Unity.Gs2Ranking.Domain.Model.EzRankingGameSessionDomain(
                _domain.Ranking(
                    scorerUserId,
                    index
                ),
                this._gameSession,
                this._connection
            );
        }

    }
}
