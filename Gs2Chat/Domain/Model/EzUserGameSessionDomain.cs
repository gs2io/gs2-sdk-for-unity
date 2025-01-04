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

    public partial class EzUserGameSessionDomain {
        private readonly Gs2.Gs2Chat.Domain.Model.UserAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.IGameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string? NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserGameSessionDomain(
            Gs2.Gs2Chat.Domain.Model.UserAccessTokenDomain domain,
            Gs2.Unity.Util.IGameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }

        [Obsolete("The name has been changed to CreateRoomFuture.")]
        public IFuture<Gs2.Unity.Gs2Chat.Domain.Model.EzRoomGameSessionDomain> CreateRoom(
            string? name = null,
            string? metadata = null,
            string? password = null,
            string[] whiteListUserIds = null
        )
        {
            return CreateRoomFuture(
                name,
                metadata,
                password,
                whiteListUserIds
            );
        }

        public IFuture<Gs2.Unity.Gs2Chat.Domain.Model.EzRoomGameSessionDomain> CreateRoomFuture(
            string? name = null,
            string? metadata = null,
            string? password = null,
            string[] whiteListUserIds = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Chat.Domain.Model.EzRoomGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.CreateRoomFuture(
                        new CreateRoomRequest()
                            .WithName(name)
                            .WithMetadata(metadata)
                            .WithPassword(password)
                            .WithWhiteListUserIds(whiteListUserIds)
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
        public async UniTask<Gs2.Unity.Gs2Chat.Domain.Model.EzRoomGameSessionDomain> CreateRoomAsync(
            string? name = null,
            string? metadata = null,
            string? password = null,
            string[] whiteListUserIds = null
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.CreateRoomAsync(
                    new CreateRoomRequest()
                        .WithName(name)
                        .WithMetadata(metadata)
                        .WithPassword(password)
                        .WithWhiteListUserIds(whiteListUserIds)
                )
            );
            return new Gs2.Unity.Gs2Chat.Domain.Model.EzRoomGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        public Gs2Iterator<Gs2.Unity.Gs2Chat.Model.EzSubscribe> Subscribes(
        )
        {
            return new Gs2.Unity.Gs2Chat.Domain.Iterator.EzListSubscribeRoomsIterator(
                this._domain,
                this._gameSession,
                this._connection
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Chat.Model.EzSubscribe> SubscribesAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Chat.Model.EzSubscribe>(async (writer, token) =>
            {
                var it = _domain.SubscribesAsync(
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        this._gameSession,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.SubscribesAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Chat.Model.EzSubscribe.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeSubscribes(
            Action<Gs2.Unity.Gs2Chat.Model.EzSubscribe[]> callback
        ) {
            return this._domain.SubscribeSubscribes(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Chat.Model.EzSubscribe.FromModel).ToArray());
                }
            );
        }

        public void UnsubscribeSubscribes(
            ulong callbackId
        ) {
            this._domain.UnsubscribeSubscribes(
                callbackId
            );
        }

        public void InvalidateSubscribes(
        ) {
            this._domain.InvalidateSubscribes(
            );
        }

        public Gs2.Unity.Gs2Chat.Domain.Model.EzRoomGameSessionDomain Room(
            string roomName,
            string? password = null
        ) {
            return new Gs2.Unity.Gs2Chat.Domain.Model.EzRoomGameSessionDomain(
                _domain.Room(
                    roomName,
                    password
                ),
                this._gameSession,
                this._connection
            );
        }

        public Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain Subscribe(
            string roomName
        ) {
            return new Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain(
                _domain.Subscribe(
                    roomName
                ),
                this._gameSession,
                this._connection
            );
        }

    }
}
