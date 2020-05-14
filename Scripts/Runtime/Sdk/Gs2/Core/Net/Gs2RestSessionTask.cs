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

using System;
using System.Collections;
using Gs2.Core.Exception;
using LitJson;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Gs2.Core.Net
{
    public abstract class Gs2RestSessionTask : Gs2SessionTask
    {
        private class Gs2RestTask : Gs2.Core.Net.Gs2RestTask
        {
            private Gs2RestSession _gs2RestSession = null;
            private Gs2SessionTaskId _gs2SessionTaskId = null;

            public IEnumerator Send(Gs2RestSession gs2RestSession, Gs2SessionTaskId gs2SessionTaskId)
            {
                _gs2RestSession = gs2RestSession;
                _gs2SessionTaskId = gs2SessionTaskId;

                return Send();
            }

            public override void Callback(Gs2RestResponse gs2RestResponse)
            {
                _gs2RestSession.OnMessage(_gs2SessionTaskId, gs2RestResponse);
            }
        }

        private readonly Gs2RestTask _gs2RestTask;
        protected UnityWebRequest UnityWebRequest => _gs2RestTask.UnityWebRequest;

        protected Gs2RestSessionTask()
        {
            _gs2RestTask = new Gs2RestTask();
        }

        protected override void CancelImpl()
        {
            UnityWebRequest.Abort();
        }

        protected IEnumerator Send(Gs2RestSession gs2RestSession)
        {
            UnityWebRequest.SetRequestHeader("X-GS2-CLIENT-ID", gs2RestSession.Credential.ClientId);
            UnityWebRequest.SetRequestHeader("Authorization", string.Format("Bearer {0}", gs2RestSession.ProjectToken));

            UnityWebRequest.timeout = Timeout.Seconds;

            return _gs2RestTask.Send(gs2RestSession, Gs2SessionTaskId);
        }
    }

    public abstract class Gs2RestSessionTask<T> : Gs2RestSessionTask
    {
        private readonly UnityAction<AsyncResult<T>> _userCallback;

        protected Gs2RestSessionTask(UnityAction<AsyncResult<T>> userCallback)
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
                        var method = typeof(T).GetMethod("FromDict");
                        if (method == null)
                        {
                            throw new InvalidProgramException("not have 'FromDict'" + typeof(T));
                        }
                        result = (T)method.Invoke(null, new object[] { JsonMapper.ToObject(gs2Response.Message) });
                    }
                }
                catch (JsonException jsonException)
                {
                    error = new UnknownException("JSON parsing error: \n" + gs2Response.Message);
                }
            }

            _userCallback.Invoke(new AsyncResult<T>(result, error));
        }
    }
}
