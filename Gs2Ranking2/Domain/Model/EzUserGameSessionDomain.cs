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
using Gs2.Gs2Ranking2.Domain.Iterator;
using Gs2.Gs2Ranking2.Request;
using Gs2.Gs2Ranking2.Result;
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

namespace Gs2.Unity.Gs2Ranking2.Domain.Model
{

    public partial class EzUserGameSessionDomain {
        private readonly Gs2.Gs2Ranking2.Domain.Model.UserAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.IGameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string? NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserGameSessionDomain(
            Gs2.Gs2Ranking2.Domain.Model.UserAccessTokenDomain domain,
            Gs2.Unity.Util.IGameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }

        [Obsolete("The name has been changed to PutGlobalRankingFuture.")]
        public IFuture<Gs2.Unity.Gs2Ranking2.Domain.Model.EzGlobalRankingScoreGameSessionDomain> PutGlobalRanking(
            string rankingName,
            long score,
            string? metadata = null
        )
        {
            return PutGlobalRankingFuture(
                rankingName,
                score,
                metadata
            );
        }

        public IFuture<Gs2.Unity.Gs2Ranking2.Domain.Model.EzGlobalRankingScoreGameSessionDomain> PutGlobalRankingFuture(
            string rankingName,
            long score,
            string? metadata = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Ranking2.Domain.Model.EzGlobalRankingScoreGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.PutGlobalRankingScoreFuture(
                        new PutGlobalRankingScoreRequest()
                            .WithRankingName(rankingName)
                            .WithScore(score)
                            .WithMetadata(metadata)
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Ranking2.Domain.Model.EzGlobalRankingScoreGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Ranking2.Domain.Model.EzGlobalRankingScoreGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Ranking2.Domain.Model.EzGlobalRankingScoreGameSessionDomain> PutGlobalRankingAsync(
            string rankingName,
            long score,
            string? metadata = null
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.PutGlobalRankingScoreAsync(
                    new PutGlobalRankingScoreRequest()
                        .WithRankingName(rankingName)
                        .WithScore(score)
                        .WithMetadata(metadata)
                )
            );
            return new Gs2.Unity.Gs2Ranking2.Domain.Model.EzGlobalRankingScoreGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        [Obsolete("The name has been changed to PutClusterRankingFuture.")]
        public IFuture<Gs2.Unity.Gs2Ranking2.Domain.Model.EzClusterRankingScoreGameSessionDomain> PutClusterRanking(
            string rankingName,
            string clusterName,
            long score,
            string? metadata = null
        )
        {
            return PutClusterRankingFuture(
                rankingName,
                clusterName,
                score,
                metadata
            );
        }

        public IFuture<Gs2.Unity.Gs2Ranking2.Domain.Model.EzClusterRankingScoreGameSessionDomain> PutClusterRankingFuture(
            string rankingName,
            string clusterName,
            long score,
            string? metadata = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Ranking2.Domain.Model.EzClusterRankingScoreGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.PutClusterRankingScoreFuture(
                        new PutClusterRankingScoreRequest()
                            .WithRankingName(rankingName)
                            .WithClusterName(clusterName)
                            .WithScore(score)
                            .WithMetadata(metadata)
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Ranking2.Domain.Model.EzClusterRankingScoreGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Ranking2.Domain.Model.EzClusterRankingScoreGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Ranking2.Domain.Model.EzClusterRankingScoreGameSessionDomain> PutClusterRankingAsync(
            string rankingName,
            string clusterName,
            long score,
            string? metadata = null
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.PutClusterRankingScoreAsync(
                    new PutClusterRankingScoreRequest()
                        .WithRankingName(rankingName)
                        .WithClusterName(clusterName)
                        .WithScore(score)
                        .WithMetadata(metadata)
                )
            );
            return new Gs2.Unity.Gs2Ranking2.Domain.Model.EzClusterRankingScoreGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        [Obsolete("The name has been changed to PutSubscribeRankingFuture.")]
        public IFuture<Gs2.Unity.Gs2Ranking2.Domain.Model.EzSubscribeRankingScoreGameSessionDomain> PutSubscribeRanking(
            string rankingName,
            long score,
            string? metadata = null
        )
        {
            return PutSubscribeRankingFuture(
                rankingName,
                score,
                metadata
            );
        }

        public IFuture<Gs2.Unity.Gs2Ranking2.Domain.Model.EzSubscribeRankingScoreGameSessionDomain> PutSubscribeRankingFuture(
            string rankingName,
            long score,
            string? metadata = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Ranking2.Domain.Model.EzSubscribeRankingScoreGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.PutSubscribeRankingScoreFuture(
                        new PutSubscribeRankingScoreRequest()
                            .WithRankingName(rankingName)
                            .WithScore(score)
                            .WithMetadata(metadata)
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Ranking2.Domain.Model.EzSubscribeRankingScoreGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Ranking2.Domain.Model.EzSubscribeRankingScoreGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Ranking2.Domain.Model.EzSubscribeRankingScoreGameSessionDomain> PutSubscribeRankingAsync(
            string rankingName,
            long score,
            string? metadata = null
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.PutSubscribeRankingScoreAsync(
                    new PutSubscribeRankingScoreRequest()
                        .WithRankingName(rankingName)
                        .WithScore(score)
                        .WithMetadata(metadata)
                )
            );
            return new Gs2.Unity.Gs2Ranking2.Domain.Model.EzSubscribeRankingScoreGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        public Gs2Iterator<Gs2.Unity.Gs2Ranking2.Model.EzGlobalRankingReceivedReward> GlobalRankingReceivedRewards(
            string? rankingName = null,
            long? season = null
        )
        {
            return new Gs2.Unity.Gs2Ranking2.Domain.Iterator.EzListGlobalRankingReceivedRewardsIterator(
                this._domain,
                this._gameSession,
                this._connection,
                rankingName,
                season
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Ranking2.Model.EzGlobalRankingReceivedReward> GlobalRankingReceivedRewardsAsync(
              string? rankingName = null,
              long? season = null
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Ranking2.Model.EzGlobalRankingReceivedReward>(async (writer, token) =>
            {
                var it = _domain.GlobalRankingReceivedRewardsAsync(
                    rankingName,
                    season
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        this._gameSession,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.GlobalRankingReceivedRewardsAsync(
                                rankingName,
                                season
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Ranking2.Model.EzGlobalRankingReceivedReward.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeGlobalRankingReceivedRewards(
            Action<Gs2.Unity.Gs2Ranking2.Model.EzGlobalRankingReceivedReward[]> callback,
            string? rankingName = null,
            long? season = null
        ) {
            return this._domain.SubscribeGlobalRankingReceivedRewards(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Ranking2.Model.EzGlobalRankingReceivedReward.FromModel).ToArray());
                },
                rankingName,
                season
            );
        }

        public void UnsubscribeGlobalRankingReceivedRewards(
            ulong callbackId,
            string? rankingName = null,
            long? season = null
        ) {
            this._domain.UnsubscribeGlobalRankingReceivedRewards(
                callbackId,
                rankingName,
                season
            );
        }

        public Gs2Iterator<Gs2.Unity.Gs2Ranking2.Model.EzGlobalRankingScore> GlobalRankingScores(
            string? rankingName = null
        )
        {
            return new Gs2.Unity.Gs2Ranking2.Domain.Iterator.EzListGlobalRankingScoresIterator(
                this._domain,
                this._gameSession,
                this._connection,
                rankingName
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Ranking2.Model.EzGlobalRankingScore> GlobalRankingScoresAsync(
              string? rankingName = null
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Ranking2.Model.EzGlobalRankingScore>(async (writer, token) =>
            {
                var it = _domain.GlobalRankingScoresAsync(
                    rankingName
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        this._gameSession,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.GlobalRankingScoresAsync(
                                rankingName
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Ranking2.Model.EzGlobalRankingScore.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeGlobalRankingScores(
            Action<Gs2.Unity.Gs2Ranking2.Model.EzGlobalRankingScore[]> callback,
            string? rankingName = null
        ) {
            return this._domain.SubscribeGlobalRankingScores(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Ranking2.Model.EzGlobalRankingScore.FromModel).ToArray());
                },
                rankingName
            );
        }

        public void UnsubscribeGlobalRankingScores(
            ulong callbackId,
            string? rankingName = null
        ) {
            this._domain.UnsubscribeGlobalRankingScores(
                callbackId,
                rankingName
            );
        }

        public Gs2Iterator<Gs2.Unity.Gs2Ranking2.Model.EzClusterRankingReceivedReward> ClusterRankingReceivedRewards(
            string? rankingName = null,
            string? clusterName = null,
            long? season = null
        )
        {
            return new Gs2.Unity.Gs2Ranking2.Domain.Iterator.EzListClusterRankingReceivedRewardsIterator(
                this._domain,
                this._gameSession,
                this._connection,
                rankingName,
                clusterName,
                season
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Ranking2.Model.EzClusterRankingReceivedReward> ClusterRankingReceivedRewardsAsync(
              string? rankingName = null,
              string? clusterName = null,
              long? season = null
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Ranking2.Model.EzClusterRankingReceivedReward>(async (writer, token) =>
            {
                var it = _domain.ClusterRankingReceivedRewardsAsync(
                    rankingName,
                    clusterName,
                    season
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        this._gameSession,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.ClusterRankingReceivedRewardsAsync(
                                rankingName,
                                clusterName,
                                season
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Ranking2.Model.EzClusterRankingReceivedReward.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeClusterRankingReceivedRewards(
            Action<Gs2.Unity.Gs2Ranking2.Model.EzClusterRankingReceivedReward[]> callback,
            string? rankingName = null,
            string? clusterName = null,
            long? season = null
        ) {
            return this._domain.SubscribeClusterRankingReceivedRewards(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Ranking2.Model.EzClusterRankingReceivedReward.FromModel).ToArray());
                },
                rankingName,
                clusterName,
                season
            );
        }

        public void UnsubscribeClusterRankingReceivedRewards(
            ulong callbackId,
            string? rankingName = null,
            string? clusterName = null,
            long? season = null
        ) {
            this._domain.UnsubscribeClusterRankingReceivedRewards(
                callbackId,
                rankingName,
                clusterName,
                season
            );
        }

        public Gs2Iterator<Gs2.Unity.Gs2Ranking2.Model.EzClusterRankingScore> ClusterRankingScores(
            string? rankingName = null,
            string? clusterName = null,
            long? season = null
        )
        {
            return new Gs2.Unity.Gs2Ranking2.Domain.Iterator.EzListClusterRankingScoresIterator(
                this._domain,
                this._gameSession,
                this._connection,
                rankingName,
                clusterName,
                season
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Ranking2.Model.EzClusterRankingScore> ClusterRankingScoresAsync(
              string? rankingName = null,
              string? clusterName = null,
              long? season = null
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Ranking2.Model.EzClusterRankingScore>(async (writer, token) =>
            {
                var it = _domain.ClusterRankingScoresAsync(
                    rankingName,
                    clusterName,
                    season
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        this._gameSession,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.ClusterRankingScoresAsync(
                                rankingName,
                                clusterName,
                                season
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Ranking2.Model.EzClusterRankingScore.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeClusterRankingScores(
            Action<Gs2.Unity.Gs2Ranking2.Model.EzClusterRankingScore[]> callback,
            string? rankingName = null,
            string? clusterName = null,
            long? season = null
        ) {
            return this._domain.SubscribeClusterRankingScores(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Ranking2.Model.EzClusterRankingScore.FromModel).ToArray());
                },
                rankingName,
                clusterName,
                season
            );
        }

        public void UnsubscribeClusterRankingScores(
            ulong callbackId,
            string? rankingName = null,
            string? clusterName = null,
            long? season = null
        ) {
            this._domain.UnsubscribeClusterRankingScores(
                callbackId,
                rankingName,
                clusterName,
                season
            );
        }

        public Gs2Iterator<Gs2.Unity.Gs2Ranking2.Model.EzSubscribeUser> Subscribes(
            string? rankingName = null
        )
        {
            return new Gs2.Unity.Gs2Ranking2.Domain.Iterator.EzListSubscribesIterator(
                this._domain,
                this._gameSession,
                this._connection,
                rankingName
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Ranking2.Model.EzSubscribeUser> SubscribesAsync(
              string? rankingName = null
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Ranking2.Model.EzSubscribeUser>(async (writer, token) =>
            {
                var it = _domain.SubscribesAsync(
                    rankingName
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        this._gameSession,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.SubscribesAsync(
                                rankingName
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Ranking2.Model.EzSubscribeUser.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeSubscribes(
            Action<Gs2.Unity.Gs2Ranking2.Model.EzSubscribeUser[]> callback,
            string? rankingName = null
        ) {
            return this._domain.SubscribeSubscribes(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Ranking2.Model.EzSubscribeUser.FromModel).ToArray());
                },
                rankingName
            );
        }

        public void UnsubscribeSubscribes(
            ulong callbackId,
            string? rankingName = null
        ) {
            this._domain.UnsubscribeSubscribes(
                callbackId,
                rankingName
            );
        }

        public Gs2.Unity.Gs2Ranking2.Domain.Model.EzGlobalRankingScoreGameSessionDomain GlobalRankingScore(
            string rankingName,
            long? season = null
        ) {
            return new Gs2.Unity.Gs2Ranking2.Domain.Model.EzGlobalRankingScoreGameSessionDomain(
                _domain.GlobalRankingScore(
                    rankingName,
                    season
                ),
                this._gameSession,
                this._connection
            );
        }

        public Gs2.Unity.Gs2Ranking2.Domain.Model.EzSubscribeRankingSeasonGameSessionDomain SubscribeRankingSeason(
            string rankingName,
            long? season = null
        ) {
            return new Gs2.Unity.Gs2Ranking2.Domain.Model.EzSubscribeRankingSeasonGameSessionDomain(
                _domain.SubscribeRankingSeason(
                    rankingName,
                    season
                ),
                this._gameSession,
                this._connection
            );
        }

        public Gs2.Unity.Gs2Ranking2.Domain.Model.EzSubscribeGameSessionDomain Subscribe(
            string rankingName
        ) {
            return new Gs2.Unity.Gs2Ranking2.Domain.Model.EzSubscribeGameSessionDomain(
                _domain.Subscribe(
                    rankingName
                ),
                this._gameSession,
                this._connection
            );
        }

        public Gs2.Unity.Gs2Ranking2.Domain.Model.EzGlobalRankingReceivedRewardGameSessionDomain GlobalRankingReceivedReward(
            string rankingName,
            long? season = null
        ) {
            return new Gs2.Unity.Gs2Ranking2.Domain.Model.EzGlobalRankingReceivedRewardGameSessionDomain(
                _domain.GlobalRankingReceivedReward(
                    rankingName,
                    season
                ),
                this._gameSession,
                this._connection
            );
        }

        public Gs2.Unity.Gs2Ranking2.Domain.Model.EzClusterRankingReceivedRewardGameSessionDomain ClusterRankingReceivedReward(
            string rankingName,
            string clusterName,
            long? season = null
        ) {
            return new Gs2.Unity.Gs2Ranking2.Domain.Model.EzClusterRankingReceivedRewardGameSessionDomain(
                _domain.ClusterRankingReceivedReward(
                    rankingName,
                    clusterName,
                    season
                ),
                this._gameSession,
                this._connection
            );
        }

        public Gs2.Unity.Gs2Ranking2.Domain.Model.EzClusterRankingScoreGameSessionDomain ClusterRankingScore(
            string rankingName,
            string clusterName,
            long? season = null
        ) {
            return new Gs2.Unity.Gs2Ranking2.Domain.Model.EzClusterRankingScoreGameSessionDomain(
                _domain.ClusterRankingScore(
                    rankingName,
                    clusterName,
                    season
                ),
                this._gameSession,
                this._connection
            );
        }

    }
}
