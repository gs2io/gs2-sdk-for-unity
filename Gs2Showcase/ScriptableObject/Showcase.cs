using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.Gs2Showcase.ScriptableObject
{
    [CreateAssetMenu(fileName = "Showcase", menuName = "Game Server Services/Gs2Showcase/Showcase")]
    public class Showcase : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string showcaseName;
        
        public static Showcase Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<Showcase>(assetPath));
        }
        
        public static Showcase New(
            Namespace Namespace,
            string showcaseName
        )
        {
            var instance = CreateInstance<Showcase>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.showcaseName = showcaseName;
            return instance;
        }
    }
}