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
using Gs2.Gs2Money2.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Money2.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzSubscribeTransaction
	{
		[SerializeField]
		public string ContentName;
		[SerializeField]
		public string Store;
		[SerializeField]
		public string TransactionId;
		[SerializeField]
		public string StatusDetail;
		[SerializeField]
		public long ExpiresAt;

        public Gs2.Gs2Money2.Model.SubscribeTransaction ToModel()
        {
            return new Gs2.Gs2Money2.Model.SubscribeTransaction {
                ContentName = ContentName,
                Store = Store,
                TransactionId = TransactionId,
                StatusDetail = StatusDetail,
                ExpiresAt = ExpiresAt,
            };
        }

        public static EzSubscribeTransaction FromModel(Gs2.Gs2Money2.Model.SubscribeTransaction model)
        {
            return new EzSubscribeTransaction {
                ContentName = model.ContentName == null ? null : model.ContentName,
                Store = model.Store == null ? null : model.Store,
                TransactionId = model.TransactionId == null ? null : model.TransactionId,
                StatusDetail = model.StatusDetail == null ? null : model.StatusDetail,
                ExpiresAt = model.ExpiresAt ?? 0,
            };
        }
    }
}