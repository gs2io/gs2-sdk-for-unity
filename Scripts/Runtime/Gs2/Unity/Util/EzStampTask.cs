
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

using System.Collections;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Unity.Gs2Distributor.Result;
using LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.Util
{
    public class EzStampTask
    {
        private string _stampTaskStr;
        private StampTask _stampTask;
        
        public EzStampTask(string stampTask)
        {
            var signedStampTask = JsonMapper.ToObject<SignedStampSheet>(stampTask);
            _stampTaskStr = stampTask;
            _stampTask = JsonMapper.ToObject<StampTask>(signedStampTask.body);
        }
        
        public T ToRequest<T> () where T: IRequest 
        {
            return (T)typeof(T).GetMethod("FromDict")?.Invoke(null, new object[] { JsonMapper.ToObject(Args) });
        }

        public string TaskId => _stampTask.taskId;

        public string Action => _stampTask.action;
                
        public string Args => _stampTask.args;
                
        public long Timestamp => _stampTask.timestamp;

        public IEnumerator Execute(
            UnityAction<AsyncResult<EzRunStampTaskResult>> callback, 
            string stackContext,
            Gs2.Unity.Client client,
            string distributorNamespaceName,
            string stampSheetEncryptKeyId
        )
        {
            yield return client.Distributor.RunStampTask(
                callback,
                distributorNamespaceName,
                _stampTaskStr,
                stampSheetEncryptKeyId,
                stackContext
            );
        }
    }
}