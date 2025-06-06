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

    public partial class EzSeasonGameSessionDomain {
        private readonly Gs2.Gs2Matchmaking.Domain.Model.SeasonAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.IGameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string? NextPageToken => _domain.NextPageToken;
        public string? MatchmakingContextToken => _domain.MatchmakingContextToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;
        public string SeasonName => _domain?.SeasonName;
        public long? Season => _domain?.Season;

        public EzSeasonGameSessionDomain(
            Gs2.Gs2Matchmaking.Domain.Model.SeasonAccessTokenDomain domain,
            Gs2.Unity.Util.IGameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }

        public Gs2Iterator<Gs2.Unity.Gs2Matchmaking.Model.EzSeasonGathering> DoSeasonMatchmaking(
        )
        {
            return new Gs2.Unity.Gs2Matchmaking.Domain.Iterator.EzDoSeasonMatchmakingIterator(
                this._domain,
                this._gameSession,
                this._connection
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Matchmaking.Model.EzSeasonGathering> DoSeasonMatchmakingAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Matchmaking.Model.EzSeasonGathering>(async (writer, token) =>
            {
                var it = _domain.DoSeasonMatchmakingAsync(
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        this._gameSession,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.DoSeasonMatchmakingAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Matchmaking.Model.EzSeasonGathering.FromModel(it.Current));
                }
            });
        }
        #endif

        public Gs2Iterator<Gs2.Unity.Gs2Matchmaking.Model.EzJoinedSeasonGathering> JoinedSeasonGatherings(
        )
        {
            return new Gs2.Unity.Gs2Matchmaking.Domain.Iterator.EzListJoinedSeasonGatheringsIterator(
                this._domain,
                this._gameSession,
                this._connection
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Matchmaking.Model.EzJoinedSeasonGathering> JoinedSeasonGatheringsAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Matchmaking.Model.EzJoinedSeasonGathering>(async (writer, token) =>
            {
                var it = _domain.JoinedSeasonGatheringsAsync(
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        this._gameSession,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.JoinedSeasonGatheringsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Matchmaking.Model.EzJoinedSeasonGathering.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeJoinedSeasonGatherings(
            Action<Gs2.Unity.Gs2Matchmaking.Model.EzJoinedSeasonGathering[]> callback
        ) {
            return this._domain.SubscribeJoinedSeasonGatherings(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Matchmaking.Model.EzJoinedSeasonGathering.FromModel).ToArray());
                }
            );
        }

        public void UnsubscribeJoinedSeasonGatherings(
            ulong callbackId
        ) {
            this._domain.UnsubscribeJoinedSeasonGatherings(
                callbackId
            );
        }

        public void InvalidateJoinedSeasonGatherings(
        ) {
            this._domain.InvalidateJoinedSeasonGatherings(
            );
        }

        public Gs2.Unity.Gs2Matchmaking.Domain.Model.EzSeasonGatheringGameSessionDomain SeasonGathering(
            long tier,
            string seasonGatheringName
        ) {
            return new Gs2.Unity.Gs2Matchmaking.Domain.Model.EzSeasonGatheringGameSessionDomain(
                _domain.SeasonGathering(
                    tier,
                    seasonGatheringName
                ),
                this._gameSession,
                this._connection
            );
        }

        public Gs2.Unity.Gs2Matchmaking.Domain.Model.EzJoinedSeasonGatheringGameSessionDomain JoinedSeasonGathering(
        ) {
            return new Gs2.Unity.Gs2Matchmaking.Domain.Model.EzJoinedSeasonGatheringGameSessionDomain(
                _domain.JoinedSeasonGathering(
                ),
                this._gameSession,
                this._connection
            );
        }

    }
}
