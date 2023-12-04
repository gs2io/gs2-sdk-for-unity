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
using Gs2.Gs2Formation.Domain.Iterator;
using Gs2.Gs2Formation.Request;
using Gs2.Gs2Formation.Result;
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

namespace Gs2.Unity.Gs2Formation.Domain.Model
{

    public partial class EzNamespaceDomain {
        private readonly Gs2.Gs2Formation.Domain.Model.NamespaceDomain _domain;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string? Status => _domain.Status;
        public string? Url => _domain.Url;
        public string? UploadToken => _domain.UploadToken;
        public string? UploadUrl => _domain.UploadUrl;
        public string? NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;

        public EzNamespaceDomain(
            Gs2.Gs2Formation.Domain.Model.NamespaceDomain domain,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._connection = connection;
        }

        public Gs2Iterator<Gs2.Unity.Gs2Formation.Model.EzMoldModel> MoldModels(
        )
        {
            return new Gs2.Unity.Gs2Formation.Domain.Iterator.EzListMoldModelsIterator(
                this._domain,
                this._connection
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Formation.Model.EzMoldModel> MoldModelsAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Formation.Model.EzMoldModel>(async (writer, token) =>
            {
                var it = _domain.MoldModelsAsync(
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        null,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.MoldModelsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Formation.Model.EzMoldModel.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeMoldModels(Action callback) {
            return this._domain.SubscribeMoldModels(callback);
        }

        public void UnsubscribeMoldModels(ulong callbackId) {
            this._domain.UnsubscribeMoldModels(callbackId);
        }

        public Gs2Iterator<Gs2.Unity.Gs2Formation.Model.EzPropertyFormModel> PropertyFormModels(
        )
        {
            return new Gs2.Unity.Gs2Formation.Domain.Iterator.EzListPropertyFormModelsIterator(
                this._domain,
                this._connection
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Formation.Model.EzPropertyFormModel> PropertyFormModelsAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Formation.Model.EzPropertyFormModel>(async (writer, token) =>
            {
                var it = _domain.PropertyFormModelsAsync(
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        null,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.PropertyFormModelsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Formation.Model.EzPropertyFormModel.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribePropertyFormModels(Action callback) {
            return this._domain.SubscribePropertyFormModels(callback);
        }

        public void UnsubscribePropertyFormModels(ulong callbackId) {
            this._domain.UnsubscribePropertyFormModels(callbackId);
        }

        public Gs2.Unity.Gs2Formation.Domain.Model.EzMoldModelDomain MoldModel(
            string moldModelName
        ) {
            return new Gs2.Unity.Gs2Formation.Domain.Model.EzMoldModelDomain(
                _domain.MoldModel(
                    moldModelName
                ),
                this._connection
            );
        }

        public Gs2.Unity.Gs2Formation.Domain.Model.EzPropertyFormModelDomain PropertyFormModel(
            string propertyFormModelName
        ) {
            return new Gs2.Unity.Gs2Formation.Domain.Model.EzPropertyFormModelDomain(
                _domain.PropertyFormModel(
                    propertyFormModelName
                ),
                this._connection
            );
        }

        public Gs2.Unity.Gs2Formation.Domain.Model.EzUserDomain User(
            string userId
        ) {
            return new Gs2.Unity.Gs2Formation.Domain.Model.EzUserDomain(
                _domain.User(
                    userId
                ),
                this._connection
            );
        }

        public EzUserGameSessionDomain Me(
            Gs2.Unity.Util.GameSession gameSession
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
