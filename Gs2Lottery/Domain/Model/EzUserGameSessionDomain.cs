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
using Gs2.Gs2Lottery.Domain.Iterator;
using Gs2.Gs2Lottery.Request;
using Gs2.Gs2Lottery.Result;
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

namespace Gs2.Unity.Gs2Lottery.Domain.Model
{

    public partial class EzUserGameSessionDomain {
        private readonly Gs2.Gs2Lottery.Domain.Model.UserAccessTokenDomain _domain;
        public string NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserGameSessionDomain(
            Gs2.Gs2Lottery.Domain.Model.UserAccessTokenDomain domain
        ) {
            this._domain = domain;
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Lottery.Model.EzProbability> Probabilities(
        #else
        public class EzProbabilitiesIterator : Gs2Iterator<Gs2.Unity.Gs2Lottery.Model.EzProbability>
        {
            private readonly Gs2Iterator<Gs2.Gs2Lottery.Model.Probability> _it;

            public EzProbabilitiesIterator(
                Gs2Iterator<Gs2.Gs2Lottery.Model.Probability> it
            )
            {
                _it = it;
            }

            public override bool HasNext()
            {
                return _it.HasNext();
            }

            protected override IEnumerator Next(Action<Gs2.Unity.Gs2Lottery.Model.EzProbability> callback)
            {
                yield return _it.Next();
                callback.Invoke(Gs2.Unity.Gs2Lottery.Model.EzProbability.FromModel(_it.Current));
            }
        }

        public Gs2Iterator<Gs2.Unity.Gs2Lottery.Model.EzProbability> Probabilities(
        #endif
              string lotteryName
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Lottery.Model.EzProbability>(async (writer, token) =>
            {
                var it = _domain.Probabilities(
                    lotteryName
                ).GetAsyncEnumerator();
                while(await it.MoveNextAsync())
                {
                    await writer.YieldAsync(Gs2.Unity.Gs2Lottery.Model.EzProbability.FromModel(it.Current));
                }
            });
        #else
            return new EzProbabilitiesIterator(_domain.Probabilities(
               lotteryName
            ));
        #endif
        }

        public Gs2.Unity.Gs2Lottery.Domain.Model.EzProbabilityGameSessionDomain Probability(
        ) {
            return new Gs2.Unity.Gs2Lottery.Domain.Model.EzProbabilityGameSessionDomain(
                _domain.Probability(
                )
            );
        }

        #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Lottery.Model.EzBox> Boxes(
        #else
        public class EzBoxesIterator : Gs2Iterator<Gs2.Unity.Gs2Lottery.Model.EzBox>
        {
            private readonly Gs2Iterator<Gs2.Gs2Lottery.Model.Box> _it;

            public EzBoxesIterator(
                Gs2Iterator<Gs2.Gs2Lottery.Model.Box> it
            )
            {
                _it = it;
            }

            public override bool HasNext()
            {
                return _it.HasNext();
            }

            protected override IEnumerator Next(Action<Gs2.Unity.Gs2Lottery.Model.EzBox> callback)
            {
                yield return _it.Next();
                callback.Invoke(Gs2.Unity.Gs2Lottery.Model.EzBox.FromModel(_it.Current));
            }
        }

        public Gs2Iterator<Gs2.Unity.Gs2Lottery.Model.EzBox> Boxes(
        #endif
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Lottery.Model.EzBox>(async (writer, token) =>
            {
                var it = _domain.Boxes(
                ).GetAsyncEnumerator();
                while(await it.MoveNextAsync())
                {
                    await writer.YieldAsync(Gs2.Unity.Gs2Lottery.Model.EzBox.FromModel(it.Current));
                }
            });
        #else
            return new EzBoxesIterator(_domain.Boxes(
            ));
        #endif
        }

        public Gs2.Unity.Gs2Lottery.Domain.Model.EzBoxGameSessionDomain Box(
            string prizeTableName
        ) {
            return new Gs2.Unity.Gs2Lottery.Domain.Model.EzBoxGameSessionDomain(
                _domain.Box(
                    prizeTableName
                )
            );
        }

    }
}
