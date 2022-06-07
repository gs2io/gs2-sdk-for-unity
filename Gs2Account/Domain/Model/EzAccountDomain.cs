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
using Gs2.Gs2Account.Domain.Iterator;
using Gs2.Gs2Account.Request;
using Gs2.Gs2Account.Result;
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

namespace Gs2.Unity.Gs2Account.Domain.Model
{

    public partial class EzAccountDomain {
        private readonly Gs2.Gs2Account.Domain.Model.AccountDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public string Body => _domain.Body;
        public string Signature => _domain.Signature;
        public string NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzAccountDomain(
            Gs2.Gs2Account.Domain.Model.AccountDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain> Authentication(
              string keyId,
              string password
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain> self)
            {
                yield return AuthenticationAsync(
                    keyId,
                    password
                ).ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain> AuthenticationAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain> Authentication(
        #endif
              string keyId,
              string password
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                null,
                async () =>
                {
                    return await _domain.AuthenticationAsync(
                        new AuthenticationRequest()
                            .WithKeyId(keyId)
                            .WithPassword(password)
                    );
                }
            );
            return new Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain> self)
            {
                var future = _domain.Authentication(
                    new AuthenticationRequest()
                        .WithKeyId(keyId)
                        .WithPassword(password)
                );
                yield return _profile.RunFuture(
                    null,
                    future
                );
                if (future.Error != null)
                {
                    self.OnError(future.Error);
                    yield break;
                }
                var result = future.Result;
                self.OnComplete(new Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain(result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain>(Impl);
        #endif
        }

        public class EzTakeOversIterator : Gs2Iterator<Gs2.Unity.Gs2Account.Model.EzTakeOver>
        {
            private readonly Gs2Iterator<Gs2.Gs2Account.Model.TakeOver> _it;

            public EzTakeOversIterator(
                Gs2Iterator<Gs2.Gs2Account.Model.TakeOver> it
            )
            {
                _it = it;
            }

            public override bool HasNext()
            {
                return _it.HasNext();
            }

            protected override IEnumerator Next(Action<Gs2.Unity.Gs2Account.Model.EzTakeOver> callback)
            {
                yield return _it.Next();
                callback.Invoke(_it.Current == null ? null : Gs2.Unity.Gs2Account.Model.EzTakeOver.FromModel(_it.Current));
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Account.Model.EzTakeOver> TakeOvers(
        )
        {
            return new EzTakeOversIterator(_domain.TakeOvers(
            ));
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Account.Model.EzTakeOver> TakeOversAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Account.Model.EzTakeOver> TakeOvers(
        #endif
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Account.Model.EzTakeOver>(async (writer, token) =>
            {
                var it = _domain.TakeOversAsync(
                ).GetAsyncEnumerator();
                while(await it.MoveNextAsync())
                {
                    await writer.YieldAsync(Gs2.Unity.Gs2Account.Model.EzTakeOver.FromModel(it.Current));
                }
            });
        #else
            return new EzTakeOversIterator(_domain.TakeOvers(
            ));
        #endif
        }

        public Gs2.Unity.Gs2Account.Domain.Model.EzTakeOverDomain TakeOver(
            int type
        ) {
            return new Gs2.Unity.Gs2Account.Domain.Model.EzTakeOverDomain(
                _domain.TakeOver(
                    type
                ),
                _profile
            );
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Account.Model.EzAccount> Model()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Account.Model.EzAccount> self)
            {
                yield return ModelAsync().ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Account.Model.EzAccount>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Account.Model.EzAccount> ModelAsync()
        {
            var item = await _profile.RunAsync(
                null,
                async () =>
                {
                    return await _domain.Model();
                }
            );
            if (item == null) {
                return null;
            }
            return Gs2.Unity.Gs2Account.Model.EzAccount.FromModel(
                item
            );
        }
        #else
        public IFuture<Gs2.Unity.Gs2Account.Model.EzAccount> Model()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Account.Model.EzAccount> self)
            {
                var future = _domain.Model();
                yield return _profile.RunFuture(
                    null,
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
                self.OnComplete(Gs2.Unity.Gs2Account.Model.EzAccount.FromModel(
                    item
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Account.Model.EzAccount>(Impl);
        }
        #endif

    }
}
