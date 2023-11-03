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
        
        public bool AutoRunStampSheet => this._domain.AutoRunStampSheet;
        public string TransactionId => this._domain.TransactionId;
        public string StampSheet => this._domain.StampSheet;
        public string StampSheetEncryptionKey => this._domain.StampSheetEncryptionKey;

        public EzTransactionDomain(
            TransactionAccessTokenDomain domain
        ) {
            this._domain = domain;
        }
        
        public IFuture<EzTransactionDomain> WaitFuture() {
            IEnumerator Impl(IFuture<EzTransactionDomain> self) {
                var future = this._domain.WaitFuture();
                yield return future;
                if (future.Error != null) {
                    self.OnError(future.Error);
                    yield break;
                }
                self.OnComplete(this);
            }
            return new Gs2InlineFuture<EzTransactionDomain>(Impl);
        }
        
#if GS2_ENABLE_UNITASK
        public async UniTask<EzTransactionDomain> WaitAsync() {
            await this._domain.WaitAsync();
            return this;
        }
#endif
    }
}
