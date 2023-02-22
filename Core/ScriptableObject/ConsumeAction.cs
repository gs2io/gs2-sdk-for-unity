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
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable CheckNamespace


using Gs2.Core.Control;
using Gs2.Util.LitJson;
using UnityEngine;
#if UNITY_INCLUDE_TESTS
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Core.Consume
{
    public class ConsumeAction : ScriptableObject
    {
        public string action;
        public string request;

        public string Action => this.action;
        public string Request => this.request;

#if UNITY_INCLUDE_TESTS
        public static ConsumeAction Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<ConsumeAction>(assetPath)
            );
        }
#endif

        public static ConsumeAction New(
            string action,
            string request
        )
        {
            var instance = CreateInstance<ConsumeAction>();
            instance.name = "Runtime";
            instance.action = action;
            instance.request = request;
            return instance;
        }

        public ConsumeAction Clone()
        {
            var instance = CreateInstance<ConsumeAction>();
            instance.name = "Runtime";
            instance.action = action;
            instance.request = request;
            return instance;
        }

        private Gs2Request ToRequest() {
            return new Gs2.Core.Model.ConsumeAction {
                Action = Action,
                Request = Request,
            }.ToRequest();
        }

        public string Id => ToRequest().UniqueKey();

        public static ConsumeAction operator *(ConsumeAction x, int y) {
            var request = x.ToRequest() * y;
            return New(
                x.Action,
                JsonMapper.ToJson(request.ToJson())
            );
        }

        public static ConsumeAction operator +(ConsumeAction x, ConsumeAction y) {
            var request1 = x.ToRequest();
            var request2 = y.ToRequest();
            return New(
                x.Action,
                JsonMapper.ToJson((request1 + request2).ToJson())
            );
        }
    }
}