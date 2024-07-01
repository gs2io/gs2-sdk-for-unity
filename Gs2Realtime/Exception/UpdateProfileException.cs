using System;
using Google.Protobuf;
using Gs2.Core.Exception;
using Gs2.Gs2Realtime.Message;
using Gs2.Unity.Util;
using UnityEngine.Scripting;

namespace Gs2.Unity.Gs2Realtime.Exception
{
    [Preserve]
    public class UpdateProfileException : Gs2Exception
    {
        public ByteString Profile;
        
        public UpdateProfileException(ByteString profile) : base("UpdateProfileException")
        {
            Profile = profile;
        }

        public override int StatusCode => 500;
    }
}