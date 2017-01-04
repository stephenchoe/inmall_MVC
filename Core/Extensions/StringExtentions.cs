using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class StringExtentions
    {
        public static IList<string> GetKeywords(this string input)
        {
            if (String.IsNullOrWhiteSpace(input) || String.IsNullOrEmpty(input)) return null;

            return input.Split(new string[] { "-", " " }, StringSplitOptions.RemoveEmptyEntries);

        }
        public static string RemoveScriptTags(this string htmlString)
        {

            //移除  javascript code.
            htmlString = Regex.Replace(htmlString, @"<script[\d\D]*?>[\d\D]*?</script>", String.Empty);

            return htmlString;


        }
        public static string RemoveHtmlTags(this string htmlString)
        {
            //移除html tag.
            if (String.IsNullOrEmpty(htmlString)) return htmlString;
            var regexCss = new Regex("(\\<script(.+?)\\</script\\>)|(\\<style(.+?)\\</style\\>)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            htmlString = regexCss.Replace(htmlString, string.Empty);

            if (String.IsNullOrEmpty(htmlString)) return htmlString;
            string htmlTagPattern = @"<[^>]+>|&nbsp;";
            htmlString = Regex.Replace(htmlString, htmlTagPattern, string.Empty);

            if (String.IsNullOrEmpty(htmlString)) return htmlString;
            htmlString = Regex.Replace(htmlString, @"\s{2,}", " ");

            if (String.IsNullOrEmpty(htmlString)) return htmlString;
            htmlString = Regex.Replace(htmlString, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);


            return htmlString;
            


            //htmlString = Regex.Replace(htmlString, @"<[^>]*>", String.Empty);

            //return htmlString;
        }
        public static string RemoveSciptAndHtmlTags(this string htmlString)
        {
            htmlString = RemoveScriptTags(htmlString);

            return RemoveHtmlTags(htmlString);
        }
        public static string ReverseString(this string str)
        {
            return String.IsNullOrEmpty(str) ? string.Empty : new string(str.ToCharArray().Reverse().ToArray());
        }
        public static int ToInt(this string str)
        {
            int value = 0;
            if (!int.TryParse(str, out value)) value = 0;

            return value;
        }
    }
}
