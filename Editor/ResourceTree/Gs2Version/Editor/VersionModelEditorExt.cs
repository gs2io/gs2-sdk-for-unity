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

namespace Gs2.Editor.ResourceTree.Gs2Version.Editor
{
    public static class VersionModelEditorExt
    {
        public static void OnGUI(Gs2.Gs2Version.Model.VersionModel item) {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField("VersionModelId", item.VersionModelId);
            EditorGUILayout.TextField("Name", item.Name);
            EditorGUILayout.TextField("Metadata", item.Metadata);
            EditorGUILayout.TextField("Scope", item.Scope);
            EditorGUILayout.TextField("Type", item.Type);
            if (item.CurrentVersion == null) {
                EditorGUILayout.TextField("CurrentVersion", "");
            }
            else {
                EditorGUILayout.LabelField("CurrentVersion");
                EditorGUI.indentLevel++;
                VersionEditorExt.OnGUI(item.CurrentVersion);
                EditorGUI.indentLevel--;
            }
            if (item.WarningVersion == null) {
                EditorGUILayout.TextField("WarningVersion", "");
            }
            else {
                EditorGUILayout.LabelField("WarningVersion");
                EditorGUI.indentLevel++;
                VersionEditorExt.OnGUI(item.WarningVersion);
                EditorGUI.indentLevel--;
            }
            if (item.ErrorVersion == null) {
                EditorGUILayout.TextField("ErrorVersion", "");
            }
            else {
                EditorGUILayout.LabelField("ErrorVersion");
                EditorGUI.indentLevel++;
                VersionEditorExt.OnGUI(item.ErrorVersion);
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.TextField("NeedSignature", item.NeedSignature?.ToString());
            EditorGUILayout.TextField("SignatureKeyId", item.SignatureKeyId);
            EditorGUILayout.TextField("ApproveRequirement", item.ApproveRequirement);
            EditorGUI.EndDisabledGroup();
        }
    }
}