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
using Gs2.Gs2Gateway.Domain.Iterator;
using Gs2.Gs2Gateway.Request;
using Gs2.Gs2Gateway.Result;
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

namespace Gs2.Unity.Gs2Gateway.Domain.Model
{

    public partial class EzWebSocketSessionGameSessionDomain {
        private readonly Gs2.Gs2Gateway.Domain.Model.WebSocketSessionAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public string Protocol => _domain.Protocol;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzWebSocketSessionGameSessionDomain(
            Gs2.Gs2Gateway.Domain.Model.WebSocketSessionAccessTokenDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Gateway.Domain.Model.EzWebSocketSessionGameSessionDomain> SetUserId(
              bool? allowConcurrentAccess = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Gateway.Domain.Model.EzWebSocketSessionGameSessionDomain> self)
            {
                yield return SetUserIdAsync(
                    allowConcurrentAccess
                ).ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Gateway.Domain.Model.EzWebSocketSessionGameSessionDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Gateway.Domain.Model.EzWebSocketSessionGameSessionDomain> SetUserIdAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Gateway.Domain.Model.EzWebSocketSessionGameSessionDomain> SetUserId(
        #endif
              bool? allowConcurrentAccess = null
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.SetUserIdAsync(
                        new SetUserIdRequest()
                            .WithAllowConcurrentAccess(allowConcurrentAccess)
                            .WithAccessToken(_domain.AccessToken.Token)
                    );
                }
            );
            return new Gs2.Unity.Gs2Gateway.Domain.Model.EzWebSocketSessionGameSessionDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Gateway.Domain.Model.EzWebSocketSessionGameSessionDomain> self)
            {
                var future = _domain.SetUserId(
                    new SetUserIdRequest()
                        .WithAllowConcurrentAccess(allowConcurrentAccess)
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return _profile.RunFuture(
                    _domain.AccessToken,
                    future
                );
                if (future.Error != null)
                {
                    self.OnError(future.Error);
                    yield break;
                }
                var result = future.Result;
                self.OnComplete(new Gs2.Unity.Gs2Gateway.Domain.Model.EzWebSocketSessionGameSessionDomain(result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Gateway.Domain.Model.EzWebSocketSessionGameSessionDomain>(Impl);
        #endif
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Gateway.Model.EzWebSocketSession> Model()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Gateway.Model.EzWebSocketSession> self)
            {
                yield return ModelAsync().ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Gateway.Model.EzWebSocketSession>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Gateway.Model.EzWebSocketSession> ModelAsync()
        {
            var item = await _domain.Model();
            if (item == null) {
                return null;
            }
            return Gs2.Unity.Gs2Gateway.Model.EzWebSocketSession.FromModel(
                item
            );
        }
        #else
        public IFuture<Gs2.Unity.Gs2Gateway.Model.EzWebSocketSession> Model()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Gateway.Model.EzWebSocketSession> self)
            {
                var future = _domain.Model();
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
                self.OnComplete(Gs2.Unity.Gs2Gateway.Model.EzWebSocketSession.FromModel(
                    item
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Gateway.Model.EzWebSocketSession>(Impl);
        }
        #endif

    }
}
