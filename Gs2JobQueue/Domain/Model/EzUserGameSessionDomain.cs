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
using Gs2.Gs2JobQueue.Domain.Iterator;
using Gs2.Gs2JobQueue.Request;
using Gs2.Gs2JobQueue.Result;
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

namespace Gs2.Unity.Gs2JobQueue.Domain.Model
{

    public partial class EzUserGameSessionDomain {
        private readonly Gs2.Gs2JobQueue.Domain.Model.UserAccessTokenDomain _domain;
        public bool? IsLastJob => _domain.IsLastJob;
        public string NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserGameSessionDomain(
            Gs2.Gs2JobQueue.Domain.Model.UserAccessTokenDomain domain
        ) {
            this._domain = domain;
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2JobQueue.Domain.Model.EzJobGameSessionDomain> Run(
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2JobQueue.Domain.Model.EzJobGameSessionDomain> self)
            {
                yield return RunAsync(
                ).ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2JobQueue.Domain.Model.EzJobGameSessionDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2JobQueue.Domain.Model.EzJobGameSessionDomain> RunAsync(
        #else
        public IFuture<Gs2.Unity.Gs2JobQueue.Domain.Model.EzJobGameSessionDomain> Run(
        #endif
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _domain.RunAsync(
                new RunRequest()
            );
            return new Gs2.Unity.Gs2JobQueue.Domain.Model.EzJobGameSessionDomain(result);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2JobQueue.Domain.Model.EzJobGameSessionDomain> self)
            {
                var future = _domain.Run(
                    new RunRequest()
                );
                yield return future;
                if (future.Error != null)
                {
                    self.OnError(future.Error);
                    yield break;
                }
                var result = future.Result;
                self.OnComplete(new Gs2.Unity.Gs2JobQueue.Domain.Model.EzJobGameSessionDomain(result));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2JobQueue.Domain.Model.EzJobGameSessionDomain>(Impl);
        #endif
        }

        public Gs2.Unity.Gs2JobQueue.Domain.Model.EzJobGameSessionDomain Job(
            string jobName
        ) {
            return new Gs2.Unity.Gs2JobQueue.Domain.Model.EzJobGameSessionDomain(
                _domain.Job(
                    jobName
                )
            );
        }

    }
}
