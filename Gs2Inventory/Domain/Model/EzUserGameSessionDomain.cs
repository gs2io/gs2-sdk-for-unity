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

    public partial class EzUserGameSessionDomain {
        private readonly Gs2.Gs2Inventory.Domain.Model.UserAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.GameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string? NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserGameSessionDomain(
            Gs2.Gs2Inventory.Domain.Model.UserAccessTokenDomain domain,
            Gs2.Unity.Util.GameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }

        public Gs2Iterator<Gs2.Unity.Gs2Inventory.Model.EzInventory> Inventories(
        )
        {
            return new Gs2.Unity.Gs2Inventory.Domain.Iterator.EzListInventoriesIterator(
                this._domain,
                this._gameSession,
                this._connection
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Inventory.Model.EzInventory> InventoriesAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Inventory.Model.EzInventory>(async (writer, token) =>
            {
                var it = _domain.InventoriesAsync(
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        this._gameSession,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.InventoriesAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Inventory.Model.EzInventory.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeInventories(
            Action<Gs2.Unity.Gs2Inventory.Model.EzInventory[]> callback
        ) {
            return this._domain.SubscribeInventories(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Inventory.Model.EzInventory.FromModel).ToArray());
                }
            );
        }

        public void UnsubscribeInventories(
            ulong callbackId
        ) {
            this._domain.UnsubscribeInventories(
                callbackId
            );
        }

        public Gs2.Unity.Gs2Inventory.Domain.Model.EzInventoryGameSessionDomain Inventory(
            string inventoryName
        ) {
            return new Gs2.Unity.Gs2Inventory.Domain.Model.EzInventoryGameSessionDomain(
                _domain.Inventory(
                    inventoryName
                ),
                this._gameSession,
                this._connection
            );
        }

        public Gs2.Unity.Gs2Inventory.Domain.Model.EzSimpleInventoryGameSessionDomain SimpleInventory(
            string inventoryName
        ) {
            return new Gs2.Unity.Gs2Inventory.Domain.Model.EzSimpleInventoryGameSessionDomain(
                _domain.SimpleInventory(
                    inventoryName
                ),
                this._gameSession,
                this._connection
            );
        }

        public Gs2.Unity.Gs2Inventory.Domain.Model.EzBigInventoryGameSessionDomain BigInventory(
            string inventoryName
        ) {
            return new Gs2.Unity.Gs2Inventory.Domain.Model.EzBigInventoryGameSessionDomain(
                _domain.BigInventory(
                    inventoryName
                ),
                this._gameSession,
                this._connection
            );
        }

    }
}
