#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
using Gs2.Unity.Gs2Limit.Model;
using Gs2.Unity.Gs2Limit.ScriptableObject;
using Gs2.Unity.Util;

namespace Gs2.Unity.Express.Gs2Limit
{
    public static class LimitExt
    {
        public static async UniTask<EzCounter> GetCounter(
            this Limit limit,
            string counterName
        )
        {
            return await Gs2ClientHolder.Instance.Gs2.Limit.Namespace(
                limit.Namespace.namespaceName
            ).Me(
                Gs2GameSessionHolder.Instance.GameSession
            ).Counter(
                limit.limitName,
                counterName
            ).ModelAsync();
        }
    }
}
#endif