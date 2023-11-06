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
using Gs2.Gs2Datastore.Domain.Iterator;
using Gs2.Gs2Datastore.Request;
using Gs2.Gs2Datastore.Result;
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

namespace Gs2.Unity.Gs2Datastore.Domain.Model
{

    public partial class EzNamespaceDomain {
        private readonly Gs2.Gs2Datastore.Domain.Model.NamespaceDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public string Status => _domain.Status;
        public string Url => _domain.Url;
        public string UploadToken => _domain.UploadToken;
        public string UploadUrl => _domain.UploadUrl;
        public string NamespaceName => _domain?.NamespaceName;

        public EzNamespaceDomain(
            Gs2.Gs2Datastore.Domain.Model.NamespaceDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        [Obsolete("The name has been changed to RestoreDataObjectFuture.")]
        public IFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectDomain> RestoreDataObject(
            string dataObjectId
        )
        {
            return RestoreDataObjectFuture(
                dataObjectId
            );
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectDomain> RestoreDataObjectFuture(
            string dataObjectId
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectDomain> self)
            {
                yield return RestoreDataObjectAsync(
                    dataObjectId
                ).ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectDomain> RestoreDataObjectAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectDomain> RestoreDataObjectFuture(
        #endif
            string dataObjectId
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                null,
                async () =>
                {
                    return await _domain.RestoreDataObjectAsync(
                        new RestoreDataObjectRequest()
                            .WithDataObjectId(dataObjectId)
                    );
                }
            );
            return new Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectDomain> self)
            {
                var future = _domain.RestoreDataObjectFuture(
                    new RestoreDataObjectRequest()
                        .WithDataObjectId(dataObjectId)
                );
                yield return _profile.RunFuture(
                    null,
                    future,
                    () =>
        			{
                		return future = _domain.RestoreDataObjectFuture(
                    		new RestoreDataObjectRequest()
                	        .WithDataObjectId(dataObjectId)
        		        );
        			}
                );
                if (future.Error != null)
                {
                    self.OnError(future.Error);
                    yield break;
                }
                var result = future.Result;
                self.OnComplete(new Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectDomain(result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Datastore.Domain.Model.EzDataObjectDomain>(Impl);
        #endif
        }

        public Gs2.Unity.Gs2Datastore.Domain.Model.EzUserDomain User(
            string userId
        ) {
            return new Gs2.Unity.Gs2Datastore.Domain.Model.EzUserDomain(
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
