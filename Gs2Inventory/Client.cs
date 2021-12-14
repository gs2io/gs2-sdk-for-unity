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
                cb => _client.GetInventoryModel(
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
    }
}