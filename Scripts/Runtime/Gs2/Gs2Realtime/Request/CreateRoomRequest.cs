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
using Gs2.Core.Control;
using Gs2.Core.Model;
using Gs2.Gs2Realtime.Model;

namespace Gs2.Gs2Realtime.Request
{
	public class CreateRoomRequest : Gs2Request<CreateRoomRequest>
	{

        /** ネームスペース名 */
        public string namespaceName { set; get; }

        /**
         * ネームスペース名を設定
         *
         * @param namespaceName ネームスペース名
         * @return this
         */
        public CreateRoomRequest WithNamespaceName(string namespaceName) {
            this.namespaceName = namespaceName;
            return this;
        }


        /** ルーム名 */
        public string name { set; get; }

        /**
         * ルーム名を設定
         *
         * @param name ルーム名
         * @return this
         */
        public CreateRoomRequest WithName(string name) {
            this.name = name;
            return this;
        }


        /** IPアドレス */
        public string ipAddress { set; get; }

        /**
         * IPアドレスを設定
         *
         * @param ipAddress IPアドレス
         * @return this
         */
        public CreateRoomRequest WithIpAddress(string ipAddress) {
            this.ipAddress = ipAddress;
            return this;
        }


        /** 待受ポート */
        public int? port { set; get; }

        /**
         * 待受ポートを設定
         *
         * @param port 待受ポート
         * @return this
         */
        public CreateRoomRequest WithPort(int? port) {
            this.port = port;
            return this;
        }


        /** 暗号鍵 */
        public string encryptionKey { set; get; }

        /**
         * 暗号鍵を設定
         *
         * @param encryptionKey 暗号鍵
         * @return this
         */
        public CreateRoomRequest WithEncryptionKey(string encryptionKey) {
            this.encryptionKey = encryptionKey;
            return this;
        }


	}
}