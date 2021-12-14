using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Gs2.Gs2Distributor.Request;
using Gs2.Unity.Gs2Distributor.Result;

namespace Gs2.Unity.Core
{
    public class EzStampTask
    {
        private string _stampTask;
        private string _keyId;
        
        public EzStampTask(
            string stampTask,
            string keyId
        )
        {
            _stampTask = stampTask;
            _keyId = keyId;
        }

        public async UniTask<EzRunStampTaskWithoutNamespaceResult> Run(
            Gs2Domain domain,
            string contextStack
        )
        {
            return await domain.Distributor.RunStampTaskWithoutNamespace(
                _stampTask,
                _keyId,
                contextStack
            );
        }
    }
}