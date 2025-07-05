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

    public partial class EzClusterRankingSeasonDomain {
        private readonly Gs2.Gs2Ranking2.Domain.Model.ClusterRankingSeasonDomain _domain;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string? NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string RankingName => _domain?.RankingName;
        public string ClusterName => _domain?.ClusterName;
        public long? Season => _domain?.Season;
        public string UserId => _domain?.UserId;

        public EzClusterRankingSeasonDomain(
            Gs2.Gs2Ranking2.Domain.Model.ClusterRankingSeasonDomain domain,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._connection = connection;
        }

        public Gs2.Unity.Gs2Ranking2.Domain.Model.EzClusterRankingReceivedRewardDomain ClusterRankingReceivedReward(
        ) {
            return new Gs2.Unity.Gs2Ranking2.Domain.Model.EzClusterRankingReceivedRewardDomain(
                _domain.ClusterRankingReceivedReward(
                ),
                this._connection
            );
        }

        public Gs2.Unity.Gs2Ranking2.Domain.Model.EzClusterRankingScoreDomain ClusterRankingScore(
        ) {
            return new Gs2.Unity.Gs2Ranking2.Domain.Model.EzClusterRankingScoreDomain(
                _domain.ClusterRankingScore(
                ),
                this._connection
            );
        }

    }
}
