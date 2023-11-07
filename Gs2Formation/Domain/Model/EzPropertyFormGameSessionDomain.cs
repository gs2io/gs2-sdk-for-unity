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
using Gs2.Gs2Formation.Domain.Iterator;
using Gs2.Gs2Formation.Request;
using Gs2.Gs2Formation.Result;
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

namespace Gs2.Unity.Gs2Formation.Domain.Model
{

    public partial class EzPropertyFormGameSessionDomain {
        private readonly Gs2.Gs2Formation.Domain.Model.PropertyFormAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public string Body => _domain.Body;
        public string Signature => _domain.Signature;
        public string TransactionId => _domain.TransactionId;
        public bool? AutoRunStampSheet => _domain.AutoRunStampSheet;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;
        public string PropertyFormModelName => _domain?.PropertyFormModelName;
        public string PropertyId => _domain?.PropertyId;

        public EzPropertyFormGameSessionDomain(
            Gs2.Gs2Formation.Domain.Model.PropertyFormAccessTokenDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        [Obsolete("The name has been changed to GetPropertyFormWithSignatureFuture.")]
        public IFuture<Gs2.Unity.Gs2Formation.Domain.Model.EzPropertyFormGameSessionDomain> GetPropertyFormWithSignature(
            string keyId
        )
        {
            return GetPropertyFormWithSignatureFuture(
                keyId
            );
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Formation.Domain.Model.EzPropertyFormGameSessionDomain> GetPropertyFormWithSignatureFuture(
            string keyId
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Formation.Domain.Model.EzPropertyFormGameSessionDomain> self)
            {
                var future = this._domain.GetWithSignatureFuture(
                    new GetPropertyFormWithSignatureRequest()
                        .WithKeyId(keyId)
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Formation.Domain.Model.EzPropertyFormGameSessionDomain(future.Result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Formation.Domain.Model.EzPropertyFormGameSessionDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Formation.Domain.Model.EzPropertyFormGameSessionDomain> GetPropertyFormWithSignatureAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Formation.Domain.Model.EzPropertyFormGameSessionDomain> GetPropertyFormWithSignatureFuture(
        #endif
            string keyId
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.GetWithSignatureAsync(
                        new GetPropertyFormWithSignatureRequest()
                            .WithKeyId(keyId)
                            .WithAccessToken(_domain.AccessToken.Token)
                    );
                }
            );
            return new Gs2.Unity.Gs2Formation.Domain.Model.EzPropertyFormGameSessionDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Formation.Domain.Model.EzPropertyFormGameSessionDomain> self)
            {
                var future = _domain.GetWithSignatureFuture(
                    new GetPropertyFormWithSignatureRequest()
                        .WithKeyId(keyId)
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return _profile.RunFuture(
                    _domain.AccessToken,
                    future,
                    () =>
        			{
                		return future = _domain.GetWithSignatureFuture(
                    		new GetPropertyFormWithSignatureRequest()
                	        .WithKeyId(keyId)
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
                self.OnComplete(new Gs2.Unity.Gs2Formation.Domain.Model.EzPropertyFormGameSessionDomain(result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Formation.Domain.Model.EzPropertyFormGameSessionDomain>(Impl);
        #endif
        }

        [Obsolete("The name has been changed to SetPropertyFormFuture.")]
        public IFuture<Gs2.Unity.Gs2Formation.Domain.Model.EzPropertyFormGameSessionDomain> SetPropertyForm(
            Gs2.Unity.Gs2Formation.Model.EzSlotWithSignature[] slots,
            string keyId
        )
        {
            return SetPropertyFormFuture(
                slots,
                keyId
            );
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Formation.Domain.Model.EzPropertyFormGameSessionDomain> SetPropertyFormFuture(
            Gs2.Unity.Gs2Formation.Model.EzSlotWithSignature[] slots,
            string keyId
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Formation.Domain.Model.EzPropertyFormGameSessionDomain> self)
            {
                var future = this._domain.SetWithSignatureFuture(
                    new SetPropertyFormWithSignatureRequest()
                        .WithSlots(slots?.Select(v => v.ToModel()).ToArray())
                        .WithKeyId(keyId)
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Formation.Domain.Model.EzPropertyFormGameSessionDomain(future.Result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Formation.Domain.Model.EzPropertyFormGameSessionDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Formation.Domain.Model.EzPropertyFormGameSessionDomain> SetPropertyFormAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Formation.Domain.Model.EzPropertyFormGameSessionDomain> SetPropertyFormFuture(
        #endif
            Gs2.Unity.Gs2Formation.Model.EzSlotWithSignature[] slots,
            string keyId
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.SetWithSignatureAsync(
                        new SetPropertyFormWithSignatureRequest()
                            .WithSlots(slots?.Select(v => v.ToModel()).ToArray())
                            .WithKeyId(keyId)
                            .WithAccessToken(_domain.AccessToken.Token)
                    );
                }
            );
            return new Gs2.Unity.Gs2Formation.Domain.Model.EzPropertyFormGameSessionDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Formation.Domain.Model.EzPropertyFormGameSessionDomain> self)
            {
                var future = _domain.SetWithSignatureFuture(
                    new SetPropertyFormWithSignatureRequest()
                        .WithSlots(slots?.Select(v => v.ToModel()).ToArray())
                        .WithKeyId(keyId)
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return _profile.RunFuture(
                    _domain.AccessToken,
                    future,
                    () =>
        			{
                		return future = _domain.SetWithSignatureFuture(
                    		new SetPropertyFormWithSignatureRequest()
        	                .WithSlots(slots?.Select(v => v.ToModel()).ToArray())
                	        .WithKeyId(keyId)
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
                self.OnComplete(new Gs2.Unity.Gs2Formation.Domain.Model.EzPropertyFormGameSessionDomain(result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Formation.Domain.Model.EzPropertyFormGameSessionDomain>(Impl);
        #endif
        }

        [Obsolete("The name has been changed to DeletePropertyFormFuture.")]
        public IFuture<Gs2.Unity.Gs2Formation.Domain.Model.EzPropertyFormGameSessionDomain> DeletePropertyForm(
        )
        {
            return DeletePropertyFormFuture(
            );
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Formation.Domain.Model.EzPropertyFormGameSessionDomain> DeletePropertyFormFuture(
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Formation.Domain.Model.EzPropertyFormGameSessionDomain> self)
            {
                var future = this._domain.DeleteFuture(
                    new DeletePropertyFormRequest()
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Formation.Domain.Model.EzPropertyFormGameSessionDomain(future.Result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Formation.Domain.Model.EzPropertyFormGameSessionDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Formation.Domain.Model.EzPropertyFormGameSessionDomain> DeletePropertyFormAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Formation.Domain.Model.EzPropertyFormGameSessionDomain> DeletePropertyFormFuture(
        #endif
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.DeleteAsync(
                        new DeletePropertyFormRequest()
                            .WithAccessToken(_domain.AccessToken.Token)
                    );
                }
            );
            return new Gs2.Unity.Gs2Formation.Domain.Model.EzPropertyFormGameSessionDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Formation.Domain.Model.EzPropertyFormGameSessionDomain> self)
            {
                var future = _domain.DeleteFuture(
                    new DeletePropertyFormRequest()
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return _profile.RunFuture(
                    _domain.AccessToken,
                    future,
                    () =>
        			{
                		return future = _domain.DeleteFuture(
                    		new DeletePropertyFormRequest()
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
                self.OnComplete(new Gs2.Unity.Gs2Formation.Domain.Model.EzPropertyFormGameSessionDomain(result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Formation.Domain.Model.EzPropertyFormGameSessionDomain>(Impl);
        #endif
        }

        [Obsolete("The name has been changed to ModelFuture.")]
        public IFuture<Gs2.Unity.Gs2Formation.Model.EzPropertyForm> Model()
        {
            return ModelFuture();
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Formation.Model.EzPropertyForm> ModelFuture()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Formation.Model.EzPropertyForm> self)
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
            return new Gs2InlineFuture<Gs2.Unity.Gs2Formation.Model.EzPropertyForm>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Formation.Model.EzPropertyForm> ModelAsync()
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
            return Gs2.Unity.Gs2Formation.Model.EzPropertyForm.FromModel(
                item
            );
        }
        #else
        public IFuture<Gs2.Unity.Gs2Formation.Model.EzPropertyForm> ModelFuture()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Formation.Model.EzPropertyForm> self)
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
                self.OnComplete(Gs2.Unity.Gs2Formation.Model.EzPropertyForm.FromModel(
                    item
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Formation.Model.EzPropertyForm>(Impl);
        }
        #endif

        public ulong Subscribe(Action<Gs2.Unity.Gs2Formation.Model.EzPropertyForm> callback)
        {
            return this._domain.Subscribe(item => {
                callback.Invoke(Gs2.Unity.Gs2Formation.Model.EzPropertyForm.FromModel(
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
