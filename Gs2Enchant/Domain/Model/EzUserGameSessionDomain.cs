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
using Gs2.Gs2Enchant.Domain.Iterator;
using Gs2.Gs2Enchant.Request;
using Gs2.Gs2Enchant.Result;
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

namespace Gs2.Unity.Gs2Enchant.Domain.Model
{

    public partial class EzUserGameSessionDomain {
        private readonly Gs2.Gs2Enchant.Domain.Model.UserAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.GameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string? NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserGameSessionDomain(
            Gs2.Gs2Enchant.Domain.Model.UserAccessTokenDomain domain,
            Gs2.Unity.Util.GameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }

        public Gs2Iterator<Gs2.Unity.Gs2Enchant.Model.EzBalanceParameterStatus> BalanceParameterStatuses(
            string? parameterName = null
        )
        {
            return new Gs2.Unity.Gs2Enchant.Domain.Iterator.EzListBalanceParameterStatusesIterator(
                this._domain,
                this._gameSession,
                this._connection,
                parameterName
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Enchant.Model.EzBalanceParameterStatus> BalanceParameterStatusesAsync(
              string? parameterName = null
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Enchant.Model.EzBalanceParameterStatus>(async (writer, token) =>
            {
                var it = _domain.BalanceParameterStatusesAsync(
                    parameterName
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        this._gameSession,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.BalanceParameterStatusesAsync(
                                parameterName
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Enchant.Model.EzBalanceParameterStatus.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeBalanceParameterStatuses(
            Action<Gs2.Unity.Gs2Enchant.Model.EzBalanceParameterStatus[]> callback,
            string? parameterName = null
        ) {
            return this._domain.SubscribeBalanceParameterStatuses(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Enchant.Model.EzBalanceParameterStatus.FromModel).ToArray());
                },
                parameterName
            );
        }

        public void UnsubscribeBalanceParameterStatuses(
            ulong callbackId,
            string? parameterName = null
        ) {
            this._domain.UnsubscribeBalanceParameterStatuses(
                callbackId,
                parameterName
            );
        }

        public Gs2Iterator<Gs2.Unity.Gs2Enchant.Model.EzRarityParameterStatus> RarityParameterStatuses(
            string? parameterName = null
        )
        {
            return new Gs2.Unity.Gs2Enchant.Domain.Iterator.EzListRarityParameterStatusesIterator(
                this._domain,
                this._gameSession,
                this._connection,
                parameterName
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Enchant.Model.EzRarityParameterStatus> RarityParameterStatusesAsync(
              string? parameterName = null
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Enchant.Model.EzRarityParameterStatus>(async (writer, token) =>
            {
                var it = _domain.RarityParameterStatusesAsync(
                    parameterName
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        this._gameSession,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.RarityParameterStatusesAsync(
                                parameterName
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Enchant.Model.EzRarityParameterStatus.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeRarityParameterStatuses(
            Action<Gs2.Unity.Gs2Enchant.Model.EzRarityParameterStatus[]> callback,
            string? parameterName = null
        ) {
            return this._domain.SubscribeRarityParameterStatuses(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Enchant.Model.EzRarityParameterStatus.FromModel).ToArray());
                },
                parameterName
            );
        }

        public void UnsubscribeRarityParameterStatuses(
            ulong callbackId,
            string? parameterName = null
        ) {
            this._domain.UnsubscribeRarityParameterStatuses(
                callbackId,
                parameterName
            );
        }

        public Gs2.Unity.Gs2Enchant.Domain.Model.EzBalanceParameterStatusGameSessionDomain BalanceParameterStatus(
            string parameterName,
            string propertyId
        ) {
            return new Gs2.Unity.Gs2Enchant.Domain.Model.EzBalanceParameterStatusGameSessionDomain(
                _domain.BalanceParameterStatus(
                    parameterName,
                    propertyId
                ),
                this._gameSession,
                this._connection
            );
        }

        public Gs2.Unity.Gs2Enchant.Domain.Model.EzRarityParameterStatusGameSessionDomain RarityParameterStatus(
            string parameterName,
            string propertyId
        ) {
            return new Gs2.Unity.Gs2Enchant.Domain.Model.EzRarityParameterStatusGameSessionDomain(
                _domain.RarityParameterStatus(
                    parameterName,
                    propertyId
                ),
                this._gameSession,
                this._connection
            );
        }

    }
}
