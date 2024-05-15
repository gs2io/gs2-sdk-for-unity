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
        private readonly Gs2.Unity.Util.IGameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string? UploadUrl => _domain.UploadUrl;
        public string? FileUrl => _domain.FileUrl;
        public long? ContentLength => _domain.ContentLength;
        public string? NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserGameSessionDomain(
            Gs2.Gs2Datastore.Domain.Model.UserAccessTokenDomain domain,
            Gs2.Unity.Util.IGameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }

        [Obsolete("The name has been changed to PrepareUploadFuture.")]
        public IFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> PrepareUpload(
            string? name = null,
            string? scope = null,
            string? contentType = null,
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

        public IFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> PrepareUploadFuture(
            string? name = null,
            string? scope = null,
            string? contentType = null,
            string[] allowUserIds = null,
            bool? updateIfExists = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.PrepareUploadFuture(
                        new PrepareUploadRequest()
                            .WithName(name)
                            .WithScope(scope)
                            .WithContentType(contentType)
                            .WithAllowUserIds(allowUserIds)
                            .WithUpdateIfExists(updateIfExists)
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> PrepareUploadAsync(
            string? name = null,
            string? scope = null,
            string? contentType = null,
            string[] allowUserIds = null,
            bool? updateIfExists = null
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.PrepareUploadAsync(
                    new PrepareUploadRequest()
                        .WithName(name)
                        .WithScope(scope)
                        .WithContentType(contentType)
                        .WithAllowUserIds(allowUserIds)
                        .WithUpdateIfExists(updateIfExists)
                )
            );
            return new Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        [Obsolete("The name has been changed to PrepareDownloadFuture.")]
        public IFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> PrepareDownload(
            string dataObjectId
        )
        {
            return PrepareDownloadFuture(
                dataObjectId
            );
        }

        public IFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> PrepareDownloadFuture(
            string dataObjectId
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.PrepareDownloadFuture(
                        new PrepareDownloadRequest()
                            .WithDataObjectId(dataObjectId)
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain> PrepareDownloadAsync(
            string dataObjectId
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.PrepareDownloadAsync(
                    new PrepareDownloadRequest()
                        .WithDataObjectId(dataObjectId)
                )
            );
            return new Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        public Gs2Iterator<Gs2.Unity.Gs2Datastore.Model.EzDataObject> DataObjects(
            string? status = null
        )
        {
            return new Gs2.Unity.Gs2Datastore.Domain.Iterator.EzListMyDataObjectsIterator(
                this._domain,
                this._gameSession,
                this._connection,
                status
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Datastore.Model.EzDataObject> DataObjectsAsync(
              string? status = null
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Datastore.Model.EzDataObject>(async (writer, token) =>
            {
                var it = _domain.DataObjectsAsync(
                    status
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        this._gameSession,
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
        }
        #endif

        public ulong SubscribeDataObjects(
            Action<Gs2.Unity.Gs2Datastore.Model.EzDataObject[]> callback,
            string? status = null
        ) {
            return this._domain.SubscribeDataObjects(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Datastore.Model.EzDataObject.FromModel).ToArray());
                },
                status
            );
        }

        public void UnsubscribeDataObjects(
            ulong callbackId,
            string? status = null
        ) {
            this._domain.UnsubscribeDataObjects(
                callbackId,
                status
            );
        }

        public Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain DataObject(
            string dataObjectName
        ) {
            return new Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectGameSessionDomain(
                _domain.DataObject(
                    dataObjectName
                ),
                this._gameSession,
                this._connection
            );
        }

    }
}
