#define DISABLE_SIGNATURE_CHECK

using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Gs2.Gs2Realtime.Message;

namespace Gs2.Unity.Gs2Realtime.Util
{
    public class Messenger
    {
        private readonly HMACSHA256 _sha256;
        
        public Messenger(
            string encryptionKey
        )
        {
            _sha256 = new HMACSHA256(
                System.Text.Encoding.ASCII.GetBytes(encryptionKey)
            );
        }
        
        public byte[] Pack(
            IMessage payload,
            Container.Types.MessageType? messageType = null
        )
        {
            if (!messageType.HasValue)
            {
                switch (payload)
                {
                    case HelloRequest _:
                        messageType = Container.Types.MessageType.HelloRequest;
                        break;
                    case HelloResult _:
                        messageType = Container.Types.MessageType.HelloResult;
                        break;
                    case ByeRequest _:
                        messageType = Container.Types.MessageType.ByeRequest;
                        break;
                    case UpdateProfileRequest _:
                        messageType = Container.Types.MessageType.UpdateProfileRequest;
                        break;
                    case JoinNotification _:
                        messageType = Container.Types.MessageType.JoinNotification;
                        break;
                    case LeaveNotification _:
                        messageType = Container.Types.MessageType.LeaveNotification;
                        break;
                    case UpdateProfileNotification _:
                        messageType = Container.Types.MessageType.UpdateProfileNotification;
                        break;
                    case BinaryMessage _:
                        messageType = Container.Types.MessageType.BinaryMessage;
                        break;
                    case Error _:
                        messageType = Container.Types.MessageType.Error;
                        break;
                    default:
                        throw new TypeAccessException("unknown payload type.");
                }
            }
            return new Container
            {
                MessageType = messageType.Value,
                Payload = new Any()
                {
                    TypeUrl = "type.googleapis.com",
                    Value = payload.ToByteString()
                },
                Signature = ByteString.CopyFrom(
                    _sha256.ComputeHash(payload.ToByteArray())
                )
            }.ToByteArray();
        }

        public Tuple<Container.Types.MessageType, Any, uint, uint> Unpack(byte[] data)
        {
            var container = Container.Parser.ParseFrom(data);
            #if !DISABLE_SIGNATURE_CHECK
            _sha256.Initialize();
            var hash = _sha256.ComputeHash(container.Payload.Value.ToByteArray());
            if (!StructuralComparisons.StructuralEqualityComparer.Equals(hash, container.Signature.ToByteArray()))
            {
                throw new InvalidDataException("invalid signature.");
            }
            #endif
            return new Tuple<Container.Types.MessageType, Any, uint, uint>(
                container.MessageType,
                container.Payload,
                container.SequenceNumber,
                container.LifeTimeMilliSeconds
            );
        }

        public IMessage Parse(
            Container.Types.MessageType messageType,
            Any payload
        )
        {
            switch (messageType)
            {
                case Container.Types.MessageType.HelloRequest:
                    return HelloRequest.Parser.ParseFrom(payload.Value);
                case Container.Types.MessageType.HelloResult:
                    return HelloResult.Parser.ParseFrom(payload.Value);
                case Container.Types.MessageType.ByeRequest:
                    return ByeRequest.Parser.ParseFrom(payload.Value);
                case Container.Types.MessageType.UpdateProfileRequest:
                    return UpdateProfileRequest.Parser.ParseFrom(payload.Value);
                case Container.Types.MessageType.JoinNotification:
                    return JoinNotification.Parser.ParseFrom(payload.Value);
                case Container.Types.MessageType.LeaveNotification:
                    return LeaveNotification.Parser.ParseFrom(payload.Value);
                case Container.Types.MessageType.UpdateProfileNotification:
                    return UpdateProfileNotification.Parser.ParseFrom(payload.Value);
                case Container.Types.MessageType.BinaryMessage:
                    return BinaryMessage.Parser.ParseFrom(payload.Value);
                case Container.Types.MessageType.Error:
                    return Error.Parser.ParseFrom(payload.Value);
                default:
                    throw new TypeAccessException("unknown message type.");
            }
        }

        public Tuple<T, uint, uint> UnpackAndParse<T>(byte[] data) where T: IMessage
        {
            var (messageType, payload, sequenceNumber, lifeTimeMilliSeconds) = Unpack(data);
            return new Tuple<T, uint, uint>(
                (T)Parse(
                    messageType,
                    payload
                ), 
                sequenceNumber, 
                lifeTimeMilliSeconds
            );
        }
    }
}