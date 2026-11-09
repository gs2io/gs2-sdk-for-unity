#if UNITY_2021_3_OR_NEWER
using System;
namespace Gs2.Realtime.Sync
{
    /// <summary>
    /// The room rules' RoomMath, for prediction code generated from [Predict] methods.
    /// The room evaluates the same functions in Lua with the same IEEE double operations,
    /// so both compute the same int32; a result outside int32 throws OverflowException.
    /// </summary>
    public static class RoomMath
    {
        /// <summary>
        /// value limited to min..max. Throws ArgumentException when min exceeds max.
        /// </summary>
        public static int Clamp(int value, int min, int max)
        {
            if (min > max)
                throw new ArgumentException("RoomMath.Clamp: min exceeds max");
            return value < min ? min : value > max ? max : value;
        }
        public static int Abs(int value)
        {
            return checked(value < 0 ? -value : value);
        }
        public static int Sign(int value)
        {
            return value < 0 ? -1 : value > 0 ? 1 : 0;
        }
        /// <summary>Square root rounded down. Throws for a negative value.</summary>
        public static int Sqrt(int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "RoomMath.Sqrt of a negative value");
            return (int)Math.Floor(Math.Sqrt(value));
        }
        // Each product is its own statement, so no compiler fuses it into the sum (FMA),
        // which would round differently from the room.
        /// <summary>Length of the vector (x, y), rounded down.</summary>
        public static int Length(int x, int y)
        {
            double xx = (double)x * x;
            double yy = (double)y * y;
            double sum = xx + yy;
            return checked((int)Math.Floor(Math.Sqrt(sum)));
        }
        /// <summary>Length of the vector (x, y, z), rounded down.</summary>
        public static int Length(int x, int y, int z)
        {
            double xx = (double)x * x;
            double yy = (double)y * y;
            double zz = (double)z * z;
            double sum = xx + yy;
            sum += zz;
            return checked((int)Math.Floor(Math.Sqrt(sum)));
        }
        /// <summary>
        /// value * numerator / denominator with the exact (64-bit) product, truncated toward zero.
        /// </summary>
        public static int MulDiv(int value, int numerator, int denominator)
        {
            if (denominator == 0)
                throw new DivideByZeroException("RoomMath.MulDiv: division by zero");
            return checked((int)((long)value * numerator / denominator));
        }
    }
}
#endif
