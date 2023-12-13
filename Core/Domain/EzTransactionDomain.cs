using System.Collections;
using Gs2.Core.Domain;
using Gs2.Core.Net;

#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
#endif

namespace Gs2.Unity.Core.Domain
{
    public class EzTransactionDomain
    {
        private readonly TransactionAccessTokenDomain _domain;
        
        public EzTransactionDomain(
            TransactionAccessTokenDomain domain
        ) {
            this._domain = domain;
        }
        
        public IFuture<EzTransactionDomain> WaitFuture(bool all = false) {
            IEnumerator Impl(IFuture<EzTransactionDomain> self) {
                var future = this._domain.WaitFuture(all);
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(new EzTransactionDomain(future.Result));
            }
            return new Gs2InlineFuture<EzTransactionDomain>(Impl);
        }
        
#if GS2_ENABLE_UNITASK
        public async UniTask<EzTransactionDomain> WaitAsync(bool all = false) {
            return new EzTransactionDomain(await this._domain.WaitAsync(all));
        }
#endif
    }
}
