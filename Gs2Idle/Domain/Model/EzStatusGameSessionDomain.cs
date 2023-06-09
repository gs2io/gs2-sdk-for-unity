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

using System;
using System.Linq;
using System.Text.RegularExpressions;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Gs2Idle.Domain.Iterator;
using Gs2.Gs2Idle.Request;
using Gs2.Gs2Idle.Result;
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

namespace Gs2.Unity.Gs2Idle.Domain.Model
{

    public partial class EzStatusGameSessionDomain {
        private readonly Gs2.Gs2Idle.Domain.Model.StatusAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public string TransactionId => _domain.TransactionId;
        public bool? AutoRunStampSheet => _domain.AutoRunStampSheet;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;
        public string CategoryName => _domain?.CategoryName;

        public EzStatusGameSessionDomain(
            Gs2.Gs2Idle.Domain.Model.StatusAccessTokenDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Core.Model.EzAcquireAction[]> Prediction(
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Core.Model.EzAcquireAction[]> self)
            {
                yield return PredictionAsync(
                ).ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Core.Model.EzAcquireAction[]>(Impl);
        }

        public async UniTask<Gs2.Unity.Core.Model.EzAcquireAction[]> PredictionAsync(
        #else
        public IFuture<Gs2.Unity.Core.Model.EzAcquireAction[]> Prediction(
        #endif
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.PredictionAsync(
                        new PredictionRequest()
                            .WithAccessToken(_domain.AccessToken.Token)
                    );
                }
            );
            return result.Select(v => new Gs2.Unity.Core.Model.EzAcquireAction {
                Action = v.Action,
                Request = v.Request
            }).ToArray();
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Core.Model.EzAcquireAction[]> self)
            {
                var future = _domain.Prediction(
                    new PredictionRequest()
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return _profile.RunFuture(
                    _domain.AccessToken,
                    future,
                    () =>
        			{
                		return future = _domain.Prediction(
                    		new PredictionRequest()
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
                self.OnComplete(result.Select(v => new Gs2.Unity.Core.Model.EzAcquireAction {
                    Action = v.Action,
                    Request = v.Request
                }).ToArray());
            }
            return new Gs2InlineFuture<Gs2.Unity.Core.Model.EzAcquireAction[]>(Impl);
        #endif
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Idle.Domain.Model.EzStatusGameSessionDomain> Receive(
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Idle.Domain.Model.EzStatusGameSessionDomain> self)
            {
                yield return ReceiveAsync(
                ).ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Idle.Domain.Model.EzStatusGameSessionDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Idle.Domain.Model.EzStatusGameSessionDomain> ReceiveAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Idle.Domain.Model.EzStatusGameSessionDomain> Receive(
        #endif
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.ReceiveAsync(
                        new ReceiveRequest()
                            .WithAccessToken(_domain.AccessToken.Token)
                    );
                }
            );
            return new Gs2.Unity.Gs2Idle.Domain.Model.EzStatusGameSessionDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Idle.Domain.Model.EzStatusGameSessionDomain> self)
            {
                var future = _domain.Receive(
                    new ReceiveRequest()
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return _profile.RunFuture(
                    _domain.AccessToken,
                    future,
                    () =>
        			{
                		return future = _domain.Receive(
                    		new ReceiveRequest()
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
                self.OnComplete(new Gs2.Unity.Gs2Idle.Domain.Model.EzStatusGameSessionDomain(result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Idle.Domain.Model.EzStatusGameSessionDomain>(Impl);
        #endif
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Idle.Model.EzStatus> Model()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Idle.Model.EzStatus> self)
            {
                yield return ModelAsync().ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Idle.Model.EzStatus>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Idle.Model.EzStatus> ModelAsync()
        {
            var item = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.Model();
                }
            );
            if (item == null) {
                return null;
            }
            return Gs2.Unity.Gs2Idle.Model.EzStatus.FromModel(
                item
            );
        }
        #else
        public IFuture<Gs2.Unity.Gs2Idle.Model.EzStatus> Model()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Idle.Model.EzStatus> self)
            {
                var future = _domain.Model();
                yield return _profile.RunFuture(
                    _domain.AccessToken,
                    future,
                    () => {
                    	return future = _domain.Model();
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
                self.OnComplete(Gs2.Unity.Gs2Idle.Model.EzStatus.FromModel(
                    item
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Idle.Model.EzStatus>(Impl);
        }
        #endif

    }
}
