#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Gs2.Unity.Core;
using Gs2.Unity.Gs2Experience.Model;
using Gs2.Unity.Gs2Experience.ScriptableObject;
using Gs2.Unity.Util;

namespace Gs2.Unity.Express.Gs2Experience
{
    public static class ExperienceExt
    {
        public static async UniTask<EzStatus> GetStatus(
            this Experience experience,
            string propertyId,
            Gs2Domain gs2 = null
        )
        {
            return await (gs2 ?? Gs2ClientHolder.Instance.Gs2).Experience.Namespace(
                experience.Namespace.namespaceName
            ).Me(
                Gs2GameSessionHolder.Instance.GameSession
            ).Status(
                experience.experienceName,
                propertyId
            ).ModelAsync();
        }
        
        public static async UniTask<EzStatus[]> ListStatuses(
            this Experience experience,
            Gs2Domain gs2 = null
        )
        {
            return (await (gs2 ?? Gs2ClientHolder.Instance.Gs2).Experience.Namespace(
                experience.Namespace.namespaceName
            ).Me(
                Gs2GameSessionHolder.Instance.GameSession
            ).StatusesAsync(
                experience.experienceName
            ).ToListAsync()).ToArray();
        }
    }
}
#endif