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

namespace Gs2.Editor.ResourceTree.Gs2Chat.Editor
{
    public static class NamespaceEditorExt
    {
        public static void OnGUI(Gs2.Gs2Chat.Model.Namespace item) {
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
            EditorGUILayout.TextField("AllowCreateRoom", item.AllowCreateRoom?.ToString());
            EditorGUILayout.TextField("MessageLifeTimeDays", item.MessageLifeTimeDays?.ToString());
            if (item.PostMessageScript == null) {
                EditorGUILayout.TextField("PostMessageScript", "");
            }
            else {
                EditorGUILayout.LabelField("PostMessageScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.PostMessageScript);
                EditorGUI.indentLevel--;
            }
            if (item.CreateRoomScript == null) {
                EditorGUILayout.TextField("CreateRoomScript", "");
            }
            else {
                EditorGUILayout.LabelField("CreateRoomScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.CreateRoomScript);
                EditorGUI.indentLevel--;
            }
            if (item.DeleteRoomScript == null) {
                EditorGUILayout.TextField("DeleteRoomScript", "");
            }
            else {
                EditorGUILayout.LabelField("DeleteRoomScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.DeleteRoomScript);
                EditorGUI.indentLevel--;
            }
            if (item.SubscribeRoomScript == null) {
                EditorGUILayout.TextField("SubscribeRoomScript", "");
            }
            else {
                EditorGUILayout.LabelField("SubscribeRoomScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.SubscribeRoomScript);
                EditorGUI.indentLevel--;
            }
            if (item.UnsubscribeRoomScript == null) {
                EditorGUILayout.TextField("UnsubscribeRoomScript", "");
            }
            else {
                EditorGUILayout.LabelField("UnsubscribeRoomScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.UnsubscribeRoomScript);
                EditorGUI.indentLevel--;
            }
            if (item.PostNotification == null) {
                EditorGUILayout.TextField("PostNotification", "");
            }
            else {
                EditorGUILayout.LabelField("PostNotification");
                EditorGUI.indentLevel++;
                NotificationSettingEditorExt.OnGUI(item.PostNotification);
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