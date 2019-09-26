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

using Gs2.Core.Model;
using LitJson;

namespace Gs2.Core.Net
{
	public class Gs2RestResponse : Gs2Response
	{
	    public Gs2RestResponse(string message, long statusCode) : base(message)
	    {
		    string errorMessage = null;
		    try
		    {
			    errorMessage = JsonMapper.ToObject<GeneralError>(message).Message;
		    }
		    catch (System.Exception e)
		    {
			    errorMessage = message;
		    }

		    Error = ExtractError(errorMessage, statusCode);
		}
	}
}

