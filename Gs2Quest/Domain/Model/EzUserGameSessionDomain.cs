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
using Gs2.Gs2Quest.Domain.Iterator;
using Gs2.Gs2Quest.Request;
using Gs2.Gs2Quest.Result;
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

namespace Gs2.Unity.Gs2Quest.Domain.Model
{

    public partial class EzUserGameSessionDomain {
        private readonly Gs2.Gs2Quest.Domain.Model.UserAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public string NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserGameSessionDomain(
            Gs2.Gs2Quest.Domain.Model.UserAccessTokenDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Quest.Domain.Model.EzUserGameSessionDomain> Start(
              string questGroupName,
              string questName,
              bool? force = null,
              Gs2.Unity.Gs2Quest.Model.EzConfig[] config = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Quest.Domain.Model.EzUserGameSessionDomain> self)
            {
                yield return StartAsync(
                    questGroupName,
                    questName,
                    force,
                    config
                ).ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Quest.Domain.Model.EzUserGameSessionDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Quest.Domain.Model.EzUserGameSessionDomain> StartAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Quest.Domain.Model.EzUserGameSessionDomain> Start(
        #endif
              string questGroupName,
              string questName,
              bool? force = null,
              Gs2.Unity.Gs2Quest.Model.EzConfig[] config = null
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.StartAsync(
                        new StartRequest()
                            .WithQuestGroupName(questGroupName)
                            .WithQuestName(questName)
                            .WithForce(force)
                            .WithConfig(config?.Select(v => v.ToModel()).ToArray())
                            .WithAccessToken(_domain.AccessToken.Token)
                    );
                }
            );
            return new Gs2.Unity.Gs2Quest.Domain.Model.EzUserGameSessionDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Quest.Domain.Model.EzUserGameSessionDomain> self)
            {
                var future = _domain.Start(
                    new StartRequest()
                        .WithQuestGroupName(questGroupName)
                        .WithQuestName(questName)
                        .WithForce(force)
                        .WithConfig(config?.Select(v => v.ToModel()).ToArray())
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return _profile.RunFuture(
                    _domain.AccessToken,
                    future
                );
                if (future.Error != null)
                {
                    self.OnError(future.Error);
                    yield break;
                }
                var result = future.Result;
                self.OnComplete(new Gs2.Unity.Gs2Quest.Domain.Model.EzUserGameSessionDomain(result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Quest.Domain.Model.EzUserGameSessionDomain>(Impl);
        #endif
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Quest.Domain.Model.EzProgressGameSessionDomain> DeleteProgress(
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Quest.Domain.Model.EzProgressGameSessionDomain> self)
            {
                yield return DeleteProgressAsync(
                ).ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Quest.Domain.Model.EzProgressGameSessionDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Quest.Domain.Model.EzProgressGameSessionDomain> DeleteProgressAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Quest.Domain.Model.EzProgressGameSessionDomain> DeleteProgress(
        #endif
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                _domain.AccessToken,
                async () =>
                {
                    return await _domain.DeleteProgressAsync(
                        new DeleteProgressRequest()
                            .WithAccessToken(_domain.AccessToken.Token)
                    );
                }
            );
            return new Gs2.Unity.Gs2Quest.Domain.Model.EzProgressGameSessionDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Quest.Domain.Model.EzProgressGameSessionDomain> self)
            {
                var future = _domain.DeleteProgress(
                    new DeleteProgressRequest()
                        .WithAccessToken(_domain.AccessToken.Token)
                );
                yield return _profile.RunFuture(
                    _domain.AccessToken,
                    future
                );
                if (future.Error != null)
                {
                    self.OnError(future.Error);
                    yield break;
                }
                var result = future.Result;
                self.OnComplete(new Gs2.Unity.Gs2Quest.Domain.Model.EzProgressGameSessionDomain(result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Quest.Domain.Model.EzProgressGameSessionDomain>(Impl);
        #endif
        }

        public Gs2.Unity.Gs2Quest.Domain.Model.EzProgressGameSessionDomain Progress(
        ) {
            return new Gs2.Unity.Gs2Quest.Domain.Model.EzProgressGameSessionDomain(
                _domain.Progress(
                ),
                _profile
            );
        }

        public class EzCompletedQuestListsIterator : Gs2Iterator<Gs2.Unity.Gs2Quest.Model.EzCompletedQuestList>
        {
            private readonly Gs2Iterator<Gs2.Gs2Quest.Model.CompletedQuestList> _it;

            public EzCompletedQuestListsIterator(
                Gs2Iterator<Gs2.Gs2Quest.Model.CompletedQuestList> it
            )
            {
                _it = it;
            }

            public override bool HasNext()
            {
                return _it.HasNext();
            }

            protected override IEnumerator Next(Action<Gs2.Unity.Gs2Quest.Model.EzCompletedQuestList> callback)
            {
                yield return _it.Next();
                callback.Invoke(_it.Current == null ? null : Gs2.Unity.Gs2Quest.Model.EzCompletedQuestList.FromModel(_it.Current));
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Quest.Model.EzCompletedQuestList> CompletedQuestLists(
        )
        {
            return new EzCompletedQuestListsIterator(_domain.CompletedQuestLists(
            ));
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Quest.Model.EzCompletedQuestList> CompletedQuestListsAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Quest.Model.EzCompletedQuestList> CompletedQuestLists(
        #endif
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Quest.Model.EzCompletedQuestList>(async (writer, token) =>
            {
                var it = _domain.CompletedQuestListsAsync(
                ).GetAsyncEnumerator();
                while(await it.MoveNextAsync())
                {
                    await writer.YieldAsync(Gs2.Unity.Gs2Quest.Model.EzCompletedQuestList.FromModel(it.Current));
                }
            });
        #else
            return new EzCompletedQuestListsIterator(_domain.CompletedQuestLists(
            ));
        #endif
        }

        public Gs2.Unity.Gs2Quest.Domain.Model.EzCompletedQuestListGameSessionDomain CompletedQuestList(
            string questGroupName
        ) {
            return new Gs2.Unity.Gs2Quest.Domain.Model.EzCompletedQuestListGameSessionDomain(
                _domain.CompletedQuestList(
                    questGroupName
                ),
                _profile
            );
        }

    }
}
