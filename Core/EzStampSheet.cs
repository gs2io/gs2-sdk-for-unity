using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Gs2.Core.Model;
using Gs2.Unity.Gs2Distributor.Result;
using Gs2.Util.LitJson;

namespace Gs2.Unity.Core
{
    public class EzStampSheet
    {
        private string _stampSheet;
        private string _keyId;

        private EzStampTask[] _tasks;
        
        public EzStampSheet(
            string stampSheet,
            string keyId
        )
        {
            _stampSheet = stampSheet;
            _keyId = keyId;
            
            var signedStampSheet = JsonMapper.ToObject<SignedStampSheet>(stampSheet);
            var stampSheetModel = JsonMapper.ToObject<StampSheet>(signedStampSheet.body);
            _tasks = stampSheetModel.tasks.Select(
                task => new EzStampTask(task, keyId)
            ).ToArray();
        }

        public async UniTask Run(
            Gs2Domain domain
        )
        {
            string contextStack = null;
            foreach (var task in _tasks)
            {
                var result = await task.Run(
                    domain,
                    contextStack
                );
                contextStack = result.ContextStack;
            }
            await domain.Distributor.RunStampSheetWithoutNamespace(
                _stampSheet,
                _keyId,
                contextStack
            );
        }
    }
}