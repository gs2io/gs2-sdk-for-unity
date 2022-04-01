using System;
using System.Collections;
using Gs2.Unity.UiKit.Gs2Friend.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Friend
{
    [AddComponentMenu("GS2 UIKit/Friend/Gs2FriendProfileFetcher")]
    public partial class Gs2FriendProfileFetcher : MonoBehaviour
    {
        public IEnumerator Start()
        {
            var publicProfileFetcher = GetComponentInParent<Gs2FriendPublicProfileFetcher>() ?? GetComponent<Gs2FriendPublicProfileFetcher>();
            var followFetcher = GetComponentInParent<Gs2FriendFollowFetcher>() ?? GetComponent<Gs2FriendFollowFetcher>();
            var friendFetcher = GetComponentInParent<Gs2FriendFriendFetcher>() ?? GetComponent<Gs2FriendFriendFetcher>();

            if (publicProfileFetcher != null)
            {
                yield return new WaitUntil(() => publicProfileFetcher.Fetched);
                FriendProfile = "";
                FollowerProfile = "";
                PublicProfile = publicProfileFetcher.PublicProfile?.PublicProfile ?? "";
            }
            if (followFetcher != null && followFetcher.withProfile)
            {
                yield return new WaitUntil(() => followFetcher.Fetched);
                FriendProfile = "";
                FollowerProfile = followFetcher.Follow.FollowerProfile ?? "";
                PublicProfile = followFetcher.Follow.PublicProfile ?? "";
            }
            if (friendFetcher != null && friendFetcher.withProfile)
            {
                yield return new WaitUntil(() => friendFetcher.Fetched);
                FollowerProfile = "";
                FriendProfile = friendFetcher.Friend.FriendProfile ?? "";
                PublicProfile = friendFetcher.Friend.PublicProfile ?? "";
            }

            Fetched = true;
        }
    }
    
    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2FriendProfileFetcher
    {
        public string PublicProfile { get; private set; }
        public string FollowerProfile { get; private set; }
        public string FriendProfile { get; private set; }
        public bool Fetched { get; private set; }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FriendProfileFetcher
    {
        
    }
}