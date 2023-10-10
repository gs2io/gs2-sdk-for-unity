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

    public partial class EzNamespaceDomain {
        private readonly Gs2.Gs2Account.Domain.Model.NamespaceDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public string Status => _domain.Status;
        public string Url => _domain.Url;
        public string NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;

        public EzNamespaceDomain(
            Gs2.Gs2Account.Domain.Model.NamespaceDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain> Create(
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain> self)
            {
                yield return CreateAsync(
                ).ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain> CreateAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain> Create(
        #endif
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                null,
                async () =>
                {
                    return await _domain.CreateAccountAsync(
                        new CreateAccountRequest()
                    );
                }
            );
            return new Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain> self)
            {
                var future = _domain.CreateAccount(
                    new CreateAccountRequest()
                );
                yield return _profile.RunFuture(
                    null,
                    future,
                    () =>
        			{
                		return future = _domain.CreateAccount(
                    		new CreateAccountRequest()
        		        );
        			}
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

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain> DoTakeOver(
              int type,
              string userIdentifier,
              string password
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain> self)
            {
                yield return DoTakeOverAsync(
                    type,
                    userIdentifier,
                    password
                ).ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain> DoTakeOverAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain> DoTakeOver(
        #endif
              int type,
              string userIdentifier,
              string password
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                null,
                async () =>
                {
                    return await _domain.DoTakeOverAsync(
                        new DoTakeOverRequest()
                            .WithType(type)
                            .WithUserIdentifier(userIdentifier)
                            .WithPassword(password)
                    );
                }
            );
            return new Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain> self)
            {
                var future = _domain.DoTakeOver(
                    new DoTakeOverRequest()
                        .WithType(type)
                        .WithUserIdentifier(userIdentifier)
                        .WithPassword(password)
                );
                yield return _profile.RunFuture(
                    null,
                    future,
                    () =>
        			{
                		return future = _domain.DoTakeOver(
                    		new DoTakeOverRequest()
                	        .WithType(type)
                	        .WithUserIdentifier(userIdentifier)
                	        .WithPassword(password)
        		        );
        			}
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

        public class EzAccountsIterator : Gs2Iterator<Gs2.Unity.Gs2Account.Model.EzAccount>
        {
            private Gs2Iterator<Gs2.Gs2Account.Model.Account> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly Gs2.Gs2Account.Domain.Model.NamespaceDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzAccountsIterator(
                Gs2Iterator<Gs2.Gs2Account.Model.Account> it,
        #if !GS2_ENABLE_UNITASK
                Gs2.Gs2Account.Domain.Model.NamespaceDomain domain,
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

            protected override IEnumerator Next(Action<AsyncResult<Gs2.Unity.Gs2Account.Model.EzAccount>> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    null,
                    _it,
                    () =>
                    {
                        return _it = _domain.Accounts(
                        );
                    }
                );
        #endif
                callback.Invoke(
                    new AsyncResult<Gs2.Unity.Gs2Account.Model.EzAccount>(
                        _it.Current == null ? null : Gs2.Unity.Gs2Account.Model.EzAccount.FromModel(_it.Current),
                        _it.Error
                    )
                );
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Account.Model.EzAccount> Accounts(
        )
        {
            return new EzAccountsIterator(
                _domain.Accounts(
                ),
                _profile
            );
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Account.Model.EzAccount> AccountsAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Account.Model.EzAccount> Accounts(
        #endif
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Account.Model.EzAccount>(async (writer, token) =>
            {
                var it = _domain.AccountsAsync(
                ).GetAsyncEnumerator();
                while(
                    await _profile.RunIteratorAsync(
                        null,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.AccountsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Account.Model.EzAccount.FromModel(it.Current));
                }
            });
        #else
            return new EzAccountsIterator(
                _domain.Accounts(
                ),
                _domain,
                _profile
            );
        #endif
        }

        public ulong SubscribeAccounts(Action callback) {
            return this._domain.SubscribeAccounts(callback);
        }

        public void UnsubscribeAccounts(ulong callbackId) {
            this._domain.UnsubscribeAccounts(callbackId);
        }

        public Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain Account(
            string userId
        ) {
            return new Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain(
                _domain.Account(
                    userId
                ),
                _profile
            );
        }

        public EzAccountGameSessionDomain Me(
            Gs2.Unity.Util.GameSession gameSession
        ) {
            return new EzAccountGameSessionDomain(
                _domain.AccessToken(
                    gameSession.AccessToken
                ),
                _profile
            );
        }

    }
}
