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

using Gs2.Core.Exception;

namespace Gs2.Core.Net
{
	public class Gs2Response
	{
		public string Message { get; private set; }
		public Gs2Exception Error { get; protected set; }

		protected Gs2Response(string message)
		{
			Message = message;
		    Error = null;
		}

		public Gs2Response(Gs2Exception error)
		{
		    Message = null;
			Error = error;
		}
		
	    protected static Gs2Exception ExtractError(string message, long statusCode)
		{
			switch (statusCode)
            {
                case 0:    // インターネット非接続のときに UnityWebRequest のステータスコードは 0 になる
                    return new NoInternetConnectionException(message);
                case 200:
                    return null;
                case 400:
                    return new BadRequestException(message);
                case 401:
                    return new UnauthorizedException(message);
                case 402:
                    return new QuotaLimitExceededException(message);
                case 404:
                    return new NotFoundException(message);
                case 409:
                    return new ConflictException(message);
                case 500:
                    return new InternalServerErrorException(message);
                case 502:
                    return new BadGatewayException(message);
                case 503:
                    return new ServiceUnavailableException(message);
                case 504:
                    return new RequestTimeoutException(message);
                default:
                    return new UnknownException(message);
            }
		}
	}
}
