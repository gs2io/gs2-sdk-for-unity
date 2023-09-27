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
        private readonly Gs2.Unity.Util.Profile _profile;
        public string NamespaceName => _domain?.NamespaceName;
        public string AreaModelName => _domain?.AreaModelName;

        public EzAreaModelDomain(
            Gs2.Gs2MegaField.Domain.Model.AreaModelDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        public class EzLayerModelsIterator : Gs2Iterator<Gs2.Unity.Gs2MegaField.Model.EzLayerModel>
        {
            private Gs2Iterator<Gs2.Gs2MegaField.Model.LayerModel> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly Gs2.Gs2MegaField.Domain.Model.AreaModelDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzLayerModelsIterator(
                Gs2Iterator<Gs2.Gs2MegaField.Model.LayerModel> it,
        #if !GS2_ENABLE_UNITASK
                Gs2.Gs2MegaField.Domain.Model.AreaModelDomain domain,
        #endif
                Gs2.Unity.Util.Profile profile
            )
            {
                _it = it;
        #if !GS2_ENABLE_UNITASK
                _domain = domain;
        #endif
                _profile = profile;
            }

            public override bool HasNext()
            {
                return _it.HasNext();
            }

            protected override IEnumerator Next(Action<AsyncResult<Gs2.Unity.Gs2MegaField.Model.EzLayerModel>> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    null,
                    _it,
                    () =>
                    {
                        return _it = _domain.LayerModels(
                        );
                    }
                );
        #endif
                callback.Invoke(
                    new AsyncResult<Gs2.Unity.Gs2MegaField.Model.EzLayerModel>(
                        _it.Current == null ? null : Gs2.Unity.Gs2MegaField.Model.EzLayerModel.FromModel(_it.Current),
                        _it.Error
                    )
                );
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2MegaField.Model.EzLayerModel> LayerModels(
        )
        {
            return new EzLayerModelsIterator(
                _domain.LayerModels(
                ),
                _profile
            );
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2MegaField.Model.EzLayerModel> LayerModelsAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2MegaField.Model.EzLayerModel> LayerModels(
        #endif
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2MegaField.Model.EzLayerModel>(async (writer, token) =>
            {
                var it = _domain.LayerModelsAsync(
                ).GetAsyncEnumerator();
                while(
                    await _profile.RunIteratorAsync(
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
        #else
            return new EzLayerModelsIterator(
                _domain.LayerModels(
                ),
                _domain,
                _profile
            );
        #endif
        }

        public ulong SubscribeLayerModels(Action callback) {
            return this._domain.SubscribeLayerModels(callback);
        }

        public void UnsubscribeLayerModels(ulong callbackId) {
            this._domain.UnsubscribeLayerModels(callbackId);
        }

        public Gs2.Unity.Gs2MegaField.Domain.Model.EzLayerModelDomain LayerModel(
            string layerModelName
        ) {
            return new Gs2.Unity.Gs2MegaField.Domain.Model.EzLayerModelDomain(
                _domain.LayerModel(
                    layerModelName
                ),
                _profile
            );
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2MegaField.Model.EzAreaModel> Model()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2MegaField.Model.EzAreaModel> self)
            {
                yield return ModelAsync().ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2MegaField.Model.EzAreaModel>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2MegaField.Model.EzAreaModel> ModelAsync()
        {
            var item = await _profile.RunAsync(
                null,
                async () =>
                {
                    return await _domain.Model();
                }
            );
            if (item == null) {
                return null;
            }
            return Gs2.Unity.Gs2MegaField.Model.EzAreaModel.FromModel(
                item
            );
        }
        #else
        public IFuture<Gs2.Unity.Gs2MegaField.Model.EzAreaModel> Model()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2MegaField.Model.EzAreaModel> self)
            {
                var future = _domain.Model();
                yield return _profile.RunFuture(
                    null,
                    future,
                    () => {
                    	return future = _domain.Model();
                    }
                );
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
        #endif

        public ulong Subscribe(Action<Gs2.Unity.Gs2MegaField.Model.EzAreaModel> callback)
        {
            return this._domain.Subscribe(item => {
                callback.Invoke(Gs2.Unity.Gs2MegaField.Model.EzAreaModel.FromModel(
                    item
                ));
            });
        }

        public void Unsubscribe(ulong callbackId)
        {
            this._domain.Unsubscribe(callbackId);
        }

    }
}
