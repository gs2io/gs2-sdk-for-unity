#if UNITY_2021_3_OR_NEWER
using System;
using UnityEngine;
namespace Gs2.Realtime.Sync
{
    // Ticking rooms (Room.DefineTickRate in the room rules) run a fixed-rate simulation:
    // each tick the rules read every member's input and write the simulated objects.
    // This client stamps its input with the tick the room should apply it at
    // (InputTick) and keeps that tick slightly ahead of the room, so that inputs arrive
    // InputLeadTicks before they are needed. Simulated objects themselves are shown
    // from the room's state, interpolated like other remote objects.
    public sealed partial class NetworkWorld
    {
        [SerializeField, Range(1, 10), Tooltip("Ticks by which inputs should reach the room before it simulates them.")]
        int inputLeadTicks = 2;
        [SerializeField, Min(0), Tooltip("Seconds remote objects are shown behind the room; keep equal to NetworkTransform.InterpolationDelay so that lag-compensated hit tests match the screen.")]
        float viewDelay = .1f;
        double clockServerTime = double.NaN;
        readonly SlotValues input = new SlotValues();
        // Following the room's clock: Stopped until the first clock; Settling ignores the
        // clocks sent before the room saw a jump of InputTick; Measuring takes the next
        // lead as it is; Following smooths the lead and steers InputTick towards it.
        enum ClockPhase : byte { Stopped, Settling, Measuring, Following }
        ClockPhase clockPhase;
        int settlingClocks;
        double tickPhase;
        float tickScale = 1;
        int pausedTicks; // ticks not sampled, so that InputTick falls back
        float nextSendWarning;

        /// <summary>
        /// True once the room's first clock arrived, for rooms with a tick rate
        /// (Room.DefineTickRate in the room rules).
        /// </summary>
        public bool IsTicking => Client != null && Client.TickRate != 0 && clockStarted;
        /// <summary>Simulation steps per second of the room (0 when it is not ticking).</summary>
        public int TickRate => (int)(Client?.TickRate ?? 0);
        /// <summary>The step the room completed last, as of its latest clock.</summary>
        public uint ServerTick { get; private set; }
        /// <summary>
        /// The tick inputs are stamped with; runs ahead of ServerTick so that inputs arrive
        /// about InputLeadTicks before the room simulates them.
        /// </summary>
        public uint InputTick { get; private set; }
        /// <summary>Target of InputLead: how many ticks early inputs should arrive (1-10).</summary>
        public int InputLeadTicks
        {
            get => inputLeadTicks;
            set => inputLeadTicks = Mathf.Clamp(value, 1, 10);
        }
        /// <summary>
        /// Measured lead: ticks between the room's tick and the newest input it had received
        /// from this client (smoothed). Approaches InputLeadTicks.
        /// </summary>
        public float InputLead { get; private set; }
        /// <summary>
        /// Seconds remote objects are shown behind the room. Keep it equal to
        /// NetworkTransform.InterpolationDelay so that lag-compensated hit tests match the screen.
        /// </summary>
        public float ViewDelay
        {
            get => viewDelay;
            set => viewDelay = Mathf.Max(0, value);
        }
        /// <summary>
        /// The (fractional) room tick of the remote objects on screen: ServerTick moved back by
        /// ViewDelay when the world receives server timestamps. Each input carries
        /// InputTick - ViewTick so that the room can rewind hit tests to this view.
        /// </summary>
        public double ViewTick
        {
            get
            {
                bool timed = !double.IsNaN(clockServerTime) && HasServerTime;
                if (!timed || TickRate == 0)
                    return ServerTick;
                return ServerTick + (ServerTime - viewDelay - clockServerTime) * Client.TickRate;
            }
        }
        /// <summary>
        /// Raised with the tick before its input is sampled, for games that set inputs per
        /// tick rather than per frame. Runs from Update.
        /// </summary>
        public event Action<uint> InputSampling;

        // ---- Input ----
        /// <summary>
        /// Sets an input slot. The input is a held state of up to 32 integer slots, sampled every
        /// tick and sent while this client owns a simulated object. Room rules read it with
        /// Room.Input.
        /// </summary>
        public void SetInput(int slot, int value)
        {
            input.Set(slot, value);
        }
        /// <summary>Sets an input slot to 1 (true) or 0 (false).</summary>
        public void SetInput(int slot, bool value)
        {
            SetInput(slot, value ? 1 : 0);
        }
        /// <summary>
        /// Sets an input slot quantized like synchronized float fields: value / precision, rounded.
        /// </summary>
        public void SetInput(int slot, float value, float precision)
        {
            SetInput(slot, SyncCodec.Quantize(value, precision));
        }
        /// <summary>The value currently held in an input slot.</summary>
        public int GetInput(int slot)
        {
            return input[slot];
        }
        /// <summary>Releases every input slot (all read as 0 by the room).</summary>
        public void ClearInput()
        {
            input.Clear();
        }

        // ---- Clock ----
        void ResetClock()
        {
            clockPhase = ClockPhase.Stopped;
            tickPhase = 0;
            tickScale = 1;
            pausedTicks = settlingClocks = 0;
            InputLead = 0;
            ServerTick = InputTick = 0;
            clockServerTime = double.NaN;
            ResetPrediction();
        }
        bool clockStarted => clockPhase != ClockPhase.Stopped;
        void OnClock(uint tick, uint rate, uint latest)
        {
            // Clocks travel unreliably: one overtaken by a newer clock is stale.
            bool overtaken = clockStarted && Serial.Before(tick, ServerTick);
            if (overtaken)
                return;
            clockServerTime = ReceivedServerTime;
            ServerTick = tick;
            Confirm(tick, Client.ClockState);
            // Start a little ahead; the measured lead corrects it within a second.
            uint start = tick + (uint)(inputLeadTicks + Mathf.CeilToInt(rate * .1f));
            if (clockPhase == ClockPhase.Stopped)
            {
                InputTick = start;
                clockPhase = ClockPhase.Measuring;
                return;
            }
            bool behindTheRoom = !Serial.After(InputTick, tick);
            if (behindTheRoom)
            {
                // Behind the room itself (e.g. after long frames while no input was sent):
                // every input would arrive too late. Restart ahead.
                InputTick = start;
                pausedTicks = 0;
                Settle(rate);
                return;
            }
            if (latest == 0)
                return; // no input received yet
            if (clockPhase == ClockPhase.Settling)
            {
                if (--settlingClocks <= 0)
                    clockPhase = ClockPhase.Measuring;
                return;
            }
            float lead = Serial.Diff(latest, tick);
            InputLead = clockPhase == ClockPhase.Following ? InputLead + (lead - InputLead) * .1f : lead;
            clockPhase = ClockPhase.Following;
            float error = inputLeadTicks - InputLead;
            if (Mathf.Abs(error) > rate * .5f)
            {
                // More than half a second off (a stall or a latency change): jump. Inputs
                // only move forward, so falling back means skipping tick sampling.
                int ticks = Mathf.RoundToInt(error);
                if (ticks > 0)
                    InputTick += (uint)ticks;
                else
                    pausedTicks = -ticks;
                Settle(rate);
                return;
            }
            // Otherwise run up to 10% faster or slower until the lead matches.
            tickScale = 1 + Mathf.Clamp(error * .02f, -.1f, .1f);
        }
        // After InputTick jumped: ignore the clocks of half a second, then measure afresh.
        void Settle(uint rate)
        {
            settlingClocks = (int)rate / 2;
            clockPhase = settlingClocks > 0 ? ClockPhase.Settling : ClockPhase.Measuring;
            tickScale = 1;
        }
        void ServiceTick()
        {
            if (!clockStarted || Client.TickRate == 0)
                return;
            tickPhase += Time.unscaledDeltaTime * Client.TickRate * tickScale;
            for (int steps = 0; tickPhase >= 1; steps++)
            {
                if (steps == 4)
                {
                    tickPhase = 0; // a long frame: the clock feedback recovers the lead
                    break;
                }
                tickPhase -= 1;
                if (pausedTicks > 0)
                {
                    pausedTicks--;
                    continue;
                }
                InputTick++;
                try
                {
                    InputSampling?.Invoke(InputTick);
                }
                catch (Exception e)
                {
                    Debug.LogException(e, this); // the tick's input is still recorded and sent
                }
                RecordInput(InputTick);
                bool sent = !OwnsSimulatedObject() || Client.SendInput(InputTick, input, ViewLag());
                if (!sent && Time.unscaledTime >= nextSendWarning)
                {
                    nextSendWarning = Time.unscaledTime + 1;
                    Debug.LogWarning("Input for tick " + InputTick + " was not sent (transport busy)", this);
                }
            }
        }
        // InputTick - ViewTick in 1/16 ticks, at most one second (the room clamps further).
        uint ViewLag()
        {
            double lag = (InputTick - ViewTick) * 16;
            return (uint)Math.Round(Math.Max(0, Math.Min(lag, Client.TickRate * 16)));
        }
        bool OwnsSimulatedObject()
        {
            foreach (var o in Client.Objects)
                if (o.SimulatedForMe)
                    return true;
            return false;
        }
    }
}
#endif
