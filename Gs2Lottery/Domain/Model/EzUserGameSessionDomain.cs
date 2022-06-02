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
 *
 * deny overwrite
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
        private readonly Gs2.Unity.Util.Profile _profile;
        public string NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserGameSessionDomain(
            Gs2.Gs2Lottery.Domain.Model.UserAccessTokenDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Lottery.Model.EzBoxItems> GetBox(
              string prizeTableName
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Lottery.Model.EzBoxItems> self)
            {
                yield return GetBoxAsync(
                    prizeTableName
                ).ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Lottery.Model.EzBoxItems>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Lottery.Model.EzBoxItems> GetBoxAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Lottery.Model.EzBoxItems> GetBox(
        #endif
              string prizeTableName
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _domain.GetBoxAsync(
                new GetBoxRequest()
                    .WithPrizeTableName(prizeTableName)
            );
            return Gs2.Unity.Gs2Lottery.Model.EzBoxItems.FromModel(result);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Lottery.Model.EzBoxItems> self)
            {
                var future = _domain.GetBox(
                    new GetBoxRequest()
                        .WithPrizeTableName(prizeTableName)
                );
                yield return future;
                if (future.Error != null)
                {
                    self.OnError(future.Error);
                    yield break;
                }
                var result = future.Result;
                self.OnComplete(new Gs2.Unity.Gs2Lottery.Model.EzBoxItems()); // TODO:
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Lottery.Model.EzBoxItems>(Impl);
        #endif
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Lottery.Domain.Model.EzUserGameSessionDomain> ResetBox(
              string prizeTableName
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Lottery.Domain.Model.EzUserGameSessionDomain> self)
            {
                yield return ResetBoxAsync(
                    prizeTableName
                ).ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Lottery.Domain.Model.EzUserGameSessionDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Lottery.Domain.Model.EzUserGameSessionDomain> ResetBoxAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Lottery.Domain.Model.EzUserGameSessionDomain> ResetBox(
        #endif
              string prizeTableName
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _domain.ResetBoxAsync(
                new ResetBoxRequest()
                    .WithPrizeTableName(prizeTableName)
            );
            return new Gs2.Unity.Gs2Lottery.Domain.Model.EzUserGameSessionDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Lottery.Domain.Model.EzUserGameSessionDomain> self)
            {
                var future = _domain.ResetBox(
                    new ResetBoxRequest()
                        .WithPrizeTableName(prizeTableName)
                );
                yield return future;
                if (future.Error != null)
                {
                    self.OnError(future.Error);
                    yield break;
                }
                var result = future.Result;
                self.OnComplete(new Gs2.Unity.Gs2Lottery.Domain.Model.EzUserGameSessionDomain(result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Lottery.Domain.Model.EzUserGameSessionDomain>(Impl);
        #endif
        }

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
                callback.Invoke(_it.Current == null ? null : Gs2.Unity.Gs2Lottery.Model.EzProbability.FromModel(_it.Current));
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Lottery.Model.EzProbability> Probabilities(
              string lotteryName
        )
        {
            return new EzProbabilitiesIterator(_domain.Probabilities(
               lotteryName
            ));
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Lottery.Model.EzProbability> ProbabilitiesAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Lottery.Model.EzProbability> Probabilities(
        #endif
              string lotteryName
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Lottery.Model.EzProbability>(async (writer, token) =>
            {
                var it = _domain.ProbabilitiesAsync(
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
                _domain.Probability(), _profile
            );
        }

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
                callback.Invoke(_it.Current == null ? null : Gs2.Unity.Gs2Lottery.Model.EzBox.FromModel(_it.Current));
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Lottery.Model.EzBox> Boxes(
        )
        {
            return new EzBoxesIterator(_domain.Boxes(
            ));
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Lottery.Model.EzBox> BoxesAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Lottery.Model.EzBox> Boxes(
        #endif
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Lottery.Model.EzBox>(async (writer, token) =>
            {
                var it = _domain.BoxesAsync(
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
            string prizeTableName,
            string index
        ) {
            return new Gs2.Unity.Gs2Lottery.Domain.Model.EzBoxGameSessionDomain(
                _domain.Box(
                    prizeTableName
                )
            );
        }

    }
}
