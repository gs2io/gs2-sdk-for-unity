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
using Gs2.Gs2Inbox.Domain.Iterator;
using Gs2.Gs2Inbox.Request;
using Gs2.Gs2Inbox.Result;
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

namespace Gs2.Unity.Gs2Inbox.Domain.Model
{

    public partial class EzUserGameSessionDomain {
        private readonly Gs2.Gs2Inbox.Domain.Model.UserAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public string NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserGameSessionDomain(
            Gs2.Gs2Inbox.Domain.Model.UserAccessTokenDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Inbox.Domain.Model.EzMessageGameSessionDomain[]> ReceiveGlobalMessage(
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Inbox.Domain.Model.EzMessageGameSessionDomain[]> self)
            {
                yield return ReceiveGlobalMessageAsync(
                ).ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Inbox.Domain.Model.EzMessageGameSessionDomain[]>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Inbox.Domain.Model.EzMessageGameSessionDomain[]> ReceiveGlobalMessageAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Inbox.Domain.Model.EzMessageGameSessionDomain[]> ReceiveGlobalMessage(
        #endif
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.ReceiveGlobalMessageAsync(
                        new ReceiveGlobalMessageRequest()
                            .WithAccessToken(_domain.AccessToken.Token)
                    );
                }
            );
            return result.Select(v => new Gs2.Unity.Gs2Inbox.Domain.Model.EzMessageGameSessionDomain(v, _profile)).ToArray();
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Inbox.Domain.Model.EzMessageGameSessionDomain[]> self)
            {
                var future = _domain.ReceiveGlobalMessageFuture(
                    new ReceiveGlobalMessageRequest()
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return _profile.RunFuture(
                    _domain.AccessToken,
                    future,
                    () =>
        			{
                		return future = _domain.ReceiveGlobalMessageFuture(
                    		new ReceiveGlobalMessageRequest()
                    	    .WithAccessToken(_domain.AccessToken.Token)
        		        );
        			}
                );
                if (future.Error != null)
                {
                    self.OnError(future.Error);
                    yield break;
                }
                var result = future.Result;
                self.OnComplete(result.Select(v => new Gs2.Unity.Gs2Inbox.Domain.Model.EzMessageGameSessionDomain(v, _profile)).ToArray());
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Inbox.Domain.Model.EzMessageGameSessionDomain[]>(Impl);
        #endif
        }

        public class EzMessagesIterator : Gs2Iterator<Gs2.Unity.Gs2Inbox.Model.EzMessage>
        {
            private Gs2Iterator<Gs2.Gs2Inbox.Model.Message> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly bool? _isRead;
            private readonly Gs2.Gs2Inbox.Domain.Model.UserAccessTokenDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzMessagesIterator(
                Gs2Iterator<Gs2.Gs2Inbox.Model.Message> it,
        #if !GS2_ENABLE_UNITASK
                bool? isRead,
                Gs2.Gs2Inbox.Domain.Model.UserAccessTokenDomain domain,
        #endif
                Gs2.Unity.Util.Profile profile
            )
            {
                _it = it;
        #if !GS2_ENABLE_UNITASK
                _isRead = isRead;
                _domain = domain;
        #endif
                _profile = profile;
            }

            public override bool HasNext()
            {
                return _it.HasNext();
            }

            protected override IEnumerator Next(Action<AsyncResult<Gs2.Unity.Gs2Inbox.Model.EzMessage>> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    _domain.AccessToken,
                    _it,
                    () =>
                    {
                        return _it = _domain.Messages(
                            _isRead
                        );
                    }
                );
        #endif
                callback.Invoke(
                    new AsyncResult<Gs2.Unity.Gs2Inbox.Model.EzMessage>(
                        _it.Current == null ? null : Gs2.Unity.Gs2Inbox.Model.EzMessage.FromModel(_it.Current),
                        _it.Error
                    )
                );
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Inbox.Model.EzMessage> Messages(
              bool? isRead = null
        )
        {
            return new EzMessagesIterator(
                _domain.Messages(
                    isRead
                ),
                _profile
            );
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Inbox.Model.EzMessage> MessagesAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Inbox.Model.EzMessage> Messages(
        #endif
              bool? isRead = null
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Inbox.Model.EzMessage>(async (writer, token) =>
            {
                var it = _domain.MessagesAsync(
                    isRead
                ).GetAsyncEnumerator();
                while(
                    await _profile.RunIteratorAsync(
                        _domain.AccessToken,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.MessagesAsync(
                                isRead
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Inbox.Model.EzMessage.FromModel(it.Current));
                }
            });
        #else
            return new EzMessagesIterator(
                _domain.Messages(
                    isRead
                ),
                isRead,
                _domain,
                _profile
            );
        #endif
        }

        public ulong SubscribeMessages(Action callback) {
            return this._domain.SubscribeMessages(callback);
        }

        public void UnsubscribeMessages(ulong callbackId) {
            this._domain.UnsubscribeMessages(callbackId);
        }

        public Gs2.Unity.Gs2Inbox.Domain.Model.EzMessageGameSessionDomain Message(
            string messageName
        ) {
            return new Gs2.Unity.Gs2Inbox.Domain.Model.EzMessageGameSessionDomain(
                _domain.Message(
                    messageName
                ),
                _profile
            );
        }

    }
}
