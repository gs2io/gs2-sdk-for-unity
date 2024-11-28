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
using Gs2.Gs2Enhance.Domain.Iterator;
using Gs2.Gs2Enhance.Request;
using Gs2.Gs2Enhance.Result;
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

namespace Gs2.Unity.Gs2Enhance.Domain.Model
{

    public partial class EzEnhanceGameSessionDomain {
        private readonly Gs2.Gs2Enhance.Domain.Model.EnhanceAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.IGameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public long? AcquireExperience => _domain.AcquireExperience;
        public float? BonusRate => _domain.BonusRate;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzEnhanceGameSessionDomain(
            Gs2.Gs2Enhance.Domain.Model.EnhanceAccessTokenDomain domain,
            Gs2.Unity.Util.IGameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }

        [Obsolete("The name has been changed to EnhanceFuture.")]
        public IFuture<Gs2.Unity.Core.Domain.EzTransactionDomain> Enhance(
            string rateName,
            string targetItemSetId,
            Gs2.Unity.Gs2Enhance.Model.EzMaterial[] materials,
            Gs2.Unity.Gs2Enhance.Model.EzConfig[] config = null,
            bool speculativeExecute = true
        )
        {
            return EnhanceFuture(
                rateName,
                targetItemSetId,
                materials,
                config,
                speculativeExecute
            );
        }

        public IFuture<Gs2.Unity.Core.Domain.EzTransactionDomain> EnhanceFuture(
            string rateName,
            string targetItemSetId,
            Gs2.Unity.Gs2Enhance.Model.EzMaterial[] materials,
            Gs2.Unity.Gs2Enhance.Model.EzConfig[] config = null,
            bool speculativeExecute = true
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Core.Domain.EzTransactionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.DirectFuture(
                        new DirectEnhanceRequest()
                            .WithRateName(rateName)
                            .WithTargetItemSetId(targetItemSetId)
                            .WithMaterials(materials?.Select(v => v.ToModel()).ToArray())
                            .WithConfig(config?.Select(v => v.ToModel()).ToArray()),
                        speculativeExecute
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(future.Result == null ? null : new Gs2.Unity.Core.Domain.EzTransactionDomain(future.Result));
            }
            return new Gs2InlineFuture<Gs2.Unity.Core.Domain.EzTransactionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Core.Domain.EzTransactionDomain> EnhanceAsync(
            string rateName,
            string targetItemSetId,
            Gs2.Unity.Gs2Enhance.Model.EzMaterial[] materials,
            Gs2.Unity.Gs2Enhance.Model.EzConfig[] config = null,
            bool speculativeExecute = true
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.DirectAsync(
                    new DirectEnhanceRequest()
                        .WithRateName(rateName)
                        .WithTargetItemSetId(targetItemSetId)
                        .WithMaterials(materials?.Select(v => v.ToModel()).ToArray())
                        .WithConfig(config?.Select(v => v.ToModel()).ToArray()),
                    speculativeExecute
                )
            );
            return result == null ? null : new Gs2.Unity.Core.Domain.EzTransactionDomain(result);
        }
        #endif

    }
}
