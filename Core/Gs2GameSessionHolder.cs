using System;
using Gs2.Unity.Gs2Auth.Model;
using Gs2.Unity.Util;
using UnityEngine;

namespace Gs2.Unity.UiKit.Core
{
    [AddComponentMenu("GS2 UIKit/Core/Gs2GameSessionHolder")]
    public class Gs2GameSessionHolder : MonoBehaviour
    {
        public GameSession GameSession { get; private set; }
        public bool Initialized => GameSession != null;

        public static Gs2GameSessionHolder Instance
        {
            get
            {
                var accessTokenHolders = GameObject.FindObjectsOfType<Gs2GameSessionHolder>();
                if (accessTokenHolders.Length != 1)
                {
                    Debug.LogError("Either no Gs2GameSessionHolder was found in the scene or more than one was found.");
                    Debug.LogError("シーン内に Gs2GameSessionHolder が見つからなかったか、2つ以上見つかりました。");
                    throw new ApplicationException("Either no Gs2GameSessionHolder was found in the scene or more than one was found.");
                }

                return accessTokenHolders[0];
            }
        }

        public void UpdateAccessToken(EzAccessToken accessToken)
        {
            GameSession = new GameSession(accessToken.ToModel());
        }

        public void Logout()
        {
            GameSession = null;
        }
    }
}