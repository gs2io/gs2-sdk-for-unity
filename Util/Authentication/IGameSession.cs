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
#if UNITY_2017_1_OR_NEWER && GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
#elif !UNITY_2017_1_OR_NEWER
using System.Threading.Tasks;
#endif

namespace Gs2.Unity.Util
{
    public interface IGameSession
    {
        public string UserId { get; }
        public AccessToken AccessToken { get; }

        public Gs2Future RefreshFuture();

#if !UNITY_2017_1_OR_NEWER || GS2_ENABLE_UNITASK
    #if UNITY_2017_1_OR_NEWER
        public UniTask RefreshAsync();
    #else
        public Task RefreshAsync();
    #endif
#endif

        public Gs2Future<bool> RefreshIfNeedRefreshFuture();

#if !UNITY_2017_1_OR_NEWER || GS2_ENABLE_UNITASK
    #if UNITY_2017_1_OR_NEWER
        public UniTask<bool> RefreshIfNeedRefreshAsync();
    #else
        public Task<bool> RefreshIfNeedRefreshAsync();
    #endif
#endif
    }
}