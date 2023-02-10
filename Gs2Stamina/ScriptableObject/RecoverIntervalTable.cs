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
#if UNITY_INCLUDE_TESTS
using UnityEditor;
#endif
using UnityEngine;

namespace Gs2.Unity.Gs2Stamina.ScriptableObject
{
    [CreateAssetMenu(fileName = "RecoverIntervalTable", menuName = "Game Server Services/Gs2Stamina/RecoverIntervalTable")]
    public class RecoverIntervalTable : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string recoverIntervalTableName;

        public string NamespaceName => this.Namespace.NamespaceName;
        public string RecoverIntervalTableName => this.recoverIntervalTableName;

#if UNITY_INCLUDE_TESTS
        public static RecoverIntervalTable Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<RecoverIntervalTable>(assetPath)
            );
        }
#endif

        public static RecoverIntervalTable New(
            Namespace Namespace,
            string recoverIntervalTableName
        )
        {
            var instance = CreateInstance<RecoverIntervalTable>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.recoverIntervalTableName = recoverIntervalTableName;
            return instance;
        }

        public RecoverIntervalTable Clone()
        {
            var instance = CreateInstance<RecoverIntervalTable>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.recoverIntervalTableName = recoverIntervalTableName;
            return instance;
        }
    }
}