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

namespace Gs2.Unity.Gs2Datastore.ScriptableObject
{
    [CreateAssetMenu(fileName = "OwnDataObjectHistory", menuName = "Game Server Services/Gs2Datastore/OwnDataObjectHistory")]
    public class OwnDataObjectHistory : UnityEngine.ScriptableObject
    {
        public OwnDataObject DataObject;
        public string generation;

        public string NamespaceName => this.DataObject.NamespaceName;
        public string DataObjectName => this.DataObject.DataObjectName;
        public string Generation => this.generation;

#if UNITY_INCLUDE_TESTS
        public static OwnDataObjectHistory Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<OwnDataObjectHistory>(assetPath)
            );
        }
#endif

        public static OwnDataObjectHistory New(
            OwnDataObject DataObject,
            string generation
        )
        {
            var instance = CreateInstance<OwnDataObjectHistory>();
            instance.name = "Runtime";
            instance.DataObject = DataObject;
            instance.generation = generation;
            return instance;
        }

        public OwnDataObjectHistory Clone()
        {
            var instance = CreateInstance<OwnDataObjectHistory>();
            instance.name = "Runtime";
            instance.DataObject = DataObject;
            instance.generation = generation;
            return instance;
        }
    }
}