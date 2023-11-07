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
        private readonly Gs2.Unity.Util.Profile _profile;
        public string NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserGameSessionDomain(
            Gs2.Gs2Chat.Domain.Model.UserAccessTokenDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        [Obsolete("The name has been changed to CreateRoomFuture.")]
        public IFuture<Gs2.Unity.Gs2Chat.Domain.Model.EzRoomGameSessionDomain> CreateRoom(
            string name = null,
            string metadata = null,
            string password = null,
            string[] whiteListUserIds = null
        )
        {
            return CreateRoomFuture(
                name,
                metadata,
                password,
                whiteListUserIds
            );
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Chat.Domain.Model.EzRoomGameSessionDomain> CreateRoomFuture(
            string name = null,
            string metadata = null,
            string password = null,
            string[] whiteListUserIds = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Chat.Domain.Model.EzRoomGameSessionDomain> self)
            {
                var future = this._domain.CreateRoomFuture(
                    new CreateRoomRequest()
                        .WithName(name)
                        .WithMetadata(metadata)
                        .WithPassword(password)
                        .WithWhiteListUserIds(whiteListUserIds)
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Chat.Domain.Model.EzRoomGameSessionDomain(future.Result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Chat.Domain.Model.EzRoomGameSessionDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Chat.Domain.Model.EzRoomGameSessionDomain> CreateRoomAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Chat.Domain.Model.EzRoomGameSessionDomain> CreateRoomFuture(
        #endif
            string name = null,
            string metadata = null,
            string password = null,
            string[] whiteListUserIds = null
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.CreateRoomAsync(
                        new CreateRoomRequest()
                            .WithName(name)
                            .WithMetadata(metadata)
                            .WithPassword(password)
                            .WithWhiteListUserIds(whiteListUserIds)
                            .WithAccessToken(_domain.AccessToken.Token)
                    );
                }
            );
            return new Gs2.Unity.Gs2Chat.Domain.Model.EzRoomGameSessionDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Chat.Domain.Model.EzRoomGameSessionDomain> self)
            {
                var future = _domain.CreateRoomFuture(
                    new CreateRoomRequest()
                        .WithName(name)
                        .WithMetadata(metadata)
                        .WithPassword(password)
                        .WithWhiteListUserIds(whiteListUserIds)
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return _profile.RunFuture(
                    _domain.AccessToken,
                    future,
                    () =>
        			{
                		return future = _domain.CreateRoomFuture(
                    		new CreateRoomRequest()
                	        .WithName(name)
                	        .WithMetadata(metadata)
                	        .WithPassword(password)
                	        .WithWhiteListUserIds(whiteListUserIds)
                    	    .WithAccessToken(_domain.AccessToken.Token)
        		        );
        			}
                );
                if (future.Error != null)
                {
                    self.OnError(future.Error);
                    yield break;
                }
                var result = future.Result;
                self.OnComplete(new Gs2.Unity.Gs2Chat.Domain.Model.EzRoomGameSessionDomain(result, _profile));
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
                ),
                _profile
            );
        }

        public class EzSubscribesIterator : Gs2Iterator<Gs2.Unity.Gs2Chat.Model.EzSubscribe>
        {
            private Gs2Iterator<Gs2.Gs2Chat.Model.Subscribe> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly Gs2.Gs2Chat.Domain.Model.UserAccessTokenDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzSubscribesIterator(
                Gs2Iterator<Gs2.Gs2Chat.Model.Subscribe> it,
        #if !GS2_ENABLE_UNITASK
                Gs2.Gs2Chat.Domain.Model.UserAccessTokenDomain domain,
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

            protected override IEnumerator Next(Action<AsyncResult<Gs2.Unity.Gs2Chat.Model.EzSubscribe>> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    _domain.AccessToken,
                    _it,
                    () =>
                    {
                        return _it = _domain.Subscribes(
                        );
                    }
                );
        #endif
                callback.Invoke(
                    new AsyncResult<Gs2.Unity.Gs2Chat.Model.EzSubscribe>(
                        _it.Current == null ? null : Gs2.Unity.Gs2Chat.Model.EzSubscribe.FromModel(_it.Current),
                        _it.Error
                    )
                );
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
                        _domain.AccessToken,
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

        public ulong SubscribeSubscribes(Action callback) {
            return this._domain.SubscribeSubscribes(callback);
        }

        public void UnsubscribeSubscribes(ulong callbackId) {
            this._domain.UnsubscribeSubscribes(callbackId);
        }

        public Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain Subscribe(
            string roomName
        ) {
            return new Gs2.Unity.Gs2Chat.Domain.Model.EzSubscribeGameSessionDomain(
                _domain.Subscribe(
                    roomName
                ),
                _profile
            );
        }

    }
}
