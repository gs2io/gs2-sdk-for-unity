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
#pragma warning disable CS0169, CS0168

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
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string NextPageToken => _domain.NextPageToken;
        public bool? Processing => _domain.Processing;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserDomain(
            Gs2.Gs2Ranking.Domain.Model.UserDomain domain,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._connection = connection;
        }

        public Gs2Iterator<Gs2.Unity.Gs2Ranking.Model.EzRanking> NearRankings(
              string categoryName,
              long score,
              string additionalScopeName = null
        )
        {
            return new Gs2.Unity.Gs2Ranking.Domain.Iterator.EzGetNearRankingIterator(
                this._domain,
                this._connection,
                categoryName,
                score,
                additionalScopeName
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Ranking.Model.EzRanking> NearRankingsAsync(
              string categoryName,
              long score,
              string additionalScopeName = null
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Ranking.Model.EzRanking>(async (writer, token) =>
            {
                var it = _domain.NearRankingsAsync(
                    categoryName,
                    additionalScopeName,
                    score
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        null,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.NearRankingsAsync(
                                categoryName,
                                additionalScopeName,
                                score
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

        public ulong SubscribeNearRankings(Action callback) {
            return this._domain.SubscribeNearRankings(callback);
        }

        public void UnsubscribeNearRankings(ulong callbackId) {
            this._domain.UnsubscribeNearRankings(callbackId);
        }

        public Gs2.Unity.Gs2Ranking.Domain.Model.EzSubscribeUserDomain SubscribeUser(
            string categoryName,
            string targetUserId
        ) {
            return new Gs2.Unity.Gs2Ranking.Domain.Model.EzSubscribeUserDomain(
                _domain.SubscribeUser(
                    categoryName,
                    targetUserId
                ),
                this._connection
            );
        }

        public Gs2.Unity.Gs2Ranking.Domain.Model.EzRankingDomain Ranking(
            string categoryName
        ) {
            return new Gs2.Unity.Gs2Ranking.Domain.Model.EzRankingDomain(
                _domain.Ranking(
                    categoryName
                ),
                this._connection
            );
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
                ),
                this._connection
            );
        }

    }
}
