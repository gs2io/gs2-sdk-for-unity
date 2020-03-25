﻿/*
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

using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Gs2.Editor.Core
{
    enum Status
    {
        Present,
        Installing,
    }

    public class CloudWeaveWindow : EditorWindow
    {
        private Status _status = Status.Present;

        [MenuItem ("Game Server Services/CloudWeave")]
        public static void Open ()
        {
            GetWindow<CloudWeaveWindow>(false, "CloudWeave");
        }
        
        void OnGUI() 
        {
            var style = new GUIStyle(GUI.skin.label) {wordWrap = true};
            using (new GUILayout.VerticalScope(GUI.skin.box))
            {
                GUILayout.Label("Core", style);
                GUILayout.Label("CloudWeave を利用するために必要となる基本コードセットです。", style);
                GUILayout.Label("Author: Game Server Services", style);
                DrawInstaller("io.gs2.unity.weave.core", null, null);
            }
            GUILayout.Label("");
            using (new GUILayout.VerticalScope(GUI.skin.box))
            {
                GUILayout.Label("Credential", style);
                GUILayout.Label("Game Server Services を利用する為に必要な、ゲーム内に組み込むクレデンシャルを作成します。", style);
                GUILayout.Label("Author: Game Server Services", style);
                DrawInstaller("io.gs2.unity.weave.credential", "Credential", "Gs2.Weave.Credential.Editor.Tutorial.TutorialWindow");
            }
            
            GUILayout.Label("");
            using (new GUILayout.VerticalScope(GUI.skin.box))
            {
                GUILayout.Label("Login", style);
                GUILayout.Label("GS2-Account を使用して、ログイン機能を追加します。", style);
                GUILayout.Label("Author: Game Server Services", style);
                DrawInstaller("io.gs2.unity.weave.login", "LoginSample", "Gs2.Weave.Login.Editor.Tutorial.TutorialWindow");
            }
        }

        private async void DrawInstaller(string packageUrl, string sceneName, string tutorialWindowClass)
        {
            switch (_status)
            {
                case Status.Present:
                    if (GUILayout.Button("インストール"))
                    {
                        _status = Status.Installing;

                        var request = Client.Add(packageUrl);
                        while (request.Status == StatusCode.InProgress)
                        {
                            await Task.Delay(1000);
                        }

                        if (request.Error != null)
                        {
                            Debug.LogError(request.Error.message);
                        }

                        if (sceneName != null)
                        {
                            var scenePath = AssetDatabase
                                .FindAssets("t:SceneAsset")
                                .Select(AssetDatabase.GUIDToAssetPath)
                                .Select(path => AssetDatabase.LoadAssetAtPath(path, typeof(SceneAsset)))
                                .Where(obj => obj != null)
                                .Select(obj => (SceneAsset) obj)
                                .Where(scene => scene.name == sceneName)
                                .Select(AssetDatabase.GetAssetPath)
                                .First(path => path.StartsWith("Packages/" + packageUrl));

                            AssetDatabase.CopyAsset(scenePath, "Assets/Scenes/" + sceneName + ".unity");

                            EditorSceneManager.OpenScene("Assets/Scenes/" + sceneName + ".unity");
                        }
                        
                        
                        if (tutorialWindowClass != null)
                        {
                            var tutorialWindowType = Type.GetType(tutorialWindowClass);
                            GetWindowWithRect(tutorialWindowType, new Rect(0, 0, 700, 700), true, "チュートリアル");
                        }

                        _status = Status.Present;
                    }
                    break;
                case Status.Installing:
                    GUILayout.Button("インストール中… しばらくお待ちください。");
                    break;
            }
        }
    }
}