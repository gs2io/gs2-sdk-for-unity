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
using Gs2.Gs2StateMachine.Domain.Iterator;
using Gs2.Gs2StateMachine.Request;
using Gs2.Gs2StateMachine.Result;
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

namespace Gs2.Unity.Gs2StateMachine.Domain.Model
{

    public partial class EzStatusGameSessionDomain {
        private readonly Gs2.Gs2StateMachine.Domain.Model.StatusAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.GameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;
        public string StatusName => _domain?.StatusName;

        public EzStatusGameSessionDomain(
            Gs2.Gs2StateMachine.Domain.Model.StatusAccessTokenDomain domain,
            Gs2.Unity.Util.GameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }

        [Obsolete("The name has been changed to EmitFuture.")]
        public IFuture<Gs2.Unity.Gs2StateMachine.Domain.Model.EzStatusGameSessionDomain> Emit(
            string eventName,
            string? args = null
        )
        {
            return EmitFuture(
                eventName,
                args
            );
        }

        public IFuture<Gs2.Unity.Gs2StateMachine.Domain.Model.EzStatusGameSessionDomain> EmitFuture(
            string eventName,
            string? args = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2StateMachine.Domain.Model.EzStatusGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.EmitFuture(
                        new EmitRequest()
                            .WithEventName(eventName)
                            .WithArgs(args)
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2StateMachine.Domain.Model.EzStatusGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2StateMachine.Domain.Model.EzStatusGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2StateMachine.Domain.Model.EzStatusGameSessionDomain> EmitAsync(
            string eventName,
            string? args = null
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.EmitAsync(
                    new EmitRequest()
                        .WithEventName(eventName)
                        .WithArgs(args)
                )
            );
            return new Gs2.Unity.Gs2StateMachine.Domain.Model.EzStatusGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        [Obsolete("The name has been changed to ExitFuture.")]
        public IFuture<Gs2.Unity.Gs2StateMachine.Domain.Model.EzStatusGameSessionDomain> Exit(
        )
        {
            return ExitFuture(
            );
        }

        public IFuture<Gs2.Unity.Gs2StateMachine.Domain.Model.EzStatusGameSessionDomain> ExitFuture(
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2StateMachine.Domain.Model.EzStatusGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.ExitStateMachineFuture(
                        new ExitStateMachineRequest()
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2StateMachine.Domain.Model.EzStatusGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2StateMachine.Domain.Model.EzStatusGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2StateMachine.Domain.Model.EzStatusGameSessionDomain> ExitAsync(
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.ExitStateMachineAsync(
                    new ExitStateMachineRequest()
                )
            );
            return new Gs2.Unity.Gs2StateMachine.Domain.Model.EzStatusGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        [Obsolete("The name has been changed to ModelFuture.")]
        public IFuture<Gs2.Unity.Gs2StateMachine.Model.EzStatus> Model()
        {
            return ModelFuture();
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2StateMachine.Model.EzStatus> ModelAsync()
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
            return Gs2.Unity.Gs2StateMachine.Model.EzStatus.FromModel(
                item
            );
        }
        #endif

        public IFuture<Gs2.Unity.Gs2StateMachine.Model.EzStatus> ModelFuture()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2StateMachine.Model.EzStatus> self)
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
                self.OnComplete(Gs2.Unity.Gs2StateMachine.Model.EzStatus.FromModel(
                    item
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2StateMachine.Model.EzStatus>(Impl);
        }

        public ulong Subscribe(Action<Gs2.Unity.Gs2StateMachine.Model.EzStatus> callback)
        {
            return this._domain.Subscribe(item => {
                callback.Invoke(Gs2.Unity.Gs2StateMachine.Model.EzStatus.FromModel(
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
