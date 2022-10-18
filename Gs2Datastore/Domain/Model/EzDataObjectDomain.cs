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
        private readonly Gs2.Unity.Util.Profile _profile;
        public string UploadUrl => _domain.UploadUrl;
        public string FileUrl => _domain.FileUrl;
        public long? ContentLength => _domain.ContentLength;
        public string NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;
        public string DataObjectName => _domain?.DataObjectName;

        public EzDataObjectDomain(
            Gs2.Gs2Datastore.Domain.Model.DataObjectDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectDomain> PrepareDownloadByUserIdAndDataObjectName(
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectDomain> self)
            {
                yield return PrepareDownloadByUserIdAndDataObjectNameAsync(
                ).ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectDomain> PrepareDownloadByUserIdAndDataObjectNameAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectDomain> PrepareDownloadByUserIdAndDataObjectName(
        #endif
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                null,
                async () =>
                {
                    return await _domain.PrepareDownloadByUserIdAndNameAsync(
                        new PrepareDownloadByUserIdAndDataObjectNameRequest()
                    );
                }
            );
            return new Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectDomain> self)
            {
                var future = _domain.PrepareDownloadByUserIdAndName(
                    new PrepareDownloadByUserIdAndDataObjectNameRequest()
                );
                yield return _profile.RunFuture(
                    null,
                    future,
                    () =>
        			{
                		return future = _domain.PrepareDownloadByUserIdAndName(
                    		new PrepareDownloadByUserIdAndDataObjectNameRequest()
        		        );
        			}
                );
                if (future.Error != null)
                {
                    self.OnError(future.Error);
                    yield break;
                }
                var result = future.Result;
                self.OnComplete(new Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectDomain(result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectDomain>(Impl);
        #endif
        }

        public class EzDataObjectHistoriesIterator : Gs2Iterator<Gs2.Unity.Gs2Datastore.Model.EzDataObjectHistory>
        {
            private Gs2Iterator<Gs2.Gs2Datastore.Model.DataObjectHistory> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly Gs2.Gs2Datastore.Domain.Model.DataObjectDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzDataObjectHistoriesIterator(
                Gs2Iterator<Gs2.Gs2Datastore.Model.DataObjectHistory> it,
        #if !GS2_ENABLE_UNITASK
                Gs2.Gs2Datastore.Domain.Model.DataObjectDomain domain,
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

            protected override IEnumerator Next(Action<Gs2.Unity.Gs2Datastore.Model.EzDataObjectHistory> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    null,
                    _it,
                    () =>
                    {
                        _it = _domain.DataObjectHistories(
                        );
                    }
                );
        #endif
                callback.Invoke(_it.Current == null ? null : Gs2.Unity.Gs2Datastore.Model.EzDataObjectHistory.FromModel(_it.Current));
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Datastore.Model.EzDataObjectHistory> DataObjectHistories(
        )
        {
            return new EzDataObjectHistoriesIterator(
                _domain.DataObjectHistories(
                ),
                _profile
            );
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Datastore.Model.EzDataObjectHistory> DataObjectHistoriesAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Datastore.Model.EzDataObjectHistory> DataObjectHistories(
        #endif
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Datastore.Model.EzDataObjectHistory>(async (writer, token) =>
            {
                var it = _domain.DataObjectHistoriesAsync(
                ).GetAsyncEnumerator();
                while(
                    await _profile.RunIteratorAsync(
                        null,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.DataObjectHistoriesAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Datastore.Model.EzDataObjectHistory.FromModel(it.Current));
                }
            });
        #else
            return new EzDataObjectHistoriesIterator(
                _domain.DataObjectHistories(
                ),
                _domain,
                _profile
            );
        #endif
        }

        public Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectHistoryDomain DataObjectHistory(
            string generation
        ) {
            return new Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectHistoryDomain(
                _domain.DataObjectHistory(
                    generation
                ),
                _profile
            );
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Datastore.Model.EzDataObject> Model()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Datastore.Model.EzDataObject> self)
            {
                yield return ModelAsync().ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Datastore.Model.EzDataObject>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Datastore.Model.EzDataObject> ModelAsync()
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
            return Gs2.Unity.Gs2Datastore.Model.EzDataObject.FromModel(
                item
            );
        }
        #else
        public IFuture<Gs2.Unity.Gs2Datastore.Model.EzDataObject> Model()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Datastore.Model.EzDataObject> self)
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
                self.OnComplete(Gs2.Unity.Gs2Datastore.Model.EzDataObject.FromModel(
                    item
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Datastore.Model.EzDataObject>(Impl);
        }
        #endif

    }
}
