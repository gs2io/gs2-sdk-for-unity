using System;
using System.Collections;
using System.Linq;
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
#if GS2_ENABLE_UNITASK
using Gs2.Unity.Core;
using Cysharp.Threading.Tasks;
#endif

namespace Gs2.Unity.Util
{
    public class Gs2AccountAuthenticator : IAuthenticator
    {
        private readonly AccountSetting _accountSetting;
        private readonly GatewaySetting _gatewaySetting;
        private readonly VersionSetting _versionSetting;

        [Obsolete("Already this member is not needed")]
        internal string userId;
        [Obsolete("Already this member is not needed")]
        internal string password;
        
        public DetectVersionUpEvent onDetectVersionUp = new DetectVersionUpEvent();

        [Obsolete("Migration to Gs2AccountAuthenticator(AccountSetting, GatewaySetting, VersionSetting)")]
        public Gs2AccountAuthenticator(
            Gs2WebSocketSession webSocketSession,
            Gs2RestSession restSession,
            string accountNamespaceName,
            string keyId,
            string userId,
            string password,
            GatewaySetting gatewaySetting = null,
            VersionSetting versionSetting = null
        ): this(
            new AccountSetting {
                accountNamespaceName = accountNamespaceName,
                keyId = keyId
            },
            gatewaySetting,
            versionSetting
        ) {
            this.userId = userId;
            this.password = password;
        }
        
        public Gs2AccountAuthenticator(
            AccountSetting accountSetting,
            GatewaySetting gatewaySetting = null,
            VersionSetting versionSetting = null
        ) {
            this._accountSetting = accountSetting;
            this._gatewaySetting = gatewaySetting ?? new GatewaySetting {
                gatewayNamespaceName = "default",
                allowConcurrentAccess = true
            };
            this._versionSetting = versionSetting;
        }

#if GS2_ENABLE_UNITASK

        internal override async UniTask<AccessToken> AuthenticationAsync(
            Gs2Connection connection,
            string userId,
            string password
        )
        {
            if (connection.IsDisconnected()) {
                await connection.ConnectAsync();
            }

            var accountClient = new Gs2AccountRestClient(connection.RestSession);

            string body;
            string signature;
            {
                var result = await accountClient.AuthenticationAsync(
                    new AuthenticationRequest()
                        .WithNamespaceName(this._accountSetting.accountNamespaceName)
                        .WithUserId(userId)
                        .WithPassword(password)
                        .WithKeyId(this._accountSetting.keyId)
                );

                body = result.Body;
                signature = result.Signature;
            }

            var authClient = new Gs2AuthRestClient(connection.RestSession);

            var result2 = await authClient.LoginBySignatureAsync(
                new LoginBySignatureRequest()
                    .WithKeyId(this._accountSetting.keyId)
                    .WithBody(body)
                    .WithSignature(signature)
            );

            var accessToken = new AccessToken()
                .WithToken(result2.Token)
                .WithUserId(result2.UserId)
                .WithExpire(result2.Expire);
            
            if (this._gatewaySetting != null) {
                try {
                    await new Gs2GatewayWebSocketClient(connection.WebSocketSession).SetUserIdAsync(
                        new SetUserIdRequest()
                            .WithNamespaceName(this._gatewaySetting.gatewayNamespaceName)
                            .WithAccessToken(accessToken.Token)
                            .WithAllowConcurrentAccess(this._gatewaySetting.allowConcurrentAccess)
                    );
                }
                catch (ConflictException) {
                }
                catch (NotFoundException) {
                    Debug.Log("The GS2-Gateway namespace does not exist and could not be configured to receive notifications from the server.");
                }
                catch (SessionNotOpenException) {
                    await connection.WebSocketSession.ReOpenFuture();
                    throw;
                }
                NeedReAuthentication = false;
                connection.WebSocketSession.OnDisconnect -= OnDisconnect;
                connection.WebSocketSession.OnDisconnect += OnDisconnect;
            }
            if (this._versionSetting != null) {
                var checkVersionResult = await new Gs2VersionRestClient(connection.RestSession).CheckVersionAsync(
                    new CheckVersionRequest()
                        .WithNamespaceName(this._versionSetting.versionNamespaceName)
                        .WithAccessToken(accessToken.Token)
                        .WithTargetVersions(this._versionSetting.targetVersions.Select(
                                v => v.ToModel()
                            ).ToArray()
                        )
                );
                if (checkVersionResult.ProjectToken != null) {
                    connection.RestSession.Credential.ProjectToken = checkVersionResult.ProjectToken;
                    connection.WebSocketSession.Credential.ProjectToken = checkVersionResult.ProjectToken;
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
        
        internal override Gs2Future<AccessToken> AuthenticationFuture(
            Gs2Connection connection,
            string userId,
            string password
        )
        {
            IEnumerator Impl(Gs2Future<AccessToken> result) {
                if (connection.IsDisconnected()) {
                    yield return connection.ConnectFuture();
                }

                var accountClient = new Gs2AccountRestClient(connection.RestSession);

                var authenticationFuture = accountClient.AuthenticationFuture(
                    new AuthenticationRequest()
                        .WithNamespaceName(this._accountSetting.accountNamespaceName)
                        .WithUserId(userId)
                        .WithPassword(password)
                        .WithKeyId(this._accountSetting.keyId)
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

                var authClient = new Gs2AuthRestClient(connection.RestSession);

                var future2 = authClient.LoginBySignatureFuture(
                    new LoginBySignatureRequest()
                        .WithKeyId(this._accountSetting.keyId)
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
                    var future = new Gs2GatewayWebSocketClient(connection.WebSocketSession).SetUserIdFuture(
                        new SetUserIdRequest()
                            .WithNamespaceName(this._gatewaySetting.gatewayNamespaceName)
                            .WithAccessToken(accessToken.Token)
                            .WithAllowConcurrentAccess(this._gatewaySetting.allowConcurrentAccess)
                    );
                    yield return future;
                    if (future.Error != null) {
                        if (future.Error is ConflictException) {
                        }
                        else if (future.Error is NotFoundException) {
                            Debug.Log("The GS2-Gateway namespace does not exist and could not be configured to receive notifications from the server.");
                        }
                        else if (future.Error is SessionNotOpenException) {
                            yield return connection.WebSocketSession.ReOpenFuture();
                            result.OnError(future.Error);
                            yield break;
                        }
                        else {
                            result.OnError(future.Error);
                            yield break;
                        }
                    }
                    NeedReAuthentication = false;
                    connection.WebSocketSession.OnDisconnect -= OnDisconnect;
                    connection.WebSocketSession.OnDisconnect += OnDisconnect;
                }
                if (this._versionSetting != null && !string.IsNullOrEmpty(this._versionSetting.versionNamespaceName)) {
                    var future = new Gs2VersionRestClient(connection.RestSession).CheckVersionFuture(
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
                        connection.RestSession.Credential.ProjectToken = checkVersionResult.ProjectToken;
                        connection.WebSocketSession.Credential.ProjectToken = checkVersionResult.ProjectToken;
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
    }
}