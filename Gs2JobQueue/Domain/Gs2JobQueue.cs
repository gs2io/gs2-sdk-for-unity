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
using Gs2.Gs2JobQueue.Domain.Iterator;
using Gs2.Gs2JobQueue.Model;
using Gs2.Gs2JobQueue.Domain.Model;
using Gs2.Gs2JobQueue.Request;
using Gs2.Gs2JobQueue.Result;
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

namespace Gs2.Unity.Gs2JobQueue.Domain
{

    public class Gs2JobQueue {
        private readonly Gs2.Gs2JobQueue.Domain.Gs2JobQueue _domain;
        private readonly Gs2.Unity.Util.Profile _profile;

        public Gs2JobQueue(
            Gs2.Gs2JobQueue.Domain.Gs2JobQueue domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        public Gs2.Unity.Gs2JobQueue.Domain.Model.EzNamespaceDomain Namespace(
            string namespaceName
        ) {
            return new Gs2.Unity.Gs2JobQueue.Domain.Model.EzNamespaceDomain(
                _domain.Namespace(
                    namespaceName
                ),
                _profile
            );
        }

        public event UnityAction<PushNotification> OnPushNotification
        {
            add => _domain.OnPushNotification += value;
            remove => _domain.OnPushNotification -= value;
        }

        public event UnityAction<RunNotification> OnRunNotification
        {
            add => _domain.OnRunNotification += value;
            remove => _domain.OnRunNotification -= value;
        }
    }
}
