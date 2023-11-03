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
        private readonly Gs2.Unity.Util.Profile _profile;
        public string NextPageToken => _domain.NextPageToken;
        public bool? Processing => _domain.Processing;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserGameSessionDomain(
            Gs2.Gs2Ranking.Domain.Model.UserAccessTokenDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Ranking.Domain.Model.EzSubscribeUserGameSessionDomain> Subscribe(
              string categoryName,
              string targetUserId
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Ranking.Domain.Model.EzSubscribeUserGameSessionDomain> self)
            {
                yield return SubscribeAsync(
                    categoryName,
                    targetUserId
                ).ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Ranking.Domain.Model.EzSubscribeUserGameSessionDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Ranking.Domain.Model.EzSubscribeUserGameSessionDomain> SubscribeAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Ranking.Domain.Model.EzSubscribeUserGameSessionDomain> Subscribe(
        #endif
              string categoryName,
              string targetUserId
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.SubscribeAsync(
                        new SubscribeRequest()
                            .WithCategoryName(categoryName)
                            .WithTargetUserId(targetUserId)
                            .WithAccessToken(_domain.AccessToken.Token)
                    );
                }
            );
            return new Gs2.Unity.Gs2Ranking.Domain.Model.EzSubscribeUserGameSessionDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Ranking.Domain.Model.EzSubscribeUserGameSessionDomain> self)
            {
                var future = _domain.SubscribeFuture(
                    new SubscribeRequest()
                        .WithCategoryName(categoryName)
                        .WithTargetUserId(targetUserId)
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return _profile.RunFuture(
                    _domain.AccessToken,
                    future,
                    () =>
        			{
                		return future = _domain.SubscribeFuture(
                    		new SubscribeRequest()
                	        .WithCategoryName(categoryName)
                	        .WithTargetUserId(targetUserId)
                    	    .WithAccessToken(_domain.AccessToken.Token)
        		        );
        			}
                );
                if (future.Error != null)
                {
                    self.OnError(future.Error);
                    yield break;
                }
                var result = future.Result;
                self.OnComplete(new Gs2.Unity.Gs2Ranking.Domain.Model.EzSubscribeUserGameSessionDomain(result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Ranking.Domain.Model.EzSubscribeUserGameSessionDomain>(Impl);
        #endif
        }

        public class EzSubscribeUsersIterator : Gs2Iterator<Gs2.Unity.Gs2Ranking.Model.EzSubscribeUser>
        {
            private Gs2Iterator<Gs2.Gs2Ranking.Model.SubscribeUser> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly string _categoryName;
            private readonly Gs2.Gs2Ranking.Domain.Model.UserAccessTokenDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzSubscribeUsersIterator(
                Gs2Iterator<Gs2.Gs2Ranking.Model.SubscribeUser> it,
        #if !GS2_ENABLE_UNITASK
                string categoryName,
                Gs2.Gs2Ranking.Domain.Model.UserAccessTokenDomain domain,
        #endif
                Gs2.Unity.Util.Profile profile
            )
            {
                _it = it;
        #if !GS2_ENABLE_UNITASK
                _categoryName = categoryName;
                _domain = domain;
        #endif
                _profile = profile;
            }

            public override bool HasNext()
            {
                return _it.HasNext();
            }

            protected override IEnumerator Next(Action<AsyncResult<Gs2.Unity.Gs2Ranking.Model.EzSubscribeUser>> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    _domain.AccessToken,
                    _it,
                    () =>
                    {
                        return _it = _domain.SubscribeUsers(
                            _categoryName
                        );
                    }
                );
        #endif
                callback.Invoke(
                    new AsyncResult<Gs2.Unity.Gs2Ranking.Model.EzSubscribeUser>(
                        _it.Current == null ? null : Gs2.Unity.Gs2Ranking.Model.EzSubscribeUser.FromModel(_it.Current),
                        _it.Error
                    )
                );
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Ranking.Model.EzSubscribeUser> SubscribeUsers(
              string categoryName
        )
        {
            return new EzSubscribeUsersIterator(
                _domain.SubscribeUsers(
                    categoryName
                ),
                _profile
            );
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Ranking.Model.EzSubscribeUser> SubscribeUsersAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Ranking.Model.EzSubscribeUser> SubscribeUsers(
        #endif
              string categoryName
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Ranking.Model.EzSubscribeUser>(async (writer, token) =>
            {
                var it = _domain.SubscribeUsersAsync(
                    categoryName
                ).GetAsyncEnumerator();
                while(
                    await _profile.RunIteratorAsync(
                        _domain.AccessToken,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.SubscribeUsersAsync(
                                categoryName
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Ranking.Model.EzSubscribeUser.FromModel(it.Current));
                }
            });
        #else
            return new EzSubscribeUsersIterator(
                _domain.SubscribeUsers(
                    categoryName
                ),
                categoryName,
                _domain,
                _profile
            );
        #endif
        }

        public ulong SubscribeSubscribeUsers(Action callback) {
            return this._domain.SubscribeSubscribeUsers(callback);
        }

        public void UnsubscribeSubscribeUsers(ulong callbackId) {
            this._domain.UnsubscribeSubscribeUsers(callbackId);
        }

        public Gs2.Unity.Gs2Ranking.Domain.Model.EzSubscribeUserGameSessionDomain SubscribeUser(
            string categoryName,
            string targetUserId
        ) {
            return new Gs2.Unity.Gs2Ranking.Domain.Model.EzSubscribeUserGameSessionDomain(
                _domain.SubscribeUser(
                    categoryName,
                    targetUserId
                ),
                _profile
            );
        }

        public class EzRankingsIterator : Gs2Iterator<Gs2.Unity.Gs2Ranking.Model.EzRanking>
        {
            private Gs2Iterator<Gs2.Gs2Ranking.Model.Ranking> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly string _categoryName;
            private readonly string _additionalScopeName;
            private readonly Gs2.Gs2Ranking.Domain.Model.UserAccessTokenDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzRankingsIterator(
                Gs2Iterator<Gs2.Gs2Ranking.Model.Ranking> it,
        #if !GS2_ENABLE_UNITASK
                string categoryName,
                string additionalScopeName,
                Gs2.Gs2Ranking.Domain.Model.UserAccessTokenDomain domain,
        #endif
                Gs2.Unity.Util.Profile profile
            )
            {
                _it = it;
        #if !GS2_ENABLE_UNITASK
                _categoryName = categoryName;
                _additionalScopeName = additionalScopeName;
                _domain = domain;
        #endif
                _profile = profile;
            }

            public override bool HasNext()
            {
                return _it.HasNext();
            }

            protected override IEnumerator Next(Action<AsyncResult<Gs2.Unity.Gs2Ranking.Model.EzRanking>> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    _domain.AccessToken,
                    _it,
                    () =>
                    {
                        return _it = _domain.Rankings(
                            _categoryName,
                            _additionalScopeName
                        );
                    }
                );
        #endif
                callback.Invoke(
                    new AsyncResult<Gs2.Unity.Gs2Ranking.Model.EzRanking>(
                        _it.Current == null ? null : Gs2.Unity.Gs2Ranking.Model.EzRanking.FromModel(_it.Current),
                        _it.Error
                    )
                );
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Ranking.Model.EzRanking> Rankings(
              string categoryName,
              string additionalScopeName = null
        )
        {
            return new EzRankingsIterator(
                _domain.Rankings(
                    categoryName,
                    additionalScopeName
                ),
                _profile
            );
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Ranking.Model.EzRanking> RankingsAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Ranking.Model.EzRanking> Rankings(
        #endif
              string categoryName,
              string additionalScopeName = null
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Ranking.Model.EzRanking>(async (writer, token) =>
            {
                var it = _domain.RankingsAsync(
                    categoryName,
                    additionalScopeName
                ).GetAsyncEnumerator();
                while(
                    await _profile.RunIteratorAsync(
                        _domain.AccessToken,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.RankingsAsync(
                                categoryName,
                                additionalScopeName
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Ranking.Model.EzRanking.FromModel(it.Current));
                }
            });
        #else
            return new EzRankingsIterator(
                _domain.Rankings(
                    categoryName,
                    additionalScopeName
                ),
                categoryName,
                additionalScopeName,
                _domain,
                _profile
            );
        #endif
        }

        public ulong SubscribeRankings(Action callback) {
            return this._domain.SubscribeRankings(callback);
        }

        public void UnsubscribeRankings(ulong callbackId) {
            this._domain.UnsubscribeRankings(callbackId);
        }

        public Gs2.Unity.Gs2Ranking.Domain.Model.EzRankingGameSessionDomain Ranking(
            string categoryName
        ) {
            return new Gs2.Unity.Gs2Ranking.Domain.Model.EzRankingGameSessionDomain(
                _domain.Ranking(
                    categoryName
                ),
                _profile
            );
        }

        public class EzScoresIterator : Gs2Iterator<Gs2.Unity.Gs2Ranking.Model.EzScore>
        {
            private Gs2Iterator<Gs2.Gs2Ranking.Model.Score> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly string _categoryName;
            private readonly string _scorerUserId;
            private readonly Gs2.Gs2Ranking.Domain.Model.UserAccessTokenDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzScoresIterator(
                Gs2Iterator<Gs2.Gs2Ranking.Model.Score> it,
        #if !GS2_ENABLE_UNITASK
                string categoryName,
                string scorerUserId,
                Gs2.Gs2Ranking.Domain.Model.UserAccessTokenDomain domain,
        #endif
                Gs2.Unity.Util.Profile profile
            )
            {
                _it = it;
        #if !GS2_ENABLE_UNITASK
                _categoryName = categoryName;
                _scorerUserId = scorerUserId;
                _domain = domain;
        #endif
                _profile = profile;
            }

            public override bool HasNext()
            {
                return _it.HasNext();
            }

            protected override IEnumerator Next(Action<AsyncResult<Gs2.Unity.Gs2Ranking.Model.EzScore>> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    _domain.AccessToken,
                    _it,
                    () =>
                    {
                        return _it = _domain.Scores(
                            _categoryName,
                            _scorerUserId
                        );
                    }
                );
        #endif
                callback.Invoke(
                    new AsyncResult<Gs2.Unity.Gs2Ranking.Model.EzScore>(
                        _it.Current == null ? null : Gs2.Unity.Gs2Ranking.Model.EzScore.FromModel(_it.Current),
                        _it.Error
                    )
                );
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Ranking.Model.EzScore> Scores(
              string categoryName,
              string scorerUserId
        )
        {
            return new EzScoresIterator(
                _domain.Scores(
                    categoryName,
                    scorerUserId
                ),
                _profile
            );
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Ranking.Model.EzScore> ScoresAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Ranking.Model.EzScore> Scores(
        #endif
              string categoryName,
              string scorerUserId
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Ranking.Model.EzScore>(async (writer, token) =>
            {
                var it = _domain.ScoresAsync(
                    categoryName,
                    scorerUserId
                ).GetAsyncEnumerator();
                while(
                    await _profile.RunIteratorAsync(
                        _domain.AccessToken,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.ScoresAsync(
                                categoryName,
                                scorerUserId
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Ranking.Model.EzScore.FromModel(it.Current));
                }
            });
        #else
            return new EzScoresIterator(
                _domain.Scores(
                    categoryName,
                    scorerUserId
                ),
                categoryName,
                scorerUserId,
                _domain,
                _profile
            );
        #endif
        }

        public ulong SubscribeScores(Action callback) {
            return this._domain.SubscribeScores(callback);
        }

        public void UnsubscribeScores(ulong callbackId) {
            this._domain.UnsubscribeScores(callbackId);
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
                ),
                _profile
            );
        }

    }
}
