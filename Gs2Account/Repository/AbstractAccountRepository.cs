using Gs2.Unity.Gs2Account.Model;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Account
{
    public abstract class AbstractAccountRepository : MonoBehaviour
    {
        public abstract EzAccount Load();
        public abstract void Save(EzAccount account);
        public abstract void Delete();
    }
}