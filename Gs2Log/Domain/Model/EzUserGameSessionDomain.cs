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
using Gs2.Gs2Log.Domain.Iterator;
using Gs2.Gs2Log.Request;
using Gs2.Gs2Log.Result;
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

namespace Gs2.Unity.Gs2Log.Domain.Model
{

    public partial class EzUserGameSessionDomain {
        private readonly Gs2.Gs2Log.Domain.Model.UserAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.IGameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string? NextPageToken => _domain.NextPageToken;
        public long? TotalCount => _domain.TotalCount;
        public long? ScanSize => _domain.ScanSize;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserGameSessionDomain(
            Gs2.Gs2Log.Domain.Model.UserAccessTokenDomain domain,
            Gs2.Unity.Util.IGameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }

        [Obsolete("The name has been changed to SendInGameLogFuture.")]
        public IFuture<Gs2.Unity.Gs2Log.Domain.Model.EzInGameLogGameSessionDomain> SendInGameLog(
            string payload,
            Gs2.Unity.Gs2Log.Model.EzInGameLogTag[] tags = null
        )
        {
            return SendInGameLogFuture(
                payload,
                tags
            );
        }

        public IFuture<Gs2.Unity.Gs2Log.Domain.Model.EzInGameLogGameSessionDomain> SendInGameLogFuture(
            string payload,
            Gs2.Unity.Gs2Log.Model.EzInGameLogTag[] tags = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Log.Domain.Model.EzInGameLogGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.SendInGameLogFuture(
                        new SendInGameLogRequest()
                            .WithTags(tags?.Select(v => v.ToModel()).ToArray())
                            .WithPayload(payload)
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Log.Domain.Model.EzInGameLogGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Log.Domain.Model.EzInGameLogGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Log.Domain.Model.EzInGameLogGameSessionDomain> SendInGameLogAsync(
            string payload,
            Gs2.Unity.Gs2Log.Model.EzInGameLogTag[] tags = null
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.SendInGameLogAsync(
                    new SendInGameLogRequest()
                        .WithTags(tags?.Select(v => v.ToModel()).ToArray())
                        .WithPayload(payload)
                )
            );
            return new Gs2.Unity.Gs2Log.Domain.Model.EzInGameLogGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

    }
}
