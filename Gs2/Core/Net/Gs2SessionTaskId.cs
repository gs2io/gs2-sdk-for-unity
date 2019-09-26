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

namespace Gs2.Core.Net
{
	public class Gs2SessionTaskId
	{
		private const int InvalidIdValue = 0;
		private const int LoginIdValue = 1;
		private const int ReservedIdValueMax = 10000;

		public static readonly Gs2SessionTaskId InvalidId = new Gs2SessionTaskId(InvalidIdValue);
		public static readonly Gs2SessionTaskId LoginId = new Gs2SessionTaskId(LoginIdValue);

		private readonly int _value;

		private Gs2SessionTaskId(int value)
		{
			_value = value;
		}

		public Gs2SessionTaskId(string value)
		{
			int num;
			_value = int.TryParse(value, out num) ? num : InvalidIdValue;
		}

		public string ToString()
		{
			return _value.ToString();
		}
		
		public static bool operator ==(Gs2SessionTaskId gs2SessionTaskId1, Gs2SessionTaskId gs2SessionTaskId2)
		{
			return gs2SessionTaskId1._value == gs2SessionTaskId2._value;
		}

		public static bool operator !=(Gs2SessionTaskId gs2SessionTaskId1, Gs2SessionTaskId gs2SessionTaskId2)
		{
			return gs2SessionTaskId1 != gs2SessionTaskId2;
		}

		public class Generator
		{
			private int _valueCounter = InvalidIdValue;

			public Gs2SessionTaskId Issue()
			{
				if (++_valueCounter <= ReservedIdValueMax)
				{
					_valueCounter = ReservedIdValueMax + 1;
				}

				return new Gs2SessionTaskId(_valueCounter);
			}
		}
	}
}
