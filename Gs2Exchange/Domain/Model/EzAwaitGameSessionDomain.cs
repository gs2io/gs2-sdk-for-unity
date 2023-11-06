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

    public partial class EzAwaitGameSessionDomain {
        private readonly Gs2.Gs2Exchange.Domain.Model.AwaitAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public long? UnlockAt => _domain.UnlockAt;
        public string TransactionId => _domain.TransactionId;
        public bool? AutoRunStampSheet => _domain.AutoRunStampSheet;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;
        public string AwaitName => _domain?.AwaitName;

        public EzAwaitGameSessionDomain(
            Gs2.Gs2Exchange.Domain.Model.AwaitAccessTokenDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        [Obsolete("The name has been changed to AcquireFuture.")]
        public IFuture<Gs2.Unity.Core.Domain.EzTransactionDomain> Acquire(
        )
        {
            return AcquireFuture(
            );
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Core.Domain.EzTransactionDomain> AcquireFuture(
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Core.Domain.EzTransactionDomain> self)
            {
                yield return AcquireAsync(
                ).ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Core.Domain.EzTransactionDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Core.Domain.EzTransactionDomain> AcquireAsync(
        #else
        public IFuture<Gs2.Unity.Core.Domain.EzTransactionDomain> AcquireFuture(
        #endif
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.AcquireAsync(
                        new AcquireRequest()
                            .WithAccessToken(_domain.AccessToken.Token)
                    );
                }
            );
            return new Gs2.Unity.Core.Domain.EzTransactionDomain(result);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Core.Domain.EzTransactionDomain> self)
            {
                var future = _domain.AcquireFuture(
                    new AcquireRequest()
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return _profile.RunFuture(
                    _domain.AccessToken,
                    future,
                    () =>
        			{
                		return future = _domain.AcquireFuture(
                    		new AcquireRequest()
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
                self.OnComplete(new Gs2.Unity.Core.Domain.EzTransactionDomain(result));
            }
            return new Gs2InlineFuture<Gs2.Unity.Core.Domain.EzTransactionDomain>(Impl);
        #endif
        }

        [Obsolete("The name has been changed to SkipFuture.")]
        public IFuture<Gs2.Unity.Core.Domain.EzTransactionDomain> Skip(
        )
        {
            return SkipFuture(
            );
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Core.Domain.EzTransactionDomain> SkipFuture(
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Core.Domain.EzTransactionDomain> self)
            {
                yield return SkipAsync(
                ).ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Core.Domain.EzTransactionDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Core.Domain.EzTransactionDomain> SkipAsync(
        #else
        public IFuture<Gs2.Unity.Core.Domain.EzTransactionDomain> SkipFuture(
        #endif
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.SkipAsync(
                        new SkipRequest()
                            .WithAccessToken(_domain.AccessToken.Token)
                    );
                }
            );
            return new Gs2.Unity.Core.Domain.EzTransactionDomain(result);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Core.Domain.EzTransactionDomain> self)
            {
                var future = _domain.SkipFuture(
                    new SkipRequest()
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return _profile.RunFuture(
                    _domain.AccessToken,
                    future,
                    () =>
        			{
                		return future = _domain.SkipFuture(
                    		new SkipRequest()
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
                self.OnComplete(new Gs2.Unity.Core.Domain.EzTransactionDomain(result));
            }
            return new Gs2InlineFuture<Gs2.Unity.Core.Domain.EzTransactionDomain>(Impl);
        #endif
        }

        [Obsolete("The name has been changed to DeleteAwaitFuture.")]
        public IFuture<Gs2.Unity.Gs2Exchange.Domain.Model.EzAwaitGameSessionDomain> DeleteAwait(
        )
        {
            return DeleteAwaitFuture(
            );
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Exchange.Domain.Model.EzAwaitGameSessionDomain> DeleteAwaitFuture(
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Exchange.Domain.Model.EzAwaitGameSessionDomain> self)
            {
                yield return DeleteAwaitAsync(
                ).ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Exchange.Domain.Model.EzAwaitGameSessionDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Exchange.Domain.Model.EzAwaitGameSessionDomain> DeleteAwaitAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Exchange.Domain.Model.EzAwaitGameSessionDomain> DeleteAwaitFuture(
        #endif
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.DeleteAsync(
                        new DeleteAwaitRequest()
                            .WithAccessToken(_domain.AccessToken.Token)
                    );
                }
            );
            return new Gs2.Unity.Gs2Exchange.Domain.Model.EzAwaitGameSessionDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Exchange.Domain.Model.EzAwaitGameSessionDomain> self)
            {
                var future = _domain.DeleteFuture(
                    new DeleteAwaitRequest()
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return _profile.RunFuture(
                    _domain.AccessToken,
                    future,
                    () =>
        			{
                		return future = _domain.DeleteFuture(
                    		new DeleteAwaitRequest()
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
                self.OnComplete(new Gs2.Unity.Gs2Exchange.Domain.Model.EzAwaitGameSessionDomain(result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Exchange.Domain.Model.EzAwaitGameSessionDomain>(Impl);
        #endif
        }

        [Obsolete("The name has been changed to ModelFuture.")]
        public IFuture<Gs2.Unity.Gs2Exchange.Model.EzAwait> Model()
        {
            return ModelFuture();
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Exchange.Model.EzAwait> ModelFuture()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Exchange.Model.EzAwait> self)
            {
                yield return ModelAsync().ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Exchange.Model.EzAwait>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Exchange.Model.EzAwait> ModelAsync()
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
            return Gs2.Unity.Gs2Exchange.Model.EzAwait.FromModel(
                item
            );
        }
        #else
        public IFuture<Gs2.Unity.Gs2Exchange.Model.EzAwait> ModelFuture()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Exchange.Model.EzAwait> self)
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
                self.OnComplete(Gs2.Unity.Gs2Exchange.Model.EzAwait.FromModel(
                    item
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Exchange.Model.EzAwait>(Impl);
        }
        #endif

        public ulong Subscribe(Action<Gs2.Unity.Gs2Exchange.Model.EzAwait> callback)
        {
            return this._domain.Subscribe(item => {
                callback.Invoke(Gs2.Unity.Gs2Exchange.Model.EzAwait.FromModel(
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
