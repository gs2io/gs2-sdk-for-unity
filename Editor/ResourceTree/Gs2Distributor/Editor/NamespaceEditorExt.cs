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

namespace Gs2.Editor.ResourceTree.Gs2Distributor.Editor
{
    public static class NamespaceEditorExt
    {
        public static void OnGUI(Gs2.Gs2Distributor.Model.Namespace item) {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField("NamespaceId", item.NamespaceId);
            EditorGUILayout.TextField("Name", item.Name);
            EditorGUILayout.TextField("Description", item.Description);
            EditorGUILayout.TextField("AssumeUserId", item.AssumeUserId);
            if (item.AutoRunStampSheetNotification == null) {
                EditorGUILayout.TextField("AutoRunStampSheetNotification", "");
            }
            else {
                EditorGUILayout.LabelField("AutoRunStampSheetNotification");
                EditorGUI.indentLevel++;
                NotificationSettingEditorExt.OnGUI(item.AutoRunStampSheetNotification);
                EditorGUI.indentLevel--;
            }
            if (item.AutoRunTransactionNotification == null) {
                EditorGUILayout.TextField("AutoRunTransactionNotification", "");
            }
            else {
                EditorGUILayout.LabelField("AutoRunTransactionNotification");
                EditorGUI.indentLevel++;
                NotificationSettingEditorExt.OnGUI(item.AutoRunTransactionNotification);
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