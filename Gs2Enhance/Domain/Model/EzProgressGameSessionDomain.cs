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
using Gs2.Gs2Enhance.Domain.Iterator;
using Gs2.Gs2Enhance.Request;
using Gs2.Gs2Enhance.Result;
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

namespace Gs2.Unity.Gs2Enhance.Domain.Model
{

    public partial class EzProgressGameSessionDomain {
        private readonly Gs2.Gs2Enhance.Domain.Model.ProgressAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.IGameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public long? AcquireExperience => _domain.AcquireExperience;
        public float? BonusRate => _domain.BonusRate;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzProgressGameSessionDomain(
            Gs2.Gs2Enhance.Domain.Model.ProgressAccessTokenDomain domain,
            Gs2.Unity.Util.IGameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }

        [Obsolete("The name has been changed to StartFuture.")]
        public IFuture<Gs2.Unity.Core.Domain.EzTransactionDomain> Start(
            string rateName,
            string targetItemSetId,
            Gs2.Unity.Gs2Enhance.Model.EzMaterial[] materials = null,
            bool? force = null,
            Gs2.Unity.Gs2Enhance.Model.EzConfig[] config = null,
            bool speculativeExecute = true
        )
        {
            return StartFuture(
                rateName,
                targetItemSetId,
                materials,
                force,
                config,
                speculativeExecute
            );
        }

        public IFuture<Gs2.Unity.Core.Domain.EzTransactionDomain> StartFuture(
            string rateName,
            string targetItemSetId,
            Gs2.Unity.Gs2Enhance.Model.EzMaterial[] materials = null,
            bool? force = null,
            Gs2.Unity.Gs2Enhance.Model.EzConfig[] config = null,
            bool speculativeExecute = true
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Core.Domain.EzTransactionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.StartFuture(
                        new StartRequest()
                            .WithRateName(rateName)
                            .WithTargetItemSetId(targetItemSetId)
                            .WithMaterials(materials?.Select(v => v.ToModel()).ToArray())
                            .WithForce(force)
                            .WithConfig(config?.Select(v => v.ToModel()).ToArray()),
                        speculativeExecute
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(future.Result == null ? null : new Gs2.Unity.Core.Domain.EzTransactionDomain(future.Result));
            }
            return new Gs2InlineFuture<Gs2.Unity.Core.Domain.EzTransactionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Core.Domain.EzTransactionDomain> StartAsync(
            string rateName,
            string targetItemSetId,
            Gs2.Unity.Gs2Enhance.Model.EzMaterial[] materials = null,
            bool? force = null,
            Gs2.Unity.Gs2Enhance.Model.EzConfig[] config = null,
            bool speculativeExecute = true
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.StartAsync(
                    new StartRequest()
                        .WithRateName(rateName)
                        .WithTargetItemSetId(targetItemSetId)
                        .WithMaterials(materials?.Select(v => v.ToModel()).ToArray())
                        .WithForce(force)
                        .WithConfig(config?.Select(v => v.ToModel()).ToArray()),
                    speculativeExecute
                )
            );
            return result == null ? null : new Gs2.Unity.Core.Domain.EzTransactionDomain(result);
        }
        #endif

        [Obsolete("The name has been changed to EndFuture.")]
        public IFuture<Gs2.Unity.Core.Domain.EzTransactionDomain> End(
            Gs2.Unity.Gs2Enhance.Model.EzConfig[] config = null,
            bool speculativeExecute = true
        )
        {
            return EndFuture(
                config,
                speculativeExecute
            );
        }

        public IFuture<Gs2.Unity.Core.Domain.EzTransactionDomain> EndFuture(
            Gs2.Unity.Gs2Enhance.Model.EzConfig[] config = null,
            bool speculativeExecute = true
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Core.Domain.EzTransactionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.EndFuture(
                        new EndRequest()
                            .WithConfig(config?.Select(v => v.ToModel()).ToArray()),
                        speculativeExecute
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(future.Result == null ? null : new Gs2.Unity.Core.Domain.EzTransactionDomain(future.Result));
            }
            return new Gs2InlineFuture<Gs2.Unity.Core.Domain.EzTransactionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Core.Domain.EzTransactionDomain> EndAsync(
            Gs2.Unity.Gs2Enhance.Model.EzConfig[] config = null,
            bool speculativeExecute = true
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.EndAsync(
                    new EndRequest()
                        .WithConfig(config?.Select(v => v.ToModel()).ToArray()),
                    speculativeExecute
                )
            );
            return result == null ? null : new Gs2.Unity.Core.Domain.EzTransactionDomain(result);
        }
        #endif

        [Obsolete("The name has been changed to DeleteProgressFuture.")]
        public IFuture<Gs2.Unity.Gs2Enhance.Domain.Model.EzProgressGameSessionDomain> DeleteProgress(
        )
        {
            return DeleteProgressFuture(
            );
        }

        public IFuture<Gs2.Unity.Gs2Enhance.Domain.Model.EzProgressGameSessionDomain> DeleteProgressFuture(
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Enhance.Domain.Model.EzProgressGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.DeleteFuture(
                        new DeleteProgressRequest()
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Enhance.Domain.Model.EzProgressGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Enhance.Domain.Model.EzProgressGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Enhance.Domain.Model.EzProgressGameSessionDomain> DeleteProgressAsync(
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.DeleteAsync(
                    new DeleteProgressRequest()
                )
            );
            return new Gs2.Unity.Gs2Enhance.Domain.Model.EzProgressGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        [Obsolete("The name has been changed to ModelFuture.")]
        public IFuture<Gs2.Unity.Gs2Enhance.Model.EzProgress> Model()
        {
            return ModelFuture();
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Enhance.Model.EzProgress> ModelAsync()
        {
            var item = await this._connection.RunAsync(
                this._gameSession,
                async () =>
                {
                    return await _domain.ModelAsync();
                }
            );
            if (item == null) {
                return null;
            }
            return Gs2.Unity.Gs2Enhance.Model.EzProgress.FromModel(
                item
            );
        }
        #endif

        public IFuture<Gs2.Unity.Gs2Enhance.Model.EzProgress> ModelFuture()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Enhance.Model.EzProgress> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => {
                    	return _domain.ModelFuture();
                    }
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                var item = future.Result;
                if (item == null) {
                    self.OnComplete(null);
                    yield break;
                }
                self.OnComplete(Gs2.Unity.Gs2Enhance.Model.EzProgress.FromModel(
                    item
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Enhance.Model.EzProgress>(Impl);
        }

        public void Invalidate()
        {
            this._domain.Invalidate();
        }

        public ulong Subscribe(Action<Gs2.Unity.Gs2Enhance.Model.EzProgress> callback)
        {
            return this._domain.Subscribe(item => {
                callback.Invoke(item == null ? null : Gs2.Unity.Gs2Enhance.Model.EzProgress.FromModel(
                    item
                ));
            });
        }

        public void Unsubscribe(ulong callbackId)
        {
            this._domain.Unsubscribe(callbackId);
        }

        #if UNITY_2017_1_OR_NEWER
        public Gs2Future<ulong> SubscribeWithInitialCallFuture(Action<Gs2.Unity.Gs2Enhance.Model.EzProgress> callback)
        {
            IEnumerator Impl(IFuture<ulong> self)
            {
                var future = ModelFuture();
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                var item = future.Result;
                var callbackId = Subscribe(callback);
                callback.Invoke(item);
                self.OnComplete(callbackId);
            }
            return new Gs2InlineFuture<ulong>(Impl);
        }
        #endif

        #if !UNITY_2017_1_OR_NEWER || GS2_ENABLE_UNITASK
            #if UNITY_2017_1_OR_NEWER
        public async UniTask<ulong> SubscribeWithInitialCallAsync(Action<Gs2.Unity.Gs2Enhance.Model.EzProgress> callback)
            #else
        public async Task<ulong> SubscribeWithInitialCallAsync(Action<Gs2.Unity.Gs2Enhance.Model.EzProgress> callback)
            #endif
        {
            var item = await ModelAsync();
            var callbackId = Subscribe(callback);
            callback.Invoke(item);
            return callbackId;
        }
        #endif

    }
}
