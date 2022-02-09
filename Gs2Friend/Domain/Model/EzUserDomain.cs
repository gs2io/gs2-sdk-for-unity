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

    public partial class EzUserDomain {
        private readonly Gs2.Gs2Friend.Domain.Model.UserDomain _domain;
        public string NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserDomain(
            Gs2.Gs2Friend.Domain.Model.UserDomain domain
        ) {
            this._domain = domain;
        }

        public Gs2.Unity.Gs2Friend.Domain.Model.EzProfileDomain Profile(
        ) {
            return new Gs2.Unity.Gs2Friend.Domain.Model.EzProfileDomain(
                _domain.Profile(
                )
            );
        }

        public Gs2.Unity.Gs2Friend.Domain.Model.EzPublicProfileDomain PublicProfile(
        ) {
            return new Gs2.Unity.Gs2Friend.Domain.Model.EzPublicProfileDomain(
                _domain.PublicProfile(
                )
            );
        }

        public class EzBlackListsIterator : Gs2Iterator<string>
        {
            private readonly Gs2Iterator<string> _it;

            public EzBlackListsIterator(
                Gs2Iterator<string> it
            )
            {
                _it = it;
            }

            public override bool HasNext()
            {
                return _it.HasNext();
            }

            protected override IEnumerator Next(Action<string> callback)
            {
                yield return _it.Next();
                callback.Invoke(_it.Current);
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<string> BlackLists(
        )
        {
            return new EzBlackListsIterator(_domain.BlackLists(
            ));
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
                while(await it.MoveNextAsync())
                {
                    await writer.YieldAsync(it.Current);
                }
            });
        #else
            return new EzBlackListsIterator(_domain.BlackLists(
            ));
        #endif
        }

        public Gs2.Unity.Gs2Friend.Domain.Model.EzBlackListDomain BlackList(
        ) {
            return new Gs2.Unity.Gs2Friend.Domain.Model.EzBlackListDomain(
                _domain.BlackList(
                )
            );
        }

        public class EzFollowsIterator : Gs2Iterator<Gs2.Unity.Gs2Friend.Model.EzFollowUser>
        {
            private readonly Gs2Iterator<Gs2.Gs2Friend.Model.FollowUser> _it;

            public EzFollowsIterator(
                Gs2Iterator<Gs2.Gs2Friend.Model.FollowUser> it
            )
            {
                _it = it;
            }

            public override bool HasNext()
            {
                return _it.HasNext();
            }

            protected override IEnumerator Next(Action<Gs2.Unity.Gs2Friend.Model.EzFollowUser> callback)
            {
                yield return _it.Next();
                callback.Invoke(_it.Current == null ? null : Gs2.Unity.Gs2Friend.Model.EzFollowUser.FromModel(_it.Current));
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Friend.Model.EzFollowUser> Follows(
              bool? withProfile = null
        )
        {
            return new EzFollowsIterator(_domain.Follows(
               withProfile
            ));
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
                while(await it.MoveNextAsync())
                {
                    await writer.YieldAsync(Gs2.Unity.Gs2Friend.Model.EzFollowUser.FromModel(it.Current));
                }
            });
        #else
            return new EzFollowsIterator(_domain.Follows(
               withProfile
            ));
        #endif
        }

        public Gs2.Unity.Gs2Friend.Domain.Model.EzFollowUserDomain FollowUser(
            string targetUserId
        ) {
            return new Gs2.Unity.Gs2Friend.Domain.Model.EzFollowUserDomain(
                _domain.FollowUser(
                    targetUserId
                )
            );
        }

    }
}
