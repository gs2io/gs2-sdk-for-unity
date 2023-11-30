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

    public partial class EzBlackListGameSessionDomain {
        private readonly Gs2.Gs2Friend.Domain.Model.BlackListAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.GameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzBlackListGameSessionDomain(
            Gs2.Gs2Friend.Domain.Model.BlackListAccessTokenDomain domain,
            Gs2.Unity.Util.GameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }

        [Obsolete("The name has been changed to RegisterBlackListFuture.")]
        public IFuture<Gs2.Unity.Gs2Friend.Domain.Model.EzBlackListGameSessionDomain> RegisterBlackList(
            string targetUserId
        )
        {
            return RegisterBlackListFuture(
                targetUserId
            );
        }

        public IFuture<Gs2.Unity.Gs2Friend.Domain.Model.EzBlackListGameSessionDomain> RegisterBlackListFuture(
            string targetUserId
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Friend.Domain.Model.EzBlackListGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.RegisterFuture(
                        new RegisterBlackListRequest()
                            .WithTargetUserId(targetUserId)
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Friend.Domain.Model.EzBlackListGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Friend.Domain.Model.EzBlackListGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Friend.Domain.Model.EzBlackListGameSessionDomain> RegisterBlackListAsync(
            string targetUserId
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.RegisterAsync(
                    new RegisterBlackListRequest()
                        .WithTargetUserId(targetUserId)
                )
            );
            return new Gs2.Unity.Gs2Friend.Domain.Model.EzBlackListGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        [Obsolete("The name has been changed to UnregisterBlackListFuture.")]
        public IFuture<Gs2.Unity.Gs2Friend.Domain.Model.EzBlackListGameSessionDomain> UnregisterBlackList(
            string targetUserId
        )
        {
            return UnregisterBlackListFuture(
                targetUserId
            );
        }

        public IFuture<Gs2.Unity.Gs2Friend.Domain.Model.EzBlackListGameSessionDomain> UnregisterBlackListFuture(
            string targetUserId
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Friend.Domain.Model.EzBlackListGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.UnregisterFuture(
                        new UnregisterBlackListRequest()
                            .WithTargetUserId(targetUserId)
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Friend.Domain.Model.EzBlackListGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Friend.Domain.Model.EzBlackListGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Friend.Domain.Model.EzBlackListGameSessionDomain> UnregisterBlackListAsync(
            string targetUserId
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.UnregisterAsync(
                    new UnregisterBlackListRequest()
                        .WithTargetUserId(targetUserId)
                )
            );
            return new Gs2.Unity.Gs2Friend.Domain.Model.EzBlackListGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        [Obsolete("The name has been changed to ModelFuture.")]
        public IFuture<Gs2.Unity.Gs2Friend.Model.EzBlackList> Model()
        {
            return ModelFuture();
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Friend.Model.EzBlackList> ModelAsync()
        {
            var item = await this._connection.RunAsync(
                this._gameSession,
                async () =>
                {
                    return await _domain.ModelAsync();
                }
            );
            if (item == null) {
                return null;
            }
            return Gs2.Unity.Gs2Friend.Model.EzBlackList.FromModel(
                item
            );
        }
        #endif

        public IFuture<Gs2.Unity.Gs2Friend.Model.EzBlackList> ModelFuture()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Friend.Model.EzBlackList> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => {
                    	return _domain.ModelFuture();
                    }
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                var item = future.Result;
                if (item == null) {
                    self.OnComplete(null);
                    yield break;
                }
                self.OnComplete(Gs2.Unity.Gs2Friend.Model.EzBlackList.FromModel(
                    item
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Friend.Model.EzBlackList>(Impl);
        }

        public ulong Subscribe(Action<Gs2.Unity.Gs2Friend.Model.EzBlackList> callback)
        {
            return this._domain.Subscribe(item => {
                callback.Invoke(Gs2.Unity.Gs2Friend.Model.EzBlackList.FromModel(
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
