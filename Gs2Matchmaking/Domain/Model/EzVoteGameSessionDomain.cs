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

using System.Linq;
using Cysharp.Threading.Tasks;
using Gs2.Unity.Gs2Matchmaking.Domain.Iterator;
using Gs2.Unity.Util;

namespace Gs2.Unity.Gs2Matchmaking.Domain.Model
{

    public partial class EzVoteGameSessionDomain {

        private Gs2.Gs2Matchmaking.Domain.Model.VoteAccessTokenDomain _domain;

        public EzVoteGameSessionDomain(
            Gs2.Gs2Matchmaking.Domain.Model.VoteAccessTokenDomain domain
        ) {
            _domain = domain;
        }

        public async UniTask<Gs2.Unity.Gs2Matchmaking.Result.EzCreateVoteResult> Create(
        )
        {
            return Gs2.Unity.Gs2Matchmaking.Result.EzCreateVoteResult.FromModel(
                await _domain.GetBallot(
                    new Gs2.Gs2Matchmaking.Request.GetBallotRequest()
                )
            );
        }
    }
}
