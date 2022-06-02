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
using Gs2.Unity.Util;

namespace Gs2.Unity.Gs2JobQueue.Domain.Model
{

    public partial class EzJobGameSessionDomain {

        private Gs2.Gs2JobQueue.Domain.Model.JobAccessTokenDomain _domain;
        private readonly Gs2.Unity.Util.Profile _profile;
        
        public EzJobGameSessionDomain(
            Gs2.Gs2JobQueue.Domain.Model.JobAccessTokenDomain domain,
            Gs2.Unity.Util.Profile profile
        ) {
            _domain = domain;
            this._profile = profile;
        }
    }
}
