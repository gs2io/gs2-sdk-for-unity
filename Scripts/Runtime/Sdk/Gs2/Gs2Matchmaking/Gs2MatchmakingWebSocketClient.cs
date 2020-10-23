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
using UnityEngine.Events;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Util.LitJson;

namespace Gs2.Gs2Matchmaking
{
	public class Gs2MatchmakingWebSocketClient : AbstractGs2Client
	{

		public static string Endpoint = "matchmaking";

        protected Gs2WebSocketSession Gs2WebSocketSession => (Gs2WebSocketSession) Gs2Session;

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="Gs2WebSocketSession">WebSocket API 用セッション</param>
		public Gs2MatchmakingWebSocketClient(Gs2WebSocketSession Gs2WebSocketSession) : base(Gs2WebSocketSession)
		{

		}

        private class CreateNamespaceTask : Gs2WebSocketSessionTask<Result.CreateNamespaceResult>
        {
			private readonly Request.CreateNamespaceRequest _request;

			public CreateNamespaceTask(Request.CreateNamespaceRequest request, UnityAction<AsyncResult<Result.CreateNamespaceResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.name != null)
                {
                    jsonWriter.WritePropertyName("name");
                    jsonWriter.Write(_request.name.ToString());
                }
                if (_request.description != null)
                {
                    jsonWriter.WritePropertyName("description");
                    jsonWriter.Write(_request.description.ToString());
                }
                if (_request.enableRating != null)
                {
                    jsonWriter.WritePropertyName("enableRating");
                    jsonWriter.Write(_request.enableRating.ToString());
                }
                if (_request.createGatheringTriggerType != null)
                {
                    jsonWriter.WritePropertyName("createGatheringTriggerType");
                    jsonWriter.Write(_request.createGatheringTriggerType.ToString());
                }
                if (_request.createGatheringTriggerRealtimeNamespaceId != null)
                {
                    jsonWriter.WritePropertyName("createGatheringTriggerRealtimeNamespaceId");
                    jsonWriter.Write(_request.createGatheringTriggerRealtimeNamespaceId.ToString());
                }
                if (_request.createGatheringTriggerScriptId != null)
                {
                    jsonWriter.WritePropertyName("createGatheringTriggerScriptId");
                    jsonWriter.Write(_request.createGatheringTriggerScriptId.ToString());
                }
                if (_request.completeMatchmakingTriggerType != null)
                {
                    jsonWriter.WritePropertyName("completeMatchmakingTriggerType");
                    jsonWriter.Write(_request.completeMatchmakingTriggerType.ToString());
                }
                if (_request.completeMatchmakingTriggerRealtimeNamespaceId != null)
                {
                    jsonWriter.WritePropertyName("completeMatchmakingTriggerRealtimeNamespaceId");
                    jsonWriter.Write(_request.completeMatchmakingTriggerRealtimeNamespaceId.ToString());
                }
                if (_request.completeMatchmakingTriggerScriptId != null)
                {
                    jsonWriter.WritePropertyName("completeMatchmakingTriggerScriptId");
                    jsonWriter.Write(_request.completeMatchmakingTriggerScriptId.ToString());
                }
                if (_request.joinNotification != null)
                {
                    jsonWriter.WritePropertyName("joinNotification");
                    _request.joinNotification.WriteJson(jsonWriter);
                }
                if (_request.leaveNotification != null)
                {
                    jsonWriter.WritePropertyName("leaveNotification");
                    _request.leaveNotification.WriteJson(jsonWriter);
                }
                if (_request.completeNotification != null)
                {
                    jsonWriter.WritePropertyName("completeNotification");
                    _request.completeNotification.WriteJson(jsonWriter);
                }
                if (_request.logSetting != null)
                {
                    jsonWriter.WritePropertyName("logSetting");
                    _request.logSetting.WriteJson(jsonWriter);
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("matchmaking");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("namespace");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("createNamespace");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  ネームスペースを新規作成<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator CreateNamespace(
                Request.CreateNamespaceRequest request,
                UnityAction<AsyncResult<Result.CreateNamespaceResult>> callback
        )
		{
			var task = new CreateNamespaceTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class GetNamespaceStatusTask : Gs2WebSocketSessionTask<Result.GetNamespaceStatusResult>
        {
			private readonly Request.GetNamespaceStatusRequest _request;

			public GetNamespaceStatusTask(Request.GetNamespaceStatusRequest request, UnityAction<AsyncResult<Result.GetNamespaceStatusResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("matchmaking");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("namespace");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("getNamespaceStatus");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  ネームスペースの状態を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetNamespaceStatus(
                Request.GetNamespaceStatusRequest request,
                UnityAction<AsyncResult<Result.GetNamespaceStatusResult>> callback
        )
		{
			var task = new GetNamespaceStatusTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class GetNamespaceTask : Gs2WebSocketSessionTask<Result.GetNamespaceResult>
        {
			private readonly Request.GetNamespaceRequest _request;

			public GetNamespaceTask(Request.GetNamespaceRequest request, UnityAction<AsyncResult<Result.GetNamespaceResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("matchmaking");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("namespace");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("getNamespace");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  ネームスペースを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetNamespace(
                Request.GetNamespaceRequest request,
                UnityAction<AsyncResult<Result.GetNamespaceResult>> callback
        )
		{
			var task = new GetNamespaceTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class UpdateNamespaceTask : Gs2WebSocketSessionTask<Result.UpdateNamespaceResult>
        {
			private readonly Request.UpdateNamespaceRequest _request;

			public UpdateNamespaceTask(Request.UpdateNamespaceRequest request, UnityAction<AsyncResult<Result.UpdateNamespaceResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.description != null)
                {
                    jsonWriter.WritePropertyName("description");
                    jsonWriter.Write(_request.description.ToString());
                }
                if (_request.enableRating != null)
                {
                    jsonWriter.WritePropertyName("enableRating");
                    jsonWriter.Write(_request.enableRating.ToString());
                }
                if (_request.createGatheringTriggerType != null)
                {
                    jsonWriter.WritePropertyName("createGatheringTriggerType");
                    jsonWriter.Write(_request.createGatheringTriggerType.ToString());
                }
                if (_request.createGatheringTriggerRealtimeNamespaceId != null)
                {
                    jsonWriter.WritePropertyName("createGatheringTriggerRealtimeNamespaceId");
                    jsonWriter.Write(_request.createGatheringTriggerRealtimeNamespaceId.ToString());
                }
                if (_request.createGatheringTriggerScriptId != null)
                {
                    jsonWriter.WritePropertyName("createGatheringTriggerScriptId");
                    jsonWriter.Write(_request.createGatheringTriggerScriptId.ToString());
                }
                if (_request.completeMatchmakingTriggerType != null)
                {
                    jsonWriter.WritePropertyName("completeMatchmakingTriggerType");
                    jsonWriter.Write(_request.completeMatchmakingTriggerType.ToString());
                }
                if (_request.completeMatchmakingTriggerRealtimeNamespaceId != null)
                {
                    jsonWriter.WritePropertyName("completeMatchmakingTriggerRealtimeNamespaceId");
                    jsonWriter.Write(_request.completeMatchmakingTriggerRealtimeNamespaceId.ToString());
                }
                if (_request.completeMatchmakingTriggerScriptId != null)
                {
                    jsonWriter.WritePropertyName("completeMatchmakingTriggerScriptId");
                    jsonWriter.Write(_request.completeMatchmakingTriggerScriptId.ToString());
                }
                if (_request.joinNotification != null)
                {
                    jsonWriter.WritePropertyName("joinNotification");
                    _request.joinNotification.WriteJson(jsonWriter);
                }
                if (_request.leaveNotification != null)
                {
                    jsonWriter.WritePropertyName("leaveNotification");
                    _request.leaveNotification.WriteJson(jsonWriter);
                }
                if (_request.completeNotification != null)
                {
                    jsonWriter.WritePropertyName("completeNotification");
                    _request.completeNotification.WriteJson(jsonWriter);
                }
                if (_request.logSetting != null)
                {
                    jsonWriter.WritePropertyName("logSetting");
                    _request.logSetting.WriteJson(jsonWriter);
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("matchmaking");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("namespace");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("updateNamespace");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  ネームスペースを更新<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator UpdateNamespace(
                Request.UpdateNamespaceRequest request,
                UnityAction<AsyncResult<Result.UpdateNamespaceResult>> callback
        )
		{
			var task = new UpdateNamespaceTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class DeleteNamespaceTask : Gs2WebSocketSessionTask<Result.DeleteNamespaceResult>
        {
			private readonly Request.DeleteNamespaceRequest _request;

			public DeleteNamespaceTask(Request.DeleteNamespaceRequest request, UnityAction<AsyncResult<Result.DeleteNamespaceResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("matchmaking");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("namespace");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("deleteNamespace");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  ネームスペースを削除<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeleteNamespace(
                Request.DeleteNamespaceRequest request,
                UnityAction<AsyncResult<Result.DeleteNamespaceResult>> callback
        )
		{
			var task = new DeleteNamespaceTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class CreateGatheringTask : Gs2WebSocketSessionTask<Result.CreateGatheringResult>
        {
			private readonly Request.CreateGatheringRequest _request;

			public CreateGatheringTask(Request.CreateGatheringRequest request, UnityAction<AsyncResult<Result.CreateGatheringResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.player != null)
                {
                    jsonWriter.WritePropertyName("player");
                    _request.player.WriteJson(jsonWriter);
                }
                if (_request.attributeRanges != null)
                {
                    jsonWriter.WritePropertyName("attributeRanges");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.attributeRanges)
                    {
                        item.WriteJson(jsonWriter);
                    }
                    jsonWriter.WriteArrayEnd();
                }
                if (_request.capacityOfRoles != null)
                {
                    jsonWriter.WritePropertyName("capacityOfRoles");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.capacityOfRoles)
                    {
                        item.WriteJson(jsonWriter);
                    }
                    jsonWriter.WriteArrayEnd();
                }
                if (_request.allowUserIds != null)
                {
                    jsonWriter.WritePropertyName("allowUserIds");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.allowUserIds)
                    {
                        jsonWriter.Write(item);
                    }
                    jsonWriter.WriteArrayEnd();
                }
                if (_request.expiresAt != null)
                {
                    jsonWriter.WritePropertyName("expiresAt");
                    jsonWriter.Write(_request.expiresAt.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }
                if (_request.accessToken != null)
                {
                    jsonWriter.WritePropertyName("xGs2AccessToken");
                    jsonWriter.Write(_request.accessToken);
                }
                if (_request.duplicationAvoider != null)
                {
                    jsonWriter.WritePropertyName("xGs2DuplicationAvoider");
                    jsonWriter.Write(_request.duplicationAvoider);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("matchmaking");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("gathering");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("createGathering");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  ギャザリングを作成して募集を開始<br />
		///    <br />
		///    `募集条件` には、作成したギャザリングに参加を許可する各属性値の範囲を指定します。<br />
		///    <br />
		///    たとえば、同一ゲームモードを希望するプレイヤーを募集したい場合は、ゲームモードに対応した属性値が完全一致する参加条件プレイヤーとマッチメイキングするように<br />
		///    `属性名：ゲームモード` `属性最小値: ゲームモードを表す数値` `属性最大値: ゲームモードを表す数値`<br />
		///    とすることで、同一ゲームモードを希望するプレイヤー同士をマッチメイキングできます。<br />
		///    <br />
		///    他にレーティングをベースにしたマッチメイキングを実施したい場合は、<br />
		///    ルーム作成者のレーティング値を中心とした属性値の範囲を指定することで、レーティング値の近いプレイヤー同士をマッチメイキングできます。<br />
		///    この `募集条件` はあとで更新することができますので、徐々に条件を緩和していくことができます。<br />
		///    <br />
		///    ロール とは 盾役1人・回復役1人・攻撃役2人 などの役割ごとに募集人数を設定したい場合に使用します。<br />
		///    ロールにはエイリアスを指定できます。<br />
		///    たとえば、盾役は パラディン と ナイト の2種類の `ジョブ` に更に分類できるとします。<br />
		///    この場合、ロール名 に `盾役` エイリアス に `パラディン` `ナイト` として募集を出すようにゲームを実装します。<br />
		///    そして、プレイヤーは自分自身の `ジョブ` を自身のプレイヤー情報のロールに指定します。<br />
		///    <br />
		///    こうすることで、募集条件が `盾役` になっているギャザリングには `パラディン` も `ナイト` も参加できます。<br />
		///    一方で、ギャザリングを作成するときに、 `パラディン` だけ募集したくて、 `ナイト` を募集したくない場合は、<br />
		///    募集するロール名に `パラディン` を直接指定したり、エイリアスに `ナイト` を含めないようにすることで実現できます。<br />
		///    <br />
		///    `参加者` の `募集人数` はプレイヤーの募集人数を指定します。ロール名を指定することで、ロール名ごとの募集人数を設定できます。<br />
		///    <br />
		///    `参加者` の `参加者のプレイヤー情報リスト` には事前にプレイヤー間でパーティを構築している場合や、参加者が離脱したあとの追加募集で使用します。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator CreateGathering(
                Request.CreateGatheringRequest request,
                UnityAction<AsyncResult<Result.CreateGatheringResult>> callback
        )
		{
			var task = new CreateGatheringTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class CreateGatheringByUserIdTask : Gs2WebSocketSessionTask<Result.CreateGatheringByUserIdResult>
        {
			private readonly Request.CreateGatheringByUserIdRequest _request;

			public CreateGatheringByUserIdTask(Request.CreateGatheringByUserIdRequest request, UnityAction<AsyncResult<Result.CreateGatheringByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.userId != null)
                {
                    jsonWriter.WritePropertyName("userId");
                    jsonWriter.Write(_request.userId.ToString());
                }
                if (_request.player != null)
                {
                    jsonWriter.WritePropertyName("player");
                    _request.player.WriteJson(jsonWriter);
                }
                if (_request.attributeRanges != null)
                {
                    jsonWriter.WritePropertyName("attributeRanges");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.attributeRanges)
                    {
                        item.WriteJson(jsonWriter);
                    }
                    jsonWriter.WriteArrayEnd();
                }
                if (_request.capacityOfRoles != null)
                {
                    jsonWriter.WritePropertyName("capacityOfRoles");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.capacityOfRoles)
                    {
                        item.WriteJson(jsonWriter);
                    }
                    jsonWriter.WriteArrayEnd();
                }
                if (_request.allowUserIds != null)
                {
                    jsonWriter.WritePropertyName("allowUserIds");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.allowUserIds)
                    {
                        jsonWriter.Write(item);
                    }
                    jsonWriter.WriteArrayEnd();
                }
                if (_request.expiresAt != null)
                {
                    jsonWriter.WritePropertyName("expiresAt");
                    jsonWriter.Write(_request.expiresAt.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }
                if (_request.duplicationAvoider != null)
                {
                    jsonWriter.WritePropertyName("xGs2DuplicationAvoider");
                    jsonWriter.Write(_request.duplicationAvoider);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("matchmaking");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("gathering");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("createGatheringByUserId");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  ギャザリングを作成して募集を開始<br />
		///    <br />
		///    `募集条件` には、作成したギャザリングに参加を許可する各属性値の範囲を指定します。<br />
		///    <br />
		///    たとえば、同一ゲームモードを希望するプレイヤーを募集したい場合は、ゲームモードに対応した属性値が完全一致する参加条件プレイヤーとマッチメイキングするように<br />
		///    `属性名：ゲームモード` `属性最小値: ゲームモードを表す数値` `属性最大値: ゲームモードを表す数値`<br />
		///    とすることで、同一ゲームモードを希望するプレイヤー同士をマッチメイキングできます。<br />
		///    <br />
		///    他にレーティングをベースにしたマッチメイキングを実施したい場合は、<br />
		///    ルーム作成者のレーティング値を中心とした属性値の範囲を指定することで、レーティング値の近いプレイヤー同士をマッチメイキングできます。<br />
		///    この `募集条件` はあとで更新することができますので、徐々に条件を緩和していくことができます。<br />
		///    <br />
		///    ロール とは 盾役1人・回復役1人・攻撃役2人 などの役割ごとに募集人数を設定したい場合に使用します。<br />
		///    ロールにはエイリアスを指定できます。<br />
		///    たとえば、盾役は パラディン と ナイト の2種類の `ジョブ` に更に分類できるとします。<br />
		///    この場合、ロール名 に `盾役` エイリアス に `パラディン` `ナイト` として募集を出すようにゲームを実装します。<br />
		///    そして、プレイヤーは自分自身の `ジョブ` を自身のプレイヤー情報のロールに指定します。<br />
		///    <br />
		///    こうすることで、募集条件が `盾役` になっているギャザリングには `パラディン` も `ナイト` も参加できます。<br />
		///    一方で、ギャザリングを作成するときに、 `パラディン` だけ募集したくて、 `ナイト` を募集したくない場合は、<br />
		///    募集するロール名に `パラディン` を直接指定したり、エイリアスに `ナイト` を含めないようにすることで実現できます。<br />
		///    <br />
		///    `参加者` の `募集人数` はプレイヤーの募集人数を指定します。ロール名を指定することで、ロール名ごとの募集人数を設定できます。<br />
		///    <br />
		///    `参加者` の `参加者のプレイヤー情報リスト` には事前にプレイヤー間でパーティを構築している場合や、参加者が離脱したあとの追加募集で使用します。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator CreateGatheringByUserId(
                Request.CreateGatheringByUserIdRequest request,
                UnityAction<AsyncResult<Result.CreateGatheringByUserIdResult>> callback
        )
		{
			var task = new CreateGatheringByUserIdTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class UpdateGatheringTask : Gs2WebSocketSessionTask<Result.UpdateGatheringResult>
        {
			private readonly Request.UpdateGatheringRequest _request;

			public UpdateGatheringTask(Request.UpdateGatheringRequest request, UnityAction<AsyncResult<Result.UpdateGatheringResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.gatheringName != null)
                {
                    jsonWriter.WritePropertyName("gatheringName");
                    jsonWriter.Write(_request.gatheringName.ToString());
                }
                if (_request.attributeRanges != null)
                {
                    jsonWriter.WritePropertyName("attributeRanges");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.attributeRanges)
                    {
                        item.WriteJson(jsonWriter);
                    }
                    jsonWriter.WriteArrayEnd();
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }
                if (_request.accessToken != null)
                {
                    jsonWriter.WritePropertyName("xGs2AccessToken");
                    jsonWriter.Write(_request.accessToken);
                }
                if (_request.duplicationAvoider != null)
                {
                    jsonWriter.WritePropertyName("xGs2DuplicationAvoider");
                    jsonWriter.Write(_request.duplicationAvoider);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("matchmaking");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("gathering");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("updateGathering");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  ギャザリングを更新する<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator UpdateGathering(
                Request.UpdateGatheringRequest request,
                UnityAction<AsyncResult<Result.UpdateGatheringResult>> callback
        )
		{
			var task = new UpdateGatheringTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class UpdateGatheringByUserIdTask : Gs2WebSocketSessionTask<Result.UpdateGatheringByUserIdResult>
        {
			private readonly Request.UpdateGatheringByUserIdRequest _request;

			public UpdateGatheringByUserIdTask(Request.UpdateGatheringByUserIdRequest request, UnityAction<AsyncResult<Result.UpdateGatheringByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.gatheringName != null)
                {
                    jsonWriter.WritePropertyName("gatheringName");
                    jsonWriter.Write(_request.gatheringName.ToString());
                }
                if (_request.userId != null)
                {
                    jsonWriter.WritePropertyName("userId");
                    jsonWriter.Write(_request.userId.ToString());
                }
                if (_request.attributeRanges != null)
                {
                    jsonWriter.WritePropertyName("attributeRanges");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.attributeRanges)
                    {
                        item.WriteJson(jsonWriter);
                    }
                    jsonWriter.WriteArrayEnd();
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }
                if (_request.duplicationAvoider != null)
                {
                    jsonWriter.WritePropertyName("xGs2DuplicationAvoider");
                    jsonWriter.Write(_request.duplicationAvoider);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("matchmaking");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("gathering");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("updateGatheringByUserId");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  ギャザリングを更新する<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator UpdateGatheringByUserId(
                Request.UpdateGatheringByUserIdRequest request,
                UnityAction<AsyncResult<Result.UpdateGatheringByUserIdResult>> callback
        )
		{
			var task = new UpdateGatheringByUserIdTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class DoMatchmakingByPlayerTask : Gs2WebSocketSessionTask<Result.DoMatchmakingByPlayerResult>
        {
			private readonly Request.DoMatchmakingByPlayerRequest _request;

			public DoMatchmakingByPlayerTask(Request.DoMatchmakingByPlayerRequest request, UnityAction<AsyncResult<Result.DoMatchmakingByPlayerResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.player != null)
                {
                    jsonWriter.WritePropertyName("player");
                    _request.player.WriteJson(jsonWriter);
                }
                if (_request.matchmakingContextToken != null)
                {
                    jsonWriter.WritePropertyName("matchmakingContextToken");
                    jsonWriter.Write(_request.matchmakingContextToken.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("matchmaking");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("gathering");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("doMatchmakingByPlayer");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  Player が参加できるギャザリングを探して参加する<br />
		///    <br />
		///    一定時間 検索を行い、対象が見つからなかったときには `マッチメイキングの状態を保持するトークン` を返す。<br />
		///    次回 `マッチメイキングの状態を保持するトークン` をつけて再度リクエストを出すことで、前回の続きから検索処理を再開できる。<br />
		///    すべてのギャザリングを検索したが、参加できるギャザリングが存在しなかった場合はギャザリングもトークンもどちらも null が応答される。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DoMatchmakingByPlayer(
                Request.DoMatchmakingByPlayerRequest request,
                UnityAction<AsyncResult<Result.DoMatchmakingByPlayerResult>> callback
        )
		{
			var task = new DoMatchmakingByPlayerTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class DoMatchmakingTask : Gs2WebSocketSessionTask<Result.DoMatchmakingResult>
        {
			private readonly Request.DoMatchmakingRequest _request;

			public DoMatchmakingTask(Request.DoMatchmakingRequest request, UnityAction<AsyncResult<Result.DoMatchmakingResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.player != null)
                {
                    jsonWriter.WritePropertyName("player");
                    _request.player.WriteJson(jsonWriter);
                }
                if (_request.matchmakingContextToken != null)
                {
                    jsonWriter.WritePropertyName("matchmakingContextToken");
                    jsonWriter.Write(_request.matchmakingContextToken.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }
                if (_request.accessToken != null)
                {
                    jsonWriter.WritePropertyName("xGs2AccessToken");
                    jsonWriter.Write(_request.accessToken);
                }
                if (_request.duplicationAvoider != null)
                {
                    jsonWriter.WritePropertyName("xGs2DuplicationAvoider");
                    jsonWriter.Write(_request.duplicationAvoider);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("matchmaking");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("gathering");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("doMatchmaking");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  自分が参加できるギャザリングを探して参加する<br />
		///    <br />
		///    一定時間 検索を行い、対象が見つからなかったときには `マッチメイキングの状態を保持するトークン` を返す。<br />
		///    次回 `マッチメイキングの状態を保持するトークン` をつけて再度リクエストを出すことで、前回の続きから検索処理を再開できる。<br />
		///    すべてのギャザリングを検索したが、参加できるギャザリングが存在しなかった場合はギャザリングもトークンもどちらも null が応答される。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DoMatchmaking(
                Request.DoMatchmakingRequest request,
                UnityAction<AsyncResult<Result.DoMatchmakingResult>> callback
        )
		{
			var task = new DoMatchmakingTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class GetGatheringTask : Gs2WebSocketSessionTask<Result.GetGatheringResult>
        {
			private readonly Request.GetGatheringRequest _request;

			public GetGatheringTask(Request.GetGatheringRequest request, UnityAction<AsyncResult<Result.GetGatheringResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.gatheringName != null)
                {
                    jsonWriter.WritePropertyName("gatheringName");
                    jsonWriter.Write(_request.gatheringName.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("matchmaking");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("gathering");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("getGathering");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  ギャザリングを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetGathering(
                Request.GetGatheringRequest request,
                UnityAction<AsyncResult<Result.GetGatheringResult>> callback
        )
		{
			var task = new GetGatheringTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class CancelMatchmakingTask : Gs2WebSocketSessionTask<Result.CancelMatchmakingResult>
        {
			private readonly Request.CancelMatchmakingRequest _request;

			public CancelMatchmakingTask(Request.CancelMatchmakingRequest request, UnityAction<AsyncResult<Result.CancelMatchmakingResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.gatheringName != null)
                {
                    jsonWriter.WritePropertyName("gatheringName");
                    jsonWriter.Write(_request.gatheringName.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }
                if (_request.accessToken != null)
                {
                    jsonWriter.WritePropertyName("xGs2AccessToken");
                    jsonWriter.Write(_request.accessToken);
                }
                if (_request.duplicationAvoider != null)
                {
                    jsonWriter.WritePropertyName("xGs2DuplicationAvoider");
                    jsonWriter.Write(_request.duplicationAvoider);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("matchmaking");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("gathering");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("cancelMatchmaking");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  マッチメイキングをキャンセルする<br />
		///    <br />
		///    ギャザリングから離脱する前にマッチメイキングが完了した場合は、NotFoundException(404エラー) が発生し失敗します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator CancelMatchmaking(
                Request.CancelMatchmakingRequest request,
                UnityAction<AsyncResult<Result.CancelMatchmakingResult>> callback
        )
		{
			var task = new CancelMatchmakingTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class CancelMatchmakingByUserIdTask : Gs2WebSocketSessionTask<Result.CancelMatchmakingByUserIdResult>
        {
			private readonly Request.CancelMatchmakingByUserIdRequest _request;

			public CancelMatchmakingByUserIdTask(Request.CancelMatchmakingByUserIdRequest request, UnityAction<AsyncResult<Result.CancelMatchmakingByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.gatheringName != null)
                {
                    jsonWriter.WritePropertyName("gatheringName");
                    jsonWriter.Write(_request.gatheringName.ToString());
                }
                if (_request.userId != null)
                {
                    jsonWriter.WritePropertyName("userId");
                    jsonWriter.Write(_request.userId.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }
                if (_request.duplicationAvoider != null)
                {
                    jsonWriter.WritePropertyName("xGs2DuplicationAvoider");
                    jsonWriter.Write(_request.duplicationAvoider);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("matchmaking");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("gathering");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("cancelMatchmakingByUserId");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  ユーザIDを指定してマッチメイキングをキャンセルする<br />
		///    <br />
		///    ギャザリングから離脱する前にマッチメイキングが完了した場合は、NotFoundException(404エラー) が発生し失敗します<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator CancelMatchmakingByUserId(
                Request.CancelMatchmakingByUserIdRequest request,
                UnityAction<AsyncResult<Result.CancelMatchmakingByUserIdResult>> callback
        )
		{
			var task = new CancelMatchmakingByUserIdTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class DeleteGatheringTask : Gs2WebSocketSessionTask<Result.DeleteGatheringResult>
        {
			private readonly Request.DeleteGatheringRequest _request;

			public DeleteGatheringTask(Request.DeleteGatheringRequest request, UnityAction<AsyncResult<Result.DeleteGatheringResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.gatheringName != null)
                {
                    jsonWriter.WritePropertyName("gatheringName");
                    jsonWriter.Write(_request.gatheringName.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("matchmaking");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("gathering");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("deleteGathering");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  ギャザリングを削除<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeleteGathering(
                Request.DeleteGatheringRequest request,
                UnityAction<AsyncResult<Result.DeleteGatheringResult>> callback
        )
		{
			var task = new DeleteGatheringTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class CreateRatingModelMasterTask : Gs2WebSocketSessionTask<Result.CreateRatingModelMasterResult>
        {
			private readonly Request.CreateRatingModelMasterRequest _request;

			public CreateRatingModelMasterTask(Request.CreateRatingModelMasterRequest request, UnityAction<AsyncResult<Result.CreateRatingModelMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.name != null)
                {
                    jsonWriter.WritePropertyName("name");
                    jsonWriter.Write(_request.name.ToString());
                }
                if (_request.description != null)
                {
                    jsonWriter.WritePropertyName("description");
                    jsonWriter.Write(_request.description.ToString());
                }
                if (_request.metadata != null)
                {
                    jsonWriter.WritePropertyName("metadata");
                    jsonWriter.Write(_request.metadata.ToString());
                }
                if (_request.volatility != null)
                {
                    jsonWriter.WritePropertyName("volatility");
                    jsonWriter.Write(_request.volatility.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("matchmaking");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("ratingModelMaster");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("createRatingModelMaster");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  レーティングモデルマスターを新規作成<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator CreateRatingModelMaster(
                Request.CreateRatingModelMasterRequest request,
                UnityAction<AsyncResult<Result.CreateRatingModelMasterResult>> callback
        )
		{
			var task = new CreateRatingModelMasterTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class GetRatingModelMasterTask : Gs2WebSocketSessionTask<Result.GetRatingModelMasterResult>
        {
			private readonly Request.GetRatingModelMasterRequest _request;

			public GetRatingModelMasterTask(Request.GetRatingModelMasterRequest request, UnityAction<AsyncResult<Result.GetRatingModelMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.ratingName != null)
                {
                    jsonWriter.WritePropertyName("ratingName");
                    jsonWriter.Write(_request.ratingName.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("matchmaking");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("ratingModelMaster");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("getRatingModelMaster");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  レーティングモデルマスターを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetRatingModelMaster(
                Request.GetRatingModelMasterRequest request,
                UnityAction<AsyncResult<Result.GetRatingModelMasterResult>> callback
        )
		{
			var task = new GetRatingModelMasterTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class UpdateRatingModelMasterTask : Gs2WebSocketSessionTask<Result.UpdateRatingModelMasterResult>
        {
			private readonly Request.UpdateRatingModelMasterRequest _request;

			public UpdateRatingModelMasterTask(Request.UpdateRatingModelMasterRequest request, UnityAction<AsyncResult<Result.UpdateRatingModelMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.ratingName != null)
                {
                    jsonWriter.WritePropertyName("ratingName");
                    jsonWriter.Write(_request.ratingName.ToString());
                }
                if (_request.description != null)
                {
                    jsonWriter.WritePropertyName("description");
                    jsonWriter.Write(_request.description.ToString());
                }
                if (_request.metadata != null)
                {
                    jsonWriter.WritePropertyName("metadata");
                    jsonWriter.Write(_request.metadata.ToString());
                }
                if (_request.volatility != null)
                {
                    jsonWriter.WritePropertyName("volatility");
                    jsonWriter.Write(_request.volatility.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("matchmaking");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("ratingModelMaster");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("updateRatingModelMaster");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  レーティングモデルマスターを更新<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator UpdateRatingModelMaster(
                Request.UpdateRatingModelMasterRequest request,
                UnityAction<AsyncResult<Result.UpdateRatingModelMasterResult>> callback
        )
		{
			var task = new UpdateRatingModelMasterTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class DeleteRatingModelMasterTask : Gs2WebSocketSessionTask<Result.DeleteRatingModelMasterResult>
        {
			private readonly Request.DeleteRatingModelMasterRequest _request;

			public DeleteRatingModelMasterTask(Request.DeleteRatingModelMasterRequest request, UnityAction<AsyncResult<Result.DeleteRatingModelMasterResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.ratingName != null)
                {
                    jsonWriter.WritePropertyName("ratingName");
                    jsonWriter.Write(_request.ratingName.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("matchmaking");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("ratingModelMaster");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("deleteRatingModelMaster");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  レーティングモデルマスターを削除<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeleteRatingModelMaster(
                Request.DeleteRatingModelMasterRequest request,
                UnityAction<AsyncResult<Result.DeleteRatingModelMasterResult>> callback
        )
		{
			var task = new DeleteRatingModelMasterTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class GetRatingModelTask : Gs2WebSocketSessionTask<Result.GetRatingModelResult>
        {
			private readonly Request.GetRatingModelRequest _request;

			public GetRatingModelTask(Request.GetRatingModelRequest request, UnityAction<AsyncResult<Result.GetRatingModelResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.ratingName != null)
                {
                    jsonWriter.WritePropertyName("ratingName");
                    jsonWriter.Write(_request.ratingName.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("matchmaking");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("ratingModel");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("getRatingModel");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  レーティングモデルを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetRatingModel(
                Request.GetRatingModelRequest request,
                UnityAction<AsyncResult<Result.GetRatingModelResult>> callback
        )
		{
			var task = new GetRatingModelTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class GetRatingTask : Gs2WebSocketSessionTask<Result.GetRatingResult>
        {
			private readonly Request.GetRatingRequest _request;

			public GetRatingTask(Request.GetRatingRequest request, UnityAction<AsyncResult<Result.GetRatingResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.ratingName != null)
                {
                    jsonWriter.WritePropertyName("ratingName");
                    jsonWriter.Write(_request.ratingName.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }
                if (_request.accessToken != null)
                {
                    jsonWriter.WritePropertyName("xGs2AccessToken");
                    jsonWriter.Write(_request.accessToken);
                }
                if (_request.duplicationAvoider != null)
                {
                    jsonWriter.WritePropertyName("xGs2DuplicationAvoider");
                    jsonWriter.Write(_request.duplicationAvoider);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("matchmaking");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("rating");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("getRating");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  レーティングを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetRating(
                Request.GetRatingRequest request,
                UnityAction<AsyncResult<Result.GetRatingResult>> callback
        )
		{
			var task = new GetRatingTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class GetRatingByUserIdTask : Gs2WebSocketSessionTask<Result.GetRatingByUserIdResult>
        {
			private readonly Request.GetRatingByUserIdRequest _request;

			public GetRatingByUserIdTask(Request.GetRatingByUserIdRequest request, UnityAction<AsyncResult<Result.GetRatingByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.userId != null)
                {
                    jsonWriter.WritePropertyName("userId");
                    jsonWriter.Write(_request.userId.ToString());
                }
                if (_request.ratingName != null)
                {
                    jsonWriter.WritePropertyName("ratingName");
                    jsonWriter.Write(_request.ratingName.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }
                if (_request.duplicationAvoider != null)
                {
                    jsonWriter.WritePropertyName("xGs2DuplicationAvoider");
                    jsonWriter.Write(_request.duplicationAvoider);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("matchmaking");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("rating");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("getRatingByUserId");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  レーティングを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetRatingByUserId(
                Request.GetRatingByUserIdRequest request,
                UnityAction<AsyncResult<Result.GetRatingByUserIdResult>> callback
        )
		{
			var task = new GetRatingByUserIdTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class PutResultTask : Gs2WebSocketSessionTask<Result.PutResultResult>
        {
			private readonly Request.PutResultRequest _request;

			public PutResultTask(Request.PutResultRequest request, UnityAction<AsyncResult<Result.PutResultResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.ratingName != null)
                {
                    jsonWriter.WritePropertyName("ratingName");
                    jsonWriter.Write(_request.ratingName.ToString());
                }
                if (_request.gameResults != null)
                {
                    jsonWriter.WritePropertyName("gameResults");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.gameResults)
                    {
                        item.WriteJson(jsonWriter);
                    }
                    jsonWriter.WriteArrayEnd();
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("matchmaking");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("rating");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("putResult");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  レーティング値の再計算を実行<br />
		///    <br />
		///    レーティングの計算処理には Glicko-2 rating system をベースとした計算アルゴリズムを採用しています。<br />
		///    レーティング値の初期値は1500で、レーティングの値が離れた相手に勝利するほど上昇幅は大きく、同じく負けた側は減少幅は大きくなります。<br />
		///    <br />
		///    レーティングの計算には参加したユーザIDのリストが必要となります。<br />
		///    そのため、クライアントから直接このAPIを呼び出すのは適切ではありません。ゲームの勝敗を判断できるゲームサーバから呼び出すようにしてください。<br />
		///    P2P 対戦など、クライアント主導で対戦を実現している場合は、投票機能を利用して勝敗を決定するようにしてください。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator PutResult(
                Request.PutResultRequest request,
                UnityAction<AsyncResult<Result.PutResultResult>> callback
        )
		{
			var task = new PutResultTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class DeleteRatingTask : Gs2WebSocketSessionTask<Result.DeleteRatingResult>
        {
			private readonly Request.DeleteRatingRequest _request;

			public DeleteRatingTask(Request.DeleteRatingRequest request, UnityAction<AsyncResult<Result.DeleteRatingResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.userId != null)
                {
                    jsonWriter.WritePropertyName("userId");
                    jsonWriter.Write(_request.userId.ToString());
                }
                if (_request.ratingName != null)
                {
                    jsonWriter.WritePropertyName("ratingName");
                    jsonWriter.Write(_request.ratingName.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }
                if (_request.duplicationAvoider != null)
                {
                    jsonWriter.WritePropertyName("xGs2DuplicationAvoider");
                    jsonWriter.Write(_request.duplicationAvoider);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("matchmaking");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("rating");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("deleteRating");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  レーティングを削除<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator DeleteRating(
                Request.DeleteRatingRequest request,
                UnityAction<AsyncResult<Result.DeleteRatingResult>> callback
        )
		{
			var task = new DeleteRatingTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class GetBallotTask : Gs2WebSocketSessionTask<Result.GetBallotResult>
        {
			private readonly Request.GetBallotRequest _request;

			public GetBallotTask(Request.GetBallotRequest request, UnityAction<AsyncResult<Result.GetBallotResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.ratingName != null)
                {
                    jsonWriter.WritePropertyName("ratingName");
                    jsonWriter.Write(_request.ratingName.ToString());
                }
                if (_request.gatheringName != null)
                {
                    jsonWriter.WritePropertyName("gatheringName");
                    jsonWriter.Write(_request.gatheringName.ToString());
                }
                if (_request.gatheringId != null)
                {
                    jsonWriter.WritePropertyName("gatheringId");
                    jsonWriter.Write(_request.gatheringId.ToString());
                }
                if (_request.numberOfPlayer != null)
                {
                    jsonWriter.WritePropertyName("numberOfPlayer");
                    jsonWriter.Write(_request.numberOfPlayer.ToString());
                }
                if (_request.keyId != null)
                {
                    jsonWriter.WritePropertyName("keyId");
                    jsonWriter.Write(_request.keyId.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }
                if (_request.accessToken != null)
                {
                    jsonWriter.WritePropertyName("xGs2AccessToken");
                    jsonWriter.Write(_request.accessToken);
                }
                if (_request.duplicationAvoider != null)
                {
                    jsonWriter.WritePropertyName("xGs2DuplicationAvoider");
                    jsonWriter.Write(_request.duplicationAvoider);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("matchmaking");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("vote");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("getBallot");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  投票用紙を取得します。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetBallot(
                Request.GetBallotRequest request,
                UnityAction<AsyncResult<Result.GetBallotResult>> callback
        )
		{
			var task = new GetBallotTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class GetBallotByUserIdTask : Gs2WebSocketSessionTask<Result.GetBallotByUserIdResult>
        {
			private readonly Request.GetBallotByUserIdRequest _request;

			public GetBallotByUserIdTask(Request.GetBallotByUserIdRequest request, UnityAction<AsyncResult<Result.GetBallotByUserIdResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.ratingName != null)
                {
                    jsonWriter.WritePropertyName("ratingName");
                    jsonWriter.Write(_request.ratingName.ToString());
                }
                if (_request.gatheringName != null)
                {
                    jsonWriter.WritePropertyName("gatheringName");
                    jsonWriter.Write(_request.gatheringName.ToString());
                }
                if (_request.userId != null)
                {
                    jsonWriter.WritePropertyName("userId");
                    jsonWriter.Write(_request.userId.ToString());
                }
                if (_request.gatheringId != null)
                {
                    jsonWriter.WritePropertyName("gatheringId");
                    jsonWriter.Write(_request.gatheringId.ToString());
                }
                if (_request.numberOfPlayer != null)
                {
                    jsonWriter.WritePropertyName("numberOfPlayer");
                    jsonWriter.Write(_request.numberOfPlayer.ToString());
                }
                if (_request.keyId != null)
                {
                    jsonWriter.WritePropertyName("keyId");
                    jsonWriter.Write(_request.keyId.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }
                if (_request.duplicationAvoider != null)
                {
                    jsonWriter.WritePropertyName("xGs2DuplicationAvoider");
                    jsonWriter.Write(_request.duplicationAvoider);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("matchmaking");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("vote");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("getBallotByUserId");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  投票用紙を取得します。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator GetBallotByUserId(
                Request.GetBallotByUserIdRequest request,
                UnityAction<AsyncResult<Result.GetBallotByUserIdResult>> callback
        )
		{
			var task = new GetBallotByUserIdTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class VoteTask : Gs2WebSocketSessionTask<Result.VoteResult>
        {
			private readonly Request.VoteRequest _request;

			public VoteTask(Request.VoteRequest request, UnityAction<AsyncResult<Result.VoteResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.ballotBody != null)
                {
                    jsonWriter.WritePropertyName("ballotBody");
                    jsonWriter.Write(_request.ballotBody.ToString());
                }
                if (_request.ballotSignature != null)
                {
                    jsonWriter.WritePropertyName("ballotSignature");
                    jsonWriter.Write(_request.ballotSignature.ToString());
                }
                if (_request.gameResults != null)
                {
                    jsonWriter.WritePropertyName("gameResults");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.gameResults)
                    {
                        item.WriteJson(jsonWriter);
                    }
                    jsonWriter.WriteArrayEnd();
                }
                if (_request.keyId != null)
                {
                    jsonWriter.WritePropertyName("keyId");
                    jsonWriter.Write(_request.keyId.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("matchmaking");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("vote");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("vote");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  対戦結果を投票します。<br />
		///    <br />
		///    投票は最初の投票が行われてから5分以内に行う必要があります。<br />
		///    つまり、結果は即座に反映されず、投票開始からおよそ5分後または全てのプレイヤーが投票を行った際に結果が反映されます。<br />
		///    5分以内に全ての投票用紙を回収できなかった場合はその時点の投票内容で多数決をとって結果を決定します。<br />
		///    各結果の投票数が同一だった場合は結果は捨てられます（スクリプトで挙動を変更可）。<br />
		///    <br />
		///    結果を即座に反映したい場合は、勝利した側の代表プレイヤーが投票用紙を各プレイヤーから集めて voteMultiple を呼び出すことで結果を即座に反映できます。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator Vote(
                Request.VoteRequest request,
                UnityAction<AsyncResult<Result.VoteResult>> callback
        )
		{
			var task = new VoteTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class VoteMultipleTask : Gs2WebSocketSessionTask<Result.VoteMultipleResult>
        {
			private readonly Request.VoteMultipleRequest _request;

			public VoteMultipleTask(Request.VoteMultipleRequest request, UnityAction<AsyncResult<Result.VoteMultipleResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.signedBallots != null)
                {
                    jsonWriter.WritePropertyName("signedBallots");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.signedBallots)
                    {
                        item.WriteJson(jsonWriter);
                    }
                    jsonWriter.WriteArrayEnd();
                }
                if (_request.gameResults != null)
                {
                    jsonWriter.WritePropertyName("gameResults");
                    jsonWriter.WriteArrayStart();
                    foreach(var item in _request.gameResults)
                    {
                        item.WriteJson(jsonWriter);
                    }
                    jsonWriter.WriteArrayEnd();
                }
                if (_request.keyId != null)
                {
                    jsonWriter.WritePropertyName("keyId");
                    jsonWriter.Write(_request.keyId.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("matchmaking");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("vote");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("voteMultiple");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  対戦結果をまとめて投票します。<br />
		///    <br />
		///    ゲームに勝利した側が他プレイヤーの投票用紙を集めてまとめて投票するのに使用します。<br />
		///    『勝利した側』としているのは、敗北した側が自分たちが勝ったことにして報告することにインセンティブはありますが、その逆はないためです。<br />
		///    負けた側が投票用紙を渡してこない可能性がありますが、その場合も過半数の投票用紙があれば結果を通すことができます。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator VoteMultiple(
                Request.VoteMultipleRequest request,
                UnityAction<AsyncResult<Result.VoteMultipleResult>> callback
        )
		{
			var task = new VoteMultipleTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }

        private class CommitVoteTask : Gs2WebSocketSessionTask<Result.CommitVoteResult>
        {
			private readonly Request.CommitVoteRequest _request;

			public CommitVoteTask(Request.CommitVoteRequest request, UnityAction<AsyncResult<Result.CommitVoteResult>> userCallback) : base(userCallback)
			{
				_request = request;
			}

            protected override IEnumerator ExecuteImpl(Gs2Session gs2Session)
            {
                var stringBuilder = new StringBuilder();
                var jsonWriter = new JsonWriter(stringBuilder);

                jsonWriter.WriteObjectStart();

                if (_request.namespaceName != null)
                {
                    jsonWriter.WritePropertyName("namespaceName");
                    jsonWriter.Write(_request.namespaceName.ToString());
                }
                if (_request.ratingName != null)
                {
                    jsonWriter.WritePropertyName("ratingName");
                    jsonWriter.Write(_request.ratingName.ToString());
                }
                if (_request.gatheringName != null)
                {
                    jsonWriter.WritePropertyName("gatheringName");
                    jsonWriter.Write(_request.gatheringName.ToString());
                }
                if (_request.contextStack != null)
                {
                    jsonWriter.WritePropertyName("contextStack");
                    jsonWriter.Write(_request.contextStack.ToString());
                }
                if (_request.requestId != null)
                {
                    jsonWriter.WritePropertyName("xGs2RequestId");
                    jsonWriter.Write(_request.requestId);
                }

                jsonWriter.WritePropertyName("xGs2ClientId");
                jsonWriter.Write(gs2Session.Credential.ClientId);
                jsonWriter.WritePropertyName("xGs2ProjectToken");
                jsonWriter.Write(gs2Session.ProjectToken);

                jsonWriter.WritePropertyName("x_gs2");
                jsonWriter.WriteObjectStart();
                jsonWriter.WritePropertyName("service");
                jsonWriter.Write("matchmaking");
                jsonWriter.WritePropertyName("component");
                jsonWriter.Write("vote");
                jsonWriter.WritePropertyName("function");
                jsonWriter.Write("commitVote");
                jsonWriter.WritePropertyName("contentType");
                jsonWriter.Write("application/json");
                jsonWriter.WritePropertyName("requestId");
                jsonWriter.Write(Gs2SessionTaskId.ToString());
                jsonWriter.WriteObjectEnd();

                jsonWriter.WriteObjectEnd();

                ((Gs2WebSocketSession)gs2Session).Send(stringBuilder.ToString());

                return new EmptyCoroutine();
            }
        }

		/// <summary>
		///  投票状況を強制確定<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="request">リクエストパラメータ</param>
		public IEnumerator CommitVote(
                Request.CommitVoteRequest request,
                UnityAction<AsyncResult<Result.CommitVoteResult>> callback
        )
		{
			var task = new CommitVoteTask(request, callback);
			return Gs2WebSocketSession.Execute(task);
        }
	}
}