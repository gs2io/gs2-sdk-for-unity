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
using Gs2.Core.Net;
using Gs2.Unity.Gs2Deploy.Domain.Model;
using Gs2.Unity.Util;

namespace Gs2.Unity.Gs2Deploy.Domain
{

    public class Gs2Deploy {

        private Gs2.Gs2Deploy.Domain.Gs2Deploy _domain;

        public Gs2Deploy(
            Profile profile
        ) {
            _domain = new Gs2.Gs2Deploy.Domain.Gs2Deploy(
                profile.Gs2RestSession
            );
        }
        public EzStackDomain Stack(
            string stackName
        )
        {
            return new EzStackDomain(
                _domain.Stack(
                    stackName
                )
            );
        }
    }
}
