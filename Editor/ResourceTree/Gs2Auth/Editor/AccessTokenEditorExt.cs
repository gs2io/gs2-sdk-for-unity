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

using System.Globalization;
using Gs2.Core.Util;
using Gs2.Editor.ResourceTree.Core.Editor;
using UnityEditor;

namespace Gs2.Editor.ResourceTree.Gs2Auth.Editor
{
    public static class AccessTokenEditorExt
    {
        public static void OnGUI(Gs2.Gs2Auth.Model.AccessToken item) {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField("Token", item.Token);
            EditorGUILayout.TextField("UserId", item.UserId);
            EditorGUILayout.TextField("FederationFromUserId", item.FederationFromUserId);
            EditorGUILayout.TextField("Expire", UnixTime.FromUnixTime(item.Expire ?? 0).ToString(CultureInfo.CurrentUICulture));
            EditorGUILayout.TextField("TimeOffset", item.TimeOffset?.ToString());
            EditorGUI.EndDisabledGroup();
        }
    }
}