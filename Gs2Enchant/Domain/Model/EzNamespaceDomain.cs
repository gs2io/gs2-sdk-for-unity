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

    public partial class EzNamespaceDomain {
        private readonly Gs2.Gs2Enchant.Domain.Model.NamespaceDomain _domain;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string? Status => _domain.Status;
        public string? Url => _domain.Url;
        public string? UploadToken => _domain.UploadToken;
        public string? UploadUrl => _domain.UploadUrl;
        public string? NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;

        public EzNamespaceDomain(
            Gs2.Gs2Enchant.Domain.Model.NamespaceDomain domain,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._connection = connection;
        }

        public Gs2Iterator<Gs2.Unity.Gs2Enchant.Model.EzBalanceParameterModel> BalanceParameterModels(
        )
        {
            return new Gs2.Unity.Gs2Enchant.Domain.Iterator.EzListBalanceParameterModelsIterator(
                this._domain,
                this._connection
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Enchant.Model.EzBalanceParameterModel> BalanceParameterModelsAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Enchant.Model.EzBalanceParameterModel>(async (writer, token) =>
            {
                var it = _domain.BalanceParameterModelsAsync(
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        null,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.BalanceParameterModelsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Enchant.Model.EzBalanceParameterModel.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeBalanceParameterModels(
            Action<Gs2.Unity.Gs2Enchant.Model.EzBalanceParameterModel[]> callback
        ) {
            return this._domain.SubscribeBalanceParameterModels(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Enchant.Model.EzBalanceParameterModel.FromModel).ToArray());
                }
            );
        }

        public void UnsubscribeBalanceParameterModels(
            ulong callbackId
        ) {
            this._domain.UnsubscribeBalanceParameterModels(
                callbackId
            );
        }

        public void InvalidateBalanceParameterModels(
        ) {
            this._domain.InvalidateBalanceParameterModels(
            );
        }

        public Gs2Iterator<Gs2.Unity.Gs2Enchant.Model.EzRarityParameterModel> RarityParameterModels(
        )
        {
            return new Gs2.Unity.Gs2Enchant.Domain.Iterator.EzListRarityParameterModelsIterator(
                this._domain,
                this._connection
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Enchant.Model.EzRarityParameterModel> RarityParameterModelsAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Enchant.Model.EzRarityParameterModel>(async (writer, token) =>
            {
                var it = _domain.RarityParameterModelsAsync(
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        null,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.RarityParameterModelsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Enchant.Model.EzRarityParameterModel.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeRarityParameterModels(
            Action<Gs2.Unity.Gs2Enchant.Model.EzRarityParameterModel[]> callback
        ) {
            return this._domain.SubscribeRarityParameterModels(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Enchant.Model.EzRarityParameterModel.FromModel).ToArray());
                }
            );
        }

        public void UnsubscribeRarityParameterModels(
            ulong callbackId
        ) {
            this._domain.UnsubscribeRarityParameterModels(
                callbackId
            );
        }

        public void InvalidateRarityParameterModels(
        ) {
            this._domain.InvalidateRarityParameterModels(
            );
        }

        public Gs2.Unity.Gs2Enchant.Domain.Model.EzBalanceParameterModelDomain BalanceParameterModel(
            string parameterName
        ) {
            return new Gs2.Unity.Gs2Enchant.Domain.Model.EzBalanceParameterModelDomain(
                _domain.BalanceParameterModel(
                    parameterName
                ),
                this._connection
            );
        }

        public Gs2.Unity.Gs2Enchant.Domain.Model.EzRarityParameterModelDomain RarityParameterModel(
            string parameterName
        ) {
            return new Gs2.Unity.Gs2Enchant.Domain.Model.EzRarityParameterModelDomain(
                _domain.RarityParameterModel(
                    parameterName
                ),
                this._connection
            );
        }

        public Gs2.Unity.Gs2Enchant.Domain.Model.EzUserDomain User(
            string userId
        ) {
            return new Gs2.Unity.Gs2Enchant.Domain.Model.EzUserDomain(
                _domain.User(
                    userId
                ),
                this._connection
            );
        }

        public EzUserGameSessionDomain Me(
            Gs2.Unity.Util.IGameSession gameSession
        ) {
            return new EzUserGameSessionDomain(
                _domain.AccessToken(
                    gameSession.AccessToken
                ),
                gameSession,
                this._connection
            );
        }

    }
}
