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
using Gs2.Gs2Showcase.Domain.Iterator;
using Gs2.Gs2Showcase.Request;
using Gs2.Gs2Showcase.Result;
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

namespace Gs2.Unity.Gs2Showcase.Domain.Model
{

    public partial class EzRandomDisplayItemGameSessionDomain {
        private readonly Gs2.Gs2Showcase.Domain.Model.RandomDisplayItemAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public string TransactionId => _domain.TransactionId;
        public bool? AutoRunStampSheet => _domain.AutoRunStampSheet;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;
        public string ShowcaseName => _domain?.ShowcaseName;
        public string DisplayItemName => _domain?.DisplayItemName;

        public EzRandomDisplayItemGameSessionDomain(
            Gs2.Gs2Showcase.Domain.Model.RandomDisplayItemAccessTokenDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        [Obsolete("The name has been changed to RandomShowcaseBuyFuture.")]
        public IFuture<Gs2.Unity.Core.Domain.EzTransactionDomain> RandomShowcaseBuy(
            int? quantity = null,
            Gs2.Unity.Gs2Showcase.Model.EzConfig[] config = null
        )
        {
            return RandomShowcaseBuyFuture(
                quantity,
                config
            );
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Core.Domain.EzTransactionDomain> RandomShowcaseBuyFuture(
            int? quantity = null,
            Gs2.Unity.Gs2Showcase.Model.EzConfig[] config = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Core.Domain.EzTransactionDomain> self)
            {
                var future = this._domain.RandomShowcaseBuyFuture(
                    new RandomShowcaseBuyRequest()
                        .WithQuantity(quantity)
                        .WithConfig(config?.Select(v => v.ToModel()).ToArray())
                        .WithAccessToken(_domain.AccessToken.Token)
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

        public async UniTask<Gs2.Unity.Core.Domain.EzTransactionDomain> RandomShowcaseBuyAsync(
        #else
        public IFuture<Gs2.Unity.Core.Domain.EzTransactionDomain> RandomShowcaseBuyFuture(
        #endif
            int? quantity = null,
            Gs2.Unity.Gs2Showcase.Model.EzConfig[] config = null
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.RandomShowcaseBuyAsync(
                        new RandomShowcaseBuyRequest()
                            .WithQuantity(quantity)
                            .WithConfig(config?.Select(v => v.ToModel()).ToArray())
                            .WithAccessToken(_domain.AccessToken.Token)
                    );
                }
            );
            return result == null ? null : new Gs2.Unity.Core.Domain.EzTransactionDomain(result);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Core.Domain.EzTransactionDomain> self)
            {
                var future = _domain.RandomShowcaseBuyFuture(
                    new RandomShowcaseBuyRequest()
                        .WithQuantity(quantity)
                        .WithConfig(config?.Select(v => v.ToModel()).ToArray())
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return _profile.RunFuture(
                    _domain.AccessToken,
                    future,
                    () =>
        			{
                		return future = _domain.RandomShowcaseBuyFuture(
                    		new RandomShowcaseBuyRequest()
                	        .WithQuantity(quantity)
        	                .WithConfig(config?.Select(v => v.ToModel()).ToArray())
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
                self.OnComplete(result == null ? null : new Gs2.Unity.Core.Domain.EzTransactionDomain(result));
            }
            return new Gs2InlineFuture<Gs2.Unity.Core.Domain.EzTransactionDomain>(Impl);
        #endif
        }

        [Obsolete("The name has been changed to ModelFuture.")]
        public IFuture<Gs2.Unity.Gs2Showcase.Model.EzRandomDisplayItem> Model()
        {
            return ModelFuture();
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Showcase.Model.EzRandomDisplayItem> ModelFuture()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Showcase.Model.EzRandomDisplayItem> self)
            {
                yield return ModelAsync().ToCoroutine(
                    self.OnComplete,
                    e =>
                    {
                        if (e is Gs2.Core.Exception.Gs2Exception e2) {
                            self.OnError(e2);
                        }
                        else {
                            UnityEngine.Debug.LogError(e.Message);
                            self.OnError(new Gs2.Core.Exception.UnknownException(e.Message));
                        }
                    }
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Showcase.Model.EzRandomDisplayItem>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Showcase.Model.EzRandomDisplayItem> ModelAsync()
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
            return Gs2.Unity.Gs2Showcase.Model.EzRandomDisplayItem.FromModel(
                item
            );
        }
        #else
        public IFuture<Gs2.Unity.Gs2Showcase.Model.EzRandomDisplayItem> ModelFuture()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Showcase.Model.EzRandomDisplayItem> self)
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
                self.OnComplete(Gs2.Unity.Gs2Showcase.Model.EzRandomDisplayItem.FromModel(
                    item
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Showcase.Model.EzRandomDisplayItem>(Impl);
        }
        #endif

        public ulong Subscribe(Action<Gs2.Unity.Gs2Showcase.Model.EzRandomDisplayItem> callback)
        {
            return this._domain.Subscribe(item => {
                callback.Invoke(Gs2.Unity.Gs2Showcase.Model.EzRandomDisplayItem.FromModel(
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
