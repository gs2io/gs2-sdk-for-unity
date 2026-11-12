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
#pragma warning disable CS0169, CS0168

using System;
using System.Linq;
using System.Text.RegularExpressions;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Gs2Account.Domain.Iterator;
using Gs2.Gs2Account.Request;
using Gs2.Gs2Account.Result;
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

#if UNITY_2017_1_OR_NEWER
namespace Gs2.Unity.Gs2Account.Domain.Model
{

    public partial class EzAccountDomain {

        /// <summary>
        /// A GameSession that reads the account returned by PreviewTakeOver / PreviewTakeOverOpenIdConnect.
        /// Read the profile or progress to show in the confirmation dialog with it, then call DoTakeOver
        /// once the user agrees. Its access token can only call Describe / Get APIs, expires in
        /// 10 minutes and is never refreshed.
        /// Returns null when no token was issued (the account is banned, or has no data to read yet).
        /// </summary>
        public Gs2.Unity.Util.GameSession PreviewGameSession() {
            if (string.IsNullOrEmpty(this.Token)) {
                return null;
            }
            return new Gs2.Unity.Util.GameSession(
                null,
                this._connection,
                this.UserId,
                null
            ) {
                AccessToken = new AccessToken()
                    .WithToken(this.Token)
                    .WithUserId(this.UserId)
                    .WithExpire(this.Expire),
            };
        }
    }
}
#endif
