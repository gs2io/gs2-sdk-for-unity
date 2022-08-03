#if UNITY_INCLUDE_TESTS
using UnityEditor;
#endif
using UnityEngine;

namespace Gs2.Unity.Gs2Version.ScriptableObject
{
    [CreateAssetMenu(fileName = "Version", menuName = "Game Server Services/Gs2Version/Version")]
    public class Version : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string versionName;
        
#if UNITY_INCLUDE_TESTS
        public static Version Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<Version>(assetPath));
        }
#endif
        
        public static Version New(
            Namespace Namespace,
            string versionName
        )
        {
            var instance = CreateInstance<Version>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.versionName = versionName;
            return instance;
        }

        public Version Clone()
        {
            var instance = CreateInstance<Version>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.versionName = versionName;
            return instance;
        }
    }
}