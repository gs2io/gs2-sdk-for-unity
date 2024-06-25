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

namespace Gs2.Editor.ResourceTree.Gs2Money2.Editor
{
    public static class PlatformSettingEditorExt
    {
        public static void OnGUI(Gs2.Gs2Money2.Model.PlatformSetting item) {
            EditorGUI.BeginDisabledGroup(true);
            if (item.AppleAppStore == null) {
                EditorGUILayout.TextField("AppleAppStore", "");
            }
            else {
                EditorGUILayout.LabelField("AppleAppStore");
                EditorGUI.indentLevel++;
                AppleAppStoreSettingEditorExt.OnGUI(item.AppleAppStore);
                EditorGUI.indentLevel--;
            }
            if (item.GooglePlay == null) {
                EditorGUILayout.TextField("GooglePlay", "");
            }
            else {
                EditorGUILayout.LabelField("GooglePlay");
                EditorGUI.indentLevel++;
                GooglePlaySettingEditorExt.OnGUI(item.GooglePlay);
                EditorGUI.indentLevel--;
            }
            if (item.Fake == null) {
                EditorGUILayout.TextField("Fake", "");
            }
            else {
                EditorGUILayout.LabelField("Fake");
                EditorGUI.indentLevel++;
                FakeSettingEditorExt.OnGUI(item.Fake);
                EditorGUI.indentLevel--;
            }
            EditorGUI.EndDisabledGroup();
        }
    }
}