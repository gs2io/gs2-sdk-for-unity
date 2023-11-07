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

    public partial class EzUserGameSessionDomain {
        private readonly Gs2.Gs2Datastore.Domain.Model.UserAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public string UploadUrl => _domain.UploadUrl;
        public string FileUrl => _domain.FileUrl;
        public long? ContentLength => _domain.ContentLength;
        public string NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserGameSessionDomain(
            Gs2.Gs2Datastore.Domain.Model.UserAccessTokenDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        [Obsolete("The name has been changed to PrepareUploadFuture.")]
        public IFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> PrepareUpload(
            string name = null,
            string scope = null,
            string contentType = null,
            string[] allowUserIds = null,
            bool? updateIfExists = null
        )
        {
            return PrepareUploadFuture(
                name,
                scope,
                contentType,
                allowUserIds,
                updateIfExists
            );
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> PrepareUploadFuture(
            string name = null,
            string scope = null,
            string contentType = null,
            string[] allowUserIds = null,
            bool? updateIfExists = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> self)
            {
                var future = this._domain.PrepareUploadFuture(
                    new PrepareUploadRequest()
                        .WithName(name)
                        .WithScope(scope)
                        .WithContentType(contentType)
                        .WithAllowUserIds(allowUserIds)
                        .WithUpdateIfExists(updateIfExists)
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain(future.Result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> PrepareUploadAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> PrepareUploadFuture(
        #endif
            string name = null,
            string scope = null,
            string contentType = null,
            string[] allowUserIds = null,
            bool? updateIfExists = null
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.PrepareUploadAsync(
                        new PrepareUploadRequest()
                            .WithName(name)
                            .WithScope(scope)
                            .WithContentType(contentType)
                            .WithAllowUserIds(allowUserIds)
                            .WithUpdateIfExists(updateIfExists)
                            .WithAccessToken(_domain.AccessToken.Token)
                    );
                }
            );
            return new Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> self)
            {
                var future = _domain.PrepareUploadFuture(
                    new PrepareUploadRequest()
                        .WithName(name)
                        .WithScope(scope)
                        .WithContentType(contentType)
                        .WithAllowUserIds(allowUserIds)
                        .WithUpdateIfExists(updateIfExists)
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return _profile.RunFuture(
                    _domain.AccessToken,
                    future,
                    () =>
        			{
                		return future = _domain.PrepareUploadFuture(
                    		new PrepareUploadRequest()
                	        .WithName(name)
                	        .WithScope(scope)
                	        .WithContentType(contentType)
                	        .WithAllowUserIds(allowUserIds)
                	        .WithUpdateIfExists(updateIfExists)
                    	    .WithAccessToken(_domain.AccessToken.Token)
        		        );
        			}
                );
                if (future.Error != null)
                {
                    self.OnError(future.Error);
                    yield break;
                }
                var result = future.Result;
                self.OnComplete(new Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain(result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain>(Impl);
        #endif
        }

        [Obsolete("The name has been changed to PrepareDownloadFuture.")]
        public IFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> PrepareDownload(
            string dataObjectId
        )
        {
            return PrepareDownloadFuture(
                dataObjectId
            );
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> PrepareDownloadFuture(
            string dataObjectId
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> self)
            {
                var future = this._domain.PrepareDownloadFuture(
                    new PrepareDownloadRequest()
                        .WithDataObjectId(dataObjectId)
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain(future.Result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> PrepareDownloadAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> PrepareDownloadFuture(
        #endif
            string dataObjectId
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.PrepareDownloadAsync(
                        new PrepareDownloadRequest()
                            .WithDataObjectId(dataObjectId)
                            .WithAccessToken(_domain.AccessToken.Token)
                    );
                }
            );
            return new Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> self)
            {
                var future = _domain.PrepareDownloadFuture(
                    new PrepareDownloadRequest()
                        .WithDataObjectId(dataObjectId)
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return _profile.RunFuture(
                    _domain.AccessToken,
                    future,
                    () =>
        			{
                		return future = _domain.PrepareDownloadFuture(
                    		new PrepareDownloadRequest()
                	        .WithDataObjectId(dataObjectId)
                    	    .WithAccessToken(_domain.AccessToken.Token)
        		        );
        			}
                );
                if (future.Error != null)
                {
                    self.OnError(future.Error);
                    yield break;
                }
                var result = future.Result;
                self.OnComplete(new Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain(result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain>(Impl);
        #endif
        }

        public class EzDataObjectsIterator : Gs2Iterator<Gs2.Unity.Gs2Datastore.Model.EzDataObject>
        {
            private Gs2Iterator<Gs2.Gs2Datastore.Model.DataObject> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly string _status;
            private readonly Gs2.Gs2Datastore.Domain.Model.UserAccessTokenDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzDataObjectsIterator(
                Gs2Iterator<Gs2.Gs2Datastore.Model.DataObject> it,
        #if !GS2_ENABLE_UNITASK
                string status,
                Gs2.Gs2Datastore.Domain.Model.UserAccessTokenDomain domain,
        #endif
                Gs2.Unity.Util.Profile profile
            )
            {
                _it = it;
        #if !GS2_ENABLE_UNITASK
                _status = status;
                _domain = domain;
        #endif
                _profile = profile;
            }

            public override bool HasNext()
            {
                return _it.HasNext();
            }

            protected override IEnumerator Next(Action<AsyncResult<Gs2.Unity.Gs2Datastore.Model.EzDataObject>> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    _domain.AccessToken,
                    _it,
                    () =>
                    {
                        return _it = _domain.DataObjects(
                            _status
                        );
                    }
                );
        #endif
                callback.Invoke(
                    new AsyncResult<Gs2.Unity.Gs2Datastore.Model.EzDataObject>(
                        _it.Current == null ? null : Gs2.Unity.Gs2Datastore.Model.EzDataObject.FromModel(_it.Current),
                        _it.Error
                    )
                );
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Datastore.Model.EzDataObject> DataObjects(
              string status = null
        )
        {
            return new EzDataObjectsIterator(
                _domain.DataObjects(
                    status
                ),
                _profile
            );
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Datastore.Model.EzDataObject> DataObjectsAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Datastore.Model.EzDataObject> DataObjects(
        #endif
              string status = null
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Datastore.Model.EzDataObject>(async (writer, token) =>
            {
                var it = _domain.DataObjectsAsync(
                    status
                ).GetAsyncEnumerator();
                while(
                    await _profile.RunIteratorAsync(
                        _domain.AccessToken,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.DataObjectsAsync(
                                status
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Datastore.Model.EzDataObject.FromModel(it.Current));
                }
            });
        #else
            return new EzDataObjectsIterator(
                _domain.DataObjects(
                    status
                ),
                status,
                _domain,
                _profile
            );
        #endif
        }

        public ulong SubscribeDataObjects(Action callback) {
            return this._domain.SubscribeDataObjects(callback);
        }

        public void UnsubscribeDataObjects(ulong callbackId) {
            this._domain.UnsubscribeDataObjects(callbackId);
        }

        public Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain DataObject(
            string dataObjectName
        ) {
            return new Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain(
                _domain.DataObject(
                    dataObjectName
                ),
                _profile
            );
        }

    }
}
