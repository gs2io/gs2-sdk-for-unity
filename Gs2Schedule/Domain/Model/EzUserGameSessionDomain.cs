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
using Gs2.Gs2Schedule.Domain.Iterator;
using Gs2.Gs2Schedule.Request;
using Gs2.Gs2Schedule.Result;
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

namespace Gs2.Unity.Gs2Schedule.Domain.Model
{

    public partial class EzUserGameSessionDomain {
        private readonly Gs2.Gs2Schedule.Domain.Model.UserAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.IGameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string? NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserGameSessionDomain(
            Gs2.Gs2Schedule.Domain.Model.UserAccessTokenDomain domain,
            Gs2.Unity.Util.IGameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }

        public Gs2Iterator<Gs2.Unity.Gs2Schedule.Model.EzTrigger> Triggers(
        )
        {
            return new Gs2.Unity.Gs2Schedule.Domain.Iterator.EzListTriggersIterator(
                this._domain,
                this._gameSession,
                this._connection
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Schedule.Model.EzTrigger> TriggersAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Schedule.Model.EzTrigger>(async (writer, token) =>
            {
                var it = _domain.TriggersAsync(
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        this._gameSession,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.TriggersAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Schedule.Model.EzTrigger.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeTriggers(
            Action<Gs2.Unity.Gs2Schedule.Model.EzTrigger[]> callback
        ) {
            return this._domain.SubscribeTriggers(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Schedule.Model.EzTrigger.FromModel).ToArray());
                }
            );
        }

        public void UnsubscribeTriggers(
            ulong callbackId
        ) {
            this._domain.UnsubscribeTriggers(
                callbackId
            );
        }

        public void InvalidateTriggers(
        ) {
            this._domain.InvalidateTriggers(
            );
        }

        public Gs2Iterator<Gs2.Unity.Gs2Schedule.Model.EzEvent> Events(
        )
        {
            return new Gs2.Unity.Gs2Schedule.Domain.Iterator.EzListEventsIterator(
                this._domain,
                this._gameSession,
                this._connection
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Schedule.Model.EzEvent> EventsAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Schedule.Model.EzEvent>(async (writer, token) =>
            {
                var it = _domain.EventsAsync(
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        this._gameSession,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.EventsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Schedule.Model.EzEvent.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeEvents(
            Action<Gs2.Unity.Gs2Schedule.Model.EzEvent[]> callback
        ) {
            return this._domain.SubscribeEvents(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Schedule.Model.EzEvent.FromModel).ToArray());
                }
            );
        }

        public void UnsubscribeEvents(
            ulong callbackId
        ) {
            this._domain.UnsubscribeEvents(
                callbackId
            );
        }

        public void InvalidateEvents(
        ) {
            this._domain.InvalidateEvents(
            );
        }

        public Gs2.Unity.Gs2Schedule.Domain.Model.EzTriggerGameSessionDomain Trigger(
            string triggerName
        ) {
            return new Gs2.Unity.Gs2Schedule.Domain.Model.EzTriggerGameSessionDomain(
                _domain.Trigger(
                    triggerName
                ),
                this._gameSession,
                this._connection
            );
        }

        public Gs2.Unity.Gs2Schedule.Domain.Model.EzEventGameSessionDomain Event(
            string eventName
        ) {
            return new Gs2.Unity.Gs2Schedule.Domain.Model.EzEventGameSessionDomain(
                _domain.Event(
                    eventName
                ),
                this._gameSession,
                this._connection
            );
        }

    }
}
