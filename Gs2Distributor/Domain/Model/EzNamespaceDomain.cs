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
using Gs2.Gs2Distributor.Domain.Iterator;
using Gs2.Gs2Distributor.Request;
using Gs2.Gs2Distributor.Result;
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

namespace Gs2.Unity.Gs2Distributor.Domain.Model
{

    public partial class EzNamespaceDomain {
        private readonly Gs2.Gs2Distributor.Domain.Model.NamespaceDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public string Status => _domain.Status;
        public string Result => _domain.Result;
        public string ContextStack => _domain.ContextStack;
        public string[] TaskResults => _domain.TaskResults;
        public string SheetResult => _domain.SheetResult;
        public string NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;

        public EzNamespaceDomain(
            Gs2.Gs2Distributor.Domain.Model.NamespaceDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        public class EzDistributorModelsIterator : Gs2Iterator<Gs2.Unity.Gs2Distributor.Model.EzDistributorModel>
        {
            private readonly Gs2Iterator<Gs2.Gs2Distributor.Model.DistributorModel> _it;

            public EzDistributorModelsIterator(
                Gs2Iterator<Gs2.Gs2Distributor.Model.DistributorModel> it
            )
            {
                _it = it;
            }

            public override bool HasNext()
            {
                return _it.HasNext();
            }

            protected override IEnumerator Next(Action<Gs2.Unity.Gs2Distributor.Model.EzDistributorModel> callback)
            {
                yield return _it.Next();
                callback.Invoke(_it.Current == null ? null : Gs2.Unity.Gs2Distributor.Model.EzDistributorModel.FromModel(_it.Current));
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Distributor.Model.EzDistributorModel> DistributorModels(
        )
        {
            return new EzDistributorModelsIterator(_domain.DistributorModels(
            ));
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Distributor.Model.EzDistributorModel> DistributorModelsAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Distributor.Model.EzDistributorModel> DistributorModels(
        #endif
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Distributor.Model.EzDistributorModel>(async (writer, token) =>
            {
                var it = _domain.DistributorModelsAsync(
                ).GetAsyncEnumerator();
                while(await it.MoveNextAsync())
                {
                    await writer.YieldAsync(Gs2.Unity.Gs2Distributor.Model.EzDistributorModel.FromModel(it.Current));
                }
            });
        #else
            return new EzDistributorModelsIterator(_domain.DistributorModels(
            ));
        #endif
        }

        public Gs2.Unity.Gs2Distributor.Domain.Model.EzDistributorModelDomain DistributorModel(
            string distributorName
        ) {
            return new Gs2.Unity.Gs2Distributor.Domain.Model.EzDistributorModelDomain(
                _domain.DistributorModel(
                    distributorName
                ),
                _profile
            );
        }

    }
}
