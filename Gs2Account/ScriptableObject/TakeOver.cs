using UnityEngine;

namespace Gs2.Unity.Gs2Account.ScriptableObject
{
    [CreateAssetMenu(fileName = "TakeOver", menuName = "Game Server Services/Gs2Account/TakeOver")]
    public class TakeOver : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public int type;
        
        public static TakeOver New(
            Namespace @namespace,
            int type
        )
        {
            var instance = CreateInstance<TakeOver>();
            instance.name = "Runtime";
            instance.Namespace = @namespace;
            instance.type = type;
            return instance;
        }

        public TakeOver Clone()
        {
            var instance = CreateInstance<TakeOver>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.type = type;
            return instance;
        }
    }
}