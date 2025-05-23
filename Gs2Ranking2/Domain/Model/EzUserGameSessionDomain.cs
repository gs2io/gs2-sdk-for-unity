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

    public partial class EzUserGameSessionDomain {
        private readonly Gs2.Gs2Ranking2.Domain.Model.UserAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.IGameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string? NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserGameSessionDomain(
            Gs2.Gs2Ranking2.Domain.Model.UserAccessTokenDomain domain,
            Gs2.Unity.Util.IGameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }

        public Gs2Iterator<Gs2.Unity.Gs2Ranking2.Model.EzSubscribeUser> Subscribes(
            string rankingName
        )
        {
            return new Gs2.Unity.Gs2Ranking2.Domain.Iterator.EzListSubscribesIterator(
                this._domain,
                this._gameSession,
                this._connection,
                rankingName
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Ranking2.Model.EzSubscribeUser> SubscribesAsync(
              string rankingName
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Ranking2.Model.EzSubscribeUser>(async (writer, token) =>
            {
                var it = _domain.SubscribesAsync(
                    rankingName
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        this._gameSession,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.SubscribesAsync(
                                rankingName
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Ranking2.Model.EzSubscribeUser.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeSubscribes(
            Action<Gs2.Unity.Gs2Ranking2.Model.EzSubscribeUser[]> callback,
            string rankingName
        ) {
            return this._domain.SubscribeSubscribes(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Ranking2.Model.EzSubscribeUser.FromModel).ToArray());
                },
                rankingName
            );
        }

        public void UnsubscribeSubscribes(
            ulong callbackId,
            string rankingName
        ) {
            this._domain.UnsubscribeSubscribes(
                callbackId,
                rankingName
            );
        }

        public void InvalidateSubscribes(
            string rankingName
        ) {
            this._domain.InvalidateSubscribes(
                rankingName
            );
        }

        public Gs2.Unity.Gs2Ranking2.Domain.Model.EzSubscribeGameSessionDomain Subscribe(
            string rankingName
        ) {
            return new Gs2.Unity.Gs2Ranking2.Domain.Model.EzSubscribeGameSessionDomain(
                _domain.Subscribe(
                    rankingName
                ),
                this._gameSession,
                this._connection
            );
        }

    }
}
