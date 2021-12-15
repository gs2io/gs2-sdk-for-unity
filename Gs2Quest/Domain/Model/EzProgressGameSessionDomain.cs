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
using Gs2.Gs2Quest.Domain.Iterator;
using Gs2.Gs2Quest.Request;
using Gs2.Gs2Quest.Result;
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

namespace Gs2.Unity.Gs2Quest.Domain.Model
{

    public partial class EzProgressGameSessionDomain {
        private readonly Gs2.Gs2Quest.Domain.Model.ProgressAccessTokenDomain _domain;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzProgressGameSessionDomain(
            Gs2.Gs2Quest.Domain.Model.ProgressAccessTokenDomain domain
        ) {
            this._domain = domain;
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Quest.Domain.Model.EzProgressGameSessionDomain> EndAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Quest.Domain.Model.EzProgressGameSessionDomain> End(
        #endif
              bool isComplete,
              Gs2.Unity.Gs2Quest.Model.EzReward[] rewards = null,
              Gs2.Unity.Gs2Quest.Model.EzConfig[] config = null
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _domain.EndAsync(
                new EndRequest()
                    .WithRewards(rewards?.Select(v => v.ToModel()).ToArray())
                    .WithIsComplete(isComplete)
                    .WithConfig(config?.Select(v => v.ToModel()).ToArray())
            );
            return new Gs2.Unity.Gs2Quest.Domain.Model.EzProgressGameSessionDomain(result);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Quest.Domain.Model.EzProgressGameSessionDomain> self)
            {
                var future = _domain.End(
                    new EndRequest()
                        .WithRewards(rewards?.Select(v => v.ToModel()).ToArray())
                        .WithIsComplete(isComplete)
                        .WithConfig(config?.Select(v => v.ToModel()).ToArray())
                );
                yield return future;
                if (future.Error != null)
                {
                    self.OnError(future.Error);
                    yield break;
                }
                var result = future.Result;
                self.OnComplete(new Gs2.Unity.Gs2Quest.Domain.Model.EzProgressGameSessionDomain(result));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Quest.Domain.Model.EzProgressGameSessionDomain>(Impl);
        #endif
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Quest.Model.EzProgress> Model()
        {
            var item = await _domain.Model();
            if (item == null) {
                return null;
            }
            return Gs2.Unity.Gs2Quest.Model.EzProgress.FromModel(
                item
            );
        }
        #else
        public IFuture<Gs2.Unity.Gs2Quest.Model.EzProgress> Model()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Quest.Model.EzProgress> self)
            {
                var future = _domain.Model();
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                var item = future.Result;
                if (item == null) {
                    self.OnComplete(null);
                    yield break;
                }
                self.OnComplete(Gs2.Unity.Gs2Quest.Model.EzProgress.FromModel(
                    item
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Quest.Model.EzProgress>(Impl);
        }
        #endif

    }
}
