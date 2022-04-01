using System;

namespace Gs2.Unity.UiKit.Core
{
    public class ShortUuid
    {
        private readonly string _value;
        
        public ShortUuid(string shortUuid)
        {
            _value = shortUuid.Replace("-", "");
        }

        public override string ToString()
        {
            return _value.Substring(0, 6) + "-" + 
                   _value.Substring(6, 5) + "-" + 
                   _value.Substring(11, 6) + "-" + 
                   _value.Substring(17, 5);
        }

        public static ShortUuid ParseUuid(string uuid)
        {
            return new ShortUuid(Convert.ToBase64String(Guid.Parse(uuid).ToByteArray()).Replace("==", ""));
        }

        public string ToUuid()
        {
            return new Guid(Convert.FromBase64String(this._value + "==")).ToString();
        }
    }
}