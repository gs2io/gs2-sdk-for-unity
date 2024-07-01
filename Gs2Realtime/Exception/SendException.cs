using System;
using Gs2.Core.Exception;
using Gs2.Gs2Realtime.Message;
using UnityEngine.Scripting;

namespace Gs2.Unity.Gs2Realtime.Exception
{
	[Preserve]
    public class SendException : Gs2Exception
    {
        public BinaryMessage SendFailedMessage;
        
        public SendException(BinaryMessage sendFailedMessage) : base("SendException")
        {
            SendFailedMessage = sendFailedMessage;
        }

        public override int StatusCode => 500;
    }
}