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

using Gs2.Gs2Account.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Account.Model
{
	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzAccount
	{
		[SerializeField]
		public string UserId;
		[SerializeField]
		public string Password;
		[SerializeField]
		public long CreatedAt;

        public Gs2.Gs2Account.Model.Account ToModel()
        {
            return new Gs2.Gs2Account.Model.Account {
                UserId = UserId,
                Password = Password,
                CreatedAt = CreatedAt,
            };
        }

        public static EzAccount FromModel(Gs2.Gs2Account.Model.Account model)
        {
            return new EzAccount {
                UserId = model.UserId == null ? null : model.UserId,
                Password = model.Password == null ? null : model.Password,
                CreatedAt = model.CreatedAt ?? 0,
            };
        }
    }
}