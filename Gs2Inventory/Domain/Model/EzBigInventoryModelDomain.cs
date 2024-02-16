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

    public partial class EzBigInventoryModelDomain {
        private readonly Gs2.Gs2Inventory.Domain.Model.BigInventoryModelDomain _domain;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string NamespaceName => _domain?.NamespaceName;
        public string InventoryName => _domain?.InventoryName;

        public EzBigInventoryModelDomain(
            Gs2.Gs2Inventory.Domain.Model.BigInventoryModelDomain domain,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._connection = connection;
        }

        public Gs2Iterator<Gs2.Unity.Gs2Inventory.Model.EzBigItemModel> BigItemModels(
        )
        {
            return new Gs2.Unity.Gs2Inventory.Domain.Iterator.EzListBigItemModelsIterator(
                this._domain,
                this._connection
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Inventory.Model.EzBigItemModel> BigItemModelsAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Inventory.Model.EzBigItemModel>(async (writer, token) =>
            {
                var it = _domain.BigItemModelsAsync(
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        null,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.BigItemModelsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Inventory.Model.EzBigItemModel.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeBigItemModels(
            Action<Gs2.Unity.Gs2Inventory.Model.EzBigItemModel[]> callback
        ) {
            return this._domain.SubscribeBigItemModels(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Inventory.Model.EzBigItemModel.FromModel).ToArray());
                }
            );
        }

        public void UnsubscribeBigItemModels(
            ulong callbackId
        ) {
            this._domain.UnsubscribeBigItemModels(
                callbackId
            );
        }

        public Gs2.Unity.Gs2Inventory.Domain.Model.EzBigItemModelDomain BigItemModel(
            string itemName
        ) {
            return new Gs2.Unity.Gs2Inventory.Domain.Model.EzBigItemModelDomain(
                _domain.BigItemModel(
                    itemName
                ),
                this._connection
            );
        }

        [Obsolete("The name has been changed to ModelFuture.")]
        public IFuture<Gs2.Unity.Gs2Inventory.Model.EzBigInventoryModel> Model()
        {
            return ModelFuture();
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Inventory.Model.EzBigInventoryModel> ModelAsync()
        {
            var item = await this._connection.RunAsync(
                null,
                async () =>
                {
                    return await _domain.ModelAsync();
                }
            );
            if (item == null) {
                return null;
            }
            return Gs2.Unity.Gs2Inventory.Model.EzBigInventoryModel.FromModel(
                item
            );
        }
        #endif

        public IFuture<Gs2.Unity.Gs2Inventory.Model.EzBigInventoryModel> ModelFuture()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Inventory.Model.EzBigInventoryModel> self)
            {
                var future = this._connection.RunFuture(
                    null,
                    () => {
                    	return _domain.ModelFuture();
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
                self.OnComplete(Gs2.Unity.Gs2Inventory.Model.EzBigInventoryModel.FromModel(
                    item
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Inventory.Model.EzBigInventoryModel>(Impl);
        }

        public void Invalidate()
        {
            this._domain.Invalidate();
        }

        public ulong Subscribe(Action<Gs2.Unity.Gs2Inventory.Model.EzBigInventoryModel> callback)
        {
            return this._domain.Subscribe(item => {
                callback.Invoke(item == null ? null : Gs2.Unity.Gs2Inventory.Model.EzBigInventoryModel.FromModel(
                    item
                ));
            });
        }

        public void Unsubscribe(ulong callbackId)
        {
            this._domain.Unsubscribe(callbackId);
        }

        #if UNITY_2017_1_OR_NEWER
        public Gs2Future<ulong> SubscribeWithInitialCallFuture(Action<Gs2.Unity.Gs2Inventory.Model.EzBigInventoryModel> callback)
        {
            IEnumerator Impl(IFuture<ulong> self)
            {
                var future = ModelFuture();
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                var item = future.Result;
                var callbackId = Subscribe(callback);
                callback.Invoke(item);
                self.OnComplete(callbackId);
            }
            return new Gs2InlineFuture<ulong>(Impl);
        }
        #endif

        #if !UNITY_2017_1_OR_NEWER || GS2_ENABLE_UNITASK
            #if UNITY_2017_1_OR_NEWER
        public async UniTask<ulong> SubscribeWithInitialCallAsync(Action<Gs2.Unity.Gs2Inventory.Model.EzBigInventoryModel> callback)
            #else
        public async Task<ulong> SubscribeWithInitialCallAsync(Action<Gs2.Unity.Gs2Inventory.Model.EzBigInventoryModel> callback)
            #endif
        {
            var item = await ModelAsync();
            var callbackId = Subscribe(callback);
            callback.Invoke(item);
            return callbackId;
        }
        #endif

    }
}
