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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Domain;
using Gs2.Core.Util;
using Gs2.Gs2Auth.Model;
using Gs2.Util.LitJson;
using UnityEngine.Scripting;

namespace Gs2.Unity.Gs2LoginReward.Domain.Iterator
{

    public class EzListReceiveStatussIterator : Gs2Iterator<Gs2.Unity.Gs2LoginReward.Model.EzReceiveStatus>
    {
        private Gs2Iterator<Gs2.Gs2LoginReward.Model.ReceiveStatus> _it;
        private readonly Gs2.Gs2LoginReward.Domain.Model.UserAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.IGameSession _gameSession;
        private readonly Gs2.Unity.Util.Gs2Connection _connection;

        public EzListReceiveStatussIterator(
            Gs2.Gs2LoginReward.Domain.Model.UserAccessTokenDomain domain,
            Gs2.Unity.Util.IGameSession gameSession,
            Gs2.Unity.Util.Gs2Connection connection
        )
        {
            _domain = domain;
            _gameSession = gameSession;
            _connection = connection;
            _it = _domain.ReceiveStatuses(
            );
        }

        public override bool HasNext()
        {
            return _it.HasNext();
        }

        protected override IEnumerator Next(Action<AsyncResult<Gs2.Unity.Gs2LoginReward.Model.EzReceiveStatus>> callback)
        {
            yield return _connection.RunIterator(
                _gameSession,
                _it,
                () =>
                {
                    return _it = _domain.ReceiveStatuses(
                    );
                }
            );
            callback.Invoke(
                new AsyncResult<Gs2.Unity.Gs2LoginReward.Model.EzReceiveStatus>(
                    _it.Current == null ? null : Gs2.Unity.Gs2LoginReward.Model.EzReceiveStatus.FromModel(_it.Current),
                    _it.Error
                )
            );
        }
    }

}
