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
using Gs2.Gs2Mission.Domain.Iterator;
using Gs2.Gs2Mission.Request;
using Gs2.Gs2Mission.Result;
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

namespace Gs2.Unity.Gs2Mission.Domain.Model
{

    public partial class EzNamespaceDomain {
        private readonly Gs2.Gs2Mission.Domain.Model.NamespaceDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public string Status => _domain.Status;
        public string Url => _domain.Url;
        public string UploadToken => _domain.UploadToken;
        public string UploadUrl => _domain.UploadUrl;
        public string NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;

        public EzNamespaceDomain(
            Gs2.Gs2Mission.Domain.Model.NamespaceDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        public class EzMissionGroupModelsIterator : Gs2Iterator<Gs2.Unity.Gs2Mission.Model.EzMissionGroupModel>
        {
            private Gs2Iterator<Gs2.Gs2Mission.Model.MissionGroupModel> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly Gs2.Gs2Mission.Domain.Model.NamespaceDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzMissionGroupModelsIterator(
                Gs2Iterator<Gs2.Gs2Mission.Model.MissionGroupModel> it,
        #if !GS2_ENABLE_UNITASK
                Gs2.Gs2Mission.Domain.Model.NamespaceDomain domain,
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

            protected override IEnumerator Next(Action<AsyncResult<Gs2.Unity.Gs2Mission.Model.EzMissionGroupModel>> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    null,
                    _it,
                    () =>
                    {
                        return _it = _domain.MissionGroupModels(
                        );
                    }
                );
        #endif
                callback.Invoke(
                    new AsyncResult<Gs2.Unity.Gs2Mission.Model.EzMissionGroupModel>(
                        _it.Current == null ? null : Gs2.Unity.Gs2Mission.Model.EzMissionGroupModel.FromModel(_it.Current),
                        _it.Error
                    )
                );
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Mission.Model.EzMissionGroupModel> MissionGroupModels(
        )
        {
            return new EzMissionGroupModelsIterator(
                _domain.MissionGroupModels(
                ),
                _profile
            );
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Mission.Model.EzMissionGroupModel> MissionGroupModelsAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Mission.Model.EzMissionGroupModel> MissionGroupModels(
        #endif
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Mission.Model.EzMissionGroupModel>(async (writer, token) =>
            {
                var it = _domain.MissionGroupModelsAsync(
                ).GetAsyncEnumerator();
                while(
                    await _profile.RunIteratorAsync(
                        null,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.MissionGroupModelsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Mission.Model.EzMissionGroupModel.FromModel(it.Current));
                }
            });
        #else
            return new EzMissionGroupModelsIterator(
                _domain.MissionGroupModels(
                ),
                _domain,
                _profile
            );
        #endif
        }

        public ulong SubscribeMissionGroupModels(Action callback) {
            return this._domain.SubscribeMissionGroupModels(callback);
        }

        public void UnsubscribeMissionGroupModels(ulong callbackId) {
            this._domain.UnsubscribeMissionGroupModels(callbackId);
        }

        public Gs2.Unity.Gs2Mission.Domain.Model.EzMissionGroupModelDomain MissionGroupModel(
            string missionGroupName
        ) {
            return new Gs2.Unity.Gs2Mission.Domain.Model.EzMissionGroupModelDomain(
                _domain.MissionGroupModel(
                    missionGroupName
                ),
                _profile
            );
        }

        public class EzCounterModelsIterator : Gs2Iterator<Gs2.Unity.Gs2Mission.Model.EzCounterModel>
        {
            private Gs2Iterator<Gs2.Gs2Mission.Model.CounterModel> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly Gs2.Gs2Mission.Domain.Model.NamespaceDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzCounterModelsIterator(
                Gs2Iterator<Gs2.Gs2Mission.Model.CounterModel> it,
        #if !GS2_ENABLE_UNITASK
                Gs2.Gs2Mission.Domain.Model.NamespaceDomain domain,
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

            protected override IEnumerator Next(Action<AsyncResult<Gs2.Unity.Gs2Mission.Model.EzCounterModel>> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    null,
                    _it,
                    () =>
                    {
                        return _it = _domain.CounterModels(
                        );
                    }
                );
        #endif
                callback.Invoke(
                    new AsyncResult<Gs2.Unity.Gs2Mission.Model.EzCounterModel>(
                        _it.Current == null ? null : Gs2.Unity.Gs2Mission.Model.EzCounterModel.FromModel(_it.Current),
                        _it.Error
                    )
                );
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Mission.Model.EzCounterModel> CounterModels(
        )
        {
            return new EzCounterModelsIterator(
                _domain.CounterModels(
                ),
                _profile
            );
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Mission.Model.EzCounterModel> CounterModelsAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Mission.Model.EzCounterModel> CounterModels(
        #endif
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Mission.Model.EzCounterModel>(async (writer, token) =>
            {
                var it = _domain.CounterModelsAsync(
                ).GetAsyncEnumerator();
                while(
                    await _profile.RunIteratorAsync(
                        null,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.CounterModelsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Mission.Model.EzCounterModel.FromModel(it.Current));
                }
            });
        #else
            return new EzCounterModelsIterator(
                _domain.CounterModels(
                ),
                _domain,
                _profile
            );
        #endif
        }

        public ulong SubscribeCounterModels(Action callback) {
            return this._domain.SubscribeCounterModels(callback);
        }

        public void UnsubscribeCounterModels(ulong callbackId) {
            this._domain.UnsubscribeCounterModels(callbackId);
        }

        public Gs2.Unity.Gs2Mission.Domain.Model.EzCounterModelDomain CounterModel(
            string counterName
        ) {
            return new Gs2.Unity.Gs2Mission.Domain.Model.EzCounterModelDomain(
                _domain.CounterModel(
                    counterName
                ),
                _profile
            );
        }

        public Gs2.Unity.Gs2Mission.Domain.Model.EzUserDomain User(
            string userId
        ) {
            return new Gs2.Unity.Gs2Mission.Domain.Model.EzUserDomain(
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
