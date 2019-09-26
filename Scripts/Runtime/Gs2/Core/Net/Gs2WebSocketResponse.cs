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
using Gs2.Core.Exception;
using LitJson;

namespace Gs2.Core.Net
{
	public class Gs2WebSocketResponse : Gs2Response
	{
		public Gs2SessionTaskId Gs2SessionTaskId;

		private class Gs2Message
	    {
	        /** Gs2SessionTaskId */
	        public string requestId { set; get; }

	        /** HTTP ステータスコード */
	        public int? status { set; get; }
	    }

		public class Gs2Message<T>
		{
			/** メッセージ本体 */
			public T body { set; get; }
		}

	    public Gs2WebSocketResponse(string message) : base(message)
	    {
	        try
	        {
		        var gs2Message = JsonMapper.ToObject<Gs2Message>(message);

		        string errorMessage;
		        try
		        {
			        errorMessage = JsonMapper.ToObject<Gs2Message<GeneralError>>(message).body.Message;
		        }
		        catch (JsonException e)
		        {
			        errorMessage = message;
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

