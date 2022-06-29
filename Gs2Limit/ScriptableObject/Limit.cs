using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.Gs2Limit.ScriptableObject
{
    [CreateAssetMenu(fileName = "Limit", menuName = "Game Server Services/Gs2Limit/Limit")]
    public class Limit : UnityEngine.ScriptableObject
    {
        public Namespace Namespace;
        public string limitName;
        
        public static Limit Load(
            string assetPath
        )
        {
            return Instantiate(
                AssetDatabase.LoadAssetAtPath<Limit>(assetPath));
        }
        
        public static Limit New(
            Namespace Namespace,
            string limitName
        )
        {
            var instance = CreateInstance<Limit>();
            instance.name = "Runtime";
            instance.Namespace = Namespace;
            instance.limitName = limitName;
            return instance;
        }
    }
}