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

namespace Gs2.Core.Control
{
	public class Gs2Request<T> : IRequest where T : class
	{
        /** GS2認証クライアントID */
	    public string XGs2ClientId { set; get; }

	    public T WithXGs2ClientId(string xGs2ClientId)
		{
			XGs2RequestSign = xGs2ClientId;
			return this as T;
		}
        
	    /** タイムスタンプ */
		public long XGs2Timestamp { set; get; }

	    public T WithXGs2Timestamp(long xGs2Timestamp)
		{
			XGs2Timestamp = xGs2Timestamp;
			return this as T;
		}
        
	    /** GS2認証署名 */
	    public string XGs2RequestSign { set; get; }

	    public T WithXGs2RequestSign(string xGs2RequestSign)
	    {
		    XGs2RequestSign = xGs2RequestSign;
		    return this as T;
	    }
        
	    /** GS2リクエストID */
	    public string requestId { set; get; }

	    public T WithRequestId(string requestId)
	    {
		    this.requestId = requestId;
		    return this as T;
	    }
        
	    /** コンテキストスタック */
	    public string contextStack { set; get; }

	    public T WithContextStack(string contextStack)
	    {
		    this.contextStack = contextStack;
		    return this as T;
	    }
    }
}