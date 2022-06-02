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

    public partial class EzUserDomain {
        private readonly Gs2.Gs2Matchmaking.Domain.Model.UserDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        public string NextPageToken => _domain.NextPageToken;
        public string MatchmakingContextToken => _domain.MatchmakingContextToken;
        public string NamespaceName => _domain?.NamespaceName;
        public string UserId => _domain?.UserId;

        public EzUserDomain(
            Gs2.Gs2Matchmaking.Domain.Model.UserDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            this._domain = domain;
            this._profile = profile;
        }

        public class EzGatheringsIterator : Gs2Iterator<Gs2.Unity.Gs2Matchmaking.Model.EzGathering>
        {
            private readonly Gs2Iterator<Gs2.Gs2Matchmaking.Model.Gathering> _it;

            public EzGatheringsIterator(
                Gs2Iterator<Gs2.Gs2Matchmaking.Model.Gathering> it
            )
            {
                _it = it;
            }

            public override bool HasNext()
            {
                return _it.HasNext();
            }

            protected override IEnumerator Next(Action<Gs2.Unity.Gs2Matchmaking.Model.EzGathering> callback)
            {
                yield return _it.Next();
                callback.Invoke(_it.Current == null ? null : Gs2.Unity.Gs2Matchmaking.Model.EzGathering.FromModel(_it.Current));
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Matchmaking.Model.EzGathering> Gatherings(
        )
        {
            return new EzGatheringsIterator(_domain.Gatherings(
            ));
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Matchmaking.Model.EzGathering> GatheringsAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Matchmaking.Model.EzGathering> Gatherings(
        #endif
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Matchmaking.Model.EzGathering>(async (writer, token) =>
            {
                var it = _domain.GatheringsAsync(
                ).GetAsyncEnumerator();
                while(await it.MoveNextAsync())
                {
                    await writer.YieldAsync(Gs2.Unity.Gs2Matchmaking.Model.EzGathering.FromModel(it.Current));
                }
            });
        #else
            return new EzGatheringsIterator(_domain.Gatherings(
            ));
        #endif
        }

        public class EzDoMatchmakingByPlayerIterator : Gs2Iterator<Gs2.Unity.Gs2Matchmaking.Model.EzGathering>
        {
            private readonly Gs2Iterator<Gs2.Gs2Matchmaking.Model.Gathering> _it;

            public EzDoMatchmakingByPlayerIterator(
                Gs2Iterator<Gs2.Gs2Matchmaking.Model.Gathering> it
            )
            {
                _it = it;
            }

            public override bool HasNext()
            {
                return _it.HasNext();
            }

            protected override IEnumerator Next(Action<Gs2.Unity.Gs2Matchmaking.Model.EzGathering> callback)
            {
                yield return _it.Next();
                callback.Invoke(_it.Current == null ? null : Gs2.Unity.Gs2Matchmaking.Model.EzGathering.FromModel(_it.Current));
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Matchmaking.Model.EzGathering> DoMatchmakingByPlayer(
              Gs2.Unity.Gs2Matchmaking.Model.EzPlayer player
        )
        {
            return new EzDoMatchmakingByPlayerIterator(_domain.DoMatchmakingByPlayer(
               player.ToModel()
            ));
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Matchmaking.Model.EzGathering> DoMatchmakingByPlayerAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Matchmaking.Model.EzGathering> DoMatchmakingByPlayer(
        #endif
              Gs2.Unity.Gs2Matchmaking.Model.EzPlayer player
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Matchmaking.Model.EzGathering>(async (writer, token) =>
            {
                var it = _domain.DoMatchmakingByPlayerAsync(
                    player.ToModel()
                ).GetAsyncEnumerator();
                while(await it.MoveNextAsync())
                {
                    await writer.YieldAsync(Gs2.Unity.Gs2Matchmaking.Model.EzGathering.FromModel(it.Current));
                }
            });
        #else
            return new EzDoMatchmakingByPlayerIterator(_domain.DoMatchmakingByPlayer(
               player.ToModel()
            ));
        #endif
        }

        public class EzDoMatchmakingIterator : Gs2Iterator<Gs2.Unity.Gs2Matchmaking.Model.EzGathering>
        {
            private readonly Gs2Iterator<Gs2.Gs2Matchmaking.Model.Gathering> _it;

            public EzDoMatchmakingIterator(
                Gs2Iterator<Gs2.Gs2Matchmaking.Model.Gathering> it
            )
            {
                _it = it;
            }

            public override bool HasNext()
            {
                return _it.HasNext();
            }

            protected override IEnumerator Next(Action<Gs2.Unity.Gs2Matchmaking.Model.EzGathering> callback)
            {
                yield return _it.Next();
                callback.Invoke(_it.Current == null ? null : Gs2.Unity.Gs2Matchmaking.Model.EzGathering.FromModel(_it.Current));
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Matchmaking.Model.EzGathering> DoMatchmaking(
              Gs2.Unity.Gs2Matchmaking.Model.EzPlayer player
        )
        {
            return new EzDoMatchmakingIterator(_domain.DoMatchmaking(
               player.ToModel()
            ));
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Matchmaking.Model.EzGathering> DoMatchmakingAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Matchmaking.Model.EzGathering> DoMatchmaking(
        #endif
              Gs2.Unity.Gs2Matchmaking.Model.EzPlayer player
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Matchmaking.Model.EzGathering>(async (writer, token) =>
            {
                var it = _domain.DoMatchmakingAsync(
                    player.ToModel()
                ).GetAsyncEnumerator();
                while(await it.MoveNextAsync())
                {
                    await writer.YieldAsync(Gs2.Unity.Gs2Matchmaking.Model.EzGathering.FromModel(it.Current));
                }
            });
        #else
            return new EzDoMatchmakingIterator(_domain.DoMatchmaking(
               player.ToModel()
            ));
        #endif
        }

        public Gs2.Unity.Gs2Matchmaking.Domain.Model.EzGatheringDomain Gathering(
            string gatheringName
        ) {
            return new Gs2.Unity.Gs2Matchmaking.Domain.Model.EzGatheringDomain(
                _domain.Gathering(
                    gatheringName
                ),
                _profile
            );
        }

        public Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain Ballot(
            string ratingName,
            string gatheringName,
            int numberOfPlayer,
            string keyId
        ) {
            return new Gs2.Unity.Gs2Matchmaking.Domain.Model.EzBallotDomain(
                _domain.Ballot(
                    ratingName,
                    gatheringName,
                    numberOfPlayer,
                    keyId
                ),
                _profile
            );
        }

        public class EzRatingsIterator : Gs2Iterator<Gs2.Unity.Gs2Matchmaking.Model.EzRating>
        {
            private readonly Gs2Iterator<Gs2.Gs2Matchmaking.Model.Rating> _it;

            public EzRatingsIterator(
                Gs2Iterator<Gs2.Gs2Matchmaking.Model.Rating> it
            )
            {
                _it = it;
            }

            public override bool HasNext()
            {
                return _it.HasNext();
            }

            protected override IEnumerator Next(Action<Gs2.Unity.Gs2Matchmaking.Model.EzRating> callback)
            {
                yield return _it.Next();
                callback.Invoke(_it.Current == null ? null : Gs2.Unity.Gs2Matchmaking.Model.EzRating.FromModel(_it.Current));
            }
        }

        #if GS2_ENABLE_UNITASK
        public Gs2Iterator<Gs2.Unity.Gs2Matchmaking.Model.EzRating> Ratings(
        )
        {
            return new EzRatingsIterator(_domain.Ratings(
            ));
        }

        public IUniTaskAsyncEnumerable<Gs2.Unity.Gs2Matchmaking.Model.EzRating> RatingsAsync(
        #else
        public Gs2Iterator<Gs2.Unity.Gs2Matchmaking.Model.EzRating> Ratings(
        #endif
        )
        {
        #if GS2_ENABLE_UNITASK
            return UniTaskAsyncEnumerable.Create<Gs2.Unity.Gs2Matchmaking.Model.EzRating>(async (writer, token) =>
            {
                var it = _domain.RatingsAsync(
                ).GetAsyncEnumerator();
                while(await it.MoveNextAsync())
                {
                    await writer.YieldAsync(Gs2.Unity.Gs2Matchmaking.Model.EzRating.FromModel(it.Current));
                }
            });
        #else
            return new EzRatingsIterator(_domain.Ratings(
            ));
        #endif
        }

        public Gs2.Unity.Gs2Matchmaking.Domain.Model.EzRatingDomain Rating(
            string ratingName
        ) {
            return new Gs2.Unity.Gs2Matchmaking.Domain.Model.EzRatingDomain(
                _domain.Rating(
                    ratingName
                ),
                _profile
            );
        }

    }
}
