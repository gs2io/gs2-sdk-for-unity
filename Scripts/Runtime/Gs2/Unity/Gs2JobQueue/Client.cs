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
using Gs2.Gs2JobQueue;
using Gs2.Gs2JobQueue.Model;
using Gs2.Gs2JobQueue.Request;
using Gs2.Gs2JobQueue.Result;
using Gs2.Unity.Gs2JobQueue.Model;
using Gs2.Unity.Gs2JobQueue.Result;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Unity.Util;
using UnityEngine.Events;

namespace Gs2.Unity.Gs2JobQueue
{
	public class Client
	{
		private readonly Gs2.Unity.Util.Profile _profile;
		private readonly Gs2JobQueueWebSocketClient _client;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2JobQueueWebSocketClient(profile.Gs2Session);
		}

		/// <summary>
		///  タスクキューのジョブを実行します。<br />
		///    <br />
		///    タスクキューのプッシュ通知設定をすることでタスクキューに新しくジョブが追加されたときにプッシュ通知を受けることができます。<br />
		///    定期的にこのAPIを呼び出すか、プッシュ通知をトリガーしてこのAPIを呼び出してください。<br />
		///    `isLastJob` が false を返している間は繰り返し実行してください。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		public IEnumerator Run(
		        UnityAction<AsyncResult<EzRunResult>> callback,
		        GameSession session,
                string namespaceName
        )
		{
            yield return _client.Run(
                new RunRequest()
                    .WithNamespaceName(namespaceName)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzRunResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzRunResult>(
                                new EzRunResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}
	}
}