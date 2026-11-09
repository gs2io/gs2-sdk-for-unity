using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
#if GS2_ENABLE_UNITASK
using Cysharp.Threading.Tasks;
#endif
using Google.Protobuf;
using Google.Protobuf.Collections;
using Gs2.Core;
using Gs2.Gs2Realtime.Message;
using Gs2.Unity.Gs2Realtime.Exception;
using Gs2.Unity.Gs2Realtime.Util;
using Gs2.Unity.Util;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Scripting;

namespace Gs2.Unity.Gs2Realtime
{
    public delegate void OnRelayMessageHandler(RelayBinaryMessage message);
    public delegate void OnRelayMessageWithMetadataHandler(RelayBinaryMessage message, MessageMetadata metadata);
    
    /// <summary>
    /// A RealtimeSession for relay rooms: binary messages are RelayBinaryMessages, which can be
    /// addressed to specific connections, and arrive through OnRelayMessage instead of OnMessage.
    /// </summary>
    [Preserve]
    public class RelayRealtimeSession : RealtimeSession
    {
        /// <summary>A relayed message arrived. Raised by Dispatch on the main thread.</summary>
        public event OnRelayMessageHandler OnRelayMessage;
        public event OnRelayMessageWithMetadataHandler OnRelayMessageWithMetadata;

        public RelayRealtimeSession(
            string accessToken, 
            string ipAddress, 
            int port, 
            string encryptionKey,
            ByteString profile = null
        ) : base(
                accessToken, 
                ipAddress, 
                port, 
                encryptionKey,
                profile
            )
        {
            
        }

        protected override void Handle(IMessage message, uint sequenceNumber, uint lifeTimeMilliSeconds)
        {
            if (message is BinaryMessage binaryMessage)
            {
                Enqueue(new OnRelayMessageEvent(
                    RelayBinaryMessage.Parser.ParseFrom(binaryMessage.Data),
                    new MessageMetadata
                    {
                        SequenceNumber = sequenceNumber,
                        LifeTimeMilliSeconds = lifeTimeMilliSeconds,
                    }
                ));
                return;
            }
            base.Handle(message, sequenceNumber, lifeTimeMilliSeconds);
        }

        internal void RaiseRelayMessage(RelayBinaryMessage message, MessageMetadata metadata)
        {
            OnRelayMessage?.Invoke(message);
            OnRelayMessageWithMetadata?.Invoke(message, metadata);
        }
#if GS2_ENABLE_UNITASK
        
        /// <summary>As the coroutine Send; throws SendException on failure.</summary>
        public async UniTask SendAsync(ByteString data, uint[] targetConnectionIds = null)
        {
            var relayBinaryMessage = new RelayBinaryMessage
            {
                Data = data
            };
            if (targetConnectionIds != null)
            {
                relayBinaryMessage.TargetConnectionId.AddRange(targetConnectionIds);
            }
            await SendAsync(
                new BinaryMessage
                {
                    Data = relayBinaryMessage.ToByteString()
                }
            );
        }

        private new async UniTask SendAsync(BinaryMessage message)
        {
            await base.SendAsync(message);
        }
#endif
        /// <summary>
        /// Relays data through the room. When targetConnectionIds is given, the message is
        /// addressed to those connections only.
        /// </summary>
        public IEnumerator Send(UnityAction<AsyncResult<bool>> callback, ByteString data, uint[] targetConnectionIds = null)
        {
            var relayBinaryMessage = new RelayBinaryMessage
            {
                Data = data
            };
            if (targetConnectionIds != null)
            {
                relayBinaryMessage.TargetConnectionId.AddRange(targetConnectionIds);
            }
            yield return Send(
                callback,
                new BinaryMessage
                {
                    Data = relayBinaryMessage.ToByteString()
                }
            );
        }

        private new IEnumerator Send(UnityAction<AsyncResult<bool>> callback, BinaryMessage message)
        {
            yield return base.Send(callback, message);
        }
    }
}