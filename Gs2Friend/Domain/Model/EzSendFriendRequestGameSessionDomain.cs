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
using Gs2.Gs2Friend.Domain.Iterator;
using Gs2.Gs2Friend.Request;
using Gs2.Gs2Friend.Result;
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

namespace Gs2.Unity.Gs2Friend.Domain.Model
{

    public partial class EzSendFriendRequestGameSessionDomain {
        private readonly Gs2.Gs2Friend.Domain.Model.SendFriendRequestAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;
        public string TargetUserId => _domain?.TargetUserId;

        public EzSendFriendRequestGameSessionDomain(
            Gs2.Gs2Friend.Domain.Model.SendFriendRequestAccessTokenDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        [Obsolete("The name has been changed to DeleteRequestFuture.")]
        public IFuture<Gs2.Unity.Gs2Friend.Domain.Model.EzFriendRequestGameSessionDomain> DeleteRequest(
        )
        {
            return DeleteRequestFuture(
            );
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Friend.Domain.Model.EzFriendRequestGameSessionDomain> DeleteRequestFuture(
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Friend.Domain.Model.EzFriendRequestGameSessionDomain> self)
            {
                var future = this._domain.DeleteFuture(
                    new DeleteRequestRequest()
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Friend.Domain.Model.EzFriendRequestGameSessionDomain(future.Result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Friend.Domain.Model.EzFriendRequestGameSessionDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Friend.Domain.Model.EzFriendRequestGameSessionDomain> DeleteRequestAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Friend.Domain.Model.EzFriendRequestGameSessionDomain> DeleteRequestFuture(
        #endif
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.DeleteAsync(
                        new DeleteRequestRequest()
                            .WithAccessToken(_domain.AccessToken.Token)
                    );
                }
            );
            return new Gs2.Unity.Gs2Friend.Domain.Model.EzFriendRequestGameSessionDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Friend.Domain.Model.EzFriendRequestGameSessionDomain> self)
            {
                var future = _domain.DeleteFuture(
                    new DeleteRequestRequest()
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return _profile.RunFuture(
                    _domain.AccessToken,
                    future,
                    () =>
        			{
                		return future = _domain.DeleteFuture(
                    		new DeleteRequestRequest()
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
                self.OnComplete(new Gs2.Unity.Gs2Friend.Domain.Model.EzFriendRequestGameSessionDomain(result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Friend.Domain.Model.EzFriendRequestGameSessionDomain>(Impl);
        #endif
        }

        [Obsolete("The name has been changed to ModelFuture.")]
        public IFuture<Gs2.Unity.Gs2Friend.Model.EzFriendRequest> Model()
        {
            return ModelFuture();
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Friend.Model.EzFriendRequest> ModelFuture()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Friend.Model.EzFriendRequest> self)
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
            return new Gs2InlineFuture<Gs2.Unity.Gs2Friend.Model.EzFriendRequest>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Friend.Model.EzFriendRequest> ModelAsync()
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
            return Gs2.Unity.Gs2Friend.Model.EzFriendRequest.FromModel(
                item
            );
        }
        #else
        public IFuture<Gs2.Unity.Gs2Friend.Model.EzFriendRequest> ModelFuture()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Friend.Model.EzFriendRequest> self)
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
                self.OnComplete(Gs2.Unity.Gs2Friend.Model.EzFriendRequest.FromModel(
                    item
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Friend.Model.EzFriendRequest>(Impl);
        }
        #endif

        public ulong Subscribe(Action<Gs2.Unity.Gs2Friend.Model.EzFriendRequest> callback)
        {
            return this._domain.Subscribe(item => {
                callback.Invoke(Gs2.Unity.Gs2Friend.Model.EzFriendRequest.FromModel(
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
