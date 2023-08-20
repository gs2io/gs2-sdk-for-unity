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

using Gs2.Gs2Inventory;
using Gs2.Unity.Gs2Inventory.Model;
using Gs2.Unity.Gs2Inventory.Result;
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
namespace Gs2.Unity.Gs2Inventory
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
		private readonly Gs2InventoryWebSocketClient _client;
		private readonly Gs2InventoryRestClient _restClient;

		public Client(Gs2.Unity.Util.Profile profile)
		{
			_profile = profile;
			_client = new Gs2InventoryWebSocketClient(profile.Gs2Session);
			if (profile.checkRevokeCertificate)
			{
				_restClient = new Gs2InventoryRestClient(profile.Gs2RestSession);
			}
			else
			{
				_restClient = new Gs2InventoryRestClient(profile.Gs2RestSession, new DisabledCertificateHandler());
			}
		}

        public IEnumerator GetInventoryModel(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzGetInventoryModelResult>> callback,
                string namespaceName,
                string inventoryName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.GetInventoryModel(
                    new Gs2.Gs2Inventory.Request.GetInventoryModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithInventoryName(inventoryName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzGetInventoryModelResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Inventory.Result.EzGetInventoryModelResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListInventoryModels(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzListInventoryModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.DescribeInventoryModels(
                    new Gs2.Gs2Inventory.Request.DescribeInventoryModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzListInventoryModelsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Inventory.Result.EzListInventoryModelsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetItemModel(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzGetItemModelResult>> callback,
                string namespaceName,
                string inventoryName,
                string itemName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.GetItemModel(
                    new Gs2.Gs2Inventory.Request.GetItemModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithInventoryName(inventoryName)
                        .WithItemName(itemName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzGetItemModelResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Inventory.Result.EzGetItemModelResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListItemModels(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzListItemModelsResult>> callback,
                string namespaceName,
                string inventoryName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.DescribeItemModels(
                    new Gs2.Gs2Inventory.Request.DescribeItemModelsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithInventoryName(inventoryName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzListItemModelsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Inventory.Result.EzListItemModelsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetInventory(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzGetInventoryResult>> callback,
		        GameSession session,
                string namespaceName,
                string inventoryName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetInventory(
                    new Gs2.Gs2Inventory.Request.GetInventoryRequest()
                        .WithNamespaceName(namespaceName)
                        .WithInventoryName(inventoryName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzGetInventoryResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Inventory.Result.EzGetInventoryResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListInventories(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzListInventoriesResult>> callback,
		        GameSession session,
                string namespaceName,
                string pageToken = null,
                int? limit = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeInventories(
                    new Gs2.Gs2Inventory.Request.DescribeInventoriesRequest()
                        .WithNamespaceName(namespaceName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzListInventoriesResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Inventory.Result.EzListInventoriesResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator Consume(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzConsumeResult>> callback,
		        GameSession session,
                string namespaceName,
                string inventoryName,
                string itemName,
                long consumeCount,
                string itemSetName = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.ConsumeItemSet(
                    new Gs2.Gs2Inventory.Request.ConsumeItemSetRequest()
                        .WithNamespaceName(namespaceName)
                        .WithInventoryName(inventoryName)
                        .WithItemName(itemName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithConsumeCount(consumeCount)
                        .WithItemSetName(itemSetName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzConsumeResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Inventory.Result.EzConsumeResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetItem(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzGetItemResult>> callback,
		        GameSession session,
                string namespaceName,
                string inventoryName,
                string itemName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.GetItemSet(
                    new Gs2.Gs2Inventory.Request.GetItemSetRequest()
                        .WithNamespaceName(namespaceName)
                        .WithInventoryName(inventoryName)
                        .WithItemName(itemName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzGetItemResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Inventory.Result.EzGetItemResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetItemWithSignature(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzGetItemWithSignatureResult>> callback,
		        GameSession session,
                string namespaceName,
                string inventoryName,
                string itemName,
                string keyId,
                string itemSetName = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.GetItemWithSignature(
                    new Gs2.Gs2Inventory.Request.GetItemWithSignatureRequest()
                        .WithNamespaceName(namespaceName)
                        .WithInventoryName(inventoryName)
                        .WithItemName(itemName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithItemSetName(itemSetName)
                        .WithKeyId(keyId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzGetItemWithSignatureResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Inventory.Result.EzGetItemWithSignatureResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListItems(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzListItemsResult>> callback,
		        GameSession session,
                string namespaceName,
                string inventoryName,
                string pageToken = null,
                int? limit = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeItemSets(
                    new Gs2.Gs2Inventory.Request.DescribeItemSetsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithInventoryName(inventoryName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzListItemsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Inventory.Result.EzListItemsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetSimpleInventoryModel(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzGetSimpleInventoryModelResult>> callback,
                string namespaceName,
                string inventoryName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.GetSimpleInventoryModel(
                    new Gs2.Gs2Inventory.Request.GetSimpleInventoryModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithInventoryName(inventoryName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzGetSimpleInventoryModelResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Inventory.Result.EzGetSimpleInventoryModelResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListSimpleInventoryModels(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzListSimpleInventoryModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.DescribeSimpleInventoryModels(
                    new Gs2.Gs2Inventory.Request.DescribeSimpleInventoryModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzListSimpleInventoryModelsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Inventory.Result.EzListSimpleInventoryModelsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetSimpleItemModel(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzGetSimpleItemModelResult>> callback,
                string namespaceName,
                string inventoryName,
                string itemName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.GetSimpleItemModel(
                    new Gs2.Gs2Inventory.Request.GetSimpleItemModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithInventoryName(inventoryName)
                        .WithItemName(itemName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzGetSimpleItemModelResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Inventory.Result.EzGetSimpleItemModelResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListSimpleItemModels(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzListSimpleItemModelsResult>> callback,
                string namespaceName,
                string inventoryName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.DescribeSimpleItemModels(
                    new Gs2.Gs2Inventory.Request.DescribeSimpleItemModelsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithInventoryName(inventoryName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzListSimpleItemModelsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Inventory.Result.EzListSimpleItemModelsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ConsumeSimpleItems(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzConsumeSimpleItemsResult>> callback,
		        GameSession session,
                string namespaceName,
                string inventoryName,
                List<Gs2.Unity.Gs2Inventory.Model.EzConsumeCount> consumeCounts
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.ConsumeSimpleItems(
                    new Gs2.Gs2Inventory.Request.ConsumeSimpleItemsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithInventoryName(inventoryName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithConsumeCounts(consumeCounts?.Select(v => {
                            return v?.ToModel();
                        }).ToArray()),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzConsumeSimpleItemsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Inventory.Result.EzConsumeSimpleItemsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetSimpleItem(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzGetSimpleItemResult>> callback,
		        GameSession session,
                string namespaceName,
                string inventoryName,
                string itemName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetSimpleItem(
                    new Gs2.Gs2Inventory.Request.GetSimpleItemRequest()
                        .WithNamespaceName(namespaceName)
                        .WithInventoryName(inventoryName)
                        .WithItemName(itemName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzGetSimpleItemResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Inventory.Result.EzGetSimpleItemResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetSimpleItemWithSignature(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzGetSimpleItemWithSignatureResult>> callback,
		        GameSession session,
                string namespaceName,
                string inventoryName,
                string itemName,
                string keyId
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.GetSimpleItemWithSignature(
                    new Gs2.Gs2Inventory.Request.GetSimpleItemWithSignatureRequest()
                        .WithNamespaceName(namespaceName)
                        .WithInventoryName(inventoryName)
                        .WithItemName(itemName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithKeyId(keyId),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzGetSimpleItemWithSignatureResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Inventory.Result.EzGetSimpleItemWithSignatureResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListSimpleItems(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzListSimpleItemsResult>> callback,
		        GameSession session,
                string namespaceName,
                string inventoryName,
                string pageToken = null,
                int? limit = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeSimpleItems(
                    new Gs2.Gs2Inventory.Request.DescribeSimpleItemsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithInventoryName(inventoryName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzListSimpleItemsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Inventory.Result.EzListSimpleItemsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetBigInventoryModel(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzGetBigInventoryModelResult>> callback,
                string namespaceName,
                string inventoryName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.GetBigInventoryModel(
                    new Gs2.Gs2Inventory.Request.GetBigInventoryModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithInventoryName(inventoryName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzGetBigInventoryModelResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Inventory.Result.EzGetBigInventoryModelResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListBigInventoryModels(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzListBigInventoryModelsResult>> callback,
                string namespaceName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.DescribeBigInventoryModels(
                    new Gs2.Gs2Inventory.Request.DescribeBigInventoryModelsRequest()
                        .WithNamespaceName(namespaceName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzListBigInventoryModelsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Inventory.Result.EzListBigInventoryModelsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetBigItemModel(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzGetBigItemModelResult>> callback,
                string namespaceName,
                string inventoryName,
                string itemName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _client.GetBigItemModel(
                    new Gs2.Gs2Inventory.Request.GetBigItemModelRequest()
                        .WithNamespaceName(namespaceName)
                        .WithInventoryName(inventoryName)
                        .WithItemName(itemName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzGetBigItemModelResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Inventory.Result.EzGetBigItemModelResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListBigItemModels(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzListBigItemModelsResult>> callback,
                string namespaceName,
                string inventoryName
        )
		{
            yield return _profile.Run(
                callback,
                null,
                cb => _restClient.DescribeBigItemModels(
                    new Gs2.Gs2Inventory.Request.DescribeBigItemModelsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithInventoryName(inventoryName),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzListBigItemModelsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Inventory.Result.EzListBigItemModelsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ConsumeBigItem(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzConsumeBigItemResult>> callback,
		        GameSession session,
                string namespaceName,
                string inventoryName,
                decimal consumeCount
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.ConsumeBigItem(
                    new Gs2.Gs2Inventory.Request.ConsumeBigItemRequest()
                        .WithNamespaceName(namespaceName)
                        .WithInventoryName(inventoryName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithConsumeCount(consumeCount.ToString("0")),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzConsumeBigItemResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Inventory.Result.EzConsumeBigItemResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator GetBigItem(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzGetBigItemResult>> callback,
		        GameSession session,
                string namespaceName,
                string inventoryName,
                string itemName
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _client.GetBigItem(
                    new Gs2.Gs2Inventory.Request.GetBigItemRequest()
                        .WithNamespaceName(namespaceName)
                        .WithInventoryName(inventoryName)
                        .WithItemName(itemName)
                        .WithAccessToken(session.AccessToken.Token),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzGetBigItemResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Inventory.Result.EzGetBigItemResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}

        public IEnumerator ListBigItems(
		        UnityAction<AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzListBigItemsResult>> callback,
		        GameSession session,
                string namespaceName,
                string inventoryName,
                string pageToken = null,
                int? limit = null
        )
		{
            yield return _profile.Run(
                callback,
		        session,
                cb => _restClient.DescribeBigItems(
                    new Gs2.Gs2Inventory.Request.DescribeBigItemsRequest()
                        .WithNamespaceName(namespaceName)
                        .WithInventoryName(inventoryName)
                        .WithAccessToken(session.AccessToken.Token)
                        .WithPageToken(pageToken)
                        .WithLimit(limit),
                    r => cb.Invoke(
                        new AsyncResult<Gs2.Unity.Gs2Inventory.Result.EzListBigItemsResult>(
                            r.Result == null ? null : Gs2.Unity.Gs2Inventory.Result.EzListBigItemsResult.FromModel(r.Result),
                            r.Error
                        )
                    )
                )
            );
		}
    }
}