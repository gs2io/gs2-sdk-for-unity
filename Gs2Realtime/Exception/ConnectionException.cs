using System;
using Gs2.Core.Exception;
using UnityEngine.Scripting;

namespace Gs2.Unity.Gs2Realtime.Exception
{
    [Preserve]
    public class ConnectionException : Gs2Exception
    {
        public EventArgs Args;
        
        public ConnectionException(EventArgs args) : base("ConnectionException")
        {
            Args = args;
        }

        public override int StatusCode => 0;
    }
}