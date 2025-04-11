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

    public partial class EzClusterRankingSeasonGameSessionDomain {
        private readonly Gs2.Gs2Ranking2.Domain.Model.ClusterRankingSeasonAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.IGameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string? NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string RankingName => _domain?.RankingName;
        public string ClusterName => _domain?.ClusterName;
        public long? Season => _domain?.Season;
        public string UserId => _domain?.UserId;

        public EzClusterRankingSeasonGameSessionDomain(
            Gs2.Gs2Ranking2.Domain.Model.ClusterRankingSeasonAccessTokenDomain domain,
            Gs2.Unity.Util.IGameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }

        [Obsolete("The name has been changed to PutClusterRankingFuture.")]
        public IFuture<Gs2.Unity.Gs2Ranking2.Domain.Model.EzClusterRankingScoreGameSessionDomain> PutClusterRanking(
            long score,
            string? metadata = null
        )
        {
            return PutClusterRankingFuture(
                score,
                metadata
            );
        }

        public IFuture<Gs2.Unity.Gs2Ranking2.Domain.Model.EzClusterRankingScoreGameSessionDomain> PutClusterRankingFuture(
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
            long score,
            string? metadata = null
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.PutClusterRankingScoreAsync(
                    new PutClusterRankingScoreRequest()
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

        public Gs2Iterator<Gs2.Unity.Gs2Ranking2.Model.EzClusterRankingData> ClusterRankings(
        )
        {
            return new Gs2.Unity.Gs2Ranking2.Domain.Iterator.EzListClusterRankingsIterator(
                this._domain,
                this._gameSession,
                this._connection
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Ranking2.Model.EzClusterRankingData> ClusterRankingsAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Ranking2.Model.EzClusterRankingData>(async (writer, token) =>
            {
                var it = _domain.ClusterRankingsAsync(
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        this._gameSession,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.ClusterRankingsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Ranking2.Model.EzClusterRankingData.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeClusterRankings(
            Action<Gs2.Unity.Gs2Ranking2.Model.EzClusterRankingData[]> callback
        ) {
            return this._domain.SubscribeClusterRankings(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Ranking2.Model.EzClusterRankingData.FromModel).ToArray());
                }
            );
        }

        public void UnsubscribeClusterRankings(
            ulong callbackId
        ) {
            this._domain.UnsubscribeClusterRankings(
                callbackId
            );
        }

        public void InvalidateClusterRankings(
        ) {
            this._domain.InvalidateClusterRankings(
            );
        }

        public Gs2Iterator<Gs2.Unity.Gs2Ranking2.Model.EzClusterRankingReceivedReward> ClusterRankingReceivedRewards(
        )
        {
            return new Gs2.Unity.Gs2Ranking2.Domain.Iterator.EzListClusterRankingReceivedRewardsIterator(
                this._domain,
                this._gameSession,
                this._connection
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Ranking2.Model.EzClusterRankingReceivedReward> ClusterRankingReceivedRewardsAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Ranking2.Model.EzClusterRankingReceivedReward>(async (writer, token) =>
            {
                var it = _domain.ClusterRankingReceivedRewardsAsync(
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
            Action<Gs2.Unity.Gs2Ranking2.Model.EzClusterRankingReceivedReward[]> callback
        ) {
            return this._domain.SubscribeClusterRankingReceivedRewards(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Ranking2.Model.EzClusterRankingReceivedReward.FromModel).ToArray());
                }
            );
        }

        public void UnsubscribeClusterRankingReceivedRewards(
            ulong callbackId
        ) {
            this._domain.UnsubscribeClusterRankingReceivedRewards(
                callbackId
            );
        }

        public void InvalidateClusterRankingReceivedRewards(
        ) {
            this._domain.InvalidateClusterRankingReceivedRewards(
            );
        }

        public Gs2Iterator<Gs2.Unity.Gs2Ranking2.Model.EzClusterRankingScore> ClusterRankingScores(
        )
        {
            return new Gs2.Unity.Gs2Ranking2.Domain.Iterator.EzListClusterRankingScoresIterator(
                this._domain,
                this._gameSession,
                this._connection
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Ranking2.Model.EzClusterRankingScore> ClusterRankingScoresAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Ranking2.Model.EzClusterRankingScore>(async (writer, token) =>
            {
                var it = _domain.ClusterRankingScoresAsync(
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
            Action<Gs2.Unity.Gs2Ranking2.Model.EzClusterRankingScore[]> callback
        ) {
            return this._domain.SubscribeClusterRankingScores(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Ranking2.Model.EzClusterRankingScore.FromModel).ToArray());
                }
            );
        }

        public void UnsubscribeClusterRankingScores(
            ulong callbackId
        ) {
            this._domain.UnsubscribeClusterRankingScores(
                callbackId
            );
        }

        public void InvalidateClusterRankingScores(
        ) {
            this._domain.InvalidateClusterRankingScores(
            );
        }

        public Gs2.Unity.Gs2Ranking2.Domain.Model.EzClusterRankingDataGameSessionDomain ClusterRankingData(
        ) {
            return new Gs2.Unity.Gs2Ranking2.Domain.Model.EzClusterRankingDataGameSessionDomain(
                _domain.ClusterRankingData(
                ),
                this._gameSession,
                this._connection
            );
        }

        public Gs2.Unity.Gs2Ranking2.Domain.Model.EzClusterRankingReceivedRewardGameSessionDomain ClusterRankingReceivedReward(
        ) {
            return new Gs2.Unity.Gs2Ranking2.Domain.Model.EzClusterRankingReceivedRewardGameSessionDomain(
                _domain.ClusterRankingReceivedReward(
                ),
                this._gameSession,
                this._connection
            );
        }

        public Gs2.Unity.Gs2Ranking2.Domain.Model.EzClusterRankingScoreGameSessionDomain ClusterRankingScore(
        ) {
            return new Gs2.Unity.Gs2Ranking2.Domain.Model.EzClusterRankingScoreGameSessionDomain(
                _domain.ClusterRankingScore(
                ),
                this._gameSession,
                this._connection
            );
        }

    }
}
