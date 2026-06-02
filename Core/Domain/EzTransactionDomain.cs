using System.Collections;
using Gs2.Core.Domain;
using Gs2.Core.Net;

#if UNITY_2017_1_OR_NEWER && GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
#elif !UNITY_2017_1_OR_NEWER
using System.Threading.Tasks;
#endif

namespace Gs2.Unity.Core.Domain
{
    public class EzTransactionDomain
    {
        private readonly TransactionAccessTokenDomain _domain;
        public string TransactionId => _domain?.GetTransactionId();
        public string JobName => _domain?.GetJobName();
        
        public EzTransactionDomain(
            TransactionAccessTokenDomain domain
        ) {
            this._domain = domain;
        }
        
#if UNITY_2017_1_OR_NEWER
        public IFuture<EzTransactionDomain> WaitFuture(bool all = false) {
            IEnumerator Impl(IFuture<EzTransactionDomain> self) {
                var future = this._domain.WaitFuture(all);
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(future.Result == null ? null : new EzTransactionDomain(future.Result));
            }
            return new Gs2InlineFuture<EzTransactionDomain>(Impl);
        }
#endif

#if !UNITY_2017_1_OR_NEWER || GS2_ENABLE_UNITASK
    #if UNITY_2017_1_OR_NEWER
        public async UniTask<EzTransactionDomain> WaitAsync(bool all = false) {
    #else
        public async Task<EzTransactionDomain> WaitAsync(bool all = false) {
    #endif
            var next = await this._domain.WaitAsync(all);
            return next == null ? null : new EzTransactionDomain(next);
        }
#endif
    }
}
