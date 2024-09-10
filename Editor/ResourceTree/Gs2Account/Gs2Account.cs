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

using System.Linq;
using Gs2.Editor.ResourceTree.Core;
using UnityEditor.IMGUI.Controls;

namespace Gs2.Editor.ResourceTree.Gs2Account
{
    public sealed class Gs2Account : AbstractTreeViewItem
    {
        public Gs2Account(int id) {
            this.id = id = id * 10;
            this.depth = 0;
            this.displayName = "Account";
            this.children = new TreeViewItem[] {
                new NamespaceHolder(++id),
            }.ToList();
        }

        public override void OnGUI() {

        }
    }
}