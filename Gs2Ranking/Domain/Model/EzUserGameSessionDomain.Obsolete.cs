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
using Gs2.Unity.Gs2Ranking.Model;
#if GS2_ENABLE_UNITASK
using Cysharp.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using System.Collections.Generic;
#endif

namespace Gs2.Unity.Gs2Ranking.Domain.Model
{

    public partial class EzUserGameSessionDomain {
        
#if GS2_ENABLE_UNITASK
        public UniTask<Gs2.Unity.Gs2Ranking.Domain.Model.EzSubscribeUserGameSessionDomain> Subscribe(
            string categoryName,
            string targetUserId
        ) {
            return SubscribeAsync(
                categoryName,
                targetUserId
            );
        }
#else
        public IFuture<Gs2.Unity.Gs2Ranking.Domain.Model.EzSubscribeUserGameSessionDomain> Subscribe(
            string categoryName,
            string targetUserId
        ) {
            return SubscribeFuture(
                categoryName,
                targetUserId
            );
        }
#endif

        public IFuture<Gs2.Unity.Gs2Ranking.Domain.Model.EzSubscribeUserGameSessionDomain> SubscribeFuture(
            string categoryName,
            string targetUserId
        ) {
            return this.RankingCategory(
                categoryName
            ).SubscribeFuture(
                targetUserId
            );
        }

#if GS2_ENABLE_UNITASK
        public UniTask<Gs2.Unity.Gs2Ranking.Domain.Model.EzSubscribeUserGameSessionDomain> SubscribeAsync(
            string categoryName,
            string targetUserId
        ) {
            return this.RankingCategory(
                categoryName
            ).SubscribeAsync(
                targetUserId
            );
        }
#endif

        public Gs2Iterator<EzSubscribeUser> SubscribeUsers(
            string categoryName
        ) {
            return this.RankingCategory(
                categoryName
            ).SubscribeUsers();
        }
        
#if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<EzSubscribeUser> SubscribeUsersAsync(
            string categoryName
        ) {
            return this.RankingCategory(
                categoryName
            ).SubscribeUsersAsync();
        }
#endif
        
        public Gs2.Unity.Gs2Ranking.Domain.Model.EzSubscribeUserGameSessionDomain SubscribeUser(
            string categoryName,
            string targetUserId
        ) {
            return this.RankingCategory(
                categoryName
            ).SubscribeUser(
                targetUserId
            );
        }

        public Gs2Iterator<EzRanking> Rankings(
            string categoryName,
            string? additionalScopeName = null
        ) {
            return this.RankingCategory(
                categoryName,
                additionalScopeName
            ).Rankings();
        }
        
#if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<EzRanking> RankingsAsync(
            string categoryName,
            string? additionalScopeName = null
        ) {
            return this.RankingCategory(
                categoryName,
                additionalScopeName
            ).RankingsAsync();
        }
#endif

        public Gs2.Unity.Gs2Ranking.Domain.Model.EzRankingCategoryGameSessionDomain Ranking(
            string categoryName,
            string? additionalScopeName = null
        ) {
            return this.RankingCategory(
                categoryName,
                additionalScopeName
            );
        }

    }
}
