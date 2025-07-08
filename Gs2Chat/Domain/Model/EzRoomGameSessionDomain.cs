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
using Gs2.Gs2Chat.Domain.Iterator;
using Gs2.Gs2Chat.Request;
using Gs2.Gs2Chat.Result;
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

namespace Gs2.Unity.Gs2Chat.Domain.Model
{

    public partial class EzRoomGameSessionDomain {
        private readonly Gs2.Gs2Chat.Domain.Model.RoomAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.IGameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string? NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;
        public string RoomName => _domain?.RoomName;
        public string Password => _domain?.Password;

        public EzRoomGameSessionDomain(
            Gs2.Gs2Chat.Domain.Model.RoomAccessTokenDomain domain,
            Gs2.Unity.Util.IGameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }

        [Obsolete("The name has been changed to DeleteRoomFuture.")]
        public IFuture<Gs2.Unity.Gs2Chat.Domain.Model.EzRoomGameSessionDomain> DeleteRoom(
        )
        {
            return DeleteRoomFuture(
            );
        }

        public IFuture<Gs2.Unity.Gs2Chat.Domain.Model.EzRoomGameSessionDomain> DeleteRoomFuture(
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Chat.Domain.Model.EzRoomGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.DeleteFuture(
                        new DeleteRoomRequest()
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Chat.Domain.Model.EzRoomGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Chat.Domain.Model.EzRoomGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Chat.Domain.Model.EzRoomGameSessionDomain> DeleteRoomAsync(
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.DeleteAsync(
                    new DeleteRoomRequest()
                )
            );
            return new Gs2.Unity.Gs2Chat.Domain.Model.EzRoomGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        [Obsolete("The name has been changed to PostFuture.")]
        public IFuture<Gs2.Unity.Gs2Chat.Domain.Model.EzMessageGameSessionDomain> Post(
            string metadata,
            int? category = null
        )
        {
            return PostFuture(
                metadata,
                category
            );
        }

        public IFuture<Gs2.Unity.Gs2Chat.Domain.Model.EzMessageGameSessionDomain> PostFuture(
            string metadata,
            int? category = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Chat.Domain.Model.EzMessageGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.PostFuture(
                        new PostRequest()
                            .WithCategory(category)
                            .WithMetadata(metadata)
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Chat.Domain.Model.EzMessageGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Chat.Domain.Model.EzMessageGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Chat.Domain.Model.EzMessageGameSessionDomain> PostAsync(
            string metadata,
            int? category = null
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.PostAsync(
                    new PostRequest()
                        .WithCategory(category)
                        .WithMetadata(metadata)
                )
            );
            return new Gs2.Unity.Gs2Chat.Domain.Model.EzMessageGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        public Gs2Iterator<Gs2.Unity.Gs2Chat.Model.EzMessage> LatestMessages(
            int? category = null
        )
        {
            return new Gs2.Unity.Gs2Chat.Domain.Iterator.EzListLatestMessagesIterator(
                this._domain,
                this._gameSession,
                this._connection,
                category
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Chat.Model.EzMessage> LatestMessagesAsync(
              int? category = null
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Chat.Model.EzMessage>(async (writer, token) =>
            {
                var it = _domain.LatestMessagesAsync(
                    category
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        this._gameSession,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.LatestMessagesAsync(
                                category
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Chat.Model.EzMessage.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeLatestMessages(
            Action<Gs2.Unity.Gs2Chat.Model.EzMessage[]> callback,
            int? category = null
        ) {
            return this._domain.SubscribeLatestMessages(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Chat.Model.EzMessage.FromModel).ToArray());
                },
                category
            );
        }

        public void UnsubscribeLatestMessages(
            ulong callbackId,
            int? category = null
        ) {
            this._domain.UnsubscribeLatestMessages(
                callbackId,
                category
            );
        }

        public void InvalidateLatestMessages(
            int? category = null
        ) {
            this._domain.InvalidateLatestMessages(
                category
            );
        }

        public Gs2Iterator<Gs2.Unity.Gs2Chat.Model.EzMessage> Messages(
            int? category = null
        )
        {
            return new Gs2.Unity.Gs2Chat.Domain.Iterator.EzListMessagesIterator(
                this._domain,
                this._gameSession,
                this._connection,
                category
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Chat.Model.EzMessage> MessagesAsync(
              int? category = null
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Chat.Model.EzMessage>(async (writer, token) =>
            {
                var it = _domain.MessagesAsync(
                    category
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        this._gameSession,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.MessagesAsync(
                                category
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Chat.Model.EzMessage.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeMessages(
            Action<Gs2.Unity.Gs2Chat.Model.EzMessage[]> callback,
            int? category = null
        ) {
            return this._domain.SubscribeMessages(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Chat.Model.EzMessage.FromModel).ToArray());
                },
                category
            );
        }

        public void UnsubscribeMessages(
            ulong callbackId,
            int? category = null
        ) {
            this._domain.UnsubscribeMessages(
                callbackId,
                category
            );
        }

        public void InvalidateMessages(
            int? category = null
        ) {
            this._domain.InvalidateMessages(
                category
            );
        }

        public Gs2.Unity.Gs2Chat.Domain.Model.EzMessageGameSessionDomain Message(
            string messageName
        ) {
            return new Gs2.Unity.Gs2Chat.Domain.Model.EzMessageGameSessionDomain(
                _domain.Message(
                    messageName
                ),
                this._gameSession,
                this._connection
            );
        }

        [Obsolete("The name has been changed to ModelFuture.")]
        public IFuture<Gs2.Unity.Gs2Chat.Model.EzRoom> Model()
        {
            return ModelFuture();
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Chat.Model.EzRoom> ModelAsync()
        {
            var item = await this._connection.RunAsync(
                this._gameSession,
                async () =>
                {
                    return await _domain.ModelAsync();
                }
            );
            if (item == null) {
                return null;
            }
            return Gs2.Unity.Gs2Chat.Model.EzRoom.FromModel(
                item
            );
        }
        #endif

        public IFuture<Gs2.Unity.Gs2Chat.Model.EzRoom> ModelFuture()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Chat.Model.EzRoom> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
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
                self.OnComplete(Gs2.Unity.Gs2Chat.Model.EzRoom.FromModel(
                    item
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Chat.Model.EzRoom>(Impl);
        }

        public void Invalidate()
        {
            this._domain.Invalidate();
        }

        public ulong Subscribe(Action<Gs2.Unity.Gs2Chat.Model.EzRoom> callback)
        {
            return this._domain.Subscribe(item => {
                callback.Invoke(item == null ? null : Gs2.Unity.Gs2Chat.Model.EzRoom.FromModel(
                    item
                ));
            });
        }

        public void Unsubscribe(ulong callbackId)
        {
            this._domain.Unsubscribe(callbackId);
        }

        #if UNITY_2017_1_OR_NEWER
        public Gs2Future<ulong> SubscribeWithInitialCallFuture(Action<Gs2.Unity.Gs2Chat.Model.EzRoom> callback)
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
        public async UniTask<ulong> SubscribeWithInitialCallAsync(Action<Gs2.Unity.Gs2Chat.Model.EzRoom> callback)
            #else
        public async Task<ulong> SubscribeWithInitialCallAsync(Action<Gs2.Unity.Gs2Chat.Model.EzRoom> callback)
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
