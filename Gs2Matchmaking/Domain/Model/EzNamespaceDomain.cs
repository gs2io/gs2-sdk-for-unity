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
        public string Url => _domain.Url;
        public string UploadToken => _domain.UploadToken;
        public string UploadUrl => _domain.UploadUrl;
        public string NextPageToken => _domain.NextPageToken;
        public string NamespaceName => _domain?.NamespaceName;

        public EzNamespaceDomain(
            Gs2.Gs2Matchmaking.Domain.Model.NamespaceDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        [Obsolete("The name has been changed to VoteFuture.")]
        public IFuture<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain> Vote(
            string ballotBody,
            string ballotSignature,
            string keyId,
            Gs2.Unity.Gs2Matchmaking.Model.EzGameResult[] gameResults = null
        )
        {
            return VoteFuture(
                ballotBody,
                ballotSignature,
                keyId,
                gameResults
            );
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain> VoteFuture(
            string ballotBody,
            string ballotSignature,
            string keyId,
            Gs2.Unity.Gs2Matchmaking.Model.EzGameResult[] gameResults = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain> self)
            {
                var future = this._domain.VoteFuture(
                    new VoteRequest()
                        .WithBallotBody(ballotBody)
                        .WithBallotSignature(ballotSignature)
                        .WithGameResults(gameResults?.Select(v => v.ToModel()).ToArray())
                        .WithKeyId(keyId)
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain(future.Result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain> VoteAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain> VoteFuture(
        #endif
            string ballotBody,
            string ballotSignature,
            string keyId,
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
                            .WithKeyId(keyId)
                    );
                }
            );
            return new Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain> self)
            {
                var future = _domain.VoteFuture(
                    new VoteRequest()
                        .WithBallotBody(ballotBody)
                        .WithBallotSignature(ballotSignature)
                        .WithGameResults(gameResults?.Select(v => v.ToModel()).ToArray())
                        .WithKeyId(keyId)
                );
                yield return _profile.RunFuture(
                    null,
                    future,
                    () =>
        			{
                		return future = _domain.VoteFuture(
                    		new VoteRequest()
                	        .WithBallotBody(ballotBody)
                	        .WithBallotSignature(ballotSignature)
        	                .WithGameResults(gameResults?.Select(v => v.ToModel()).ToArray())
                	        .WithKeyId(keyId)
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

        [Obsolete("The name has been changed to VoteMultipleFuture.")]
        public IFuture<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain> VoteMultiple(
            string keyId,
            Gs2.Unity.Gs2Matchmaking.Model.EzSignedBallot[] signedBallots = null,
            Gs2.Unity.Gs2Matchmaking.Model.EzGameResult[] gameResults = null
        )
        {
            return VoteMultipleFuture(
                keyId,
                signedBallots,
                gameResults
            );
        }

        #if GS2_ENABLE_UNITASK
        public IFuture<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain> VoteMultipleFuture(
            string keyId,
            Gs2.Unity.Gs2Matchmaking.Model.EzSignedBallot[] signedBallots = null,
            Gs2.Unity.Gs2Matchmaking.Model.EzGameResult[] gameResults = null
        )
        {
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain> self)
            {
                var future = this._domain.VoteMultipleFuture(
                    new VoteMultipleRequest()
                        .WithSignedBallots(signedBallots?.Select(v => v.ToModel()).ToArray())
                        .WithGameResults(gameResults?.Select(v => v.ToModel()).ToArray())
                        .WithKeyId(keyId)
                );
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain(future.Result, _profile));
            }
            return new Gs2InlineFuture<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain>(Impl);
        }

        public async UniTask<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain> VoteMultipleAsync(
        #else
        public IFuture<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain> VoteMultipleFuture(
        #endif
            string keyId,
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
                            .WithKeyId(keyId)
                    );
                }
            );
            return new Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain(result, _profile);
        #else
            IEnumerator Impl(Gs2Future<Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain> self)
            {
                var future = _domain.VoteMultipleFuture(
                    new VoteMultipleRequest()
                        .WithSignedBallots(signedBallots?.Select(v => v.ToModel()).ToArray())
                        .WithGameResults(gameResults?.Select(v => v.ToModel()).ToArray())
                        .WithKeyId(keyId)
                );
                yield return _profile.RunFuture(
                    null,
                    future,
                    () =>
        			{
                		return future = _domain.VoteMultipleFuture(
                    		new VoteMultipleRequest()
        	                .WithSignedBallots(signedBallots?.Select(v => v.ToModel()).ToArray())
        	                .WithGameResults(gameResults?.Select(v => v.ToModel()).ToArray())
                	        .WithKeyId(keyId)
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
            private Gs2Iterator<Gs2.Gs2Matchmaking.Model.RatingModel> _it;
        #if !GS2_ENABLE_UNITASK
            private readonly Gs2.Gs2Matchmaking.Domain.Model.NamespaceDomain _domain;
        #endif
            private readonly Gs2.Unity.Util.Profile _profile;

            public EzRatingModelsIterator(
                Gs2Iterator<Gs2.Gs2Matchmaking.Model.RatingModel> it,
        #if !GS2_ENABLE_UNITASK
                Gs2.Gs2Matchmaking.Domain.Model.NamespaceDomain domain,
        #endif
                Gs2.Unity.Util.Profile profile
            )
            {
                _it = it;
        #if !GS2_ENABLE_UNITASK
                _domain = domain;
        #endif
                _profile = profile;
            }

            public override bool HasNext()
            {
                return _it.HasNext();
            }

            protected override IEnumerator Next(Action<AsyncResult<Gs2.Unity.Gs2Matchmaking.Model.EzRatingModel>> callback)
            {
        #if GS2_ENABLE_UNITASK
                yield return _it.Next();
        #else
                yield return _profile.RunIterator(
                    null,
                    _it,
                    () =>
                    {
                        return _it = _domain.RatingModels(
                        );
                    }
                );
        #endif
                callback.Invoke(
                    new AsyncResult<Gs2.Unity.Gs2Matchmaking.Model.EzRatingModel>(
                        _it.Current == null ? null : Gs2.Unity.Gs2Matchmaking.Model.EzRatingModel.FromModel(_it.Current),
                        _it.Error
                    )
                );
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Matchmaking.Model.EzRatingModel> RatingModels(
        )
        {
            return new EzRatingModelsIterator(
                _domain.RatingModels(
                ),
                _profile
            );
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
                while(
                    await _profile.RunIteratorAsync(
                        null,
                        async () =>
                        {
                            return await it.MoveNextAsync();
                        },
                        () => {
                            it = _domain.RatingModelsAsync(
                            ).GetAsyncEnumerator();
                        }
                    )
                )
                {
                    await writer.YieldAsync(it.Current == null ? null : Gs2.Unity.Gs2Matchmaking.Model.EzRatingModel.FromModel(it.Current));
                }
            });
        #else
            return new EzRatingModelsIterator(
                _domain.RatingModels(
                ),
                _domain,
                _profile
            );
        #endif
        }

        public ulong SubscribeRatingModels(Action callback) {
            return this._domain.SubscribeRatingModels(callback);
        }

        public void UnsubscribeRatingModels(ulong callbackId) {
            this._domain.UnsubscribeRatingModels(callbackId);
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
