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
using Gs2.Gs2Showcase.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
#if UNITY_2017_1_OR_NEWER
using UnityEngine;
using UnityEngine.Scripting;
#endif

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Showcase.Model
{

#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzDisplayItem
	{
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string DisplayItemId;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string Type;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public Gs2.Unity.Gs2Showcase.Model.EzSalesItem SalesItem;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public Gs2.Unity.Gs2Showcase.Model.EzSalesItemGroup SalesItemGroup;

        public Gs2.Gs2Showcase.Model.DisplayItem ToModel()
        {
            return new Gs2.Gs2Showcase.Model.DisplayItem {
                DisplayItemId = DisplayItemId,
                Type = Type,
                SalesItem = SalesItem?.ToModel(),
                SalesItemGroup = SalesItemGroup?.ToModel(),
            };
        }

        public static EzDisplayItem FromModel(Gs2.Gs2Showcase.Model.DisplayItem model)
        {
            return new EzDisplayItem {
                DisplayItemId = model.DisplayItemId == null ? null : model.DisplayItemId,
                Type = model.Type == null ? null : model.Type,
                SalesItem = model.SalesItem == null ? null : Gs2.Unity.Gs2Showcase.Model.EzSalesItem.FromModel(model.SalesItem),
                SalesItemGroup = model.SalesItemGroup == null ? null : Gs2.Unity.Gs2Showcase.Model.EzSalesItemGroup.FromModel(model.SalesItemGroup),
            };
        }
    }
}