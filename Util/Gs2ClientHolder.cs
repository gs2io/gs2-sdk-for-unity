using System;
using System.Collections;
using Gs2.Core.Exception;
#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
#endif
using Gs2.Unity.Core;
using Gs2.Unity.Core.ScriptableObject;
using Gs2.Unity.Gs2Distributor.ScriptableObject;
using Gs2.Unity.Util;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.Util
{
    [AddComponentMenu("GS2/Core/Gs2ClientHolder")]
    public class Gs2ClientHolder : MonoBehaviour
    {
        private Profile _profile;
        public Profile Profile => _profile;
        
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
        
        public event UnityAction<Gs2Exception> OnError
        {
            add => onError.AddListener(value);
            remove => onError.RemoveListener(value);
        }

        protected internal static Gs2ClientHolder _instance;
        
        public static Gs2ClientHolder Instance
        {
            get
            {
                if (_instance == null)
                {
                    var clientHolders = GameObject.FindObjectsOfType<Gs2ClientHolder>();
                    if (clientHolders.Length > 0)
                    {
                        return clientHolders[0];
                    }

                    _instance = new GameObject("Gs2ClientHolder").AddComponent<Gs2ClientHolder>();
                }
                return _instance;
            }
        }

        public void Awake()
        {
            var items = FindObjectsOfType<Gs2ClientHolder>();
            foreach (var item in items)
            {
                if (item != this)
                {
                    DestroyImmediate(this.gameObject);
                }
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
                onError.Invoke(initializeFuture.Error);
                yield break;
            }
            
            Gs2 = new Gs2Domain(
                _profile,
                distributorNamespace?.namespaceName
            );
            
            onInitialized.Invoke();
        }
        
#if GS2_ENABLE_UNITASK
        public async UniTask StartAsync()
        {
            _profile = new Profile(
                environment.clientId,
                environment.clientSecret,
                new Gs2BasicReopener(),
                environment.region
            );
            await _profile.InitializeAsync();
            
            Gs2 = new Gs2Domain(
                _profile,
                distributorNamespace.namespaceName
            );
            
            onInitialized.Invoke();
        }
#endif
    }
}