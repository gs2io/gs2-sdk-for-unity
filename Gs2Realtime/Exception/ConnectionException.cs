using System;
using Gs2.Core.Exception;
using Gs2.Gs2Realtime.Message;
using UnityEngine.Scripting;

namespace Gs2.Unity.Gs2Realtime.Exception
{
    /// <summary>
    /// RealtimeSession.Connect failed. StatusCode is the room's status code, or 0 when the room
    /// did not answer.
    /// </summary>
    [Preserve]
    public class ConnectionException : Gs2Exception
    {
        public EventArgs Args;
        /// <summary>
        /// The room's answer when it refused the connection (an invalid access token, for
        /// example); null when the connection itself failed (unreachable, timed out).
        /// </summary>
        public Error ServerError { get; }

        public ConnectionException(EventArgs args) : this(args, null)
        {
        }

        public ConnectionException(EventArgs args, Error serverError)
            : base(serverError == null ? "ConnectionException" :
                "ConnectionException: " + serverError.StatusCode + " " + serverError.Component + " " + serverError.Message)
        {
            Args = args;
            ServerError = serverError;
        }

        public override bool RecommendRetry => false;
        public override bool RecommendAutoRetry => false;

        public override int StatusCode => (int)(ServerError?.StatusCode ?? 0);
    }
}
