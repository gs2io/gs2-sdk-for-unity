using System;
using Google.Protobuf;
using Gs2.Core.Exception;
using Gs2.Gs2Realtime.Message;
using Gs2.Unity.Util;

namespace Gs2.Unity.Gs2Realtime.Exception
{
    public class UpdateProfileException : Gs2Exception
    {
        public ByteString Profile;
        
        public UpdateProfileException(ByteString profile) : base("UpdateProfileException")
        {
            Profile = profile;
        }
    }
}