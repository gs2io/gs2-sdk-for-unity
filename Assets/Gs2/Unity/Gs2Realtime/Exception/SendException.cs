using System;
using Gs2.Core.Exception;
using Gs2.Gs2Realtime.Message;

namespace Gs2.Unity.Gs2Realtime.Exception
{
    public class SendException : Gs2Exception
    {
        public BinaryMessage SendFailedMessage;
        
        public SendException(BinaryMessage sendFailedMessage) : base("SendException")
        {
            SendFailedMessage = sendFailedMessage;
        }
    }
}