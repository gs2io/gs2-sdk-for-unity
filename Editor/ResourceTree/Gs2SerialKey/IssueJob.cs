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
using Gs2.Editor.ResourceTree.Gs2SerialKey.Editor;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Gs2.Editor.ResourceTree.Gs2SerialKey
{
    public sealed class IssueJob : AbstractTreeViewItem
    {
        private Gs2.Gs2SerialKey.Model.IssueJob _item;
        private CampaignModel _parent;
        public string NamespaceName => _parent.NamespaceName;
        public string CampaignModelName => _parent.CampaignModelName;
        public string IssueJobName => _item.Name;

        public IssueJob(
                int id,
                CampaignModel parent,
                Gs2.Gs2SerialKey.Model.IssueJob item
        ) {
            this.id = id = id * 100;
            this.depth = 6;
            this.icon = EditorGUIUtility.ObjectContent(null, typeof(GameObject)).image.ToTexture2D();
            this.displayName = item.Name;
            this.children = new TreeViewItem[] {
            }.ToList();
            this._parent = parent;
            this._item = item;
        }

        public override ScriptableObject ToScriptableObject() {
            Gs2.Unity.Gs2SerialKey.ScriptableObject.CampaignModel parent = null;
            var guids = AssetDatabase.FindAssets("t:Gs2.Unity.Gs2SerialKey.ScriptableObject.CampaignModel");
            foreach (var guid in guids) {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var item = AssetDatabase.LoadAssetAtPath<Gs2.Unity.Gs2SerialKey.ScriptableObject.CampaignModel>(path);
                if (
                    item.NamespaceName == NamespaceName &&
                    item.CampaignModelName == CampaignModelName
                ) {
                    parent = item;
                }
            }
            if (parent == null) {
                Debug.LogError("Gs2.Unity.Gs2SerialKey.ScriptableObject.CampaignModel not found.");
                return null;
            }
            var instance = Gs2.Unity.Gs2SerialKey.ScriptableObject.IssueJob.New(
                parent,
                this._item.Name
            );
            instance.name = this._item.Name + "IssueJob";
            return instance;
        }

        public override void OnGUI() {
            IssueJobEditorExt.OnGUI(this._item);
            
            if (GUILayout.Button("Create Reference Object")) {
                var directory = "Assets/Gs2/Resources/SerialKey";
                directory += "/Namespace" + "/" + NamespaceName;
                directory += "/CampaignModel" + "/" + CampaignModelName;
                directory += "/IssueJob" + "/" + IssueJobName;

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