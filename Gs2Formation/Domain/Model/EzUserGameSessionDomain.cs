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
using Gs2.Gs2Formation.Domain.Iterator;
using Gs2.Gs2Formation.Request;
using Gs2.Gs2Formation.Result;
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

namespace Gs2.Unity.Gs2Formation.Domain.Model
{

    public partial class EzUserGameSessionDomain {
        private readonly Gs2.Gs2Formation.Domain.Model.UserAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public string TransactionId => _domain.TransactionId;
        public bool? AutoRunStampSheet => _domain.AutoRunStampSheet;
        public string NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserGameSessionDomain(
            Gs2.Gs2Formation.Domain.Model.UserAccessTokenDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        public class EzMoldsIterator : Gs2Iterator<Gs2.Unity.Gs2Formation.Model.EzMold>
        {
            private Gs2Iterator<Gs2.Gs2Formation.Model.Mold> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly Gs2.Gs2Formation.Domain.Model.UserAccessTokenDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzMoldsIterator(
                Gs2Iterator<Gs2.Gs2Formation.Model.Mold> it,
        #if !GS2_ENABLE_UNITASK
                Gs2.Gs2Formation.Domain.Model.UserAccessTokenDomain domain,
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

            protected override IEnumerator Next(Action<AsyncResult<Gs2.Unity.Gs2Formation.Model.EzMold>> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    _domain.AccessToken,
                    _it,
                    () =>
                    {
                        return _it = _domain.Molds(
                        );
                    }
                );
        #endif
                callback.Invoke(
                    new AsyncResult<Gs2.Unity.Gs2Formation.Model.EzMold>(
                        _it.Current == null ? null : Gs2.Unity.Gs2Formation.Model.EzMold.FromModel(_it.Current),
                        _it.Error
                    )
                );
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Formation.Model.EzMold> Molds(
        )
        {
            return new EzMoldsIterator(
                _domain.Molds(
                ),
                _profile
            );
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Formation.Model.EzMold> MoldsAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Formation.Model.EzMold> Molds(
        #endif
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Formation.Model.EzMold>(async (writer, token) =>
            {
                var it = _domain.MoldsAsync(
                ).GetAsyncEnumerator();
                while(
                    await _profile.RunIteratorAsync(
                        _domain.AccessToken,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.MoldsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Formation.Model.EzMold.FromModel(it.Current));
                }
            });
        #else
            return new EzMoldsIterator(
                _domain.Molds(
                ),
                _domain,
                _profile
            );
        #endif
        }

        public Gs2.Unity.Gs2Formation.Domain.Model.EzMoldGameSessionDomain Mold(
            string moldName
        ) {
            return new Gs2.Unity.Gs2Formation.Domain.Model.EzMoldGameSessionDomain(
                _domain.Mold(
                    moldName
                ),
                _profile
            );
        }

        public class EzPropertyFormsIterator : Gs2Iterator<Gs2.Unity.Gs2Formation.Model.EzPropertyForm>
        {
            private Gs2Iterator<Gs2.Gs2Formation.Model.PropertyForm> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly string _formModelName;
            private readonly Gs2.Gs2Formation.Domain.Model.UserAccessTokenDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzPropertyFormsIterator(
                Gs2Iterator<Gs2.Gs2Formation.Model.PropertyForm> it,
        #if !GS2_ENABLE_UNITASK
                string formModelName,
                Gs2.Gs2Formation.Domain.Model.UserAccessTokenDomain domain,
        #endif
                Gs2.Unity.Util.Profile profile
            )
            {
                _it = it;
        #if !GS2_ENABLE_UNITASK
                _formModelName = formModelName;
                _domain = domain;
        #endif
                _profile = profile;
            }

            public override bool HasNext()
            {
                return _it.HasNext();
            }

            protected override IEnumerator Next(Action<AsyncResult<Gs2.Unity.Gs2Formation.Model.EzPropertyForm>> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    _domain.AccessToken,
                    _it,
                    () =>
                    {
                        return _it = _domain.PropertyForms(
                            _formModelName
                        );
                    }
                );
        #endif
                callback.Invoke(
                    new AsyncResult<Gs2.Unity.Gs2Formation.Model.EzPropertyForm>(
                        _it.Current == null ? null : Gs2.Unity.Gs2Formation.Model.EzPropertyForm.FromModel(_it.Current),
                        _it.Error
                    )
                );
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Formation.Model.EzPropertyForm> PropertyForms(
              string formModelName
        )
        {
            return new EzPropertyFormsIterator(
                _domain.PropertyForms(
                    formModelName
                ),
                _profile
            );
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Formation.Model.EzPropertyForm> PropertyFormsAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Formation.Model.EzPropertyForm> PropertyForms(
        #endif
              string formModelName
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Formation.Model.EzPropertyForm>(async (writer, token) =>
            {
                var it = _domain.PropertyFormsAsync(
                    formModelName
                ).GetAsyncEnumerator();
                while(
                    await _profile.RunIteratorAsync(
                        _domain.AccessToken,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.PropertyFormsAsync(
                                formModelName
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Formation.Model.EzPropertyForm.FromModel(it.Current));
                }
            });
        #else
            return new EzPropertyFormsIterator(
                _domain.PropertyForms(
                    formModelName
                ),
                formModelName,
                _domain,
                _profile
            );
        #endif
        }

        public Gs2.Unity.Gs2Formation.Domain.Model.EzPropertyFormGameSessionDomain PropertyForm(
            string formModelName,
            string propertyId
        ) {
            return new Gs2.Unity.Gs2Formation.Domain.Model.EzPropertyFormGameSessionDomain(
                _domain.PropertyForm(
                    formModelName,
                    propertyId
                ),
                _profile
            );
        }

    }
}
