using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.Gs2Formation.ScriptableObject
{
    [CreateAssetMenu(fileName = "Namespace", menuName = "Game Server Services/Gs2Formation/Namespace")]
    public class Namespace : UnityEngine.ScriptableObject
    {
        public string namespaceName;
        
        public static Namespace Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<Namespace>(assetPath));
        }
        
        public static Namespace New(
            string namespaceName
        )
        {
            var instance = CreateInstance<Namespace>();
            instance.name = "Runtime";
            instance.namespaceName = namespaceName;
            return instance;
        }
    }
}