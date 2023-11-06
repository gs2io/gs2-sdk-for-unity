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
using Gs2.Gs2SerialKey.Domain.Iterator;
using Gs2.Gs2SerialKey.Request;
using Gs2.Gs2SerialKey.Result;
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

namespace Gs2.Unity.Gs2SerialKey.Domain.Model
{

    public partial class EzSerialKeyGameSessionDomain {
        private readonly Gs2.Gs2SerialKey.Domain.Model.SerialKeyAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public string Url => _domain.Url;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;
        public string SerialKeyCode => _domain?.SerialKeyCode;

        public EzSerialKeyGameSessionDomain(
            Gs2.Gs2SerialKey.Domain.Model.SerialKeyAccessTokenDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        [Obsolete("The name has been changed to UseSerialCodeFuture.")]
        public IFuture<Gs2.Unity.Gs2SerialKey.Domain.Model.EzSerialKeyGameSessionDomain> UseSerialCode(
            string code
        )
        {
            return UseSerialCodeFuture(
                code
            );
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2SerialKey.Domain.Model.EzSerialKeyGameSessionDomain> UseSerialCodeFuture(
            string code
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2SerialKey.Domain.Model.EzSerialKeyGameSessionDomain> self)
            {
                yield return UseSerialCodeAsync(
                    code
                ).ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2SerialKey.Domain.Model.EzSerialKeyGameSessionDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2SerialKey.Domain.Model.EzSerialKeyGameSessionDomain> UseSerialCodeAsync(
        #else
        public IFuture<Gs2.Unity.Gs2SerialKey.Domain.Model.EzSerialKeyGameSessionDomain> UseSerialCodeFuture(
        #endif
            string code
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.UseAsync(
                        new UseRequest()
                            .WithCode(code)
                            .WithAccessToken(_domain.AccessToken.Token)
                    );
                }
            );
            return new Gs2.Unity.Gs2SerialKey.Domain.Model.EzSerialKeyGameSessionDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2SerialKey.Domain.Model.EzSerialKeyGameSessionDomain> self)
            {
                var future = _domain.UseFuture(
                    new UseRequest()
                        .WithCode(code)
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return _profile.RunFuture(
                    _domain.AccessToken,
                    future,
                    () =>
        			{
                		return future = _domain.UseFuture(
                    		new UseRequest()
                	        .WithCode(code)
                    	    .WithAccessToken(_domain.AccessToken.Token)
        		        );
        			}
                );
                if (future.Error != null)
                {
                    self.OnError(future.Error);
                    yield break;
                }
                var result = future.Result;
                self.OnComplete(new Gs2.Unity.Gs2SerialKey.Domain.Model.EzSerialKeyGameSessionDomain(result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2SerialKey.Domain.Model.EzSerialKeyGameSessionDomain>(Impl);
        #endif
        }

        [Obsolete("The name has been changed to ModelFuture.")]
        public IFuture<Gs2.Unity.Gs2SerialKey.Model.EzSerialKey> Model()
        {
            return ModelFuture();
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2SerialKey.Model.EzSerialKey> ModelFuture()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2SerialKey.Model.EzSerialKey> self)
            {
                yield return ModelAsync().ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2SerialKey.Model.EzSerialKey>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2SerialKey.Model.EzSerialKey> ModelAsync()
        {
            var item = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.ModelAsync();
                }
            );
            if (item == null) {
                return null;
            }
            return Gs2.Unity.Gs2SerialKey.Model.EzSerialKey.FromModel(
                item
            );
        }
        #else
        public IFuture<Gs2.Unity.Gs2SerialKey.Model.EzSerialKey> ModelFuture()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2SerialKey.Model.EzSerialKey> self)
            {
                var future = _domain.ModelFuture();
                yield return _profile.RunFuture(
                    _domain.AccessToken,
                    future,
                    () => {
                    	return future = _domain.ModelFuture();
                    }
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
                self.OnComplete(Gs2.Unity.Gs2SerialKey.Model.EzSerialKey.FromModel(
                    item
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2SerialKey.Model.EzSerialKey>(Impl);
        }
        #endif

        public ulong Subscribe(Action<Gs2.Unity.Gs2SerialKey.Model.EzSerialKey> callback)
        {
            return this._domain.Subscribe(item => {
                callback.Invoke(Gs2.Unity.Gs2SerialKey.Model.EzSerialKey.FromModel(
                    item
                ));
            });
        }

        public void Unsubscribe(ulong callbackId)
        {
            this._domain.Unsubscribe(callbackId);
        }

    }
}
