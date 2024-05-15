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
using System.Linq;
using Gs2.Gs2News;
using Gs2.Gs2News.Model;
using Gs2.Gs2News.Request;
using Gs2.Gs2News.Result;
using Gs2.Unity.Gs2News.Model;
using Gs2.Unity.Gs2News.Result;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Unity.Util;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Gs2.Unity.Gs2News
{
    public partial class Client
    {
        

        /// <summary>
        ///  達成したミッションの一覧を取得<br />
        /// </summary>
        ///
        /// <returns>IEnumerator</returns>
        /// <param name="callback">コールバックハンドラ</param>
        /// <param name="session">ゲームセッション</param>
        /// <param name="namespaceName">ネームスペースの名前</param>
        public IEnumerator DownloadZip(
            UnityAction<AsyncResult<byte[]>> callback,
            IGameSession session,
            string namespaceName
        )
        {
            string zipUrl = null;
            yield return _restClient.WantGrant(
                new WantGrantRequest()
                    .WithNamespaceName(namespaceName)
                    .WithAccessToken(session.AccessToken.Token),
                r =>
                {
                    if(r.Result == null)
                    {
                        callback.Invoke(
                            new AsyncResult<byte[]>(
                                null,
                                r.Error
                            )
                        );
                    }
                    else
                    {
                        zipUrl = r.Result.ZipUrl;
                    }
                }
            );

            if (zipUrl == null)
            {
                yield break;
            }
			
            using var request = UnityWebRequest.Get(zipUrl);
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();

            var result = new RestResult(
                (int) request.responseCode,
                request.responseCode == 200 ? "{}" : string.IsNullOrEmpty(request.error) ? "{}" : request.error
            );

            if (result.Error != null)
            {
                callback.Invoke(
                    new AsyncResult<byte[]>(
                        null,
                        result.Error
                    )
                );
                yield break;
            }
			
            callback.Invoke(
                new AsyncResult<byte[]>(
                    request.downloadHandler.data,
                    result.Error
                )
            );
        }
    }
}