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

namespace Gs2.Editor.ResourceTree.Gs2Matchmaking.Editor
{
    public static class NamespaceEditorExt
    {
        public static void OnGUI(Gs2.Gs2Matchmaking.Model.Namespace item) {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField("NamespaceId", item.NamespaceId);
            EditorGUILayout.TextField("Name", item.Name);
            EditorGUILayout.TextField("Description", item.Description);
            EditorGUILayout.TextField("EnableRating", item.EnableRating?.ToString());
            EditorGUILayout.TextField("EnableDisconnectDetection", item.EnableDisconnectDetection);
            EditorGUILayout.TextField("DisconnectDetectionTimeoutSeconds", item.DisconnectDetectionTimeoutSeconds?.ToString());
            EditorGUILayout.TextField("CreateGatheringTriggerType", item.CreateGatheringTriggerType);
            EditorGUILayout.TextField("CreateGatheringTriggerRealtimeNamespaceId", item.CreateGatheringTriggerRealtimeNamespaceId);
            EditorGUILayout.TextField("CreateGatheringTriggerScriptId", item.CreateGatheringTriggerScriptId);
            EditorGUILayout.TextField("CompleteMatchmakingTriggerType", item.CompleteMatchmakingTriggerType);
            EditorGUILayout.TextField("CompleteMatchmakingTriggerRealtimeNamespaceId", item.CompleteMatchmakingTriggerRealtimeNamespaceId);
            EditorGUILayout.TextField("CompleteMatchmakingTriggerScriptId", item.CompleteMatchmakingTriggerScriptId);
            EditorGUILayout.TextField("EnableCollaborateSeasonRating", item.EnableCollaborateSeasonRating);
            EditorGUILayout.TextField("CollaborateSeasonRatingNamespaceId", item.CollaborateSeasonRatingNamespaceId);
            EditorGUILayout.TextField("CollaborateSeasonRatingTtl", item.CollaborateSeasonRatingTtl?.ToString());
            if (item.ChangeRatingScript == null) {
                EditorGUILayout.TextField("ChangeRatingScript", "");
            }
            else {
                EditorGUILayout.LabelField("ChangeRatingScript");
                EditorGUI.indentLevel++;
                ScriptSettingEditorExt.OnGUI(item.ChangeRatingScript);
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
            if (item.CompleteNotification == null) {
                EditorGUILayout.TextField("CompleteNotification", "");
            }
            else {
                EditorGUILayout.LabelField("CompleteNotification");
                EditorGUI.indentLevel++;
                NotificationSettingEditorExt.OnGUI(item.CompleteNotification);
                EditorGUI.indentLevel--;
            }
            if (item.ChangeRatingNotification == null) {
                EditorGUILayout.TextField("ChangeRatingNotification", "");
            }
            else {
                EditorGUILayout.LabelField("ChangeRatingNotification");
                EditorGUI.indentLevel++;
                NotificationSettingEditorExt.OnGUI(item.ChangeRatingNotification);
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