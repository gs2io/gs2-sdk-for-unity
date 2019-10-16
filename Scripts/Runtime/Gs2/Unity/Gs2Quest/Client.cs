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

namespace Gs2.Unity.Gs2Quest
{
	public class Client
	{
		private readonly Gs2.Unity.Util.Profile _profile;
		private readonly Gs2QuestWebSocketClient _client;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2QuestWebSocketClient(profile.Gs2Session);
		}

		/// <summary>
		///  クエストの開始を宣言<br />
		///    <br />
		///    すでに同一ゲームプレイヤーで開始済みのクエストがある場合は失敗します。<br />
		///    それでも強制的に開始したい場合は `force` オプションに true を指定してください。<br />
		///    <br />
		///    クエストの開始が完了すると、そのクエストで得られる最大報酬に関する情報が応答されます。<br />
		///    その内容をクエスト内の演出で排出してください。<br />
		///    その際に、応答に含まれる乱数シードを使用してゲームプレイに再現性があるように設計しておくと、アプリで乱数起因の不具合が発生したときに調査しやすくなります。<br />
		///    <br />
		///    進行中のクエストを一意に特定するためのIDとして `クエストトランザクションID` が応答されます。<br />
		///    クエストの完了を報告する際には `クエストトランザクションID` を指定することで、どのクエストに対する完了報告かを識別します。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">カテゴリ名</param>
		/// <param name="questGroupName">クエストグループ名</param>
		/// <param name="questName">クエストモデル名</param>
		/// <param name="force">すでに開始しているクエストがある場合にそれを破棄して開始するか</param>
		/// <param name="config">スタンプシートの変数に適用する設定値</param>
		public IEnumerator Start(
		        UnityAction<AsyncResult<EzStartResult>> callback,
		        GameSession session,
                string namespaceName,
                string questGroupName,
                string questName,
                bool? force=null,
                List<EzConfig> config=null
        )
		{
            yield return _client.Start(
                new StartRequest()
                    .WithNamespaceName(namespaceName)
                    .WithQuestGroupName(questGroupName)
                    .WithQuestName(questName)
                    .WithForce(force)
                    .WithConfig(config != null ? config.Select(item => item.ToModel()).ToList() : new List<Config>(new Config[]{}))
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzStartResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzStartResult>(
                                new EzStartResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}

		/// <summary>
		///  クエストの完了を報告<br />
		///    <br />
		///    クエストが完了したことを報告します。その際に `isComplete` にクエストに成功したか、失敗したかを渡します。<br />
		///    クエストに成功した場合は `rewards` にクエスト内で入手した報酬を報告する必要があります。<br />
		///    <br />
		///    `rewards` で報告された内容は評価され、開始時に渡した数量以上や入手できないリソースを入手したと報告してきた場合はエラーとなります。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">カテゴリ名</param>
		/// <param name="rewards">実際にクエストで得た報酬</param>
		/// <param name="transactionId">トランザクションID</param>
		/// <param name="isComplete">クエストをクリアしたか</param>
		/// <param name="config">スタンプシートの変数に適用する設定値</param>
		public IEnumerator End(
		        UnityAction<AsyncResult<EzEndResult>> callback,
		        GameSession session,
                string namespaceName,
                string transactionId,
                List<EzReward> rewards=null,
                bool? isComplete=null,
                List<EzConfig> config=null
        )
		{
            yield return _client.End(
                new EndRequest()
                    .WithNamespaceName(namespaceName)
                    .WithRewards(rewards != null ? rewards.Select(item => item.ToModel()).ToList() : new List<Reward>(new Reward[]{}))
                    .WithTransactionId(transactionId)
                    .WithIsComplete(isComplete)
                    .WithConfig(config != null ? config.Select(item => item.ToModel()).ToList() : new List<Config>(new Config[]{}))
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzEndResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzEndResult>(
                                new EzEndResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}

		/// <summary>
		///  クエストの進行情報を取得。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">カテゴリ名</param>
		public IEnumerator GetProgress(
		        UnityAction<AsyncResult<EzGetProgressResult>> callback,
		        GameSession session,
                string namespaceName
        )
		{
            yield return _client.GetProgress(
                new GetProgressRequest()
                    .WithNamespaceName(namespaceName)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzGetProgressResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzGetProgressResult>(
                                new EzGetProgressResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}

		/// <summary>
		///  クエストの進行情報を削除。<br />
		///    <br />
		///    クエストの開始時に `force` オプションを使うのではなく、明示的に進行情報を削除したい場合に使用してください。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">カテゴリ名</param>
		public IEnumerator DeleteProgress(
		        UnityAction<AsyncResult<EzDeleteProgressResult>> callback,
		        GameSession session,
                string namespaceName
        )
		{
            yield return _client.DeleteProgress(
                new DeleteProgressRequest()
                    .WithNamespaceName(namespaceName)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzDeleteProgressResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzDeleteProgressResult>(
                                new EzDeleteProgressResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}

		/// <summary>
		///  クエスト進行情報の一覧を取得<br />
		///    <br />
		///    クエストグループごとに1つの `クエスト進行情報` として登録されており、<br />
		///    `クエスト進行情報` にはクエストグループ内でクリア済みのクエスト名の一覧が記録されています。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">カテゴリ名</param>
		/// <param name="pageToken">データの取得を開始する位置を指定するトークン</param>
		/// <param name="limit">データの取得件数</param>
		public IEnumerator DescribeCompletedQuestLists(
		        UnityAction<AsyncResult<EzDescribeCompletedQuestListsResult>> callback,
		        GameSession session,
                string namespaceName,
                string pageToken=null,
                long? limit=null
        )
		{
            yield return _client.DescribeCompletedQuestLists(
                new DescribeCompletedQuestListsRequest()
                    .WithNamespaceName(namespaceName)
                    .WithPageToken(pageToken)
                    .WithLimit(limit)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzDescribeCompletedQuestListsResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzDescribeCompletedQuestListsResult>(
                                new EzDescribeCompletedQuestListsResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}

		/// <summary>
		///  クエスト進行情報を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">カテゴリ名</param>
		/// <param name="questGroupName">クエストグループモデル名</param>
		public IEnumerator GetCompletedQuestList(
		        UnityAction<AsyncResult<EzGetCompletedQuestListResult>> callback,
		        GameSession session,
                string namespaceName,
                string questGroupName
        )
		{
            yield return _client.GetCompletedQuestList(
                new GetCompletedQuestListRequest()
                    .WithNamespaceName(namespaceName)
                    .WithQuestGroupName(questGroupName)
                    .WithAccessToken(session.AccessToken.token),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzGetCompletedQuestListResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzGetCompletedQuestListResult>(
                                new EzGetCompletedQuestListResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}

		/// <summary>
		///  クエストグループの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">カテゴリ名</param>
		public IEnumerator ListQuestGroups(
		        UnityAction<AsyncResult<EzListQuestGroupsResult>> callback,
                string namespaceName
        )
		{
            yield return _client.DescribeQuestGroupModels(
                new DescribeQuestGroupModelsRequest()
                    .WithNamespaceName(namespaceName),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzListQuestGroupsResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzListQuestGroupsResult>(
                                new EzListQuestGroupsResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}

		/// <summary>
		///  クエストグループの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">カテゴリ名</param>
		/// <param name="questGroupName">クエストグループモデル名</param>
		public IEnumerator GetQuestGroup(
		        UnityAction<AsyncResult<EzGetQuestGroupResult>> callback,
                string namespaceName,
                string questGroupName
        )
		{
            yield return _client.GetQuestGroupModel(
                new GetQuestGroupModelRequest()
                    .WithNamespaceName(namespaceName)
                    .WithQuestGroupName(questGroupName),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzGetQuestGroupResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzGetQuestGroupResult>(
                                new EzGetQuestGroupResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}

		/// <summary>
		///  クエストモデルの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">カテゴリ名</param>
		/// <param name="questGroupName">クエストグループモデル名</param>
		public IEnumerator ListQuests(
		        UnityAction<AsyncResult<EzListQuestsResult>> callback,
                string namespaceName,
                string questGroupName
        )
		{
            yield return _client.DescribeQuestModels(
                new DescribeQuestModelsRequest()
                    .WithNamespaceName(namespaceName)
                    .WithQuestGroupName(questGroupName),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzListQuestsResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzListQuestsResult>(
                                new EzListQuestsResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}

		/// <summary>
		///  クエストモデルの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">カテゴリ名</param>
		/// <param name="questGroupName">クエストグループモデル名</param>
		/// <param name="questName">クエスト名</param>
		public IEnumerator GetQuest(
		        UnityAction<AsyncResult<EzGetQuestResult>> callback,
                string namespaceName,
                string questGroupName,
                string questName
        )
		{
            yield return _client.GetQuestModel(
                new GetQuestModelRequest()
                    .WithNamespaceName(namespaceName)
                    .WithQuestGroupName(questGroupName)
                    .WithQuestName(questName),
				r =>
				{
				    if(r.Result == null)
				    {
                        callback.Invoke(
                            new AsyncResult<EzGetQuestResult>(
                                null,
                                r.Error
                            )
                        );
				    }
				    else
				    {
                        callback.Invoke(
                            new AsyncResult<EzGetQuestResult>(
                                new EzGetQuestResult(r.Result),
                                r.Error
                            )
                        );
                    }
				}
            );
		}
	}
}