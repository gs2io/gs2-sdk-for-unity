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
using Gs2.Gs2Mission.Domain.Iterator;
using Gs2.Gs2Mission.Request;
using Gs2.Gs2Mission.Result;
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

namespace Gs2.Unity.Gs2Mission.Domain.Model
{

    public partial class EzUserGameSessionDomain {
        private readonly Gs2.Gs2Mission.Domain.Model.UserAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.IGameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public Gs2.Unity.Gs2Mission.Model.EzComplete[] ChangedCompletes => _domain.ChangedCompletes.Select(Gs2.Unity.Gs2Mission.Model.EzComplete.FromModel).ToArray();
        public string? NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserGameSessionDomain(
            Gs2.Gs2Mission.Domain.Model.UserAccessTokenDomain domain,
            Gs2.Unity.Util.IGameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }

        public Gs2Iterator<Gs2.Unity.Gs2Mission.Model.EzComplete> Completes(
        )
        {
            return new Gs2.Unity.Gs2Mission.Domain.Iterator.EzListCompletesIterator(
                this._domain,
                this._gameSession,
                this._connection
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Mission.Model.EzComplete> CompletesAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Mission.Model.EzComplete>(async (writer, token) =>
            {
                var it = _domain.CompletesAsync(
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        this._gameSession,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.CompletesAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Mission.Model.EzComplete.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeCompletes(
            Action<Gs2.Unity.Gs2Mission.Model.EzComplete[]> callback
        ) {
            return this._domain.SubscribeCompletes(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Mission.Model.EzComplete.FromModel).ToArray());
                }
            );
        }

        public void UnsubscribeCompletes(
            ulong callbackId
        ) {
            this._domain.UnsubscribeCompletes(
                callbackId
            );
        }

        public Gs2Iterator<Gs2.Unity.Gs2Mission.Model.EzCounter> Counters(
        )
        {
            return new Gs2.Unity.Gs2Mission.Domain.Iterator.EzListCountersIterator(
                this._domain,
                this._gameSession,
                this._connection
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Mission.Model.EzCounter> CountersAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Mission.Model.EzCounter>(async (writer, token) =>
            {
                var it = _domain.CountersAsync(
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        this._gameSession,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.CountersAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Mission.Model.EzCounter.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeCounters(
            Action<Gs2.Unity.Gs2Mission.Model.EzCounter[]> callback
        ) {
            return this._domain.SubscribeCounters(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Mission.Model.EzCounter.FromModel).ToArray());
                }
            );
        }

        public void UnsubscribeCounters(
            ulong callbackId
        ) {
            this._domain.UnsubscribeCounters(
                callbackId
            );
        }

        public Gs2.Unity.Gs2Mission.Domain.Model.EzCompleteGameSessionDomain Complete(
            string missionGroupName
        ) {
            return new Gs2.Unity.Gs2Mission.Domain.Model.EzCompleteGameSessionDomain(
                _domain.Complete(
                    missionGroupName
                ),
                this._gameSession,
                this._connection
            );
        }

        public Gs2.Unity.Gs2Mission.Domain.Model.EzCounterGameSessionDomain Counter(
            string counterName
        ) {
            return new Gs2.Unity.Gs2Mission.Domain.Model.EzCounterGameSessionDomain(
                _domain.Counter(
                    counterName
                ),
                this._gameSession,
                this._connection
            );
        }

    }
}
