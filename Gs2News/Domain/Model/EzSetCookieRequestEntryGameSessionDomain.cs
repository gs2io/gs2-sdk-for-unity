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
using Gs2.Gs2News.Domain.Iterator;
using Gs2.Gs2News.Request;
using Gs2.Gs2News.Result;
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

namespace Gs2.Unity.Gs2News.Domain.Model
{

    public partial class EzSetCookieRequestEntryGameSessionDomain {

        private Gs2.Gs2News.Domain.Model.SetCookieRequestEntryAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.IGameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;
        public string Key => _domain.Key;
        public string Value => _domain.Value;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;
        
        public EzSetCookieRequestEntryGameSessionDomain(
            Gs2.Gs2News.Domain.Model.SetCookieRequestEntryAccessTokenDomain domain,
            Gs2.Unity.Util.IGameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        ) {
            _domain = domain;
            this._gameSession = gameSession;
            this._connection = connection;
        }
        
        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2News.Model.EzSetCookieRequestEntry> Model()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2News.Model.EzSetCookieRequestEntry> self)
            {
                yield return ModelAsync().ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2News.Model.EzSetCookieRequestEntry>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2News.Model.EzSetCookieRequestEntry> ModelAsync()
        {
            var item = await _connection.RunAsync(
                null,
                async () =>
                {
                    return await _domain.ModelAsync();
                }
            );
            if (item == null) {
                return null;
            }
            return Gs2.Unity.Gs2News.Model.EzSetCookieRequestEntry.FromModel(
                item
            );
        }
        #else
        public IFuture<Gs2.Unity.Gs2News.Model.EzSetCookieRequestEntry> Model()
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2News.Model.EzSetCookieRequestEntry> self)
            {
                var future = _connection.RunFuture(
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
                self.OnComplete(Gs2.Unity.Gs2News.Model.EzSetCookieRequestEntry.FromModel(
                    item
                ));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2News.Model.EzSetCookieRequestEntry>(Impl);
        }
        #endif
        
    }
}
