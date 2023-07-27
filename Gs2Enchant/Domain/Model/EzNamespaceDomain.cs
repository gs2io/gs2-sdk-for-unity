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

    public partial class EzNamespaceDomain {
        private readonly Gs2.Gs2Enchant.Domain.Model.NamespaceDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public string Status => _domain.Status;
        public string NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;

        public EzNamespaceDomain(
            Gs2.Gs2Enchant.Domain.Model.NamespaceDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        public class EzBalanceParameterModelsIterator : Gs2Iterator<Gs2.Unity.Gs2Enchant.Model.EzBalanceParameterModel>
        {
            private Gs2Iterator<Gs2.Gs2Enchant.Model.BalanceParameterModel> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly Gs2.Gs2Enchant.Domain.Model.NamespaceDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzBalanceParameterModelsIterator(
                Gs2Iterator<Gs2.Gs2Enchant.Model.BalanceParameterModel> it,
        #if !GS2_ENABLE_UNITASK
                Gs2.Gs2Enchant.Domain.Model.NamespaceDomain domain,
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

            protected override IEnumerator Next(Action<AsyncResult<Gs2.Unity.Gs2Enchant.Model.EzBalanceParameterModel>> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    null,
                    _it,
                    () =>
                    {
                        return _it = _domain.BalanceParameterModels(
                        );
                    }
                );
        #endif
                callback.Invoke(
                    new AsyncResult<Gs2.Unity.Gs2Enchant.Model.EzBalanceParameterModel>(
                        _it.Current == null ? null : Gs2.Unity.Gs2Enchant.Model.EzBalanceParameterModel.FromModel(_it.Current),
                        _it.Error
                    )
                );
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Enchant.Model.EzBalanceParameterModel> BalanceParameterModels(
        )
        {
            return new EzBalanceParameterModelsIterator(
                _domain.BalanceParameterModels(
                ),
                _profile
            );
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Enchant.Model.EzBalanceParameterModel> BalanceParameterModelsAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Enchant.Model.EzBalanceParameterModel> BalanceParameterModels(
        #endif
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Enchant.Model.EzBalanceParameterModel>(async (writer, token) =>
            {
                var it = _domain.BalanceParameterModelsAsync(
                ).GetAsyncEnumerator();
                while(
                    await _profile.RunIteratorAsync(
                        null,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.BalanceParameterModelsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Enchant.Model.EzBalanceParameterModel.FromModel(it.Current));
                }
            });
        #else
            return new EzBalanceParameterModelsIterator(
                _domain.BalanceParameterModels(
                ),
                _domain,
                _profile
            );
        #endif
        }

        public Gs2.Unity.Gs2Enchant.Domain.Model.EzBalanceParameterModelDomain BalanceParameterModel(
            string parameterName
        ) {
            return new Gs2.Unity.Gs2Enchant.Domain.Model.EzBalanceParameterModelDomain(
                _domain.BalanceParameterModel(
                    parameterName
                ),
                _profile
            );
        }

        public class EzRarityParameterModelsIterator : Gs2Iterator<Gs2.Unity.Gs2Enchant.Model.EzRarityParameterModel>
        {
            private Gs2Iterator<Gs2.Gs2Enchant.Model.RarityParameterModel> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly Gs2.Gs2Enchant.Domain.Model.NamespaceDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzRarityParameterModelsIterator(
                Gs2Iterator<Gs2.Gs2Enchant.Model.RarityParameterModel> it,
        #if !GS2_ENABLE_UNITASK
                Gs2.Gs2Enchant.Domain.Model.NamespaceDomain domain,
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

            protected override IEnumerator Next(Action<AsyncResult<Gs2.Unity.Gs2Enchant.Model.EzRarityParameterModel>> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    null,
                    _it,
                    () =>
                    {
                        return _it = _domain.RarityParameterModels(
                        );
                    }
                );
        #endif
                callback.Invoke(
                    new AsyncResult<Gs2.Unity.Gs2Enchant.Model.EzRarityParameterModel>(
                        _it.Current == null ? null : Gs2.Unity.Gs2Enchant.Model.EzRarityParameterModel.FromModel(_it.Current),
                        _it.Error
                    )
                );
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Enchant.Model.EzRarityParameterModel> RarityParameterModels(
        )
        {
            return new EzRarityParameterModelsIterator(
                _domain.RarityParameterModels(
                ),
                _profile
            );
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Enchant.Model.EzRarityParameterModel> RarityParameterModelsAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Enchant.Model.EzRarityParameterModel> RarityParameterModels(
        #endif
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Enchant.Model.EzRarityParameterModel>(async (writer, token) =>
            {
                var it = _domain.RarityParameterModelsAsync(
                ).GetAsyncEnumerator();
                while(
                    await _profile.RunIteratorAsync(
                        null,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.RarityParameterModelsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Enchant.Model.EzRarityParameterModel.FromModel(it.Current));
                }
            });
        #else
            return new EzRarityParameterModelsIterator(
                _domain.RarityParameterModels(
                ),
                _domain,
                _profile
            );
        #endif
        }

        public Gs2.Unity.Gs2Enchant.Domain.Model.EzRarityParameterModelDomain RarityParameterModel(
            string parameterName
        ) {
            return new Gs2.Unity.Gs2Enchant.Domain.Model.EzRarityParameterModelDomain(
                _domain.RarityParameterModel(
                    parameterName
                ),
                _profile
            );
        }

        public Gs2.Unity.Gs2Enchant.Domain.Model.EzUserDomain User(
            string userId
        ) {
            return new Gs2.Unity.Gs2Enchant.Domain.Model.EzUserDomain(
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