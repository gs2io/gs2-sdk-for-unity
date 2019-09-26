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
                Payload = Any.Pack(payload),
                Signature = ByteString.CopyFrom(
                    _sha256.ComputeHash(payload.ToByteArray())
                )
            }.ToByteArray();
        }

        public Tuple<Container.Types.MessageType, Any> Unpack(byte[] data)
        {
            var container = Container.Parser.ParseFrom(data);
            _sha256.Initialize();
            var hash = _sha256.ComputeHash(container.Payload.Value.ToByteArray());
            if (!StructuralComparisons.StructuralEqualityComparer.Equals(hash, container.Signature.ToByteArray()))
            {
                throw new InvalidDataException("invalid signature.");
            }
            return new Tuple<Container.Types.MessageType, Any>(
                container.MessageType,
                container.Payload
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
                    return payload.Unpack<HelloRequest>();
                case Container.Types.MessageType.HelloResult:
                    return payload.Unpack<HelloResult>();
                case Container.Types.MessageType.ByeRequest:
                    return payload.Unpack<ByeRequest>();
                case Container.Types.MessageType.UpdateProfileRequest:
                    return payload.Unpack<UpdateProfileRequest>();
                case Container.Types.MessageType.JoinNotification:
                    return payload.Unpack<JoinNotification>();
                case Container.Types.MessageType.LeaveNotification:
                    return payload.Unpack<LeaveNotification>();
                case Container.Types.MessageType.UpdateProfileNotification:
                    return payload.Unpack<UpdateProfileNotification>();
                case Container.Types.MessageType.BinaryMessage:
                    return payload.Unpack<BinaryMessage>();
                case Container.Types.MessageType.Error:
                    return payload.Unpack<Error>();
                default:
                    throw new TypeAccessException("unknown message type.");
            }
        }

        public T UnpackAndParse<T>(byte[] data) where T: IMessage
        {
            var (messageType, payload) = Unpack(data);
            return (T)Parse(
                messageType,
                payload
            );
        }
    }
}