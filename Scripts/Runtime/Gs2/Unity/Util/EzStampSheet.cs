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
using System.Collections.Generic;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Unity.Gs2Distributor.Result;
using LitJson;
using UnityEngine.Events;

namespace Gs2.Unity.Util
{
    public class EzStampSheet
    {
        private string _stampSheetStr;
        private StampSheet _stampSheet;
        private List<EzStampTask> _stampTasks;
        
        public EzStampSheet(string stampSheet)
        {
            var signedStampSheet = JsonMapper.ToObject<SignedStampSheet>(stampSheet);
            _stampSheetStr = stampSheet;
            _stampSheet = JsonMapper.ToObject<StampSheet>(signedStampSheet.body);
            
            _stampTasks = new List<EzStampTask>();
            foreach (var task in _stampSheet.tasks)
            {
                _stampTasks.Add(
                    new EzStampTask(task)
                );
            }
        }
        
        public T ToRequest<T> () where T: IRequest 
        {
            return (T)typeof(T).GetMethod("FromDict")?.Invoke(null, new object[] { JsonMapper.ToObject(Args) });
        }

        public string OwnerId => _stampSheet.ownerId;

        public string UserId => _stampSheet.userId;
        
        public string Action => _stampSheet.action;
                
        public string Args => _stampSheet.args;

        public List<EzStampTask> Tasks => _stampTasks;
                
        public string TransactionId => _stampSheet.transactionId;
                
        public long Timestamp => _stampSheet.timestamp;

        public IEnumerator Execute(
            UnityAction<AsyncResult<EzRunStampSheetResult>> callback, 
            string stackContext,
            Gs2.Unity.Client client,
            string distributorNamespaceName,
            string stampSheetEncryptKeyId
        )
        {
            yield return client.Distributor.RunStampSheet(
                callback,
                distributorNamespaceName,
                _stampSheetStr,
                stampSheetEncryptKeyId,
                stackContext
            );
        }
    }
}