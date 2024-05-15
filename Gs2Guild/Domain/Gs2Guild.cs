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
using Gs2.Gs2Guild.Domain.Iterator;
using Gs2.Gs2Guild.Model;
using Gs2.Gs2Guild.Domain.Model;
using Gs2.Gs2Guild.Request;
using Gs2.Gs2Guild.Result;
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

namespace Gs2.Unity.Gs2Guild.Domain
{

    public partial class Gs2Guild {
        private readonly Gs2.Gs2Guild.Domain.Gs2Guild _domain;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;

        public Gs2Guild(
            Gs2.Gs2Guild.Domain.Gs2Guild domain,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._connection = connection;
        }

        public Gs2.Unity.Gs2Guild.Domain.Model.EzNamespaceDomain Namespace(
            string namespaceName
        ) {
            return new Gs2.Unity.Gs2Guild.Domain.Model.EzNamespaceDomain(
                _domain.Namespace(
                    namespaceName
                ),
                this._connection
            );
        }

        public event UnityAction<ReceiveRequestNotification> OnReceiveRequestNotification
        {
            add => _domain.OnReceiveRequestNotification += value;
            remove => _domain.OnReceiveRequestNotification -= value;
        }

        public event UnityAction<RemoveRequestNotification> OnRemoveRequestNotification
        {
            add => _domain.OnRemoveRequestNotification += value;
            remove => _domain.OnRemoveRequestNotification -= value;
        }

        public event UnityAction<JoinNotification> OnJoinNotification
        {
            add => _domain.OnJoinNotification += value;
            remove => _domain.OnJoinNotification -= value;
        }

        public event UnityAction<LeaveNotification> OnLeaveNotification
        {
            add => _domain.OnLeaveNotification += value;
            remove => _domain.OnLeaveNotification -= value;
        }

        public event UnityAction<ChangeMemberNotification> OnChangeMemberNotification
        {
            add => _domain.OnChangeMemberNotification += value;
            remove => _domain.OnChangeMemberNotification -= value;
        }
    }
}
