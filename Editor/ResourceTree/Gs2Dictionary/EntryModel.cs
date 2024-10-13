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

using System.IO;
using System.Linq;
using Gs2.Editor.ResourceTree.Core;
using Gs2.Editor.ResourceTree.Gs2Dictionary.Editor;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Gs2.Editor.ResourceTree.Gs2Dictionary
{
    public sealed class EntryModel : AbstractTreeViewItem
    {
        private Gs2.Gs2Dictionary.Model.EntryModel _item;
        private Namespace _parent;
        public string NamespaceName => _parent.NamespaceName;
        public string EntryModelName => _item.Name;

        public EntryModel(
                int id,
                Namespace parent,
                Gs2.Gs2Dictionary.Model.EntryModel item
        ) {
            this.id = id = id * 100;
            this.depth = 4;
            this.icon = EditorGUIUtility.ObjectContent(null, typeof(GameObject)).image.ToTexture2D();
            this.displayName = item.Name;
            this.children = new TreeViewItem[] {
                new OwnEntry(++id, parent, item)
            }.ToList();
            this._parent = parent;
            this._item = item;
        }

        public override ScriptableObject ToScriptableObject() {
            Gs2.Unity.Gs2Dictionary.ScriptableObject.Namespace parent = null;
            var guids = AssetDatabase.FindAssets("t:Gs2.Unity.Gs2Dictionary.ScriptableObject.Namespace");
            foreach (var guid in guids) {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var item = AssetDatabase.LoadAssetAtPath<Gs2.Unity.Gs2Dictionary.ScriptableObject.Namespace>(path);
                if (
                    item.NamespaceName == NamespaceName
                ) {
                    parent = item;
                }
            }
            if (parent == null) {
                Debug.LogError("Gs2.Unity.Gs2Dictionary.ScriptableObject.Namespace not found.");
                return null;
            }
            var instance = Gs2.Unity.Gs2Dictionary.ScriptableObject.EntryModel.New(
                parent,
                this._item.Name
            );
            instance.name = this._item.Name + "EntryModel";
            return instance;
        }

        public override void OnGUI() {
            EntryModelEditorExt.OnGUI(this._item);
            
            if (GUILayout.Button("Create Reference Object")) {
                var directory = "Assets/Gs2/Resources/Dictionary";
                directory += "/Namespace" + "/" + NamespaceName;
                directory += "/EntryModel" + "/" + EntryModelName;

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