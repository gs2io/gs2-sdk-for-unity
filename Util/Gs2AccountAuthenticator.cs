using System.Collections;
using System.Linq;
#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
#endif
using Gs2.Core;
using Gs2.Core.Domain;
using Gs2.Core.Exception;
using Gs2.Core.Model;
using Gs2.Core.Net;
using Gs2.Gs2Account;
using Gs2.Gs2Account.Request;
using Gs2.Gs2Auth;
using Gs2.Gs2Auth.Model;
using Gs2.Gs2Auth.Request;
using Gs2.Gs2Gateway;
using Gs2.Gs2Gateway.Request;
using Gs2.Gs2Version;
using Gs2.Gs2Version.Request;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.Util
{
    public class Gs2AccountAuthenticator : IAuthenticator
    {
        private readonly Gs2WebSocketSession _session;
        private readonly Gs2RestSession _restSession;
        private readonly string _accountNamespaceName;
        private readonly string _keyId;
        private readonly string _userId;
        private readonly string _password;
        private readonly GatewaySetting _gatewaySetting;
        private readonly VersionSetting _versionSetting;
        
        public DetectVersionUpEvent onDetectVersionUp = new DetectVersionUpEvent();
        
        public Gs2AccountAuthenticator(
            Gs2WebSocketSession session,
            Gs2RestSession restSession,
            string accountNamespaceName,
            string keyId,
            string userId,
            string password,
            GatewaySetting gatewaySetting = null,
            VersionSetting versionSetting = null
        )
        {
            _session = session;
            _restSession = restSession;
            _accountNamespaceName = accountNamespaceName;
            _keyId = keyId;
            _userId = userId;
            _password = password;
            _gatewaySetting = gatewaySetting;
            _versionSetting = versionSetting;
        }

#if GS2_ENABLE_UNITASK

        public override async UniTask<AccessToken> AuthenticationAsync()
        {
            var accountClient = new Gs2AccountRestClient(_restSession);

            if (this._session.IsDisconnected()) {
                await this._session.ReOpenAsync();
            }
            
            string body = null;
            string signature = null;
            {
                var result = await accountClient.AuthenticationAsync(
                    new AuthenticationRequest()
                        .WithNamespaceName(_accountNamespaceName)
                        .WithUserId(_userId)
                        .WithPassword(_password)
                        .WithKeyId(_keyId)
                );

                body = result.Body;
                signature = result.Signature;
            }

            var authClient = new Gs2AuthRestClient(_restSession);

            var result2 = await authClient.LoginBySignatureAsync(
                new LoginBySignatureRequest()
                    .WithKeyId(_keyId)
                    .WithBody(body)
                    .WithSignature(signature)
            );

            var accessToken = new AccessToken()
                .WithToken(result2.Token)
                .WithUserId(result2.UserId)
                .WithExpire(result2.Expire);
            
            if (this._gatewaySetting != null) {
                try {
                    await new Gs2GatewayWebSocketClient(this._session).SetUserIdAsync(
                        new SetUserIdRequest()
                            .WithNamespaceName(this._gatewaySetting.gatewayNamespaceName)
                            .WithAccessToken(accessToken.Token)
                            .WithAllowConcurrentAccess(this._gatewaySetting.allowConcurrentAccess)
                    );
                }
                catch (ConflictException) {
                }
                catch (SessionNotOpenException) {
                    await this._session.ReOpenFuture();
                    throw;
                }
                NeedReAuthentication = false;
                this._session.OnDisconnect -= OnDisconnect;
                this._session.OnDisconnect += OnDisconnect;
            }
            if (this._versionSetting != null) {
                var checkVersionResult = await new Gs2VersionRestClient(_restSession).CheckVersionAsync(
                    new CheckVersionRequest()
                        .WithNamespaceName(this._versionSetting.versionNamespaceName)
                        .WithAccessToken(accessToken.Token)
                        .WithTargetVersions(this._versionSetting.targetVersions.Select(
                                v => v.ToModel()
                            ).ToArray()
                        )
                );
                if (checkVersionResult.ProjectToken != null) {
                    _restSession.Credential.ProjectToken = checkVersionResult.ProjectToken;
                    _session.Credential.ProjectToken = checkVersionResult.ProjectToken;
                }
                if (checkVersionResult.Errors.Length > 0) {
                    onDetectVersionUp.Invoke();
                    
                    throw new UnauthorizedException(
                        new[] {
                            new RequestError {
                                Component = "version",
                                Message = "version.version.check.error.failed",
                            }
                        }
                    );
                }
            }
            
            return accessToken;
        }

#endif
        
        private void OnDisconnect() {
            NeedReAuthentication = true;
        }
        
        public override Gs2Future<AccessToken> AuthenticationFuture()
        {
            IEnumerator Impl(Gs2Future<AccessToken> result) {
                var accountClient = new Gs2AccountRestClient(this._restSession);

                if (this._session.IsDisconnected()) {
                    yield return this._session.ReOpenFuture();
                }

                var authenticationFuture = accountClient.AuthenticationFuture(
                    new AuthenticationRequest()
                        .WithNamespaceName(_accountNamespaceName)
                        .WithUserId(_userId)
                        .WithPassword(_password)
                        .WithKeyId(_keyId)
                );
                yield return authenticationFuture;
                if (authenticationFuture.Error != null) {
                    result.OnError(authenticationFuture.Error);
                    yield break;
                }

                var body = authenticationFuture.Result.Body;
                var signature = authenticationFuture.Result.Signature;

                if (body == null || signature == null) {
                    yield break;
                }

                var authClient = new Gs2AuthRestClient(this._restSession);

                var future2 = authClient.LoginBySignatureFuture(
                    new LoginBySignatureRequest()
                        .WithKeyId(_keyId)
                        .WithBody(body)
                        .WithSignature(signature)
                );
                yield return future2;
                
                if (future2.Error != null) {
                    result.OnError(future2.Error);
                    yield break;
                }

                var accessToken = new AccessToken()
                    .WithToken(future2.Result.Token)
                    .WithExpire(future2.Result.Expire)
                    .WithUserId(future2.Result.UserId);
                
                if (this._gatewaySetting != null) {
                    var future = new Gs2GatewayWebSocketClient(this._session).SetUserIdFuture(
                        new SetUserIdRequest()
                            .WithNamespaceName(this._gatewaySetting.gatewayNamespaceName)
                            .WithAccessToken(accessToken.Token)
                            .WithAllowConcurrentAccess(this._gatewaySetting.allowConcurrentAccess)
                    );
                    yield return future;
                    if (future.Error != null) {
                        if (future.Error is ConflictException) {
                        }
                        else if (future.Error is SessionNotOpenException) {
                            yield return this._session.ReOpenFuture();
                            result.OnError(future.Error);
                            yield break;
                        }
                        else {
                            result.OnError(future.Error);
                            yield break;
                        }
                    }
                    NeedReAuthentication = false;
                    this._session.OnDisconnect -= OnDisconnect;
                    this._session.OnDisconnect += OnDisconnect;
                }
                if (this._versionSetting != null) {
                    var future = new Gs2VersionRestClient(_restSession).CheckVersionFuture(
                        new CheckVersionRequest()
                            .WithNamespaceName(this._versionSetting.versionNamespaceName)
                            .WithAccessToken(accessToken.Token)
                            .WithTargetVersions(this._versionSetting.targetVersions.Select(
                                    v => v.ToModel()
                                ).ToArray()
                            )
                    );
                    yield return future;
                    if (future.Error != null) {
                        result.OnError(future.Error);
                        yield break;
                    }
                    var checkVersionResult = future.Result;
                    if (checkVersionResult.ProjectToken != null) {
                        _restSession.Credential.ProjectToken = checkVersionResult.ProjectToken;
                        _session.Credential.ProjectToken = checkVersionResult.ProjectToken;
                    }
                    if (checkVersionResult.Errors.Length > 0) {
                        onDetectVersionUp.Invoke();

                        result.OnError(
                            new UnauthorizedException(
                                new[] {
                                    new RequestError {
                                        Component = "version",
                                        Message = "version.version.check.error.failed",
                                    }
                                }
                            )
                        );
                        yield break;
                    }
                }
                
                result.OnComplete(accessToken);
            }

            return new Gs2InlineFuture<AccessToken>(Impl);
        }
        
        public override IEnumerator Authentication(UnityAction<AsyncResult<AccessToken>> callback)
        {
            var future = AuthenticationFuture();
            yield return future;
            callback.Invoke(new AsyncResult<AccessToken>(
                future.Result,
                future.Error
            ));
        }
    }
}