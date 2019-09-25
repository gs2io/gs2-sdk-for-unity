﻿/*
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

using Gs2.Core.Exception;

namespace Gs2.Core
{
	public class AsyncResult<T>
	{
		public AsyncResult (T result, Gs2Exception e)
		{
			Result = result;
			Error = e;
		}

		public T Result { get; private set; }
		public Gs2Exception Error { get; private set; }
	}
}

