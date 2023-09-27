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

    public partial class EzDataObjectGameSessionDomain {
        private readonly Gs2.Gs2Datastore.Domain.Model.DataObjectAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public string UploadUrl => _domain.UploadUrl;
        public string FileUrl => _domain.FileUrl;
        public long? ContentLength => _domain.ContentLength;
        public string NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;
        public string DataObjectName => _domain?.DataObjectName;

        public EzDataObjectGameSessionDomain(
            Gs2.Gs2Datastore.Domain.Model.DataObjectAccessTokenDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> UpdateDataObject(
              string scope = null,
              string[] allowUserIds = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> self)
            {
                yield return UpdateDataObjectAsync(
                    scope,
                    allowUserIds
                ).ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> UpdateDataObjectAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> UpdateDataObject(
        #endif
              string scope = null,
              string[] allowUserIds = null
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.UpdateAsync(
                        new UpdateDataObjectRequest()
                            .WithScope(scope)
                            .WithAllowUserIds(allowUserIds)
                            .WithAccessToken(_domain.AccessToken.Token)
                    );
                }
            );
            return new Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> self)
            {
                var future = _domain.Update(
                    new UpdateDataObjectRequest()
                        .WithScope(scope)
                        .WithAllowUserIds(allowUserIds)
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return _profile.RunFuture(
                    _domain.AccessToken,
                    future,
                    () =>
        			{
                		return future = _domain.Update(
                    		new UpdateDataObjectRequest()
                	        .WithScope(scope)
                	        .WithAllowUserIds(allowUserIds)
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

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> PrepareReUpload(
              string contentType = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> self)
            {
                yield return PrepareReUploadAsync(
                    contentType
                ).ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> PrepareReUploadAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> PrepareReUpload(
        #endif
              string contentType = null
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.PrepareReUploadAsync(
                        new PrepareReUploadRequest()
                            .WithContentType(contentType)
                            .WithAccessToken(_domain.AccessToken.Token)
                    );
                }
            );
            return new Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> self)
            {
                var future = _domain.PrepareReUpload(
                    new PrepareReUploadRequest()
                        .WithContentType(contentType)
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return _profile.RunFuture(
                    _domain.AccessToken,
                    future,
                    () =>
        			{
                		return future = _domain.PrepareReUpload(
                    		new PrepareReUploadRequest()
                	        .WithContentType(contentType)
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

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> DoneUpload(
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> self)
            {
                yield return DoneUploadAsync(
                ).ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> DoneUploadAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> DoneUpload(
        #endif
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.DoneUploadAsync(
                        new DoneUploadRequest()
                            .WithAccessToken(_domain.AccessToken.Token)
                    );
                }
            );
            return new Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> self)
            {
                var future = _domain.DoneUpload(
                    new DoneUploadRequest()
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return _profile.RunFuture(
                    _domain.AccessToken,
                    future,
                    () =>
        			{
                		return future = _domain.DoneUpload(
                    		new DoneUploadRequest()
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

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> DeleteDataObject(
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> self)
            {
                yield return DeleteDataObjectAsync(
                ).ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> DeleteDataObjectAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> DeleteDataObject(
        #endif
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.DeleteAsync(
                        new DeleteDataObjectRequest()
                            .WithAccessToken(_domain.AccessToken.Token)
                    );
                }
            );
            return new Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> self)
            {
                var future = _domain.Delete(
                    new DeleteDataObjectRequest()
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return _profile.RunFuture(
                    _domain.AccessToken,
                    future,
                    () =>
        			{
                		return future = _domain.Delete(
                    		new DeleteDataObjectRequest()
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

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> PrepareDownloadOwnData(
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> self)
            {
                yield return PrepareDownloadOwnDataAsync(
                ).ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> PrepareDownloadOwnDataAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> PrepareDownloadOwnData(
        #endif
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.PrepareDownloadOwnDataAsync(
                        new PrepareDownloadOwnDataRequest()
                            .WithAccessToken(_domain.AccessToken.Token)
                    );
                }
            );
            return new Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> self)
            {
                var future = _domain.PrepareDownloadOwnData(
                    new PrepareDownloadOwnDataRequest()
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return _profile.RunFuture(
                    _domain.AccessToken,
                    future,
                    () =>
        			{
                		return future = _domain.PrepareDownloadOwnData(
                    		new PrepareDownloadOwnDataRequest()
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

        public class EzDataObjectHistoriesIterator : Gs2Iterator<Gs2.Unity.Gs2Datastore.Model.EzDataObjectHistory>
        {
            private Gs2Iterator<Gs2.Gs2Datastore.Model.DataObjectHistory> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly Gs2.Gs2Datastore.Domain.Model.DataObjectAccessTokenDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzDataObjectHistoriesIterator(
                Gs2Iterator<Gs2.Gs2Datastore.Model.DataObjectHistory> it,
        #if !GS2_ENABLE_UNITASK
                Gs2.Gs2Datastore.Domain.Model.DataObjectAccessTokenDomain domain,
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

            protected override IEnumerator Next(Action<AsyncResult<Gs2.Unity.Gs2Datastore.Model.EzDataObjectHistory>> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    _domain.AccessToken,
                    _it,
                    () =>
                    {
                        return _it = _domain.DataObjectHistories(
                        );
                    }
                );
        #endif
                callback.Invoke(
                    new AsyncResult<Gs2.Unity.Gs2Datastore.Model.EzDataObjectHistory>(
                        _it.Current == null ? null : Gs2.Unity.Gs2Datastore.Model.EzDataObjectHistory.FromModel(_it.Current),
                        _it.Error
                    )
                );
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
                        _domain.AccessToken,
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

        public ulong SubscribeDataObjectHistories(Action callback) {
            return this._domain.SubscribeDataObjectHistories(callback);
        }

        public void UnsubscribeDataObjectHistories(ulong callbackId) {
            this._domain.UnsubscribeDataObjectHistories(callbackId);
        }

        public Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectHistoryGameSessionDomain DataObjectHistory(
            string generation
        ) {
            return new Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectHistoryGameSessionDomain(
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
                _domain.AccessToken,
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
                    _domain.AccessToken,
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

        public ulong Subscribe(Action<Gs2.Unity.Gs2Datastore.Model.EzDataObject> callback)
        {
            return this._domain.Subscribe(item => {
                callback.Invoke(Gs2.Unity.Gs2Datastore.Model.EzDataObject.FromModel(
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
