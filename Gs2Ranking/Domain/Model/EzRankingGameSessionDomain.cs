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

    public partial class EzRankingGameSessionDomain {
        private readonly Gs2.Gs2Ranking.Domain.Model.RankingAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.GameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;
        public string CategoryName => _domain?.CategoryName;

        public EzRankingGameSessionDomain(
            Gs2.Gs2Ranking.Domain.Model.RankingAccessTokenDomain domain,
            Gs2.Unity.Util.GameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }

        [Obsolete("The name has been changed to PutScoreFuture.")]
        public IFuture<Gs2.Unity.Gs2Ranking.Domain.Model.EzScoreGameSessionDomain> PutScore(
            long score,
            string metadata = null
        )
        {
            return PutScoreFuture(
                score,
                metadata
            );
        }

        public IFuture<Gs2.Unity.Gs2Ranking.Domain.Model.EzScoreGameSessionDomain> PutScoreFuture(
            long score,
            string metadata = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Ranking.Domain.Model.EzScoreGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.PutScoreFuture(
                        new PutScoreRequest()
                            .WithScore(score)
                            .WithMetadata(metadata)
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Ranking.Domain.Model.EzScoreGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Ranking.Domain.Model.EzScoreGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Ranking.Domain.Model.EzScoreGameSessionDomain> PutScoreAsync(
            long score,
            string metadata = null
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.PutScoreAsync(
                    new PutScoreRequest()
                        .WithScore(score)
                        .WithMetadata(metadata)
                )
            );
            return new Gs2.Unity.Gs2Ranking.Domain.Model.EzScoreGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        [Obsolete("The name has been changed to ModelFuture.")]
        public IFuture<Gs2.Unity.Gs2Ranking.Model.EzRanking> Model(
            string scorerUserId
        )
        {
            return ModelFuture(scorerUserId);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Ranking.Model.EzRanking> ModelAsync(
            string scorerUserId
        )
        {
            var item = await this._connection.RunAsync(
                this._gameSession,
                async () =>
                {
                    return await _domain.ModelAsync(scorerUserId);
                }
            );
            if (item == null) {
                return null;
            }
            return Gs2.Unity.Gs2Ranking.Model.EzRanking.FromModel(
                item
            );
        }
        #endif

        public IFuture<Gs2.Unity.Gs2Ranking.Model.EzRanking> ModelFuture(
            string scorerUserId
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Ranking.Model.EzRanking> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => {
                    	return _domain.ModelFuture(scorerUserId);
                    }
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                var item = future.Result;
                if (item == null) {
                    self.OnComplete(null);
                    yield break;
                }
                self.OnComplete(Gs2.Unity.Gs2Ranking.Model.EzRanking.FromModel(
                    item
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Ranking.Model.EzRanking>(Impl);
        }

        public ulong Subscribe(
            Action<Gs2.Unity.Gs2Ranking.Model.EzRanking> callback,
            string scorerUserId
        )
        {
            return this._domain.Subscribe(item => {
                    callback.Invoke(Gs2.Unity.Gs2Ranking.Model.EzRanking.FromModel(
                        item
                    ));
                },
                scorerUserId
            );
        }

        public void Unsubscribe(
            ulong callbackId,
            string scorerUserId
        )
        {
            this._domain.Unsubscribe(callbackId, scorerUserId);
        }

    }
}
