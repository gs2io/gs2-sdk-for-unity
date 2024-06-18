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

namespace Gs2.Editor.ResourceTree.Gs2Buff.Editor
{
    public static class BuffEntryModelEditorExt
    {
        public static void OnGUI(Gs2.Gs2Buff.Model.BuffEntryModel item) {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField("BuffEntryModelId", item.BuffEntryModelId);
            EditorGUILayout.TextField("Name", item.Name);
            EditorGUILayout.TextField("Metadata", item.Metadata);
            EditorGUILayout.TextField("Expression", item.Expression);
            EditorGUILayout.TextField("TargetType", item.TargetType);
            if (item.TargetModel == null) {
                EditorGUILayout.TextField("TargetModel", "");
            }
            else {
                EditorGUILayout.LabelField("TargetModel");
                EditorGUI.indentLevel++;
                BuffTargetModelEditorExt.OnGUI(item.TargetModel);
                EditorGUI.indentLevel--;
            }
            if (item.TargetAction == null) {
                EditorGUILayout.TextField("TargetAction", "");
            }
            else {
                EditorGUILayout.LabelField("TargetAction");
                EditorGUI.indentLevel++;
                BuffTargetActionEditorExt.OnGUI(item.TargetAction);
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.TextField("Priority", item.Priority?.ToString());
            EditorGUILayout.TextField("ApplyPeriodScheduleEventId", item.ApplyPeriodScheduleEventId);
            EditorGUI.EndDisabledGroup();
        }
    }
}