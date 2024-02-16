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

    public partial class EzAreaModelDomain {
        private readonly Gs2.Gs2MegaField.Domain.Model.AreaModelDomain _domain;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string NamespaceName => _domain?.NamespaceName;
        public string AreaModelName => _domain?.AreaModelName;

        public EzAreaModelDomain(
            Gs2.Gs2MegaField.Domain.Model.AreaModelDomain domain,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._connection = connection;
        }

        public Gs2Iterator<Gs2.Unity.Gs2MegaField.Model.EzLayerModel> LayerModels(
        )
        {
            return new Gs2.Unity.Gs2MegaField.Domain.Iterator.EzDescribeLayerModelsIterator(
                this._domain,
                this._connection
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2MegaField.Model.EzLayerModel> LayerModelsAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2MegaField.Model.EzLayerModel>(async (writer, token) =>
            {
                var it = _domain.LayerModelsAsync(
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        null,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.LayerModelsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2MegaField.Model.EzLayerModel.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeLayerModels(
            Action<Gs2.Unity.Gs2MegaField.Model.EzLayerModel[]> callback
        ) {
            return this._domain.SubscribeLayerModels(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2MegaField.Model.EzLayerModel.FromModel).ToArray());
                }
            );
        }

        public void UnsubscribeLayerModels(
            ulong callbackId
        ) {
            this._domain.UnsubscribeLayerModels(
                callbackId
            );
        }

        public Gs2.Unity.Gs2MegaField.Domain.Model.EzLayerModelDomain LayerModel(
            string layerModelName
        ) {
            return new Gs2.Unity.Gs2MegaField.Domain.Model.EzLayerModelDomain(
                _domain.LayerModel(
                    layerModelName
                ),
                this._connection
            );
        }

        [Obsolete("The name has been changed to ModelFuture.")]
        public IFuture<Gs2.Unity.Gs2MegaField.Model.EzAreaModel> Model()
        {
            return ModelFuture();
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2MegaField.Model.EzAreaModel> ModelAsync()
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
            return Gs2.Unity.Gs2MegaField.Model.EzAreaModel.FromModel(
                item
            );
        }
        #endif

        public IFuture<Gs2.Unity.Gs2MegaField.Model.EzAreaModel> ModelFuture()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2MegaField.Model.EzAreaModel> self)
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
                self.OnComplete(Gs2.Unity.Gs2MegaField.Model.EzAreaModel.FromModel(
                    item
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2MegaField.Model.EzAreaModel>(Impl);
        }

        public void Invalidate()
        {
            this._domain.Invalidate();
        }

        public ulong Subscribe(Action<Gs2.Unity.Gs2MegaField.Model.EzAreaModel> callback)
        {
            return this._domain.Subscribe(item => {
                callback.Invoke(item == null ? null : Gs2.Unity.Gs2MegaField.Model.EzAreaModel.FromModel(
                    item
                ));
            });
        }

        public void Unsubscribe(ulong callbackId)
        {
            this._domain.Unsubscribe(callbackId);
        }

        #if UNITY_2017_1_OR_NEWER
        public Gs2Future<ulong> SubscribeWithInitialCallFuture(Action<Gs2.Unity.Gs2MegaField.Model.EzAreaModel> callback)
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
        public async UniTask<ulong> SubscribeWithInitialCallAsync(Action<Gs2.Unity.Gs2MegaField.Model.EzAreaModel> callback)
            #else
        public async Task<ulong> SubscribeWithInitialCallAsync(Action<Gs2.Unity.Gs2MegaField.Model.EzAreaModel> callback)
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
