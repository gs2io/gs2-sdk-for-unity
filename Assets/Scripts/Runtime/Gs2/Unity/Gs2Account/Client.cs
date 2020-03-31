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
using Gs2.Gs2Account;
using Gs2.Gs2Account.Model;
using Gs2.Gs2Account.Request;
using Gs2.Gs2Account.Result;
using Gs2.Unity.Gs2Account.Model;
using Gs2.Unity.Gs2Account.Result;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Unity.Util;
using UnityEngine.Events;

namespace Gs2.Unity.Gs2Account
{
	public class Client
	{
		private readonly Gs2.Unity.Util.Profile _profile;
		private readonly Gs2AccountWebSocketClient _client;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2AccountWebSocketClient(profile.Gs2Session);
		}

		/// <summary>
		///  ゲームプレイヤーを識別するアカウントを新規作成<br />
		///    <br />
		///    このAPIの実行に成功すると、作成したアカウントの情報が返ります。<br />
		///    返ったアカウント情報のうち、認証処理に使用するユーザIDとパスワードを永続化してください。<br />
		///    <br />
		///    ここで発行されるパスワードはランダム値であり、ゲームプレイヤーの任意の値を指定することはできません。<br />
		///    `引き継ぎ設定` としてゲームプレイヤーにとってわかりやすい識別子を登録することができます。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		public IEnumerator Create(
		        UnityAction<AsyncResult<EzCreateResult>> callback,
                string namespaceName
        )
		{
            yield return _client.CreateAccount(
                new CreateAccountRequest()
                    .WithNamespaceName(namespaceName),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzCreateResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzCreateResult>(
                                new EzCreateResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}

		/// <summary>
		///  アカウントの認証<br />
		///    <br />
		///    create 関数で発行したユーザID・パスワードを使用してゲームプレイヤーの認証を行います。<br />
		///    認証が完了すると `アカウント認証情報` と `署名` が発行されます。<br />
		///    `アカウント認証情報` と `署名` を GS2-Auth の loginBySignature にわたすことで、GS2の各サービスにアクセスするための `アクセストークン` を得ることができます。<br />
		///    <br />
		///    `アカウント認証情報` と `署名` は1時間の有効期限が存在します。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="userId">アカウントID</param>
		/// <param name="keyId">認証トークンの暗号化に使用する暗号鍵 のGRN</param>
		/// <param name="password">パスワード</param>
		public IEnumerator Authentication(
		        UnityAction<AsyncResult<EzAuthenticationResult>> callback,
                string namespaceName,
                string userId,
                string keyId,
                string password
        )
		{
            yield return _client.Authentication(
                new AuthenticationRequest()
                    .WithNamespaceName(namespaceName)
                    .WithUserId(userId)
                    .WithKeyId(keyId)
                    .WithPassword(password),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzAuthenticationResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzAuthenticationResult>(
                                new EzAuthenticationResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}

		/// <summary>
		///  `引き継ぎ設定` を追加<br />
		///    <br />
		///    `引き継ぎ設定` は機種変更などを行ったときにアカウントの引き継ぎをできるようにする設定です。<br />
		///    `引き継ぎ設定` は `引き継ぎ用ユーザーID` と `引き継ぎ用パスワード` の組み合わせで実行できるようにします。<br />
		///    <br />
		///    `スロット番号` に異なる値を指定することで、1つのアカウントに対して複数の ``引き継ぎ設定` ` を保持できます。<br />
		///    たとえば、 `スロット番号:0` にメールアドレス・パスワード を、 `スロット番号:1` にソーシャルメディアのID情報を格納するようにし、<br />
		///    ゲームプレイヤーは好みの引き継ぎ手段を選択できるようにする。といった運用が可能です。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="type">スロット番号</param>
		/// <param name="userIdentifier">引き継ぎ用ユーザーID</param>
		/// <param name="password">パスワード</param>
		public IEnumerator AddTakeOverSetting(
		        UnityAction<AsyncResult<EzAddTakeOverSettingResult>> callback,
		        GameSession session,
                string namespaceName,
                int type,
                string userIdentifier,
                string password
        )
		{
            yield return _client.CreateTakeOver(
                new CreateTakeOverRequest()
                    .WithNamespaceName(namespaceName)
                    .WithType(type)
                    .WithUserIdentifier(userIdentifier)
                    .WithPassword(password)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzAddTakeOverSettingResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzAddTakeOverSettingResult>(
                                new EzAddTakeOverSettingResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}

		/// <summary>
		///  設定されている `引き継ぎ設定` の一覧を取得<br />
		///    <br />
		///    ゲームプレイヤーが設定した `引き継ぎ設定` の一覧を取得できます。<br />
		///    設定されている `引き継ぎ用パスワード` の値は取得できません。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="pageToken">データの取得を開始する位置を指定するトークン</param>
		/// <param name="limit">データの取得件数</param>
		public IEnumerator ListTakeOverSettings(
		        UnityAction<AsyncResult<EzListTakeOverSettingsResult>> callback,
		        GameSession session,
                string namespaceName,
                string pageToken=null,
                long? limit=null
        )
		{
            yield return _client.DescribeTakeOvers(
                new DescribeTakeOversRequest()
                    .WithNamespaceName(namespaceName)
                    .WithPageToken(pageToken)
                    .WithLimit(limit)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzListTakeOverSettingsResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzListTakeOverSettingsResult>(
                                new EzListTakeOverSettingsResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}

		/// <summary>
		///  ``引き継ぎ設定` ` のパスワードを変更する<br />
		///    <br />
		///    このAPIを経由して `引き継ぎ用パスワード` を更新するためには、すでに設定されている `引き継ぎ用パスワード` を知っていなければ実行できません。<br />
		///    セキュアな `引き継ぎ設定` の更新を実現したい場合に使用します。<br />
		///    <br />
		///    このAPIを使用する際には、 `引き継ぎ設定` の削除APIのアクセス権限を剥奪することを忘れないようにしてください。<br />
		///    ゲームプレイヤーが自分の `引き継ぎ設定` の削除するにはパスワードの認証が必要ありません。<br />
		///    削除して再作成することで、実質的に `引き継ぎ用パスワード` の変更ができてしまいます。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="type">スロット番号</param>
		/// <param name="oldPassword">古いパスワード</param>
		/// <param name="password">新しいパスワード</param>
		public IEnumerator UpdateTakeOverSetting(
		        UnityAction<AsyncResult<EzUpdateTakeOverSettingResult>> callback,
		        GameSession session,
                string namespaceName,
                int type,
                string oldPassword,
                string password
        )
		{
            yield return _client.UpdateTakeOver(
                new UpdateTakeOverRequest()
                    .WithNamespaceName(namespaceName)
                    .WithType(type)
                    .WithOldPassword(oldPassword)
                    .WithPassword(password)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzUpdateTakeOverSettingResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzUpdateTakeOverSettingResult>(
                                new EzUpdateTakeOverSettingResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}

		/// <summary>
		///  `引き継ぎ設定` の削除<br />
		///    <br />
		///    設定されている `引き継ぎ設定` を削除します。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="type">スロット番号</param>
		public IEnumerator DeleteTakeOverSetting(
		        UnityAction<AsyncResult<EzDeleteTakeOverSettingResult>> callback,
		        GameSession session,
                string namespaceName,
                int type
        )
		{
            yield return _client.DeleteTakeOver(
                new DeleteTakeOverRequest()
                    .WithNamespaceName(namespaceName)
                    .WithType(type)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzDeleteTakeOverSettingResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzDeleteTakeOverSettingResult>(
                                new EzDeleteTakeOverSettingResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}

		/// <summary>
		///  引き継ぎを実行<br />
		///    <br />
		///    指定された `引き継ぎ用ユーザID` と `引き継ぎ用パスワード` が一致していた場合、設定されたアカウント情報を応答します。<br />
		///    応答されたアカウント情報から `ユーザID` と `パスワード` を永続化して利用してください。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="type">スロット番号</param>
		/// <param name="userIdentifier">引き継ぎ用ユーザーID</param>
		/// <param name="password">パスワード</param>
		public IEnumerator DoTakeOver(
		        UnityAction<AsyncResult<EzDoTakeOverResult>> callback,
                string namespaceName,
                int type,
                string userIdentifier,
                string password
        )
		{
            yield return _client.DoTakeOver(
                new DoTakeOverRequest()
                    .WithNamespaceName(namespaceName)
                    .WithType(type)
                    .WithUserIdentifier(userIdentifier)
                    .WithPassword(password),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzDoTakeOverResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzDoTakeOverResult>(
                                new EzDoTakeOverResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}
	}
}