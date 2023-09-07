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
 *
 * deny overwrite
 */

using System.IO;
using System.Linq;
using Gs2.Editor.ResourceTree.Core;
using Gs2.Editor.ResourceTree.Gs2Formation.Editor;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Gs2.Editor.ResourceTree.Gs2Formation
{
    public sealed class OwnForm : AbstractTreeViewItem
    {
        private MoldModel _parent;
        private Gs2.Gs2Formation.Model.FormModel _item;
        public string NamespaceName => _parent.NamespaceName;
        public string MoldModelName => _parent.MoldModelName;
        public string FormModelName => _item.Name;

        public OwnForm(
                int id,
                MoldModel parent,
                Gs2.Gs2Formation.Model.FormModel item
        ) {
            this.id = id = id * 100;
            this.depth = 7;
            this.icon = EditorGUIUtility.ObjectContent(null, typeof(GameObject)).image.ToTexture2D();
            this.displayName = "OwnForm";
            this.children = new TreeViewItem[] {
            }.ToList();
            this._parent = parent;
            this._item = item;
        }

        public override ScriptableObject ToScriptableObject() {
            Gs2.Unity.Gs2Formation.ScriptableObject.OwnMold parent = null;
            var guids = AssetDatabase.FindAssets("t:Gs2.Unity.Gs2Formation.ScriptableObject.OwnMold");
            foreach (var guid in guids) {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var item = AssetDatabase.LoadAssetAtPath<Gs2.Unity.Gs2Formation.ScriptableObject.OwnMold>(path);
                if (
                    item.NamespaceName == NamespaceName &&
                    item.MoldModelName == MoldModelName
                ) {
                    parent = item;
                }
            }
            if (parent == null) {
                Debug.LogError("Gs2.Unity.Gs2Formation.ScriptableObject.OwnMold not found.");
                return null;
            }
            var instance = Gs2.Unity.Gs2Formation.ScriptableObject.OwnForm.New(
                parent,
                0
            );
            instance.name = this._item.Name + "OwnForm";
            return instance;
        }

        public override void OnGUI() {
            if (GUILayout.Button("Create Reference Object")) {
                var directory = "Assets/Gs2/Resources/Formation";
                directory += "/Namespace/" + NamespaceName;
                directory += "/MoldModel/" + MoldModelName;
                directory += "/FormModel/" + FormModelName;

                CreateFolder(directory);

                var instance = ToScriptableObject();
                if (instance != null) {
                    AssetDatabase.CreateAsset(instance, AssetDatabase.GenerateUniqueAssetPath(directory + "/" + instance.name + ".asset"));
                    AssetDatabase.SaveAssets();
                }
            }
        }
    }
}