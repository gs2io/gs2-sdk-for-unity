#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
using Gs2.Unity.Gs2Formation.Model;
using Gs2.Unity.Gs2Formation.ScriptableObject;
using Gs2.Unity.Util;

namespace Gs2.Unity.Express.Gs2Formation
{
    public static class MoldExt
    {
        public static async UniTask<EzMold> Get(
            this Mold mold
        )
        {
            return await Gs2ClientHolder.Instance.Gs2.Formation.Namespace(
                mold.Namespace.namespaceName
            ).Me(
                Gs2GameSessionHolder.Instance.GameSession
            ).Mold(
                mold.moldName
            ).ModelAsync();
        }

        public static async UniTask<EzForm> GetForm(
            this Mold mold,
            int index
        )
        {
            return await Gs2ClientHolder.Instance.Gs2.Formation.Namespace(
                mold.Namespace.namespaceName
            ).Me(
                Gs2GameSessionHolder.Instance.GameSession
            ).Mold(
                mold.moldName
            ).Form(
                index
            ).ModelAsync();
        }
    }
}
#endif