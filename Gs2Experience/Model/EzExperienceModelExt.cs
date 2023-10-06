using System;
using System.Linq;

namespace Gs2.Unity.Gs2Experience.Model
{
    public static class EzExperienceModelExt
    {
        public static long Rank(this EzExperienceModel self, EzStatus status) {
            return Math.Min(self.RankThreshold.Values.Count(v => v <= status.ExperienceValue) + 1, status.RankCapValue);
        }

        public static long NextRankExperienceValue(this EzExperienceModel self, EzStatus status) {
            var newRank = self.Rank(status);
            if (newRank == status.RankCapValue) {
                return 0;
            }
            return self.RankThreshold.Values[(int)Math.Min(newRank, status.RankCapValue)-1];
        }
    }
}