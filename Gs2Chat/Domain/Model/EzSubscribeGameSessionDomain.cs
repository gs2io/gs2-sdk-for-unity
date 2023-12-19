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
using Gs2.Gs2Chat.Domain.Iterator;
using Gs2.Gs2Chat.Request;
using Gs2.Gs2Chat.Result;
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

namespace Gs2.Unity.Gs2Chat.Domain.Model
{

    public partial class EzSubscribeGameSessionDomain {
        private readonly Gs2.Gs2Chat.Domain.Model.SubscribeAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.GameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;
        public string RoomName => _domain?.RoomName;

        public EzSubscribeGameSessionDomain(
            Gs2.Gs2Chat.Domain.Model.SubscribeAccessTokenDomain domain,
            Gs2.Unity.Util.GameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }

        [Obsolete("The name has been changed to SubscribeFuture.")]
        public IFuture<Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain> Subscribe(
            Gs2.Unity.Gs2Chat.Model.EzNotificationType[] notificationTypes = null
        )
        {
            return SubscribeFuture(
                notificationTypes
            );
        }

        public IFuture<Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain> SubscribeFuture(
            Gs2.Unity.Gs2Chat.Model.EzNotificationType[] notificationTypes = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.SubscribeFuture(
                        new SubscribeRequest()
                            .WithNotificationTypes(notificationTypes?.Select(v => v.ToModel()).ToArray())
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain> SubscribeAsync(
            Gs2.Unity.Gs2Chat.Model.EzNotificationType[] notificationTypes = null
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.SubscribeAsync(
                    new SubscribeRequest()
                        .WithNotificationTypes(notificationTypes?.Select(v => v.ToModel()).ToArray())
                )
            );
            return new Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        [Obsolete("The name has been changed to UpdateSubscribeSettingFuture.")]
        public IFuture<Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain> UpdateSubscribeSetting(
            Gs2.Unity.Gs2Chat.Model.EzNotificationType[] notificationTypes = null
        )
        {
            return UpdateSubscribeSettingFuture(
                notificationTypes
            );
        }

        public IFuture<Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain> UpdateSubscribeSettingFuture(
            Gs2.Unity.Gs2Chat.Model.EzNotificationType[] notificationTypes = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.UpdateNotificationTypeFuture(
                        new UpdateNotificationTypeRequest()
                            .WithNotificationTypes(notificationTypes?.Select(v => v.ToModel()).ToArray())
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain> UpdateSubscribeSettingAsync(
            Gs2.Unity.Gs2Chat.Model.EzNotificationType[] notificationTypes = null
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.UpdateNotificationTypeAsync(
                    new UpdateNotificationTypeRequest()
                        .WithNotificationTypes(notificationTypes?.Select(v => v.ToModel()).ToArray())
                )
            );
            return new Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        [Obsolete("The name has been changed to UnsubscribeFuture.")]
        public IFuture<Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain> Unsubscribe(
        )
        {
            return UnsubscribeFuture(
            );
        }

        public IFuture<Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain> UnsubscribeFuture(
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.UnsubscribeFuture(
                        new UnsubscribeRequest()
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain> UnsubscribeAsync(
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.UnsubscribeAsync(
                    new UnsubscribeRequest()
                )
            );
            return new Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        [Obsolete("The name has been changed to ModelFuture.")]
        public IFuture<Gs2.Unity.Gs2Chat.Model.EzSubscribe> Model()
        {
            return ModelFuture();
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Chat.Model.EzSubscribe> ModelAsync()
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
            return Gs2.Unity.Gs2Chat.Model.EzSubscribe.FromModel(
                item
            );
        }
        #endif

        public IFuture<Gs2.Unity.Gs2Chat.Model.EzSubscribe> ModelFuture()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Chat.Model.EzSubscribe> self)
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
                self.OnComplete(Gs2.Unity.Gs2Chat.Model.EzSubscribe.FromModel(
                    item
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Chat.Model.EzSubscribe>(Impl);
        }

        public void Invalidate()
        {
            this._domain.Invalidate();
        }

        public ulong Subscribe(Action<Gs2.Unity.Gs2Chat.Model.EzSubscribe> callback)
        {
            return this._domain.Subscribe(item => {
                callback.Invoke(Gs2.Unity.Gs2Chat.Model.EzSubscribe.FromModel(
                    item
                ));
            });
        }

        public void Unsubscribe(ulong callbackId)
        {
            this._domain.Unsubscribe(callbackId);
        }

        #if UNITY_2017_1_OR_NEWER
        public Gs2Future<ulong> SubscribeWithInitialCallFuture(Action<Gs2.Unity.Gs2Chat.Model.EzSubscribe> callback)
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
        public async UniTask<ulong> SubscribeWithInitialCallAsync(Action<Gs2.Unity.Gs2Chat.Model.EzSubscribe> callback)
            #else
        public async Task<ulong> SubscribeWithInitialCallAsync(Action<Gs2.Unity.Gs2Chat.Model.EzSubscribe> callback)
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
