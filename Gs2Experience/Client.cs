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
using Gs2.Gs2Experience;
using Gs2.Gs2Experience.Model;
using Gs2.Gs2Experience.Request;
using Gs2.Gs2Experience.Result;
using Gs2.Unity.Gs2Experience.Model;
using Gs2.Unity.Gs2Experience.Result;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Unity.Util;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Gs2.Unity.Gs2Experience
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
		private readonly Gs2ExperienceWebSocketClient _client;
		private readonly Gs2ExperienceRestClient _restClient;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2ExperienceWebSocketClient(profile.Gs2Session);
			if (profile.checkRevokeCertificate)
			{
				_restClient = new Gs2ExperienceRestClient(profile.Gs2RestSession);
			}
			else
			{
				_restClient = new Gs2ExperienceRestClient(profile.Gs2RestSession, new DisabledCertificateHandler());
			}
		}

		/// <summary>
		///  経験値・ランクアップ閾値モデル情報の一覧を取得<br />
		///    <br />
		///    ランクキャップの情報やランクアップ閾値の情報を取得します。<br />
		///    次のランクアップまでに必要な獲得経験値量などをゲーム内で表示したい場合はこのモデルデータを使ってください。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		public IEnumerator ListExperienceModels(
		        UnityAction<AsyncResult<EzListExperienceModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.DescribeExperienceModels(
                    new DescribeExperienceModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<EzListExperienceModelsResult>(
                            r.Result == null ? null : new EzListExperienceModelsResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  経験値・ランクアップ閾値モデル情報を取得<br />
		///    <br />
		///    `経験値の種類名` を指定してランクキャップの情報やランクアップ閾値の情報を取得します。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="experienceName">経験値の種類名</param>
		public IEnumerator GetExperienceModel(
		        UnityAction<AsyncResult<EzGetExperienceModelResult>> callback,
                string namespaceName,
                string experienceName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.GetExperienceModel(
                    new GetExperienceModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithExperienceName(experienceName),
                    r => cb.Invoke(
                        new AsyncResult<EzGetExperienceModelResult>(
                            r.Result == null ? null : new EzGetExperienceModelResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  ステータス情報の一覧を取得<br />
		///    <br />
		///    経験値の種類名 は省略可能で、指定しなかった場合はゲームプレイヤーに属する全てのステータス情報が取得できます。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="experienceName">経験値の種類名</param>
		/// <param name="pageToken">データの取得を開始する位置を指定するトークン</param>
		/// <param name="limit">データの取得件数</param>
		public IEnumerator ListStatuses(
		        UnityAction<AsyncResult<EzListStatusesResult>> callback,
		        GameSession session,
                string namespaceName,
                string experienceName=null,
                string pageToken=null,
                long? limit=null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeStatuses(
                    new DescribeStatusesRequest()
                        .WithNamespaceName(namespaceName)
                        .WithExperienceName(experienceName)
                        .WithPageToken(pageToken)
                        .WithLimit(limit)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzListStatusesResult>(
                            r.Result == null ? null : new EzListStatusesResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  `経験値の種類` と `プロパティID` を指定してステータス情報を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="experienceName">経験値の種類の名前</param>
		/// <param name="propertyId">プロパティID</param>
		public IEnumerator GetStatus(
		        UnityAction<AsyncResult<EzGetStatusResult>> callback,
		        GameSession session,
                string namespaceName,
                string experienceName,
                string propertyId
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetStatus(
                    new GetStatusRequest()
                        .WithNamespaceName(namespaceName)
                        .WithExperienceName(experienceName)
                        .WithPropertyId(propertyId)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzGetStatusResult>(
                            r.Result == null ? null : new EzGetStatusResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
	}
}