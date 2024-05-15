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
#pragma warning disable CS0169, CS0168

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

    public partial class EzNamespaceDomain {
        private readonly Gs2.Gs2Matchmaking.Domain.Model.NamespaceDomain _domain;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string? Status => _domain.Status;
        public string? Url => _domain.Url;
        public string? UploadToken => _domain.UploadToken;
        public string? UploadUrl => _domain.UploadUrl;
        public string? NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;

        public EzNamespaceDomain(
            Gs2.Gs2Matchmaking.Domain.Model.NamespaceDomain domain,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._connection = connection;
        }

        [Obsolete("The name has been changed to VoteFuture.")]
        public IFuture<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain> Vote(
            string ballotBody,
            string ballotSignature,
            Gs2.Unity.Gs2Matchmaking.Model.EzGameResult[] gameResults = null,
            string keyId = null
        )
        {
            return VoteFuture(
                ballotBody,
                ballotSignature,
                gameResults,
                keyId
            );
        }

        public IFuture<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain> VoteFuture(
            string ballotBody,
            string ballotSignature,
            Gs2.Unity.Gs2Matchmaking.Model.EzGameResult[] gameResults = null,
            string keyId = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain> self)
            {
                var future = this._connection.RunFuture(
                    null,
                    () => this._domain.VoteFuture(
                        new VoteRequest()
                            .WithBallotBody(ballotBody)
                            .WithBallotSignature(ballotSignature)
                            .WithGameResults(gameResults?.Select(v => v.ToModel()).ToArray())
                            .WithKeyId(keyId)
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain(
                    future.Result,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain> VoteAsync(
            string ballotBody,
            string ballotSignature,
            Gs2.Unity.Gs2Matchmaking.Model.EzGameResult[] gameResults = null,
            string keyId = null
        ) {
            var result = await this._connection.RunAsync(
                null,
                () => this._domain.VoteAsync(
                    new VoteRequest()
                        .WithBallotBody(ballotBody)
                        .WithBallotSignature(ballotSignature)
                        .WithGameResults(gameResults?.Select(v => v.ToModel()).ToArray())
                        .WithKeyId(keyId)
                )
            );
            return new Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain(
                result,
                this._connection
            );
        }
        #endif

        [Obsolete("The name has been changed to VoteMultipleFuture.")]
        public IFuture<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain> VoteMultiple(
            Gs2.Unity.Gs2Matchmaking.Model.EzSignedBallot[] signedBallots = null,
            Gs2.Unity.Gs2Matchmaking.Model.EzGameResult[] gameResults = null,
            string keyId = null
        )
        {
            return VoteMultipleFuture(
                signedBallots,
                gameResults,
                keyId
            );
        }

        public IFuture<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain> VoteMultipleFuture(
            Gs2.Unity.Gs2Matchmaking.Model.EzSignedBallot[] signedBallots = null,
            Gs2.Unity.Gs2Matchmaking.Model.EzGameResult[] gameResults = null,
            string keyId = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain> self)
            {
                var future = this._connection.RunFuture(
                    null,
                    () => this._domain.VoteMultipleFuture(
                        new VoteMultipleRequest()
                            .WithSignedBallots(signedBallots?.Select(v => v.ToModel()).ToArray())
                            .WithGameResults(gameResults?.Select(v => v.ToModel()).ToArray())
                            .WithKeyId(keyId)
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain(
                    future.Result,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain> VoteMultipleAsync(
            Gs2.Unity.Gs2Matchmaking.Model.EzSignedBallot[] signedBallots = null,
            Gs2.Unity.Gs2Matchmaking.Model.EzGameResult[] gameResults = null,
            string keyId = null
        ) {
            var result = await this._connection.RunAsync(
                null,
                () => this._domain.VoteMultipleAsync(
                    new VoteMultipleRequest()
                        .WithSignedBallots(signedBallots?.Select(v => v.ToModel()).ToArray())
                        .WithGameResults(gameResults?.Select(v => v.ToModel()).ToArray())
                        .WithKeyId(keyId)
                )
            );
            return new Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain(
                result,
                this._connection
            );
        }
        #endif

        public Gs2Iterator<Gs2.Unity.Gs2Matchmaking.Model.EzRatingModel> RatingModels(
        )
        {
            return new Gs2.Unity.Gs2Matchmaking.Domain.Iterator.EzListRatingModelsIterator(
                this._domain,
                this._connection
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Matchmaking.Model.EzRatingModel> RatingModelsAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Matchmaking.Model.EzRatingModel>(async (writer, token) =>
            {
                var it = _domain.RatingModelsAsync(
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        null,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.RatingModelsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Matchmaking.Model.EzRatingModel.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeRatingModels(
            Action<Gs2.Unity.Gs2Matchmaking.Model.EzRatingModel[]> callback
        ) {
            return this._domain.SubscribeRatingModels(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Matchmaking.Model.EzRatingModel.FromModel).ToArray());
                }
            );
        }

        public void UnsubscribeRatingModels(
            ulong callbackId
        ) {
            this._domain.UnsubscribeRatingModels(
                callbackId
            );
        }

        public Gs2.Unity.Gs2Matchmaking.Domain.Model.EzUserDomain User(
            string userId
        ) {
            return new Gs2.Unity.Gs2Matchmaking.Domain.Model.EzUserDomain(
                _domain.User(
                    userId
                ),
                this._connection
            );
        }

        public EzUserGameSessionDomain Me(
            Gs2.Unity.Util.IGameSession gameSession
        ) {
            return new EzUserGameSessionDomain(
                _domain.AccessToken(
                    gameSession.AccessToken
                ),
                gameSession,
                this._connection
            );
        }

        public Gs2.Unity.Gs2Matchmaking.Domain.Model.EzRatingModelDomain RatingModel(
            string ratingName
        ) {
            return new Gs2.Unity.Gs2Matchmaking.Domain.Model.EzRatingModelDomain(
                _domain.RatingModel(
                    ratingName
                ),
                this._connection
            );
        }

    }
}
