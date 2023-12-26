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

namespace Gs2.Editor.ResourceTree.Gs2Grade.Editor
{
    public static class GradeEntryModelEditorExt
    {
        public static void OnGUI(Gs2.Gs2Grade.Model.GradeEntryModel item) {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField("Metadata", item.Metadata);
            EditorGUILayout.TextField("RankCapValue", item.RankCapValue?.ToString());
            EditorGUILayout.TextField("PropertyIdRegex", item.PropertyIdRegex);
            EditorGUILayout.TextField("GradeUpPropertyIdRegex", item.GradeUpPropertyIdRegex);
            EditorGUI.EndDisabledGroup();
        }
    }
}