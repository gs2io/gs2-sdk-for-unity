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
using Gs2.Gs2Realtime.Domain.Iterator;
using Gs2.Gs2Realtime.Model;
using Gs2.Gs2Realtime.Domain.Model;
using Gs2.Gs2Realtime.Request;
using Gs2.Gs2Realtime.Result;
using Gs2.Gs2Auth.Model;
using Gs2.Util.LitJson;
using Gs2.Core;
using Gs2.Core.Domain;
using System.Collections;
using Gs2.Unity.Core;
using Gs2.Unity.Util;
using Gs2.Gs2Realtime;
using UnityEngine.Events;
using UnityEngine.Scripting;
#if GS2_ENABLE_UNITASK
using Cysharp.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
#endif

namespace Gs2.Unity.Gs2Realtime.Domain
{
    public partial class Gs2Realtime {
        
#if GS2_ENABLE_UNITASK
        public async UniTask<Gs2DateTime> Gs2DateTimeAsync(
            IGameSession gameSession
        )
        {
            var item = await this._connection.RunAsync(
                null,
                async () =>
                {
                    var result = await new Gs2RealtimeRestClient(this._connection.RestSession).NowAsync(
                        new NowRequest()
                            .WithAccessToken(gameSession.AccessToken.Token)
                    );
                    if (result.Timestamp == null) {
                        return null;
                    }
                    return Gs2DateTime.FromMilliseconds(result.Timestamp.Value);
                }
            );
            if (item == null) {
                return null;
            }
            return item;
        }
#endif

        public IFuture<Gs2DateTime> Gs2DateTimeFuture(
            IGameSession gameSession
        )
        {
            IEnumerator Impl(Gs2Future<Gs2DateTime> self)
            {
                var future = this._connection.RunFuture(
                    null,
                    () => {
                        return new Gs2RealtimeRestClient(this._connection.RestSession).NowFuture(
                            new NowRequest()
                                .WithAccessToken(gameSession.AccessToken.Token)
                        );
                    }
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                if (future.Result.Timestamp == null) {
                    self.OnComplete(null);
                    yield break;
                }
                self.OnComplete(Gs2DateTime.FromMilliseconds(future.Result.Timestamp.Value));
            }
            return new Gs2InlineFuture<Gs2DateTime>(Impl);
        }

    }
}
