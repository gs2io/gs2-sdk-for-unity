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

namespace Gs2.Editor.ResourceTree.Gs2Friend.Editor
{
    public static class NamespaceEditorExt
    {
        public static void OnGUI(Gs2.Gs2Friend.Model.Namespace item) {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField("NamespaceId", item.NamespaceId);
            EditorGUILayout.TextField("Name", item.Name);
            EditorGUILayout.TextField("Description", item.Description);
            if (item.FollowScript == null) {
                EditorGUILayout.TextField("FollowScript", "");
            }
            else {
                EditorGUILayout.LabelField("FollowScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.FollowScript);
                EditorGUI.indentLevel--;
            }
            if (item.UnfollowScript == null) {
                EditorGUILayout.TextField("UnfollowScript", "");
            }
            else {
                EditorGUILayout.LabelField("UnfollowScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.UnfollowScript);
                EditorGUI.indentLevel--;
            }
            if (item.SendRequestScript == null) {
                EditorGUILayout.TextField("SendRequestScript", "");
            }
            else {
                EditorGUILayout.LabelField("SendRequestScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.SendRequestScript);
                EditorGUI.indentLevel--;
            }
            if (item.CancelRequestScript == null) {
                EditorGUILayout.TextField("CancelRequestScript", "");
            }
            else {
                EditorGUILayout.LabelField("CancelRequestScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.CancelRequestScript);
                EditorGUI.indentLevel--;
            }
            if (item.AcceptRequestScript == null) {
                EditorGUILayout.TextField("AcceptRequestScript", "");
            }
            else {
                EditorGUILayout.LabelField("AcceptRequestScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.AcceptRequestScript);
                EditorGUI.indentLevel--;
            }
            if (item.RejectRequestScript == null) {
                EditorGUILayout.TextField("RejectRequestScript", "");
            }
            else {
                EditorGUILayout.LabelField("RejectRequestScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.RejectRequestScript);
                EditorGUI.indentLevel--;
            }
            if (item.DeleteFriendScript == null) {
                EditorGUILayout.TextField("DeleteFriendScript", "");
            }
            else {
                EditorGUILayout.LabelField("DeleteFriendScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.DeleteFriendScript);
                EditorGUI.indentLevel--;
            }
            if (item.UpdateProfileScript == null) {
                EditorGUILayout.TextField("UpdateProfileScript", "");
            }
            else {
                EditorGUILayout.LabelField("UpdateProfileScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.UpdateProfileScript);
                EditorGUI.indentLevel--;
            }
            if (item.FollowNotification == null) {
                EditorGUILayout.TextField("FollowNotification", "");
            }
            else {
                EditorGUILayout.LabelField("FollowNotification");
                EditorGUI.indentLevel++;
                NotificationSettingEditorExt.OnGUI(item.FollowNotification);
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
            if (item.CancelRequestNotification == null) {
                EditorGUILayout.TextField("CancelRequestNotification", "");
            }
            else {
                EditorGUILayout.LabelField("CancelRequestNotification");
                EditorGUI.indentLevel++;
                NotificationSettingEditorExt.OnGUI(item.CancelRequestNotification);
                EditorGUI.indentLevel--;
            }
            if (item.AcceptRequestNotification == null) {
                EditorGUILayout.TextField("AcceptRequestNotification", "");
            }
            else {
                EditorGUILayout.LabelField("AcceptRequestNotification");
                EditorGUI.indentLevel++;
                NotificationSettingEditorExt.OnGUI(item.AcceptRequestNotification);
                EditorGUI.indentLevel--;
            }
            if (item.RejectRequestNotification == null) {
                EditorGUILayout.TextField("RejectRequestNotification", "");
            }
            else {
                EditorGUILayout.LabelField("RejectRequestNotification");
                EditorGUI.indentLevel++;
                NotificationSettingEditorExt.OnGUI(item.RejectRequestNotification);
                EditorGUI.indentLevel--;
            }
            if (item.DeleteFriendNotification == null) {
                EditorGUILayout.TextField("DeleteFriendNotification", "");
            }
            else {
                EditorGUILayout.LabelField("DeleteFriendNotification");
                EditorGUI.indentLevel++;
                NotificationSettingEditorExt.OnGUI(item.DeleteFriendNotification);
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