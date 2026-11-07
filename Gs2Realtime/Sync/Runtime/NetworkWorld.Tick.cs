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
        readonly int[] inputValues = new int[32];
        uint inputMask;
        bool clockStarted, leadMeasured;
        double tickPhase;
        float tickScale = 1;
        int pausedTicks, settlingClocks;
        float nextSendWarning;

        // True once a ticking room's clock arrived.
        public bool IsTicking => Client != null && Client.TickRate != 0 && clockStarted;
        // Simulation steps per second of the room (0 when it is not ticking).
        public int TickRate => (int)(Client?.TickRate ?? 0);
        // The step the room completed last, as of its latest clock.
        public uint ServerTick { get; private set; }
        // The tick of the next input; runs ahead of ServerTick.
        public uint InputTick { get; private set; }
        // Target of InputLead: how many ticks early inputs should arrive (1..10).
        public int InputLeadTicks
        {
            get => inputLeadTicks;
            set => inputLeadTicks = Mathf.Clamp(value, 1, 10);
        }
        // Measured lead: ticks between the room's tick and the newest input it had
        // received from this client (smoothed). Approaches InputLeadTicks.
        public float InputLead { get; private set; }
        public float ViewDelay
        {
            get => viewDelay;
            set => viewDelay = Mathf.Max(0, value);
        }
        // The (fractional) room tick of the remote objects on screen: ServerTick moved
        // back by ViewDelay when the world receives server timestamps. Each input carries
        // InputTick - ViewTick so that the room can rewind hit tests to this view.
        public double ViewTick
        {
            get
            {
                if (double.IsNaN(clockServerTime) || !HasServerTime || Client == null || Client.TickRate == 0)
                    return ServerTick;
                return ServerTick + (ServerTime - viewDelay - clockServerTime) * Client.TickRate;
            }
        }
        // Raised before the input of each tick is sampled (argument: its tick), for
        // games that set inputs per tick rather than per frame.
        public event Action<uint> InputSampling;

        // ---- Input ----
        // The input is a held state of up to 32 integer slots, sampled every tick while
        // this client owns a simulated object. Room rules read it with Room.Input.
        public void SetInput(int slot, int value)
        {
            if (slot < 0 || slot >= 32)
                throw new ArgumentOutOfRangeException(nameof(slot));
            inputValues[slot] = value;
            inputMask |= 1u << slot;
        }
        public void SetInput(int slot, bool value)
        {
            SetInput(slot, value ? 1 : 0);
        }
        // Quantized like synchronized float fields: value / precision, rounded.
        public void SetInput(int slot, float value, float precision)
        {
            SetInput(slot, SyncCodec.Quantize(value, precision));
        }
        // The value currently held in an input slot.
        public int GetInput(int slot)
        {
            if (slot < 0 || slot >= 32)
                throw new ArgumentOutOfRangeException(nameof(slot));
            return inputValues[slot];
        }
        // Releases every input slot (all read as 0 by the room).
        public void ClearInput()
        {
            inputMask = 0;
            Array.Clear(inputValues, 0, 32);
        }

        // ---- Clock ----
        void ResetClock()
        {
            clockStarted = leadMeasured = false;
            tickPhase = 0;
            tickScale = 1;
            pausedTicks = settlingClocks = 0;
            InputLead = 0;
            ServerTick = InputTick = 0;
            clockServerTime = double.NaN;
            ResetPrediction();
        }
        void OnClock(uint tick, uint rate, uint latest)
        {
            // Clocks travel unreliably: one overtaken by a newer clock is stale.
            if (clockStarted && unchecked((int)(tick - ServerTick)) < 0)
                return;
            clockServerTime = ReceivedServerTime;
            ServerTick = tick;
            Confirm(tick);
            if (!clockStarted)
            {
                // Start a little ahead; the measured lead corrects it within a second.
                InputTick = tick + (uint)(inputLeadTicks + Mathf.CeilToInt(rate * .1f));
                clockStarted = true;
                return;
            }
            if (unchecked((int)(InputTick - tick)) <= 0)
            {
                // Behind the room itself (e.g. after long frames while no input was sent):
                // every input would arrive too late. Restart ahead as on the first clock.
                InputTick = tick + (uint)(inputLeadTicks + Mathf.CeilToInt(rate * .1f));
                pausedTicks = 0;
                leadMeasured = false;
                settlingClocks = (int)rate / 2;
                tickScale = 1;
                return;
            }
            if (latest == 0)
                return; // no input received yet
            if (settlingClocks > 0)
            {
                // Clocks sent before the room saw a jump would undo it.
                settlingClocks--;
                return;
            }
            float lead = unchecked((int)(latest - tick));
            InputLead = leadMeasured ? InputLead + (lead - InputLead) * .1f : lead;
            leadMeasured = true;
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
                leadMeasured = false;
                settlingClocks = (int)rate / 2;
                tickScale = 1;
                return;
            }
            // Otherwise run up to 10% faster or slower until the lead matches.
            tickScale = 1 + Mathf.Clamp(error * .02f, -.1f, .1f);
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
                if (OwnsSimulatedObject() && !Client.SendInput(InputTick, inputMask, inputValues, ViewLag()) && Time.unscaledTime >= nextSendWarning)
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
                if (o.Mode == ObjectMode.Simulated && o.Owner == Client.ConnectionId)
                    return true;
            return false;
        }
    }
}
#endif
