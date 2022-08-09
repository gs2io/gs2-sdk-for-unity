#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
using Gs2.Gs2Money.Request;
using Gs2.Unity.Core;
using Gs2.Unity.Gs2Money.Model;
using Gs2.Unity.Gs2Money.ScriptableObject;
using Gs2.Unity.Util;

namespace Gs2.Unity.Express.Gs2Money
{
    public static class WalletExt
    {
        public static async UniTask<EzWallet> DepositPaid(
            this Wallet wallet,
            int count,
            Gs2Domain gs2 = null
        )
        {
            await (gs2 ?? Gs2ClientHolder.Instance.Gs2).Super.Money.Namespace(
                wallet.Namespace.namespaceName
            ).User(
                Gs2GameSessionHolder.Instance.GameSession.AccessToken.UserId
            ).Wallet(
                wallet.slot
            ).DepositAsync(
                new DepositByUserIdRequest()
                    .WithPrice(1)
                    .WithCount(count)
            );

            return await (gs2 ?? Gs2ClientHolder.Instance.Gs2).Money.Namespace(
                wallet.Namespace.namespaceName
            ).User(
                Gs2GameSessionHolder.Instance.GameSession.AccessToken.UserId
            ).Wallet(
                wallet.slot
            ).ModelAsync();
        }
        
        public static async UniTask<EzWallet> Withdraw(
            this Wallet wallet,
            int count,
            Gs2Domain gs2 = null
        )
        {
            await (gs2 ?? Gs2ClientHolder.Instance.Gs2).Money.Namespace(
                wallet.Namespace.namespaceName
            ).Me(
                Gs2GameSessionHolder.Instance.GameSession
            ).Wallet(
                wallet.slot
            ).WithdrawAsync(
                count,
                false
            );

            return await (gs2 ?? Gs2ClientHolder.Instance.Gs2).Money.Namespace(
                wallet.Namespace.namespaceName
            ).User(
                Gs2GameSessionHolder.Instance.GameSession.AccessToken.UserId
            ).Wallet(
                wallet.slot
            ).ModelAsync();
        }

        public static async UniTask<EzWallet> WithdrawPaid(
            this Wallet wallet,
            int count,
            Gs2Domain gs2 = null
        )
        {
            await (gs2 ?? Gs2ClientHolder.Instance.Gs2).Money.Namespace(
                wallet.Namespace.namespaceName
            ).Me(
                Gs2GameSessionHolder.Instance.GameSession
            ).Wallet(
                wallet.slot
            ).WithdrawAsync(
                count,
                true
            );

            return await (gs2 ?? Gs2ClientHolder.Instance.Gs2).Money.Namespace(
                wallet.Namespace.namespaceName
            ).User(
                Gs2GameSessionHolder.Instance.GameSession.AccessToken.UserId
            ).Wallet(
                wallet.slot
            ).ModelAsync();
        }
        
        public static async UniTask<EzWallet> DepositFree(
            this Wallet wallet,
            int count,
            Gs2Domain gs2 = null
        )
        {
            await (gs2 ?? Gs2ClientHolder.Instance.Gs2).Super.Money.Namespace(
                wallet.Namespace.namespaceName
            ).User(
                Gs2GameSessionHolder.Instance.GameSession.AccessToken.UserId
            ).Wallet(
                wallet.slot
            ).DepositAsync(
                new DepositByUserIdRequest()
                    .WithPrice(0)
                    .WithCount(count)
            );

            return await (gs2 ?? Gs2ClientHolder.Instance.Gs2).Money.Namespace(
                wallet.Namespace.namespaceName
            ).User(
                Gs2GameSessionHolder.Instance.GameSession.AccessToken.UserId
            ).Wallet(
                wallet.slot
            ).ModelAsync();
        }

        public static async UniTask<EzWallet> Get(
            this Wallet wallet,
            Gs2Domain gs2 = null
        )
        {
            return await (gs2 ?? Gs2ClientHolder.Instance.Gs2).Money.Namespace(
                wallet.Namespace.namespaceName
            ).Me(
                Gs2GameSessionHolder.Instance.GameSession
            ).Wallet(
                wallet.slot
            ).ModelAsync();
        }
    }
}
#endif