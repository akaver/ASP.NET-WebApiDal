using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Web.Helpers
{
    public static class StringHelper
    {
        public static string Nl2Br(this string str)
        {
            return Nl2Br(str, true);
        }

        public static string Nl2Br(this string str, bool isXhtml)
        {
            var brTag = "<br>";
            if (isXhtml)
            {
                brTag = "<br />";
            }
            return str.Replace("\r\n", brTag + "\r\n");
        }

        public static string HtmlToPlainText(this string html)
        {
            //http://stackoverflow.com/questions/286813/how-do-you-convert-html-to-plain-text

            const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";
                //matches one or more (white space or line breaks) between '>' and '<'
            const string stripFormatting = @"<[^>]*(>|$)";
                //match any character between '<' and '>', even when end tag is missing
            const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>"; //matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />

            var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
            var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
            var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);

            var text = html;

            text = text.Replace("<p>", "").Replace("</p>", Environment.NewLine);

            //Remove tag whitespace/line breaks
            text = tagWhiteSpaceRegex.Replace(text, "><");

            //Replace <br /> with line breaks
            text = lineBreakRegex.Replace(text, Environment.NewLine);

            //Strip formatting
            text = stripFormattingRegex.Replace(text, string.Empty);

            //Decode html specific characters
            text = System.Net.WebUtility.HtmlDecode(text);

            return text;
        }
    }
}