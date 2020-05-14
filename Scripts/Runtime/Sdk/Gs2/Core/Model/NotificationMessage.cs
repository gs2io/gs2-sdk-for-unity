/*
 * Copyright 2016 Game Server Services, Inc. or its affiliates. All Rights
 * Reserved.
 *
 * Licensed under the Apache License, Version 2.0(the "License").
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

using LitJson;

namespace Gs2.Core.Model
{
    public class NotificationMessage
    {
        public string issuer { set; get; }
        
        public string subject { set; get; }
                
        public string payload { set; get; }

        public static NotificationMessage FromDict(JsonData data)
        {
            return new NotificationMessage
            {
                issuer = data.Keys.Contains("issuer") ? (string)data["issuer"] : null,
                subject = data.Keys.Contains("subject") ? (string)data["subject"] : null,
                payload = data.Keys.Contains("payload") ? (string)data["payload"] : null,
            };
        }
    }
}