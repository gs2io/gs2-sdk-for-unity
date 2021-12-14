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

    public partial class EzUserDomain {
        private readonly Gs2.Gs2Matchmaking.Domain.Model.UserDomain _domain;
        public string NextPageToken => _domain.NextPageToken;
        public string MatchmakingContextToken => _domain.MatchmakingContextToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserDomain(
            Gs2.Gs2Matchmaking.Domain.Model.UserDomain domain
        ) {
            this._domain = domain;
        }

        public Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain Ballot(
            string ratingName,
            string gatheringName,
            int numberOfPlayer,
            string keyId
        ) {
            return new Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain(
                _domain.Ballot(
                    ratingName,
                    gatheringName,
                    numberOfPlayer,
                    keyId
                )
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Matchmaking.Model.EzGathering> Gatherings(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Matchmaking.Model.EzGathering> Gatherings(
        #endif
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Matchmaking.Model.EzGathering>(async (writer, token) =>
            {
                var it = _domain.Gatherings(
                ).GetAsyncEnumerator();
                while(await it.MoveNextAsync())
                {
                    await writer.YieldAsync(Gs2.Unity.Gs2Matchmaking.Model.EzGathering.FromModel(it.Current));
                }
            });
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Matchmaking.Model.EzGathering> DoMatchmakingByPlayer(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Matchmaking.Model.EzGathering> DoMatchmakingByPlayer(
        #endif
              Gs2.Unity.Gs2Matchmaking.Model.EzPlayer player
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Matchmaking.Model.EzGathering>(async (writer, token) =>
            {
                var it = _domain.DoMatchmakingByPlayer(
                    player.ToModel()
                ).GetAsyncEnumerator();
                while(await it.MoveNextAsync())
                {
                    await writer.YieldAsync(Gs2.Unity.Gs2Matchmaking.Model.EzGathering.FromModel(it.Current));
                }
            });
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Matchmaking.Model.EzGathering> DoMatchmaking(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Matchmaking.Model.EzGathering> DoMatchmaking(
        #endif
              Gs2.Unity.Gs2Matchmaking.Model.EzPlayer player
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Matchmaking.Model.EzGathering>(async (writer, token) =>
            {
                var it = _domain.DoMatchmaking(
                    player.ToModel()
                ).GetAsyncEnumerator();
                while(await it.MoveNextAsync())
                {
                    await writer.YieldAsync(Gs2.Unity.Gs2Matchmaking.Model.EzGathering.FromModel(it.Current));
                }
            });
        }

        public Gs2.Unity.Gs2Matchmaking.Domain.Model.EzGatheringDomain Gathering(
            string gatheringName
        ) {
            return new Gs2.Unity.Gs2Matchmaking.Domain.Model.EzGatheringDomain(
                _domain.Gathering(
                    gatheringName
                )
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Matchmaking.Model.EzRating> Ratings(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Matchmaking.Model.EzRating> Ratings(
        #endif
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Matchmaking.Model.EzRating>(async (writer, token) =>
            {
                var it = _domain.Ratings(
                ).GetAsyncEnumerator();
                while(await it.MoveNextAsync())
                {
                    await writer.YieldAsync(Gs2.Unity.Gs2Matchmaking.Model.EzRating.FromModel(it.Current));
                }
            });
        }

        public Gs2.Unity.Gs2Matchmaking.Domain.Model.EzRatingDomain Rating(
            string ratingName
        ) {
            return new Gs2.Unity.Gs2Matchmaking.Domain.Model.EzRatingDomain(
                _domain.Rating(
                    ratingName
                )
            );
        }

    }
}