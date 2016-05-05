using NExtensions;

namespace Kiehl.App.Business.Utility
{
    public static class StringExtensions
    {
        public static string TrimmedOrNull(this string item)
        {
            return string.IsNullOrWhiteSpace(item) ? null : item.Trim();
        }

        public static int? ToNullableInt32(this string item)
        {
            int i;
            if (int.TryParse(item, out i)) return i;
            return null;
        }

        public static decimal? ToNullableDecimal(this string item)
        {
            decimal i;
            if (decimal.TryParse(item, out i)) return i;
            return null;
        }

        public static string SSNMask(this string item)
        {
            return "XXX-XX-{0}".FormatWith(item.Substring(item.Length - 4, 4));
        }
    }
}
