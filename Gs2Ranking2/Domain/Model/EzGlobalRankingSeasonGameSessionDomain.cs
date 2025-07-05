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

    public partial class EzGlobalRankingSeasonGameSessionDomain {
        private readonly Gs2.Gs2Ranking2.Domain.Model.GlobalRankingSeasonAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.IGameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string? NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string RankingName => _domain?.RankingName;
        public long? Season => _domain?.Season;
        public string UserId => _domain?.UserId;

        public EzGlobalRankingSeasonGameSessionDomain(
            Gs2.Gs2Ranking2.Domain.Model.GlobalRankingSeasonAccessTokenDomain domain,
            Gs2.Unity.Util.IGameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }

        [Obsolete("The name has been changed to PutGlobalRankingFuture.")]
        public IFuture<Gs2.Unity.Gs2Ranking2.Domain.Model.EzGlobalRankingScoreGameSessionDomain> PutGlobalRanking(
            long score,
            string? metadata = null
        )
        {
            return PutGlobalRankingFuture(
                score,
                metadata
            );
        }

        public IFuture<Gs2.Unity.Gs2Ranking2.Domain.Model.EzGlobalRankingScoreGameSessionDomain> PutGlobalRankingFuture(
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
            long score,
            string? metadata = null
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.PutGlobalRankingScoreAsync(
                    new PutGlobalRankingScoreRequest()
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

        [Obsolete("The name has been changed to GetGlobalRankingRankFuture.")]
        public IFuture<Gs2.Unity.Gs2Ranking2.Domain.Model.EzGlobalRankingDataGameSessionDomain> GetGlobalRankingRank(
        )
        {
            return GetGlobalRankingRankFuture(
            );
        }

        public IFuture<Gs2.Unity.Gs2Ranking2.Domain.Model.EzGlobalRankingDataGameSessionDomain> GetGlobalRankingRankFuture(
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Ranking2.Domain.Model.EzGlobalRankingDataGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.GetGlobalRankingFuture(
                        new GetGlobalRankingRequest()
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Ranking2.Domain.Model.EzGlobalRankingDataGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Ranking2.Domain.Model.EzGlobalRankingDataGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Ranking2.Domain.Model.EzGlobalRankingDataGameSessionDomain> GetGlobalRankingRankAsync(
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.GetGlobalRankingAsync(
                    new GetGlobalRankingRequest()
                )
            );
            return new Gs2.Unity.Gs2Ranking2.Domain.Model.EzGlobalRankingDataGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        public Gs2Iterator<Gs2.Unity.Gs2Ranking2.Model.EzGlobalRankingData> GlobalRankings(
        )
        {
            return new Gs2.Unity.Gs2Ranking2.Domain.Iterator.EzListGlobalRankingsIterator(
                this._domain,
                this._gameSession,
                this._connection
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Ranking2.Model.EzGlobalRankingData> GlobalRankingsAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Ranking2.Model.EzGlobalRankingData>(async (writer, token) =>
            {
                var it = _domain.GlobalRankingsAsync(
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        this._gameSession,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.GlobalRankingsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Ranking2.Model.EzGlobalRankingData.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeGlobalRankings(
            Action<Gs2.Unity.Gs2Ranking2.Model.EzGlobalRankingData[]> callback
        ) {
            return this._domain.SubscribeGlobalRankings(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Ranking2.Model.EzGlobalRankingData.FromModel).ToArray());
                }
            );
        }

        public void UnsubscribeGlobalRankings(
            ulong callbackId
        ) {
            this._domain.UnsubscribeGlobalRankings(
                callbackId
            );
        }

        public void InvalidateGlobalRankings(
        ) {
            this._domain.InvalidateGlobalRankings(
            );
        }

        public Gs2Iterator<Gs2.Unity.Gs2Ranking2.Model.EzGlobalRankingReceivedReward> GlobalRankingReceivedRewards(
        )
        {
            return new Gs2.Unity.Gs2Ranking2.Domain.Iterator.EzListGlobalRankingReceivedRewardsIterator(
                this._domain,
                this._gameSession,
                this._connection
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Ranking2.Model.EzGlobalRankingReceivedReward> GlobalRankingReceivedRewardsAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Ranking2.Model.EzGlobalRankingReceivedReward>(async (writer, token) =>
            {
                var it = _domain.GlobalRankingReceivedRewardsAsync(
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
            Action<Gs2.Unity.Gs2Ranking2.Model.EzGlobalRankingReceivedReward[]> callback
        ) {
            return this._domain.SubscribeGlobalRankingReceivedRewards(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Ranking2.Model.EzGlobalRankingReceivedReward.FromModel).ToArray());
                }
            );
        }

        public void UnsubscribeGlobalRankingReceivedRewards(
            ulong callbackId
        ) {
            this._domain.UnsubscribeGlobalRankingReceivedRewards(
                callbackId
            );
        }

        public void InvalidateGlobalRankingReceivedRewards(
        ) {
            this._domain.InvalidateGlobalRankingReceivedRewards(
            );
        }

        public Gs2Iterator<Gs2.Unity.Gs2Ranking2.Model.EzGlobalRankingScore> GlobalRankingScores(
        )
        {
            return new Gs2.Unity.Gs2Ranking2.Domain.Iterator.EzListGlobalRankingScoresIterator(
                this._domain,
                this._gameSession,
                this._connection
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Ranking2.Model.EzGlobalRankingScore> GlobalRankingScoresAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Ranking2.Model.EzGlobalRankingScore>(async (writer, token) =>
            {
                var it = _domain.GlobalRankingScoresAsync(
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
            Action<Gs2.Unity.Gs2Ranking2.Model.EzGlobalRankingScore[]> callback
        ) {
            return this._domain.SubscribeGlobalRankingScores(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Ranking2.Model.EzGlobalRankingScore.FromModel).ToArray());
                }
            );
        }

        public void UnsubscribeGlobalRankingScores(
            ulong callbackId
        ) {
            this._domain.UnsubscribeGlobalRankingScores(
                callbackId
            );
        }

        public void InvalidateGlobalRankingScores(
        ) {
            this._domain.InvalidateGlobalRankingScores(
            );
        }

        public Gs2.Unity.Gs2Ranking2.Domain.Model.EzGlobalRankingScoreGameSessionDomain GlobalRankingScore(
        ) {
            return new Gs2.Unity.Gs2Ranking2.Domain.Model.EzGlobalRankingScoreGameSessionDomain(
                _domain.GlobalRankingScore(
                ),
                this._gameSession,
                this._connection
            );
        }

        public Gs2.Unity.Gs2Ranking2.Domain.Model.EzGlobalRankingDataGameSessionDomain GlobalRankingData(
            string scorerUserId
        ) {
            return new Gs2.Unity.Gs2Ranking2.Domain.Model.EzGlobalRankingDataGameSessionDomain(
                _domain.GlobalRankingData(
                    scorerUserId
                ),
                this._gameSession,
                this._connection
            );
        }

        public Gs2.Unity.Gs2Ranking2.Domain.Model.EzGlobalRankingReceivedRewardGameSessionDomain GlobalRankingReceivedReward(
        ) {
            return new Gs2.Unity.Gs2Ranking2.Domain.Model.EzGlobalRankingReceivedRewardGameSessionDomain(
                _domain.GlobalRankingReceivedReward(
                ),
                this._gameSession,
                this._connection
            );
        }

    }
}
