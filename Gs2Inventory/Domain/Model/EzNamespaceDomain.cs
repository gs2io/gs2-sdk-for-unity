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
#pragma warning disable CS0169, CS0168

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

    public partial class EzNamespaceDomain {
        private readonly Gs2.Gs2Inventory.Domain.Model.NamespaceDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public string Status => _domain.Status;
        public string Url => _domain.Url;
        public string UploadToken => _domain.UploadToken;
        public string UploadUrl => _domain.UploadUrl;
        public string NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;

        public EzNamespaceDomain(
            Gs2.Gs2Inventory.Domain.Model.NamespaceDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        public class EzInventoryModelsIterator : Gs2Iterator<Gs2.Unity.Gs2Inventory.Model.EzInventoryModel>
        {
            private Gs2Iterator<Gs2.Gs2Inventory.Model.InventoryModel> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly Gs2.Gs2Inventory.Domain.Model.NamespaceDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzInventoryModelsIterator(
                Gs2Iterator<Gs2.Gs2Inventory.Model.InventoryModel> it,
        #if !GS2_ENABLE_UNITASK
                Gs2.Gs2Inventory.Domain.Model.NamespaceDomain domain,
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

            protected override IEnumerator Next(Action<AsyncResult<Gs2.Unity.Gs2Inventory.Model.EzInventoryModel>> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    null,
                    _it,
                    () =>
                    {
                        return _it = _domain.InventoryModels(
                        );
                    }
                );
        #endif
                callback.Invoke(
                    new AsyncResult<Gs2.Unity.Gs2Inventory.Model.EzInventoryModel>(
                        _it.Current == null ? null : Gs2.Unity.Gs2Inventory.Model.EzInventoryModel.FromModel(_it.Current),
                        _it.Error
                    )
                );
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Inventory.Model.EzInventoryModel> InventoryModels(
        )
        {
            return new EzInventoryModelsIterator(
                _domain.InventoryModels(
                ),
                _profile
            );
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Inventory.Model.EzInventoryModel> InventoryModelsAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Inventory.Model.EzInventoryModel> InventoryModels(
        #endif
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Inventory.Model.EzInventoryModel>(async (writer, token) =>
            {
                var it = _domain.InventoryModelsAsync(
                ).GetAsyncEnumerator();
                while(
                    await _profile.RunIteratorAsync(
                        null,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.InventoryModelsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Inventory.Model.EzInventoryModel.FromModel(it.Current));
                }
            });
        #else
            return new EzInventoryModelsIterator(
                _domain.InventoryModels(
                ),
                _domain,
                _profile
            );
        #endif
        }

        public ulong SubscribeInventoryModels(Action callback) {
            return this._domain.SubscribeInventoryModels(callback);
        }

        public void UnsubscribeInventoryModels(ulong callbackId) {
            this._domain.UnsubscribeInventoryModels(callbackId);
        }

        public Gs2.Unity.Gs2Inventory.Domain.Model.EzInventoryModelDomain InventoryModel(
            string inventoryName
        ) {
            return new Gs2.Unity.Gs2Inventory.Domain.Model.EzInventoryModelDomain(
                _domain.InventoryModel(
                    inventoryName
                ),
                _profile
            );
        }

        public Gs2.Unity.Gs2Inventory.Domain.Model.EzUserDomain User(
            string userId
        ) {
            return new Gs2.Unity.Gs2Inventory.Domain.Model.EzUserDomain(
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

        public class EzSimpleInventoryModelsIterator : Gs2Iterator<Gs2.Unity.Gs2Inventory.Model.EzSimpleInventoryModel>
        {
            private Gs2Iterator<Gs2.Gs2Inventory.Model.SimpleInventoryModel> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly Gs2.Gs2Inventory.Domain.Model.NamespaceDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzSimpleInventoryModelsIterator(
                Gs2Iterator<Gs2.Gs2Inventory.Model.SimpleInventoryModel> it,
        #if !GS2_ENABLE_UNITASK
                Gs2.Gs2Inventory.Domain.Model.NamespaceDomain domain,
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

            protected override IEnumerator Next(Action<AsyncResult<Gs2.Unity.Gs2Inventory.Model.EzSimpleInventoryModel>> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    null,
                    _it,
                    () =>
                    {
                        return _it = _domain.SimpleInventoryModels(
                        );
                    }
                );
        #endif
                callback.Invoke(
                    new AsyncResult<Gs2.Unity.Gs2Inventory.Model.EzSimpleInventoryModel>(
                        _it.Current == null ? null : Gs2.Unity.Gs2Inventory.Model.EzSimpleInventoryModel.FromModel(_it.Current),
                        _it.Error
                    )
                );
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Inventory.Model.EzSimpleInventoryModel> SimpleInventoryModels(
        )
        {
            return new EzSimpleInventoryModelsIterator(
                _domain.SimpleInventoryModels(
                ),
                _profile
            );
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Inventory.Model.EzSimpleInventoryModel> SimpleInventoryModelsAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Inventory.Model.EzSimpleInventoryModel> SimpleInventoryModels(
        #endif
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Inventory.Model.EzSimpleInventoryModel>(async (writer, token) =>
            {
                var it = _domain.SimpleInventoryModelsAsync(
                ).GetAsyncEnumerator();
                while(
                    await _profile.RunIteratorAsync(
                        null,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.SimpleInventoryModelsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Inventory.Model.EzSimpleInventoryModel.FromModel(it.Current));
                }
            });
        #else
            return new EzSimpleInventoryModelsIterator(
                _domain.SimpleInventoryModels(
                ),
                _domain,
                _profile
            );
        #endif
        }

        public ulong SubscribeSimpleInventoryModels(Action callback) {
            return this._domain.SubscribeSimpleInventoryModels(callback);
        }

        public void UnsubscribeSimpleInventoryModels(ulong callbackId) {
            this._domain.UnsubscribeSimpleInventoryModels(callbackId);
        }

        public Gs2.Unity.Gs2Inventory.Domain.Model.EzSimpleInventoryModelDomain SimpleInventoryModel(
            string inventoryName
        ) {
            return new Gs2.Unity.Gs2Inventory.Domain.Model.EzSimpleInventoryModelDomain(
                _domain.SimpleInventoryModel(
                    inventoryName
                ),
                _profile
            );
        }

        public class EzBigInventoryModelsIterator : Gs2Iterator<Gs2.Unity.Gs2Inventory.Model.EzBigInventoryModel>
        {
            private Gs2Iterator<Gs2.Gs2Inventory.Model.BigInventoryModel> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly Gs2.Gs2Inventory.Domain.Model.NamespaceDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzBigInventoryModelsIterator(
                Gs2Iterator<Gs2.Gs2Inventory.Model.BigInventoryModel> it,
        #if !GS2_ENABLE_UNITASK
                Gs2.Gs2Inventory.Domain.Model.NamespaceDomain domain,
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

            protected override IEnumerator Next(Action<AsyncResult<Gs2.Unity.Gs2Inventory.Model.EzBigInventoryModel>> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    null,
                    _it,
                    () =>
                    {
                        return _it = _domain.BigInventoryModels(
                        );
                    }
                );
        #endif
                callback.Invoke(
                    new AsyncResult<Gs2.Unity.Gs2Inventory.Model.EzBigInventoryModel>(
                        _it.Current == null ? null : Gs2.Unity.Gs2Inventory.Model.EzBigInventoryModel.FromModel(_it.Current),
                        _it.Error
                    )
                );
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Inventory.Model.EzBigInventoryModel> BigInventoryModels(
        )
        {
            return new EzBigInventoryModelsIterator(
                _domain.BigInventoryModels(
                ),
                _profile
            );
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Inventory.Model.EzBigInventoryModel> BigInventoryModelsAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Inventory.Model.EzBigInventoryModel> BigInventoryModels(
        #endif
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Inventory.Model.EzBigInventoryModel>(async (writer, token) =>
            {
                var it = _domain.BigInventoryModelsAsync(
                ).GetAsyncEnumerator();
                while(
                    await _profile.RunIteratorAsync(
                        null,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.BigInventoryModelsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Inventory.Model.EzBigInventoryModel.FromModel(it.Current));
                }
            });
        #else
            return new EzBigInventoryModelsIterator(
                _domain.BigInventoryModels(
                ),
                _domain,
                _profile
            );
        #endif
        }

        public ulong SubscribeBigInventoryModels(Action callback) {
            return this._domain.SubscribeBigInventoryModels(callback);
        }

        public void UnsubscribeBigInventoryModels(ulong callbackId) {
            this._domain.UnsubscribeBigInventoryModels(callbackId);
        }

        public Gs2.Unity.Gs2Inventory.Domain.Model.EzBigInventoryModelDomain BigInventoryModel(
            string inventoryName
        ) {
            return new Gs2.Unity.Gs2Inventory.Domain.Model.EzBigInventoryModelDomain(
                _domain.BigInventoryModel(
                    inventoryName
                ),
                _profile
            );
        }

    }
}
