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
 *
 * deny overwrite
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
using Gs2.Gs2Inventory.Domain.Iterator;
using Gs2.Gs2Inventory.Request;
using Gs2.Gs2Inventory.Result;
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

namespace Gs2.Unity.Gs2Inventory.Domain.Model
{

    public partial class EzItemSetGameSessionDomain {
        private readonly Gs2.Gs2Inventory.Domain.Model.ItemSetAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.GameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string Body => _domain.Body;
        public string Signature => _domain.Signature;
        public long? OverflowCount => _domain.OverflowCount;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;
        public string InventoryName => _domain?.InventoryName;
        public string ItemName => _domain?.ItemName;
        public string ItemSetName => _domain?.ItemSetName;

        public EzItemSetGameSessionDomain(
            Gs2.Gs2Inventory.Domain.Model.ItemSetAccessTokenDomain domain,
            Gs2.Unity.Util.GameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }

        [Obsolete("The name has been changed to GetItemWithSignatureFuture.")]
        public IFuture<Gs2.Unity.Gs2Inventory.Domain.Model.EzItemSetGameSessionDomain> GetItemWithSignature(
            string keyId = null
        )
        {
            return GetItemWithSignatureFuture(
                keyId
            );
        }

        public IFuture<Gs2.Unity.Gs2Inventory.Domain.Model.EzItemSetGameSessionDomain> GetItemWithSignatureFuture(
            string keyId = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Inventory.Domain.Model.EzItemSetGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.GetItemWithSignatureFuture(
                        new GetItemWithSignatureRequest()
                            .WithKeyId(keyId)
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Inventory.Domain.Model.EzItemSetGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Inventory.Domain.Model.EzItemSetGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Inventory.Domain.Model.EzItemSetGameSessionDomain> GetItemWithSignatureAsync(
            string keyId = null
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.GetItemWithSignatureAsync(
                    new GetItemWithSignatureRequest()
                        .WithKeyId(keyId)
                )
            );
            return new Gs2.Unity.Gs2Inventory.Domain.Model.EzItemSetGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        [Obsolete("The name has been changed to ConsumeFuture.")]
        public IFuture<Gs2.Unity.Gs2Inventory.Domain.Model.EzItemSetGameSessionDomain> Consume(
            long consumeCount
        )
        {
            return ConsumeFuture(
                consumeCount
            );
        }

        public IFuture<Gs2.Unity.Gs2Inventory.Domain.Model.EzItemSetGameSessionDomain> ConsumeFuture(
            long consumeCount
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Inventory.Domain.Model.EzItemSetGameSessionDomain> self)
            {
                var future = this._connection.RunFuture(
                    this._gameSession,
                    () => this._domain.ConsumeFuture(
                        new ConsumeItemSetRequest()
                            .WithConsumeCount(consumeCount)
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Inventory.Domain.Model.EzItemSetGameSessionDomain(
                    future.Result,
                    this._gameSession,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Inventory.Domain.Model.EzItemSetGameSessionDomain>(Impl);
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Inventory.Domain.Model.EzItemSetGameSessionDomain> ConsumeAsync(
            long consumeCount
        ) {
            var result = await this._connection.RunAsync(
                this._gameSession,
                () => this._domain.ConsumeAsync(
                    new ConsumeItemSetRequest()
                        .WithConsumeCount(consumeCount)
                )
            );
            return new Gs2.Unity.Gs2Inventory.Domain.Model.EzItemSetGameSessionDomain(
                result,
                this._gameSession,
                this._connection
            );
        }
        #endif

        public Gs2.Unity.Gs2Inventory.Domain.Model.EzReferenceOfGameSessionDomain ReferenceOf(
            string referenceOf
        ) {
            return new Gs2.Unity.Gs2Inventory.Domain.Model.EzReferenceOfGameSessionDomain(
                _domain.ReferenceOf(
                    referenceOf
                ),
                this._gameSession,
                this._connection
            );
        }

        [Obsolete("The name has been changed to ModelFuture.")]
        public IFuture<Gs2.Unity.Gs2Inventory.Model.EzItemSet[]> Model()
        {
            return ModelFuture();
        }

        #if GS2_ENABLE_UNITASK
        public async UniTask<Gs2.Unity.Gs2Inventory.Model.EzItemSet[]> ModelAsync()
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
            return item.Select(Gs2.Unity.Gs2Inventory.Model.EzItemSet.FromModel).ToArray();
        }
        #endif

        public IFuture<Gs2.Unity.Gs2Inventory.Model.EzItemSet[]> ModelFuture()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Inventory.Model.EzItemSet[]> self)
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
                self.OnComplete(item.Select(Gs2.Unity.Gs2Inventory.Model.EzItemSet.FromModel).ToArray());
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Inventory.Model.EzItemSet[]>(Impl);
        }

        public ulong Subscribe(Action<Gs2.Unity.Gs2Inventory.Model.EzItemSet> callback)
        {
            return this._domain.Subscribe(item => {
                callback.Invoke(Gs2.Unity.Gs2Inventory.Model.EzItemSet.FromModel(
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
