using System;
using Gs2.Core.Exception;

namespace Gs2.Unity.Gs2Realtime.Exception
{
    public class ConnectionException : Gs2Exception
    {
        public EventArgs Args;
        
        public ConnectionException(EventArgs args) : base("ConnectionException")
        {
            Args = args;
        }
    }
}