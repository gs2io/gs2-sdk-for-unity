using System.Collections.Generic;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Chat
{
    [CreateAssetMenu(fileName = "StampElementCollection", menuName = "Game Server Services/Gs2Chat/UIKit/StampElementCollection")]
    public class Gs2ChatMessageStampElementCollection : ScriptableObject
    {
        public List<Gs2ChatMessageStampElement> elements;
    }
}