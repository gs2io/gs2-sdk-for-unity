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
using Gs2.Gs2Exchange.Domain.Iterator;
using Gs2.Gs2Exchange.Request;
using Gs2.Gs2Exchange.Result;
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

namespace Gs2.Unity.Gs2Exchange.Domain.Model
{

    public partial class EzExchangeGameSessionDomain {
        private readonly Gs2.Gs2Exchange.Domain.Model.ExchangeAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public string TransactionId => _domain.TransactionId;
        public bool? AutoRunStampSheet => _domain.AutoRunStampSheet;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzExchangeGameSessionDomain(
            Gs2.Gs2Exchange.Domain.Model.ExchangeAccessTokenDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        [Obsolete("The name has been changed to ExchangeFuture.")]
        public IFuture<Gs2.Unity.Core.Domain.EzTransactionDomain> Exchange(
            string rateName,
            int count,
            Gs2.Unity.Gs2Exchange.Model.EzConfig[] config = null
        )
        {
            return ExchangeFuture(
                rateName,
                count,
                config
            );
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Core.Domain.EzTransactionDomain> ExchangeFuture(
            string rateName,
            int count,
            Gs2.Unity.Gs2Exchange.Model.EzConfig[] config = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Core.Domain.EzTransactionDomain> self)
            {
                var future = this._domain.ExchangeFuture(
                    new ExchangeRequest()
                        .WithRateName(rateName)
                        .WithCount(count)
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

        public async UniTask<Gs2.Unity.Core.Domain.EzTransactionDomain> ExchangeAsync(
        #else
        public IFuture<Gs2.Unity.Core.Domain.EzTransactionDomain> ExchangeFuture(
        #endif
            string rateName,
            int count,
            Gs2.Unity.Gs2Exchange.Model.EzConfig[] config = null
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.ExchangeAsync(
                        new ExchangeRequest()
                            .WithRateName(rateName)
                            .WithCount(count)
                            .WithConfig(config?.Select(v => v.ToModel()).ToArray())
                            .WithAccessToken(_domain.AccessToken.Token)
                    );
                }
            );
            return result == null ? null : new Gs2.Unity.Core.Domain.EzTransactionDomain(result);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Core.Domain.EzTransactionDomain> self)
            {
                var future = _domain.ExchangeFuture(
                    new ExchangeRequest()
                        .WithRateName(rateName)
                        .WithCount(count)
                        .WithConfig(config?.Select(v => v.ToModel()).ToArray())
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return _profile.RunFuture(
                    _domain.AccessToken,
                    future,
                    () =>
        			{
                		return future = _domain.ExchangeFuture(
                    		new ExchangeRequest()
                	        .WithRateName(rateName)
                	        .WithCount(count)
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

        [Obsolete("The name has been changed to IncrementalExchangeFuture.")]
        public IFuture<Gs2.Unity.Core.Domain.EzTransactionDomain> IncrementalExchange(
            string rateName,
            int count,
            Gs2.Unity.Gs2Exchange.Model.EzConfig[] config = null
        )
        {
            return IncrementalExchangeFuture(
                rateName,
                count,
                config
            );
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Core.Domain.EzTransactionDomain> IncrementalExchangeFuture(
            string rateName,
            int count,
            Gs2.Unity.Gs2Exchange.Model.EzConfig[] config = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Core.Domain.EzTransactionDomain> self)
            {
                var future = this._domain.IncrementalFuture(
                    new IncrementalExchangeRequest()
                        .WithRateName(rateName)
                        .WithCount(count)
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

        public async UniTask<Gs2.Unity.Core.Domain.EzTransactionDomain> IncrementalExchangeAsync(
        #else
        public IFuture<Gs2.Unity.Core.Domain.EzTransactionDomain> IncrementalExchangeFuture(
        #endif
            string rateName,
            int count,
            Gs2.Unity.Gs2Exchange.Model.EzConfig[] config = null
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.IncrementalAsync(
                        new IncrementalExchangeRequest()
                            .WithRateName(rateName)
                            .WithCount(count)
                            .WithConfig(config?.Select(v => v.ToModel()).ToArray())
                            .WithAccessToken(_domain.AccessToken.Token)
                    );
                }
            );
            return result == null ? null : new Gs2.Unity.Core.Domain.EzTransactionDomain(result);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Core.Domain.EzTransactionDomain> self)
            {
                var future = _domain.IncrementalFuture(
                    new IncrementalExchangeRequest()
                        .WithRateName(rateName)
                        .WithCount(count)
                        .WithConfig(config?.Select(v => v.ToModel()).ToArray())
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return _profile.RunFuture(
                    _domain.AccessToken,
                    future,
                    () =>
        			{
                		return future = _domain.IncrementalFuture(
                    		new IncrementalExchangeRequest()
                	        .WithRateName(rateName)
                	        .WithCount(count)
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

    }
}
