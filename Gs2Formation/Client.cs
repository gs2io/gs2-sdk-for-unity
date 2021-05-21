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
using Gs2.Gs2Formation;
using Gs2.Gs2Formation.Model;
using Gs2.Gs2Formation.Request;
using Gs2.Gs2Formation.Result;
using Gs2.Unity.Gs2Formation.Model;
using Gs2.Unity.Gs2Formation.Result;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Unity.Util;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Gs2.Unity.Gs2Formation
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
		private readonly Gs2FormationWebSocketClient _client;
		private readonly Gs2FormationRestClient _restClient;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2FormationWebSocketClient(profile.Gs2Session);
			if (profile.checkRevokeCertificate)
			{
				_restClient = new Gs2FormationRestClient(profile.Gs2RestSession);
			}
			else
			{
				_restClient = new Gs2FormationRestClient(profile.Gs2RestSession, new DisabledCertificateHandler());
			}
		}

		/// <summary>
		///  フォームモデル情報の一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		public IEnumerator ListMoldModels(
		        UnityAction<AsyncResult<EzListMoldModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.DescribeMoldModels(
                    new DescribeMoldModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<EzListMoldModelsResult>(
                            r.Result == null ? null : new EzListMoldModelsResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  フォームモデル情報を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="moldName">フォームの保存領域名</param>
		public IEnumerator GetMoldModel(
		        UnityAction<AsyncResult<EzGetMoldModelResult>> callback,
                string namespaceName,
                string moldName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.GetMoldModel(
                    new GetMoldModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithMoldName(moldName),
                    r => cb.Invoke(
                        new AsyncResult<EzGetMoldModelResult>(
                            r.Result == null ? null : new EzGetMoldModelResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  保存したフォーム情報の一覧を取得<br />
		///    <br />
		///    フォームの保存領域名 は省略可能で、指定しなかった場合はゲームプレイヤーに属する全ての保存したフォーム情報が取得できます。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="pageToken">データの取得を開始する位置を指定するトークン</param>
		/// <param name="limit">データの取得件数</param>
		public IEnumerator ListMolds(
		        UnityAction<AsyncResult<EzListMoldsResult>> callback,
		        GameSession session,
                string namespaceName,
                string pageToken=null,
                long? limit=null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeMolds(
                    new DescribeMoldsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithPageToken(pageToken)
                        .WithLimit(limit)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzListMoldsResult>(
                            r.Result == null ? null : new EzListMoldsResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  `フォームの保存領域` と `プロパティID` を指定して保存したフォーム情報を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="moldName">フォームの保存領域の名前</param>
		public IEnumerator GetMold(
		        UnityAction<AsyncResult<EzGetMoldResult>> callback,
		        GameSession session,
                string namespaceName,
                string moldName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetMold(
                    new GetMoldRequest()
                        .WithNamespaceName(namespaceName)
                        .WithMoldName(moldName)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzGetMoldResult>(
                            r.Result == null ? null : new EzGetMoldResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  フォームの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="moldName">フォームの保存領域の名前</param>
		/// <param name="pageToken">データの取得を開始する位置を指定するトークン</param>
		/// <param name="limit">データの取得件数</param>
		public IEnumerator ListForms(
		        UnityAction<AsyncResult<EzListFormsResult>> callback,
		        GameSession session,
                string namespaceName,
                string moldName=null,
                string pageToken=null,
                long? limit=null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeForms(
                    new DescribeFormsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithMoldName(moldName)
                        .WithPageToken(pageToken)
                        .WithLimit(limit)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzListFormsResult>(
                            r.Result == null ? null : new EzListFormsResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  フォームを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="moldName">フォームの保存領域の名前</param>
		/// <param name="index">保存領域のインデックス</param>
		public IEnumerator GetForm(
		        UnityAction<AsyncResult<EzGetFormResult>> callback,
		        GameSession session,
                string namespaceName,
                string moldName,
                int index
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetForm(
                    new GetFormRequest()
                        .WithNamespaceName(namespaceName)
                        .WithMoldName(moldName)
                        .WithIndex(index)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzGetFormResult>(
                            r.Result == null ? null : new EzGetFormResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  フォームを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="moldName">フォームの保存領域の名前</param>
		/// <param name="index">保存領域のインデックス</param>
		/// <param name="keyId">署名の発行に使用する暗号鍵 のGRN</param>
		public IEnumerator GetFormWithSignature(
		        UnityAction<AsyncResult<EzGetFormWithSignatureResult>> callback,
		        GameSession session,
                string namespaceName,
                string moldName,
                int index,
                string keyId
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetFormWithSignature(
                    new GetFormWithSignatureRequest()
                        .WithNamespaceName(namespaceName)
                        .WithMoldName(moldName)
                        .WithIndex(index)
                        .WithKeyId(keyId)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzGetFormWithSignatureResult>(
                            r.Result == null ? null : new EzGetFormWithSignatureResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  フォームを更新<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="moldName">フォームの保存領域の名前</param>
		/// <param name="index">保存領域のインデックス</param>
		/// <param name="slots">編成するスロットのリスト</param>
		/// <param name="keyId">署名の発行に使用した GS2-Key の暗号鍵GRN</param>
		public IEnumerator SetForm(
		        UnityAction<AsyncResult<EzSetFormResult>> callback,
		        GameSession session,
                string namespaceName,
                string moldName,
                int index,
                List<EzSlotWithSignature> slots,
                string keyId
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.SetFormWithSignature(
                    new SetFormWithSignatureRequest()
                        .WithNamespaceName(namespaceName)
                        .WithMoldName(moldName)
                        .WithIndex(index)
                        .WithSlots(slots != null ? slots.Select(item => item?.ToModel()).ToList() : new List<SlotWithSignature>(new SlotWithSignature[]{}))
                        .WithKeyId(keyId)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzSetFormResult>(
                            r.Result == null ? null : new EzSetFormResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
	}
}