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
using Gs2.Gs2Matchmaking.Domain.Iterator;
using Gs2.Gs2Matchmaking.Request;
using Gs2.Gs2Matchmaking.Result;
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

namespace Gs2.Unity.Gs2Matchmaking.Domain.Model
{

    public partial class EzNamespaceDomain {
        private readonly Gs2.Gs2Matchmaking.Domain.Model.NamespaceDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public string Status => _domain.Status;
        public string NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;

        public EzNamespaceDomain(
            Gs2.Gs2Matchmaking.Domain.Model.NamespaceDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain> Vote(
              string ballotBody,
              string ballotSignature,
              Gs2.Unity.Gs2Matchmaking.Model.EzGameResult[] gameResults = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain> self)
            {
                yield return VoteAsync(
                    ballotBody,
                    ballotSignature,
                    gameResults
                ).ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain> VoteAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain> Vote(
        #endif
              string ballotBody,
              string ballotSignature,
              Gs2.Unity.Gs2Matchmaking.Model.EzGameResult[] gameResults = null
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                null,
                async () =>
                {
                    return await _domain.VoteAsync(
                        new VoteRequest()
                            .WithBallotBody(ballotBody)
                            .WithBallotSignature(ballotSignature)
                            .WithGameResults(gameResults?.Select(v => v.ToModel()).ToArray())
                    );
                }
            );
            return new Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain> self)
            {
                var future = _domain.Vote(
                    new VoteRequest()
                        .WithBallotBody(ballotBody)
                        .WithBallotSignature(ballotSignature)
                        .WithGameResults(gameResults?.Select(v => v.ToModel()).ToArray())
                );
                yield return _profile.RunFuture(
                    null,
                    future,
                    () =>
        			{
                		return future = _domain.Vote(
                    		new VoteRequest()
                	        .WithBallotBody(ballotBody)
                	        .WithBallotSignature(ballotSignature)
        	                .WithGameResults(gameResults?.Select(v => v.ToModel()).ToArray())
        		        );
        			}
                );
                if (future.Error != null)
                {
                    self.OnError(future.Error);
                    yield break;
                }
                var result = future.Result;
                self.OnComplete(new Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain(result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain>(Impl);
        #endif
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain> VoteMultiple(
              Gs2.Unity.Gs2Matchmaking.Model.EzSignedBallot[] signedBallots = null,
              Gs2.Unity.Gs2Matchmaking.Model.EzGameResult[] gameResults = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain> self)
            {
                yield return VoteMultipleAsync(
                    signedBallots,
                    gameResults
                ).ToCoroutine(
                    self.OnComplete,
                    e => self.OnError((Gs2.Core.Exception.Gs2Exception)e)
                );
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain> VoteMultipleAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain> VoteMultiple(
        #endif
              Gs2.Unity.Gs2Matchmaking.Model.EzSignedBallot[] signedBallots = null,
              Gs2.Unity.Gs2Matchmaking.Model.EzGameResult[] gameResults = null
        ) {
        #if GS2_ENABLE_UNITASK
            var result = await _profile.RunAsync(
                null,
                async () =>
                {
                    return await _domain.VoteMultipleAsync(
                        new VoteMultipleRequest()
                            .WithSignedBallots(signedBallots?.Select(v => v.ToModel()).ToArray())
                            .WithGameResults(gameResults?.Select(v => v.ToModel()).ToArray())
                    );
                }
            );
            return new Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain> self)
            {
                var future = _domain.VoteMultiple(
                    new VoteMultipleRequest()
                        .WithSignedBallots(signedBallots?.Select(v => v.ToModel()).ToArray())
                        .WithGameResults(gameResults?.Select(v => v.ToModel()).ToArray())
                );
                yield return _profile.RunFuture(
                    null,
                    future,
                    () =>
        			{
                		return future = _domain.VoteMultiple(
                    		new VoteMultipleRequest()
        	                .WithSignedBallots(signedBallots?.Select(v => v.ToModel()).ToArray())
        	                .WithGameResults(gameResults?.Select(v => v.ToModel()).ToArray())
        		        );
        			}
                );
                if (future.Error != null)
                {
                    self.OnError(future.Error);
                    yield break;
                }
                var result = future.Result;
                self.OnComplete(new Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain(result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain>(Impl);
        #endif
        }

        public Gs2.Unity.Gs2Matchmaking.Domain.Model.EzUserDomain User(
            string userId
        ) {
            return new Gs2.Unity.Gs2Matchmaking.Domain.Model.EzUserDomain(
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

        public class EzRatingModelsIterator : Gs2Iterator<Gs2.Unity.Gs2Matchmaking.Model.EzRatingModel>
        {
            private readonly Gs2Iterator<Gs2.Gs2Matchmaking.Model.RatingModel> _it;

            public EzRatingModelsIterator(
                Gs2Iterator<Gs2.Gs2Matchmaking.Model.RatingModel> it
            )
            {
                _it = it;
            }

            public override bool HasNext()
            {
                return _it.HasNext();
            }

            protected override IEnumerator Next(Action<Gs2.Unity.Gs2Matchmaking.Model.EzRatingModel> callback)
            {
                yield return _it.Next();
                callback.Invoke(_it.Current == null ? null : Gs2.Unity.Gs2Matchmaking.Model.EzRatingModel.FromModel(_it.Current));
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Matchmaking.Model.EzRatingModel> RatingModels(
        )
        {
            return new EzRatingModelsIterator(_domain.RatingModels(
            ));
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Matchmaking.Model.EzRatingModel> RatingModelsAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Matchmaking.Model.EzRatingModel> RatingModels(
        #endif
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Matchmaking.Model.EzRatingModel>(async (writer, token) =>
            {
                var it = _domain.RatingModelsAsync(
                ).GetAsyncEnumerator();
                while(await it.MoveNextAsync())
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Matchmaking.Model.EzRatingModel.FromModel(it.Current));
                }
            });
        #else
            return new EzRatingModelsIterator(_domain.RatingModels(
            ));
        #endif
        }

        public Gs2.Unity.Gs2Matchmaking.Domain.Model.EzRatingModelDomain RatingModel(
            string ratingName
        ) {
            return new Gs2.Unity.Gs2Matchmaking.Domain.Model.EzRatingModelDomain(
                _domain.RatingModel(
                    ratingName
                ),
                _profile
            );
        }

    }
}
