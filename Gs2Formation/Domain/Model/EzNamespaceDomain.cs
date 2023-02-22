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

    public partial class EzNamespaceDomain {
        private readonly Gs2.Gs2Formation.Domain.Model.NamespaceDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public string Status => _domain.Status;
        public string NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;

        public EzNamespaceDomain(
            Gs2.Gs2Formation.Domain.Model.NamespaceDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        public class EzMoldModelsIterator : Gs2Iterator<Gs2.Unity.Gs2Formation.Model.EzMoldModel>
        {
            private Gs2Iterator<Gs2.Gs2Formation.Model.MoldModel> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly Gs2.Gs2Formation.Domain.Model.NamespaceDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzMoldModelsIterator(
                Gs2Iterator<Gs2.Gs2Formation.Model.MoldModel> it,
        #if !GS2_ENABLE_UNITASK
                Gs2.Gs2Formation.Domain.Model.NamespaceDomain domain,
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

            protected override IEnumerator Next(Action<AsyncResult<Gs2.Unity.Gs2Formation.Model.EzMoldModel>> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    null,
                    _it,
                    () =>
                    {
                        _it = _domain.MoldModels(
                        );
                    }
                );
        #endif
                callback.Invoke(
                    new AsyncResult<Gs2.Unity.Gs2Formation.Model.EzMoldModel>(
                        _it.Current == null ? null : Gs2.Unity.Gs2Formation.Model.EzMoldModel.FromModel(_it.Current),
                        _it.Error
                    )
                );
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Formation.Model.EzMoldModel> MoldModels(
        )
        {
            return new EzMoldModelsIterator(
                _domain.MoldModels(
                ),
                _profile
            );
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Formation.Model.EzMoldModel> MoldModelsAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Formation.Model.EzMoldModel> MoldModels(
        #endif
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Formation.Model.EzMoldModel>(async (writer, token) =>
            {
                var it = _domain.MoldModelsAsync(
                ).GetAsyncEnumerator();
                while(
                    await _profile.RunIteratorAsync(
                        null,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.MoldModelsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Formation.Model.EzMoldModel.FromModel(it.Current));
                }
            });
        #else
            return new EzMoldModelsIterator(
                _domain.MoldModels(
                ),
                _domain,
                _profile
            );
        #endif
        }

        public Gs2.Unity.Gs2Formation.Domain.Model.EzMoldModelDomain MoldModel(
            string moldName
        ) {
            return new Gs2.Unity.Gs2Formation.Domain.Model.EzMoldModelDomain(
                _domain.MoldModel(
                    moldName
                ),
                _profile
            );
        }

        public class EzFormModelsIterator : Gs2Iterator<Gs2.Unity.Gs2Formation.Model.EzFormModel>
        {
            private Gs2Iterator<Gs2.Gs2Formation.Model.FormModel> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly Gs2.Gs2Formation.Domain.Model.NamespaceDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzFormModelsIterator(
                Gs2Iterator<Gs2.Gs2Formation.Model.FormModel> it,
        #if !GS2_ENABLE_UNITASK
                Gs2.Gs2Formation.Domain.Model.NamespaceDomain domain,
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

            protected override IEnumerator Next(Action<AsyncResult<Gs2.Unity.Gs2Formation.Model.EzFormModel>> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    null,
                    _it,
                    () =>
                    {
                        _it = _domain.FormModels(
                        );
                    }
                );
        #endif
                callback.Invoke(
                    new AsyncResult<Gs2.Unity.Gs2Formation.Model.EzFormModel>(
                        _it.Current == null ? null : Gs2.Unity.Gs2Formation.Model.EzFormModel.FromModel(_it.Current),
                        _it.Error
                    )
                );
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Formation.Model.EzFormModel> FormModels(
        )
        {
            return new EzFormModelsIterator(
                _domain.FormModels(
                ),
                _profile
            );
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Formation.Model.EzFormModel> FormModelsAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Formation.Model.EzFormModel> FormModels(
        #endif
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Formation.Model.EzFormModel>(async (writer, token) =>
            {
                var it = _domain.FormModelsAsync(
                ).GetAsyncEnumerator();
                while(
                    await _profile.RunIteratorAsync(
                        null,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.FormModelsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Formation.Model.EzFormModel.FromModel(it.Current));
                }
            });
        #else
            return new EzFormModelsIterator(
                _domain.FormModels(
                ),
                _domain,
                _profile
            );
        #endif
        }

        public Gs2.Unity.Gs2Formation.Domain.Model.EzFormModelDomain FormModel(
            string formModelName
        ) {
            return new Gs2.Unity.Gs2Formation.Domain.Model.EzFormModelDomain(
                _domain.FormModel(
                    formModelName
                ),
                _profile
            );
        }

        public Gs2.Unity.Gs2Formation.Domain.Model.EzUserDomain User(
            string userId
        ) {
            return new Gs2.Unity.Gs2Formation.Domain.Model.EzUserDomain(
                _domain.User(
                    userId
                ),
                _profile
            );
        }

        public EzUserGameSessionDomain Me(
            Gs2.Unity.Util.GameSession gameSession
        ) {
            return new EzUserGameSessionDomain(
                _domain.AccessToken(
                    gameSession.AccessToken
                ),
                _profile
            );
        }

    }
}
