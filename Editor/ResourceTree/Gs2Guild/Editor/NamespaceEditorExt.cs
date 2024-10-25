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

namespace Gs2.Editor.ResourceTree.Gs2Guild.Editor
{
    public static class NamespaceEditorExt
    {
        public static void OnGUI(Gs2.Gs2Guild.Model.Namespace item) {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField("NamespaceId", item.NamespaceId);
            EditorGUILayout.TextField("Name", item.Name);
            EditorGUILayout.TextField("Description", item.Description);
            if (item.ChangeNotification == null) {
                EditorGUILayout.TextField("ChangeNotification", "");
            }
            else {
                EditorGUILayout.LabelField("ChangeNotification");
                EditorGUI.indentLevel++;
                NotificationSettingEditorExt.OnGUI(item.ChangeNotification);
                EditorGUI.indentLevel--;
            }
            if (item.JoinNotification == null) {
                EditorGUILayout.TextField("JoinNotification", "");
            }
            else {
                EditorGUILayout.LabelField("JoinNotification");
                EditorGUI.indentLevel++;
                NotificationSettingEditorExt.OnGUI(item.JoinNotification);
                EditorGUI.indentLevel--;
            }
            if (item.LeaveNotification == null) {
                EditorGUILayout.TextField("LeaveNotification", "");
            }
            else {
                EditorGUILayout.LabelField("LeaveNotification");
                EditorGUI.indentLevel++;
                NotificationSettingEditorExt.OnGUI(item.LeaveNotification);
                EditorGUI.indentLevel--;
            }
            if (item.ChangeMemberNotification == null) {
                EditorGUILayout.TextField("ChangeMemberNotification", "");
            }
            else {
                EditorGUILayout.LabelField("ChangeMemberNotification");
                EditorGUI.indentLevel++;
                NotificationSettingEditorExt.OnGUI(item.ChangeMemberNotification);
                EditorGUI.indentLevel--;
            }
            if (item.ReceiveRequestNotification == null) {
                EditorGUILayout.TextField("ReceiveRequestNotification", "");
            }
            else {
                EditorGUILayout.LabelField("ReceiveRequestNotification");
                EditorGUI.indentLevel++;
                NotificationSettingEditorExt.OnGUI(item.ReceiveRequestNotification);
                EditorGUI.indentLevel--;
            }
            if (item.RemoveRequestNotification == null) {
                EditorGUILayout.TextField("RemoveRequestNotification", "");
            }
            else {
                EditorGUILayout.LabelField("RemoveRequestNotification");
                EditorGUI.indentLevel++;
                NotificationSettingEditorExt.OnGUI(item.RemoveRequestNotification);
                EditorGUI.indentLevel--;
            }
            if (item.CreateGuildScript == null) {
                EditorGUILayout.TextField("CreateGuildScript", "");
            }
            else {
                EditorGUILayout.LabelField("CreateGuildScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.CreateGuildScript);
                EditorGUI.indentLevel--;
            }
            if (item.UpdateGuildScript == null) {
                EditorGUILayout.TextField("UpdateGuildScript", "");
            }
            else {
                EditorGUILayout.LabelField("UpdateGuildScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.UpdateGuildScript);
                EditorGUI.indentLevel--;
            }
            if (item.JoinGuildScript == null) {
                EditorGUILayout.TextField("JoinGuildScript", "");
            }
            else {
                EditorGUILayout.LabelField("JoinGuildScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.JoinGuildScript);
                EditorGUI.indentLevel--;
            }
            if (item.LeaveGuildScript == null) {
                EditorGUILayout.TextField("LeaveGuildScript", "");
            }
            else {
                EditorGUILayout.LabelField("LeaveGuildScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.LeaveGuildScript);
                EditorGUI.indentLevel--;
            }
            if (item.ChangeRoleScript == null) {
                EditorGUILayout.TextField("ChangeRoleScript", "");
            }
            else {
                EditorGUILayout.LabelField("ChangeRoleScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.ChangeRoleScript);
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