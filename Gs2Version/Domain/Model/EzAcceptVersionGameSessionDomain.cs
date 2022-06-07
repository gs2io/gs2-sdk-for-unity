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
using Gs2.Gs2Version.Domain.Iterator;
using Gs2.Gs2Version.Request;
using Gs2.Gs2Version.Result;
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

namespace Gs2.Unity.Gs2Version.Domain.Model
{

    public partial class EzAcceptVersionGameSessionDomain {
        private readonly Gs2.Gs2Version.Domain.Model.AcceptVersionAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;
        public string VersionName => _domain?.VersionName;

        public EzAcceptVersionGameSessionDomain(
            Gs2.Gs2Version.Domain.Model.AcceptVersionAccessTokenDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Version.Domain.Model.EzAcceptVersionGameSessionDomain> Accept(
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Version.Domain.Model.EzAcceptVersionGameSessionDomain> self)
            {
                yield return AcceptAsync(
                ).ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Version.Domain.Model.EzAcceptVersionGameSessionDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Version.Domain.Model.EzAcceptVersionGameSessionDomain> AcceptAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Version.Domain.Model.EzAcceptVersionGameSessionDomain> Accept(
        #endif
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.AcceptAsync(
                        new AcceptRequest()
                            .WithAccessToken(_domain.AccessToken.Token)
                    );
                }
            );
            return new Gs2.Unity.Gs2Version.Domain.Model.EzAcceptVersionGameSessionDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Version.Domain.Model.EzAcceptVersionGameSessionDomain> self)
            {
                var future = _domain.Accept(
                    new AcceptRequest()
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return _profile.RunFuture(
                    _domain.AccessToken,
                    future
                );
                if (future.Error != null)
                {
                    self.OnError(future.Error);
                    yield break;
                }
                var result = future.Result;
                self.OnComplete(new Gs2.Unity.Gs2Version.Domain.Model.EzAcceptVersionGameSessionDomain(result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Version.Domain.Model.EzAcceptVersionGameSessionDomain>(Impl);
        #endif
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Version.Domain.Model.EzAcceptVersionGameSessionDomain> Delete(
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Version.Domain.Model.EzAcceptVersionGameSessionDomain> self)
            {
                yield return DeleteAsync(
                ).ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Version.Domain.Model.EzAcceptVersionGameSessionDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Version.Domain.Model.EzAcceptVersionGameSessionDomain> DeleteAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Version.Domain.Model.EzAcceptVersionGameSessionDomain> Delete(
        #endif
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.DeleteAsync(
                        new DeleteAcceptVersionRequest()
                            .WithAccessToken(_domain.AccessToken.Token)
                    );
                }
            );
            return new Gs2.Unity.Gs2Version.Domain.Model.EzAcceptVersionGameSessionDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Version.Domain.Model.EzAcceptVersionGameSessionDomain> self)
            {
                var future = _domain.Delete(
                    new DeleteAcceptVersionRequest()
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return _profile.RunFuture(
                    _domain.AccessToken,
                    future
                );
                if (future.Error != null)
                {
                    self.OnError(future.Error);
                    yield break;
                }
                var result = future.Result;
                self.OnComplete(new Gs2.Unity.Gs2Version.Domain.Model.EzAcceptVersionGameSessionDomain(result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Version.Domain.Model.EzAcceptVersionGameSessionDomain>(Impl);
        #endif
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Version.Model.EzAcceptVersion> Model()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Version.Model.EzAcceptVersion> self)
            {
                yield return ModelAsync().ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Version.Model.EzAcceptVersion>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Version.Model.EzAcceptVersion> ModelAsync()
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
            return Gs2.Unity.Gs2Version.Model.EzAcceptVersion.FromModel(
                item
            );
        }
        #else
        public IFuture<Gs2.Unity.Gs2Version.Model.EzAcceptVersion> Model()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Version.Model.EzAcceptVersion> self)
            {
                var future = _domain.Model();
                yield return _profile.RunFuture(
                    _domain.AccessToken,
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
                self.OnComplete(Gs2.Unity.Gs2Version.Model.EzAcceptVersion.FromModel(
                    item
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Version.Model.EzAcceptVersion>(Impl);
        }
        #endif

    }
}
