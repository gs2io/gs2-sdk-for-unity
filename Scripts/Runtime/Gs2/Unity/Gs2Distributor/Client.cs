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
using Gs2.Gs2Distributor;
using Gs2.Gs2Distributor.Model;
using Gs2.Gs2Distributor.Request;
using Gs2.Gs2Distributor.Result;
using Gs2.Unity.Gs2Distributor.Model;
using Gs2.Unity.Gs2Distributor.Result;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Unity.Util;
using UnityEngine.Events;

namespace Gs2.Unity.Gs2Distributor
{
	public class Client
	{
		private readonly Gs2.Unity.Util.Profile _profile;
		private readonly Gs2DistributorWebSocketClient _client;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2DistributorWebSocketClient(profile.Gs2Session);
		}

		/// <summary>
		///  配信設定を認証<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		public IEnumerator ListDistributorModels(
		        UnityAction<AsyncResult<EzListDistributorModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _client.DescribeDistributorModels(
                new DescribeDistributorModelsRequest()
                    .WithNamespaceName(namespaceName),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzListDistributorModelsResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzListDistributorModelsResult>(
                                new EzListDistributorModelsResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}

		/// <summary>
		///  配信設定を認証<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="distributorName">配信設定名</param>
		public IEnumerator GetDistributorModel(
		        UnityAction<AsyncResult<EzGetDistributorModelResult>> callback,
                string namespaceName,
                string distributorName
        )
		{
            yield return _client.GetDistributorModel(
                new GetDistributorModelRequest()
                    .WithNamespaceName(namespaceName)
                    .WithDistributorName(distributorName),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzGetDistributorModelResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzGetDistributorModelResult>(
                                new EzGetDistributorModelResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}

		/// <summary>
		///  スタンプタスクを実行<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="stampTask">実行するスタンプタスク</param>
		/// <param name="keyId">スタンプシートの暗号化に使用した暗号鍵GRN</param>
		/// <param name="contextStack">スタンプシートの実行状況を記録するスタックメモリ</param>
		public IEnumerator RunStampTask(
		        UnityAction<AsyncResult<EzRunStampTaskResult>> callback,
                string namespaceName,
                string stampTask,
                string keyId,
                string contextStack=null
        )
		{
            yield return _client.RunStampTask(
                new RunStampTaskRequest()
                    .WithNamespaceName(namespaceName)
                    .WithStampTask(stampTask)
                    .WithKeyId(keyId)
                    .WithContextStack(contextStack),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzRunStampTaskResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzRunStampTaskResult>(
                                new EzRunStampTaskResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}

		/// <summary>
		///  スタンプシートを実行<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="stampSheet">実行するスタンプタスク</param>
		/// <param name="keyId">スタンプシートの暗号化に使用した暗号鍵GRN</param>
		/// <param name="contextStack">スタンプシートの実行状況を記録するスタックメモリ</param>
		public IEnumerator RunStampSheet(
		        UnityAction<AsyncResult<EzRunStampSheetResult>> callback,
                string namespaceName,
                string stampSheet,
                string keyId,
                string contextStack=null
        )
		{
            yield return _client.RunStampSheet(
                new RunStampSheetRequest()
                    .WithNamespaceName(namespaceName)
                    .WithStampSheet(stampSheet)
                    .WithKeyId(keyId)
                    .WithContextStack(contextStack),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzRunStampSheetResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzRunStampSheetResult>(
                                new EzRunStampSheetResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}
	}
}