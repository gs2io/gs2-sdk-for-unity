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
using Gs2.Gs2Enhance;
using Gs2.Gs2Enhance.Model;
using Gs2.Gs2Enhance.Request;
using Gs2.Gs2Enhance.Result;
using Gs2.Unity.Gs2Enhance.Model;
using Gs2.Unity.Gs2Enhance.Result;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Unity.Util;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Gs2.Unity.Gs2Enhance
{
	public class DisabledCertificateHandler : CertificateHandler {
		protected override bool ValidateCertificate(byte[] certificateData)
		{
			return true;
		}
	}

	public class Client
	{
		private readonly Gs2.Unity.Util.Profile _profile;
		private readonly Gs2EnhanceWebSocketClient _client;
		private readonly Gs2EnhanceRestClient _restClient;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2EnhanceWebSocketClient(profile.Gs2Session);
			if (profile.checkRevokeCertificate)
			{
				_restClient = new Gs2EnhanceRestClient(profile.Gs2RestSession);
			}
			else
			{
				_restClient = new Gs2EnhanceRestClient(profile.Gs2RestSession, new DisabledCertificateHandler());
			}
		}

		/// <summary>
		///  強化レートモデル情報の一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		public IEnumerator ListRateModels(
		        UnityAction<AsyncResult<EzListRateModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.DescribeRateModels(
                    new DescribeRateModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<EzListRateModelsResult>(
                            r.Result == null ? null : new EzListRateModelsResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  強化レートモデル情報を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="rateName">強化レート名</param>
		public IEnumerator GetRateModel(
		        UnityAction<AsyncResult<EzGetRateModelResult>> callback,
                string namespaceName,
                string rateName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.GetRateModel(
                    new GetRateModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRateName(rateName),
                    r => cb.Invoke(
                        new AsyncResult<EzGetRateModelResult>(
                            r.Result == null ? null : new EzGetRateModelResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  強化を実行<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="rateName">強化レート名</param>
		/// <param name="targetItemSetId">強化対象の GS2-Inventory アイテムセットGRN</param>
		/// <param name="materials">強化素材リスト</param>
		/// <param name="config">設定値</param>
		public IEnumerator Enhance(
		        UnityAction<AsyncResult<EzEnhanceResult>> callback,
		        GameSession session,
                string namespaceName,
                string rateName,
                string targetItemSetId,
                List<EzMaterial> materials,
                List<EzConfig> config=null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DirectEnhance(
                    new DirectEnhanceRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRateName(rateName)
                        .WithTargetItemSetId(targetItemSetId)
                        .WithMaterials(materials != null ? materials.Select(item => item?.ToModel()).ToList() : new List<Material>(new Material[]{}))
                        .WithConfig(config != null ? config.Select(item => item?.ToModel()).ToList() : new List<Config>(new Config[]{}))
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzEnhanceResult>(
                            r.Result == null ? null : new EzEnhanceResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  強化の開始を宣言<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="rateName">強化レート名</param>
		/// <param name="targetItemSetId">強化対象の GS2-Inventory アイテムセットGRN</param>
		/// <param name="materials">強化素材リスト</param>
		/// <param name="force">すでに開始している強化がある場合にそれを破棄して開始するか</param>
		/// <param name="config">スタンプシートの変数に適用する設定値</param>
		public IEnumerator Start(
		        UnityAction<AsyncResult<EzStartResult>> callback,
		        GameSession session,
                string namespaceName,
                string rateName,
                string targetItemSetId,
                List<EzMaterial> materials,
                bool? force=null,
                List<EzConfig> config=null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.Start(
                    new StartRequest()
                        .WithNamespaceName(namespaceName)
                        .WithRateName(rateName)
                        .WithTargetItemSetId(targetItemSetId)
                        .WithMaterials(materials != null ? materials.Select(item => item?.ToModel()).ToList() : new List<Material>(new Material[]{}))
                        .WithForce(force)
                        .WithConfig(config != null ? config.Select(item => item?.ToModel()).ToList() : new List<Config>(new Config[]{}))
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzStartResult>(
                            r.Result == null ? null : new EzStartResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  強化の完了を報告<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="config">スタンプシートの変数に適用する設定値</param>
		public IEnumerator End(
		        UnityAction<AsyncResult<EzEndResult>> callback,
		        GameSession session,
                string namespaceName,
                List<EzConfig> config=null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.End(
                    new EndRequest()
                        .WithNamespaceName(namespaceName)
                        .WithConfig(config != null ? config.Select(item => item?.ToModel()).ToList() : new List<Config>(new Config[]{}))
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzEndResult>(
                            r.Result == null ? null : new EzEndResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  強化の進行情報を取得。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		public IEnumerator GetProgress(
		        UnityAction<AsyncResult<EzGetProgressResult>> callback,
		        GameSession session,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetProgress(
                    new GetProgressRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzGetProgressResult>(
                            r.Result == null ? null : new EzGetProgressResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  強化の進行情報を削除。<br />
		///    <br />
		///    強化の開始時に `force` オプションを使うのではなく、明示的に進行情報を削除したい場合に使用してください。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		public IEnumerator DeleteProgress(
		        UnityAction<AsyncResult<EzDeleteProgressResult>> callback,
		        GameSession session,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.DeleteProgress(
                    new DeleteProgressRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzDeleteProgressResult>(
                            r.Result == null ? null : new EzDeleteProgressResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
	}
}