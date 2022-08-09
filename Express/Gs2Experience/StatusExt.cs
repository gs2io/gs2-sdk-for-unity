#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
using Gs2.Unity.Core;
using Gs2.Unity.Gs2Experience.Model;
using Gs2.Unity.Util;
using Status = Gs2.Unity.Gs2Experience.ScriptableObject.Status;

namespace Gs2.Unity.Express.Gs2Experience
{
    public static class StatusExt
    {
        public static async UniTask<EzStatus> Get(
            this Status status,
            Gs2Domain gs2 = null
        )
        {
            return await (gs2 ?? Gs2ClientHolder.Instance.Gs2).Experience.Namespace(
                status.experience.Namespace.namespaceName
            ).Me(
                Gs2GameSessionHolder.Instance.GameSession
            ).Status(
                status.experience.experienceName,
                status.propertyId
            ).ModelAsync();
        }
    }
}
#endif