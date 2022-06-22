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
using Gs2.Gs2Auth.Request;
using Gs2.Gs2Auth.Result;
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

namespace Gs2.Unity.Gs2Auth.Domain.Model
{

    public partial class EzAccessTokenDomain {
        private readonly Gs2.Gs2Auth.Domain.Model.AccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public string Token => _domain.Token;
        public string UserId => _domain.UserId;
        public long? Expire => _domain.Expire;

        public EzAccessTokenDomain(
            Gs2.Gs2Auth.Domain.Model.AccessTokenDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Auth.Domain.Model.EzAccessTokenDomain> Login(
              string keyId,
              string body,
              string signature
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Auth.Domain.Model.EzAccessTokenDomain> self)
            {
                yield return LoginAsync(
                    keyId,
                    body,
                    signature
                ).ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Auth.Domain.Model.EzAccessTokenDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Auth.Domain.Model.EzAccessTokenDomain> LoginAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Auth.Domain.Model.EzAccessTokenDomain> Login(
        #endif
              string keyId,
              string body,
              string signature
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                null,
                async () =>
                {
                    return await _domain.LoginBySignatureAsync(
                        new LoginBySignatureRequest()
                            .WithKeyId(keyId)
                            .WithBody(body)
                            .WithSignature(signature)
                    );
                }
            );
            return new Gs2.Unity.Gs2Auth.Domain.Model.EzAccessTokenDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Auth.Domain.Model.EzAccessTokenDomain> self)
            {
                var future = _domain.LoginBySignature(
                    new LoginBySignatureRequest()
                        .WithKeyId(keyId)
                        .WithBody(body)
                        .WithSignature(signature)
                );
                yield return _profile.RunFuture(
                    null,
                    future
                );
                if (future.Error != null)
                {
                    self.OnError(future.Error);
                    yield break;
                }
                var result = future.Result;
                self.OnComplete(new Gs2.Unity.Gs2Auth.Domain.Model.EzAccessTokenDomain(result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Auth.Domain.Model.EzAccessTokenDomain>(Impl);
        #endif
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Auth.Model.EzAccessToken> Model()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Auth.Model.EzAccessToken> self)
            {
                yield return ModelAsync().ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Auth.Model.EzAccessToken>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Auth.Model.EzAccessToken> ModelAsync()
        {
            var item = await _profile.RunAsync(
                null,
                async () =>
                {
                    return await _domain.Model();
                }
            );
            if (item == null) {
                return null;
            }
            return Gs2.Unity.Gs2Auth.Model.EzAccessToken.FromModel(
                item
            );
        }
        #else
        public IFuture<Gs2.Unity.Gs2Auth.Model.EzAccessToken> Model()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Auth.Model.EzAccessToken> self)
            {
                var future = _domain.Model();
                yield return _profile.RunFuture(
                    null,
                    future
                );
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                var item = future.Result;
                if (item == null) {
                    self.OnComplete(null);
                    yield break;
                }
                self.OnComplete(Gs2.Unity.Gs2Auth.Model.EzAccessToken.FromModel(
                    item
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Auth.Model.EzAccessToken>(Impl);
        }
        #endif

    }
}
