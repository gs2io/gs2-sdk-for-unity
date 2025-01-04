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
using Gs2.Gs2Inbox.Domain.Iterator;
using Gs2.Gs2Inbox.Request;
using Gs2.Gs2Inbox.Result;
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

namespace Gs2.Unity.Gs2Inbox.Domain.Model
{

    public partial class EzUserGameSessionDomain {
        private readonly Gs2.Gs2Inbox.Domain.Model.UserAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.IGameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string? NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserGameSessionDomain(
            Gs2.Gs2Inbox.Domain.Model.UserAccessTokenDomain domain,
            Gs2.Unity.Util.IGameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }

        [Obsolete("The name has been changed to ReceiveGlobalMessageFuture.")]
        public IFuture<Gs2.Unity.Gs2Inbox.Domain.Model.EzMessageGameSessionDomain[]> ReceiveGlobalMessage(
        )
        {
            return ReceiveGlobalMessageFuture(
            );
        }

        public IFuture<Gs2.Unity.Gs2Inbox.Domain.Model.EzMessageGameSessionDomain[]> ReceiveGlobalMessageFuture(
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Inbox.Domain.Model.EzMessageGameSessionDomain[]> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.ReceiveGlobalMessageFuture(
                        new ReceiveGlobalMessageRequest()
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(future.Result.Select(v => new Gs2.Unity.Gs2Inbox.Domain.Model.EzMessageGameSessionDomain(
                    v,
                    this._gameSession,
                    this._connection
                )).ToArray());
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Inbox.Domain.Model.EzMessageGameSessionDomain[]>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Inbox.Domain.Model.EzMessageGameSessionDomain[]> ReceiveGlobalMessageAsync(
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.ReceiveGlobalMessageAsync(
                    new ReceiveGlobalMessageRequest()
                )
            );
            return result.Select(v => new Gs2.Unity.Gs2Inbox.Domain.Model.EzMessageGameSessionDomain(
                v,
                this._gameSession,
                this._connection
            )).ToArray();
        }
        #endif

        public Gs2Iterator<Gs2.Unity.Gs2Inbox.Model.EzMessage> Messages(
            bool? isRead = null
        )
        {
            return new Gs2.Unity.Gs2Inbox.Domain.Iterator.EzListIterator(
                this._domain,
                this._gameSession,
                this._connection,
                isRead
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Inbox.Model.EzMessage> MessagesAsync(
              bool? isRead = null
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Inbox.Model.EzMessage>(async (writer, token) =>
            {
                var it = _domain.MessagesAsync(
                    isRead
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
                                isRead
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Inbox.Model.EzMessage.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeMessages(
            Action<Gs2.Unity.Gs2Inbox.Model.EzMessage[]> callback,
            bool? isRead = null
        ) {
            return this._domain.SubscribeMessages(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Inbox.Model.EzMessage.FromModel).ToArray());
                },
                isRead
            );
        }

        public void UnsubscribeMessages(
            ulong callbackId,
            bool? isRead = null
        ) {
            this._domain.UnsubscribeMessages(
                callbackId,
                isRead
            );
        }

        public void InvalidateMessages(
            bool? isRead = null
        ) {
            this._domain.InvalidateMessages(
                isRead
            );
        }

        public Gs2.Unity.Gs2Inbox.Domain.Model.EzMessageGameSessionDomain Message(
            string messageName
        ) {
            return new Gs2.Unity.Gs2Inbox.Domain.Model.EzMessageGameSessionDomain(
                _domain.Message(
                    messageName
                ),
                this._gameSession,
                this._connection
            );
        }

    }
}
