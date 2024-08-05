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

namespace Gs2.Editor.ResourceTree.Gs2Log.Editor
{
    public static class ExecuteStampSheetLogCountEditorExt
    {
        public static void OnGUI(Gs2.Gs2Log.Model.ExecuteStampSheetLogCount item) {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField("Service", item.Service);
            EditorGUILayout.TextField("Method", item.Method);
            EditorGUILayout.TextField("UserId", item.UserId);
            EditorGUILayout.TextField("Action", item.Action);
            EditorGUILayout.TextField("Count", item.Count?.ToString());
            EditorGUI.EndDisabledGroup();
        }
    }
}