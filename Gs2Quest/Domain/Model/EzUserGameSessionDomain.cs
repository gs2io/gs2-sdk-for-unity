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
using Gs2.Gs2Quest.Domain.Iterator;
using Gs2.Gs2Quest.Request;
using Gs2.Gs2Quest.Result;
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

namespace Gs2.Unity.Gs2Quest.Domain.Model
{

    public partial class EzUserGameSessionDomain {
        private readonly Gs2.Gs2Quest.Domain.Model.UserAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.IGameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string? NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserGameSessionDomain(
            Gs2.Gs2Quest.Domain.Model.UserAccessTokenDomain domain,
            Gs2.Unity.Util.IGameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }

        [Obsolete("The name has been changed to StartFuture.")]
        public IFuture<Gs2.Unity.Core.Domain.EzTransactionDomain> Start(
            string questGroupName,
            string questName,
            bool? force = null,
            Gs2.Unity.Gs2Quest.Model.EzConfig[] config = null,
            bool speculativeExecute = true
        )
        {
            return StartFuture(
                questGroupName,
                questName,
                force,
                config,
                speculativeExecute
            );
        }

        public IFuture<Gs2.Unity.Core.Domain.EzTransactionDomain> StartFuture(
            string questGroupName,
            string questName,
            bool? force = null,
            Gs2.Unity.Gs2Quest.Model.EzConfig[] config = null,
            bool speculativeExecute = true
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Core.Domain.EzTransactionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.StartFuture(
                        new StartRequest()
                            .WithQuestGroupName(questGroupName)
                            .WithQuestName(questName)
                            .WithForce(force)
                            .WithConfig(config?.Select(v => v.ToModel()).ToArray()),
                        speculativeExecute
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(future.Result == null ? null : new Gs2.Unity.Core.Domain.EzTransactionDomain(future.Result));
            }
            return new Gs2InlineFuture<Gs2.Unity.Core.Domain.EzTransactionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Core.Domain.EzTransactionDomain> StartAsync(
            string questGroupName,
            string questName,
            bool? force = null,
            Gs2.Unity.Gs2Quest.Model.EzConfig[] config = null,
            bool speculativeExecute = true
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.StartAsync(
                    new StartRequest()
                        .WithQuestGroupName(questGroupName)
                        .WithQuestName(questName)
                        .WithForce(force)
                        .WithConfig(config?.Select(v => v.ToModel()).ToArray()),
                    speculativeExecute
                )
            );
            return result == null ? null : new Gs2.Unity.Core.Domain.EzTransactionDomain(result);
        }
        #endif

        public Gs2Iterator<Gs2.Unity.Gs2Quest.Model.EzCompletedQuestList> CompletedQuestLists(
        )
        {
            return new Gs2.Unity.Gs2Quest.Domain.Iterator.EzDescribeCompletedQuestListsIterator(
                this._domain,
                this._gameSession,
                this._connection
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Quest.Model.EzCompletedQuestList> CompletedQuestListsAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Quest.Model.EzCompletedQuestList>(async (writer, token) =>
            {
                var it = _domain.CompletedQuestListsAsync(
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        this._gameSession,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.CompletedQuestListsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Quest.Model.EzCompletedQuestList.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeCompletedQuestLists(
            Action<Gs2.Unity.Gs2Quest.Model.EzCompletedQuestList[]> callback
        ) {
            return this._domain.SubscribeCompletedQuestLists(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Quest.Model.EzCompletedQuestList.FromModel).ToArray());
                }
            );
        }

        public void UnsubscribeCompletedQuestLists(
            ulong callbackId
        ) {
            this._domain.UnsubscribeCompletedQuestLists(
                callbackId
            );
        }

        public void InvalidateCompletedQuestLists(
        ) {
            this._domain.InvalidateCompletedQuestLists(
            );
        }

        public Gs2.Unity.Gs2Quest.Domain.Model.EzCompletedQuestListGameSessionDomain CompletedQuestList(
            string questGroupName
        ) {
            return new Gs2.Unity.Gs2Quest.Domain.Model.EzCompletedQuestListGameSessionDomain(
                _domain.CompletedQuestList(
                    questGroupName
                ),
                this._gameSession,
                this._connection
            );
        }

        public Gs2.Unity.Gs2Quest.Domain.Model.EzProgressGameSessionDomain Progress(
        ) {
            return new Gs2.Unity.Gs2Quest.Domain.Model.EzProgressGameSessionDomain(
                _domain.Progress(
                ),
                this._gameSession,
                this._connection
            );
        }

    }
}
