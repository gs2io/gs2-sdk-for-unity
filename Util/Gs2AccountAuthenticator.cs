using System.Collections;
using Gs2.Core;
using Gs2.Core.Net;
using Gs2.Gs2Account;
using Gs2.Gs2Account.Request;
using Gs2.Gs2Auth;
using Gs2.Gs2Auth.Model;
using Gs2.Gs2Auth.Request;
using UnityEngine.Events;

namespace Gs2.Unity.Util
{
    public class Gs2AccountAuthenticator : IAuthenticator
    {
        private readonly Gs2RestSession _session;
        private readonly string _accountNamespaceName;
        private readonly string _keyId;
        private readonly string _userId;
        private readonly string _password;
        
        public Gs2AccountAuthenticator(
            Gs2RestSession session,
            string accountNamespaceName,
            string keyId,
            string userId,
            string password
        )
        {
            _session = session;
            _accountNamespaceName = accountNamespaceName;
            _keyId = keyId;
            _userId = userId;
            _password = password;
        }

        public override IEnumerator Authentication(UnityAction<AsyncResult<AccessToken>> callback)
        {
            var accountClient = new Gs2AccountRestClient(_session);

            string body = null;
            string signature = null;
            
            yield return accountClient.Authentication(
                new AuthenticationRequest()
                    .WithNamespaceName(_accountNamespaceName)
                    .WithUserId(_userId)
                    .WithPassword(_password)
                    .WithKeyId(_keyId),
                r =>
                {
                    if (r.Error != null)
                    {
                        callback.Invoke(
                            new AsyncResult<AccessToken>(
                                null,
                                r.Error
                            )
                        );
                    }
                    else
                    {
                        body = r.Result.Body;
                        signature = r.Result.Signature;
                    }
                }
            );

            if (body == null || signature == null)
            {
                yield break;
            }
            
            var authClient = new Gs2AuthRestClient(_session);

            yield return authClient.LoginBySignature(
                new LoginBySignatureRequest()
                    .WithUserId(_userId)
                    .WithKeyId(_keyId)
                    .WithBody(body)
                    .WithSignature(signature),
                r =>
                {
                    if (r.Error != null)
                    {
                        callback.Invoke(
                            new AsyncResult<AccessToken>(
                                null,
                                r.Error
                            )
                        );
                    }
                    else
                    {
                        callback.Invoke(
                            new AsyncResult<AccessToken>(
                                new AccessToken()
                                    .WithToken(r.Result.Token)
                                    .WithExpire(r.Result.Expire)
                                    .WithUserId(r.Result.UserId), 
                                r.Error
                            )
                        );
                    }
                }
            );
        }
    }
}