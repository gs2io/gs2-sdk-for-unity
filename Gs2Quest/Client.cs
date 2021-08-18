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

using Gs2.Gs2Quest;
using Gs2.Unity.Gs2Quest.Model;
using Gs2.Unity.Gs2Quest.Result;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Gs2Quest;
using Gs2.Gs2Quest.Model;
using Gs2.Gs2Quest.Request;
using Gs2.Gs2Quest.Result;
using Gs2.Unity.Gs2Quest.Model;
using Gs2.Unity.Gs2Quest.Result;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Unity.Util;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Quest
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
		private readonly Gs2.Unity.Util.Profile _profile;
		private readonly Gs2QuestWebSocketClient _client;
		private readonly Gs2QuestRestClient _restClient;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2QuestWebSocketClient(profile.Gs2Session);
			if (profile.checkRevokeCertificate)
			{
				_restClient = new Gs2QuestRestClient(profile.Gs2RestSession);
			}
			else
			{
				_restClient = new Gs2QuestRestClient(profile.Gs2RestSession, new DisabledCertificateHandler());
			}
		}

        public IEnumerator DeleteProgress(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Quest.Result.EzDeleteProgressResult>> callback,
		        GameSession session,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DeleteProgress(
                    new Gs2.Gs2Quest.Request.DeleteProgressRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Quest.Result.EzDeleteProgressResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Quest.Result.EzDeleteProgressResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator End(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Quest.Result.EzEndResult>> callback,
		        GameSession session,
                string namespaceName,
                string transactionId,
                List<Gs2.Unity.Gs2Quest.Model.EzReward> rewards = null,
                bool? isComplete = null,
                List<Gs2.Unity.Gs2Quest.Model.EzConfig> config = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.End(
                    new Gs2.Gs2Quest.Request.EndRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithRewards(rewards?.Select(v => {
                            return v?.ToModel();
                        }).ToArray())
                        .WithTransactionId(transactionId)
                        .WithIsComplete(isComplete)
                        .WithConfig(config?.Select(v => {
                            return v?.ToModel();
                        }).ToArray()),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Quest.Result.EzEndResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Quest.Result.EzEndResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetProgress(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Quest.Result.EzGetProgressResult>> callback,
		        GameSession session,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.GetProgress(
                    new Gs2.Gs2Quest.Request.GetProgressRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Quest.Result.EzGetProgressResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Quest.Result.EzGetProgressResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator Start(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Quest.Result.EzStartResult>> callback,
		        GameSession session,
                string namespaceName,
                string questGroupName,
                string questName,
                bool? force = null,
                List<Gs2.Unity.Gs2Quest.Model.EzConfig> config = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.Start(
                    new Gs2.Gs2Quest.Request.StartRequest()
                        .WithNamespaceName(namespaceName)
                        .WithQuestGroupName(questGroupName)
                        .WithQuestName(questName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithForce(force)
                        .WithConfig(config?.Select(v => {
                            return v?.ToModel();
                        }).ToArray()),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Quest.Result.EzStartResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Quest.Result.EzStartResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator DescribeCompletedQuestLists(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Quest.Result.EzDescribeCompletedQuestListsResult>> callback,
		        GameSession session,
                string namespaceName,
                int limit,
                string pageToken = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeCompletedQuestLists(
                    new Gs2.Gs2Quest.Request.DescribeCompletedQuestListsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Quest.Result.EzDescribeCompletedQuestListsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Quest.Result.EzDescribeCompletedQuestListsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetCompletedQuestList(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Quest.Result.EzGetCompletedQuestListResult>> callback,
		        GameSession session,
                string namespaceName,
                string questGroupName = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.GetCompletedQuestList(
                    new Gs2.Gs2Quest.Request.GetCompletedQuestListRequest()
                        .WithNamespaceName(namespaceName)
                        .WithQuestGroupName(questGroupName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Quest.Result.EzGetCompletedQuestListResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Quest.Result.EzGetCompletedQuestListResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetQuestGroup(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Quest.Result.EzGetQuestGroupResult>> callback,
                string namespaceName,
                string questGroupName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.GetQuestGroupModel(
                    new Gs2.Gs2Quest.Request.GetQuestGroupModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithQuestGroupName(questGroupName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Quest.Result.EzGetQuestGroupResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Quest.Result.EzGetQuestGroupResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListQuestGroups(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Quest.Result.EzListQuestGroupsResult>> callback,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.DescribeQuestGroupModels(
                    new Gs2.Gs2Quest.Request.DescribeQuestGroupModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Quest.Result.EzListQuestGroupsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Quest.Result.EzListQuestGroupsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetQuest(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Quest.Result.EzGetQuestResult>> callback,
                string namespaceName,
                string questGroupName,
                string questName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.GetQuestModel(
                    new Gs2.Gs2Quest.Request.GetQuestModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithQuestGroupName(questGroupName)
                        .WithQuestName(questName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Quest.Result.EzGetQuestResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Quest.Result.EzGetQuestResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListQuests(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Quest.Result.EzListQuestsResult>> callback,
                string namespaceName,
                string questGroupName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.DescribeQuestModels(
                    new Gs2.Gs2Quest.Request.DescribeQuestModelsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithQuestGroupName(questGroupName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Quest.Result.EzListQuestsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Quest.Result.EzListQuestsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}