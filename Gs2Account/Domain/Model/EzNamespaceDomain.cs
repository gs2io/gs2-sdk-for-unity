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
#pragma warning disable CS0169, CS0168

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
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string Status => _domain.Status;
        public string Url => _domain.Url;
        public string UploadToken => _domain.UploadToken;
        public string UploadUrl => _domain.UploadUrl;
        public string NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;

        public EzNamespaceDomain(
            Gs2.Gs2Account.Domain.Model.NamespaceDomain domain,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._connection = connection;
        }

        [Obsolete("The name has been changed to CreateFuture.")]
        public IFuture<Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain> Create(
        )
        {
            return CreateFuture(
            );
        }

        public IFuture<Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain> CreateFuture(
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain> self)
            {
                var future = this._connection.RunFuture(
                    null,
                    () => this._domain.CreateAccountFuture(
                        new CreateAccountRequest()
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain(
                    future.Result,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain> CreateAsync(
        ) {
            var result = await this._connection.RunAsync(
                null,
                () => this._domain.CreateAccountAsync(
                    new CreateAccountRequest()
                )
            );
            return new Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain(
                result,
                this._connection
            );
        }
        #endif

        [Obsolete("The name has been changed to DoTakeOverFuture.")]
        public IFuture<Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain> DoTakeOver(
            int type,
            string userIdentifier,
            string password
        )
        {
            return DoTakeOverFuture(
                type,
                userIdentifier,
                password
            );
        }

        public IFuture<Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain> DoTakeOverFuture(
            int type,
            string userIdentifier,
            string password
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain> self)
            {
                var future = this._connection.RunFuture(
                    null,
                    () => this._domain.DoTakeOverFuture(
                        new DoTakeOverRequest()
                            .WithType(type)
                            .WithUserIdentifier(userIdentifier)
                            .WithPassword(password)
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain(
                    future.Result,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain> DoTakeOverAsync(
            int type,
            string userIdentifier,
            string password
        ) {
            var result = await this._connection.RunAsync(
                null,
                () => this._domain.DoTakeOverAsync(
                    new DoTakeOverRequest()
                        .WithType(type)
                        .WithUserIdentifier(userIdentifier)
                        .WithPassword(password)
                )
            );
            return new Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain(
                result,
                this._connection
            );
        }
        #endif

        public Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain Account(
            string userId
        ) {
            return new Gs2.Unity.Gs2Account.Domain.Model.EzAccountDomain(
                _domain.Account(
                    userId
                ),
                this._connection
            );
        }

        public EzAccountGameSessionDomain Me(
            Gs2.Unity.Util.GameSession gameSession
        ) {
            return new EzAccountGameSessionDomain(
                _domain.AccessToken(
                    gameSession.AccessToken
                ),
                gameSession,
                this._connection
            );
        }

    }
}
