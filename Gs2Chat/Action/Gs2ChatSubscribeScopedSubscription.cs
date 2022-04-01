using System;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Chat
{
	[AddComponentMenu("GS2 UIKit/Chat/Gs2ChatSubscribeScopedSubscription")]
    public class Gs2ChatSubscribeScopedSubscription : MonoBehaviour
    {
        public Gs2ChatRoomSubscribeAction subscribeAction;
        public Gs2ChatRoomUnsubscribeAction unsubscribeAction;

        public void OnEnable()
        {
            subscribeAction.gameObject.SetActive(true);
        }

        public void OnDisable()
        {
            unsubscribeAction.gameObject.SetActive(true);
        }
    }
}