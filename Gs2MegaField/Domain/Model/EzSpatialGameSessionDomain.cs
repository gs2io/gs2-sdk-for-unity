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
using Gs2.Gs2MegaField.Domain.Iterator;
using Gs2.Gs2MegaField.Request;
using Gs2.Gs2MegaField.Result;
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

namespace Gs2.Unity.Gs2MegaField.Domain.Model
{

    public partial class EzSpatialGameSessionDomain {
        private readonly Gs2.Gs2MegaField.Domain.Model.SpatialAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;
        public string AreaModelName => _domain?.AreaModelName;
        public string LayerModelName => _domain?.LayerModelName;

        public EzSpatialGameSessionDomain(
            Gs2.Gs2MegaField.Domain.Model.SpatialAccessTokenDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2MegaField.Domain.Model.EzSpatialGameSessionDomain[]> Update(
              Gs2.Unity.Gs2MegaField.Model.EzMyPosition position,
              Gs2.Unity.Gs2MegaField.Model.EzScope scope
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2MegaField.Domain.Model.EzSpatialGameSessionDomain[]> self)
            {
                yield return UpdateAsync(
                    position,
                    scope
                ).ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2MegaField.Domain.Model.EzSpatialGameSessionDomain[]>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2MegaField.Domain.Model.EzSpatialGameSessionDomain[]> UpdateAsync(
        #else
        public IFuture<Gs2.Unity.Gs2MegaField.Domain.Model.EzSpatialGameSessionDomain[]> Update(
        #endif
              Gs2.Unity.Gs2MegaField.Model.EzMyPosition position,
              Gs2.Unity.Gs2MegaField.Model.EzScope scope
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.ActionAsync(
                        new ActionRequest()
                            .WithPosition(position?.ToModel())
                            .WithScope(scope?.ToModel())
                            .WithAccessToken(_domain.AccessToken.Token)
                    );
                }
            );
            return result.Select(v => new Gs2.Unity.Gs2MegaField.Domain.Model.EzSpatialGameSessionDomain(v, _profile)).ToArray();
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2MegaField.Domain.Model.EzSpatialGameSessionDomain[]> self)
            {
                var future = _domain.Action(
                    new ActionRequest()
                        .WithPosition(position?.ToModel())
                        .WithScope(scope?.ToModel())
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return _profile.RunFuture(
                    _domain.AccessToken,
                    future,
                    () =>
        			{
                		return future = _domain.Action(
                    		new ActionRequest()
            	            .WithPosition(position?.ToModel())
            	            .WithScope(scope?.ToModel())
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
                self.OnComplete(result.Select(v => new Gs2.Unity.Gs2MegaField.Domain.Model.EzSpatialGameSessionDomain(v, _profile)).ToArray());
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2MegaField.Domain.Model.EzSpatialGameSessionDomain[]>(Impl);
        #endif
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2MegaField.Model.EzSpatial> Model()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2MegaField.Model.EzSpatial> self)
            {
                yield return ModelAsync().ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2MegaField.Model.EzSpatial>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2MegaField.Model.EzSpatial> ModelAsync()
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
            return Gs2.Unity.Gs2MegaField.Model.EzSpatial.FromModel(
                item
            );
        }
        #else
        public IFuture<Gs2.Unity.Gs2MegaField.Model.EzSpatial> Model()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2MegaField.Model.EzSpatial> self)
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
                self.OnComplete(Gs2.Unity.Gs2MegaField.Model.EzSpatial.FromModel(
                    item
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2MegaField.Model.EzSpatial>(Impl);
        }
        #endif

    }
}