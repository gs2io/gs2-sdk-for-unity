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
using Gs2.Gs2Lottery.Domain.Iterator;
using Gs2.Gs2Lottery.Request;
using Gs2.Gs2Lottery.Result;
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

namespace Gs2.Unity.Gs2Lottery.Domain.Model
{

    public partial class EzLotteryGameSessionDomain {
        
        public class EzDrawnPrizesIterator : Gs2Iterator<Gs2.Unity.Gs2Lottery.Model.EzDrawnPrize>
        {
            private Gs2Iterator<Gs2.Gs2Lottery.Model.DrawnPrize> _it;
#if !GS2_ENABLE_UNITASK
            private readonly Gs2.Gs2Lottery.Domain.Model.LotteryAccessTokenDomain _domain;
#endif
            private readonly Gs2.Unity.Util.GameSession _gameSession;
            private readonly Gs2.Unity.Util.Gs2Connection _connection;

            public EzDrawnPrizesIterator(
                Gs2Iterator<Gs2.Gs2Lottery.Model.DrawnPrize> it,
#if !GS2_ENABLE_UNITASK
                Gs2.Gs2Lottery.Domain.Model.LotteryAccessTokenDomain domain,
#endif
                Gs2.Unity.Util.GameSession gameSession,
                Gs2.Unity.Util.Gs2Connection connection
            )
            {
                _it = it;
#if !GS2_ENABLE_UNITASK
                _domain = domain;
#endif
                this._gameSession = gameSession;
                this._connection = connection;
            }

            public override bool HasNext()
            {
                return _it.HasNext();
            }

            protected override IEnumerator Next(Action<AsyncResult<Gs2.Unity.Gs2Lottery.Model.EzDrawnPrize>> callback)
            {
#if GS2_ENABLE_UNITASK
                yield return _it.Next();
#else
                yield return this._connection.RunIterator(
                    null,
                    _it,
                    () =>
                    {
                        return _it = _domain.DrawnPrizes(
                        );
                    }
                );
#endif
                callback.Invoke(
                    new AsyncResult<Gs2.Unity.Gs2Lottery.Model.EzDrawnPrize>(
                        _it.Current == null ? null : Gs2.Unity.Gs2Lottery.Model.EzDrawnPrize.FromModel(_it.Current),
                        _it.Error
                    )
                );
            }
        }
        
#if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Lottery.Model.EzDrawnPrize> DrawnPrizes(
        )
        {
            return new EzDrawnPrizesIterator(
                _domain.DrawnPrizes(
                ),
                _gameSession,
                _connection
            );
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Lottery.Model.EzDrawnPrize> DrawnPrizesAsync(
#else
        public Gs2Iterator<Gs2.Unity.Gs2Lottery.Model.EzDrawnPrize> DrawnPrizes(
#endif
        )
        {
#if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Lottery.Model.EzDrawnPrize>(async (writer, token) =>
            {
                var it = _domain.DrawnPrizesAsync(
                ).GetAsyncEnumerator();
                while(
                    await _connection.RunIteratorAsync(
                        _gameSession,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.DrawnPrizesAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Lottery.Model.EzDrawnPrize.FromModel(it.Current));
                }
            });
#else
            return new EzDrawnPrizesIterator(
                _domain.DrawnPrizes(
                ),
                _domain,
                _gameSession,
                this._connection
            );
#endif
        }
        
        public ulong SubscribeDrawnPrizes(Action callback) {
            return this._domain.SubscribeDrawnPrizes(callback);
        }

        public void UnsubscribeDrawnPrizes(ulong callbackId) {
            this._domain.UnsubscribeDrawnPrizes(callbackId);
        }

        public Gs2.Unity.Gs2Lottery.Domain.Model.EzDrawnPrizeGameSessionDomain DrawnPrize(
            int index
        ) {
            return new Gs2.Unity.Gs2Lottery.Domain.Model.EzDrawnPrizeGameSessionDomain(
                _domain.DrawnPrize(
                    index
                ),
                _gameSession,
                _connection
            );
        }
    }
}
