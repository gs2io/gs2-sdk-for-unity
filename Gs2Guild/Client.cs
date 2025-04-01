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

using Gs2.Gs2Guild;
using Gs2.Unity.Gs2Guild.Model;
using Gs2.Unity.Gs2Guild.Result;
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
namespace Gs2.Unity.Gs2Guild
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
		private readonly Gs2GuildWebSocketClient _client;
		private readonly Gs2GuildRestClient _restClient;

		public Client(Gs2.Unity.Util.Gs2Connection connection)
		{
			_connection = connection;
			_client = new Gs2GuildWebSocketClient(connection.WebSocketSession);
            _restClient = new Gs2GuildRestClient(connection.RestSession);
		}

        public IEnumerator GetGuildModel(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Guild.Result.EzGetGuildModelResult>> callback,
                string namespaceName,
                string guildModelName
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _restClient.GetGuildModel(
                    new Gs2.Gs2Guild.Request.GetGuildModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithGuildModelName(guildModelName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Guild.Result.EzGetGuildModelResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Guild.Result.EzGetGuildModelResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListGuildModels(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Guild.Result.EzListGuildModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _connection.Run(
                callback,
                null,
                cb => _restClient.DescribeGuildModels(
                    new Gs2.Gs2Guild.Request.DescribeGuildModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Guild.Result.EzListGuildModelsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Guild.Result.EzListGuildModelsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator Assume(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Guild.Result.EzAssumeResult>> callback,
		        IGameSession session,
                string namespaceName,
                string guildModelName,
                string guildName = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.Assume(
                    new Gs2.Gs2Guild.Request.AssumeRequest()
                        .WithNamespaceName(namespaceName)
                        .WithGuildModelName(guildModelName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithGuildName(guildName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Guild.Result.EzAssumeResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Guild.Result.EzAssumeResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator BatchUpdateGuildMemberRole(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Guild.Result.EzBatchUpdateGuildMemberRoleResult>> callback,
		        IGameSession session,
                string namespaceName,
                string guildModelName,
                List<Gs2.Unity.Gs2Guild.Model.EzMember> members
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.BatchUpdateMemberRole(
                    new Gs2.Gs2Guild.Request.BatchUpdateMemberRoleRequest()
                        .WithNamespaceName(namespaceName)
                        .WithGuildModelName(guildModelName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithMembers(members?.Select(v => {
                            return v?.ToModel();
                        }).ToArray()),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Guild.Result.EzBatchUpdateGuildMemberRoleResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Guild.Result.EzBatchUpdateGuildMemberRoleResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator CreateGuild(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Guild.Result.EzCreateGuildResult>> callback,
		        IGameSession session,
                string namespaceName,
                string guildModelName,
                string displayName,
                string joinPolicy,
                int? attribute1 = null,
                int? attribute2 = null,
                int? attribute3 = null,
                int? attribute4 = null,
                int? attribute5 = null,
                string metadata = null,
                string memberMetadata = null,
                List<Gs2.Unity.Gs2Guild.Model.EzRoleModel> customRoles = null,
                string guildMemberDefaultRole = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.CreateGuild(
                    new Gs2.Gs2Guild.Request.CreateGuildRequest()
                        .WithNamespaceName(namespaceName)
                        .WithGuildModelName(guildModelName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithDisplayName(displayName)
                        .WithAttribute1(attribute1)
                        .WithAttribute2(attribute2)
                        .WithAttribute3(attribute3)
                        .WithAttribute4(attribute4)
                        .WithAttribute5(attribute5)
                        .WithMetadata(metadata)
                        .WithMemberMetadata(memberMetadata)
                        .WithJoinPolicy(joinPolicy)
                        .WithCustomRoles(customRoles?.Select(v => {
                            return v?.ToModel();
                        }).ToArray())
                        .WithGuildMemberDefaultRole(guildMemberDefaultRole),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Guild.Result.EzCreateGuildResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Guild.Result.EzCreateGuildResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator DeleteGuild(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Guild.Result.EzDeleteGuildResult>> callback,
		        IGameSession session,
                string namespaceName,
                string guildModelName
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.DeleteGuild(
                    new Gs2.Gs2Guild.Request.DeleteGuildRequest()
                        .WithNamespaceName(namespaceName)
                        .WithGuildModelName(guildModelName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Guild.Result.EzDeleteGuildResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Guild.Result.EzDeleteGuildResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator DeleteMemberFromGuild(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Guild.Result.EzDeleteMemberFromGuildResult>> callback,
		        IGameSession session,
                string namespaceName,
                string guildModelName,
                string targetUserId
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.DeleteMember(
                    new Gs2.Gs2Guild.Request.DeleteMemberRequest()
                        .WithNamespaceName(namespaceName)
                        .WithGuildModelName(guildModelName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithTargetUserId(targetUserId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Guild.Result.EzDeleteMemberFromGuildResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Guild.Result.EzDeleteMemberFromGuildResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetGuild(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Guild.Result.EzGetGuildResult>> callback,
		        IGameSession session,
                string namespaceName,
                string guildModelName,
                string guildName = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.GetGuild(
                    new Gs2.Gs2Guild.Request.GetGuildRequest()
                        .WithNamespaceName(namespaceName)
                        .WithGuildModelName(guildModelName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithGuildName(guildName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Guild.Result.EzGetGuildResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Guild.Result.EzGetGuildResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListGuilds(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Guild.Result.EzListGuildsResult>> callback,
		        IGameSession session,
                string namespaceName,
                string displayName = null,
                List<int> attributes1 = null,
                List<int> attributes2 = null,
                List<int> attributes3 = null,
                List<int> attributes4 = null,
                List<int> attributes5 = null,
                List<string> joinPolicies = null,
                bool? includeFullMembersGuild = null,
                string orderBy = null,
                string pageToken = null,
                int? limit = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.SearchGuilds(
                    new Gs2.Gs2Guild.Request.SearchGuildsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithDisplayName(displayName)
                        .WithAttributes1(attributes1?.Select(v => {
                            return v;
                        }).ToArray())
                        .WithAttributes2(attributes2?.Select(v => {
                            return v;
                        }).ToArray())
                        .WithAttributes3(attributes3?.Select(v => {
                            return v;
                        }).ToArray())
                        .WithAttributes4(attributes4?.Select(v => {
                            return v;
                        }).ToArray())
                        .WithAttributes5(attributes5?.Select(v => {
                            return v;
                        }).ToArray())
                        .WithJoinPolicies(joinPolicies?.Select(v => {
                            return v;
                        }).ToArray())
                        .WithIncludeFullMembersGuild(includeFullMembersGuild)
                        .WithOrderBy(orderBy)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Guild.Result.EzListGuildsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Guild.Result.EzListGuildsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator UpdateGuild(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Guild.Result.EzUpdateGuildResult>> callback,
		        IGameSession session,
                string namespaceName,
                string guildModelName,
                string displayName,
                string joinPolicy,
                int? attribute1 = null,
                int? attribute2 = null,
                int? attribute3 = null,
                int? attribute4 = null,
                int? attribute5 = null,
                string metadata = null,
                List<Gs2.Unity.Gs2Guild.Model.EzRoleModel> customRoles = null,
                string guildMemberDefaultRole = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.UpdateGuild(
                    new Gs2.Gs2Guild.Request.UpdateGuildRequest()
                        .WithNamespaceName(namespaceName)
                        .WithGuildModelName(guildModelName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithDisplayName(displayName)
                        .WithAttribute1(attribute1)
                        .WithAttribute2(attribute2)
                        .WithAttribute3(attribute3)
                        .WithAttribute4(attribute4)
                        .WithAttribute5(attribute5)
                        .WithMetadata(metadata)
                        .WithJoinPolicy(joinPolicy)
                        .WithCustomRoles(customRoles?.Select(v => {
                            return v?.ToModel();
                        }).ToArray())
                        .WithGuildMemberDefaultRole(guildMemberDefaultRole),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Guild.Result.EzUpdateGuildResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Guild.Result.EzUpdateGuildResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator UpdateGuildMemberRole(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Guild.Result.EzUpdateGuildMemberRoleResult>> callback,
		        IGameSession session,
                string namespaceName,
                string guildModelName,
                string targetUserId,
                string roleName
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.UpdateMemberRole(
                    new Gs2.Gs2Guild.Request.UpdateMemberRoleRequest()
                        .WithNamespaceName(namespaceName)
                        .WithGuildModelName(guildModelName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithTargetUserId(targetUserId)
                        .WithRoleName(roleName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Guild.Result.EzUpdateGuildMemberRoleResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Guild.Result.EzUpdateGuildMemberRoleResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator AcceptRequest(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Guild.Result.EzAcceptRequestResult>> callback,
		        IGameSession session,
                string namespaceName,
                string guildModelName,
                string fromUserId
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.AcceptRequest(
                    new Gs2.Gs2Guild.Request.AcceptRequestRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithGuildModelName(guildModelName)
                        .WithFromUserId(fromUserId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Guild.Result.EzAcceptRequestResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Guild.Result.EzAcceptRequestResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetReceiveRequest(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Guild.Result.EzGetReceiveRequestResult>> callback,
		        IGameSession session,
                string namespaceName,
                string guildModelName,
                string fromUserId
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.GetReceiveRequest(
                    new Gs2.Gs2Guild.Request.GetReceiveRequestRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithGuildModelName(guildModelName)
                        .WithFromUserId(fromUserId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Guild.Result.EzGetReceiveRequestResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Guild.Result.EzGetReceiveRequestResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListReceiveRequests(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Guild.Result.EzListReceiveRequestsResult>> callback,
		        IGameSession session,
                string namespaceName,
                string guildModelName,
                string pageToken = null,
                int? limit = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.DescribeReceiveRequests(
                    new Gs2.Gs2Guild.Request.DescribeReceiveRequestsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithGuildModelName(guildModelName)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Guild.Result.EzListReceiveRequestsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Guild.Result.EzListReceiveRequestsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator RejectRequest(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Guild.Result.EzRejectRequestResult>> callback,
		        IGameSession session,
                string namespaceName,
                string guildModelName,
                string fromUserId
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.RejectRequest(
                    new Gs2.Gs2Guild.Request.RejectRequestRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithGuildModelName(guildModelName)
                        .WithFromUserId(fromUserId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Guild.Result.EzRejectRequestResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Guild.Result.EzRejectRequestResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator CancelRequest(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Guild.Result.EzCancelRequestResult>> callback,
		        IGameSession session,
                string namespaceName,
                string guildModelName,
                string targetGuildName
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.DeleteRequest(
                    new Gs2.Gs2Guild.Request.DeleteRequestRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithGuildModelName(guildModelName)
                        .WithTargetGuildName(targetGuildName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Guild.Result.EzCancelRequestResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Guild.Result.EzCancelRequestResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetSendRequest(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Guild.Result.EzGetSendRequestResult>> callback,
		        IGameSession session,
                string namespaceName,
                string guildModelName,
                string targetGuildName
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.GetSendRequest(
                    new Gs2.Gs2Guild.Request.GetSendRequestRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithGuildModelName(guildModelName)
                        .WithTargetGuildName(targetGuildName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Guild.Result.EzGetSendRequestResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Guild.Result.EzGetSendRequestResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListSendRequests(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Guild.Result.EzListSendRequestsResult>> callback,
		        IGameSession session,
                string namespaceName,
                string guildModelName,
                string pageToken = null,
                int? limit = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.DescribeSendRequests(
                    new Gs2.Gs2Guild.Request.DescribeSendRequestsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithGuildModelName(guildModelName)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Guild.Result.EzListSendRequestsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Guild.Result.EzListSendRequestsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator SendRequest(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Guild.Result.EzSendRequestResult>> callback,
		        IGameSession session,
                string namespaceName,
                string guildModelName,
                string targetGuildName,
                string metadata = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.SendRequest(
                    new Gs2.Gs2Guild.Request.SendRequestRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithGuildModelName(guildModelName)
                        .WithTargetGuildName(targetGuildName)
                        .WithMetadata(metadata),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Guild.Result.EzSendRequestResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Guild.Result.EzSendRequestResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetJoinedGuild(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Guild.Result.EzGetJoinedGuildResult>> callback,
		        IGameSession session,
                string namespaceName,
                string guildModelName,
                string guildName = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.GetJoinedGuild(
                    new Gs2.Gs2Guild.Request.GetJoinedGuildRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithGuildModelName(guildModelName)
                        .WithGuildName(guildName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Guild.Result.EzGetJoinedGuildResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Guild.Result.EzGetJoinedGuildResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListJoinedGuilds(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Guild.Result.EzListJoinedGuildsResult>> callback,
		        IGameSession session,
                string namespaceName,
                string guildModelName = null,
                string pageToken = null,
                int? limit = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.DescribeJoinedGuilds(
                    new Gs2.Gs2Guild.Request.DescribeJoinedGuildsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithGuildModelName(guildModelName)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Guild.Result.EzListJoinedGuildsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Guild.Result.EzListJoinedGuildsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator UpdateMemberMetadata(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Guild.Result.EzUpdateMemberMetadataResult>> callback,
		        IGameSession session,
                string namespaceName,
                string guildModelName,
                string guildName = null,
                string metadata = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.UpdateMemberMetadata(
                    new Gs2.Gs2Guild.Request.UpdateMemberMetadataRequest()
                        .WithNamespaceName(namespaceName)
                        .WithGuildModelName(guildModelName)
                        .WithGuildName(guildName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithMetadata(metadata),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Guild.Result.EzUpdateMemberMetadataResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Guild.Result.EzUpdateMemberMetadataResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator WithdrawGuild(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Guild.Result.EzWithdrawGuildResult>> callback,
		        IGameSession session,
                string namespaceName,
                string guildModelName,
                string guildName = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.Withdrawal(
                    new Gs2.Gs2Guild.Request.WithdrawalRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithGuildModelName(guildModelName)
                        .WithGuildName(guildName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Guild.Result.EzWithdrawGuildResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Guild.Result.EzWithdrawGuildResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator AddIgnoreUser(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Guild.Result.EzAddIgnoreUserResult>> callback,
		        IGameSession session,
                string namespaceName,
                string guildModelName,
                string userId
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.AddIgnoreUser(
                    new Gs2.Gs2Guild.Request.AddIgnoreUserRequest()
                        .WithNamespaceName(namespaceName)
                        .WithGuildModelName(guildModelName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithUserId(userId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Guild.Result.EzAddIgnoreUserResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Guild.Result.EzAddIgnoreUserResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator DeleteIgnoreUser(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Guild.Result.EzDeleteIgnoreUserResult>> callback,
		        IGameSession session,
                string namespaceName,
                string guildModelName,
                string userId
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.DeleteIgnoreUser(
                    new Gs2.Gs2Guild.Request.DeleteIgnoreUserRequest()
                        .WithNamespaceName(namespaceName)
                        .WithGuildModelName(guildModelName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithUserId(userId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Guild.Result.EzDeleteIgnoreUserResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Guild.Result.EzDeleteIgnoreUserResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetIgnoreUser(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Guild.Result.EzGetIgnoreUserResult>> callback,
		        IGameSession session,
                string namespaceName,
                string guildModelName,
                string userId
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _client.GetIgnoreUser(
                    new Gs2.Gs2Guild.Request.GetIgnoreUserRequest()
                        .WithNamespaceName(namespaceName)
                        .WithGuildModelName(guildModelName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithUserId(userId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Guild.Result.EzGetIgnoreUserResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Guild.Result.EzGetIgnoreUserResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListIgnoreUsers(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Guild.Result.EzListIgnoreUsersResult>> callback,
		        IGameSession session,
                string namespaceName,
                string guildModelName,
                string pageToken = null,
                int? limit = null
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.DescribeIgnoreUsers(
                    new Gs2.Gs2Guild.Request.DescribeIgnoreUsersRequest()
                        .WithNamespaceName(namespaceName)
                        .WithGuildModelName(guildModelName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Guild.Result.EzListIgnoreUsersResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Guild.Result.EzListIgnoreUsersResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetLastGuildMasterActivity(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Guild.Result.EzGetLastGuildMasterActivityResult>> callback,
		        IGameSession session,
                string namespaceName,
                string guildModelName
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.GetLastGuildMasterActivity(
                    new Gs2.Gs2Guild.Request.GetLastGuildMasterActivityRequest()
                        .WithNamespaceName(namespaceName)
                        .WithGuildModelName(guildModelName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Guild.Result.EzGetLastGuildMasterActivityResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Guild.Result.EzGetLastGuildMasterActivityResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator PromoteSeniorMember(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Guild.Result.EzPromoteSeniorMemberResult>> callback,
		        IGameSession session,
                string namespaceName,
                string guildModelName
        )
		{
            yield return _connection.Run(
                callback,
		        session,
                cb => _restClient.PromoteSeniorMember(
                    new Gs2.Gs2Guild.Request.PromoteSeniorMemberRequest()
                        .WithNamespaceName(namespaceName)
                        .WithGuildModelName(guildModelName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Guild.Result.EzPromoteSeniorMemberResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Guild.Result.EzPromoteSeniorMemberResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}