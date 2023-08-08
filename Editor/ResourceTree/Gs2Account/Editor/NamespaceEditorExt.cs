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

namespace Gs2.Editor.ResourceTree.Gs2Account.Editor
{
    public static class NamespaceEditorExt
    {
        public static void OnGUI(Gs2.Gs2Account.Model.Namespace item) {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField("NamespaceId", item.NamespaceId);
            EditorGUILayout.TextField("Name", item.Name);
            EditorGUILayout.TextField("Description", item.Description);
            EditorGUILayout.TextField("ChangePasswordIfTakeOver", item.ChangePasswordIfTakeOver?.ToString());
            EditorGUILayout.TextField("DifferentUserIdForLoginAndDataRetention", item.DifferentUserIdForLoginAndDataRetention?.ToString());
            if (item.CreateAccountScript == null) {
                EditorGUILayout.TextField("CreateAccountScript", "");
            }
            else {
                EditorGUILayout.LabelField("CreateAccountScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.CreateAccountScript);
                EditorGUI.indentLevel--;
            }
            if (item.AuthenticationScript == null) {
                EditorGUILayout.TextField("AuthenticationScript", "");
            }
            else {
                EditorGUILayout.LabelField("AuthenticationScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.AuthenticationScript);
                EditorGUI.indentLevel--;
            }
            if (item.CreateTakeOverScript == null) {
                EditorGUILayout.TextField("CreateTakeOverScript", "");
            }
            else {
                EditorGUILayout.LabelField("CreateTakeOverScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.CreateTakeOverScript);
                EditorGUI.indentLevel--;
            }
            if (item.DoTakeOverScript == null) {
                EditorGUILayout.TextField("DoTakeOverScript", "");
            }
            else {
                EditorGUILayout.LabelField("DoTakeOverScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.DoTakeOverScript);
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