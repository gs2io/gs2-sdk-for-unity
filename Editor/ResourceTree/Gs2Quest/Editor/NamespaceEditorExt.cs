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

namespace Gs2.Editor.ResourceTree.Gs2Quest.Editor
{
    public static class NamespaceEditorExt
    {
        public static void OnGUI(Gs2.Gs2Quest.Model.Namespace item) {
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
            if (item.StartQuestScript == null) {
                EditorGUILayout.TextField("StartQuestScript", "");
            }
            else {
                EditorGUILayout.LabelField("StartQuestScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.StartQuestScript);
                EditorGUI.indentLevel--;
            }
            if (item.CompleteQuestScript == null) {
                EditorGUILayout.TextField("CompleteQuestScript", "");
            }
            else {
                EditorGUILayout.LabelField("CompleteQuestScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.CompleteQuestScript);
                EditorGUI.indentLevel--;
            }
            if (item.FailedQuestScript == null) {
                EditorGUILayout.TextField("FailedQuestScript", "");
            }
            else {
                EditorGUILayout.LabelField("FailedQuestScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.FailedQuestScript);
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
            EditorGUI.EndDisabledGroup();
        }
    }
}