using System;
using System.Collections;
using System.Collections.Generic;
using Google;
using Gs2.Core.Exception;
using Gs2.Core.Model;
using Gs2.Unity.Core.Exception;
using Gs2.Unity.UiKit.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Account
{
	[AddComponentMenu("GS2 UIKit/Account/Gs2AccountLoadGoogleAccount")]
    public partial class Gs2AccountLoadGoogleAccount : MonoBehaviour
    {
        private IEnumerator Process()
        {
            if (GoogleSignIn.Configuration == null)
            {
                GoogleSignIn.Configuration = new GoogleSignInConfiguration
                {
                    RequestIdToken = true,
                    WebClientId = clientId,
                    UseGameSignIn = false
                };
            }

            //string userId = null;
            System.Threading.Tasks.Task<GoogleSignInUser> task = null;
            GoogleSignIn.DefaultInstance.SignIn().ContinueWith(_task =>
            {
                //userId = user.Result.UserId;
                //Debug.Log("user.Result.UserId : " + user.Result.UserId);

                task = _task;
            });
            
            yield return new WaitUntil(() => task != null);
            
            GoogleSignIn.DefaultInstance.Disconnect();
            
            if (task.IsFaulted) {
                using (IEnumerator<System.Exception> enumerator =
                    task.Exception.InnerExceptions.GetEnumerator()) {
                    if (enumerator.MoveNext()) {
                        GoogleSignIn.SignInException error =
                            (GoogleSignIn.SignInException)enumerator.Current;
                        Debug.Log("Got Error: " + error.Status + " " + error.Message);
                    } else {
                        Debug.Log("Got Unexpected Exception?!?" + task.Exception);
                    }
                }
                onError.Invoke(new UnauthorizedException(new RequestError[]
                {
                    new RequestError(
                        "GoogleSignIn",
                        "account.login.GoogleSignIn.error.failed"
                    )
                }), null);
            } else if(task.IsCanceled) {
                Debug.Log("Canceled");
                onCancel.Invoke();
            } else  {
                Debug.Log("Welcome: " + task.Result.DisplayName + "!");
                onLoadUserIdentifier.Invoke(task.Result.UserId);
                onLoadPassword.Invoke(task.Result.UserId);
                onLoadComplete.Invoke();
            }
            yield return null;
        }
        
        public void OnEnable()
        {
            StartCoroutine(nameof(Process));
        }
        
        public void OnDisable()
        {
            StopCoroutine(nameof(Process));
        }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2AccountLoadGoogleAccount
    {
        public string clientId;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2AccountLoadGoogleAccount
    {
        [Serializable]
        private class LoadUserIdentifierEvent : UnityEvent<string>
        {
            
        }
        
        [SerializeField]
        private LoadUserIdentifierEvent onLoadUserIdentifier = new LoadUserIdentifierEvent();
        
        public event UnityAction<string> OnLoadUserIdentifier
        {
            add => onLoadUserIdentifier.AddListener(value);
            remove => onLoadUserIdentifier.RemoveListener(value);
        }

        [Serializable]
        private class LoadPasswordEvent : UnityEvent<string>
        {
            
        }
        
        [SerializeField]
        private LoadPasswordEvent onLoadPassword = new LoadPasswordEvent();
        
        public event UnityAction<string> OnLoadPassword
        {
            add => onLoadPassword.AddListener(value);
            remove => onLoadPassword.RemoveListener(value);
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

        [Serializable]
        private class CancelEvent : UnityEvent
        {
            
        }
        
        [SerializeField]
        private CancelEvent onCancel = new CancelEvent();
        
        public event UnityAction OnCancel
        {
            add => onCancel.AddListener(value);
            remove => onCancel.RemoveListener(value);
        }
        
        [SerializeField]
        internal ErrorEvent onError = new ErrorEvent();
        
        public event UnityAction<Exception, Func<IEnumerator>> OnError
        {
            add => onError.AddListener(value);
            remove => onError.RemoveListener(value);
        }
    }
}