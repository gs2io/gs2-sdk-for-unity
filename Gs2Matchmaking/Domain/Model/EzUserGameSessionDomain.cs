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
using Gs2.Gs2Matchmaking.Domain.Iterator;
using Gs2.Gs2Matchmaking.Request;
using Gs2.Gs2Matchmaking.Result;
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

namespace Gs2.Unity.Gs2Matchmaking.Domain.Model
{

    public partial class EzUserGameSessionDomain {
        private readonly Gs2.Gs2Matchmaking.Domain.Model.UserAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.GameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string NextPageToken => _domain.NextPageToken;
        public string MatchmakingContextToken => _domain.MatchmakingContextToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserGameSessionDomain(
            Gs2.Gs2Matchmaking.Domain.Model.UserAccessTokenDomain domain,
            Gs2.Unity.Util.GameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }

        [Obsolete("The name has been changed to CreateGatheringFuture.")]
        public IFuture<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzGatheringGameSessionDomain> CreateGathering(
            Gs2.Unity.Gs2Matchmaking.Model.EzPlayer player,
            Gs2.Unity.Gs2Matchmaking.Model.EzAttributeRange[] attributeRanges = null,
            Gs2.Unity.Gs2Matchmaking.Model.EzCapacityOfRole[] capacityOfRoles = null,
            string[] allowUserIds = null,
            long? expiresAt = null,
            Gs2.Unity.Gs2Matchmaking.Model.EzTimeSpan expiresAtTimeSpan = null
        )
        {
            return CreateGatheringFuture(
                player,
                attributeRanges,
                capacityOfRoles,
                allowUserIds,
                expiresAt,
                expiresAtTimeSpan
            );
        }

        public IFuture<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzGatheringGameSessionDomain> CreateGatheringFuture(
            Gs2.Unity.Gs2Matchmaking.Model.EzPlayer player,
            Gs2.Unity.Gs2Matchmaking.Model.EzAttributeRange[] attributeRanges = null,
            Gs2.Unity.Gs2Matchmaking.Model.EzCapacityOfRole[] capacityOfRoles = null,
            string[] allowUserIds = null,
            long? expiresAt = null,
            Gs2.Unity.Gs2Matchmaking.Model.EzTimeSpan expiresAtTimeSpan = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzGatheringGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.CreateGatheringFuture(
                        new CreateGatheringRequest()
                            .WithPlayer(player?.ToModel())
                            .WithAttributeRanges(attributeRanges?.Select(v => v.ToModel()).ToArray())
                            .WithCapacityOfRoles(capacityOfRoles?.Select(v => v.ToModel()).ToArray())
                            .WithAllowUserIds(allowUserIds)
                            .WithExpiresAt(expiresAt)
                            .WithExpiresAtTimeSpan(expiresAtTimeSpan?.ToModel())
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Matchmaking.Domain.Model.EzGatheringGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzGatheringGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzGatheringGameSessionDomain> CreateGatheringAsync(
            Gs2.Unity.Gs2Matchmaking.Model.EzPlayer player,
            Gs2.Unity.Gs2Matchmaking.Model.EzAttributeRange[] attributeRanges = null,
            Gs2.Unity.Gs2Matchmaking.Model.EzCapacityOfRole[] capacityOfRoles = null,
            string[] allowUserIds = null,
            long? expiresAt = null,
            Gs2.Unity.Gs2Matchmaking.Model.EzTimeSpan expiresAtTimeSpan = null
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.CreateGatheringAsync(
                    new CreateGatheringRequest()
                        .WithPlayer(player?.ToModel())
                        .WithAttributeRanges(attributeRanges?.Select(v => v.ToModel()).ToArray())
                        .WithCapacityOfRoles(capacityOfRoles?.Select(v => v.ToModel()).ToArray())
                        .WithAllowUserIds(allowUserIds)
                        .WithExpiresAt(expiresAt)
                        .WithExpiresAtTimeSpan(expiresAtTimeSpan?.ToModel())
                )
            );
            return new Gs2.Unity.Gs2Matchmaking.Domain.Model.EzGatheringGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        public Gs2Iterator<Gs2.Unity.Gs2Matchmaking.Model.EzGathering> DoMatchmaking(
              Gs2.Unity.Gs2Matchmaking.Model.EzPlayer player
        )
        {
            return new Gs2.Unity.Gs2Matchmaking.Domain.Iterator.EzDoMatchmakingIterator(
                this._domain,
                this._gameSession,
                this._connection,
                player
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Matchmaking.Model.EzGathering> DoMatchmakingAsync(
              Gs2.Unity.Gs2Matchmaking.Model.EzPlayer player
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Matchmaking.Model.EzGathering>(async (writer, token) =>
            {
                var it = _domain.DoMatchmakingAsync(
                    player.ToModel()
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        this._gameSession,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.DoMatchmakingAsync(
                                player.ToModel()
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Matchmaking.Model.EzGathering.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeDoMatchmaking(Action callback) {
            return this._domain.SubscribeDoMatchmaking(callback);
        }

        public void UnsubscribeDoMatchmaking(ulong callbackId) {
            this._domain.UnsubscribeDoMatchmaking(callbackId);
        }

        public Gs2Iterator<Gs2.Unity.Gs2Matchmaking.Model.EzRating> Ratings(
        )
        {
            return new Gs2.Unity.Gs2Matchmaking.Domain.Iterator.EzListRatingsIterator(
                this._domain,
                this._gameSession,
                this._connection
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Matchmaking.Model.EzRating> RatingsAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Matchmaking.Model.EzRating>(async (writer, token) =>
            {
                var it = _domain.RatingsAsync(
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        this._gameSession,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.RatingsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Matchmaking.Model.EzRating.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeRatings(Action callback) {
            return this._domain.SubscribeRatings(callback);
        }

        public void UnsubscribeRatings(ulong callbackId) {
            this._domain.UnsubscribeRatings(callbackId);
        }

        public Gs2.Unity.Gs2Matchmaking.Domain.Model.EzGatheringGameSessionDomain Gathering(
            string gatheringName
        ) {
            return new Gs2.Unity.Gs2Matchmaking.Domain.Model.EzGatheringGameSessionDomain(
                _domain.Gathering(
                    gatheringName
                ),
                this._gameSession,
                this._connection
            );
        }

        public Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotGameSessionDomain Ballot(
            string ratingName,
            string gatheringName,
            int numberOfPlayer,
            string keyId
        ) {
            return new Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotGameSessionDomain(
                _domain.Ballot(
                    ratingName,
                    gatheringName,
                    numberOfPlayer,
                    keyId
                ),
                this._gameSession,
                this._connection
            );
        }

        public Gs2.Unity.Gs2Matchmaking.Domain.Model.EzRatingGameSessionDomain Rating(
            string ratingName
        ) {
            return new Gs2.Unity.Gs2Matchmaking.Domain.Model.EzRatingGameSessionDomain(
                _domain.Rating(
                    ratingName
                ),
                this._gameSession,
                this._connection
            );
        }

    }
}
