#if UNITY_INCLUDE_TESTS
using UnityEditor;
#endif
using UnityEngine;

namespace Gs2.Unity.Gs2Experience.ScriptableObject
{
    [CreateAssetMenu(fileName = "Experience", menuName = "Game Server Services/Gs2Experience/Experience")]
    public class Experience : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string experienceName;
        
#if UNITY_INCLUDE_TESTS
        public static Experience Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<Experience>(assetPath));
        }
#endif
        
        public static Experience New(
            Namespace Namespace,
            string experienceName
        )
        {
            var instance = CreateInstance<Experience>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.experienceName = experienceName;
            return instance;
        }

        public Experience Clone()
        {
            var instance = CreateInstance<Experience>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.experienceName = experienceName;
            return instance;
        }
    }
}