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

    public partial class EzMissionGroupModelDomain {
        private readonly Gs2.Gs2Mission.Domain.Model.MissionGroupModelDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public string NamespaceName => _domain?.NamespaceName;
        public string MissionGroupName => _domain?.MissionGroupName;

        public EzMissionGroupModelDomain(
            Gs2.Gs2Mission.Domain.Model.MissionGroupModelDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        public class EzMissionTaskModelsIterator : Gs2Iterator<Gs2.Unity.Gs2Mission.Model.EzMissionTaskModel>
        {
            private Gs2Iterator<Gs2.Gs2Mission.Model.MissionTaskModel> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly Gs2.Gs2Mission.Domain.Model.MissionGroupModelDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzMissionTaskModelsIterator(
                Gs2Iterator<Gs2.Gs2Mission.Model.MissionTaskModel> it,
        #if !GS2_ENABLE_UNITASK
                Gs2.Gs2Mission.Domain.Model.MissionGroupModelDomain domain,
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

            protected override IEnumerator Next(Action<AsyncResult<Gs2.Unity.Gs2Mission.Model.EzMissionTaskModel>> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    null,
                    _it,
                    () =>
                    {
                        return _it = _domain.MissionTaskModels(
                        );
                    }
                );
        #endif
                callback.Invoke(
                    new AsyncResult<Gs2.Unity.Gs2Mission.Model.EzMissionTaskModel>(
                        _it.Current == null ? null : Gs2.Unity.Gs2Mission.Model.EzMissionTaskModel.FromModel(_it.Current),
                        _it.Error
                    )
                );
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Mission.Model.EzMissionTaskModel> MissionTaskModels(
        )
        {
            return new EzMissionTaskModelsIterator(
                _domain.MissionTaskModels(
                ),
                _profile
            );
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Mission.Model.EzMissionTaskModel> MissionTaskModelsAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Mission.Model.EzMissionTaskModel> MissionTaskModels(
        #endif
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Mission.Model.EzMissionTaskModel>(async (writer, token) =>
            {
                var it = _domain.MissionTaskModelsAsync(
                ).GetAsyncEnumerator();
                while(
                    await _profile.RunIteratorAsync(
                        null,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.MissionTaskModelsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Mission.Model.EzMissionTaskModel.FromModel(it.Current));
                }
            });
        #else
            return new EzMissionTaskModelsIterator(
                _domain.MissionTaskModels(
                ),
                _domain,
                _profile
            );
        #endif
        }

        public ulong SubscribeMissionTaskModels(Action callback) {
            return this._domain.SubscribeMissionTaskModels(callback);
        }

        public void UnsubscribeMissionTaskModels(ulong callbackId) {
            this._domain.UnsubscribeMissionTaskModels(callbackId);
        }

        public Gs2.Unity.Gs2Mission.Domain.Model.EzMissionTaskModelDomain MissionTaskModel(
            string missionTaskName
        ) {
            return new Gs2.Unity.Gs2Mission.Domain.Model.EzMissionTaskModelDomain(
                _domain.MissionTaskModel(
                    missionTaskName
                ),
                _profile
            );
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Mission.Model.EzMissionGroupModel> Model()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Mission.Model.EzMissionGroupModel> self)
            {
                yield return ModelAsync().ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Mission.Model.EzMissionGroupModel>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Mission.Model.EzMissionGroupModel> ModelAsync()
        {
            var item = await _profile.RunAsync(
                null,
                async () =>
                {
                    return await _domain.Model();
                }
            );
            if (item == null) {
                return null;
            }
            return Gs2.Unity.Gs2Mission.Model.EzMissionGroupModel.FromModel(
                item
            );
        }
        #else
        public IFuture<Gs2.Unity.Gs2Mission.Model.EzMissionGroupModel> Model()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Mission.Model.EzMissionGroupModel> self)
            {
                var future = _domain.Model();
                yield return _profile.RunFuture(
                    null,
                    future,
                    () => {
                    	return future = _domain.Model();
                    }
                );
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                var item = future.Result;
                if (item == null) {
                    self.OnComplete(null);
                    yield break;
                }
                self.OnComplete(Gs2.Unity.Gs2Mission.Model.EzMissionGroupModel.FromModel(
                    item
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Mission.Model.EzMissionGroupModel>(Impl);
        }
        #endif

        public ulong Subscribe(Action<Gs2.Unity.Gs2Mission.Model.EzMissionGroupModel> callback)
        {
            return this._domain.Subscribe(item => {
                callback.Invoke(Gs2.Unity.Gs2Mission.Model.EzMissionGroupModel.FromModel(
                    item
                ));
            });
        }

        public void Unsubscribe(ulong callbackId)
        {
            this._domain.Unsubscribe(callbackId);
        }

    }
}
