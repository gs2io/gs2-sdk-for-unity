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
using Gs2.Gs2MegaField.Domain.Iterator;
using Gs2.Gs2MegaField.Request;
using Gs2.Gs2MegaField.Result;
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

namespace Gs2.Unity.Gs2MegaField.Domain.Model
{

    public partial class EzNamespaceDomain {
        private readonly Gs2.Gs2MegaField.Domain.Model.NamespaceDomain _domain;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string? Status => _domain.Status;
        public string? NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;

        public EzNamespaceDomain(
            Gs2.Gs2MegaField.Domain.Model.NamespaceDomain domain,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._connection = connection;
        }

        public Gs2Iterator<Gs2.Unity.Gs2MegaField.Model.EzAreaModel> AreaModels(
        )
        {
            return new Gs2.Unity.Gs2MegaField.Domain.Iterator.EzDescribeAreaModelsIterator(
                this._domain,
                this._connection
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2MegaField.Model.EzAreaModel> AreaModelsAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2MegaField.Model.EzAreaModel>(async (writer, token) =>
            {
                var it = _domain.AreaModelsAsync(
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        null,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.AreaModelsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2MegaField.Model.EzAreaModel.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeAreaModels(
            Action<Gs2.Unity.Gs2MegaField.Model.EzAreaModel[]> callback
        ) {
            return this._domain.SubscribeAreaModels(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2MegaField.Model.EzAreaModel.FromModel).ToArray());
                }
            );
        }

        public void UnsubscribeAreaModels(
            ulong callbackId
        ) {
            this._domain.UnsubscribeAreaModels(
                callbackId
            );
        }

        public void InvalidateAreaModels(
        ) {
            this._domain.InvalidateAreaModels(
            );
        }

        public Gs2.Unity.Gs2MegaField.Domain.Model.EzAreaModelDomain AreaModel(
            string areaModelName
        ) {
            return new Gs2.Unity.Gs2MegaField.Domain.Model.EzAreaModelDomain(
                _domain.AreaModel(
                    areaModelName
                ),
                this._connection
            );
        }

        public Gs2.Unity.Gs2MegaField.Domain.Model.EzUserDomain User(
            string userId
        ) {
            return new Gs2.Unity.Gs2MegaField.Domain.Model.EzUserDomain(
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
