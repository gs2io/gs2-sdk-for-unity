#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
using Gs2.Unity.Gs2Formation.Model;
using Gs2.Unity.Gs2Formation.ScriptableObject;
using Gs2.Unity.Util;

namespace Gs2.Unity.Express.Gs2Formation
{
    public static class PropertyFormExt
    {
        public static async UniTask<EzPropertyForm> Get(
            this PropertyForm propertyForm,
            string propertyId
        )
        {
            return await Gs2ClientHolder.Instance.Gs2.Formation.Namespace(
                propertyForm.Namespace.namespaceName
            ).Me(
                Gs2GameSessionHolder.Instance.GameSession
            ).PropertyForm(
                propertyForm.formName,
                propertyId
            ).ModelAsync();
        }

    }
}
#endif