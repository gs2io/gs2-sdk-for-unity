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
using Gs2.Gs2SkillTree.Domain.Iterator;
using Gs2.Gs2SkillTree.Request;
using Gs2.Gs2SkillTree.Result;
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

namespace Gs2.Unity.Gs2SkillTree.Domain.Model
{

    public partial class EzNodeModelDomain {
        private readonly Gs2.Gs2SkillTree.Domain.Model.NodeModelDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public string NamespaceName => _domain?.NamespaceName;
        public string NodeModelName => _domain?.NodeModelName;

        public EzNodeModelDomain(
            Gs2.Gs2SkillTree.Domain.Model.NodeModelDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        [Obsolete("The name has been changed to ModelFuture.")]
        public IFuture<Gs2.Unity.Gs2SkillTree.Model.EzNodeModel> Model()
        {
            return ModelFuture();
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2SkillTree.Model.EzNodeModel> ModelFuture()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2SkillTree.Model.EzNodeModel> self)
            {
                yield return ModelAsync().ToCoroutine(
                    self.OnComplete,
                    e =>
                    {
                        if (e is Gs2.Core.Exception.Gs2Exception e2) {
                            self.OnError(e2);
                        }
                        else {
                            UnityEngine.Debug.LogError(e.Message);
                            self.OnError(new Gs2.Core.Exception.UnknownException(e.Message));
                        }
                    }
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2SkillTree.Model.EzNodeModel>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2SkillTree.Model.EzNodeModel> ModelAsync()
        {
            var item = await _profile.RunAsync(
                null,
                async () =>
                {
                    return await _domain.ModelAsync();
                }
            );
            if (item == null) {
                return null;
            }
            return Gs2.Unity.Gs2SkillTree.Model.EzNodeModel.FromModel(
                item
            );
        }
        #else
        public IFuture<Gs2.Unity.Gs2SkillTree.Model.EzNodeModel> ModelFuture()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2SkillTree.Model.EzNodeModel> self)
            {
                var future = _domain.ModelFuture();
                yield return _profile.RunFuture(
                    null,
                    future,
                    () => {
                    	return future = _domain.ModelFuture();
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
                self.OnComplete(Gs2.Unity.Gs2SkillTree.Model.EzNodeModel.FromModel(
                    item
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2SkillTree.Model.EzNodeModel>(Impl);
        }
        #endif

        public ulong Subscribe(Action<Gs2.Unity.Gs2SkillTree.Model.EzNodeModel> callback)
        {
            return this._domain.Subscribe(item => {
                callback.Invoke(Gs2.Unity.Gs2SkillTree.Model.EzNodeModel.FromModel(
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
