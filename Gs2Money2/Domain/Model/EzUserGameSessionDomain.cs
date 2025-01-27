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
using Gs2.Gs2Money2.Domain.Iterator;
using Gs2.Gs2Money2.Request;
using Gs2.Gs2Money2.Result;
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

namespace Gs2.Unity.Gs2Money2.Domain.Model
{

    public partial class EzUserGameSessionDomain {
        private readonly Gs2.Gs2Money2.Domain.Model.UserAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.IGameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public Gs2.Unity.Gs2Money2.Model.EzDepositTransaction[] WithdrawTransactions => _domain.WithdrawTransactions.Select(Gs2.Unity.Gs2Money2.Model.EzDepositTransaction.FromModel).ToArray();
        public string? NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserGameSessionDomain(
            Gs2.Gs2Money2.Domain.Model.UserAccessTokenDomain domain,
            Gs2.Unity.Util.IGameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }

        public Gs2Iterator<Gs2.Unity.Gs2Money2.Model.EzWallet> Wallets(
        )
        {
            return new Gs2.Unity.Gs2Money2.Domain.Iterator.EzListIterator(
                this._domain,
                this._gameSession,
                this._connection
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Money2.Model.EzWallet> WalletsAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Money2.Model.EzWallet>(async (writer, token) =>
            {
                var it = _domain.WalletsAsync(
                ).GetAsyncEnumerator();
                while(
                    await this._connection.RunIteratorAsync(
                        this._gameSession,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.WalletsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Money2.Model.EzWallet.FromModel(it.Current));
                }
            });
        }
        #endif

        public ulong SubscribeWallets(
            Action<Gs2.Unity.Gs2Money2.Model.EzWallet[]> callback
        ) {
            return this._domain.SubscribeWallets(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Money2.Model.EzWallet.FromModel).ToArray());
                }
            );
        }

        public void UnsubscribeWallets(
            ulong callbackId
        ) {
            this._domain.UnsubscribeWallets(
                callbackId
            );
        }

        public void InvalidateWallets(
        ) {
            this._domain.InvalidateWallets(
            );
        }

        public Gs2.Unity.Gs2Money2.Domain.Model.EzWalletGameSessionDomain Wallet(
            int slot
        ) {
            return new Gs2.Unity.Gs2Money2.Domain.Model.EzWalletGameSessionDomain(
                _domain.Wallet(
                    slot
                ),
                this._gameSession,
                this._connection
            );
        }

    }
}
