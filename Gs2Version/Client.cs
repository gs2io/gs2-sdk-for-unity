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
using Gs2.Gs2Version;
using Gs2.Gs2Version.Model;
using Gs2.Gs2Version.Request;
using Gs2.Gs2Version.Result;
using Gs2.Unity.Gs2Version.Model;
using Gs2.Unity.Gs2Version.Result;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Unity.Util;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Gs2.Unity.Gs2Version
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
		private readonly Gs2VersionWebSocketClient _client;
		private readonly Gs2VersionRestClient _restClient;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2VersionWebSocketClient(profile.Gs2Session);
			if (profile.checkRevokeCertificate)
			{
				_restClient = new Gs2VersionRestClient(profile.Gs2RestSession);
			}
			else
			{
				_restClient = new Gs2VersionRestClient(profile.Gs2RestSession, new DisabledCertificateHandler());
			}
		}

		/// <summary>
		///  バージョン設定を認証<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		public IEnumerator ListVersionModels(
		        UnityAction<AsyncResult<EzListVersionModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.DescribeVersionModels(
                    new DescribeVersionModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<EzListVersionModelsResult>(
                            r.Result == null ? null : new EzListVersionModelsResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  バージョン設定を認証<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="versionName">バージョン名</param>
		public IEnumerator GetVersionModel(
		        UnityAction<AsyncResult<EzGetVersionModelResult>> callback,
                string namespaceName,
                string versionName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.GetVersionModel(
                    new GetVersionModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithVersionName(versionName),
                    r => cb.Invoke(
                        new AsyncResult<EzGetVersionModelResult>(
                            r.Result == null ? null : new EzGetVersionModelResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  承認したバージョンの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="pageToken">データの取得を開始する位置を指定するトークン</param>
		/// <param name="limit">データの取得件数</param>
		public IEnumerator List(
		        UnityAction<AsyncResult<EzListResult>> callback,
		        GameSession session,
                string namespaceName,
                string pageToken=null,
                long? limit=null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeAcceptVersions(
                    new DescribeAcceptVersionsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithPageToken(pageToken)
                        .WithLimit(limit)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzListResult>(
                            r.Result == null ? null : new EzListResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  承認する<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="versionName">承認したバージョン名</param>
		public IEnumerator Accept(
		        UnityAction<AsyncResult<EzAcceptResult>> callback,
		        GameSession session,
                string namespaceName,
                string versionName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.Accept(
                    new AcceptRequest()
                        .WithNamespaceName(namespaceName)
                        .WithVersionName(versionName)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzAcceptResult>(
                            r.Result == null ? null : new EzAcceptResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  承認したバージョンを削除する<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="versionName">承認したバージョン名</param>
		public IEnumerator Delete(
		        UnityAction<AsyncResult<EzDeleteResult>> callback,
		        GameSession session,
                string namespaceName,
                string versionName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.DeleteAcceptVersion(
                    new DeleteAcceptVersionRequest()
                        .WithNamespaceName(namespaceName)
                        .WithVersionName(versionName)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzDeleteResult>(
                            r.Result == null ? null : new EzDeleteResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  スタンプタスクを実行<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="targetVersions">加算するリソース</param>
		public IEnumerator CheckVersion(
		        UnityAction<AsyncResult<EzCheckVersionResult>> callback,
		        GameSession session,
                string namespaceName,
                List<EzTargetVersion> targetVersions=null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.CheckVersion(
                    new CheckVersionRequest()
                        .WithNamespaceName(namespaceName)
                        .WithTargetVersions(targetVersions != null ? targetVersions.Select(item => item?.ToModel()).ToList() : new List<TargetVersion>(new TargetVersion[]{}))
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzCheckVersionResult>(
                            r.Result == null ? null : new EzCheckVersionResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
	}
}