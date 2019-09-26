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

namespace Gs2.Core
{
	public class Gs2Constant {

		/** リクエストのタイムスタンプの有効レンジ(±sec) */
		public const int RequestValidTimeRange = 60 * 5;

		/** リトライ回数 */
		public const int RetryNum = 3;

		/** リトライウェイト(msec) */
		public const int RetryWait = 1000;

		/** APIエンドポイント */
		public const string EndpointHost = "https://{service}.{region}.gen2.gs2io.com";

	}
}