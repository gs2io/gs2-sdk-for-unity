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

using System;
using System.Collections;
using System.Linq;
using Gs2.Core.Net;
using Gs2.Core.Exception;
using Gs2.Editor.ResourceTree.Core;
using Gs2.Gs2Guild;
using Gs2.Gs2Guild.Request;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Gs2.Editor.ResourceTree.Gs2Guild
{
    public sealed class InboxHolder : AbstractTreeViewItem
    {
        private Guild _parent;

        public InboxHolder(
                int id,
                Guild parent
        ) {
            this.id = id = id * 100;
            this.depth = 5;
            this.displayName = "Inbox";
            this.children = new TreeViewItem[] {
                new Loading(this, ++id, this.depth + 1),
            }.ToList();
            this._parent = parent;
        }

        public override void Reload() {
            this.children = new TreeViewItem[] {
                new Loading(this, id + 1, this.depth + 1),
            }.ToList();
        }

        public override IEnumerator Load(TreeView view, Gs2RestSession session, Func<bool> reload) {
            reload.Invoke();
            yield return null;
        }

        public override void OnGUI() {

        }
    }
}