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
using Gs2.Gs2Grade.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Scripting;

// ReSharper disable once CheckNamespace
namespace Gs2.Unity.Gs2Grade.Model
{

	[Preserve]
	[System.Serializable]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class EzAcquireActionRate
	{
		[SerializeField]
		public string Name;
		[SerializeField]
		public string Mode;
		[SerializeField]
		public List<double> Rates;
		[SerializeField]
		public List<string> BigRates;

        public Gs2.Gs2Grade.Model.AcquireActionRate ToModel()
        {
            return new Gs2.Gs2Grade.Model.AcquireActionRate {
                Name = Name,
                Mode = Mode,
                Rates = Rates?.Select(v => {
                    return v;
                }).ToArray(),
                BigRates = BigRates?.Select(v => {
                    return v;
                }).ToArray(),
            };
        }

        public static EzAcquireActionRate FromModel(Gs2.Gs2Grade.Model.AcquireActionRate model)
        {
            return new EzAcquireActionRate {
                Name = model.Name == null ? null : model.Name,
                Mode = model.Mode == null ? null : model.Mode,
                Rates = model.Rates == null ? new List<double>() : model.Rates.Select(v => {
                    return v;
                }).ToList(),
                BigRates = model.BigRates == null ? new List<string>() : model.BigRates.Select(v => {
                    return v;
                }).ToList(),
            };
        }
    }
}