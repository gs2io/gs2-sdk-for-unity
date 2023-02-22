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
        private readonly Gs2.Unity.Util.Profile _profile;
        public string TransactionId => _domain.TransactionId;
        public bool? AutoRunStampSheet => _domain.AutoRunStampSheet;
        public string NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserGameSessionDomain(
            Gs2.Gs2Lottery.Domain.Model.UserAccessTokenDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        public class EzBoxesIterator : Gs2Iterator<Gs2.Unity.Gs2Lottery.Model.EzBoxItems>
        {
            private Gs2Iterator<Gs2.Gs2Lottery.Model.BoxItems> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly Gs2.Gs2Lottery.Domain.Model.UserAccessTokenDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzBoxesIterator(
                Gs2Iterator<Gs2.Gs2Lottery.Model.BoxItems> it,
        #if !GS2_ENABLE_UNITASK
                Gs2.Gs2Lottery.Domain.Model.UserAccessTokenDomain domain,
        #endif
                Gs2.Unity.Util.Profile profile
            )
            {
                _it = it;
        #if !GS2_ENABLE_UNITASK
                _domain = domain;
        #endif
                _profile = profile;
            }

            public override bool HasNext()
            {
                return _it.HasNext();
            }

            protected override IEnumerator Next(Action<AsyncResult<Gs2.Unity.Gs2Lottery.Model.EzBoxItems>> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    _domain.AccessToken,
                    _it,
                    () =>
                    {
                        _it = _domain.Boxes(
                        );
                    }
                );
        #endif
                callback.Invoke(
                    new AsyncResult<Gs2.Unity.Gs2Lottery.Model.EzBoxItems>(
                        _it.Current == null ? null : Gs2.Unity.Gs2Lottery.Model.EzBoxItems.FromModel(_it.Current),
                        _it.Error
                    )
                );
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Lottery.Model.EzBoxItems> Boxes(
        )
        {
            return new EzBoxesIterator(
                _domain.Boxes(
                ),
                _profile
            );
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Lottery.Model.EzBoxItems> BoxesAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Lottery.Model.EzBoxItems> Boxes(
        #endif
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Lottery.Model.EzBoxItems>(async (writer, token) =>
            {
                var it = _domain.BoxesAsync(
                ).GetAsyncEnumerator();
                while(
                    await _profile.RunIteratorAsync(
                        _domain.AccessToken,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.BoxesAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Lottery.Model.EzBoxItems.FromModel(it.Current));
                }
            });
        #else
            return new EzBoxesIterator(
                _domain.Boxes(
                ),
                _domain,
                _profile
            );
        #endif
        }

        public Gs2.Unity.Gs2Lottery.Domain.Model.EzBoxItemsGameSessionDomain BoxItems(
            string prizeTableName
        ) {
            return new Gs2.Unity.Gs2Lottery.Domain.Model.EzBoxItemsGameSessionDomain(
                _domain.BoxItems(
                    prizeTableName
                ),
                _profile
            );
        }

        public class EzProbabilitiesIterator : Gs2Iterator<Gs2.Unity.Gs2Lottery.Model.EzProbability>
        {
            private Gs2Iterator<Gs2.Gs2Lottery.Model.Probability> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly string _lotteryName;
            private readonly Gs2.Gs2Lottery.Domain.Model.UserAccessTokenDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzProbabilitiesIterator(
                Gs2Iterator<Gs2.Gs2Lottery.Model.Probability> it,
        #if !GS2_ENABLE_UNITASK
                string lotteryName,
                Gs2.Gs2Lottery.Domain.Model.UserAccessTokenDomain domain,
        #endif
                Gs2.Unity.Util.Profile profile
            )
            {
                _it = it;
        #if !GS2_ENABLE_UNITASK
                _lotteryName = lotteryName;
                _domain = domain;
        #endif
                _profile = profile;
            }

            public override bool HasNext()
            {
                return _it.HasNext();
            }

            protected override IEnumerator Next(Action<AsyncResult<Gs2.Unity.Gs2Lottery.Model.EzProbability>> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    _domain.AccessToken,
                    _it,
                    () =>
                    {
                        _it = _domain.Probabilities(
                            _lotteryName
                        );
                    }
                );
        #endif
                callback.Invoke(
                    new AsyncResult<Gs2.Unity.Gs2Lottery.Model.EzProbability>(
                        _it.Current == null ? null : Gs2.Unity.Gs2Lottery.Model.EzProbability.FromModel(_it.Current),
                        _it.Error
                    )
                );
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Lottery.Model.EzProbability> Probabilities(
              string lotteryName
        )
        {
            return new EzProbabilitiesIterator(
                _domain.Probabilities(
                    lotteryName
                ),
                _profile
            );
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
                while(
                    await _profile.RunIteratorAsync(
                        _domain.AccessToken,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.ProbabilitiesAsync(
                                lotteryName
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Lottery.Model.EzProbability.FromModel(it.Current));
                }
            });
        #else
            return new EzProbabilitiesIterator(
                _domain.Probabilities(
                    lotteryName
                ),
                lotteryName,
                _domain,
                _profile
            );
        #endif
        }

        public Gs2.Unity.Gs2Lottery.Domain.Model.EzProbabilityGameSessionDomain Probability(
        ) {
            return new Gs2.Unity.Gs2Lottery.Domain.Model.EzProbabilityGameSessionDomain(
                _domain.Probability(
                ),
                _profile
            );
        }

    }
}
