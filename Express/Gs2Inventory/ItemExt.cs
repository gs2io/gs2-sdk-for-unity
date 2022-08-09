#if GS2_ENABLE_UNITASK
using System.Linq;
using Cysharp.Threading.Tasks;
using Gs2.Gs2Inventory.Request;
using Gs2.Unity.Core;
using Gs2.Unity.Gs2Inventory.Model;
using Gs2.Unity.Gs2Inventory.ScriptableObject;
using Gs2.Unity.Util;

namespace Gs2.Unity.Express.Gs2Inventory
{
    public static class ItemExt
    {
        public static async UniTask<EzItemSet> Acquire(
            this Item item,
            int count,
            Gs2Domain gs2 = null
        )
        {
            await (gs2 ?? Gs2ClientHolder.Instance.Gs2).Super.Inventory.Namespace(
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

            return (await (gs2 ?? Gs2ClientHolder.Instance.Gs2).Inventory.Namespace(
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
            string itemSetName = null,
            Gs2Domain gs2 = null
        )
        {
            return await (gs2 ?? Gs2ClientHolder.Instance.Gs2).Inventory.Namespace(
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