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

namespace Gs2.Unity.Gs2Version
{
	public class Client
	{
		private readonly Gs2.Unity.Util.Profile _profile;
		private readonly Gs2VersionWebSocketClient _client;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2VersionWebSocketClient(profile.Gs2Session);
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
            yield return _client.DescribeVersionModels(
                new DescribeVersionModelsRequest()
                    .WithNamespaceName(namespaceName),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzListVersionModelsResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzListVersionModelsResult>(
                                new EzListVersionModelsResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
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
            yield return _client.GetVersionModel(
                new GetVersionModelRequest()
                    .WithNamespaceName(namespaceName)
                    .WithVersionName(versionName),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzGetVersionModelResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzGetVersionModelResult>(
                                new EzGetVersionModelResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
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
            yield return _client.DescribeAcceptVersions(
                new DescribeAcceptVersionsRequest()
                    .WithNamespaceName(namespaceName)
                    .WithPageToken(pageToken)
                    .WithLimit(limit)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzListResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzListResult>(
                                new EzListResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
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
            yield return _client.DeleteAcceptVersion(
                new DeleteAcceptVersionRequest()
                    .WithNamespaceName(namespaceName)
                    .WithVersionName(versionName)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzDeleteResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzDeleteResult>(
                                new EzDeleteResult(r.Result),
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
            yield return _client.CheckVersion(
                new CheckVersionRequest()
                    .WithNamespaceName(namespaceName)
                    .WithTargetVersions(targetVersions != null ? targetVersions.Select(item => item.ToModel()).ToList() : new List<TargetVersion>(new TargetVersion[]{}))
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzCheckVersionResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzCheckVersionResult>(
                                new EzCheckVersionResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}
	}
}