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

    public partial class EzUserDomain {
        private readonly Gs2.Gs2Chat.Domain.Model.UserDomain _domain;
        public string NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserDomain(
            Gs2.Gs2Chat.Domain.Model.UserDomain domain
        ) {
            this._domain = domain;
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Chat.Model.EzRoom> Rooms(
        #else
        public class EzRoomsIterator : Gs2Iterator<Gs2.Unity.Gs2Chat.Model.EzRoom>
        {
            private readonly Gs2Iterator<Gs2.Gs2Chat.Model.Room> _it;

            public EzRoomsIterator(
                Gs2Iterator<Gs2.Gs2Chat.Model.Room> it
            )
            {
                _it = it;
            }

            public override bool HasNext()
            {
                return _it.HasNext();
            }

            protected override IEnumerator Next(Action<Gs2.Unity.Gs2Chat.Model.EzRoom> callback)
            {
                yield return _it.Next();
                callback.Invoke(Gs2.Unity.Gs2Chat.Model.EzRoom.FromModel(_it.Current));
            }
        }

        public Gs2Iterator<Gs2.Unity.Gs2Chat.Model.EzRoom> Rooms(
        #endif
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Chat.Model.EzRoom>(async (writer, token) =>
            {
                var it = _domain.Rooms(
                ).GetAsyncEnumerator();
                while(await it.MoveNextAsync())
                {
                    await writer.YieldAsync(Gs2.Unity.Gs2Chat.Model.EzRoom.FromModel(it.Current));
                }
            });
        #else
            return new EzRoomsIterator(_domain.Rooms(
            ));
        #endif
        }

        public Gs2.Unity.Gs2Chat.Domain.Model.EzRoomDomain Room(
            string roomName,
            string password
        ) {
            return new Gs2.Unity.Gs2Chat.Domain.Model.EzRoomDomain(
                _domain.Room(
                    roomName,
                    password
                )
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Chat.Model.EzSubscribe> Subscribes(
        #else
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
                callback.Invoke(Gs2.Unity.Gs2Chat.Model.EzSubscribe.FromModel(_it.Current));
            }
        }

        public Gs2Iterator<Gs2.Unity.Gs2Chat.Model.EzSubscribe> Subscribes(
        #endif
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Chat.Model.EzSubscribe>(async (writer, token) =>
            {
                var it = _domain.Subscribes(
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

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Chat.Model.EzSubscribe> SubscribesByRoomName(
        #else
        public class EzSubscribesByRoomNameIterator : Gs2Iterator<Gs2.Unity.Gs2Chat.Model.EzSubscribe>
        {
            private readonly Gs2Iterator<Gs2.Gs2Chat.Model.Subscribe> _it;

            public EzSubscribesByRoomNameIterator(
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
                callback.Invoke(Gs2.Unity.Gs2Chat.Model.EzSubscribe.FromModel(_it.Current));
            }
        }

        public Gs2Iterator<Gs2.Unity.Gs2Chat.Model.EzSubscribe> SubscribesByRoomName(
        #endif
              string roomName
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Chat.Model.EzSubscribe>(async (writer, token) =>
            {
                var it = _domain.SubscribesByRoomName(
                    roomName
                ).GetAsyncEnumerator();
                while(await it.MoveNextAsync())
                {
                    await writer.YieldAsync(Gs2.Unity.Gs2Chat.Model.EzSubscribe.FromModel(it.Current));
                }
            });
        #else
            return new EzSubscribesByRoomNameIterator(_domain.SubscribesByRoomName(
               roomName
            ));
        #endif
        }

        public Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeDomain Subscribe(
            string roomName
        ) {
            return new Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeDomain(
                _domain.Subscribe(
                    roomName
                )
            );
        }

    }
}
