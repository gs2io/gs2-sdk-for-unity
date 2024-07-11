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

    public partial class EzPlatformIdGameSessionDomain {
        private readonly Gs2.Gs2Account.Domain.Model.PlatformIdAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.IGameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;
        public int? Type => _domain?.Type;
        public string UserIdentifier => _domain?.UserIdentifier;

        public EzPlatformIdGameSessionDomain(
            Gs2.Gs2Account.Domain.Model.PlatformIdAccessTokenDomain domain,
            Gs2.Unity.Util.IGameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }

        [Obsolete("The name has been changed to AddPlatformIdSettingFuture.")]
        public IFuture<Gs2.Unity.Gs2Account.Domain.Model.EzPlatformIdGameSessionDomain> AddPlatformIdSetting(
        )
        {
            return AddPlatformIdSettingFuture(
            );
        }

        public IFuture<Gs2.Unity.Gs2Account.Domain.Model.EzPlatformIdGameSessionDomain> AddPlatformIdSettingFuture(
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Account.Domain.Model.EzPlatformIdGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.CreateFuture(
                        new CreatePlatformIdRequest()
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Account.Domain.Model.EzPlatformIdGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Account.Domain.Model.EzPlatformIdGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Account.Domain.Model.EzPlatformIdGameSessionDomain> AddPlatformIdSettingAsync(
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.CreateAsync(
                    new CreatePlatformIdRequest()
                )
            );
            return new Gs2.Unity.Gs2Account.Domain.Model.EzPlatformIdGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        [Obsolete("The name has been changed to FindPlatformUserFuture.")]
        public IFuture<Gs2.Unity.Gs2Account.Model.EzPlatformUser> FindPlatformUser(
        )
        {
            return FindPlatformUserFuture(
            );
        }

        public IFuture<Gs2.Unity.Gs2Account.Model.EzPlatformUser> FindPlatformUserFuture(
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Account.Model.EzPlatformUser> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.FindFuture(
                        new FindPlatformIdRequest()
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(Gs2.Unity.Gs2Account.Model.EzPlatformUser.FromModel(
                    future.Result
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Account.Model.EzPlatformUser>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Account.Model.EzPlatformUser> FindPlatformUserAsync(
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.FindAsync(
                    new FindPlatformIdRequest()
                )
            );
            return Gs2.Unity.Gs2Account.Model.EzPlatformUser.FromModel(
                result
            );
        }
        #endif

        [Obsolete("The name has been changed to DeletePlatformIdSettingFuture.")]
        public IFuture<Gs2.Unity.Gs2Account.Domain.Model.EzPlatformIdGameSessionDomain> DeletePlatformIdSetting(
        )
        {
            return DeletePlatformIdSettingFuture(
            );
        }

        public IFuture<Gs2.Unity.Gs2Account.Domain.Model.EzPlatformIdGameSessionDomain> DeletePlatformIdSettingFuture(
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Account.Domain.Model.EzPlatformIdGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.DeleteFuture(
                        new DeletePlatformIdRequest()
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Account.Domain.Model.EzPlatformIdGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Account.Domain.Model.EzPlatformIdGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Account.Domain.Model.EzPlatformIdGameSessionDomain> DeletePlatformIdSettingAsync(
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.DeleteAsync(
                    new DeletePlatformIdRequest()
                )
            );
            return new Gs2.Unity.Gs2Account.Domain.Model.EzPlatformIdGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        [Obsolete("The name has been changed to ModelFuture.")]
        public IFuture<Gs2.Unity.Gs2Account.Model.EzPlatformId> Model()
        {
            return ModelFuture();
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Account.Model.EzPlatformId> ModelAsync()
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
            return Gs2.Unity.Gs2Account.Model.EzPlatformId.FromModel(
                item
            );
        }
        #endif

        public IFuture<Gs2.Unity.Gs2Account.Model.EzPlatformId> ModelFuture()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Account.Model.EzPlatformId> self)
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
                self.OnComplete(Gs2.Unity.Gs2Account.Model.EzPlatformId.FromModel(
                    item
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Account.Model.EzPlatformId>(Impl);
        }

        public void Invalidate()
        {
            this._domain.Invalidate();
        }

        public ulong Subscribe(Action<Gs2.Unity.Gs2Account.Model.EzPlatformId> callback)
        {
            return this._domain.Subscribe(item => {
                callback.Invoke(item == null ? null : Gs2.Unity.Gs2Account.Model.EzPlatformId.FromModel(
                    item
                ));
            });
        }

        public void Unsubscribe(ulong callbackId)
        {
            this._domain.Unsubscribe(callbackId);
        }

        #if UNITY_2017_1_OR_NEWER
        public Gs2Future<ulong> SubscribeWithInitialCallFuture(Action<Gs2.Unity.Gs2Account.Model.EzPlatformId> callback)
        {
            IEnumerator Impl(IFuture<ulong> self)
            {
                var future = ModelFuture();
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                var item = future.Result;
                var callbackId = Subscribe(callback);
                callback.Invoke(item);
                self.OnComplete(callbackId);
            }
            return new Gs2InlineFuture<ulong>(Impl);
        }
        #endif

        #if !UNITY_2017_1_OR_NEWER || GS2_ENABLE_UNITASK
            #if UNITY_2017_1_OR_NEWER
        public async UniTask<ulong> SubscribeWithInitialCallAsync(Action<Gs2.Unity.Gs2Account.Model.EzPlatformId> callback)
            #else
        public async Task<ulong> SubscribeWithInitialCallAsync(Action<Gs2.Unity.Gs2Account.Model.EzPlatformId> callback)
            #endif
        {
            var item = await ModelAsync();
            var callbackId = Subscribe(callback);
            callback.Invoke(item);
            return callbackId;
        }
        #endif

    }
}
