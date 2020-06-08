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
using Gs2.Gs2Inventory;
using Gs2.Gs2Inventory.Model;
using Gs2.Gs2Inventory.Request;
using Gs2.Gs2Inventory.Result;
using Gs2.Unity.Gs2Inventory.Model;
using Gs2.Unity.Gs2Inventory.Result;
using Gs2.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Unity.Util;
using UnityEngine.Events;

namespace Gs2.Unity.Gs2Inventory
{
	public class Client
	{
		private readonly Gs2.Unity.Util.Profile _profile;
		private readonly Gs2InventoryWebSocketClient _client;
		private readonly Gs2InventoryRestClient _restClient;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2InventoryWebSocketClient(profile.Gs2Session);
			_restClient = new Gs2InventoryRestClient(profile.Gs2RestSession);
		}

		/// <summary>
		///  インベントリモデルの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		public IEnumerator ListInventoryModels(
		        UnityAction<AsyncResult<EzListInventoryModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.DescribeInventoryModels(
                    new DescribeInventoryModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<EzListInventoryModelsResult>(
                            r.Result == null ? null : new EzListInventoryModelsResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  インベントリ名を指定してインベントリモデルを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="inventoryName">インベントリの種類名</param>
		public IEnumerator GetInventoryModel(
		        UnityAction<AsyncResult<EzGetInventoryModelResult>> callback,
                string namespaceName,
                string inventoryName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.GetInventoryModel(
                    new GetInventoryModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithInventoryName(inventoryName),
                    r => cb.Invoke(
                        new AsyncResult<EzGetInventoryModelResult>(
                            r.Result == null ? null : new EzGetInventoryModelResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  インベントリ名を指定してアイテムモデルの一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="inventoryName">インベントリの種類名</param>
		public IEnumerator ListItemModels(
		        UnityAction<AsyncResult<EzListItemModelsResult>> callback,
                string namespaceName,
                string inventoryName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.DescribeItemModels(
                    new DescribeItemModelsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithInventoryName(inventoryName),
                    r => cb.Invoke(
                        new AsyncResult<EzListItemModelsResult>(
                            r.Result == null ? null : new EzListItemModelsResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  インベントリ名とアイテム名を指定してアイテムモデルを取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="inventoryName">インベントリの種類名</param>
		/// <param name="itemName">アイテムモデルの種類名</param>
		public IEnumerator GetItemModel(
		        UnityAction<AsyncResult<EzGetItemModelResult>> callback,
                string namespaceName,
                string inventoryName,
                string itemName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.GetItemModel(
                    new GetItemModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithInventoryName(inventoryName)
                        .WithItemName(itemName),
                    r => cb.Invoke(
                        new AsyncResult<EzGetItemModelResult>(
                            r.Result == null ? null : new EzGetItemModelResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  インベントリの一覧を取得<br />
		///    <br />
		///    ゲームプレイヤーに紐付いたインベントリの一覧を取得します。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="pageToken">データの取得を開始する位置を指定するトークン</param>
		/// <param name="limit">データの取得件数</param>
		public IEnumerator ListInventories(
		        UnityAction<AsyncResult<EzListInventoriesResult>> callback,
		        GameSession session,
                string namespaceName,
                string pageToken=null,
                long? limit=null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeInventories(
                    new DescribeInventoriesRequest()
                        .WithNamespaceName(namespaceName)
                        .WithPageToken(pageToken)
                        .WithLimit(limit)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzListInventoriesResult>(
                            r.Result == null ? null : new EzListInventoriesResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  インベントリ名を指定してインベントリの情報を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="inventoryName">インベントリの種類名</param>
		public IEnumerator GetInventory(
		        UnityAction<AsyncResult<EzGetInventoryResult>> callback,
		        GameSession session,
                string namespaceName,
                string inventoryName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetInventory(
                    new GetInventoryRequest()
                        .WithNamespaceName(namespaceName)
                        .WithInventoryName(inventoryName)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzGetInventoryResult>(
                            r.Result == null ? null : new EzGetInventoryResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  指定したインベントリ内の所有しているアイテム一覧を取得<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="inventoryName">インベントリの種類名</param>
		/// <param name="pageToken">データの取得を開始する位置を指定するトークン</param>
		/// <param name="limit">データの取得件数</param>
		public IEnumerator ListItems(
		        UnityAction<AsyncResult<EzListItemsResult>> callback,
		        GameSession session,
                string namespaceName,
                string inventoryName,
                string pageToken=null,
                long? limit=null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeItemSets(
                    new DescribeItemSetsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithInventoryName(inventoryName)
                        .WithPageToken(pageToken)
                        .WithLimit(limit)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzListItemsResult>(
                            r.Result == null ? null : new EzListItemsResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  インベントリ名とアイテム名を指定してアイテムを取得<br />
		///    <br />
		///    アイテムは複数のスタックに分割されて応答されることがあります。<br />
		///    また、有効期限の異なるアイテムは、必ず別のスタックになります。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="inventoryName">インベントリの種類名</param>
		/// <param name="itemName">アイテムモデルの種類名</param>
		public IEnumerator GetItem(
		        UnityAction<AsyncResult<EzGetItemResult>> callback,
		        GameSession session,
                string namespaceName,
                string inventoryName,
                string itemName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetItemSet(
                    new GetItemSetRequest()
                        .WithNamespaceName(namespaceName)
                        .WithInventoryName(inventoryName)
                        .WithItemName(itemName)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzGetItemResult>(
                            r.Result == null ? null : new EzGetItemResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  インベントリ名とアイテム名を指定して署名付きアイテムを取得<br />
		///    <br />
		///    このAPIによって、APIを呼び出した瞬間に該当アイテムを所有していることを証明することができる<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="inventoryName">インベントリの種類名</param>
		/// <param name="itemName">アイテムモデルの種類名</param>
		/// <param name="itemSetName">アイテムセットを識別する名前</param>
		/// <param name="keyId">署名の発行に使用する暗号鍵 のGRN</param>
		public IEnumerator GetItemWithSignature(
		        UnityAction<AsyncResult<EzGetItemWithSignatureResult>> callback,
		        GameSession session,
                string namespaceName,
                string inventoryName,
                string itemName,
                string keyId,
                string itemSetName=null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetItemWithSignature(
                    new GetItemWithSignatureRequest()
                        .WithNamespaceName(namespaceName)
                        .WithInventoryName(inventoryName)
                        .WithItemName(itemName)
                        .WithItemSetName(itemSetName)
                        .WithKeyId(keyId)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzGetItemWithSignatureResult>(
                            r.Result == null ? null : new EzGetItemWithSignatureResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

		/// <summary>
		///  アイテムを消費<br />
		///    <br />
		///    ゲーム内からアイテムを消費したい場合に使用します。<br />
		///    <br />
		///    GS2のシステムと連携してアイテムの増減を行う場合はこのAPIを使用することはありません。<br />
		///    なぜなら、商品を購入するためや、クエストに参加するために必要な対価の場合は GS2-Showcase や GS2-Quest 上で対価を設定し、<br />
		///    商品の購入時やクエスト参加時に自動的に対価としてアイテムやその他のリソースが消費されるためです。<br />
		///    <br />
		///    そのため、このAPIはGS2を介さない要素のためにアイテムを消費する場合に使用してください。<br />
		/// </summary>
        ///
		/// <returns>IEnumerator</returns>
		/// <param name="callback">コールバックハンドラ</param>
		/// <param name="session">ゲームセッション</param>
		/// <param name="namespaceName">ネームスペース名</param>
		/// <param name="inventoryName">インベントリの名前</param>
		/// <param name="itemName">アイテムマスターの名前</param>
		/// <param name="consumeCount">消費する量</param>
		/// <param name="itemSetName">アイテムセットを識別する名前</param>
		public IEnumerator Consume(
		        UnityAction<AsyncResult<EzConsumeResult>> callback,
		        GameSession session,
                string namespaceName,
                string inventoryName,
                string itemName,
                long consumeCount,
                string itemSetName=null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.ConsumeItemSet(
                    new ConsumeItemSetRequest()
                        .WithNamespaceName(namespaceName)
                        .WithInventoryName(inventoryName)
                        .WithItemName(itemName)
                        .WithConsumeCount(consumeCount)
                        .WithItemSetName(itemSetName)
                        .WithAccessToken(session.AccessToken.token),
                    r => cb.Invoke(
                        new AsyncResult<EzConsumeResult>(
                            r.Result == null ? null : new EzConsumeResult(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
	}
}