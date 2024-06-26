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
using Gs2.Gs2Version.Domain.Iterator;
using Gs2.Gs2Version.Request;
using Gs2.Gs2Version.Result;
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

namespace Gs2.Unity.Gs2Version.Domain.Model
{

    public partial class EzCheckerGameSessionDomain {
        private readonly Gs2.Gs2Version.Domain.Model.CheckerAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.IGameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string? ProjectToken => _domain.ProjectToken;
        public Gs2.Unity.Gs2Version.Model.EzStatus[] Warnings => _domain.Warnings.Select(Gs2.Unity.Gs2Version.Model.EzStatus.FromModel).ToArray();
        public Gs2.Unity.Gs2Version.Model.EzStatus[] Errors => _domain.Errors.Select(Gs2.Unity.Gs2Version.Model.EzStatus.FromModel).ToArray();
        public string? Body => _domain.Body;
        public string? Signature => _domain.Signature;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzCheckerGameSessionDomain(
            Gs2.Gs2Version.Domain.Model.CheckerAccessTokenDomain domain,
            Gs2.Unity.Util.IGameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }

        [Obsolete("The name has been changed to CheckVersionFuture.")]
        public IFuture<Gs2.Unity.Gs2Version.Domain.Model.EzCheckerGameSessionDomain> CheckVersion(
            Gs2.Unity.Gs2Version.Model.EzTargetVersion[] targetVersions = null
        )
        {
            return CheckVersionFuture(
                targetVersions
            );
        }

        public IFuture<Gs2.Unity.Gs2Version.Domain.Model.EzCheckerGameSessionDomain> CheckVersionFuture(
            Gs2.Unity.Gs2Version.Model.EzTargetVersion[] targetVersions = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Version.Domain.Model.EzCheckerGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.CheckVersionFuture(
                        new CheckVersionRequest()
                            .WithTargetVersions(targetVersions?.Select(v => v.ToModel()).ToArray())
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Version.Domain.Model.EzCheckerGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Version.Domain.Model.EzCheckerGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Version.Domain.Model.EzCheckerGameSessionDomain> CheckVersionAsync(
            Gs2.Unity.Gs2Version.Model.EzTargetVersion[] targetVersions = null
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.CheckVersionAsync(
                    new CheckVersionRequest()
                        .WithTargetVersions(targetVersions?.Select(v => v.ToModel()).ToArray())
                )
            );
            return new Gs2.Unity.Gs2Version.Domain.Model.EzCheckerGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

    }
}
