[日本語](README.md) | [English](README-en.md) | **한국어**

# GS2 SDK for Unity

Game Server Services(https://gs2.io)를 Unity에서 사용하기 위한 SDK입니다.
GS2 SDK for Unity는 GS2-CSharp-SDK(C# 공통 모듈)와 Unity 전용 모듈로 구성되어 있습니다.

## Game Server Services란?

Game Server Services(GS2)는 게임 개발에 특화된 서비스형 백엔드(BaaS)입니다.

GS2는 게임 개발의 효율을 높이기 위해 만들어진 범용 게임 서버 솔루션으로, Games as a Service(GaaS)와 Live Gaming 등을 지원합니다.

플레이어 데이터를 유연하게 관리하고 분석할 수 있습니다. 게임 내 자원이 어떻게 유통되고 소비되는지 분석해 게임 경제의 균형을 유지할 수 있습니다.
또한 스토리 진행 관리와 소지품 관리 등의 기능을 제공하여 게임의 수익화와 플레이어 참여도 향상에 기여합니다.
GS2는 온라인 기능을 지원하고, 게임 개발자가 데이터 분석과 경제 관리를 손쉽게 할 수 있도록 하여 게임의 성공을 돕습니다.

## 시작하기

다운로드한 소스 코드를 Unity 프로젝트 내의 다음 폴더에 배치해 주세요.

`(Unity 프로젝트)/Assets/Scripts/Runtime/Sdk/Gs2/Unity`

SDK를 사용하려면 GS2 크리덴셜(클라이언트 ID와 클라이언트 시크릿)이 필요합니다.
[GS2 설정](https://docs.gs2.io/ko/get_start/tutorial/setup_gs2/) 절차에 따라 크리덴셜을 발급해 주세요.

### 동작 환경

- Unity 2020+
- GS2-CSharp-SDK

[⇒GS2 이용 시작 - SDK - Game Engine](https://docs.gs2.io/ko/get_start/#game-engine)

## SDK 상세 사양

서비스별·통신 방식별 API는 [API 레퍼런스](https://docs.gs2.io/ko/api_reference/)를, SDK 초기화 방법은 [초기화 처리](https://docs.gs2.io/ko/api_reference/initialize/) 문서를 참고해 주세요.

*이 프로젝트의 코드는 Core를 제외하고 모두 자동 생성되므로, 개별 풀 리퀘스트는 수용할 수 없습니다.*
