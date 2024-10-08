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
using Gs2.Gs2Datastore.Domain.Iterator;
using Gs2.Gs2Datastore.Request;
using Gs2.Gs2Datastore.Result;
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

namespace Gs2.Unity.Gs2Datastore.Domain.Model
{

    public partial class EzDataObjectDomain {
        private readonly Gs2.Gs2Datastore.Domain.Model.DataObjectDomain _domain;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string? UploadUrl => _domain.UploadUrl;
        public string? FileUrl => _domain.FileUrl;
        public long? ContentLength => _domain.ContentLength;
        public string? NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;
        public string DataObjectName => _domain?.DataObjectName;

        public EzDataObjectDomain(
            Gs2.Gs2Datastore.Domain.Model.DataObjectDomain domain,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._connection = connection;
        }

        [Obsolete("The name has been changed to PrepareDownloadByUserIdAndDataObjectNameFuture.")]
        public IFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectDomain> PrepareDownloadByUserIdAndDataObjectName(
        )
        {
            return PrepareDownloadByUserIdAndDataObjectNameFuture(
            );
        }

        public IFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectDomain> PrepareDownloadByUserIdAndDataObjectNameFuture(
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectDomain> self)
            {
                var future = this._connection.RunFuture(
                    null,
                    () => this._domain.PrepareDownloadByUserIdAndDataObjectNameFuture(
                        new PrepareDownloadByUserIdAndDataObjectNameRequest()
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectDomain(
                    future.Result,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectDomain> PrepareDownloadByUserIdAndDataObjectNameAsync(
        ) {
            var result = await this._connection.RunAsync(
                null,
                () => this._domain.PrepareDownloadByUserIdAndDataObjectNameAsync(
                    new PrepareDownloadByUserIdAndDataObjectNameRequest()
                )
            );
            return new Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectDomain(
                result,
                this._connection
            );
        }
        #endif

        public Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectHistoryDomain DataObjectHistory(
            string generation
        ) {
            return new Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectHistoryDomain(
                _domain.DataObjectHistory(
                    generation
                ),
                this._connection
            );
        }

        [Obsolete("The name has been changed to ModelFuture.")]
        public IFuture<Gs2.Unity.Gs2Datastore.Model.EzDataObject> Model()
        {
            return ModelFuture();
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Datastore.Model.EzDataObject> ModelAsync()
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
            return Gs2.Unity.Gs2Datastore.Model.EzDataObject.FromModel(
                item
            );
        }
        #endif

        public IFuture<Gs2.Unity.Gs2Datastore.Model.EzDataObject> ModelFuture()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Datastore.Model.EzDataObject> self)
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
                self.OnComplete(Gs2.Unity.Gs2Datastore.Model.EzDataObject.FromModel(
                    item
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Datastore.Model.EzDataObject>(Impl);
        }

        public void Invalidate()
        {
            this._domain.Invalidate();
        }

        public ulong Subscribe(Action<Gs2.Unity.Gs2Datastore.Model.EzDataObject> callback)
        {
            return this._domain.Subscribe(item => {
                callback.Invoke(item == null ? null : Gs2.Unity.Gs2Datastore.Model.EzDataObject.FromModel(
                    item
                ));
            });
        }

        public void Unsubscribe(ulong callbackId)
        {
            this._domain.Unsubscribe(callbackId);
        }

        #if UNITY_2017_1_OR_NEWER
        public Gs2Future<ulong> SubscribeWithInitialCallFuture(Action<Gs2.Unity.Gs2Datastore.Model.EzDataObject> callback)
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
        public async UniTask<ulong> SubscribeWithInitialCallAsync(Action<Gs2.Unity.Gs2Datastore.Model.EzDataObject> callback)
            #else
        public async Task<ulong> SubscribeWithInitialCallAsync(Action<Gs2.Unity.Gs2Datastore.Model.EzDataObject> callback)
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
