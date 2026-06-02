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
#pragma warning disable CS0169, CS0168

using System;
using System.Linq;
using System.Text.RegularExpressions;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Gs2Distributor.Domain.Iterator;
using Gs2.Gs2Distributor.Request;
using Gs2.Gs2Distributor.Result;
using Gs2.Gs2Auth.Model;
using Gs2.Util.LitJson;
using Gs2.Core;
using Gs2.Core.Domain;
using Gs2.Core.Util;
#if UNITY_2017_1_OR_NEWER
using UnityEngine.Scripting;
using System.Collections;
    #if GS2_ENABLE_UNITASK
using Cysharp.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using System.Collections.Generic;
    #endif
#else
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Gs2.Unity.Gs2Distributor.Domain.Model
{

    public partial class EzNamespaceDomain {
        private readonly Gs2.Gs2Distributor.Domain.Model.NamespaceDomain _domain;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string? Status => _domain.Status;
        public string? Result => _domain.Result;
        public string? ContextStack => _domain.ContextStack;
        public int? StatusCode => _domain.StatusCode;
        public int[] VerifyTaskResultCodes => _domain.VerifyTaskResultCodes;
        public string[] VerifyTaskResults => _domain.VerifyTaskResults;
        public int[] TaskResultCodes => _domain.TaskResultCodes;
        public string[] TaskResults => _domain.TaskResults;
        public int? SheetResultCode => _domain.SheetResultCode;
        public string? SheetResult => _domain.SheetResult;
        public string? NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;

        public EzNamespaceDomain(
            Gs2.Gs2Distributor.Domain.Model.NamespaceDomain domain,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            this._domain = domain;
            this._connection = connection;
        }

        #if UNITY_2017_1_OR_NEWER
        [Obsolete("The name has been changed to BatchExecuteApiFuture.")]
        public IFuture<Gs2.Unity.Gs2Distributor.Domain.Model.EzNamespaceDomain> BatchExecuteApi(
            Gs2.Unity.Gs2Distributor.Model.EzBatchRequestPayload[] requestPayloads
        )
        {
            return BatchExecuteApiFuture(
                requestPayloads
            );
        }

        public IFuture<Gs2.Unity.Gs2Distributor.Domain.Model.EzNamespaceDomain> BatchExecuteApiFuture(
            Gs2.Unity.Gs2Distributor.Model.EzBatchRequestPayload[] requestPayloads
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Distributor.Domain.Model.EzNamespaceDomain> self)
            {
                var future = this._connection.RunFuture(
                    null,
                    () => this._domain.BatchExecuteApiFuture(
                        new BatchExecuteApiRequest()
                            .WithRequestPayloads(requestPayloads?.Select(v => v.ToModel()).ToArray())
                    )
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Distributor.Domain.Model.EzNamespaceDomain(
                    future.Result,
                    this._connection
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Distributor.Domain.Model.EzNamespaceDomain>(Impl);
        }
        #endif

        #if !UNITY_2017_1_OR_NEWER || GS2_ENABLE_UNITASK
            #if UNITY_2017_1_OR_NEWER
        public async UniTask<Gs2.Unity.Gs2Distributor.Domain.Model.EzNamespaceDomain> BatchExecuteApiAsync(
            #else
        public async Task<Gs2.Unity.Gs2Distributor.Domain.Model.EzNamespaceDomain> BatchExecuteApiAsync(
            #endif
            Gs2.Unity.Gs2Distributor.Model.EzBatchRequestPayload[] requestPayloads
        ) {
            var result = await this._connection.RunAsync(
                null,
                () => this._domain.BatchExecuteApiAsync(
                    new BatchExecuteApiRequest()
                        .WithRequestPayloads(requestPayloads?.Select(v => v.ToModel()).ToArray())
                )
            );
            return new Gs2.Unity.Gs2Distributor.Domain.Model.EzNamespaceDomain(
                result,
                this._connection
            );
        }
        #endif

        #if UNITY_2017_1_OR_NEWER
        public Gs2Iterator<Gs2.Unity.Gs2Distributor.Model.EzDistributorModel> DistributorModels(
        )
        {
            return new Gs2.Unity.Gs2Distributor.Domain.Iterator.EzListDistributorModelsIterator(
                this._domain,
                this._connection
            );
        }
        #endif

        #if !UNITY_2017_1_OR_NEWER || GS2_ENABLE_UNITASK
            #if GS2_ENABLE_UNITASK
        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Distributor.Model.EzDistributorModel> DistributorModelsAsync(
        )
        {
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Distributor.Model.EzDistributorModel>(async (writer, token) =>
            {
                var it = _domain.DistributorModelsAsync(
                ).GetAsyncEnumerator();
                try
                {
                    while(
                        await this._connection.RunIteratorAsync(
                            null,
                            async () =>
                            {
                                return await it.MoveNextAsync();
                            },
                            () => {
                                it = _domain.DistributorModelsAsync(
                                ).GetAsyncEnumerator();
                            }
                        )
                    )
                    {
                        await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Distributor.Model.EzDistributorModel.FromModel(it.Current));
                    }
                }
                finally
                {
                    await it.DisposeAsync();
                }
            });
        }
            #else
        public async IAsyncEnumerable<Gs2.Unity.Gs2Distributor.Model.EzDistributorModel> DistributorModelsAsync(
        )
        {
            var it = _domain.DistributorModelsAsync(
            ).GetAsyncEnumerator();
            try
            {
                while(
                    await this._connection.RunIteratorAsync(
                        null,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.DistributorModelsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    yield return it.Current == null ? null : Gs2.Unity.Gs2Distributor.Model.EzDistributorModel.FromModel(it.Current);
                }
            }
            finally
            {
                await it.DisposeAsync();
            }
        }
            #endif
        #endif

        public ulong SubscribeDistributorModels(
            Action<Gs2.Unity.Gs2Distributor.Model.EzDistributorModel[]> callback
        ) {
            return this._domain.SubscribeDistributorModels(
                items => {
                    callback.Invoke(items.Select(Gs2.Unity.Gs2Distributor.Model.EzDistributorModel.FromModel).ToArray());
                }
            );
        }

        public void UnsubscribeDistributorModels(
            ulong callbackId
        ) {
            this._domain.UnsubscribeDistributorModels(
                callbackId
            );
        }

        public void InvalidateDistributorModels(
        ) {
            this._domain.InvalidateDistributorModels(
            );
        }

        public Gs2.Unity.Gs2Distributor.Domain.Model.EzDistributorModelDomain DistributorModel(
            string distributorName
        ) {
            return new Gs2.Unity.Gs2Distributor.Domain.Model.EzDistributorModelDomain(
                _domain.DistributorModel(
                    distributorName
                ),
                this._connection
            );
        }

        public Gs2.Unity.Gs2Distributor.Domain.Model.EzUserDomain User(
            string userId
        ) {
            return new Gs2.Unity.Gs2Distributor.Domain.Model.EzUserDomain(
                _domain.User(
                    userId
                ),
                this._connection
            );
        }

        public EzUserGameSessionDomain Me(
            Gs2.Unity.Util.IGameSession gameSession
        ) {
            return new EzUserGameSessionDomain(
                _domain.AccessToken(
                    gameSession.AccessToken
                ),
                gameSession,
                this._connection
            );
        }

    }
}
