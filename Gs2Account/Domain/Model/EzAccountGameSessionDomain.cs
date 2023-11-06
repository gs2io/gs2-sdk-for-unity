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

    public partial class EzAccountGameSessionDomain {
        private readonly Gs2.Gs2Account.Domain.Model.AccountAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public Gs2.Unity.Gs2Account.Model.EzBanStatus[] BanStatuses => _domain.BanStatuses.Select(Gs2.Unity.Gs2Account.Model.EzBanStatus.FromModel).ToArray();
        public string Body => _domain.Body;
        public string Signature => _domain.Signature;
        public string NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzAccountGameSessionDomain(
            Gs2.Gs2Account.Domain.Model.AccountAccessTokenDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        public class EzTakeOversIterator : Gs2Iterator<Gs2.Unity.Gs2Account.Model.EzTakeOver>
        {
            private Gs2Iterator<Gs2.Gs2Account.Model.TakeOver> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly Gs2.Gs2Account.Domain.Model.AccountAccessTokenDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzTakeOversIterator(
                Gs2Iterator<Gs2.Gs2Account.Model.TakeOver> it,
        #if !GS2_ENABLE_UNITASK
                Gs2.Gs2Account.Domain.Model.AccountAccessTokenDomain domain,
        #endif
                Gs2.Unity.Util.Profile profile
            )
            {
                _it = it;
        #if !GS2_ENABLE_UNITASK
                _domain = domain;
        #endif
                _profile = profile;
            }

            public override bool HasNext()
            {
                return _it.HasNext();
            }

            protected override IEnumerator Next(Action<AsyncResult<Gs2.Unity.Gs2Account.Model.EzTakeOver>> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    _domain.AccessToken,
                    _it,
                    () =>
                    {
                        return _it = _domain.TakeOvers(
                        );
                    }
                );
        #endif
                callback.Invoke(
                    new AsyncResult<Gs2.Unity.Gs2Account.Model.EzTakeOver>(
                        _it.Current == null ? null : Gs2.Unity.Gs2Account.Model.EzTakeOver.FromModel(_it.Current),
                        _it.Error
                    )
                );
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Account.Model.EzTakeOver> TakeOvers(
        )
        {
            return new EzTakeOversIterator(
                _domain.TakeOvers(
                ),
                _profile
            );
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
                while(
                    await _profile.RunIteratorAsync(
                        _domain.AccessToken,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.TakeOversAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Account.Model.EzTakeOver.FromModel(it.Current));
                }
            });
        #else
            return new EzTakeOversIterator(
                _domain.TakeOvers(
                ),
                _domain,
                _profile
            );
        #endif
        }

        public ulong SubscribeTakeOvers(Action callback) {
            return this._domain.SubscribeTakeOvers(callback);
        }

        public void UnsubscribeTakeOvers(ulong callbackId) {
            this._domain.UnsubscribeTakeOvers(callbackId);
        }

        public Gs2.Unity.Gs2Account.Domain.Model.EzTakeOverGameSessionDomain TakeOver(
            int type
        ) {
            return new Gs2.Unity.Gs2Account.Domain.Model.EzTakeOverGameSessionDomain(
                _domain.TakeOver(
                    type
                ),
                _profile
            );
        }

        [Obsolete("The name has been changed to ModelFuture.")]
        public IFuture<Gs2.Unity.Gs2Account.Model.EzAccount> Model()
        {
            return ModelFuture();
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Account.Model.EzAccount> ModelFuture()
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
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.ModelAsync();
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
        public IFuture<Gs2.Unity.Gs2Account.Model.EzAccount> ModelFuture()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Account.Model.EzAccount> self)
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
                self.OnComplete(Gs2.Unity.Gs2Account.Model.EzAccount.FromModel(
                    item
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Account.Model.EzAccount>(Impl);
        }
        #endif

        public ulong Subscribe(Action<Gs2.Unity.Gs2Account.Model.EzAccount> callback)
        {
            return this._domain.Subscribe(item => {
                callback.Invoke(Gs2.Unity.Gs2Account.Model.EzAccount.FromModel(
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
