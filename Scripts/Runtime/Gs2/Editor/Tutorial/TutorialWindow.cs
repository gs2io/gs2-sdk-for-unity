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

using Gs2.CloudWeave;
using UnityEditor;
using UnityEngine;

namespace Gs2.Tutorial
{
    enum Steps
    {
        Hello,
        Toc,
        What1,
        What2,
        What3,
        FirstStep_Credential1,
        FirstStep_Credential2,
        FirstStep_Credential3,
        FirstStep_Credential4,
        FirstStep_Deploy1,
        FirstStep_Deploy2,
        FirstStep_Deploy3,
        FirstStep_Deploy4,
        FirstStep_Deploy5,
        FirstStep_Deploy6,
        FirstStep_Deploy7,
        FirstStep_Deploy8,
        FirstStep_Deploy9,
        FirstStep_Deploy10,
        FirstStep_Deploy11,
        FirstStep_Deploy12,
        FirstStep_CloudWeave1,
        Bye,
    }
    
    public class TutorialWindow : EditorWindow
    {
        private Texture2D _texture = null;

        private Steps _steps = Steps.Hello;
        
        [MenuItem ("Game Server Services/チュートリアル")]
        public static void Open ()
        {
            GetWindowWithRect<TutorialWindow>(new Rect(0, 0, 700, 700), true, "チュートリアル");
        }
        
        void OnGUI() 
        {

            if (_texture == null)
            {
                _texture = Resources.Load("mira") as Texture2D;
            }
            switch (_steps)
            {
                case Steps.Hello:
                    GUILayout.Label("にゃ。誰かきたにゃ？");
                    GUILayout.Label("");
                    GUILayout.Label("わたしは ミラ だにゃ。");
                    GUILayout.Label("これからいわゆるチュートリアルで GS2 について説明するにゃ。");
                    GUILayout.Label("もしかして、もう GS2 に詳しい人かにゃ？");
                    GUILayout.Label("");

                    if (GUILayout.Button("ううん。はじめて"))
                    {
                        _steps = Steps.Toc;
                        Repaint();
                    }

                    if (GUILayout.Button("うん。だから説明はいらないよ"))
                    {
                        _steps = Steps.Bye;
                        Repaint();
                    }
                    break;
                case Steps.Toc:
                    GUILayout.Label("なにについて聞きたいにゃ？");
                    GUILayout.Label("");

                    if (GUILayout.Button("GS2 ってなんなの？"))
                    {
                        _steps = Steps.What1;
                        Repaint();
                    }
                    if (GUILayout.Button("いちばん基本的な使い方を教えて"))
                    {
                        _steps = Steps.FirstStep_Credential1;
                        Repaint();
                    }

                    GUILayout.Label("");
                    
                    if (GUILayout.Button("もういいよ"))
                    {
                        _steps = Steps.Bye;
                        Repaint();
                    }
                    break;
                case Steps.What1:
                case Steps.What2:
                case Steps.What3:
                    What();
                    break;
                case Steps.FirstStep_Credential1:
                case Steps.FirstStep_Credential2:
                case Steps.FirstStep_Credential3:
                case Steps.FirstStep_Credential4:
                    FirstStepCredential();
                    break;
                case Steps.FirstStep_Deploy1:
                case Steps.FirstStep_Deploy2:
                case Steps.FirstStep_Deploy3:
                case Steps.FirstStep_Deploy4:
                case Steps.FirstStep_Deploy5:
                case Steps.FirstStep_Deploy6:
                case Steps.FirstStep_Deploy7:
                case Steps.FirstStep_Deploy8:
                case Steps.FirstStep_Deploy9:
                case Steps.FirstStep_Deploy10:
                case Steps.FirstStep_Deploy11:
                case Steps.FirstStep_Deploy12:
                    FirstStepDeploy();
                    break;
                case Steps.FirstStep_CloudWeave1:
                    FirstStepCloudWeave();
                    break;
                case Steps.Bye:
                    GUILayout.Label("そうにゃか。");
                    GUILayout.Label("いつも使ってくれてありがとうにゃ。");
                    GUILayout.Label("");

                    if (GUILayout.Button("じゃあね"))
                    {
                        Close();
                    }
                    break;
            }
            
            var textureWidth = (float)_texture.width;
            var textureHeight = (float)_texture.height;
                
            if (position.width < textureWidth
                || position.height < textureHeight)
            {
                var shrinkWidth = Mathf.Min(position.width / textureWidth / 2, position.height / textureHeight / 2);

                textureWidth *= shrinkWidth;
                textureHeight *= shrinkWidth;

            }
            
            var posX = position.width - textureWidth;
            var posY = position.height - textureHeight;
            GUI.DrawTexture(new Rect(posX, posY, textureWidth, textureHeight), _texture);
        }

        private void What()
        {
            switch (_steps)
            {
                case Steps.What1:
                    GUILayout.Label("GS2 はゲームサーバの Backend as a Service(BaaS) だにゃ");
                    GUILayout.Label("");
                    GUILayout.Label("ゲームサーバのプログラムは自分たちでプログラミングして、");
                    GUILayout.Label("レンタルしたり買ってきたサーバにインストールして運用していたのが当たり前だったにゃ。");
                    GUILayout.Label("プログラムもサーバへのインストールも運用も全部 GS2 にお任せで、APIを呼び出すだけで使えるサービスにゃ。");
                    GUILayout.Label("");
                    GUILayout.Label("昔はゲームエンジンもみんながゲームを作るごとに作ってたことがあるにゃ。");
                    GUILayout.Label("でも、今の君のように Unity を使ってコンテンツの開発に集中できる環境が整ったのにゃ。");
                    GUILayout.Label("GS2 はゲームサーバにおいて同じことをやろうと思ってるのにゃ！壮大な野望なのにゃ！");
                    GUILayout.Label("");

                    if (GUILayout.Button("なるほど"))
                    {
                        _steps = Steps.What2;
                        Repaint();
                    }
                    break;
                case Steps.What2:
                    GUILayout.Label("GS2 を使うと色んなことができるのにゃ。");
                    GUILayout.Label("");
                    GUILayout.Label("例えば、アカウント管理や課金通貨の管理。クエストの進行管理や、アイテムの所持管理、経験値管理。");
                    GUILayout.Label("ゲーム内ストアの実装や、ガチャみたいな抽選もできるし、マッチメイキングやリアルタイム対戦までなんでもできるのにゃ。");
                    GUILayout.Label("");
                    GUILayout.Label("しかも、拡張性もばっちりだにゃ！");
                    GUILayout.Label("クエストをクリアしたとき、レベルが上がったとき、アイテムを使ったとき…。");
                    GUILayout.Label("いろいろなタイミングで追加の処理をスクリプトで記述できるようになってるにゃ！");
                    GUILayout.Label("これの仕組みをうまく使えば、出来ないことなんて殆どないのにゃ！");
                    GUILayout.Label("");

                    if (GUILayout.Button("へえ。すごいね"))
                    {
                        _steps = Steps.What3;
                        Repaint();
                    }
                    break;
                case Steps.What3:
                    GUILayout.Label("なにより嬉しいのは、ゲームを出したあとだにゃ！");
                    GUILayout.Label("");
                    GUILayout.Label("GS2 は秒間10万回のアクセスを処理できるのにゃ。");
                    GUILayout.Label("だから、安心してどんな規模のゲームでも使ってもらえる上に、");
                    GUILayout.Label("GS2 に任せて運用のことは忘れて、次の新作の開発に移行してしまってもいいのにゃ！");
                    GUILayout.Label("");
                    GUILayout.Label("みんなが気持ちよくゲームを作れる環境を目指してるのにゃ。");
                    GUILayout.Label("");

                    if (GUILayout.Button("ありがとう"))
                    {
                        _steps = Steps.Toc;
                        Repaint();
                    }
                    break;
            }
        }
        
        private void FirstStepCredential()
        {
            switch (_steps)
            {
                case Steps.FirstStep_Credential1:
                    GUILayout.Label("一番最初に一番大切な話をしておくのにゃ。");
                    GUILayout.Label("");
                    GUILayout.Label("GS2 が提供している API のアクセス権限のはなしだにゃ！");
                    GUILayout.Label("API を呼び出すためには `クレデンシャル` が必要なんだにゃ。");
                    GUILayout.Label("聞いたことない単語にゃ？ ミラも最初わからなかったから ググったのにゃ。");
                    GUILayout.Label("");
                    GUILayout.Label("> クレデンシャルとは、ネットワーク セキュリティの世界で使用された場合には、");
                    GUILayout.Label("> IDやパスワードをはじめとする、ユーザ等の認証に用いられる情報の総称を意味します。");
                    GUILayout.Label("https://www.f5.com/ja_jp/services/resources/glossary/credential");
                    GUILayout.Label("");
                    GUILayout.Label("だそうだにゃ！");
                    GUILayout.Label("つまり、API を呼び出すのに必要な認証情報なんだにゃ！");
                    GUILayout.Label("");

                    if (GUILayout.Button("おぼえておく"))
                    {
                        _steps = Steps.FirstStep_Credential2;
                        Repaint();
                    }
                    break;
                case Steps.FirstStep_Credential2:
                    GUILayout.Label("で、GS2 のクレデンシャルにはポリシーっていうのが付けられるのにゃ。");
                    GUILayout.Label("");
                    GUILayout.Label("ポリシー！わかるかにゃあ？？？");
                    GUILayout.Label("ようするに、このクレデンシャルを使ったアクセスではこのAPIを呼び出していいよ。という設定にゃ。");
                    GUILayout.Label("");
                    GUILayout.Label("そうなんだにゃ。GS2 では複数のクレデンシャルを発行できて、");
                    GUILayout.Label("クレデンシャルごとに呼び出せる API を制御できるんだにゃ。");
                    GUILayout.Label("");
                    GUILayout.Label("そんなことが出来て、なにが嬉しいの？って顔をしてるにゃ。");
                    GUILayout.Label("たとえば、経験値を増やすAPIをゲームに埋め込むクレデンシャルで呼び出せたら大事だにゃ。");
                    GUILayout.Label("ゲームバランスは崩壊だにゃ！");
                    GUILayout.Label("");
                    GUILayout.Label("でも、デバッグツールとかで経験値が増やせたら便利だにゃ。");
                    GUILayout.Label("そういう時はデバッグツール用にクレデンシャルを発行して、そこに経験値を増やせるポリシーを付ける。ってわけにゃ。");
                    GUILayout.Label("");

                    if (GUILayout.Button("なるほどね"))
                    {
                        _steps = Steps.FirstStep_Credential3;
                        Repaint();
                    }
                    break;
                case Steps.FirstStep_Credential3:
                    GUILayout.Label("クレデンシャルやポリシーっていうけど、どういう風に設定するの？って顔をしてるにゃ。");
                    GUILayout.Label("");
                    GUILayout.Label("ポリシーは JSON形式のファイルだにゃ。これを管理画面とかから登録して使うにゃ。");
                    GUILayout.Label("いちいち登録しなくても、全部のAPIが呼び出せるポリシー。ゲーム内に組み込んでも安心なポリシー。");
                    GUILayout.Label("そういう基本的なポリシーは GS2 があらかじめ用意してるにゃ。親切だにゃ。ミラに感謝するにゃ。");
                    GUILayout.Label("");
                    
                    using (new GUILayout.VerticalScope(GUI.skin.box))
                    {
                        GUILayout.Label("{");
                        GUILayout.Label("  \"Version\": \"2016-04-01\",");
                        GUILayout.Label("  \"Statements\": [");
                        GUILayout.Label("    {");
                        GUILayout.Label("      \"Effect\": \"Allow\",");
                        GUILayout.Label("      \"Actions\": [");
                        GUILayout.Label("        \"Gs2Account:CreateAccount\"");
                        GUILayout.Label("      ]");
                        GUILayout.Label("    }");
                        GUILayout.Label("  ]");
                        GUILayout.Label("}");
                    }

                    GUILayout.Label("");
                    GUILayout.Label("こんな風に書くと、 GS2-Account の CreateAccount `だけ` が呼び出せるポリシーを作れるにゃ。");
                    GUILayout.Label("細かいことは今は忘れていいにゃ。そんなことができるんだな。ってことだけ覚えるにゃ。");
                    GUILayout.Label("");

                    if (GUILayout.Button("はーい"))
                    {
                        _steps = Steps.FirstStep_Credential4;
                        Repaint();
                    }
                    break;
                case Steps.FirstStep_Credential4:
                    GUILayout.Label("ここまで、説明をわかりやすいくするために一部嘘をついてたのにゃ。");
                    GUILayout.Label("本当にごめんにゃ。でも、君のためだにゃ。");
                    GUILayout.Label("");
                    GUILayout.Label("さっき、クレデンシャルにポリシーが付けられるって言ってたけど、その関係は正しくないのにゃ。");
                    GUILayout.Label("");
                    
                    using (new GUILayout.VerticalScope(GUI.skin.box))
                    {
                        GUILayout.Label("- ユーザ");
                        GUILayout.Label("  - ポリシー");
                        GUILayout.Label("  - クレデンシャル");
                        GUILayout.Label("  - 管理画面ログインID/パスワード");
                    }

                    GUILayout.Label("");
                    GUILayout.Label("実際はこういう構造になっていて、クレデンシャルもポリシーも `ユーザ` の下にいるのにゃ。");
                    GUILayout.Label("見たことない項目で `管理画面ログインID/パスワード` っていうのがあるにゃ。");
                    GUILayout.Label("");
                    GUILayout.Label("なななんと！アカウント登録した E-Mail/パスワード 以外のID/パスワードでも管理画面にログインできるのにゃ！");
                    GUILayout.Label("しかも、その時はユーザのポリシーに基づいて、呼び出せるAPIに制限がかかるのにゃ。");
                    GUILayout.Label("");
                    GUILayout.Label("大きい会社だと、読み込みは出来るけど書き込みは出来ないログインユーザが欲しい。とか言われるのにゃ。");
                    GUILayout.Label("一人で全部やってる人には関係ないけど、そういうことなんだにゃ。");
                    GUILayout.Label("");

                    if (GUILayout.Button("はーい"))
                    {
                        _steps = Steps.FirstStep_Deploy1;
                        Repaint();
                    }
                    break;
            }
        }
        
        private void FirstStepDeploy()
        {
            switch (_steps)
            {
                case Steps.FirstStep_Deploy1:
                    GUILayout.Label("次はクレデンシャルの作り方だにゃ。");
                    GUILayout.Label("");
                    GUILayout.Label("管理画面でポチポチ登録することもできるけど、GS2-Deploy って機能を使うともっと便利だにゃ。");
                    GUILayout.Label("GS2-Deploy は GS2 を使う上でめちゃくちゃ便利な機能だから、絶対に覚えるのにゃ。");
                    GUILayout.Label("");
                    GUILayout.Label("さっきのクレデンシャルの説明を思い出してほしいにゃ。");
                    GUILayout.Label("一言で `クレデンシャルを作る` っていっても、実は色々登録が必要だにゃ。");
                    GUILayout.Label("きっと1回やったらしばらくやらないから、みんな忘れるにゃ！");
                    GUILayout.Label("");
                    GUILayout.Label("そこで、 GS2-Deploy だにゃ。");
                    GUILayout.Label("GS2-Deploy は GS2 内の設定を自動化する仕組みだにゃ！");
                    GUILayout.Label("");

                    if (GUILayout.Button("うーん？？？"))
                    {
                        _steps = Steps.FirstStep_Deploy2;
                        Repaint();
                    }
                    break;
                case Steps.FirstStep_Deploy2:
                    GUILayout.Label("つまり、管理画面をポチポチするのは忘れたらやりなおしだけど、");
                    GUILayout.Label("手順書を作っておけば、忘れても大丈夫ってはなしだにゃ。");
                    GUILayout.Label("GS2-Deploy は手順書をそのまま読み込んで設定をしてくれるのにゃ。");
                    GUILayout.Label("");

                    if (GUILayout.Button("ああ。バッチファイルってこと？"))
                    {
                        _steps = Steps.FirstStep_Deploy3;
                        Repaint();
                    }
                    break;
                case Steps.FirstStep_Deploy3:
                    GUILayout.Label("近いけどちょっと違うにゃ。バッチファイルより凄いんだにゃ。");
                    GUILayout.Label("たとえば、手順書を書き換えて、手順が増えたり減ったり、変更した場合、");
                    GUILayout.Label("増えた部分は追加で処理して、減った処理で作られたデータは削除して、変更があった部分は更新するにゃ。");
                    GUILayout.Label("");
                    GUILayout.Label("つまり、「こうあって欲しいにゃあ」と思ってる状態を手順書に書いておくと、");
                    GUILayout.Label("いい感じにその状態にサーバ側の設定を更新してくれるのにゃ。");
                    GUILayout.Label("");

                    if (GUILayout.Button("おお。すごい"))
                    {
                        _steps = Steps.FirstStep_Deploy4;
                        Repaint();
                    }
                    break;
                case Steps.FirstStep_Deploy4:
                    GUILayout.Label("クレデンシャルを作るために必要な手順書の書き方を説明するのにゃ。");
                    GUILayout.Label("ちなみに、GS2の用語では手順書ではなく `テンプレート` と呼んでるにゃ。");
                    GUILayout.Label("");
                    GUILayout.Label("テンプレートに書かないといけない項目は以下だにゃ。");
                    GUILayout.Label("");
                    
                    using (new GUILayout.VerticalScope(GUI.skin.box))
                    {
                        GUILayout.Label("- ユーザの作成");
                        GUILayout.Label("- ユーザにポリシーの割り当て");
                        GUILayout.Label("- クレデンシャルの作成");
                    }
                    
                    GUILayout.Label("");
                    GUILayout.Label("ポリシーは GS2 があらかじめ用意したものをつかうから、作成はいらないにゃ。");
                    GUILayout.Label("");

                    if (GUILayout.Button("なるほど"))
                    {
                        _steps = Steps.FirstStep_Deploy5;
                        Repaint();
                    }
                    break;
                case Steps.FirstStep_Deploy5:
                    GUILayout.Label("これをテンプレートで書くとこうなるのにゃ。");
                    GUILayout.Label("");
                    
                    using (new GUILayout.VerticalScope(GUI.skin.box))
                    {
                        GUILayout.Label("GS2TemplateFormatVersion: \"2019-05-01\"");
                        GUILayout.Label("Description: GS2 SDK identifier template Version 2019-07-10");
                        GUILayout.Label("");
                        GUILayout.Label("Globals:");
                        GUILayout.Label("  Alias:");
                        GUILayout.Label("    ApplicationUserName: application");
                        GUILayout.Label("");
                        GUILayout.Label("Resources:");
                        GUILayout.Label("  IdentifierApplicationUser:");
                        GUILayout.Label("    Type: GS2::Identifier::User");
                        GUILayout.Label("    Properties:");
                        GUILayout.Label("      Name: ${ApplicationUserName}");
                        GUILayout.Label("");
                        GUILayout.Label("  IdentifierApplicationUserAttachPolicy:");
                        GUILayout.Label("    Type: GS2::Identifier::AttachSecurityPolicy");
                        GUILayout.Label("    Properties:");
                        GUILayout.Label("      UserName: ${ApplicationUserName}");
                        GUILayout.Label("      SecurityPolicyId: grn:gs2::system:identifier:securityPolicy:ApplicationAccess");
                        GUILayout.Label("    DependsOn:");
                        GUILayout.Label("      - IdentifierApplicationUser");
                        GUILayout.Label("");
                        GUILayout.Label("  IdentifierApplicationIdentifier:");
                        GUILayout.Label("    Type: GS2::Identifier::Identifier");
                        GUILayout.Label("    Properties:");
                        GUILayout.Label("      UserName: ${ApplicationUserName}");
                        GUILayout.Label("    DependsOn:");
                        GUILayout.Label("      - IdentifierApplicationUser");
                        GUILayout.Label("");
                        GUILayout.Label("Outputs:");
                        GUILayout.Label("  ApplicationClientId: !GetAttr IdentifierApplicationIdentifier.Item.ClientId");
                        GUILayout.Label("  ApplicationClientSecret: !GetAttr IdentifierApplicationIdentifier.ClientSecret");
                    }
                    
                    if (GUILayout.Button("ええっ…(はみ出てる)                                        "))
                    {
                        _steps = Steps.FirstStep_Deploy6;
                        Repaint();
                    }
                    break;
                case Steps.FirstStep_Deploy6:
                    GUILayout.Label("いっきに言っても混乱するだけだから、順番に説明するにゃ。");
                    GUILayout.Label("");
                    
                    using (new GUILayout.VerticalScope(GUI.skin.box))
                    {
                        GUILayout.Label("GS2TemplateFormatVersion: \"2019-05-01\"");
                        GUILayout.Label("Description: GS2 SDK identifier template Version 2019-07-10");
                    }
                    
                    GUILayout.Label("");
                    GUILayout.Label("この部分はテンプレートのバージョンと、テンプレートについてメモ書きだにゃ。");
                    GUILayout.Label("");
                    
                    if (GUILayout.Button("はい"))
                    {
                        _steps = Steps.FirstStep_Deploy7;
                        Repaint();
                    }
                    break;
                case Steps.FirstStep_Deploy7:
                    using (new GUILayout.VerticalScope(GUI.skin.box))
                    {
                        GUILayout.Label("Globals:");
                        GUILayout.Label("  Alias:");
                        GUILayout.Label("    ApplicationUserName: application");
                    }
                    
                    GUILayout.Label("");
                    GUILayout.Label("この部分は、この後の設定内で使う定数を宣言してるのにゃ。");
                    GUILayout.Label("作成するユーザの名前を ApplicationUserName という定数名で application という値に設定してるのにゃ。");
                    GUILayout.Label("");
                    
                    if (GUILayout.Button("わかった"))
                    {
                        _steps = Steps.FirstStep_Deploy8;
                        Repaint();
                    }
                    break;
                case Steps.FirstStep_Deploy8:
                    using (new GUILayout.VerticalScope(GUI.skin.box))
                    {
                        GUILayout.Label("Resources:");
                        GUILayout.Label("  IdentifierApplicationUser:");
                        GUILayout.Label("    Type: GS2::Identifier::User");
                        GUILayout.Label("    Properties:");
                        GUILayout.Label("      Name: ${ApplicationUserName}");
                    }
                    
                    GUILayout.Label("");
                    GUILayout.Label("ここからは実際に作るデータの内容を決めてるのにゃ。");
                    GUILayout.Label("IdentifierApplicationUser は作るデータに自由に名前を付けられるにゃ。テンプレートの中でしか使わないにゃ。");
                    GUILayout.Label("Type: GS2::Identifier::User ここで宣言してるのは、クレデンシャルを管理する GS2-Identifier の ユーザを作るよって宣言にゃ。");
                    GUILayout.Label("Properties からは作るデータのパラメータを設定するにゃ。");
                    GUILayout.Label("Name: ${ApplicationUserName} ってのは、ユーザ名に定数で宣言した値を使う。ってことだにゃ。");
                    GUILayout.Label("");
                    
                    if (GUILayout.Button("なるほど"))
                    {
                        _steps = Steps.FirstStep_Deploy9;
                        Repaint();
                    }
                    break;
                case Steps.FirstStep_Deploy9:
                    using (new GUILayout.VerticalScope(GUI.skin.box))
                    {
                        GUILayout.Label("  IdentifierApplicationUserAttachPolicy:");
                        GUILayout.Label("    Type: GS2::Identifier::AttachSecurityPolicy");
                        GUILayout.Label("    Properties:");
                        GUILayout.Label("      UserName: ${ApplicationUserName}");
                        GUILayout.Label("      SecurityPolicyId: grn:gs2::system:identifier:securityPolicy:ApplicationAccess");
                        GUILayout.Label("    DependsOn:");
                        GUILayout.Label("      - IdentifierApplicationUser");
                    }
                    
                    GUILayout.Label("");
                    GUILayout.Label("これは、作成したユーザにポリシーを設定するところだにゃ。");
                    GUILayout.Label("Type: GS2::Identifier::AttachSecurityPolicy で、ユーザにポリシーを割り当てるよ。ってことになるにゃ。");
                    GUILayout.Label("");
                    GUILayout.Label("UserName: ${ApplicationUserName} は割り当てるユーザ名を定数で指定してるにゃ。");
                    GUILayout.Label("SecurityPolicyId: grn:gs2::system:identifier:securityPolicy:ApplicationAccess");
                    GUILayout.Label("これは GS2 があらかじめ用意したゲーム内に組み込んでも安心なポリシーのIDだにゃ。");
                    GUILayout.Label("");
                    GUILayout.Label("DependsOn っていうさっきは無かった項目があるにゃ。");
                    GUILayout.Label("これは `この項目のデータを作った後で実行してほしい` って宣言だにゃ。");
                    GUILayout.Label("つまり、ユーザを作り終えてからポリシーの割り当て処理をしてほしい。って意味にゃ。");
                    GUILayout.Label("");
                    
                    if (GUILayout.Button("ほうほう"))
                    {
                        _steps = Steps.FirstStep_Deploy10;
                        Repaint();
                    }
                    break;
                case Steps.FirstStep_Deploy10:
                    using (new GUILayout.VerticalScope(GUI.skin.box))
                    {
                        GUILayout.Label("  IdentifierApplicationIdentifier:");
                        GUILayout.Label("    Type: GS2::Identifier::Identifier");
                        GUILayout.Label("    Properties:");
                        GUILayout.Label("      UserName: ${ApplicationUserName}");
                        GUILayout.Label("    DependsOn:");
                        GUILayout.Label("      - IdentifierApplicationUser");
                    }
                    
                    GUILayout.Label("");
                    GUILayout.Label("これは、クレデンシャルを作るって処理だにゃ。");
                    GUILayout.Label("ポリシーの割り当てと同じで、ユーザを作り終わってからやってほしい。って書いてあるにゃ。");
                    GUILayout.Label("");
                    
                    if (GUILayout.Button("わかりました"))
                    {
                        _steps = Steps.FirstStep_Deploy11;
                        Repaint();
                    }
                    break;
                case Steps.FirstStep_Deploy11:
                    using (new GUILayout.VerticalScope(GUI.skin.box))
                    {
                        GUILayout.Label("Outputs:");
                        GUILayout.Label("  ApplicationClientId: !GetAttr IdentifierApplicationIdentifier.Item.ClientId");
                        GUILayout.Label("  ApplicationClientSecret: !GetAttr IdentifierApplicationIdentifier.ClientSecret");
                    }
                    
                    GUILayout.Label("");
                    GUILayout.Label("最後のこれは、作ったデータの情報を保存しておいて。って宣言にゃ。");
                    GUILayout.Label("ApplicationClientId にクレデンシャルの クライアントID を");
                    GUILayout.Label("ApplicationClientSecret にクレデンシャルの クライアントシークレット を書き出して。って意味にゃ。");
                    GUILayout.Label("");
                    
                    if (GUILayout.Button("ながかった…。"))
                    {
                        _steps = Steps.FirstStep_Deploy12;
                        Repaint();
                    }
                    break;
                case Steps.FirstStep_Deploy12:
                    GUILayout.Label("まあ、ざっとこんな感じにゃ。");
                    GUILayout.Label("");
                    GUILayout.Label("じゃ、実際にクレデンシャルを作ってみるにゃ！");
                    GUILayout.Label("と、言いたいところにゃが、別の説明を先にするのにゃ。");
                    GUILayout.Label("");
                    
                    if (GUILayout.Button("ええっ…"))
                    {
                        _steps = Steps.FirstStep_CloudWeave1;
                        Repaint();
                    }
                    break;
            }
        }

        private void FirstStepCloudWeave()
        {
            switch (_steps)
            {
                case Steps.FirstStep_CloudWeave1:
                    GUILayout.Label("GS2-SDK for Unity には CloudWeave（クラウドウィーブ） という機能がついているにゃ。");
                    GUILayout.Label("");
                    GUILayout.Label("CloudWeave は GS2-Deploy のテンプレートリポジトリ… 一言で言えば GS2 版の AssetStore だにゃ。");
                    GUILayout.Label("リポジトリに登録されたテンプレートをボタン一つで自分のアカウントにインストール出来る優れモノだにゃ。");
                    GUILayout.Label("");
                    GUILayout.Label("上部のメニューの Game Server Services/CloudWeave で開けるのにゃ");
                    GUILayout.Label("CloudWeave メニューで 「クレデンシャル」 と検索して、インストールしてみてほしいにゃ。");
                    GUILayout.Label("");
                    GUILayout.Label("インストールをしたらまた続きを説明するのにゃ。");
                    GUILayout.Label("");

                    if (GUILayout.Button("CloudWeave メニューを開く"))
                    {
                        PlayerPrefs.SetString("io.gs2.tutorial.credential", true.ToString());
                        PlayerPrefs.Save();

                        CloudWeaveWindow.Open();
                        Close();
                    }

                    break;
            }
        }
    }
}