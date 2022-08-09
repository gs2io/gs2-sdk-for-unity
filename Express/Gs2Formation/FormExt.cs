#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
using Gs2.Unity.Core;
using Gs2.Unity.Gs2Formation.Model;
using Gs2.Unity.Gs2Formation.ScriptableObject;
using Gs2.Unity.Util;

namespace Gs2.Unity.Express.Gs2Formation
{
    public static class FormExt
    {
        public static async UniTask<EzForm> Get(
            this Form form,
            Gs2Domain gs2 = null
        )
        {
            return await (gs2 ?? Gs2ClientHolder.Instance.Gs2).Formation.Namespace(
                form.mold.Namespace.namespaceName
            ).Me(
                Gs2GameSessionHolder.Instance.GameSession
            ).Mold(
                form.mold.moldName
            ).Form(
                form.index
            ).ModelAsync();
        }
    }
}
#endif