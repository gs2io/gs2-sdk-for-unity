using System;
using Gs2.Core.Exception;
using Gs2.Core.Model;

namespace Gs2.Unity.Core.Exception
{
    public class CanIgnoreException : Gs2Exception
    {
        public Gs2Exception Original { get; private set;  }
        
        public CanIgnoreException(Gs2Exception original) : base(Array.Empty<RequestError>())
        {
            Original = original;
        }

        public override int StatusCode => Original.StatusCode;
    }
}