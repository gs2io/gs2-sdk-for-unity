#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
#else
using System.Collections;
#endif
using Gs2.Unity.Gs2Auth.Model;
using Gs2.Unity.Util;
using UnityEngine;

namespace Gs2.Unity.Util
{
    [AddComponentMenu("GS2 UIKit/Core/Gs2GameSessionHolder")]
    public class Gs2GameSessionHolder : MonoBehaviour
    {
        public GameSession GameSession { get; private set; }
        public bool Initialized => GameSession != null;

        public void Awake()
        {
            var items = FindObjectsOfType<Gs2GameSessionHolder>();
            foreach (var item in items)
            {
                if (item != this)
                {
                    DestroyImmediate(this.gameObject);
                }
            }

#if GS2_ENABLE_UNITASK
            async UniTask Impl()
            {
                await UniTask.WaitUntil(() => Initialized);

                while (true)
                {
                    await Gs2ClientHolder.Instance.Gs2.DispatchAsync(GameSession);

                    await UniTask.Yield();
                }
            }

            StartCoroutine(Impl().ToCoroutine());
#else
            IEnumerator Impl()
            {
                while (!Initialized)
                {
                    yield return null;
                }

                while (true)
                {
                    var future = Gs2ClientHolder.Instance.Gs2.Dispatch(GameSession);
                    yield return future;
                    if (future != null)
                    {
                        yield break;
                    }
                    if (future.Result)
                    {
                        break;
                    }
                    yield return null;
                }
            }
            StartCoroutine(Impl());
#endif
        }

        protected internal static Gs2GameSessionHolder _instance;

        public static Gs2GameSessionHolder Instance
        {
            get
            {
                if (_instance == null)
                {
                    var accessTokenHolders = GameObject.FindObjectsOfType<Gs2GameSessionHolder>();
                    if (accessTokenHolders.Length > 0)
                    {
                        return accessTokenHolders[0];
                    }

                    _instance = new GameObject("Gs2GameSessionHolder").AddComponent<Gs2GameSessionHolder>();
                }
                return _instance;
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