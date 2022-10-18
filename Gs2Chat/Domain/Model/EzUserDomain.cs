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
        private readonly Gs2.Unity.Util.Profile _profile;
        public string NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserDomain(
            Gs2.Gs2Chat.Domain.Model.UserDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        public class EzRoomsIterator : Gs2Iterator<Gs2.Unity.Gs2Chat.Model.EzRoom>
        {
            private Gs2Iterator<Gs2.Gs2Chat.Model.Room> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly Gs2.Gs2Chat.Domain.Model.UserDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzRoomsIterator(
                Gs2Iterator<Gs2.Gs2Chat.Model.Room> it,
        #if !GS2_ENABLE_UNITASK
                Gs2.Gs2Chat.Domain.Model.UserDomain domain,
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

            protected override IEnumerator Next(Action<Gs2.Unity.Gs2Chat.Model.EzRoom> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    null,
                    _it,
                    () =>
                    {
                        _it = _domain.Rooms(
                        );
                    }
                );
        #endif
                callback.Invoke(_it.Current == null ? null : Gs2.Unity.Gs2Chat.Model.EzRoom.FromModel(_it.Current));
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Chat.Model.EzRoom> Rooms(
        )
        {
            return new EzRoomsIterator(
                _domain.Rooms(
                ),
                _profile
            );
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Chat.Model.EzRoom> RoomsAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Chat.Model.EzRoom> Rooms(
        #endif
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Chat.Model.EzRoom>(async (writer, token) =>
            {
                var it = _domain.RoomsAsync(
                ).GetAsyncEnumerator();
                while(
                    await _profile.RunIteratorAsync(
                        null,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.RoomsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Chat.Model.EzRoom.FromModel(it.Current));
                }
            });
        #else
            return new EzRoomsIterator(
                _domain.Rooms(
                ),
                _domain,
                _profile
            );
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
                ),
                _profile
            );
        }

        public class EzSubscribesIterator : Gs2Iterator<Gs2.Unity.Gs2Chat.Model.EzSubscribe>
        {
            private Gs2Iterator<Gs2.Gs2Chat.Model.Subscribe> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly Gs2.Gs2Chat.Domain.Model.UserDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzSubscribesIterator(
                Gs2Iterator<Gs2.Gs2Chat.Model.Subscribe> it,
        #if !GS2_ENABLE_UNITASK
                Gs2.Gs2Chat.Domain.Model.UserDomain domain,
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

            protected override IEnumerator Next(Action<Gs2.Unity.Gs2Chat.Model.EzSubscribe> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    null,
                    _it,
                    () =>
                    {
                        _it = _domain.Subscribes(
                        );
                    }
                );
        #endif
                callback.Invoke(_it.Current == null ? null : Gs2.Unity.Gs2Chat.Model.EzSubscribe.FromModel(_it.Current));
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Chat.Model.EzSubscribe> Subscribes(
        )
        {
            return new EzSubscribesIterator(
                _domain.Subscribes(
                ),
                _profile
            );
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
                while(
                    await _profile.RunIteratorAsync(
                        null,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.SubscribesAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Chat.Model.EzSubscribe.FromModel(it.Current));
                }
            });
        #else
            return new EzSubscribesIterator(
                _domain.Subscribes(
                ),
                _domain,
                _profile
            );
        #endif
        }

        public class EzSubscribesByRoomNameIterator : Gs2Iterator<Gs2.Unity.Gs2Chat.Model.EzSubscribe>
        {
            private Gs2Iterator<Gs2.Gs2Chat.Model.Subscribe> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly string _roomName;
            private readonly Gs2.Gs2Chat.Domain.Model.UserDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzSubscribesByRoomNameIterator(
                Gs2Iterator<Gs2.Gs2Chat.Model.Subscribe> it,
        #if !GS2_ENABLE_UNITASK
                string roomName,
                Gs2.Gs2Chat.Domain.Model.UserDomain domain,
        #endif
                Gs2.Unity.Util.Profile profile
            )
            {
                _it = it;
        #if !GS2_ENABLE_UNITASK
                _roomName = roomName;
                _domain = domain;
        #endif
                _profile = profile;
            }

            public override bool HasNext()
            {
                return _it.HasNext();
            }

            protected override IEnumerator Next(Action<Gs2.Unity.Gs2Chat.Model.EzSubscribe> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    null,
                    _it,
                    () =>
                    {
                        _it = _domain.SubscribesByRoomName(
                            _roomName
                        );
                    }
                );
        #endif
                callback.Invoke(_it.Current == null ? null : Gs2.Unity.Gs2Chat.Model.EzSubscribe.FromModel(_it.Current));
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Chat.Model.EzSubscribe> SubscribesByRoomName(
              string roomName
        )
        {
            return new EzSubscribesByRoomNameIterator(
                _domain.SubscribesByRoomName(
                    roomName
                ),
                _profile
            );
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Chat.Model.EzSubscribe> SubscribesByRoomNameAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Chat.Model.EzSubscribe> SubscribesByRoomName(
        #endif
              string roomName
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Chat.Model.EzSubscribe>(async (writer, token) =>
            {
                var it = _domain.SubscribesByRoomNameAsync(
                    roomName
                ).GetAsyncEnumerator();
                while(
                    await _profile.RunIteratorAsync(
                        null,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.SubscribesByRoomNameAsync(
                                roomName
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Chat.Model.EzSubscribe.FromModel(it.Current));
                }
            });
        #else
            return new EzSubscribesByRoomNameIterator(
                _domain.SubscribesByRoomName(
                    roomName
                ),
                roomName,
                _domain,
                _profile
            );
        #endif
        }

        public Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeDomain Subscribe(
            string roomName
        ) {
            return new Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeDomain(
                _domain.Subscribe(
                    roomName
                ),
                _profile
            );
        }

    }
}
