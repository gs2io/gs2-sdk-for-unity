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
	public class EzStoreContentModel
	{
		[SerializeField]
		public string Name;
		[SerializeField]
		public string Metadata;
		[SerializeField]
		public Gs2.Unity.Gs2Money2.Model.EzAppleAppStoreContent AppleAppStore;
		[SerializeField]
		public Gs2.Unity.Gs2Money2.Model.EzGooglePlayContent GooglePlay;

        public Gs2.Gs2Money2.Model.StoreContentModel ToModel()
        {
            return new Gs2.Gs2Money2.Model.StoreContentModel {
                Name = Name,
                Metadata = Metadata,
                AppleAppStore = AppleAppStore?.ToModel(),
                GooglePlay = GooglePlay?.ToModel(),
            };
        }

        public static EzStoreContentModel FromModel(Gs2.Gs2Money2.Model.StoreContentModel model)
        {
            return new EzStoreContentModel {
                Name = model.Name == null ? null : model.Name,
                Metadata = model.Metadata == null ? null : model.Metadata,
                AppleAppStore = model.AppleAppStore == null ? null : Gs2.Unity.Gs2Money2.Model.EzAppleAppStoreContent.FromModel(model.AppleAppStore),
                GooglePlay = model.GooglePlay == null ? null : Gs2.Unity.Gs2Money2.Model.EzGooglePlayContent.FromModel(model.GooglePlay),
            };
        }
    }
}