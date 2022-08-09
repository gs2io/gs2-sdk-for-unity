#if GS2_ENABLE_UNITASK
using System.Linq;
using Cysharp.Threading.Tasks;
using Gs2.Unity.Core;
using Gs2.Unity.Gs2Inventory.Model;
using Gs2.Unity.Gs2Inventory.ScriptableObject;
using Gs2.Unity.Util;

namespace Gs2.Unity.Express.Gs2Inventory
{
    public static class ItemSetExt
    {
        public static async UniTask<EzItemSet> Get(
            this ItemSet itemSet,
            Gs2Domain gs2 = null
        )
        {
            return (await (gs2 ?? Gs2ClientHolder.Instance.Gs2).Inventory.Namespace(
                itemSet.item.inventory.Namespace.namespaceName
            ).Me(
                Gs2GameSessionHolder.Instance.GameSession
            ).Inventory(
                itemSet.item.inventory.inventoryName
            ).ItemSet(
                itemSet.item.itemName,
                itemSet.itemSetName
            ).ModelAsync()).FirstOrDefault();
        }

    }
}
#endif