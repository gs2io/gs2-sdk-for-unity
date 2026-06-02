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
using Gs2.Gs2Schedule.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
#if UNITY_2017_1_OR_NEWER
using UnityEngine;
using UnityEngine.Scripting;
#endif

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Schedule.Model
{

#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzTrigger
	{
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string TriggerId;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string Name;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public long CreatedAt;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public long ExpiresAt;

        public Gs2.Gs2Schedule.Model.Trigger ToModel()
        {
            return new Gs2.Gs2Schedule.Model.Trigger {
                TriggerId = TriggerId,
                Name = Name,
                CreatedAt = CreatedAt,
                ExpiresAt = ExpiresAt,
            };
        }

        public static EzTrigger FromModel(Gs2.Gs2Schedule.Model.Trigger model)
        {
            return new EzTrigger {
                TriggerId = model.TriggerId == null ? null : model.TriggerId,
                Name = model.Name == null ? null : model.Name,
                CreatedAt = model.CreatedAt ?? 0,
                ExpiresAt = model.ExpiresAt ?? 0,
            };
        }
    }
}