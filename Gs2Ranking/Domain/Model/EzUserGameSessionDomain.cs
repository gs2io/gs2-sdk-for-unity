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

    public partial class EzUserGameSessionDomain {
        private readonly Gs2.Gs2Ranking.Domain.Model.UserAccessTokenDomain _domain;
        public string NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserGameSessionDomain(
            Gs2.Gs2Ranking.Domain.Model.UserAccessTokenDomain domain
        ) {
            this._domain = domain;
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Ranking.Domain.Model.EzSubscribeUserGameSessionDomain> SubscribeAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Ranking.Domain.Model.EzSubscribeUserGameSessionDomain> Subscribe(
        #endif
              string categoryName,
              string targetUserId
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _domain.SubscribeAsync(
                new SubscribeRequest()
                    .WithCategoryName(categoryName)
                    .WithTargetUserId(targetUserId)
            );
            return new Gs2.Unity.Gs2Ranking.Domain.Model.EzSubscribeUserGameSessionDomain(result);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Ranking.Domain.Model.EzSubscribeUserGameSessionDomain> self)
            {
                var future = _domain.Subscribe(
                    new SubscribeRequest()
                        .WithCategoryName(categoryName)
                        .WithTargetUserId(targetUserId)
                );
                yield return future;
                if (future.Error != null)
                {
                    self.OnError(future.Error);
                    yield break;
                }
                var result = future.Result;
                self.OnComplete(new Gs2.Unity.Gs2Ranking.Domain.Model.EzSubscribeUserGameSessionDomain(result));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Ranking.Domain.Model.EzSubscribeUserGameSessionDomain>(Impl);
        #endif
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Ranking.Model.EzSubscribeUser> SubscribeUsers(
        #else
        public class EzSubscribeUsersIterator : Gs2Iterator<Gs2.Unity.Gs2Ranking.Model.EzSubscribeUser>
        {
            private readonly Gs2Iterator<Gs2.Gs2Ranking.Model.SubscribeUser> _it;

            public EzSubscribeUsersIterator(
                Gs2Iterator<Gs2.Gs2Ranking.Model.SubscribeUser> it
            )
            {
                _it = it;
            }

            public override bool HasNext()
            {
                return _it.HasNext();
            }

            protected override IEnumerator Next(Action<Gs2.Unity.Gs2Ranking.Model.EzSubscribeUser> callback)
            {
                yield return _it.Next();
                callback.Invoke(Gs2.Unity.Gs2Ranking.Model.EzSubscribeUser.FromModel(_it.Current));
            }
        }

        public Gs2Iterator<Gs2.Unity.Gs2Ranking.Model.EzSubscribeUser> SubscribeUsers(
        #endif
              string categoryName
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Ranking.Model.EzSubscribeUser>(async (writer, token) =>
            {
                var it = _domain.SubscribeUsers(
                    categoryName
                ).GetAsyncEnumerator();
                while(await it.MoveNextAsync())
                {
                    await writer.YieldAsync(Gs2.Unity.Gs2Ranking.Model.EzSubscribeUser.FromModel(it.Current));
                }
            });
        #else
            return new EzSubscribeUsersIterator(_domain.SubscribeUsers(
               categoryName
            ));
        #endif
        }

        public Gs2.Unity.Gs2Ranking.Domain.Model.EzSubscribeUserGameSessionDomain SubscribeUser(
            string categoryName,
            string targetUserId
        ) {
            return new Gs2.Unity.Gs2Ranking.Domain.Model.EzSubscribeUserGameSessionDomain(
                _domain.SubscribeUser(
                    categoryName,
                    targetUserId
                )
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Ranking.Model.EzRanking> Rankings(
        #else
        public class EzRankingsIterator : Gs2Iterator<Gs2.Unity.Gs2Ranking.Model.EzRanking>
        {
            private readonly Gs2Iterator<Gs2.Gs2Ranking.Model.Ranking> _it;

            public EzRankingsIterator(
                Gs2Iterator<Gs2.Gs2Ranking.Model.Ranking> it
            )
            {
                _it = it;
            }

            public override bool HasNext()
            {
                return _it.HasNext();
            }

            protected override IEnumerator Next(Action<Gs2.Unity.Gs2Ranking.Model.EzRanking> callback)
            {
                yield return _it.Next();
                callback.Invoke(Gs2.Unity.Gs2Ranking.Model.EzRanking.FromModel(_it.Current));
            }
        }

        public Gs2Iterator<Gs2.Unity.Gs2Ranking.Model.EzRanking> Rankings(
        #endif
              string categoryName
        )
        {
        #if GS2_ENABLE_UNITASK
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
        #else
            return new EzRankingsIterator(_domain.Rankings(
               categoryName
            ));
        #endif
        }

        public Gs2.Unity.Gs2Ranking.Domain.Model.EzRankingGameSessionDomain Ranking(
            string categoryName
        ) {
            return new Gs2.Unity.Gs2Ranking.Domain.Model.EzRankingGameSessionDomain(
                _domain.Ranking(
                    categoryName
                )
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Ranking.Model.EzScore> Scores(
        #else
        public class EzScoresIterator : Gs2Iterator<Gs2.Unity.Gs2Ranking.Model.EzScore>
        {
            private readonly Gs2Iterator<Gs2.Gs2Ranking.Model.Score> _it;

            public EzScoresIterator(
                Gs2Iterator<Gs2.Gs2Ranking.Model.Score> it
            )
            {
                _it = it;
            }

            public override bool HasNext()
            {
                return _it.HasNext();
            }

            protected override IEnumerator Next(Action<Gs2.Unity.Gs2Ranking.Model.EzScore> callback)
            {
                yield return _it.Next();
                callback.Invoke(Gs2.Unity.Gs2Ranking.Model.EzScore.FromModel(_it.Current));
            }
        }

        public Gs2Iterator<Gs2.Unity.Gs2Ranking.Model.EzScore> Scores(
        #endif
              string categoryName,
              string scorerUserId
        )
        {
        #if GS2_ENABLE_UNITASK
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
        #else
            return new EzScoresIterator(_domain.Scores(
               categoryName,
               scorerUserId
            ));
        #endif
        }

        public Gs2.Unity.Gs2Ranking.Domain.Model.EzScoreGameSessionDomain Score(
            string categoryName,
            string scorerUserId,
            string uniqueId = null
        ) {
            return new Gs2.Unity.Gs2Ranking.Domain.Model.EzScoreGameSessionDomain(
                _domain.Score(
                    categoryName,
                    scorerUserId,
                    uniqueId
                )
            );
        }

    }
}
