#if UNITY_2021_3_OR_NEWER
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gs2.Realtime.Sync
{
    /// <summary>
    /// A request the room answers once: pending (Status default) until it completes. Wait
    /// for it in a coroutine (yield return request) or with OnCompleted.
    /// </summary>
    public abstract class NetworkRequest<TSelf, TStatus> : CustomYieldInstruction
        where TSelf : NetworkRequest<TSelf, TStatus>
        where TStatus : struct, Enum
    {
        Action<TSelf> completed;
        protected NetworkRequest(uint id)
        {
            Id = id;
        }
        /// <summary>The request ID (0 when it was not sent).</summary>
        public uint Id { get; }
        /// <summary>The outcome; the default value while pending.</summary>
        public TStatus Status { get; private set; }
        /// <summary>True once the request completed.</summary>
        public bool IsDone => !EqualityComparer<TStatus>.Default.Equals(Status, default);
        public override bool keepWaiting => !IsDone;
        /// <summary>Runs callback once the request completes, at once if it already has.</summary>
        public void OnCompleted(Action<TSelf> callback)
        {
            if (callback == null)
                throw new ArgumentNullException(nameof(callback));
            if (IsDone)
                callback((TSelf)this);
            else
                completed += callback;
        }
        /// <summary>
        /// Completes the request once; false when it already was. apply sets the derived
        /// request's results before the callbacks run.
        /// </summary>
        protected bool Finish(TStatus status, Action apply)
        {
            if (IsDone)
                return false;
            apply?.Invoke();
            Status = status;
            var callbacks = completed;
            completed = null;
            callbacks?.Invoke((TSelf)this);
            return true;
        }
    }
}
#endif
