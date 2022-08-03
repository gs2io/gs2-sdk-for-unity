using UnityEngine;

namespace Gs2.Unity.Gs2Exchange.ScriptableObject
{
    public class Await : UnityEngine.ScriptableObject
    {
        public Rate rate;
        public string awaitName;
        
        public static Await New(
            Rate rate,
            string awaitName
        )
        {
            var instance = CreateInstance<Await>();
            instance.name = "Runtime";
            instance.rate = rate;
            instance.awaitName = awaitName;
            return instance;
        }

        public Await Clone()
        {
            var instance = CreateInstance<Await>();
            instance.name = "Runtime";
            instance.rate = rate;
            instance.awaitName = awaitName;
            return instance;
        }
    }
}