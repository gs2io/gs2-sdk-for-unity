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
 *
 * deny overwrite
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
#pragma warning disable CS0169, CS0168

using System;
using System.Linq;
using System.Text.RegularExpressions;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Gs2Guild.Domain.Iterator;
using Gs2.Gs2Guild.Request;
using Gs2.Gs2Guild.Result;
using Gs2.Gs2Auth.Model;
using Gs2.Util.LitJson;
using Gs2.Core;
using Gs2.Core.Domain;
using Gs2.Core.Util;
using UnityEngine.Scripting;
using System.Collections;
using Gs2.Unity.Util;
#if GS2_ENABLE_UNITASK
using Cysharp.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using System.Collections.Generic;
#endif

namespace Gs2.Unity.Gs2Guild.Domain.Model
{

    public partial class EzGuildDomain {
        private readonly Gs2.Gs2Guild.Domain.Model.GuildDomain _domain;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string NamespaceName => _domain?.NamespaceName;
        public string GuildModelName => _domain?.GuildModelName;
        public string GuildName => _domain?.GuildName;

        public EzGuildDomain(
            Gs2.Gs2Guild.Domain.Model.GuildDomain domain,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._connection = connection;
        }

        public Gs2.Unity.Gs2Guild.Domain.Model.EzReceiveMemberRequestDomain ReceiveMemberRequest(
            string targetGuildName,
            string fromUserId
        ) {
            return new Gs2.Unity.Gs2Guild.Domain.Model.EzReceiveMemberRequestDomain(
                _domain.ReceiveMemberRequest(
                    targetGuildName,
                    fromUserId
                ),
                this._connection
            );
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Guild.Model.EzGuild> ModelAsync(
            GameSession gameSession
        )
        {
            var item = await this._connection.RunAsync(
                null,
                async () =>
                {
                    return await _domain.ModelAsync(gameSession.AccessToken);
                }
            );
            if (item == null) {
                return null;
            }
            return Gs2.Unity.Gs2Guild.Model.EzGuild.FromModel(
                item
            );
        }
        #endif

        public IFuture<Gs2.Unity.Gs2Guild.Model.EzGuild> ModelFuture(
            GameSession gameSession
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Guild.Model.EzGuild> self)
            {
                var future = this._connection.RunFuture(
                    null,
                    () => {
                    	return _domain.ModelFuture(gameSession.AccessToken);
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
                self.OnComplete(Gs2.Unity.Gs2Guild.Model.EzGuild.FromModel(
                    item
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Guild.Model.EzGuild>(Impl);
        }

        public void Invalidate()
        {
            this._domain.Invalidate();
        }

        public ulong Subscribe(
            GameSession gameSession,
            Action<Gs2.Unity.Gs2Guild.Model.EzGuild> callback
        )
        {
            return this._domain.Subscribe(gameSession.AccessToken, item => {
                callback.Invoke(item == null ? null : Gs2.Unity.Gs2Guild.Model.EzGuild.FromModel(
                    item
                ));
            });
        }

        public void Unsubscribe(ulong callbackId)
        {
            this._domain.Unsubscribe(callbackId);
        }

        #if UNITY_2017_1_OR_NEWER
        public Gs2Future<ulong> SubscribeWithInitialCallFuture(
            GameSession gameSession,
            Action<Gs2.Unity.Gs2Guild.Model.EzGuild> callback
        )
        {
            IEnumerator Impl(IFuture<ulong> self)
            {
                var future = ModelFuture(gameSession);
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                var item = future.Result;
                var callbackId = Subscribe(gameSession, callback);
                callback.Invoke(item);
                self.OnComplete(callbackId);
            }
            return new Gs2InlineFuture<ulong>(Impl);
        }
        #endif

        #if !UNITY_2017_1_OR_NEWER || GS2_ENABLE_UNITASK
            #if UNITY_2017_1_OR_NEWER
        public async UniTask<ulong> SubscribeWithInitialCallAsync(
            GameSession gameSession,
            Action<Gs2.Unity.Gs2Guild.Model.EzGuild> callback
        )
            #else
        public async Task<ulong> SubscribeWithInitialCallAsync(
            GameSession gameSession,
            Action<Gs2.Unity.Gs2Guild.Model.EzGuild> callback
        )
            #endif
        {
            var item = await ModelAsync(gameSession);
            var callbackId = Subscribe(gameSession, callback);
            callback.Invoke(item);
            return callbackId;
        }
        #endif
    }
}
