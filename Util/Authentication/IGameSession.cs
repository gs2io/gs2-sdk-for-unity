/*
 * Copyright 2016 Game Server Services, Inc. or its affiliates. All Rights
 * Reserved.
 *
 * Licensed under the Apache License, Version 2.0(the "License").
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

using Gs2.Core.Domain;
using Gs2.Gs2Auth.Model;
#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
#endif

namespace Gs2.Unity.Util
{
    public interface IGameSession
    {
        public string UserId { get; }
        public AccessToken AccessToken { get; }

        public Gs2Future RefreshFuture();
        
#if GS2_ENABLE_UNITASK
        public UniTask RefreshAsync();
#endif

        public Gs2Future<bool> RefreshIfNeedRefreshFuture();
        
#if GS2_ENABLE_UNITASK
        public UniTask<bool> RefreshIfNeedRefreshAsync();
#endif
    }
}