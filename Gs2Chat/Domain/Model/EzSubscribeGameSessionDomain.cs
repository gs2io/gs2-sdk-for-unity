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
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;
        public string RoomName => _domain?.RoomName;

        public EzSubscribeGameSessionDomain(
            Gs2.Gs2Chat.Domain.Model.SubscribeAccessTokenDomain domain
        ) {
            this._domain = domain;
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain> SubscribeAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain> Subscribe(
        #endif
              Gs2.Unity.Gs2Chat.Model.EzNotificationType[] notificationTypes = null
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _domain.SubscribeAsync(
                new SubscribeRequest()
                    .WithNotificationTypes(notificationTypes?.Select(v => v.ToModel()).ToArray())
            );
            return new Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain(result);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain> self)
            {
                var future = _domain.Subscribe(
                    new SubscribeRequest()
                        .WithNotificationTypes(notificationTypes?.Select(v => v.ToModel()).ToArray())
                );
                yield return future;
                if (future.Error != null)
                {
                    self.OnError(future.Error);
                    yield break;
                }
                var result = future.Result;
                self.OnComplete(new Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain(result));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain>(Impl);
        #endif
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain> UpdateSubscribeSettingAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain> UpdateSubscribeSetting(
        #endif
              Gs2.Unity.Gs2Chat.Model.EzNotificationType[] notificationTypes = null
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _domain.UpdateNotificationTypeAsync(
                new UpdateNotificationTypeRequest()
                    .WithNotificationTypes(notificationTypes?.Select(v => v.ToModel()).ToArray())
            );
            return new Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain(result);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain> self)
            {
                var future = _domain.UpdateNotificationType(
                    new UpdateNotificationTypeRequest()
                        .WithNotificationTypes(notificationTypes?.Select(v => v.ToModel()).ToArray())
                );
                yield return future;
                if (future.Error != null)
                {
                    self.OnError(future.Error);
                    yield break;
                }
                var result = future.Result;
                self.OnComplete(new Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain(result));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain>(Impl);
        #endif
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain> UnsubscribeAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain> Unsubscribe(
        #endif
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _domain.UnsubscribeAsync(
                new UnsubscribeRequest()
            );
            return new Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain(result);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain> self)
            {
                var future = _domain.Unsubscribe(
                    new UnsubscribeRequest()
                );
                yield return future;
                if (future.Error != null)
                {
                    self.OnError(future.Error);
                    yield break;
                }
                var result = future.Result;
                self.OnComplete(new Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain(result));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain>(Impl);
        #endif
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Chat.Model.EzSubscribe> Model()
        {
            var item = await _domain.Model();
            if (item == null) {
                return null;
            }
            return Gs2.Unity.Gs2Chat.Model.EzSubscribe.FromModel(
                item
            );
        }
        #else
        public IFuture<Gs2.Unity.Gs2Chat.Model.EzSubscribe> Model()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Chat.Model.EzSubscribe> self)
            {
                var future = _domain.Model();
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
        #endif

    }
}
