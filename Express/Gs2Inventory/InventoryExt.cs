#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Gs2.Unity.Gs2Inventory.Model;
using Gs2.Unity.Gs2Inventory.ScriptableObject;
using Gs2.Unity.Util;

namespace Gs2.Unity.Express.Gs2Inventory
{
    public static class InventoryExt
    {
        public static async UniTask<EzInventory> Get(
            this Inventory inventory
        )
        {
            return await Gs2ClientHolder.Instance.Gs2.Inventory.Namespace(
                inventory.Namespace.namespaceName
            ).Me(
                Gs2GameSessionHolder.Instance.GameSession
            ).Inventory(
                inventory.inventoryName
            ).ModelAsync();
        }
        
        public static async UniTask<EzItemSet[]> LoadItemSets(
            this Inventory inventory
        )
        {
            return (await Gs2ClientHolder.Instance.Gs2.Inventory.Namespace(
                inventory.Namespace.namespaceName
            ).Me(
                Gs2GameSessionHolder.Instance.GameSession
            ).Inventory(
                inventory.inventoryName
            ).ItemSetsAsync(
            ).ToListAsync()).ToArray();
        }
    }
}
#endif