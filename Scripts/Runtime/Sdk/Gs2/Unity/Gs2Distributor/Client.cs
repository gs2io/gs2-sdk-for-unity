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
using Gs2.Gs2Distributor;
using Gs2.Gs2Distributor.Model;
using Gs2.Gs2Distributor.Request;
using Gs2.Gs2Distributor.Result;
using Gs2.Unity.Gs2Distributor.Model;
using Gs2.Unity.Gs2Distributor.Result;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Unity.Util;
using UnityEngine.Events;

namespace Gs2.Unity.Gs2Distributor
{
	public class Client
	{
		private readonly Gs2.Unity.Util.Profile _profile;
		private readonly Gs2DistributorWebSocketClient _client;
		private readonly Gs2DistributorRestClient _restClient;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2DistributorWebSocketClient(profile.Gs2Session);
			_restClient = new Gs2DistributorRestClient(profile.Gs2RestSession);
		}

		/// <summary>
		///  配信設定を認証<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		public IEnumerator ListDistributorModels(
		        UnityAction<AsyncResult<EzListDistributorModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.DescribeDistributorModels(
                    new DescribeDistributorModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<EzListDistributorModelsResult>(
                            r.Result == null ? null : new EzListDistributorModelsResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  配信設定を認証<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="distributorName">配信設定名</param>
		public IEnumerator GetDistributorModel(
		        UnityAction<AsyncResult<EzGetDistributorModelResult>> callback,
                string namespaceName,
                string distributorName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.GetDistributorModel(
                    new GetDistributorModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithDistributorName(distributorName),
                    r => cb.Invoke(
                        new AsyncResult<EzGetDistributorModelResult>(
                            r.Result == null ? null : new EzGetDistributorModelResult(r.Result),
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
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="stampTask">実行するスタンプタスク</param>
		/// <param name="keyId">スタンプシートの暗号化に使用した暗号鍵GRN</param>
		/// <param name="contextStack">スタンプシートの実行状況を記録するスタックメモリ</param>
		public IEnumerator RunStampTask(
		        UnityAction<AsyncResult<EzRunStampTaskResult>> callback,
                string namespaceName,
                string stampTask,
                string keyId,
                string contextStack=null
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.RunStampTask(
                    new RunStampTaskRequest()
                        .WithNamespaceName(namespaceName)
                        .WithStampTask(stampTask)
                        .WithKeyId(keyId)
                        .WithContextStack(contextStack),
                    r => cb.Invoke(
                        new AsyncResult<EzRunStampTaskResult>(
                            r.Result == null ? null : new EzRunStampTaskResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  スタンプシートを実行<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="stampSheet">実行するスタンプタスク</param>
		/// <param name="keyId">スタンプシートの暗号化に使用した暗号鍵GRN</param>
		/// <param name="contextStack">スタンプシートの実行状況を記録するスタックメモリ</param>
		public IEnumerator RunStampSheet(
		        UnityAction<AsyncResult<EzRunStampSheetResult>> callback,
                string namespaceName,
                string stampSheet,
                string keyId,
                string contextStack=null
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.RunStampSheet(
                    new RunStampSheetRequest()
                        .WithNamespaceName(namespaceName)
                        .WithStampSheet(stampSheet)
                        .WithKeyId(keyId)
                        .WithContextStack(contextStack),
                    r => cb.Invoke(
                        new AsyncResult<EzRunStampSheetResult>(
                            r.Result == null ? null : new EzRunStampSheetResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  スタンプタスク・スタンプシートを一括実行<br />
		///    <br />
		///    一括実行をすることで、レスポンスタイムを短縮できます。<br />
		///    ただし、スタンプシートの実行の過程で失敗した際には正しくリトライできる保証はありません。<br />
		///    実行に失敗した時に備えて GS2-Log　でスタンプシートの実行ログを残しておき、カスタマーサポートの際に適切な対応ができるようにしておくことを強く推奨します。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="stampSheet">実行するスタンプタスク</param>
		/// <param name="keyId">スタンプシートの暗号化に使用した暗号鍵GRN</param>
		public IEnumerator RunStampSheetExpress(
		        UnityAction<AsyncResult<EzRunStampSheetExpressResult>> callback,
                string namespaceName,
                string stampSheet,
                string keyId
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.RunStampSheetExpress(
                    new RunStampSheetExpressRequest()
                        .WithNamespaceName(namespaceName)
                        .WithStampSheet(stampSheet)
                        .WithKeyId(keyId),
                    r => cb.Invoke(
                        new AsyncResult<EzRunStampSheetExpressResult>(
                            r.Result == null ? null : new EzRunStampSheetExpressResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  スタンプタスクを実行<br />
		///    <br />
		///    ネームスペースの指定を省略することで、<br />
		///    ログが記録できない・リソース溢れ処理が実行されないなどの副作用があります。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="stampTask">実行するスタンプタスク</param>
		/// <param name="keyId">スタンプシートの暗号化に使用した暗号鍵GRN</param>
		/// <param name="contextStack">スタンプシートの実行状況を記録するスタックメモリ</param>
		public IEnumerator RunStampTaskWithoutNamespace(
		        UnityAction<AsyncResult<EzRunStampTaskWithoutNamespaceResult>> callback,
                string stampTask,
                string keyId,
                string contextStack=null
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.RunStampTaskWithoutNamespace(
                    new RunStampTaskWithoutNamespaceRequest()
                        .WithStampTask(stampTask)
                        .WithKeyId(keyId)
                        .WithContextStack(contextStack),
                    r => cb.Invoke(
                        new AsyncResult<EzRunStampTaskWithoutNamespaceResult>(
                            r.Result == null ? null : new EzRunStampTaskWithoutNamespaceResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  スタンプシートを実行<br />
		///    <br />
		///    ネームスペースの指定を省略することで、<br />
		///    ログが記録できない・リソース溢れ処理が実行されないなどの副作用があります。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="stampSheet">実行するスタンプタスク</param>
		/// <param name="keyId">スタンプシートの暗号化に使用した暗号鍵GRN</param>
		/// <param name="contextStack">スタンプシートの実行状況を記録するスタックメモリ</param>
		public IEnumerator RunStampSheetWithoutNamespace(
		        UnityAction<AsyncResult<EzRunStampSheetWithoutNamespaceResult>> callback,
                string stampSheet,
                string keyId,
                string contextStack=null
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.RunStampSheetWithoutNamespace(
                    new RunStampSheetWithoutNamespaceRequest()
                        .WithStampSheet(stampSheet)
                        .WithKeyId(keyId)
                        .WithContextStack(contextStack),
                    r => cb.Invoke(
                        new AsyncResult<EzRunStampSheetWithoutNamespaceResult>(
                            r.Result == null ? null : new EzRunStampSheetWithoutNamespaceResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  スタンプタスク・スタンプシートを一括実行<br />
		///    <br />
		///     一括実行をすることで、レスポンスタイムを短縮できます。<br />
		///     ただし、スタンプシートの実行の過程で失敗した際には正しくリトライできる保証はありません。<br />
		///     実行に失敗した時に備えて GS2-Log　でスタンプシートの実行ログを残しておき、カスタマーサポートの際に適切な対応ができるようにしておくことを強く推奨します。<br />
		///    <br />
		///    ネームスペースの指定を省略することで、<br />
		///    ログが記録できない・リソース溢れ処理が実行されないなどの副作用があります。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="stampSheet">実行するスタンプタスク</param>
		/// <param name="keyId">スタンプシートの暗号化に使用した暗号鍵GRN</param>
		public IEnumerator RunStampSheetExpressWithoutNamespace(
		        UnityAction<AsyncResult<EzRunStampSheetExpressWithoutNamespaceResult>> callback,
                string stampSheet,
                string keyId
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.RunStampSheetExpressWithoutNamespace(
                    new RunStampSheetExpressWithoutNamespaceRequest()
                        .WithStampSheet(stampSheet)
                        .WithKeyId(keyId),
                    r => cb.Invoke(
                        new AsyncResult<EzRunStampSheetExpressWithoutNamespaceResult>(
                            r.Result == null ? null : new EzRunStampSheetExpressWithoutNamespaceResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
	}
}