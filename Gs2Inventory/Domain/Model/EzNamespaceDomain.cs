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
using Gs2.Gs2Inventory.Domain.Iterator;
using Gs2.Gs2Inventory.Request;
using Gs2.Gs2Inventory.Result;
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

namespace Gs2.Unity.Gs2Inventory.Domain.Model
{

    public partial class EzNamespaceDomain {
        private readonly Gs2.Gs2Inventory.Domain.Model.NamespaceDomain _domain;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string? Status => _domain.Status;
        public string? Url => _domain.Url;
        public string? UploadToken => _domain.UploadToken;
        public string? UploadUrl => _domain.UploadUrl;
        public string? NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;

        public EzNamespaceDomain(
            Gs2.Gs2Inventory.Domain.Model.NamespaceDomain domain,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._connection = connection;
        }

        public Gs2Iterator<Gs2.Unity.Gs2Inventory.Model.EzInventoryModel> InventoryModels(
        )
        {
            return new Gs2.Unity.Gs2Inventory.Domain.Iterator.EzListInventoryModelsIterator(
                this._domain,
                this._connection
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Inventory.Model.EzInventoryModel> InventoryModelsAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Inventory.Model.EzInventoryModel>(async (writer, token) =>
            {
                var it = _domain.InventoryModelsAsync(
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        null,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.InventoryModelsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Inventory.Model.EzInventoryModel.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeInventoryModels(
            Action<Gs2.Unity.Gs2Inventory.Model.EzInventoryModel[]> callback
        ) {
            return this._domain.SubscribeInventoryModels(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Inventory.Model.EzInventoryModel.FromModel).ToArray());
                }
            );
        }

        public void UnsubscribeInventoryModels(
            ulong callbackId
        ) {
            this._domain.UnsubscribeInventoryModels(
                callbackId
            );
        }

        public Gs2Iterator<Gs2.Unity.Gs2Inventory.Model.EzSimpleInventoryModel> SimpleInventoryModels(
        )
        {
            return new Gs2.Unity.Gs2Inventory.Domain.Iterator.EzListSimpleInventoryModelsIterator(
                this._domain,
                this._connection
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Inventory.Model.EzSimpleInventoryModel> SimpleInventoryModelsAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Inventory.Model.EzSimpleInventoryModel>(async (writer, token) =>
            {
                var it = _domain.SimpleInventoryModelsAsync(
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        null,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.SimpleInventoryModelsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Inventory.Model.EzSimpleInventoryModel.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeSimpleInventoryModels(
            Action<Gs2.Unity.Gs2Inventory.Model.EzSimpleInventoryModel[]> callback
        ) {
            return this._domain.SubscribeSimpleInventoryModels(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Inventory.Model.EzSimpleInventoryModel.FromModel).ToArray());
                }
            );
        }

        public void UnsubscribeSimpleInventoryModels(
            ulong callbackId
        ) {
            this._domain.UnsubscribeSimpleInventoryModels(
                callbackId
            );
        }

        public Gs2Iterator<Gs2.Unity.Gs2Inventory.Model.EzBigInventoryModel> BigInventoryModels(
        )
        {
            return new Gs2.Unity.Gs2Inventory.Domain.Iterator.EzListBigInventoryModelsIterator(
                this._domain,
                this._connection
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Inventory.Model.EzBigInventoryModel> BigInventoryModelsAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Inventory.Model.EzBigInventoryModel>(async (writer, token) =>
            {
                var it = _domain.BigInventoryModelsAsync(
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        null,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.BigInventoryModelsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Inventory.Model.EzBigInventoryModel.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeBigInventoryModels(
            Action<Gs2.Unity.Gs2Inventory.Model.EzBigInventoryModel[]> callback
        ) {
            return this._domain.SubscribeBigInventoryModels(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Inventory.Model.EzBigInventoryModel.FromModel).ToArray());
                }
            );
        }

        public void UnsubscribeBigInventoryModels(
            ulong callbackId
        ) {
            this._domain.UnsubscribeBigInventoryModels(
                callbackId
            );
        }

        public Gs2.Unity.Gs2Inventory.Domain.Model.EzInventoryModelDomain InventoryModel(
            string inventoryName
        ) {
            return new Gs2.Unity.Gs2Inventory.Domain.Model.EzInventoryModelDomain(
                _domain.InventoryModel(
                    inventoryName
                ),
                this._connection
            );
        }

        public Gs2.Unity.Gs2Inventory.Domain.Model.EzUserDomain User(
            string userId = null
        ) {
            return new Gs2.Unity.Gs2Inventory.Domain.Model.EzUserDomain(
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

        public Gs2.Unity.Gs2Inventory.Domain.Model.EzSimpleInventoryModelDomain SimpleInventoryModel(
            string inventoryName
        ) {
            return new Gs2.Unity.Gs2Inventory.Domain.Model.EzSimpleInventoryModelDomain(
                _domain.SimpleInventoryModel(
                    inventoryName
                ),
                this._connection
            );
        }

        public Gs2.Unity.Gs2Inventory.Domain.Model.EzBigInventoryModelDomain BigInventoryModel(
            string inventoryName
        ) {
            return new Gs2.Unity.Gs2Inventory.Domain.Model.EzBigInventoryModelDomain(
                _domain.BigInventoryModel(
                    inventoryName
                ),
                this._connection
            );
        }

    }
}
