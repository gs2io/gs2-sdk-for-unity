#if UNITY_INCLUDE_TESTS
using UnityEditor;
#endif
using UnityEngine;

namespace Gs2.Unity.Gs2Version.ScriptableObject
{
    [CreateAssetMenu(fileName = "CurrentVersion", menuName = "Game Server Services/Gs2Version/CurrentVersion")]
    public class CurrentVersion : UnityEngine.ScriptableObject
    {
        public Version version;
        public int major;
        public int minor;
        public int micro;
        public string body;
        public string signature;
        
#if UNITY_INCLUDE_TESTS
        public static CurrentVersion Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<CurrentVersion>(assetPath));
        }
#endif
        
        public static CurrentVersion New(
            Version version,
            int major,
            int minor,
            int micro,
            string body = null,
            string signature = null
        )
        {
            var instance = CreateInstance<CurrentVersion>();
            instance.name = "Runtime";
            instance.version = version;
            instance.major = major;
            instance.minor = minor;
            instance.micro = micro;
            instance.body = body;
            instance.signature = signature;
            return instance;
        }

        public CurrentVersion Clone()
        {
            var instance = CreateInstance<CurrentVersion>();
            instance.name = "Runtime";
            instance.version = version;
            instance.major = major;
            instance.minor = minor;
            instance.micro = micro;
            instance.body = body;
            instance.signature = signature;
            return instance;
        }
    }
}