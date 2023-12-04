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
        private readonly Gs2.Unity.Util.GameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public bool? AutoRun => _domain.AutoRun;
        public bool? IsLastJob => _domain.IsLastJob;
        public string? NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserGameSessionDomain(
            Gs2.Gs2JobQueue.Domain.Model.UserAccessTokenDomain domain,
            Gs2.Unity.Util.GameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }

        [Obsolete("The name has been changed to RunFuture.")]
        public IFuture<Gs2.Unity.Gs2JobQueue.Domain.Model.EzJobGameSessionDomain> Run(
        )
        {
            return RunFuture(
            );
        }

        public IFuture<Gs2.Unity.Gs2JobQueue.Domain.Model.EzJobGameSessionDomain> RunFuture(
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2JobQueue.Domain.Model.EzJobGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.RunFuture(
                        new RunRequest()
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2JobQueue.Domain.Model.EzJobGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2JobQueue.Domain.Model.EzJobGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2JobQueue.Domain.Model.EzJobGameSessionDomain> RunAsync(
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.RunAsync(
                    new RunRequest()
                )
            );
            return new Gs2.Unity.Gs2JobQueue.Domain.Model.EzJobGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        public Gs2.Unity.Gs2JobQueue.Domain.Model.EzJobGameSessionDomain Job(
            string jobName
        ) {
            return new Gs2.Unity.Gs2JobQueue.Domain.Model.EzJobGameSessionDomain(
                _domain.Job(
                    jobName
                ),
                this._gameSession,
                this._connection
            );
        }

    }
}
