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

namespace Gs2.Editor.ResourceTree.Gs2Schedule.Editor
{
    public static class RepeatScheduleEditorExt
    {
        public static void OnGUI(Gs2.Gs2Schedule.Model.RepeatSchedule item) {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField("RepeatCount", item.RepeatCount?.ToString());
            EditorGUILayout.TextField("CurrentRepeatStartAt", UnixTime.FromUnixTime(item.CurrentRepeatStartAt ?? 0).ToString(CultureInfo.CurrentUICulture));
            EditorGUILayout.TextField("CurrentRepeatEndAt", UnixTime.FromUnixTime(item.CurrentRepeatEndAt ?? 0).ToString(CultureInfo.CurrentUICulture));
            EditorGUILayout.TextField("LastRepeatEndAt", UnixTime.FromUnixTime(item.LastRepeatEndAt ?? 0).ToString(CultureInfo.CurrentUICulture));
            EditorGUILayout.TextField("NextRepeatStartAt", UnixTime.FromUnixTime(item.NextRepeatStartAt ?? 0).ToString(CultureInfo.CurrentUICulture));
            EditorGUI.EndDisabledGroup();
        }
    }
}