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
using Gs2.Gs2Exchange.Domain.Iterator;
using Gs2.Gs2Exchange.Request;
using Gs2.Gs2Exchange.Result;
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

namespace Gs2.Unity.Gs2Exchange.Domain.Model
{

    public partial class EzNamespaceDomain {
        private readonly Gs2.Gs2Exchange.Domain.Model.NamespaceDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public string Status => _domain.Status;
        public string NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;

        public EzNamespaceDomain(
            Gs2.Gs2Exchange.Domain.Model.NamespaceDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        public class EzRateModelsIterator : Gs2Iterator<Gs2.Unity.Gs2Exchange.Model.EzRateModel>
        {
            private Gs2Iterator<Gs2.Gs2Exchange.Model.RateModel> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly Gs2.Gs2Exchange.Domain.Model.NamespaceDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzRateModelsIterator(
                Gs2Iterator<Gs2.Gs2Exchange.Model.RateModel> it,
        #if !GS2_ENABLE_UNITASK
                Gs2.Gs2Exchange.Domain.Model.NamespaceDomain domain,
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

            protected override IEnumerator Next(Action<AsyncResult<Gs2.Unity.Gs2Exchange.Model.EzRateModel>> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    null,
                    _it,
                    () =>
                    {
                        return _it = _domain.RateModels(
                        );
                    }
                );
        #endif
                callback.Invoke(
                    new AsyncResult<Gs2.Unity.Gs2Exchange.Model.EzRateModel>(
                        _it.Current == null ? null : Gs2.Unity.Gs2Exchange.Model.EzRateModel.FromModel(_it.Current),
                        _it.Error
                    )
                );
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Exchange.Model.EzRateModel> RateModels(
        )
        {
            return new EzRateModelsIterator(
                _domain.RateModels(
                ),
                _profile
            );
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Exchange.Model.EzRateModel> RateModelsAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Exchange.Model.EzRateModel> RateModels(
        #endif
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Exchange.Model.EzRateModel>(async (writer, token) =>
            {
                var it = _domain.RateModelsAsync(
                ).GetAsyncEnumerator();
                while(
                    await _profile.RunIteratorAsync(
                        null,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.RateModelsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Exchange.Model.EzRateModel.FromModel(it.Current));
                }
            });
        #else
            return new EzRateModelsIterator(
                _domain.RateModels(
                ),
                _domain,
                _profile
            );
        #endif
        }

        public Gs2.Unity.Gs2Exchange.Domain.Model.EzRateModelDomain RateModel(
            string rateName
        ) {
            return new Gs2.Unity.Gs2Exchange.Domain.Model.EzRateModelDomain(
                _domain.RateModel(
                    rateName
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

        public class EzIncrementalRateModelsIterator : Gs2Iterator<Gs2.Unity.Gs2Exchange.Model.EzIncrementalRateModel>
        {
            private Gs2Iterator<Gs2.Gs2Exchange.Model.IncrementalRateModel> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly Gs2.Gs2Exchange.Domain.Model.NamespaceDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzIncrementalRateModelsIterator(
                Gs2Iterator<Gs2.Gs2Exchange.Model.IncrementalRateModel> it,
        #if !GS2_ENABLE_UNITASK
                Gs2.Gs2Exchange.Domain.Model.NamespaceDomain domain,
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

            protected override IEnumerator Next(Action<AsyncResult<Gs2.Unity.Gs2Exchange.Model.EzIncrementalRateModel>> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    null,
                    _it,
                    () =>
                    {
                        return _it = _domain.IncrementalRateModels(
                        );
                    }
                );
        #endif
                callback.Invoke(
                    new AsyncResult<Gs2.Unity.Gs2Exchange.Model.EzIncrementalRateModel>(
                        _it.Current == null ? null : Gs2.Unity.Gs2Exchange.Model.EzIncrementalRateModel.FromModel(_it.Current),
                        _it.Error
                    )
                );
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Exchange.Model.EzIncrementalRateModel> IncrementalRateModels(
        )
        {
            return new EzIncrementalRateModelsIterator(
                _domain.IncrementalRateModels(
                ),
                _profile
            );
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Exchange.Model.EzIncrementalRateModel> IncrementalRateModelsAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Exchange.Model.EzIncrementalRateModel> IncrementalRateModels(
        #endif
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Exchange.Model.EzIncrementalRateModel>(async (writer, token) =>
            {
                var it = _domain.IncrementalRateModelsAsync(
                ).GetAsyncEnumerator();
                while(
                    await _profile.RunIteratorAsync(
                        null,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.IncrementalRateModelsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Exchange.Model.EzIncrementalRateModel.FromModel(it.Current));
                }
            });
        #else
            return new EzIncrementalRateModelsIterator(
                _domain.IncrementalRateModels(
                ),
                _domain,
                _profile
            );
        #endif
        }

        public Gs2.Unity.Gs2Exchange.Domain.Model.EzIncrementalRateModelDomain IncrementalRateModel(
            string rateName
        ) {
            return new Gs2.Unity.Gs2Exchange.Domain.Model.EzIncrementalRateModelDomain(
                _domain.IncrementalRateModel(
                    rateName
                ),
                _profile
            );
        }

    }
}
