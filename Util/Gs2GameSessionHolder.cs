#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
using Gs2.Core.Exception;
#else
using System.Collections;
#endif
using Gs2.Unity.Gs2Auth.Model;
using Gs2.Unity.Util;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.Util
{
    [AddComponentMenu("GS2/Core/Gs2GameSessionHolder")]
    public class Gs2GameSessionHolder : MonoBehaviour
    {
        public GameSession GameSession { get; private set; }

        public UnityEvent OnLogin = new UnityEvent();
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

                var retryWaitSecond = 1;
                while (true)
                {
                    try {
                        await Gs2ClientHolder.Instance.Gs2.DispatchAsync(GameSession);

                        await UniTask.Yield();

                        retryWaitSecond = 1;
                    } catch (Gs2Exception e) {
                        Debug.LogError(e.Message);
                        await UniTask.Delay(retryWaitSecond);
                        retryWaitSecond *= 2;
                    }
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

                var retryWaitSecond = 1;
                while (true)
                {
                    var future = Gs2ClientHolder.Instance.Gs2.Dispatch(GameSession);
                    yield return future;
                    if (future.Error != null) {
                        Debug.LogError(future.Error.Message);
                        yield return new WaitForSeconds(retryWaitSecond);
                        retryWaitSecond *= 2;
                        continue;
                    }
                    retryWaitSecond = 1;
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

        public void UpdateAccessToken(GameSession gameSession)
        {
            GameSession = gameSession;
            this.OnLogin.Invoke();
        }

        public void Logout()
        {
            GameSession = null;
        }
    }
}