using System.Collections;
using Gs2.Core;
using Gs2.Gs2Auth.Model;
using UnityEngine.Events;

namespace Gs2.Unity.Util
{
    public abstract class IAuthenticator
    {
        public UnityAction<AsyncResult<AccessToken>> Callback { get; set; }

        public abstract IEnumerator Authentication(UnityAction<AsyncResult<AccessToken>> callback);
    }
}