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

namespace Gs2.Editor.ResourceTree.Gs2Stamina.Editor
{
    public static class StaminaModelEditorExt
    {
        public static void OnGUI(Gs2.Gs2Stamina.Model.StaminaModel item) {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField("StaminaModelId", item.StaminaModelId);
            EditorGUILayout.TextField("Name", item.Name);
            EditorGUILayout.TextField("Metadata", item.Metadata);
            EditorGUILayout.TextField("RecoverIntervalMinutes", item.RecoverIntervalMinutes?.ToString());
            EditorGUILayout.TextField("RecoverValue", item.RecoverValue?.ToString());
            EditorGUILayout.TextField("InitialCapacity", item.InitialCapacity?.ToString());
            EditorGUILayout.TextField("IsOverflow", item.IsOverflow?.ToString());
            EditorGUILayout.TextField("MaxCapacity", item.MaxCapacity?.ToString());
            if (item.MaxStaminaTable == null) {
                EditorGUILayout.TextField("MaxStaminaTable", "");
            }
            else {
                EditorGUILayout.LabelField("MaxStaminaTable");
                EditorGUI.indentLevel++;
                MaxStaminaTableEditorExt.OnGUI(item.MaxStaminaTable);
                EditorGUI.indentLevel--;
            }
            if (item.RecoverIntervalTable == null) {
                EditorGUILayout.TextField("RecoverIntervalTable", "");
            }
            else {
                EditorGUILayout.LabelField("RecoverIntervalTable");
                EditorGUI.indentLevel++;
                RecoverIntervalTableEditorExt.OnGUI(item.RecoverIntervalTable);
                EditorGUI.indentLevel--;
            }
            if (item.RecoverValueTable == null) {
                EditorGUILayout.TextField("RecoverValueTable", "");
            }
            else {
                EditorGUILayout.LabelField("RecoverValueTable");
                EditorGUI.indentLevel++;
                RecoverValueTableEditorExt.OnGUI(item.RecoverValueTable);
                EditorGUI.indentLevel--;
            }
            EditorGUI.EndDisabledGroup();
        }
    }
}