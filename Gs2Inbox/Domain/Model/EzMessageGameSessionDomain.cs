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
using Gs2.Gs2Inbox.Domain.Iterator;
using Gs2.Gs2Inbox.Request;
using Gs2.Gs2Inbox.Result;
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

namespace Gs2.Unity.Gs2Inbox.Domain.Model
{

    public partial class EzMessageGameSessionDomain {
        private readonly Gs2.Gs2Inbox.Domain.Model.MessageAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public string TransactionId => _domain.TransactionId;
        public bool? AutoRunStampSheet => _domain.AutoRunStampSheet;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;
        public string MessageName => _domain?.MessageName;

        public EzMessageGameSessionDomain(
            Gs2.Gs2Inbox.Domain.Model.MessageAccessTokenDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Inbox.Domain.Model.EzMessageGameSessionDomain> Read(
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Inbox.Domain.Model.EzMessageGameSessionDomain> self)
            {
                yield return ReadAsync(
                ).ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Inbox.Domain.Model.EzMessageGameSessionDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Inbox.Domain.Model.EzMessageGameSessionDomain> ReadAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Inbox.Domain.Model.EzMessageGameSessionDomain> Read(
        #endif
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.ReadAsync(
                        new ReadMessageRequest()
                            .WithAccessToken(_domain.AccessToken.Token)
                    );
                }
            );
            return new Gs2.Unity.Gs2Inbox.Domain.Model.EzMessageGameSessionDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Inbox.Domain.Model.EzMessageGameSessionDomain> self)
            {
                var future = _domain.Read(
                    new ReadMessageRequest()
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
                self.OnComplete(new Gs2.Unity.Gs2Inbox.Domain.Model.EzMessageGameSessionDomain(result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Inbox.Domain.Model.EzMessageGameSessionDomain>(Impl);
        #endif
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Inbox.Domain.Model.EzMessageGameSessionDomain> Delete(
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Inbox.Domain.Model.EzMessageGameSessionDomain> self)
            {
                yield return DeleteAsync(
                ).ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Inbox.Domain.Model.EzMessageGameSessionDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Inbox.Domain.Model.EzMessageGameSessionDomain> DeleteAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Inbox.Domain.Model.EzMessageGameSessionDomain> Delete(
        #endif
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.DeleteAsync(
                        new DeleteMessageRequest()
                            .WithAccessToken(_domain.AccessToken.Token)
                    );
                }
            );
            return new Gs2.Unity.Gs2Inbox.Domain.Model.EzMessageGameSessionDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Inbox.Domain.Model.EzMessageGameSessionDomain> self)
            {
                var future = _domain.Delete(
                    new DeleteMessageRequest()
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
                self.OnComplete(new Gs2.Unity.Gs2Inbox.Domain.Model.EzMessageGameSessionDomain(result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Inbox.Domain.Model.EzMessageGameSessionDomain>(Impl);
        #endif
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Inbox.Model.EzMessage> Model()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Inbox.Model.EzMessage> self)
            {
                yield return ModelAsync().ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Inbox.Model.EzMessage>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Inbox.Model.EzMessage> ModelAsync()
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
            return Gs2.Unity.Gs2Inbox.Model.EzMessage.FromModel(
                item
            );
        }
        #else
        public IFuture<Gs2.Unity.Gs2Inbox.Model.EzMessage> Model()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Inbox.Model.EzMessage> self)
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
                self.OnComplete(Gs2.Unity.Gs2Inbox.Model.EzMessage.FromModel(
                    item
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Inbox.Model.EzMessage>(Impl);
        }
        #endif

    }
}
