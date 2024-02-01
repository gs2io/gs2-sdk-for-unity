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

    public partial class EzRankingCategoryDomain {
        private readonly Gs2.Gs2Ranking.Domain.Model.RankingCategoryDomain _domain;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string? NextPageToken => _domain.NextPageToken;
        public bool? Processing => _domain.Processing;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;
        public string CategoryName => _domain?.CategoryName;
        public string AdditionalScopeName => _domain?.AdditionalScopeName;

        public EzRankingCategoryDomain(
            Gs2.Gs2Ranking.Domain.Model.RankingCategoryDomain domain,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._connection = connection;
        }

        public Gs2Iterator<Gs2.Unity.Gs2Ranking.Model.EzRanking> NearRankings(
            long score
        )
        {
            return new Gs2.Unity.Gs2Ranking.Domain.Iterator.EzGetNearRankingIterator(
                this._domain,
                this._connection,
                score
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Ranking.Model.EzRanking> NearRankingsAsync(
              long score
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Ranking.Model.EzRanking>(async (writer, token) =>
            {
                var it = _domain.NearRankingsAsync(
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

        public ulong SubscribeNearRankings(
            Action<Gs2.Unity.Gs2Ranking.Model.EzRanking[]> callback,
            long score
        ) {
            return this._domain.SubscribeNearRankings(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Ranking.Model.EzRanking.FromModel).ToArray());
                },
                score
            );
        }

        public void UnsubscribeNearRankings(
            ulong callbackId,
            long score
        ) {
            this._domain.UnsubscribeNearRankings(
                callbackId,
                score
            );
        }

        public Gs2.Unity.Gs2Ranking.Domain.Model.EzSubscribeUserDomain SubscribeUser(
            string targetUserId
        ) {
            return new Gs2.Unity.Gs2Ranking.Domain.Model.EzSubscribeUserDomain(
                _domain.SubscribeUser(
                    targetUserId
                ),
                this._connection
            );
        }

        public Gs2.Unity.Gs2Ranking.Domain.Model.EzRankingDomain Ranking(
            string scorerUserId = null,
            long? index = null
        ) {
            return new Gs2.Unity.Gs2Ranking.Domain.Model.EzRankingDomain(
                _domain.Ranking(
                    scorerUserId,
                    index
                ),
                this._connection
            );
        }

    }
}
