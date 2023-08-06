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

namespace Gs2.Unity.Gs2Matchmaking.ScriptableObject
{
    public class OwnBallot : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string ratingName;
        public string gatheringName;
        public int numberOfPlayer;
        public string keyId;

        public string NamespaceName => this.Namespace.NamespaceName;
        public string RatingName => this.ratingName;
        public string GatheringName => this.gatheringName;
        public int NumberOfPlayer => this.numberOfPlayer;
        public string KeyId => this.keyId;

#if UNITY_INCLUDE_TESTS
        public static OwnBallot Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<OwnBallot>(assetPath)
            );
        }
#endif

        public static OwnBallot New(
            Namespace Namespace,
            string ratingName,
            string gatheringName,
            int numberOfPlayer,
            string keyId
        )
        {
            var instance = CreateInstance<OwnBallot>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.ratingName = ratingName;
            instance.gatheringName = gatheringName;
            instance.numberOfPlayer = numberOfPlayer;
            instance.keyId = keyId;
            return instance;
        }

        public OwnBallot Clone()
        {
            var instance = CreateInstance<OwnBallot>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.ratingName = ratingName;
            instance.gatheringName = gatheringName;
            instance.numberOfPlayer = numberOfPlayer;
            instance.keyId = keyId;
            return instance;
        }
    }
}