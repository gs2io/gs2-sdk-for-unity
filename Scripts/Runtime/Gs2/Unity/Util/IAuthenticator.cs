using System.Collections;
using Gs2.Core;
using Gs2.Gs2Auth.Model;
using UnityEngine.Events;

namespace Gs2.Unity.Util
{
    public interface IAuthenticator
    {
        IEnumerator Authentication(UnityAction<AsyncResult<AccessToken>> callback);
    }
}