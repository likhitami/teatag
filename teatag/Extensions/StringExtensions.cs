using System.Text.RegularExpressions;

namespace teatag.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Clean any unwanted charector eg. Zero Width Space.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Clean(this string text)
        {
            return text.Clean("", "");
        }

        /// <summary>
        /// Clean any unwanted charector eg. Zero Width Space.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="replaceString"></param>
        /// <returns></returns>
        public static string Clean(this string text, string replaceString)
        {
            return text.Clean(replaceString, "");
        }

        /// <summary>
        /// Clean any unwanted charector eg. Zero Width Space.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="replaceString"></param>
        /// <param name="defaultString"></param>
        /// <returns></returns>
        public static string Clean(this string text, string replaceString, string defaultString)
        {
            text = text?.Trim();
            if (string.IsNullOrEmpty(text))
                return defaultString;

            string cleaned = Regex.Replace(text, @"[\u0000-\u001F\u007F-\u00A0\u2000-\u206F]", replaceString);
            return cleaned;
        }
    }
}
