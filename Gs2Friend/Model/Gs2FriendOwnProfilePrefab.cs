using System;
using System.Collections;
using Gs2.Unity.UiKit.Gs2Friend.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Friend
{
    [RequireComponent(typeof(Gs2FriendOwnProfileFetcher))]
    [AddComponentMenu("GS2 UIKit/Friend/Gs2FriendOwnProfilePrefab")]
    public class Gs2FriendOwnProfilePrefab : MonoBehaviour
    {
        public IEnumerator Start()
        {
            var ownProfileFetcher = GetComponent<Gs2FriendOwnProfileFetcher>();
            yield return new WaitUntil(() => ownProfileFetcher.Fetched);
            
            onLoadPublic.Invoke(ownProfileFetcher.Profile.PublicProfile);
            onLoadFollower.Invoke(ownProfileFetcher.Profile.FollowerProfile);
            onLoadFriend.Invoke(ownProfileFetcher.Profile.FriendProfile);
            
            onLoadComplete.Invoke();
        }

        [Serializable]
        private class LoadPublicEvent : UnityEvent<string>
        {
            
        }
        
        [SerializeField]
        private LoadPublicEvent onLoadPublic = new LoadPublicEvent();
        
        public event UnityAction<string> OnLoadPublic
        {
            add => onLoadPublic.AddListener(value);
            remove => onLoadPublic.RemoveListener(value);
        }
        
        [Serializable]
        private class LoadFollowerEvent : UnityEvent<string>
        {
            
        }
        
        [SerializeField]
        private LoadFollowerEvent onLoadFollower = new LoadFollowerEvent();
        
        public event UnityAction<string> OnLoadFollower
        {
            add => onLoadFollower.AddListener(value);
            remove => onLoadFollower.RemoveListener(value);
        }
        
        [Serializable]
        private class LoadFriendEvent : UnityEvent<string>
        {
            
        }
        
        [SerializeField]
        private LoadFriendEvent onLoadFriend = new LoadFriendEvent();
        
        public event UnityAction<string> OnLoadFriend
        {
            add => onLoadFriend.AddListener(value);
            remove => onLoadFriend.RemoveListener(value);
        }
        
        [Serializable]
        private class LoadCompleteEvent : UnityEvent
        {
            
        }
        
        [SerializeField]
        private LoadCompleteEvent onLoadComplete = new LoadCompleteEvent();
        
        public event UnityAction OnLoadComplete
        {
            add => onLoadComplete.AddListener(value);
            remove => onLoadComplete.RemoveListener(value);
        }
    }
}