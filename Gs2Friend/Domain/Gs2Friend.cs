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
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Core.Util;
using Gs2.Gs2Friend.Domain.Iterator;
using Gs2.Gs2Friend.Model;
using Gs2.Gs2Friend.Domain.Model;
using Gs2.Gs2Friend.Request;
using Gs2.Gs2Friend.Result;
using Gs2.Gs2Auth.Model;
using Gs2.Util.LitJson;
using Gs2.Core;
using Gs2.Core.Domain;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.Scripting;
#if GS2_ENABLE_UNITASK
using Cysharp.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
#endif

namespace Gs2.Unity.Gs2Friend.Domain
{

    public partial class Gs2Friend {
        private readonly Gs2.Gs2Friend.Domain.Gs2Friend _domain;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;

        public Gs2Friend(
            Gs2.Gs2Friend.Domain.Gs2Friend domain,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._connection = connection;
        }

        public Gs2.Unity.Gs2Friend.Domain.Model.EzNamespaceDomain Namespace(
            string namespaceName
        ) {
            return new Gs2.Unity.Gs2Friend.Domain.Model.EzNamespaceDomain(
                _domain.Namespace(
                    namespaceName
                ),
                this._connection
            );
        }

        public event UnityAction<FollowNotification> OnFollowNotification
        {
            add => _domain.OnFollowNotification += value;
            remove => _domain.OnFollowNotification -= value;
        }

        public event UnityAction<AcceptRequestNotification> OnAcceptRequestNotification
        {
            add => _domain.OnAcceptRequestNotification += value;
            remove => _domain.OnAcceptRequestNotification -= value;
        }

        public event UnityAction<RejectRequestNotification> OnRejectRequestNotification
        {
            add => _domain.OnRejectRequestNotification += value;
            remove => _domain.OnRejectRequestNotification -= value;
        }

        public event UnityAction<DeleteFriendNotification> OnDeleteFriendNotification
        {
            add => _domain.OnDeleteFriendNotification += value;
            remove => _domain.OnDeleteFriendNotification -= value;
        }

        public event UnityAction<ReceiveRequestNotification> OnReceiveRequestNotification
        {
            add => _domain.OnReceiveRequestNotification += value;
            remove => _domain.OnReceiveRequestNotification -= value;
        }

        public event UnityAction<CancelRequestNotification> OnCancelRequestNotification
        {
            add => _domain.OnCancelRequestNotification += value;
            remove => _domain.OnCancelRequestNotification -= value;
        }
    }
}
