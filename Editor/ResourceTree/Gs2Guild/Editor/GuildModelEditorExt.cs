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
    public static class GuildModelEditorExt
    {
        public static void OnGUI(Gs2.Gs2Guild.Model.GuildModel item) {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField("GuildModelId", item.GuildModelId);
            EditorGUILayout.TextField("Name", item.Name);
            EditorGUILayout.TextField("Metadata", item.Metadata);
            EditorGUILayout.TextField("DefaultMaximumMemberCount", item.DefaultMaximumMemberCount?.ToString());
            EditorGUILayout.TextField("MaximumMemberCount", item.MaximumMemberCount?.ToString());
            EditorGUILayout.TextField("InactivityPeriodDays", item.InactivityPeriodDays?.ToString());
            EditorGUILayout.TextField("GuildMasterRole", item.GuildMasterRole);
            EditorGUILayout.TextField("GuildMemberDefaultRole", item.GuildMemberDefaultRole);
            EditorGUILayout.TextField("RejoinCoolTimeMinutes", item.RejoinCoolTimeMinutes?.ToString());
            EditorGUILayout.TextField("MaxConcurrentJoinGuilds", item.MaxConcurrentJoinGuilds?.ToString());
            EditorGUILayout.TextField("MaxConcurrentGuildMasterCount", item.MaxConcurrentGuildMasterCount?.ToString());
            EditorGUI.EndDisabledGroup();
        }
    }
}