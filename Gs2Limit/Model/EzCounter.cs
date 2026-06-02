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
using Gs2.Gs2Limit.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
#if UNITY_2017_1_OR_NEWER
using UnityEngine;
using UnityEngine.Scripting;
#endif

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Limit.Model
{

#if UNITY_2017_1_OR_NEWER
	[Preserve]
#endif
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzCounter
	{
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string CounterId;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string LimitName;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public string Name;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public int Count;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public long CreatedAt;
#if UNITY_2017_1_OR_NEWER
		[SerializeField]
#endif
		public long UpdatedAt;

        public Gs2.Gs2Limit.Model.Counter ToModel()
        {
            return new Gs2.Gs2Limit.Model.Counter {
                CounterId = CounterId,
                LimitName = LimitName,
                Name = Name,
                Count = Count,
                CreatedAt = CreatedAt,
                UpdatedAt = UpdatedAt,
            };
        }

        public static EzCounter FromModel(Gs2.Gs2Limit.Model.Counter model)
        {
            return new EzCounter {
                CounterId = model.CounterId == null ? null : model.CounterId,
                LimitName = model.LimitName == null ? null : model.LimitName,
                Name = model.Name == null ? null : model.Name,
                Count = model.Count ?? 0,
                CreatedAt = model.CreatedAt ?? 0,
                UpdatedAt = model.UpdatedAt ?? 0,
            };
        }
    }
}