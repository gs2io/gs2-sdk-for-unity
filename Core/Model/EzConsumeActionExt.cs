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

using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Core.Domain;
using Gs2.Unity.Gs2Inventory.Model;
using Gs2.Unity.Util;
using Gs2.Util.LitJson;
using UnityEngine.Events;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Core.Model
{
	[Preserve]
	[Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public static class EzConsumeActionExt
	{
		public static Gs2Future<bool> Satisfy(this EzConsumeAction self, Gs2Domain domain, IGameSession session, Action onCauseChange) 
		{
			IEnumerator Impl(Gs2Future<bool> result)
			{
				switch (self.Action) {
					case "Gs2Enhance:DeleteProgressByUserId":
					{
						var request = Gs2.Gs2Enhance.Request.DeleteProgressByUserIdRequest.FromJson(JsonMapper.ToObject(self.Request));
						var d = domain.Enhance.Namespace(
							request.NamespaceName
						).Me(
							session
						).Progress();
						d.Subscribe(v =>
						{
							onCauseChange.Invoke();
						});
						var future = d.ModelFuture();
						yield return future;
						if (future.Error != null) {
							result.OnError(future.Error);
							yield break;
						}
						result.OnComplete(future.Result != null);
						yield break;
					}
					case "Gs2Exchange:DeleteAwaitByUserId":
					{
						var request = Gs2.Gs2Exchange.Request.DeleteAwaitByUserIdRequest.FromJson(JsonMapper.ToObject(self.Request));
						var d = domain.Exchange.Namespace(
							request.NamespaceName
						).Me(
							session
						).Await(
							request.AwaitName
						);
						d.Subscribe(v =>
						{
							onCauseChange.Invoke();
						});
						var future = d.ModelFuture();
						yield return future;
						if (future.Error != null) {
							result.OnError(future.Error);
							yield break;
						}
						result.OnComplete(future.Result != null);
						yield break;
					}
					case "Gs2Inbox:OpenMessageByUserId":
					{
						var request = Gs2.Gs2Inbox.Request.OpenMessageByUserIdRequest.FromJson(JsonMapper.ToObject(self.Request));
						var d = domain.Inbox.Namespace(
							request.NamespaceName
						).Me(
							session
						).Message(
							request.MessageName
						);
						d.Subscribe(v =>
						{
							onCauseChange.Invoke();
						});
						var future = d.ModelFuture();
						yield return future;
						if (future.Error != null) {
							result.OnError(future.Error);
							yield break;
						}
						if (future.Result == null) {
							result.OnComplete(false);
							yield break;
						}
						result.OnComplete(!future.Result.IsRead);
						yield break;
					}
					case "Gs2Inventory:ConsumeItemSetByUserId":
					{
						var request = Gs2.Gs2Inventory.Request.ConsumeItemSetByUserIdRequest.FromJson(JsonMapper.ToObject(self.Request));
						var d = domain.Inventory.Namespace(
							request.NamespaceName
						).Me(
							session
						).Inventory(
							request.InventoryName
						).ItemSet(
							request.ItemName,
							request.ItemSetName
						);
						d.Subscribe((EzItemSet v) =>
						{
							onCauseChange.Invoke();
						});
						var future = d.ModelFuture();
						yield return future;
						if (future.Error != null) {
							result.OnError(future.Error);
							yield break;
						}
						if (future.Result == null) {
							result.OnComplete(false);
							yield break;
						}
						result.OnComplete(future.Result.Sum(v => v.Count) >= request.ConsumeCount);
						yield break;
					}
					case "Gs2Inventory:VerifyReferenceOfByUserId":
					{
						var request = Gs2.Gs2Inventory.Request.VerifyReferenceOfByUserIdRequest.FromJson(JsonMapper.ToObject(self.Request));
						var d = domain.Inventory.Namespace(
							request.NamespaceName
						).Me(
							session
						).Inventory(
							request.InventoryName
						).ItemSet(
							request.ItemName,
							request.ItemSetName
						);
						d.Subscribe((EzItemSet v) =>
						{
							onCauseChange.Invoke();
						});
						var future = d.ModelFuture();
						yield return future;
						if (future.Error != null) {
							result.OnError(future.Error);
							yield break;
						}
						if (future.Result == null) {
							result.OnComplete(false);
							yield break;
						}
						if (future.Result.Length == 0) {
							result.OnComplete(false);
							yield break;
						}
						switch (request.VerifyType) {
							case "not_entry":
								result.OnComplete(!future.Result[0].ReferenceOf.Contains(request.ReferenceOf));
								yield break;
							case "already_entry":
								result.OnComplete(future.Result[0].ReferenceOf.Contains(request.ReferenceOf));
								yield break;
							case "empty":
								result.OnComplete(future.Result[0].ReferenceOf.Count == 0);
								yield break;
							case "not_empty":
								result.OnComplete(future.Result[0].ReferenceOf.Count > 0);
								yield break;
						}
						yield break;
					}
					case "Gs2Limit:CountUpByUserId":
					{
						var request = Gs2.Gs2Limit.Request.CountUpByUserIdRequest.FromJson(JsonMapper.ToObject(self.Request));
						var d = domain.Limit.Namespace(
							request.NamespaceName
						).Me(
							session
						).Counter(
							request.LimitName,
							request.CounterName
						);
						d.Subscribe(v =>
						{
							onCauseChange.Invoke();
						});
						var future = d.ModelFuture();
						yield return future;
						if (future.Error != null) {
							result.OnError(future.Error);
							yield break;
						}
						if (future.Result == null) {
							result.OnComplete(false);
							yield break;
						}
						result.OnComplete(future.Result.Count + request.CountUpValue <= request.MaxValue);
						yield break;
					}
					case "Gs2Mission:ReceiveByUserId":
					{
						var request = Gs2.Gs2Mission.Request.ReceiveByUserIdRequest.FromJson(JsonMapper.ToObject(self.Request));
						var d = domain.Mission.Namespace(
							request.NamespaceName
						).Me(
							session
						).Complete(
							request.MissionGroupName
						);
						d.Subscribe(v =>
						{
							onCauseChange.Invoke();
						});
						var future = d.ModelFuture();
						yield return future;
						if (future.Error != null) {
							result.OnError(future.Error);
							yield break;
						}
						if (future.Result == null) {
							result.OnComplete(false);
							yield break;
						}
						result.OnComplete(!future.Result.ReceivedMissionTaskNames.Contains(request.MissionTaskName));
						yield break;
					}
					case "Gs2Money:WithdrawByUserId":
					{
						var request = Gs2.Gs2Money.Request.WithdrawByUserIdRequest.FromJson(JsonMapper.ToObject(self.Request));
						var d = domain.Money.Namespace(
							request.NamespaceName
						).Me(
							session
						).Wallet(
							request.Slot ?? 0
						);
						d.Subscribe(v =>
						{
							onCauseChange.Invoke();
						});
						var future = d.ModelFuture();
						yield return future;
						if (future.Error != null) {
							result.OnError(future.Error);
							yield break;
						}
						if (future.Result == null) {
							result.OnComplete(false);
							yield break;
						}
						if (request.PaidOnly ?? false) {
							result.OnComplete(future.Result.Paid > request.Count);
						}
						else {
							result.OnComplete(future.Result.Free + future.Result.Paid > request.Count);
						}
						yield break;
					}
					case "Gs2Money:RecordReceipt":
					{
						result.OnComplete(true);
						yield break;
					}
					case "Gs2SerialKey:UseByUserId":
					{
						var request = Gs2.Gs2SerialKey.Request.UseByUserIdRequest.FromJson(JsonMapper.ToObject(self.Request));
						var d = domain.SerialKey.Namespace(
							request.NamespaceName
						).Me(
							session
						).SerialKey(
							request.Code
						);
						d.Subscribe(v =>
						{
							onCauseChange.Invoke();
						});
						var future = d.ModelFuture();
						yield return future;
						if (future.Error != null) {
							result.OnError(future.Error);
							yield break;
						}
						if (future.Result == null) {
							result.OnComplete(false);
							yield break;
						}
						result.OnComplete(future.Result.Status == "Active");
						yield break;
					}
					case "Gs2Stamina:ConsumeStaminaByUserId":
					{
						var request = Gs2.Gs2Stamina.Request.ConsumeStaminaByUserIdRequest.FromJson(JsonMapper.ToObject(self.Request));
						var d = domain.Stamina.Namespace(
							request.NamespaceName
						).Me(
							session
						).Stamina(
							request.StaminaName
						);
						d.Subscribe(v =>
						{
							onCauseChange.Invoke();
						});
						var future = d.ModelFuture();
						yield return future;
						if (future.Error != null) {
							result.OnError(future.Error);
							yield break;
						}
						if (future.Result == null) {
							result.OnComplete(false);
							yield break;
						}
						result.OnComplete(future.Result.Value > request.ConsumeValue);
						yield break;
					}
				}
				result.OnComplete(true);
				yield return null;
			}

			return new Gs2InlineFuture<bool>(Impl);
		}
    }
}