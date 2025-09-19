/*
 * Copyright 2016 Game Server Services, Inc. or its affiliates. All Rights
 * Reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License").
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

using Gs2.Gs2Account;
using Gs2.Unity.Gs2Account.Model;
using Gs2.Unity.Gs2Account.Result;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Unity.Util;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Account
{
	public class DisabledCertificateHandler : CertificateHandler {
		protected override bool ValidateCertificate(byte[] certificateData)
		{
			return true;
		}
	}

	[Preserve]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public partial class Client
	{
		private readonly Gs2.Unity.Util.Gs2Connection _connection;
		private readonly Gs2AccountWebSocketClient _client;
		private readonly Gs2AccountRestClient _restClient;

		public Client(Gs2.Unity.Util.Gs2Connection connection)
		{
			_connection = connection;
			_client = new Gs2AccountWebSocketClient(connection.WebSocketSession);
            _restClient = new Gs2AccountRestClient(connection.RestSession);
		}

        public IEnumerator Authentication(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Account.Result.EzAuthenticationResult>> callback,
                string namespaceName,
                string userId,
                string password,
                string keyId = null
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _restClient.Authentication(
                    new Gs2.Gs2Account.Request.AuthenticationRequest()
                        .WithNamespaceName(namespaceName)
                        .WithUserId(userId)
                        .WithKeyId(keyId)
                        .WithPassword(password),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Account.Result.EzAuthenticationResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Account.Result.EzAuthenticationResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator Create(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Account.Result.EzCreateResult>> callback,
                string namespaceName
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _client.CreateAccount(
                    new Gs2.Gs2Account.Request.CreateAccountRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Account.Result.EzCreateResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Account.Result.EzCreateResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator AddTakeOverSetting(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Account.Result.EzAddTakeOverSettingResult>> callback,
		        IGameSession session,
                string namespaceName,
                int type,
                string userIdentifier,
                string password
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.CreateTakeOver(
                    new Gs2.Gs2Account.Request.CreateTakeOverRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithType(type)
                        .WithUserIdentifier(userIdentifier)
                        .WithPassword(password),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Account.Result.EzAddTakeOverSettingResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Account.Result.EzAddTakeOverSettingResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator AddTakeOverSettingOpenIdConnect(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Account.Result.EzAddTakeOverSettingOpenIdConnectResult>> callback,
		        IGameSession session,
                string namespaceName,
                int type,
                string idToken
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.CreateTakeOverOpenIdConnect(
                    new Gs2.Gs2Account.Request.CreateTakeOverOpenIdConnectRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithType(type)
                        .WithIdToken(idToken),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Account.Result.EzAddTakeOverSettingOpenIdConnectResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Account.Result.EzAddTakeOverSettingOpenIdConnectResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator DeleteTakeOverSetting(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Account.Result.EzDeleteTakeOverSettingResult>> callback,
		        IGameSession session,
                string namespaceName,
                int type
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.DeleteTakeOver(
                    new Gs2.Gs2Account.Request.DeleteTakeOverRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithType(type),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Account.Result.EzDeleteTakeOverSettingResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Account.Result.EzDeleteTakeOverSettingResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator DoTakeOver(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Account.Result.EzDoTakeOverResult>> callback,
                string namespaceName,
                int type,
                string userIdentifier,
                string password
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _client.DoTakeOver(
                    new Gs2.Gs2Account.Request.DoTakeOverRequest()
                        .WithNamespaceName(namespaceName)
                        .WithType(type)
                        .WithUserIdentifier(userIdentifier)
                        .WithPassword(password),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Account.Result.EzDoTakeOverResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Account.Result.EzDoTakeOverResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator DoTakeOverOpenIdConnect(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Account.Result.EzDoTakeOverOpenIdConnectResult>> callback,
                string namespaceName,
                int type,
                string idToken
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _client.DoTakeOverOpenIdConnect(
                    new Gs2.Gs2Account.Request.DoTakeOverOpenIdConnectRequest()
                        .WithNamespaceName(namespaceName)
                        .WithType(type)
                        .WithIdToken(idToken),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Account.Result.EzDoTakeOverOpenIdConnectResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Account.Result.EzDoTakeOverOpenIdConnectResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator Get(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Account.Result.EzGetResult>> callback,
		        IGameSession session,
                string namespaceName,
                int type
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.GetTakeOver(
                    new Gs2.Gs2Account.Request.GetTakeOverRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithType(type),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Account.Result.EzGetResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Account.Result.EzGetResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetAuthorizationUrl(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Account.Result.EzGetAuthorizationUrlResult>> callback,
                string namespaceName,
                int type
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _client.GetAuthorizationUrl(
                    new Gs2.Gs2Account.Request.GetAuthorizationUrlRequest()
                        .WithNamespaceName(namespaceName)
                        .WithType(type),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Account.Result.EzGetAuthorizationUrlResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Account.Result.EzGetAuthorizationUrlResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListTakeOverSettings(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Account.Result.EzListTakeOverSettingsResult>> callback,
		        IGameSession session,
                string namespaceName,
                string pageToken = null,
                int? limit = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.DescribeTakeOvers(
                    new Gs2.Gs2Account.Request.DescribeTakeOversRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Account.Result.EzListTakeOverSettingsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Account.Result.EzListTakeOverSettingsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator UpdateTakeOverSetting(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Account.Result.EzUpdateTakeOverSettingResult>> callback,
		        IGameSession session,
                string namespaceName,
                int type,
                string oldPassword,
                string password
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.UpdateTakeOver(
                    new Gs2.Gs2Account.Request.UpdateTakeOverRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithType(type)
                        .WithOldPassword(oldPassword)
                        .WithPassword(password),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Account.Result.EzUpdateTakeOverSettingResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Account.Result.EzUpdateTakeOverSettingResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator AddPlatformIdSetting(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Account.Result.EzAddPlatformIdSettingResult>> callback,
		        IGameSession session,
                string namespaceName,
                int type,
                string userIdentifier
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.CreatePlatformId(
                    new Gs2.Gs2Account.Request.CreatePlatformIdRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithType(type)
                        .WithUserIdentifier(userIdentifier),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Account.Result.EzAddPlatformIdSettingResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Account.Result.EzAddPlatformIdSettingResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator DeletePlatformIdSetting(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Account.Result.EzDeletePlatformIdSettingResult>> callback,
		        IGameSession session,
                string namespaceName,
                int type
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.DeletePlatformId(
                    new Gs2.Gs2Account.Request.DeletePlatformIdRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithType(type),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Account.Result.EzDeletePlatformIdSettingResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Account.Result.EzDeletePlatformIdSettingResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator FindPlatformUser(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Account.Result.EzFindPlatformUserResult>> callback,
		        IGameSession session,
                string namespaceName,
                int type,
                string userIdentifier
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.FindPlatformId(
                    new Gs2.Gs2Account.Request.FindPlatformIdRequest()
                        .WithNamespaceName(namespaceName)
                        .WithType(type)
                        .WithUserIdentifier(userIdentifier),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Account.Result.EzFindPlatformUserResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Account.Result.EzFindPlatformUserResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetPlatformId(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Account.Result.EzGetPlatformIdResult>> callback,
		        IGameSession session,
                string namespaceName,
                int type
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.GetPlatformId(
                    new Gs2.Gs2Account.Request.GetPlatformIdRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithType(type),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Account.Result.EzGetPlatformIdResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Account.Result.EzGetPlatformIdResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListPlatformIdSettings(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Account.Result.EzListPlatformIdSettingsResult>> callback,
		        IGameSession session,
                string namespaceName,
                string pageToken = null,
                int? limit = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.DescribePlatformIds(
                    new Gs2.Gs2Account.Request.DescribePlatformIdsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Account.Result.EzListPlatformIdSettingsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Account.Result.EzListPlatformIdSettingsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}