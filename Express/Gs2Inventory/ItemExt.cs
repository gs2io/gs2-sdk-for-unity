#if GS2_ENABLE_UNITASK
using System.Linq;
using Cysharp.Threading.Tasks;
using Gs2.Gs2Inventory.Request;
using Gs2.Unity.Gs2Inventory.Model;
using Gs2.Unity.Gs2Inventory.ScriptableObject;
using Gs2.Unity.Util;

namespace Gs2.Unity.Express.Gs2Inventory
{
    public static class ItemExt
    {
        public static async UniTask<EzItemSet> Acquire(
            this Item item,
            int count
        )
        {
            await Gs2ClientHolder.Instance.Gs2.Super.Inventory.Namespace(
                item.inventory.Namespace.namespaceName
            ).User(
                Gs2GameSessionHolder.Instance.GameSession.AccessToken.UserId
            ).Inventory(
                item.inventory.inventoryName
            ).ItemSet(
                item.itemName,
                null
            ).AcquireAsync(
                new AcquireItemSetByUserIdRequest()
                    .WithAcquireCount(count)
            );

            return (await Gs2ClientHolder.Instance.Gs2.Inventory.Namespace(
                item.inventory.Namespace.namespaceName
            ).User(
                Gs2GameSessionHolder.Instance.GameSession.AccessToken.UserId
            ).Inventory(
                item.inventory.inventoryName
            ).ItemSet(
                item.itemName,
                null
            ).ModelAsync()).First();
        }
        
        public static async UniTask<EzItemSet[]> Get(
            this Item item,
            string itemSetName = null
        )
        {
            return await Gs2ClientHolder.Instance.Gs2.Inventory.Namespace(
                item.inventory.Namespace.namespaceName
            ).Me(
                Gs2GameSessionHolder.Instance.GameSession
            ).Inventory(
                item.inventory.inventoryName
            ).ItemSet(
                item.itemName,
                itemSetName
            ).ModelAsync();
        }
    }
}
#endif