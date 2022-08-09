#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
using Gs2.Gs2Mission.Request;
using Gs2.Unity.Core;
using Gs2.Unity.Gs2Mission.Model;
using Gs2.Unity.Gs2Mission.ScriptableObject;
using Gs2.Unity.Util;

namespace Gs2.Unity.Express.Gs2Mission
{
    public static class MissionCounterExt
    {
        public static async UniTask<EzCounter> Increase(
            this MissionCounter counter,
            int count,
            Gs2Domain gs2 = null
        )
        {
            await (gs2 ?? Gs2ClientHolder.Instance.Gs2).Super.Mission.Namespace(
                counter.Namespace.namespaceName
            ).User(
                Gs2GameSessionHolder.Instance.GameSession.AccessToken.UserId
            ).Counter(
                counter.missionCounterName
            ).IncreaseAsync(
                new IncreaseCounterByUserIdRequest()
                    .WithValue(count)
            );

            return await (gs2 ?? Gs2ClientHolder.Instance.Gs2).Mission.Namespace(
                counter.Namespace.namespaceName
            ).User(
                Gs2GameSessionHolder.Instance.GameSession.AccessToken.UserId
            ).Counter(
                counter.missionCounterName
            ).ModelAsync();
        }
    }
}
#endif