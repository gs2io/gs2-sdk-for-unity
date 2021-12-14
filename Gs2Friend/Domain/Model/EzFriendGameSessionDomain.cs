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

    public partial class EzFriendGameSessionDomain {
        private readonly Gs2.Gs2Friend.Domain.Model.FriendAccessTokenDomain _domain;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;
        public bool? WithProfile => _domain?.WithProfile;

        public EzFriendGameSessionDomain(
            Gs2.Gs2Friend.Domain.Model.FriendAccessTokenDomain domain
        ) {
            this._domain = domain;
        }

        public Gs2.Unity.Gs2Friend.Domain.Model.EzFriendUserGameSessionDomain FriendUser(
            string targetUserId
        ) {
            return new Gs2.Unity.Gs2Friend.Domain.Model.EzFriendUserGameSessionDomain(
                _domain.FriendUser(
                    targetUserId
                )
            );
        }

    }
}