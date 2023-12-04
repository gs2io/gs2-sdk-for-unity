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
using Gs2.Gs2LoginReward.Domain.Iterator;
using Gs2.Gs2LoginReward.Request;
using Gs2.Gs2LoginReward.Result;
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

namespace Gs2.Unity.Gs2LoginReward.Domain.Model
{

    public partial class EzBonusGameSessionDomain {
        private readonly Gs2.Gs2LoginReward.Domain.Model.BonusAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.GameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string? TransactionId => _domain.TransactionId;
        public bool? AutoRunStampSheet => _domain.AutoRunStampSheet;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzBonusGameSessionDomain(
            Gs2.Gs2LoginReward.Domain.Model.BonusAccessTokenDomain domain,
            Gs2.Unity.Util.GameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }

        [Obsolete("The name has been changed to ReceiveFuture.")]
        public IFuture<Gs2.Unity.Core.Domain.EzTransactionDomain> Receive(
            string bonusModelName,
            Gs2.Unity.Gs2LoginReward.Model.EzConfig[] config = null
        )
        {
            return ReceiveFuture(
                bonusModelName,
                config
            );
        }

        public IFuture<Gs2.Unity.Core.Domain.EzTransactionDomain> ReceiveFuture(
            string bonusModelName,
            Gs2.Unity.Gs2LoginReward.Model.EzConfig[] config = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Core.Domain.EzTransactionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.ReceiveFuture(
                        new ReceiveRequest()
                            .WithBonusModelName(bonusModelName)
                            .WithConfig(config?.Select(v => v.ToModel()).ToArray())
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
        public async UniTask<Gs2.Unity.Core.Domain.EzTransactionDomain> ReceiveAsync(
            string bonusModelName,
            Gs2.Unity.Gs2LoginReward.Model.EzConfig[] config = null
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.ReceiveAsync(
                    new ReceiveRequest()
                        .WithBonusModelName(bonusModelName)
                        .WithConfig(config?.Select(v => v.ToModel()).ToArray())
                )
            );
            return result == null ? null : new Gs2.Unity.Core.Domain.EzTransactionDomain(result);
        }
        #endif

        [Obsolete("The name has been changed to MissedReceiveFuture.")]
        public IFuture<Gs2.Unity.Core.Domain.EzTransactionDomain> MissedReceive(
            string bonusModelName,
            int stepNumber,
            Gs2.Unity.Gs2LoginReward.Model.EzConfig[] config = null
        )
        {
            return MissedReceiveFuture(
                bonusModelName,
                stepNumber,
                config
            );
        }

        public IFuture<Gs2.Unity.Core.Domain.EzTransactionDomain> MissedReceiveFuture(
            string bonusModelName,
            int stepNumber,
            Gs2.Unity.Gs2LoginReward.Model.EzConfig[] config = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Core.Domain.EzTransactionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.MissedReceiveFuture(
                        new MissedReceiveRequest()
                            .WithBonusModelName(bonusModelName)
                            .WithStepNumber(stepNumber)
                            .WithConfig(config?.Select(v => v.ToModel()).ToArray())
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
        public async UniTask<Gs2.Unity.Core.Domain.EzTransactionDomain> MissedReceiveAsync(
            string bonusModelName,
            int stepNumber,
            Gs2.Unity.Gs2LoginReward.Model.EzConfig[] config = null
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.MissedReceiveAsync(
                    new MissedReceiveRequest()
                        .WithBonusModelName(bonusModelName)
                        .WithStepNumber(stepNumber)
                        .WithConfig(config?.Select(v => v.ToModel()).ToArray())
                )
            );
            return result == null ? null : new Gs2.Unity.Core.Domain.EzTransactionDomain(result);
        }
        #endif

    }
}
