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

    public partial class EzUserGameSessionDomain {
        private readonly Gs2.Gs2Friend.Domain.Model.UserAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public string NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserGameSessionDomain(
            Gs2.Gs2Friend.Domain.Model.UserAccessTokenDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        [Obsolete("The name has been changed to SendRequestFuture.")]
        public IFuture<Gs2.Unity.Gs2Friend.Domain.Model.EzFriendRequestGameSessionDomain> SendRequest(
            string targetUserId
        )
        {
            return SendRequestFuture(
                targetUserId
            );
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Friend.Domain.Model.EzFriendRequestGameSessionDomain> SendRequestFuture(
            string targetUserId
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Friend.Domain.Model.EzFriendRequestGameSessionDomain> self)
            {
                yield return SendRequestAsync(
                    targetUserId
                ).ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Friend.Domain.Model.EzFriendRequestGameSessionDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Friend.Domain.Model.EzFriendRequestGameSessionDomain> SendRequestAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Friend.Domain.Model.EzFriendRequestGameSessionDomain> SendRequestFuture(
        #endif
            string targetUserId
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.SendRequestAsync(
                        new SendRequestRequest()
                            .WithTargetUserId(targetUserId)
                            .WithAccessToken(_domain.AccessToken.Token)
                    );
                }
            );
            return new Gs2.Unity.Gs2Friend.Domain.Model.EzFriendRequestGameSessionDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Friend.Domain.Model.EzFriendRequestGameSessionDomain> self)
            {
                var future = _domain.SendRequestFuture(
                    new SendRequestRequest()
                        .WithTargetUserId(targetUserId)
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return _profile.RunFuture(
                    _domain.AccessToken,
                    future,
                    () =>
        			{
                		return future = _domain.SendRequestFuture(
                    		new SendRequestRequest()
                	        .WithTargetUserId(targetUserId)
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
                self.OnComplete(new Gs2.Unity.Gs2Friend.Domain.Model.EzFriendRequestGameSessionDomain(result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Friend.Domain.Model.EzFriendRequestGameSessionDomain>(Impl);
        #endif
        }

        public Gs2.Unity.Gs2Friend.Domain.Model.EzProfileGameSessionDomain Profile(
        ) {
            return new Gs2.Unity.Gs2Friend.Domain.Model.EzProfileGameSessionDomain(
                _domain.Profile(
                ),
                _profile
            );
        }

        public class EzBlackListsIterator : Gs2Iterator<string>
        {
            private Gs2Iterator<string> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly Gs2.Gs2Friend.Domain.Model.UserAccessTokenDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzBlackListsIterator(
                Gs2Iterator<string> it,
        #if !GS2_ENABLE_UNITASK
                Gs2.Gs2Friend.Domain.Model.UserAccessTokenDomain domain,
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

            protected override IEnumerator Next(Action<AsyncResult<string>> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    _domain.AccessToken,
                    _it,
                    () =>
                    {
                        return _it = _domain.BlackLists(
                        );
                    }
                );
        #endif
                callback.Invoke(
                    new AsyncResult<string>(
                        _it.Current,
                        _it.Error
                    )
                );
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<string> BlackLists(
        )
        {
            return new EzBlackListsIterator(
                _domain.BlackLists(
                ),
                _profile
            );
        }

        public IUniTaskAsyncEnumerable<string> BlackListsAsync(
        #else
        public Gs2Iterator<string> BlackLists(
        #endif
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<string>(async (writer, token) =>
            {
                var it = _domain.BlackListsAsync(
                ).GetAsyncEnumerator();
                while(
                    await _profile.RunIteratorAsync(
                        _domain.AccessToken,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.BlackListsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current);
                }
            });
        #else
            return new EzBlackListsIterator(
                _domain.BlackLists(
                ),
                _domain,
                _profile
            );
        #endif
        }

        public Gs2.Unity.Gs2Friend.Domain.Model.EzBlackListGameSessionDomain BlackList(
        ) {
            return new Gs2.Unity.Gs2Friend.Domain.Model.EzBlackListGameSessionDomain(
                _domain.BlackList(
                ),
                _profile
            );
        }

        public class EzFollowsIterator : Gs2Iterator<Gs2.Unity.Gs2Friend.Model.EzFollowUser>
        {
            private Gs2Iterator<Gs2.Gs2Friend.Model.FollowUser> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly bool? _withProfile;
            private readonly Gs2.Gs2Friend.Domain.Model.UserAccessTokenDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzFollowsIterator(
                Gs2Iterator<Gs2.Gs2Friend.Model.FollowUser> it,
        #if !GS2_ENABLE_UNITASK
                bool? withProfile,
                Gs2.Gs2Friend.Domain.Model.UserAccessTokenDomain domain,
        #endif
                Gs2.Unity.Util.Profile profile
            )
            {
                _it = it;
        #if !GS2_ENABLE_UNITASK
                _withProfile = withProfile;
                _domain = domain;
        #endif
                _profile = profile;
            }

            public override bool HasNext()
            {
                return _it.HasNext();
            }

            protected override IEnumerator Next(Action<AsyncResult<Gs2.Unity.Gs2Friend.Model.EzFollowUser>> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    _domain.AccessToken,
                    _it,
                    () =>
                    {
                        return _it = _domain.Follows(
                            _withProfile
                        );
                    }
                );
        #endif
                callback.Invoke(
                    new AsyncResult<Gs2.Unity.Gs2Friend.Model.EzFollowUser>(
                        _it.Current == null ? null : Gs2.Unity.Gs2Friend.Model.EzFollowUser.FromModel(_it.Current),
                        _it.Error
                    )
                );
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Friend.Model.EzFollowUser> Follows(
              bool? withProfile = null
        )
        {
            return new EzFollowsIterator(
                _domain.Follows(
                    withProfile
                ),
                _profile
            );
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Friend.Model.EzFollowUser> FollowsAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Friend.Model.EzFollowUser> Follows(
        #endif
              bool? withProfile = null
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Friend.Model.EzFollowUser>(async (writer, token) =>
            {
                var it = _domain.FollowsAsync(
                    withProfile
                ).GetAsyncEnumerator();
                while(
                    await _profile.RunIteratorAsync(
                        _domain.AccessToken,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.FollowsAsync(
                                withProfile
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Friend.Model.EzFollowUser.FromModel(it.Current));
                }
            });
        #else
            return new EzFollowsIterator(
                _domain.Follows(
                    withProfile
                ),
                withProfile,
                _domain,
                _profile
            );
        #endif
        }

        public ulong SubscribeFollows(
            Action callback,
            bool withProfile
        ) {
            return this._domain.SubscribeFollows(callback, withProfile);
        }

        public void UnsubscribeFollows(
            ulong callbackId,
            bool withProfile
        ) {
            this._domain.UnsubscribeFollows(callbackId, withProfile);
        }

        public Gs2.Unity.Gs2Friend.Domain.Model.EzFollowUserGameSessionDomain FollowUser(
            string targetUserId,
            bool withProfile
        ) {
            return new Gs2.Unity.Gs2Friend.Domain.Model.EzFollowUserGameSessionDomain(
                _domain.FollowUser(
                    targetUserId,
                    withProfile
                ),
                _profile
            );
        }

        public class EzFriendsIterator : Gs2Iterator<Gs2.Unity.Gs2Friend.Model.EzFriendUser>
        {
            private Gs2Iterator<Gs2.Gs2Friend.Model.FriendUser> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly bool? _withProfile;
            private readonly Gs2.Gs2Friend.Domain.Model.UserAccessTokenDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzFriendsIterator(
                Gs2Iterator<Gs2.Gs2Friend.Model.FriendUser> it,
        #if !GS2_ENABLE_UNITASK
                bool? withProfile,
                Gs2.Gs2Friend.Domain.Model.UserAccessTokenDomain domain,
        #endif
                Gs2.Unity.Util.Profile profile
            )
            {
                _it = it;
        #if !GS2_ENABLE_UNITASK
                _withProfile = withProfile;
                _domain = domain;
        #endif
                _profile = profile;
            }

            public override bool HasNext()
            {
                return _it.HasNext();
            }

            protected override IEnumerator Next(Action<AsyncResult<Gs2.Unity.Gs2Friend.Model.EzFriendUser>> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    _domain.AccessToken,
                    _it,
                    () =>
                    {
                        return _it = _domain.Friends(
                            _withProfile
                        );
                    }
                );
        #endif
                callback.Invoke(
                    new AsyncResult<Gs2.Unity.Gs2Friend.Model.EzFriendUser>(
                        _it.Current == null ? null : Gs2.Unity.Gs2Friend.Model.EzFriendUser.FromModel(_it.Current),
                        _it.Error
                    )
                );
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Friend.Model.EzFriendUser> Friends(
              bool? withProfile = null
        )
        {
            return new EzFriendsIterator(
                _domain.Friends(
                    withProfile
                ),
                _profile
            );
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Friend.Model.EzFriendUser> FriendsAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Friend.Model.EzFriendUser> Friends(
        #endif
              bool? withProfile = null
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Friend.Model.EzFriendUser>(async (writer, token) =>
            {
                var it = _domain.FriendsAsync(
                    withProfile
                ).GetAsyncEnumerator();
                while(
                    await _profile.RunIteratorAsync(
                        _domain.AccessToken,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.FriendsAsync(
                                withProfile
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Friend.Model.EzFriendUser.FromModel(it.Current));
                }
            });
        #else
            return new EzFriendsIterator(
                _domain.Friends(
                    withProfile
                ),
                withProfile,
                _domain,
                _profile
            );
        #endif
        }

        public ulong SubscribeFriends(
            Action callback,
            bool withProfile
        ) {
            return this._domain.SubscribeFriends(callback, withProfile);
        }

        public void UnsubscribeFriends(
            ulong callbackId,
            bool withProfile
        ) {
            this._domain.UnsubscribeFriends(callbackId, withProfile);
        }

        public Gs2.Unity.Gs2Friend.Domain.Model.EzFriendGameSessionDomain Friend(
            bool withProfile
        ) {
            return new Gs2.Unity.Gs2Friend.Domain.Model.EzFriendGameSessionDomain(
                _domain.Friend(
                    withProfile
                ),
                _profile
            );
        }

        public class EzSendRequestsIterator : Gs2Iterator<Gs2.Unity.Gs2Friend.Model.EzFriendRequest>
        {
            private Gs2Iterator<Gs2.Gs2Friend.Model.FriendRequest> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly Gs2.Gs2Friend.Domain.Model.UserAccessTokenDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzSendRequestsIterator(
                Gs2Iterator<Gs2.Gs2Friend.Model.FriendRequest> it,
        #if !GS2_ENABLE_UNITASK
                Gs2.Gs2Friend.Domain.Model.UserAccessTokenDomain domain,
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

            protected override IEnumerator Next(Action<AsyncResult<Gs2.Unity.Gs2Friend.Model.EzFriendRequest>> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    _domain.AccessToken,
                    _it,
                    () =>
                    {
                        return _it = _domain.SendRequests(
                        );
                    }
                );
        #endif
                callback.Invoke(
                    new AsyncResult<Gs2.Unity.Gs2Friend.Model.EzFriendRequest>(
                        _it.Current == null ? null : Gs2.Unity.Gs2Friend.Model.EzFriendRequest.FromModel(_it.Current),
                        _it.Error
                    )
                );
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Friend.Model.EzFriendRequest> SendRequests(
        )
        {
            return new EzSendRequestsIterator(
                _domain.SendRequests(
                ),
                _profile
            );
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Friend.Model.EzFriendRequest> SendRequestsAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Friend.Model.EzFriendRequest> SendRequests(
        #endif
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Friend.Model.EzFriendRequest>(async (writer, token) =>
            {
                var it = _domain.SendRequestsAsync(
                ).GetAsyncEnumerator();
                while(
                    await _profile.RunIteratorAsync(
                        _domain.AccessToken,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.SendRequestsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Friend.Model.EzFriendRequest.FromModel(it.Current));
                }
            });
        #else
            return new EzSendRequestsIterator(
                _domain.SendRequests(
                ),
                _domain,
                _profile
            );
        #endif
        }

        public ulong SubscribeSendRequests(Action callback) {
            return this._domain.SubscribeSendRequests(callback);
        }

        public void UnsubscribeSendRequests(ulong callbackId) {
            this._domain.UnsubscribeSendRequests(callbackId);
        }

        public Gs2.Unity.Gs2Friend.Domain.Model.EzSendFriendRequestGameSessionDomain SendFriendRequest(
            string targetUserId
        ) {
            return new Gs2.Unity.Gs2Friend.Domain.Model.EzSendFriendRequestGameSessionDomain(
                _domain.SendFriendRequest(
                    targetUserId
                ),
                _profile
            );
        }

        public class EzReceiveRequestsIterator : Gs2Iterator<Gs2.Unity.Gs2Friend.Model.EzFriendRequest>
        {
            private Gs2Iterator<Gs2.Gs2Friend.Model.FriendRequest> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly Gs2.Gs2Friend.Domain.Model.UserAccessTokenDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzReceiveRequestsIterator(
                Gs2Iterator<Gs2.Gs2Friend.Model.FriendRequest> it,
        #if !GS2_ENABLE_UNITASK
                Gs2.Gs2Friend.Domain.Model.UserAccessTokenDomain domain,
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

            protected override IEnumerator Next(Action<AsyncResult<Gs2.Unity.Gs2Friend.Model.EzFriendRequest>> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    _domain.AccessToken,
                    _it,
                    () =>
                    {
                        return _it = _domain.ReceiveRequests(
                        );
                    }
                );
        #endif
                callback.Invoke(
                    new AsyncResult<Gs2.Unity.Gs2Friend.Model.EzFriendRequest>(
                        _it.Current == null ? null : Gs2.Unity.Gs2Friend.Model.EzFriendRequest.FromModel(_it.Current),
                        _it.Error
                    )
                );
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Friend.Model.EzFriendRequest> ReceiveRequests(
        )
        {
            return new EzReceiveRequestsIterator(
                _domain.ReceiveRequests(
                ),
                _profile
            );
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Friend.Model.EzFriendRequest> ReceiveRequestsAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Friend.Model.EzFriendRequest> ReceiveRequests(
        #endif
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Friend.Model.EzFriendRequest>(async (writer, token) =>
            {
                var it = _domain.ReceiveRequestsAsync(
                ).GetAsyncEnumerator();
                while(
                    await _profile.RunIteratorAsync(
                        _domain.AccessToken,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.ReceiveRequestsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Friend.Model.EzFriendRequest.FromModel(it.Current));
                }
            });
        #else
            return new EzReceiveRequestsIterator(
                _domain.ReceiveRequests(
                ),
                _domain,
                _profile
            );
        #endif
        }

        public ulong SubscribeReceiveRequests(Action callback) {
            return this._domain.SubscribeReceiveRequests(callback);
        }

        public void UnsubscribeReceiveRequests(ulong callbackId) {
            this._domain.UnsubscribeReceiveRequests(callbackId);
        }

        public Gs2.Unity.Gs2Friend.Domain.Model.EzReceiveFriendRequestGameSessionDomain ReceiveFriendRequest(
            string fromUserId
        ) {
            return new Gs2.Unity.Gs2Friend.Domain.Model.EzReceiveFriendRequestGameSessionDomain(
                _domain.ReceiveFriendRequest(
                    fromUserId
                ),
                _profile
            );
        }

    }
}
