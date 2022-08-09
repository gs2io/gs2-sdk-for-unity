#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
using Gs2.Unity.Core;
using Gs2.Unity.Gs2Limit.Model;
using Gs2.Unity.Gs2Limit.ScriptableObject;
using Gs2.Unity.Util;

namespace Gs2.Unity.Express.Gs2Limit
{
    public static class CounterExt
    {
        public static async UniTask<EzCounter> Get(
            this Counter counter,
            Gs2Domain gs2 = null
        )
        {
            return await (gs2 ?? Gs2ClientHolder.Instance.Gs2).Limit.Namespace(
                counter.limit.Namespace.namespaceName
            ).Me(
                Gs2GameSessionHolder.Instance.GameSession
            ).Counter(
                counter.limit.limitName,
                counter.counterName
            ).ModelAsync();
        }
    }
}
#endif