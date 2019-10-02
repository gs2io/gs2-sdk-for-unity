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

using Gs2.Core.Model;
using Gs2.Core.Exception;
using LitJson;
using UnityEngine.Events;

namespace Gs2.Core.Net
{
    public abstract class Gs2WebSocketSessionTask : Gs2SessionTask
    {
        protected override void CancelImpl()
        {
            Complete(null);     // 応答は中で SessionNotOpen に上書きされる
        }
    }

    public abstract class Gs2WebSocketSessionTask<T> : Gs2WebSocketSessionTask
    {
        private readonly UnityAction<AsyncResult<T>> _userCallback;

        protected Gs2WebSocketSessionTask(UnityAction<AsyncResult<T>> userCallback)
        {
            _userCallback = userCallback;
        }

        protected override void InvokeCallbackImpl(Gs2Response gs2Response)
        {
            var result = default(T);
            var error = gs2Response.Error;

            if (error == null)
            {
                try
                {
                    if (!string.IsNullOrEmpty(gs2Response.Message) && gs2Response.Message != "Null")
                    {
                        var message = JsonMapper.ToObject(gs2Response.Message);
                        result = (T)typeof(T).GetMethod("FromDict")?.Invoke(null, new object[] { message["body"] });
                    }
                }
                catch (System.Exception e)
                {
                    error = new UnknownException("JSON parsing error: \n" + gs2Response.Message);
                }
            }

            _userCallback.Invoke(new AsyncResult<T>(result, error));
        }
    }
}
