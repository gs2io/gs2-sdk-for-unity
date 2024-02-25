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
using Gs2.Gs2SeasonRating.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2SeasonRating.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzBallot
	{
		[SerializeField]
		public string UserId;
		[SerializeField]
		public string SeasonName;
		[SerializeField]
		public string SessionName;
		[SerializeField]
		public int NumberOfPlayer;

        public Gs2.Gs2SeasonRating.Model.Ballot ToModel()
        {
            return new Gs2.Gs2SeasonRating.Model.Ballot {
                UserId = UserId,
                SeasonName = SeasonName,
                SessionName = SessionName,
                NumberOfPlayer = NumberOfPlayer,
            };
        }

        public static EzBallot FromModel(Gs2.Gs2SeasonRating.Model.Ballot model)
        {
            return new EzBallot {
                UserId = model.UserId == null ? null : model.UserId,
                SeasonName = model.SeasonName == null ? null : model.SeasonName,
                SessionName = model.SessionName == null ? null : model.SessionName,
                NumberOfPlayer = model.NumberOfPlayer ?? 0,
            };
        }
    }
}