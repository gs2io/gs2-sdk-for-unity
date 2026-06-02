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
using Gs2.Gs2Formation.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
#if UNITY_2017_1_OR_NEWER
using UnityEngine;
using UnityEngine.Scripting;
#endif

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Formation.Model
{

#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzSlotWithSignature
	{
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string Name;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string PropertyType;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string Body;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string Signature;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string Metadata;

        public Gs2.Gs2Formation.Model.SlotWithSignature ToModel()
        {
            return new Gs2.Gs2Formation.Model.SlotWithSignature {
                Name = Name,
                PropertyType = PropertyType,
                Body = Body,
                Signature = Signature,
                Metadata = Metadata,
            };
        }

        public static EzSlotWithSignature FromModel(Gs2.Gs2Formation.Model.SlotWithSignature model)
        {
            return new EzSlotWithSignature {
                Name = model.Name == null ? null : model.Name,
                PropertyType = model.PropertyType == null ? null : model.PropertyType,
                Body = model.Body == null ? null : model.Body,
                Signature = model.Signature == null ? null : model.Signature,
                Metadata = model.Metadata == null ? null : model.Metadata,
            };
        }
    }
}