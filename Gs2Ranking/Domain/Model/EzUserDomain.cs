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

    public partial class EzUserDomain {
        private readonly Gs2.Gs2Ranking.Domain.Model.UserDomain _domain;
        public string NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserDomain(
            Gs2.Gs2Ranking.Domain.Model.UserDomain domain
        ) {
            this._domain = domain;
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Ranking.Model.EzSubscribeUser> SubscribesByCategoryName(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Ranking.Model.EzSubscribeUser> SubscribesByCategoryName(
        #endif
              string categoryName
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Ranking.Model.EzSubscribeUser>(async (writer, token) =>
            {
                var it = _domain.SubscribesByCategoryName(
                    categoryName
                ).GetAsyncEnumerator();
                while(await it.MoveNextAsync())
                {
                    await writer.YieldAsync(Gs2.Unity.Gs2Ranking.Model.EzSubscribeUser.FromModel(it.Current));
                }
            });
        }

        public Gs2.Unity.Gs2Ranking.Domain.Model.EzSubscribeUserDomain SubscribeUser(
            string categoryName,
            string targetUserId
        ) {
            return new Gs2.Unity.Gs2Ranking.Domain.Model.EzSubscribeUserDomain(
                _domain.SubscribeUser(
                    categoryName,
                    targetUserId
                )
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Ranking.Model.EzRanking> Rankings(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Ranking.Model.EzRanking> Rankings(
        #endif
              string categoryName
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Ranking.Model.EzRanking>(async (writer, token) =>
            {
                var it = _domain.Rankings(
                    categoryName
                ).GetAsyncEnumerator();
                while(await it.MoveNextAsync())
                {
                    await writer.YieldAsync(Gs2.Unity.Gs2Ranking.Model.EzRanking.FromModel(it.Current));
                }
            });
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Ranking.Model.EzRanking> NearRankings(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Ranking.Model.EzRanking> NearRankings(
        #endif
              string categoryName,
              long score
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Ranking.Model.EzRanking>(async (writer, token) =>
            {
                var it = _domain.NearRankings(
                    categoryName,
                    score
                ).GetAsyncEnumerator();
                while(await it.MoveNextAsync())
                {
                    await writer.YieldAsync(Gs2.Unity.Gs2Ranking.Model.EzRanking.FromModel(it.Current));
                }
            });
        }

        public Gs2.Unity.Gs2Ranking.Domain.Model.EzRankingDomain Ranking(
            string categoryName
        ) {
            return new Gs2.Unity.Gs2Ranking.Domain.Model.EzRankingDomain(
                _domain.Ranking(
                    categoryName
                )
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Ranking.Model.EzScore> Scores(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Ranking.Model.EzScore> Scores(
        #endif
              string categoryName,
              string scorerUserId
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Ranking.Model.EzScore>(async (writer, token) =>
            {
                var it = _domain.Scores(
                    categoryName,
                    scorerUserId
                ).GetAsyncEnumerator();
                while(await it.MoveNextAsync())
                {
                    await writer.YieldAsync(Gs2.Unity.Gs2Ranking.Model.EzScore.FromModel(it.Current));
                }
            });
        }

        public Gs2.Unity.Gs2Ranking.Domain.Model.EzScoreDomain Score(
            string categoryName,
            string scorerUserId,
            string uniqueId = null
        ) {
            return new Gs2.Unity.Gs2Ranking.Domain.Model.EzScoreDomain(
                _domain.Score(
                    categoryName,
                    scorerUserId,
                    uniqueId
                )
            );
        }

    }
}
