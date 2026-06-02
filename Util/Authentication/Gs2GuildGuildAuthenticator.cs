using System.Collections;
using Gs2.Core.Domain;
using Gs2.Core.Exception;
using Gs2.Gs2Auth.Model;
using Gs2.Gs2Gateway;
using Gs2.Gs2Gateway.Request;
using Gs2.Gs2Guild;
using Gs2.Gs2Guild.Request;
#if UNITY_2017_1_OR_NEWER
using UnityEngine;
#endif
#if UNITY_2017_1_OR_NEWER && GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
#elif !UNITY_2017_1_OR_NEWER
using System.Threading.Tasks;
#endif

namespace Gs2.Unity.Util
{
    public class Gs2GuildGuildAuthenticator : IGuildAuthenticator
    {
        public bool NeedReAuthentication { get; protected set; }

        private readonly GuildSetting _guildSetting;
        private readonly GatewaySetting _gatewaySetting;

        public Gs2GuildGuildAuthenticator(
            GuildSetting guildSetting,
            GatewaySetting gatewaySetting = null
        ) {
            this._guildSetting = guildSetting;
            this._gatewaySetting = gatewaySetting ?? new GatewaySetting {
                gatewayNamespaceName = "default",
                allowConcurrentAccess = true
            };
        }

#if !UNITY_2017_1_OR_NEWER || GS2_ENABLE_UNITASK

    #if UNITY_2017_1_OR_NEWER
        public async UniTask<AccessToken> AuthenticationAsync(
            Gs2Connection connection,
            IGameSession userGameSession,
            string guildName
        )
    #else
        public async Task<AccessToken> AuthenticationAsync(
            Gs2Connection connection,
            IGameSession userGameSession,
            string guildName
        )
    #endif
        {
            if (connection.IsDisconnected()) {
                await connection.ConnectAsync();
            }

            var guildClient = new Gs2GuildRestClient(connection.RestSession);

            var result = await guildClient.AssumeAsync(
                new AssumeRequest()
                    .WithAccessToken(userGameSession.AccessToken.Token)
                    .WithNamespaceName(this._guildSetting.guildNamespaceName)
                    .WithGuildModelName(this._guildSetting.guildModelName)
                    .WithGuildName(guildName)
            );

            var accessToken = new AccessToken()
                .WithToken(result.Token)
                .WithUserId(result.UserId)
                .WithFederationFromUserId(userGameSession.UserId)
                .WithExpire(result.Expire)
                .WithTimeOffset(userGameSession.AccessToken.TimeOffset);

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
#if UNITY_2017_1_OR_NEWER
                    Debug.Log("The GS2-Gateway namespace does not exist and could not be configured to receive notifications from the server.");
#endif
                }
                catch (SessionNotOpenException) {
                    await connection.WebSocketSession.ReOpenAsync();
                    throw;
                }
                NeedReAuthentication = false;
                connection.WebSocketSession.OnDisconnect -= OnDisconnect;
                connection.WebSocketSession.OnDisconnect += OnDisconnect;
            }
            
            return accessToken;
        }

#endif
        
        private void OnDisconnect() {
            NeedReAuthentication = true;
        }
        
#if UNITY_2017_1_OR_NEWER
        public Gs2Future<AccessToken> AuthenticationFuture(
            Gs2Connection connection,
            IGameSession userGameSession,
            string guildName
        )
        {
            IEnumerator Impl(Gs2Future<AccessToken> result) {
                if (connection.IsDisconnected()) {
                    yield return connection.ConnectFuture();
                }

                var guildClient = new Gs2GuildRestClient(connection.RestSession);

                var assumeFuture = guildClient.AssumeFuture(
                    new AssumeRequest()
                        .WithAccessToken(userGameSession.AccessToken.Token)
                        .WithNamespaceName(this._guildSetting.guildNamespaceName)
                        .WithGuildModelName(this._guildSetting.guildModelName)
                        .WithGuildName(guildName)
                );
                yield return assumeFuture;
                if (assumeFuture.Error != null) {
                    result.OnError(assumeFuture.Error);
                    yield break;
                }

                var accessToken = new AccessToken()
                    .WithToken(assumeFuture.Result.Token)
                    .WithExpire(assumeFuture.Result.Expire)
                    .WithUserId(assumeFuture.Result.UserId);
                
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
                
                result.OnComplete(accessToken);
            }

            return new Gs2InlineFuture<AccessToken>(Impl);
        }
#else
        public Gs2Future<AccessToken> AuthenticationFuture(
            Gs2Connection connection,
            IGameSession userGameSession,
            string guildName
        )
        {
            throw new System.NotSupportedException(
                "Future (coroutine) based authentication is not available outside of Unity. Use AuthenticationAsync instead."
            );
        }
#endif
    }
}