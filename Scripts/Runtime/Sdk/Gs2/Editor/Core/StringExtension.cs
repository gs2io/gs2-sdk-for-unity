using System.Linq;

namespace Gs2.Core.Editor
{
    public static class StringExtension
    {
        public static bool In(this string s, string[] items)
        {
            return items.Count(item => item == s) > 0;
        }
    }
}