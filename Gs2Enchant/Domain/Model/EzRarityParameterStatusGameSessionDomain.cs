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
using Gs2.Gs2Enchant.Domain.Iterator;
using Gs2.Gs2Enchant.Request;
using Gs2.Gs2Enchant.Result;
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

namespace Gs2.Unity.Gs2Enchant.Domain.Model
{

    public partial class EzRarityParameterStatusGameSessionDomain {
        private readonly Gs2.Gs2Enchant.Domain.Model.RarityParameterStatusAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;
        public string ParameterName => _domain?.ParameterName;
        public string PropertyId => _domain?.PropertyId;

        public EzRarityParameterStatusGameSessionDomain(
            Gs2.Gs2Enchant.Domain.Model.RarityParameterStatusAccessTokenDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        [Obsolete("The name has been changed to VerifyRarityParameterStatusFuture.")]
        public IFuture<Gs2.Unity.Gs2Enchant.Domain.Model.EzRarityParameterStatusGameSessionDomain> VerifyRarityParameterStatus(
            string verifyType,
            string parameterValueName,
            int parameterCount
        )
        {
            return VerifyRarityParameterStatusFuture(
                verifyType,
                parameterValueName,
                parameterCount
            );
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Enchant.Domain.Model.EzRarityParameterStatusGameSessionDomain> VerifyRarityParameterStatusFuture(
            string verifyType,
            string parameterValueName,
            int parameterCount
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Enchant.Domain.Model.EzRarityParameterStatusGameSessionDomain> self)
            {
                yield return VerifyRarityParameterStatusAsync(
                    verifyType,
                    parameterValueName,
                    parameterCount
                ).ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Enchant.Domain.Model.EzRarityParameterStatusGameSessionDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Enchant.Domain.Model.EzRarityParameterStatusGameSessionDomain> VerifyRarityParameterStatusAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Enchant.Domain.Model.EzRarityParameterStatusGameSessionDomain> VerifyRarityParameterStatusFuture(
        #endif
            string verifyType,
            string parameterValueName,
            int parameterCount
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.VerifyAsync(
                        new VerifyRarityParameterStatusRequest()
                            .WithVerifyType(verifyType)
                            .WithParameterValueName(parameterValueName)
                            .WithParameterCount(parameterCount)
                            .WithAccessToken(_domain.AccessToken.Token)
                    );
                }
            );
            return new Gs2.Unity.Gs2Enchant.Domain.Model.EzRarityParameterStatusGameSessionDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Enchant.Domain.Model.EzRarityParameterStatusGameSessionDomain> self)
            {
                var future = _domain.VerifyFuture(
                    new VerifyRarityParameterStatusRequest()
                        .WithVerifyType(verifyType)
                        .WithParameterValueName(parameterValueName)
                        .WithParameterCount(parameterCount)
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return _profile.RunFuture(
                    _domain.AccessToken,
                    future,
                    () =>
        			{
                		return future = _domain.VerifyFuture(
                    		new VerifyRarityParameterStatusRequest()
                	        .WithVerifyType(verifyType)
                	        .WithParameterValueName(parameterValueName)
                	        .WithParameterCount(parameterCount)
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
                self.OnComplete(new Gs2.Unity.Gs2Enchant.Domain.Model.EzRarityParameterStatusGameSessionDomain(result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Enchant.Domain.Model.EzRarityParameterStatusGameSessionDomain>(Impl);
        #endif
        }

        [Obsolete("The name has been changed to ModelFuture.")]
        public IFuture<Gs2.Unity.Gs2Enchant.Model.EzRarityParameterStatus> Model()
        {
            return ModelFuture();
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Enchant.Model.EzRarityParameterStatus> ModelFuture()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Enchant.Model.EzRarityParameterStatus> self)
            {
                yield return ModelAsync().ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Enchant.Model.EzRarityParameterStatus>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Enchant.Model.EzRarityParameterStatus> ModelAsync()
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
            return Gs2.Unity.Gs2Enchant.Model.EzRarityParameterStatus.FromModel(
                item
            );
        }
        #else
        public IFuture<Gs2.Unity.Gs2Enchant.Model.EzRarityParameterStatus> ModelFuture()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Enchant.Model.EzRarityParameterStatus> self)
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
                self.OnComplete(Gs2.Unity.Gs2Enchant.Model.EzRarityParameterStatus.FromModel(
                    item
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Enchant.Model.EzRarityParameterStatus>(Impl);
        }
        #endif

        public ulong Subscribe(Action<Gs2.Unity.Gs2Enchant.Model.EzRarityParameterStatus> callback)
        {
            return this._domain.Subscribe(item => {
                callback.Invoke(Gs2.Unity.Gs2Enchant.Model.EzRarityParameterStatus.FromModel(
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
