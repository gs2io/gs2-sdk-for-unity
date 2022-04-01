using System;
using System.Collections;
#if ENABLE_SIGN_IN_WITH_APPLE
using AppleAuth;
using AppleAuth.Enums;
using AppleAuth.Extensions;
using AppleAuth.Interfaces;
using AppleAuth.Native;
#endif
using Gs2.Core.Exception;
using Gs2.Core.Model;
using Gs2.Unity.UiKit.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Account
{
    [AddComponentMenu("GS2 UIKit/Account/Gs2AccountLoadAppleId")]
    public partial class Gs2AccountLoadAppleId : MonoBehaviour
    {
#if ENABLE_SIGN_IN_WITH_APPLE
        private AppleAuthManager _appleAuthManager;
#endif
        private IEnumerator Process()
        {
#if ENABLE_SIGN_IN_WITH_APPLE
            if (!AppleAuthManager.IsCurrentPlatformSupported)
            {
                onError.Invoke(new UnauthorizedException(new RequestError[]
                {
                    new RequestError(
                        "platform",
                        "account.login.platform.error.invalid"
                    )
                }), null);
            }

            var deserializer = new PayloadDeserializer();
            _appleAuthManager = new AppleAuthManager(deserializer);

            _appleAuthManager.QuickLogin(
                new AppleAuthQuickLoginArgs(),
                credential =>
                {
                    if (credential is IAppleIDCredential appleIdCredential)
                    {
                        onLoadUserIdentifier.Invoke(appleIdCredential.User);
                        onLoadPassword.Invoke(appleIdCredential.User);
                    }
                    else
                    {
                        onError.Invoke(new UnauthorizedException(new RequestError[]
                        {
                            new RequestError(
                                "appleIdCredential",
                                "account.login.appleIdCredential.error.failed"
                            )
                        }), null);
                        return;
                    }

                    onLoadComplete.Invoke();
                },
                error =>
                {
                    var authorizationErrorCode = error.GetAuthorizationErrorCode();
                    SignInWithApple();
                }
            );
#else
            onError.Invoke(new UnauthorizedException(new RequestError[]
            {
	            new RequestError(
                    "plugin",
                    "account.login.plugin.error.notInstalled"
                    )
            }), null);
#endif
            yield return null;
        }

#if ENABLE_SIGN_IN_WITH_APPLE
        private void SignInWithApple()
        {
            var loginArgs = new AppleAuthLoginArgs(LoginOptions.IncludeEmail | LoginOptions.IncludeFullName);

            this._appleAuthManager.LoginWithAppleId(
                loginArgs,
                credential =>
                {
                    if (credential is IAppleIDCredential appleIdCredential)
                    {
                        onLoadUserIdentifier.Invoke(appleIdCredential.User);
                        onLoadPassword.Invoke(appleIdCredential.User);
                    }
                    else
                    {
                        onError.Invoke(new UnauthorizedException(new RequestError[]
                        {
                            new RequestError(
                                "appleIdCredential",
                                "account.login.appleIdCredential.error.failed"
                            )
                        }), null);
                        return;
                    }

                    onLoadComplete.Invoke();
                },
                error =>
                {
                    var authorizationErrorCode = error.GetAuthorizationErrorCode();
                    onError.Invoke(new UnauthorizedException(new RequestError[]
                    {
                        new RequestError(
                            "signInWithApple",
                            authorizationErrorCode.ToString()
                        )
                    }), null);
                });
        }
#endif

        public void OnEnable()
        {
            StartCoroutine(nameof(Process));
        }
        
        public void OnDisable()
        {
            StopCoroutine(nameof(Process));
        }

        void Update()
        {
#if ENABLE_SIGN_IN_WITH_APPLE
            if (_appleAuthManager != null)
            {
                _appleAuthManager.Update();
            }
#endif
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2AccountLoadAppleId
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

        [SerializeField]
        internal ErrorEvent onError = new ErrorEvent();
        
        public event UnityAction<Exception, Func<IEnumerator>> OnError
        {
            add => onError.AddListener(value);
            remove => onError.RemoveListener(value);
        }
    }
}