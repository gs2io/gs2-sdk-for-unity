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

    public partial class EzNamespaceDomain {
        private readonly Gs2.Gs2Matchmaking.Domain.Model.NamespaceDomain _domain;
        public string Status => _domain.Status;
        public string NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;

        public EzNamespaceDomain(
            Gs2.Gs2Matchmaking.Domain.Model.NamespaceDomain domain
        ) {
            this._domain = domain;
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain> VoteAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain> Vote(
        #endif
              string ballotBody,
              string ballotSignature,
              Gs2.Unity.Gs2Matchmaking.Model.EzGameResult[] gameResults = null
        ) {
            var result = await _domain.VoteAsync(
                new VoteRequest()
                    .WithBallotBody(ballotBody)
                    .WithBallotSignature(ballotSignature)
                    .WithGameResults(gameResults?.Select(v => v.ToModel()).ToArray())
            );
            return new Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain(result);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain> VoteMultipleAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain> VoteMultiple(
        #endif
              Gs2.Unity.Gs2Matchmaking.Model.EzSignedBallot[] signedBallots = null,
              Gs2.Unity.Gs2Matchmaking.Model.EzGameResult[] gameResults = null
        ) {
            var result = await _domain.VoteMultipleAsync(
                new VoteMultipleRequest()
                    .WithSignedBallots(signedBallots?.Select(v => v.ToModel()).ToArray())
                    .WithGameResults(gameResults?.Select(v => v.ToModel()).ToArray())
            );
            return new Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain(result);
        }

        public Gs2.Unity.Gs2Matchmaking.Domain.Model.EzUserDomain User(
            string userId
        ) {
            return new Gs2.Unity.Gs2Matchmaking.Domain.Model.EzUserDomain(
                _domain.User(
                    userId
                )
            );
        }

        public EzUserGameSessionDomain Me(
            Gs2.Unity.Util.GameSession gameSession
        ) {
            return new EzUserGameSessionDomain(
                _domain.AccessToken(
                    gameSession.AccessToken
                )
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Matchmaking.Model.EzRatingModel> RatingModels(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Matchmaking.Model.EzRatingModel> RatingModels(
        #endif
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Matchmaking.Model.EzRatingModel>(async (writer, token) =>
            {
                var it = _domain.RatingModels(
                ).GetAsyncEnumerator();
                while(await it.MoveNextAsync())
                {
                    await writer.YieldAsync(Gs2.Unity.Gs2Matchmaking.Model.EzRatingModel.FromModel(it.Current));
                }
            });
        }

        public Gs2.Unity.Gs2Matchmaking.Domain.Model.EzRatingModelDomain RatingModel(
            string ratingName
        ) {
            return new Gs2.Unity.Gs2Matchmaking.Domain.Model.EzRatingModelDomain(
                _domain.RatingModel(
                    ratingName
                )
            );
        }

    }
}
