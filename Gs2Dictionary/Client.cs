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
using Gs2.Gs2Dictionary;
using Gs2.Gs2Dictionary.Model;
using Gs2.Gs2Dictionary.Request;
using Gs2.Gs2Dictionary.Result;
using Gs2.Unity.Gs2Dictionary.Model;
using Gs2.Unity.Gs2Dictionary.Result;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Unity.Util;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Gs2.Unity.Gs2Dictionary
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
		private readonly Gs2DictionaryWebSocketClient _client;
		private readonly Gs2DictionaryRestClient _restClient;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2DictionaryWebSocketClient(profile.Gs2Session);
			if (profile.checkRevokeCertificate)
			{
				_restClient = new Gs2DictionaryRestClient(profile.Gs2RestSession);
			}
			else
			{
				_restClient = new Gs2DictionaryRestClient(profile.Gs2RestSession, new DisabledCertificateHandler());
			}
		}

		/// <summary>
		///  エントリーモデル情報の一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		public IEnumerator ListEntryModels(
		        UnityAction<AsyncResult<EzListEntryModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.DescribeEntryModels(
                    new DescribeEntryModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<EzListEntryModelsResult>(
                            r.Result == null ? null : new EzListEntryModelsResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  エントリーモデル情報を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="entryName">エントリーモデル名</param>
		public IEnumerator GetEntryModel(
		        UnityAction<AsyncResult<EzGetEntryModelResult>> callback,
                string namespaceName,
                string entryName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.GetEntryModel(
                    new GetEntryModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithEntryName(entryName),
                    r => cb.Invoke(
                        new AsyncResult<EzGetEntryModelResult>(
                            r.Result == null ? null : new EzGetEntryModelResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  エントリーの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="limit">データの取得件数</param>
		/// <param name="pageToken">データの取得を開始する位置を指定するトークン</param>
		public IEnumerator ListEntries(
		        UnityAction<AsyncResult<EzListEntriesResult>> callback,
		        GameSession session,
                string namespaceName,
                long? limit=null,
                string pageToken=null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeEntries(
                    new DescribeEntriesRequest()
                        .WithNamespaceName(namespaceName)
                        .WithLimit(limit)
                        .WithPageToken(pageToken)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzListEntriesResult>(
                            r.Result == null ? null : new EzListEntriesResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  エントリーを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="entryModelName">エントリー名</param>
		public IEnumerator GetEntry(
		        UnityAction<AsyncResult<EzGetEntryResult>> callback,
		        GameSession session,
                string namespaceName,
                string entryModelName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetEntry(
                    new GetEntryRequest()
                        .WithNamespaceName(namespaceName)
                        .WithEntryModelName(entryModelName)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzGetEntryResult>(
                            r.Result == null ? null : new EzGetEntryResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  エントリーを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="entryModelName">エントリー名</param>
		/// <param name="keyId">署名の発行に使用する暗号鍵 のGRN</param>
		public IEnumerator GetEntryWithSignature(
		        UnityAction<AsyncResult<EzGetEntryWithSignatureResult>> callback,
		        GameSession session,
                string namespaceName,
                string entryModelName,
                string keyId
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetEntryWithSignature(
                    new GetEntryWithSignatureRequest()
                        .WithNamespaceName(namespaceName)
                        .WithEntryModelName(entryModelName)
                        .WithKeyId(keyId)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzGetEntryWithSignatureResult>(
                            r.Result == null ? null : new EzGetEntryWithSignatureResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
	}
}