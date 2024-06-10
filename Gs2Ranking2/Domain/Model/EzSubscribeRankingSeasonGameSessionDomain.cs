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

    public partial class EzSubscribeRankingSeasonGameSessionDomain {
        private readonly Gs2.Gs2Ranking2.Domain.Model.SubscribeRankingSeasonAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.IGameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string? NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;
        public string RankingName => _domain?.RankingName;
        public long? Season => _domain?.Season;

        public EzSubscribeRankingSeasonGameSessionDomain(
            Gs2.Gs2Ranking2.Domain.Model.SubscribeRankingSeasonAccessTokenDomain domain,
            Gs2.Unity.Util.IGameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }

        [Obsolete("The name has been changed to PutSubscribeRankingFuture.")]
        public IFuture<Gs2.Unity.Gs2Ranking2.Domain.Model.EzSubscribeRankingScoreGameSessionDomain> PutSubscribeRanking(
            long score,
            string? metadata = null
        )
        {
            return PutSubscribeRankingFuture(
                score,
                metadata
            );
        }

        public IFuture<Gs2.Unity.Gs2Ranking2.Domain.Model.EzSubscribeRankingScoreGameSessionDomain> PutSubscribeRankingFuture(
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
            long score,
            string? metadata = null
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.PutSubscribeRankingScoreAsync(
                    new PutSubscribeRankingScoreRequest()
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

        public Gs2Iterator<Gs2.Unity.Gs2Ranking2.Model.EzSubscribeRankingData> SubscribeRankings(
        )
        {
            return new Gs2.Unity.Gs2Ranking2.Domain.Iterator.EzListSubscribeRankingsIterator(
                this._domain,
                this._gameSession,
                this._connection
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Ranking2.Model.EzSubscribeRankingData> SubscribeRankingsAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Ranking2.Model.EzSubscribeRankingData>(async (writer, token) =>
            {
                var it = _domain.SubscribeRankingsAsync(
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        this._gameSession,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.SubscribeRankingsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Ranking2.Model.EzSubscribeRankingData.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeSubscribeRankings(
            Action<Gs2.Unity.Gs2Ranking2.Model.EzSubscribeRankingData[]> callback
        ) {
            return this._domain.SubscribeSubscribeRankings(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Ranking2.Model.EzSubscribeRankingData.FromModel).ToArray());
                }
            );
        }

        public void UnsubscribeSubscribeRankings(
            ulong callbackId
        ) {
            this._domain.UnsubscribeSubscribeRankings(
                callbackId
            );
        }

        public Gs2Iterator<Gs2.Unity.Gs2Ranking2.Model.EzSubscribeRankingScore> SubscribeRankingScores(
        )
        {
            return new Gs2.Unity.Gs2Ranking2.Domain.Iterator.EzListSubscribeRankingScoresIterator(
                this._domain,
                this._gameSession,
                this._connection
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Ranking2.Model.EzSubscribeRankingScore> SubscribeRankingScoresAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Ranking2.Model.EzSubscribeRankingScore>(async (writer, token) =>
            {
                var it = _domain.SubscribeRankingScoresAsync(
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        this._gameSession,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.SubscribeRankingScoresAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Ranking2.Model.EzSubscribeRankingScore.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeSubscribeRankingScores(
            Action<Gs2.Unity.Gs2Ranking2.Model.EzSubscribeRankingScore[]> callback
        ) {
            return this._domain.SubscribeSubscribeRankingScores(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Ranking2.Model.EzSubscribeRankingScore.FromModel).ToArray());
                }
            );
        }

        public void UnsubscribeSubscribeRankingScores(
            ulong callbackId
        ) {
            this._domain.UnsubscribeSubscribeRankingScores(
                callbackId
            );
        }

        public Gs2.Unity.Gs2Ranking2.Domain.Model.EzSubscribeRankingScoreGameSessionDomain SubscribeRankingScore(
        ) {
            return new Gs2.Unity.Gs2Ranking2.Domain.Model.EzSubscribeRankingScoreGameSessionDomain(
                _domain.SubscribeRankingScore(
                ),
                this._gameSession,
                this._connection
            );
        }

        public Gs2.Unity.Gs2Ranking2.Domain.Model.EzSubscribeRankingDataGameSessionDomain SubscribeRankingData(
            string scorerUserId
        ) {
            return new Gs2.Unity.Gs2Ranking2.Domain.Model.EzSubscribeRankingDataGameSessionDomain(
                _domain.SubscribeRankingData(
                    scorerUserId
                ),
                this._gameSession,
                this._connection
            );
        }

    }
}
