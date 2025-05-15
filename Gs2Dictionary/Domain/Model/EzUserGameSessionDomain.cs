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
using Gs2.Gs2Dictionary.Domain.Iterator;
using Gs2.Gs2Dictionary.Request;
using Gs2.Gs2Dictionary.Result;
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

namespace Gs2.Unity.Gs2Dictionary.Domain.Model
{

    public partial class EzUserGameSessionDomain {
        private readonly Gs2.Gs2Dictionary.Domain.Model.UserAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.IGameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string? NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserGameSessionDomain(
            Gs2.Gs2Dictionary.Domain.Model.UserAccessTokenDomain domain,
            Gs2.Unity.Util.IGameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }

        [Obsolete("The name has been changed to AddLikesFuture.")]
        public IFuture<Gs2.Unity.Gs2Dictionary.Domain.Model.EzLikeGameSessionDomain[]> AddLikes(
            string[] entryModelNames = null
        )
        {
            return AddLikesFuture(
                entryModelNames
            );
        }

        public IFuture<Gs2.Unity.Gs2Dictionary.Domain.Model.EzLikeGameSessionDomain[]> AddLikesFuture(
            string[] entryModelNames = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Dictionary.Domain.Model.EzLikeGameSessionDomain[]> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.AddLikesFuture(
                        new AddLikesRequest()
                            .WithEntryModelNames(entryModelNames)
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(future.Result.Select(v => new Gs2.Unity.Gs2Dictionary.Domain.Model.EzLikeGameSessionDomain(
                    v,
                    this._gameSession,
                    this._connection
                )).ToArray());
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Dictionary.Domain.Model.EzLikeGameSessionDomain[]>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Dictionary.Domain.Model.EzLikeGameSessionDomain[]> AddLikesAsync(
            string[] entryModelNames = null
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.AddLikesAsync(
                    new AddLikesRequest()
                        .WithEntryModelNames(entryModelNames)
                )
            );
            return result.Select(v => new Gs2.Unity.Gs2Dictionary.Domain.Model.EzLikeGameSessionDomain(
                v,
                this._gameSession,
                this._connection
            )).ToArray();
        }
        #endif

        [Obsolete("The name has been changed to DeleteLikesFuture.")]
        public IFuture<Gs2.Unity.Gs2Dictionary.Domain.Model.EzLikeGameSessionDomain[]> DeleteLikes(
            string[] entryModelNames = null
        )
        {
            return DeleteLikesFuture(
                entryModelNames
            );
        }

        public IFuture<Gs2.Unity.Gs2Dictionary.Domain.Model.EzLikeGameSessionDomain[]> DeleteLikesFuture(
            string[] entryModelNames = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Dictionary.Domain.Model.EzLikeGameSessionDomain[]> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.DeleteLikesFuture(
                        new DeleteLikesRequest()
                            .WithEntryModelNames(entryModelNames)
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(future.Result.Select(v => new Gs2.Unity.Gs2Dictionary.Domain.Model.EzLikeGameSessionDomain(
                    v,
                    this._gameSession,
                    this._connection
                )).ToArray());
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Dictionary.Domain.Model.EzLikeGameSessionDomain[]>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Dictionary.Domain.Model.EzLikeGameSessionDomain[]> DeleteLikesAsync(
            string[] entryModelNames = null
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.DeleteLikesAsync(
                    new DeleteLikesRequest()
                        .WithEntryModelNames(entryModelNames)
                )
            );
            return result.Select(v => new Gs2.Unity.Gs2Dictionary.Domain.Model.EzLikeGameSessionDomain(
                v,
                this._gameSession,
                this._connection
            )).ToArray();
        }
        #endif

        public Gs2Iterator<Gs2.Unity.Gs2Dictionary.Model.EzEntry> Entries(
        )
        {
            return new Gs2.Unity.Gs2Dictionary.Domain.Iterator.EzListEntriesIterator(
                this._domain,
                this._gameSession,
                this._connection
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Dictionary.Model.EzEntry> EntriesAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Dictionary.Model.EzEntry>(async (writer, token) =>
            {
                var it = _domain.EntriesAsync(
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        this._gameSession,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.EntriesAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Dictionary.Model.EzEntry.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeEntries(
            Action<Gs2.Unity.Gs2Dictionary.Model.EzEntry[]> callback
        ) {
            return this._domain.SubscribeEntries(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Dictionary.Model.EzEntry.FromModel).ToArray());
                }
            );
        }

        public void UnsubscribeEntries(
            ulong callbackId
        ) {
            this._domain.UnsubscribeEntries(
                callbackId
            );
        }

        public void InvalidateEntries(
        ) {
            this._domain.InvalidateEntries(
            );
        }

        public Gs2Iterator<Gs2.Unity.Gs2Dictionary.Model.EzLike> Likes(
        )
        {
            return new Gs2.Unity.Gs2Dictionary.Domain.Iterator.EzListLikesIterator(
                this._domain,
                this._gameSession,
                this._connection
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Dictionary.Model.EzLike> LikesAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Dictionary.Model.EzLike>(async (writer, token) =>
            {
                var it = _domain.LikesAsync(
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        this._gameSession,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.LikesAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Dictionary.Model.EzLike.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeLikes(
            Action<Gs2.Unity.Gs2Dictionary.Model.EzLike[]> callback
        ) {
            return this._domain.SubscribeLikes(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Dictionary.Model.EzLike.FromModel).ToArray());
                }
            );
        }

        public void UnsubscribeLikes(
            ulong callbackId
        ) {
            this._domain.UnsubscribeLikes(
                callbackId
            );
        }

        public void InvalidateLikes(
        ) {
            this._domain.InvalidateLikes(
            );
        }

        public Gs2.Unity.Gs2Dictionary.Domain.Model.EzEntryGameSessionDomain Entry(
            string entryModelName
        ) {
            return new Gs2.Unity.Gs2Dictionary.Domain.Model.EzEntryGameSessionDomain(
                _domain.Entry(
                    entryModelName
                ),
                this._gameSession,
                this._connection
            );
        }

        public Gs2.Unity.Gs2Dictionary.Domain.Model.EzLikeGameSessionDomain Like(
            string entryModelName
        ) {
            return new Gs2.Unity.Gs2Dictionary.Domain.Model.EzLikeGameSessionDomain(
                _domain.Like(
                    entryModelName
                ),
                this._gameSession,
                this._connection
            );
        }

    }
}
