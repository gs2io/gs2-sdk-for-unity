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
using Gs2.Editor.ResourceTree.Gs2Enhance.Editor;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Gs2.Editor.ResourceTree.Gs2Enhance
{
    public sealed class Namespace : AbstractTreeViewItem
    {
        private Gs2.Gs2Enhance.Model.Namespace _item;
        public string NamespaceName => _item.Name;

        public Namespace(
                int id,
                Gs2.Gs2Enhance.Model.Namespace item
        ) {
            this.id = id = id * 100;
            this.depth = 2;
            this.icon = EditorGUIUtility.ObjectContent(null, typeof(GameObject)).image.ToTexture2D();
            this.displayName = item.Name;
            this.children = new TreeViewItem[] {
                new RateModelHolder(++id, this)
            }.ToList();
            this._item = item;
        }

        public override ScriptableObject ToScriptableObject() {
            var instance = Gs2.Unity.Gs2Enhance.ScriptableObject.Namespace.New(
                this._item.Name
            );
            instance.name = this._item.Name + "Namespace";
            return instance;
        }

        public override void OnGUI() {
            NamespaceEditorExt.OnGUI(this._item);
            
            if (GUILayout.Button("Create Reference Object")) {
                var directory = "Assets/Gs2/Resources";
                directory += "/Namespace" + "/" + NamespaceName;

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