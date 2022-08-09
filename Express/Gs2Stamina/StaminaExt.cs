#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
using Gs2.Unity.Core;
using Gs2.Unity.Gs2Stamina.Model;
using Gs2.Unity.Gs2Stamina.ScriptableObject;
using Gs2.Unity.Util;

namespace Express.Gs2Stamina
{
    public static class StaminaExt
    {
        public static async UniTask<EzStamina> Get(
            this Stamina stamina,
            Gs2Domain gs2 = null
        )
        {
            return await (gs2 ?? Gs2ClientHolder.Instance.Gs2).Stamina.Namespace(
                stamina.Namespace.namespaceName
            ).Me(
                Gs2GameSessionHolder.Instance.GameSession
            ).Stamina(
                stamina.staminaName
            ).ModelAsync();
        }
    }
}
#endif