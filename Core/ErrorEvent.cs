using System;
using System.Collections;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Core
{
    [Serializable]
    public class ErrorEvent : UnityEvent<Exception, Func<IEnumerator>>
    {
        
    }
}