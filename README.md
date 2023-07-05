[⇒README in English](README-en.md)

# GS2 SDK for Unity

Game Server Services(https://gs2.io) を Unity で利用するためのSDKです。
GS2 SDK for Unity は GS2-CSharp-SDK（C#環境向け）とGS2 SDK for Unity（Unity環境向け）で構成されています。

## Game Server Services とは

Game Server Services(GS2) とはゲーム開発に特化したバックエンドサーバサービス(BaaS)です。

GS2は、ゲーム開発者の効率化を目指して生まれた汎用ゲームサーバーのソリューションであり、Games as a Service(GaaS) や Live Gaming などをサポートしています。

このサービスでは、プレイヤーデータの柔軟な管理やデータ分析が可能であり、ゲーム内の資源の流通や消費量を適切に分析して健全な環境を維持することができます。
さらに、ストーリー進行管理や所持品管理などの機能を提供し、ゲームの収益化やプレイヤーエンゲージメントの向上に貢献します。
GS2は、オンライン機能をサポートし、ゲーム開発者がデータの分析や経済管理を容易に行えるようにすることで、ゲームの成功を支援します。

## Getting Started

ダウンロードしたソースコードは、Unityプロジェクト内の以下のフォルダに配置してください。

`(Unityプロジェクト)/Assets/Scripts/Runtime/Sdk/Gs2/Unity`

SDKを利用するには GS2 のクレデンシャルが必要です。
[GS2のセットアップ](https://docs.gs2.io/ja/get_start/tutorial/setup_gs2/) の手順に従ってクレデンシャルを発行してください。

### 動作条件

- Unity 2020+

- GS2-CSharp-SDK

[⇒GS2の利用を開始 - SDK - Game Engine](https://docs.gs2.io/ja/get_start/#game-engine)

## SDK の詳細仕様

各種サービス・通信方式のAPIの詳細については

 [⇒API リファレンス](https://docs.gs2.io/ja/api_reference/)

初期化処理については

 [⇒API リファレンス - 初期化処理](https://docs.gs2.io/ja/api_reference/initialize/)
 
をご参照ください。

*本プロジェクトのコードは Core 以外は全て自動生成されているため、個別に Pull-Request を頂いても対応できません。*
