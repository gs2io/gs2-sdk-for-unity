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
using Gs2.Gs2Mission;
using Gs2.Gs2Mission.Model;
using Gs2.Gs2Mission.Request;
using Gs2.Gs2Mission.Result;
using Gs2.Unity.Gs2Mission.Model;
using Gs2.Unity.Gs2Mission.Result;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Unity.Util;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Gs2.Unity.Gs2Mission
{
	public class DisabledCertificateHandler : CertificateHandler {
		protected override bool ValidateCertificate(byte[] certificateData)
		{
			return true;
		}
	}

	public partial class Client
	{
		private readonly Gs2.Unity.Util.Profile _profile;
		private readonly Gs2MissionWebSocketClient _client;
		private readonly Gs2MissionRestClient _restClient;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2MissionWebSocketClient(profile.Gs2Session);
			if (profile.checkRevokeCertificate)
			{
				_restClient = new Gs2MissionRestClient(profile.Gs2RestSession);
			}
			else
			{
				_restClient = new Gs2MissionRestClient(profile.Gs2RestSession, new DisabledCertificateHandler());
			}
		}

		/// <summary>
		///  ミッショングループモデルの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		public IEnumerator ListMissionGroupModels(
		        UnityAction<AsyncResult<EzListMissionGroupModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.DescribeMissionGroupModels(
                    new DescribeMissionGroupModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<EzListMissionGroupModelsResult>(
                            r.Result == null ? null : new EzListMissionGroupModelsResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  ミッショングループ名を指定してミッショングループモデルを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="missionGroupName">グループ名</param>
		public IEnumerator GetMissionGroupModel(
		        UnityAction<AsyncResult<EzGetMissionGroupModelResult>> callback,
                string namespaceName,
                string missionGroupName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.GetMissionGroupModel(
                    new GetMissionGroupModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithMissionGroupName(missionGroupName),
                    r => cb.Invoke(
                        new AsyncResult<EzGetMissionGroupModelResult>(
                            r.Result == null ? null : new EzGetMissionGroupModelResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  達成したミッションの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="pageToken">データの取得を開始する位置を指定するトークン</param>
		/// <param name="limit">データの取得件数</param>
		public IEnumerator ListCounters(
		        UnityAction<AsyncResult<EzListCountersResult>> callback,
		        GameSession session,
                string namespaceName,
                string pageToken=null,
                long? limit=null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeCounters(
                    new DescribeCountersRequest()
                        .WithNamespaceName(namespaceName)
                        .WithPageToken(pageToken)
                        .WithLimit(limit)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzListCountersResult>(
                            r.Result == null ? null : new EzListCountersResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  ミッショングループを指定して達成したミッションを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="counterName">カウンター名</param>
		public IEnumerator GetCounter(
		        UnityAction<AsyncResult<EzGetCounterResult>> callback,
		        GameSession session,
                string namespaceName,
                string counterName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetCounter(
                    new GetCounterRequest()
                        .WithNamespaceName(namespaceName)
                        .WithCounterName(counterName)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzGetCounterResult>(
                            r.Result == null ? null : new EzGetCounterResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  達成したミッションの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="pageToken">データの取得を開始する位置を指定するトークン</param>
		/// <param name="limit">データの取得件数</param>
		public IEnumerator ListCompletes(
		        UnityAction<AsyncResult<EzListCompletesResult>> callback,
		        GameSession session,
                string namespaceName,
                string pageToken=null,
                long? limit=null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeCompletes(
                    new DescribeCompletesRequest()
                        .WithNamespaceName(namespaceName)
                        .WithPageToken(pageToken)
                        .WithLimit(limit)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzListCompletesResult>(
                            r.Result == null ? null : new EzListCompletesResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  ミッショングループを指定して達成したミッションを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="missionGroupName">ミッショングループ名</param>
		public IEnumerator GetComplete(
		        UnityAction<AsyncResult<EzGetCompleteResult>> callback,
		        GameSession session,
                string namespaceName,
                string missionGroupName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetComplete(
                    new GetCompleteRequest()
                        .WithNamespaceName(namespaceName)
                        .WithMissionGroupName(missionGroupName)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzGetCompleteResult>(
                            r.Result == null ? null : new EzGetCompleteResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  ミッションの達成報酬を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="missionGroupName">ミッショングループ名</param>
		/// <param name="missionTaskName">タスク名</param>
		public IEnumerator ReceiveRewards(
		        UnityAction<AsyncResult<EzReceiveRewardsResult>> callback,
		        GameSession session,
                string namespaceName,
                string missionGroupName,
                string missionTaskName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.Complete(
                    new CompleteRequest()
                        .WithNamespaceName(namespaceName)
                        .WithMissionGroupName(missionGroupName)
                        .WithMissionTaskName(missionTaskName)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzReceiveRewardsResult>(
                            r.Result == null ? null : new EzReceiveRewardsResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  ミッションタスクモデルの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="missionGroupName">グループ名</param>
		public IEnumerator ListMissionTaskModels(
		        UnityAction<AsyncResult<EzListMissionTaskModelsResult>> callback,
                string namespaceName,
                string missionGroupName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.DescribeMissionTaskModels(
                    new DescribeMissionTaskModelsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithMissionGroupName(missionGroupName),
                    r => cb.Invoke(
                        new AsyncResult<EzListMissionTaskModelsResult>(
                            r.Result == null ? null : new EzListMissionTaskModelsResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  ミッションタスク名を指定してミッションタスクモデルを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="missionGroupName">グループ名</param>
		/// <param name="missionTaskName">タスク名</param>
		public IEnumerator GetMissionTaskModel(
		        UnityAction<AsyncResult<EzGetMissionTaskModelResult>> callback,
                string namespaceName,
                string missionGroupName,
                string missionTaskName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.GetMissionTaskModel(
                    new GetMissionTaskModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithMissionGroupName(missionGroupName)
                        .WithMissionTaskName(missionTaskName),
                    r => cb.Invoke(
                        new AsyncResult<EzGetMissionTaskModelResult>(
                            r.Result == null ? null : new EzGetMissionTaskModelResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  カウンターの種類を認証<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		public IEnumerator ListCounterModels(
		        UnityAction<AsyncResult<EzListCounterModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.DescribeCounterModels(
                    new DescribeCounterModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<EzListCounterModelsResult>(
                            r.Result == null ? null : new EzListCounterModelsResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  カウンターの種類を認証<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="counterName">カウンター名</param>
		public IEnumerator GetCounterModel(
		        UnityAction<AsyncResult<EzGetCounterModelResult>> callback,
                string namespaceName,
                string counterName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.GetCounterModel(
                    new GetCounterModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithCounterName(counterName),
                    r => cb.Invoke(
                        new AsyncResult<EzGetCounterModelResult>(
                            r.Result == null ? null : new EzGetCounterModelResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
	}
}