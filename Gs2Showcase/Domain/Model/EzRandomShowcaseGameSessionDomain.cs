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
using Gs2.Gs2Showcase.Domain.Iterator;
using Gs2.Gs2Showcase.Request;
using Gs2.Gs2Showcase.Result;
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

namespace Gs2.Unity.Gs2Showcase.Domain.Model
{

    public partial class EzRandomShowcaseGameSessionDomain {
        private readonly Gs2.Gs2Showcase.Domain.Model.RandomShowcaseAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;
        public string ShowcaseName => _domain?.ShowcaseName;

        public EzRandomShowcaseGameSessionDomain(
            Gs2.Gs2Showcase.Domain.Model.RandomShowcaseAccessTokenDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        public class EzRandomDisplayItemsIterator : Gs2Iterator<Gs2.Unity.Gs2Showcase.Model.EzRandomDisplayItem>
        {
            private Gs2Iterator<Gs2.Gs2Showcase.Model.RandomDisplayItem> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly Gs2.Gs2Showcase.Domain.Model.RandomShowcaseAccessTokenDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzRandomDisplayItemsIterator(
                Gs2Iterator<Gs2.Gs2Showcase.Model.RandomDisplayItem> it,
        #if !GS2_ENABLE_UNITASK
                Gs2.Gs2Showcase.Domain.Model.RandomShowcaseAccessTokenDomain domain,
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

            protected override IEnumerator Next(Action<AsyncResult<Gs2.Unity.Gs2Showcase.Model.EzRandomDisplayItem>> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    _domain.AccessToken,
                    _it,
                    () =>
                    {
                        return _it = _domain.RandomDisplayItems(
                        );
                    }
                );
        #endif
                callback.Invoke(
                    new AsyncResult<Gs2.Unity.Gs2Showcase.Model.EzRandomDisplayItem>(
                        _it.Current == null ? null : Gs2.Unity.Gs2Showcase.Model.EzRandomDisplayItem.FromModel(_it.Current),
                        _it.Error
                    )
                );
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Showcase.Model.EzRandomDisplayItem> RandomDisplayItems(
        )
        {
            return new EzRandomDisplayItemsIterator(
                _domain.RandomDisplayItems(
                ),
                _profile
            );
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Showcase.Model.EzRandomDisplayItem> RandomDisplayItemsAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Showcase.Model.EzRandomDisplayItem> RandomDisplayItems(
        #endif
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Showcase.Model.EzRandomDisplayItem>(async (writer, token) =>
            {
                var it = _domain.RandomDisplayItemsAsync(
                ).GetAsyncEnumerator();
                while(
                    await _profile.RunIteratorAsync(
                        _domain.AccessToken,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.RandomDisplayItemsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Showcase.Model.EzRandomDisplayItem.FromModel(it.Current));
                }
            });
        #else
            return new EzRandomDisplayItemsIterator(
                _domain.RandomDisplayItems(
                ),
                _domain,
                _profile
            );
        #endif
        }

        public Gs2.Unity.Gs2Showcase.Domain.Model.EzRandomDisplayItemGameSessionDomain RandomDisplayItem(
            string displayItemName
        ) {
            return new Gs2.Unity.Gs2Showcase.Domain.Model.EzRandomDisplayItemGameSessionDomain(
                _domain.RandomDisplayItem(
                    displayItemName
                ),
                _profile
            );
        }

    }
}
