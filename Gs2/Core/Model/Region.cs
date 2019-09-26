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
using System.Linq;

namespace Gs2.Core.Model
{
	public enum Region
	{
		/** アジアパシフィック北東1(東京) */
		ApNortheast1
	}

	public static class RegionExt
	{
		public static string DisplayName(this Region region)
		{
			string[] names = { 
				"ap-northeast-1"
			};
			return names[(int)region];
		}
		
		public static Region ValueOf(string value)
		{
			foreach (var region in Enum.GetValues(typeof(Region)).Cast<Region>().ToList())
			{
				if (region.DisplayName() == value)
				{
					return region;
				}
			}

			return Region.ApNortheast1;
		}
	}
}