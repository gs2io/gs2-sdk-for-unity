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

namespace Gs2.Editor.ResourceTree.Gs2Formation.Editor
{
    public static class MoldModelEditorExt
    {
        public static void OnGUI(Gs2.Gs2Formation.Model.MoldModel item) {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField("MoldModelId", item.MoldModelId);
            EditorGUILayout.TextField("Name", item.Name);
            EditorGUILayout.TextField("Metadata", item.Metadata);
            EditorGUILayout.TextField("InitialMaxCapacity", item.InitialMaxCapacity?.ToString());
            EditorGUILayout.TextField("MaxCapacity", item.MaxCapacity?.ToString());
            if (item.FormModel == null) {
                EditorGUILayout.TextField("FormModel", "");
            }
            else {
                EditorGUILayout.LabelField("FormModel");
                EditorGUI.indentLevel++;
                FormModelEditorExt.OnGUI(item.FormModel);
                EditorGUI.indentLevel--;
            }
            EditorGUI.EndDisabledGroup();
        }
    }
}