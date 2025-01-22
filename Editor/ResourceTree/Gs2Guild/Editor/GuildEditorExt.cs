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
    public static class GuildEditorExt
    {
        public static void OnGUI(Gs2.Gs2Guild.Model.Guild item) {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField("GuildId", item.GuildId);
            EditorGUILayout.TextField("GuildModelName", item.GuildModelName);
            EditorGUILayout.TextField("Name", item.Name);
            EditorGUILayout.TextField("DisplayName", item.DisplayName);
            EditorGUILayout.TextField("Attribute1", item.Attribute1?.ToString());
            EditorGUILayout.TextField("Attribute2", item.Attribute2?.ToString());
            EditorGUILayout.TextField("Attribute3", item.Attribute3?.ToString());
            EditorGUILayout.TextField("Attribute4", item.Attribute4?.ToString());
            EditorGUILayout.TextField("Attribute5", item.Attribute5?.ToString());
            EditorGUILayout.TextField("Metadata", item.Metadata);
            EditorGUILayout.TextField("JoinPolicy", item.JoinPolicy);
            EditorGUILayout.TextField("GuildMemberDefaultRole", item.GuildMemberDefaultRole);
            EditorGUILayout.TextField("CurrentMaximumMemberCount", item.CurrentMaximumMemberCount?.ToString());
            EditorGUILayout.TextField("CreatedAt", UnixTime.FromUnixTime(item.CreatedAt ?? 0).ToString(CultureInfo.CurrentUICulture));
            EditorGUILayout.TextField("UpdatedAt", UnixTime.FromUnixTime(item.UpdatedAt ?? 0).ToString(CultureInfo.CurrentUICulture));
            EditorGUILayout.TextField("Revision", item.Revision?.ToString());
            EditorGUI.EndDisabledGroup();
        }
    }
}