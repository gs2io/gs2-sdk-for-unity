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
using Gs2.Gs2Limit.Domain.Iterator;
using Gs2.Gs2Limit.Request;
using Gs2.Gs2Limit.Result;
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

namespace Gs2.Unity.Gs2Limit.Domain.Model
{

    public partial class EzUserDomain {
        private readonly Gs2.Gs2Limit.Domain.Model.UserDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public string NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserDomain(
            Gs2.Gs2Limit.Domain.Model.UserDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        public class EzCountersIterator : Gs2Iterator<Gs2.Unity.Gs2Limit.Model.EzCounter>
        {
            private Gs2Iterator<Gs2.Gs2Limit.Model.Counter> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly string _limitName;
            private readonly Gs2.Gs2Limit.Domain.Model.UserDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzCountersIterator(
                Gs2Iterator<Gs2.Gs2Limit.Model.Counter> it,
        #if !GS2_ENABLE_UNITASK
                string limitName,
                Gs2.Gs2Limit.Domain.Model.UserDomain domain,
        #endif
                Gs2.Unity.Util.Profile profile
            )
            {
                _it = it;
        #if !GS2_ENABLE_UNITASK
                _limitName = limitName;
                _domain = domain;
        #endif
                _profile = profile;
            }

            public override bool HasNext()
            {
                return _it.HasNext();
            }

            protected override IEnumerator Next(Action<AsyncResult<Gs2.Unity.Gs2Limit.Model.EzCounter>> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    null,
                    _it,
                    () =>
                    {
                        _it = _domain.Counters(
                            _limitName
                        );
                    }
                );
        #endif
                callback.Invoke(
                    new AsyncResult<Gs2.Unity.Gs2Limit.Model.EzCounter>(
                        _it.Current == null ? null : Gs2.Unity.Gs2Limit.Model.EzCounter.FromModel(_it.Current),
                        _it.Error
                    )
                );
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Limit.Model.EzCounter> Counters(
              string limitName = null
        )
        {
            return new EzCountersIterator(
                _domain.Counters(
                    limitName
                ),
                _profile
            );
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Limit.Model.EzCounter> CountersAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Limit.Model.EzCounter> Counters(
        #endif
              string limitName = null
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Limit.Model.EzCounter>(async (writer, token) =>
            {
                var it = _domain.CountersAsync(
                    limitName
                ).GetAsyncEnumerator();
                while(
                    await _profile.RunIteratorAsync(
                        null,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.CountersAsync(
                                limitName
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Limit.Model.EzCounter.FromModel(it.Current));
                }
            });
        #else
            return new EzCountersIterator(
                _domain.Counters(
                    limitName
                ),
                limitName,
                _domain,
                _profile
            );
        #endif
        }

        public Gs2.Unity.Gs2Limit.Domain.Model.EzCounterDomain Counter(
            string limitName,
            string counterName
        ) {
            return new Gs2.Unity.Gs2Limit.Domain.Model.EzCounterDomain(
                _domain.Counter(
                    limitName,
                    counterName
                ),
                _profile
            );
        }

    }
}
