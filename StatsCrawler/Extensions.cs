using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace StatsCrawler
{
    public static class Extensions
    {
        public static string StripHTML(this string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty).Replace("&nbsp;",string.Empty).Trim();
        }
    }

}
