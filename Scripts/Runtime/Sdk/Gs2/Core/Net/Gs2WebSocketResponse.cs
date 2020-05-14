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
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Gs2.Core.Model;
using Gs2.Core.Exception;
using LitJson;
using UnityEngine;
using UnityEngine.Scripting;

namespace Gs2.Core.Net
{
	public class Gs2WebSocketResponse : Gs2Response
	{
		public Gs2SessionTaskId Gs2SessionTaskId;

		public JsonData Body;

		[Preserve]
		private class Gs2Message
	    {
		    public string type { set; get; }
		    
	        /** Gs2SessionTaskId */
	        public string requestId { set; get; }

	        /** HTTP ステータスコード */
	        public int? status { set; get; }
	        
	        /** メッセージ本体 */
	        public JsonData body { set; get; }

	        public static Gs2Message FromDict(JsonData data)
	        {
		        if (data == null)
		        {
			        return new Gs2Message();
		        }
		        return new Gs2Message
		        {
			        type = data.Keys.Contains("type") ? (string)data["type"] : null,
			        requestId = data.Keys.Contains("requestId") ? (string)data["requestId"] : null,
			        status = data.Keys.Contains("status") ? (int?)data["status"] : null,
			        body = data.Keys.Contains("body") ? data["body"] : null,
		        };
	        }
	    }

	    public Gs2WebSocketResponse(string message) : base(message)
	    {
	        try
	        {
		        var gs2Message = Gs2Message.FromDict(JsonMapper.ToObject(message));
		        Body = gs2Message.body;

		        var errorMessage = "";
		        if (gs2Message.status != 200)
		        {
			        var error = GeneralError.FromDict(gs2Message.body);
			        if (error != null)
			        {
				        errorMessage = error.Message;
			        }
			        else
			        {
				        errorMessage = message;
			        }
		        }

		        Error = ExtractError(errorMessage, gs2Message.status ?? 0);
		        Gs2SessionTaskId = new Gs2SessionTaskId(gs2Message.requestId);
	        }
	        catch (System.Exception e)
	        {
		        Error = new UnknownException("JSON parsing error: \n" + message);
		        Gs2SessionTaskId = Gs2SessionTaskId.InvalidId;
	        }
	    }
	}
}

