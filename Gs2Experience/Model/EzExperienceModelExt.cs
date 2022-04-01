namespace Gs2.Unity.Gs2Experience.Model
{
    public static class EzExperienceModelExt
    {
        public static long? NextRankUpExperienceValue(this EzExperienceModel self, long rank, long rankCap)
        {
            if (rank < 1)
            {
                return 0;
            }
            if (rank >= rankCap)
            {
                return null;
            }
            if (self.RankThreshold.Values.Count > rank-1)
            {
                return self.RankThreshold.Values[(int) (rank - 1)];
            }
            return null;
        }
    }
}