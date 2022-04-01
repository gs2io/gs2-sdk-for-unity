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

    public partial class EzUserGameSessionDomain {
        private readonly Gs2.Gs2Chat.Domain.Model.UserAccessTokenDomain _domain;
        public string NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserGameSessionDomain(
            Gs2.Gs2Chat.Domain.Model.UserAccessTokenDomain domain
        ) {
            this._domain = domain;
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Chat.Domain.Model.EzRoomGameSessionDomain> CreateRoom(
              string name = null,
              string metadata = null,
              string password = null,
              string[] whiteListUserIds = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Chat.Domain.Model.EzRoomGameSessionDomain> self)
            {
                yield return CreateRoomAsync(
                    name,
                    metadata,
                    password,
                    whiteListUserIds
                ).ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Chat.Domain.Model.EzRoomGameSessionDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Chat.Domain.Model.EzRoomGameSessionDomain> CreateRoomAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Chat.Domain.Model.EzRoomGameSessionDomain> CreateRoom(
        #endif
              string name = null,
              string metadata = null,
              string password = null,
              string[] whiteListUserIds = null
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _domain.CreateRoomAsync(
                new CreateRoomRequest()
                    .WithName(name)
                    .WithMetadata(metadata)
                    .WithPassword(password)
                    .WithWhiteListUserIds(whiteListUserIds)
            );
            return new Gs2.Unity.Gs2Chat.Domain.Model.EzRoomGameSessionDomain(result);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Chat.Domain.Model.EzRoomGameSessionDomain> self)
            {
                var future = _domain.CreateRoom(
                    new CreateRoomRequest()
                        .WithName(name)
                        .WithMetadata(metadata)
                        .WithPassword(password)
                        .WithWhiteListUserIds(whiteListUserIds)
                );
                yield return future;
                if (future.Error != null)
                {
                    self.OnError(future.Error);
                    yield break;
                }
                var result = future.Result;
                self.OnComplete(new Gs2.Unity.Gs2Chat.Domain.Model.EzRoomGameSessionDomain(result));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Chat.Domain.Model.EzRoomGameSessionDomain>(Impl);
        #endif
        }

        public Gs2.Unity.Gs2Chat.Domain.Model.EzRoomGameSessionDomain Room(
            string roomName,
            string password
        ) {
            return new Gs2.Unity.Gs2Chat.Domain.Model.EzRoomGameSessionDomain(
                _domain.Room(
                    roomName,
                    password
                )
            );
        }

        public class EzSubscribesIterator : Gs2Iterator<Gs2.Unity.Gs2Chat.Model.EzSubscribe>
        {
            private readonly Gs2Iterator<Gs2.Gs2Chat.Model.Subscribe> _it;

            public EzSubscribesIterator(
                Gs2Iterator<Gs2.Gs2Chat.Model.Subscribe> it
            )
            {
                _it = it;
            }

            public override bool HasNext()
            {
                return _it.HasNext();
            }

            protected override IEnumerator Next(Action<Gs2.Unity.Gs2Chat.Model.EzSubscribe> callback)
            {
                yield return _it.Next();
                callback.Invoke(_it.Current == null ? null : Gs2.Unity.Gs2Chat.Model.EzSubscribe.FromModel(_it.Current));
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Chat.Model.EzSubscribe> Subscribes(
        )
        {
            return new EzSubscribesIterator(_domain.Subscribes(
            ));
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Chat.Model.EzSubscribe> SubscribesAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Chat.Model.EzSubscribe> Subscribes(
        #endif
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Chat.Model.EzSubscribe>(async (writer, token) =>
            {
                var it = _domain.SubscribesAsync(
                ).GetAsyncEnumerator();
                while(await it.MoveNextAsync())
                {
                    await writer.YieldAsync(Gs2.Unity.Gs2Chat.Model.EzSubscribe.FromModel(it.Current));
                }
            });
        #else
            return new EzSubscribesIterator(_domain.Subscribes(
            ));
        #endif
        }

        public Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain Subscribe(
            string roomName
        ) {
            return new Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain(
                _domain.Subscribe(
                    roomName
                )
            );
        }

    }
}
