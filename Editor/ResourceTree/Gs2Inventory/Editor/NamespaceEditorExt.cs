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

namespace Gs2.Editor.ResourceTree.Gs2Inventory.Editor
{
    public static class NamespaceEditorExt
    {
        public static void OnGUI(Gs2.Gs2Inventory.Model.Namespace item) {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField("NamespaceId", item.NamespaceId);
            EditorGUILayout.TextField("Name", item.Name);
            EditorGUILayout.TextField("Description", item.Description);
            if (item.TransactionSetting == null) {
                EditorGUILayout.TextField("TransactionSetting", "");
            }
            else {
                EditorGUILayout.LabelField("TransactionSetting");
                EditorGUI.indentLevel++;
                TransactionSettingEditorExt.OnGUI(item.TransactionSetting);
                EditorGUI.indentLevel--;
            }
            if (item.AcquireScript == null) {
                EditorGUILayout.TextField("AcquireScript", "");
            }
            else {
                EditorGUILayout.LabelField("AcquireScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.AcquireScript);
                EditorGUI.indentLevel--;
            }
            if (item.OverflowScript == null) {
                EditorGUILayout.TextField("OverflowScript", "");
            }
            else {
                EditorGUILayout.LabelField("OverflowScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.OverflowScript);
                EditorGUI.indentLevel--;
            }
            if (item.ConsumeScript == null) {
                EditorGUILayout.TextField("ConsumeScript", "");
            }
            else {
                EditorGUILayout.LabelField("ConsumeScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.ConsumeScript);
                EditorGUI.indentLevel--;
            }
            if (item.SimpleItemAcquireScript == null) {
                EditorGUILayout.TextField("SimpleItemAcquireScript", "");
            }
            else {
                EditorGUILayout.LabelField("SimpleItemAcquireScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.SimpleItemAcquireScript);
                EditorGUI.indentLevel--;
            }
            if (item.SimpleItemConsumeScript == null) {
                EditorGUILayout.TextField("SimpleItemConsumeScript", "");
            }
            else {
                EditorGUILayout.LabelField("SimpleItemConsumeScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.SimpleItemConsumeScript);
                EditorGUI.indentLevel--;
            }
            if (item.BigItemAcquireScript == null) {
                EditorGUILayout.TextField("BigItemAcquireScript", "");
            }
            else {
                EditorGUILayout.LabelField("BigItemAcquireScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.BigItemAcquireScript);
                EditorGUI.indentLevel--;
            }
            if (item.BigItemConsumeScript == null) {
                EditorGUILayout.TextField("BigItemConsumeScript", "");
            }
            else {
                EditorGUILayout.LabelField("BigItemConsumeScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.BigItemConsumeScript);
                EditorGUI.indentLevel--;
            }
            if (item.LogSetting == null) {
                EditorGUILayout.TextField("LogSetting", "");
            }
            else {
                EditorGUILayout.LabelField("LogSetting");
                EditorGUI.indentLevel++;
                LogSettingEditorExt.OnGUI(item.LogSetting);
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.TextField("CreatedAt", UnixTime.FromUnixTime(item.CreatedAt ?? 0).ToString(CultureInfo.CurrentUICulture));
            EditorGUILayout.TextField("UpdatedAt", UnixTime.FromUnixTime(item.UpdatedAt ?? 0).ToString(CultureInfo.CurrentUICulture));
            EditorGUILayout.TextField("Revision", item.Revision?.ToString());
            EditorGUI.EndDisabledGroup();
        }
    }
}