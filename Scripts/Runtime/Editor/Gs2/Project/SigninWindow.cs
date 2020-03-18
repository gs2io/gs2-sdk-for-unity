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
using Gs2.Editor.Core;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Core.Util;
using Gs2.Gs2Project;
using Gs2.Gs2Project.Request;
using UnityEditor;
using UnityEngine;

namespace Gs2.Editor.Project
{
    public class SigninWindow : EditorWindow
    {
        private string _email;
        private string _password;
        private bool _saveEmail;
        private bool _savePassword;

        [MenuItem ("Game Server Services/ログイン")]
        private static void Open ()
        {
            GetWindow(typeof(SigninWindow), true, "Sign-in to GS2");
        }

        public void OnEnable()
        {
            _email = PlayerPrefs.GetString("io.gs2.email", "");
            _password = PlayerPrefs.GetString("io.gs2.password", "");
            _saveEmail = bool.Parse(PlayerPrefs.GetString("io.gs2.save.email", "false"));
            _savePassword = bool.Parse(PlayerPrefs.GetString("io.gs2.save.password", "false"));
        }
        
        void OnGUI() {
            _email = EditorGUILayout.TextField("E-Mail", _email);
            _password = EditorGUILayout.PasswordField("Password", _password);
            _saveEmail = GUILayout.Toggle(_saveEmail, "E-Mail を保存する");
            _savePassword = GUILayout.Toggle(_savePassword, "Password を保存する");

            if (GUILayout.Button("ログイン"))
            {
                RunCoroutineUtil.Run(new Gs2ProjectRestClient(
                    Context.Session
                ).SignIn(
                    new SignInRequest()
                        .WithEmail(_email)
                        .WithPassword(_password),
                    r =>
                    {
                        if (r.Error != null)
                        {
                            EditorUtility.DisplayDialog("Error", r.Error.Message, "OK");
                        }
                        else
                        {
                            if (_saveEmail)
                            {
                                PlayerPrefs.SetString("io.gs2.email", _email);
                            }
                            else
                            {
                                PlayerPrefs.DeleteKey("io.gs2.email");
                            }
                            if (_savePassword)
                            {
                                PlayerPrefs.SetString("io.gs2.password", _password);
                            }
                            else
                            {
                                PlayerPrefs.DeleteKey("io.gs2.password");
                            }
                            
                            PlayerPrefs.SetString("io.gs2.save.email", _saveEmail.ToString());
                            PlayerPrefs.SetString("io.gs2.save.password", _savePassword.ToString());
                            
                            PlayerPrefs.Save();
                            
                            Context.AccountToken = r.Result.accountToken;
                            GetWindow(typeof(SelectProjectWindow), true, "Select your project").Show();
                            Close();
                        }
                    }
                ));

            }
            
            GUILayout.Label("");
            if (GUILayout.Button("新規登録"))
            {
                Application.OpenURL("https://gs2.io/");
            }
            
            Repaint();
        } 
    }
}