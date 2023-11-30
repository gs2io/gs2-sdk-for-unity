using System;
using System.Collections;
using Gs2.Core.Exception;
using UnityEngine.Events;

namespace Gs2.Unity.Util
{
    public class ErrorEvent : UnityEvent<Gs2Exception, Func<IEnumerator>>
    {
        
    }
}
