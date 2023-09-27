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

    public partial class EzUserDomain {
        private readonly Gs2.Gs2Friend.Domain.Model.UserDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public string NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserDomain(
            Gs2.Gs2Friend.Domain.Model.UserDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        public Gs2.Unity.Gs2Friend.Domain.Model.EzProfileDomain Profile(
        ) {
            return new Gs2.Unity.Gs2Friend.Domain.Model.EzProfileDomain(
                _domain.Profile(
                ),
                _profile
            );
        }

        public Gs2.Unity.Gs2Friend.Domain.Model.EzPublicProfileDomain PublicProfile(
        ) {
            return new Gs2.Unity.Gs2Friend.Domain.Model.EzPublicProfileDomain(
                _domain.PublicProfile(
                ),
                _profile
            );
        }

        public class EzBlackListsIterator : Gs2Iterator<string>
        {
            private Gs2Iterator<string> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly Gs2.Gs2Friend.Domain.Model.UserDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzBlackListsIterator(
                Gs2Iterator<string> it,
        #if !GS2_ENABLE_UNITASK
                Gs2.Gs2Friend.Domain.Model.UserDomain domain,
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
                    null,
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
                        null,
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

        public ulong SubscribeBlackLists(Action callback) {
            return this._domain.SubscribeBlackLists(callback);
        }

        public void UnsubscribeBlackLists(ulong callbackId) {
            this._domain.UnsubscribeBlackLists(callbackId);
        }

        public Gs2.Unity.Gs2Friend.Domain.Model.EzBlackListDomain BlackList(
        ) {
            return new Gs2.Unity.Gs2Friend.Domain.Model.EzBlackListDomain(
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
            private readonly Gs2.Gs2Friend.Domain.Model.UserDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzFollowsIterator(
                Gs2Iterator<Gs2.Gs2Friend.Model.FollowUser> it,
        #if !GS2_ENABLE_UNITASK
                bool? withProfile,
                Gs2.Gs2Friend.Domain.Model.UserDomain domain,
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
                    null,
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
                        null,
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
            bool? withProfile
        ) {
            return this._domain.SubscribeFollows(callback, withProfile);
        }

        public void UnsubscribeFollows(
            ulong callbackId,
            bool? withProfile
        ) {
            this._domain.UnsubscribeFollows(callbackId, withProfile);
        }

        public Gs2.Unity.Gs2Friend.Domain.Model.EzFollowUserDomain FollowUser(
            string targetUserId,
            bool withProfile
        ) {
            return new Gs2.Unity.Gs2Friend.Domain.Model.EzFollowUserDomain(
                _domain.FollowUser(
                    targetUserId,
                    withProfile
                ),
                _profile
            );
        }

    }
}
