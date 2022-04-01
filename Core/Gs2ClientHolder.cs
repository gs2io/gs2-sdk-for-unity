using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gs2.Unity.Core;
using Gs2.Unity.Core.ScriptableObject;
using Gs2.Unity.Gs2Distributor.ScriptableObject;
using Gs2.Unity.Util;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Core
{
    [AddComponentMenu("GS2 UIKit/Core/Gs2ClientHolder")]
    public class Gs2ClientHolder : MonoBehaviour
    {
        private Profile _profile;

        public Gs2Domain Gs2 { get; private set; }
        public bool Initialized => Gs2 != null;
        
        public string activeEnvironmentName;
        public Gs2Environment environment;
        public Namespace distributorNamespace;

        [Serializable]
        private class InitializedEvent : UnityEvent
        {
            
        }
        
        [SerializeField]
        private InitializedEvent onInitialized = new InitializedEvent();
        
        public event UnityAction OnInitialized
        {
            add => onInitialized.AddListener(value);
            remove => onInitialized.RemoveListener(value);
        }
        
        [SerializeField]
        internal ErrorEvent onError = new ErrorEvent();
        
        public event UnityAction<Exception, Func<IEnumerator>> OnError
        {
            add => onError.AddListener(value);
            remove => onError.RemoveListener(value);
        }

        public static Gs2ClientHolder Instance
        {
            get
            {
                var clientHolders = GameObject.FindObjectsOfType<Gs2ClientHolder>();
                if (clientHolders.Length != 1)
                {
                    Debug.LogError("Either no Gs2ClientHolder was found in the scene or more than one was found.");
                    Debug.LogError("シーン内に Gs2ClientHolder が見つからなかったか、2つ以上見つかりました。");
                    throw new ApplicationException("Either no Gs2ClientHolder was found in the scene or more than one was found.");
                }

                return clientHolders[0];
            }
        }
        
        public IEnumerator Start()
        {
            _profile = new Profile(
                environment.clientId,
                environment.clientSecret,
                new Gs2BasicReopener(),
                environment.region
            );
            var initializeFuture = _profile.Initialize();
            yield return initializeFuture;
            if (initializeFuture.Error != null)
            {
                onError.Invoke(initializeFuture.Error, Start);
                yield break;
            }
            
            Gs2 = new Gs2Domain(
                _profile,
                distributorNamespace?.namespaceName
            );
            
            onInitialized.Invoke();
        }
    }
}