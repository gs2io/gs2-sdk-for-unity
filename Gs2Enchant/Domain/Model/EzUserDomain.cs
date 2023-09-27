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
using Gs2.Gs2Enchant.Domain.Iterator;
using Gs2.Gs2Enchant.Request;
using Gs2.Gs2Enchant.Result;
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

namespace Gs2.Unity.Gs2Enchant.Domain.Model
{

    public partial class EzUserDomain {
        private readonly Gs2.Gs2Enchant.Domain.Model.UserDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public string NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserDomain(
            Gs2.Gs2Enchant.Domain.Model.UserDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        public class EzBalanceParameterStatusesIterator : Gs2Iterator<Gs2.Unity.Gs2Enchant.Model.EzBalanceParameterStatus>
        {
            private Gs2Iterator<Gs2.Gs2Enchant.Model.BalanceParameterStatus> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly string _parameterName;
            private readonly Gs2.Gs2Enchant.Domain.Model.UserDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzBalanceParameterStatusesIterator(
                Gs2Iterator<Gs2.Gs2Enchant.Model.BalanceParameterStatus> it,
        #if !GS2_ENABLE_UNITASK
                string parameterName,
                Gs2.Gs2Enchant.Domain.Model.UserDomain domain,
        #endif
                Gs2.Unity.Util.Profile profile
            )
            {
                _it = it;
        #if !GS2_ENABLE_UNITASK
                _parameterName = parameterName;
                _domain = domain;
        #endif
                _profile = profile;
            }

            public override bool HasNext()
            {
                return _it.HasNext();
            }

            protected override IEnumerator Next(Action<AsyncResult<Gs2.Unity.Gs2Enchant.Model.EzBalanceParameterStatus>> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    null,
                    _it,
                    () =>
                    {
                        return _it = _domain.BalanceParameterStatuses(
                            _parameterName
                        );
                    }
                );
        #endif
                callback.Invoke(
                    new AsyncResult<Gs2.Unity.Gs2Enchant.Model.EzBalanceParameterStatus>(
                        _it.Current == null ? null : Gs2.Unity.Gs2Enchant.Model.EzBalanceParameterStatus.FromModel(_it.Current),
                        _it.Error
                    )
                );
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Enchant.Model.EzBalanceParameterStatus> BalanceParameterStatuses(
              string parameterName = null
        )
        {
            return new EzBalanceParameterStatusesIterator(
                _domain.BalanceParameterStatuses(
                    parameterName
                ),
                _profile
            );
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Enchant.Model.EzBalanceParameterStatus> BalanceParameterStatusesAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Enchant.Model.EzBalanceParameterStatus> BalanceParameterStatuses(
        #endif
              string parameterName = null
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Enchant.Model.EzBalanceParameterStatus>(async (writer, token) =>
            {
                var it = _domain.BalanceParameterStatusesAsync(
                    parameterName
                ).GetAsyncEnumerator();
                while(
                    await _profile.RunIteratorAsync(
                        null,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.BalanceParameterStatusesAsync(
                                parameterName
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Enchant.Model.EzBalanceParameterStatus.FromModel(it.Current));
                }
            });
        #else
            return new EzBalanceParameterStatusesIterator(
                _domain.BalanceParameterStatuses(
                    parameterName
                ),
                parameterName,
                _domain,
                _profile
            );
        #endif
        }

        public ulong SubscribeBalanceParameterStatuses(Action callback) {
            return this._domain.SubscribeBalanceParameterStatuses(callback);
        }

        public void UnsubscribeBalanceParameterStatuses(ulong callbackId) {
            this._domain.UnsubscribeBalanceParameterStatuses(callbackId);
        }

        public Gs2.Unity.Gs2Enchant.Domain.Model.EzBalanceParameterStatusDomain BalanceParameterStatus(
            string parameterName,
            string propertyId
        ) {
            return new Gs2.Unity.Gs2Enchant.Domain.Model.EzBalanceParameterStatusDomain(
                _domain.BalanceParameterStatus(
                    parameterName,
                    propertyId
                ),
                _profile
            );
        }

        public class EzRarityParameterStatusesIterator : Gs2Iterator<Gs2.Unity.Gs2Enchant.Model.EzRarityParameterStatus>
        {
            private Gs2Iterator<Gs2.Gs2Enchant.Model.RarityParameterStatus> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly string _parameterName;
            private readonly Gs2.Gs2Enchant.Domain.Model.UserDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzRarityParameterStatusesIterator(
                Gs2Iterator<Gs2.Gs2Enchant.Model.RarityParameterStatus> it,
        #if !GS2_ENABLE_UNITASK
                string parameterName,
                Gs2.Gs2Enchant.Domain.Model.UserDomain domain,
        #endif
                Gs2.Unity.Util.Profile profile
            )
            {
                _it = it;
        #if !GS2_ENABLE_UNITASK
                _parameterName = parameterName;
                _domain = domain;
        #endif
                _profile = profile;
            }

            public override bool HasNext()
            {
                return _it.HasNext();
            }

            protected override IEnumerator Next(Action<AsyncResult<Gs2.Unity.Gs2Enchant.Model.EzRarityParameterStatus>> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    null,
                    _it,
                    () =>
                    {
                        return _it = _domain.RarityParameterStatuses(
                            _parameterName
                        );
                    }
                );
        #endif
                callback.Invoke(
                    new AsyncResult<Gs2.Unity.Gs2Enchant.Model.EzRarityParameterStatus>(
                        _it.Current == null ? null : Gs2.Unity.Gs2Enchant.Model.EzRarityParameterStatus.FromModel(_it.Current),
                        _it.Error
                    )
                );
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Enchant.Model.EzRarityParameterStatus> RarityParameterStatuses(
              string parameterName = null
        )
        {
            return new EzRarityParameterStatusesIterator(
                _domain.RarityParameterStatuses(
                    parameterName
                ),
                _profile
            );
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Enchant.Model.EzRarityParameterStatus> RarityParameterStatusesAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Enchant.Model.EzRarityParameterStatus> RarityParameterStatuses(
        #endif
              string parameterName = null
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Enchant.Model.EzRarityParameterStatus>(async (writer, token) =>
            {
                var it = _domain.RarityParameterStatusesAsync(
                    parameterName
                ).GetAsyncEnumerator();
                while(
                    await _profile.RunIteratorAsync(
                        null,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.RarityParameterStatusesAsync(
                                parameterName
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Enchant.Model.EzRarityParameterStatus.FromModel(it.Current));
                }
            });
        #else
            return new EzRarityParameterStatusesIterator(
                _domain.RarityParameterStatuses(
                    parameterName
                ),
                parameterName,
                _domain,
                _profile
            );
        #endif
        }

        public ulong SubscribeRarityParameterStatuses(Action callback) {
            return this._domain.SubscribeRarityParameterStatuses(callback);
        }

        public void UnsubscribeRarityParameterStatuses(ulong callbackId) {
            this._domain.UnsubscribeRarityParameterStatuses(callbackId);
        }

        public Gs2.Unity.Gs2Enchant.Domain.Model.EzRarityParameterStatusDomain RarityParameterStatus(
            string parameterName,
            string propertyId
        ) {
            return new Gs2.Unity.Gs2Enchant.Domain.Model.EzRarityParameterStatusDomain(
                _domain.RarityParameterStatus(
                    parameterName,
                    propertyId
                ),
                _profile
            );
        }

    }
}
