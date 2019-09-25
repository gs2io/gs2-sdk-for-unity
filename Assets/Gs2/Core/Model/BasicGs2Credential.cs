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
using Gs2.Core.Util;
using UnityEngine.Networking;

namespace Gs2.Core.Model
{
	public class BasicGs2Credential : IGs2Credential
	{
		/** クライアントID */
		public string ClientId { get; set; }

		/** クライアントシークレット */
		public string ClientSecret { get; set; }
		
		/** プロジェクトトークン */
		public string ProjectToken  { get; set; }

		public BasicGs2Credential(string clientId, string clientSecret)
		{
			ClientId = clientId;
			ClientSecret = clientSecret;
		}

		public void Authorized(UnityWebRequest request)
		{
			request.SetRequestHeader("X-GS2-CLIENT-ID", ClientId);
			if (ProjectToken != null)
			{
				request.SetRequestHeader("X-GS2-PROJECT-TOKEN", ProjectToken);
			}
		}
	}
}