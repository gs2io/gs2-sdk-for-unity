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
#pragma warning disable CS0169, CS0168

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

    public partial class EzUserDomain {
        private readonly Gs2.Gs2Lottery.Domain.Model.UserDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public string TransactionId => _domain.TransactionId;
        public bool? AutoRunStampSheet => _domain.AutoRunStampSheet;
        public string NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserDomain(
            Gs2.Gs2Lottery.Domain.Model.UserDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        public class EzBoxesIterator : Gs2Iterator<Gs2.Unity.Gs2Lottery.Model.EzBoxItems>
        {
            private Gs2Iterator<Gs2.Gs2Lottery.Model.BoxItems> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly Gs2.Gs2Lottery.Domain.Model.UserDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzBoxesIterator(
                Gs2Iterator<Gs2.Gs2Lottery.Model.BoxItems> it,
        #if !GS2_ENABLE_UNITASK
                Gs2.Gs2Lottery.Domain.Model.UserDomain domain,
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
                    null,
                    _it,
                    () =>
                    {
                        return _it = _domain.Boxes(
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
                        null,
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

        public ulong SubscribeBoxes(Action callback) {
            return this._domain.SubscribeBoxes(callback);
        }

        public void UnsubscribeBoxes(ulong callbackId) {
            this._domain.UnsubscribeBoxes(callbackId);
        }

        public Gs2.Unity.Gs2Lottery.Domain.Model.EzBoxItemsDomain BoxItems(
            string prizeTableName
        ) {
            return new Gs2.Unity.Gs2Lottery.Domain.Model.EzBoxItemsDomain(
                _domain.BoxItems(
                    prizeTableName
                ),
                _profile
            );
        }

    }
}
