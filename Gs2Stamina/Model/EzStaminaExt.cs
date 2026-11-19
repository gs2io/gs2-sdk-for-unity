using System;

namespace Gs2.Unity.Gs2Stamina.Model
{
    public static class EzStaminaExt
    {
        /// <summary>
        /// Predicts natural recovery at a Unix timestamp in milliseconds on the
        /// session's clock (UTC plus AccessToken.TimeOffset in seconds).
        /// Does not mutate the source or include OverflowValue in Value.
        /// Returns the source when unchanged, or null for a null source.
        /// </summary>
        public static EzStamina PredictRecovery(this EzStamina self, long nowUnixMs)
        {
            if (self.NextRecoveryAt() == 0 || nowUnixMs < self.NextRecoverAt) {
                return self;
            }
            var intervalMs = self.RecoverIntervalMinutes * 60000L;
            var steps = 1 + (nowUnixMs - self.NextRecoverAt) / intervalMs;
            if (self.RecoverValue > 0) {
                var stepsToFull = ((long)self.MaxValue - self.Value + self.RecoverValue - 1) / self.RecoverValue;
                steps = Math.Min(steps, stepsToFull);
            }
            var value = (int)Math.Min((long)self.MaxValue, self.Value + steps * self.RecoverValue);
            var nextRecoverAt = value >= self.MaxValue ? 0 : self.NextRecoverAt + steps * intervalMs;
            // Copy through the model so all fields, including overflow, survive prediction.
            var predicted = EzStamina.FromModel(self.ToModel());
            predicted.Value = value;
            predicted.NextRecoverAt = nextRecoverAt;
            return predicted;
        }

        /// <summary>
        /// Returns the next scheduled recovery time, or 0 when recovery is disabled
        /// or the stamina is full. A zero recovery amount still advances its schedule.
        /// </summary>
        public static long NextRecoveryAt(this EzStamina self)
        {
            return self == null || self.NextRecoverAt <= 0 || self.Value >= self.MaxValue || self.RecoverIntervalMinutes <= 0
                ? 0
                : self.NextRecoverAt;
        }
    }
}
