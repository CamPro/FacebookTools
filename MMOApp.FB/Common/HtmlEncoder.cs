using KSS.Patterns.Extensions;
using KSS.Patterns.Web;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace KSS.Patterns.Encode
{
    public static class HtmlEncoder
    {
        private static readonly string[] chars = new[] { " ", "!", "\"", "#", "$", "%", "&", "'", "(", ")", "*", "+", ",", "-", ".", "/", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ":", ";", "<", "=", ">", "?", "@", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "[", @"\", "]", "^", "_", "`", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "{", "|", "}", "~", " ", "€", " ", "‚", "ƒ", "„", "…", "†", "‡", "ˆ", "‰", "Š", "‹", "Œ", " ", "Ž", " ", " ", "‘", "’", "“", "”", "•", "–", "—", "™", "š", "›", "œ", " ", "ž", "Ÿ", " ", "¡", "¢", "£", " ", "¥", "|", "§", "¨", "©", "ª", "«", "¬", "¯", "®", "¯", "°", "±", "²", "³", "´", "µ", "¶", "·", "¸", "¹", "º", "»", "¼", "½", "¾", "¿", "À", "Á", "Â", "Ã", "Ä", "Å", "Æ", "Ç", "È", "É", "Ê", "Ë", "Ì", "Í", "Î", "Ï", "Ð", "Ñ", "Ò", "Ó", "Ô", "Õ", "Ö", " ", "Ø", "Ù", "Ú", "Û", "Ü", "Ý", "Þ", "ß", "à", "á", "â", "ã", "ä", "å", "æ", "ç", "è", "é", "ê", "ë", "ì", "í", "î", "ï", "ð", "ñ", "ò", "ó", "ô", "õ", "ö", "÷", "ø", "ù", "ú", "û", "ü", "ý", "þ", "ÿ" };
        private static readonly string[] encoded = new[] { "%20", "%21", "%22", "%23", "%24", "%25", "%26", "%27", "%28", "%29", "%2A", "%2B", "%2C", "%2D", "%2E", "%2F", "%30", "%31", "%32", "%33", "%34", "%35", "%36", "%37", "%38", "%39", "%3A", "%3B", "%3C", "%3D", "%3E", "%3F", "%40", "%41", "%42", "%43", "%44", "%45", "%46", "%47", "%48", "%49", "%4A", "%4B", "%4C", "%4D", "%4E", "%4F", "%50", "%51", "%52", "%53", "%54", "%55", "%56", "%57", "%58", "%59", "%5A", "%5B", "%5C", "%5D", "%5E", "%5F", "%60", "%61", "%62", "%63", "%64", "%65", "%66", "%67", "%68", "%69", "%6A", "%6B", "%6C", "%6D", "%6E", "%6F", "%70", "%71", "%72", "%73", "%74", "%75", "%76", "%77", "%78", "%79", "%7A", "%7B", "%7C", "%7D", "%7E", "%7F", "%80", "%81", "%82", "%83", "%84", "%85", "%86", "%87", "%88", "%89", "%8A", "%8B", "%8C", "%8D", "%8E", "%8F", "%90", "%91", "%92", "%93", "%94", "%95", "%96", "%97", "%98", "%99", "%9A", "%9B", "%9C", "%9D", "%9E", "%9F", "%A0", "%A1", "%A2", "%A3", "%A4", "%A5", "%A6", "%A7", "%A8", "%A9", "%AA", "%AB", "%AC", "%AD", "%AE", "%AF", "%B0", "%B1", "%B2", "%B3", "%B4", "%B5", "%B6", "%B7", "%B8", "%B9", "%BA", "%BB", "%BC", "%BD", "%BE", "%BF", "%C0", "%C1", "%C2", "%C3", "%C4", "%C5", "%C6", "%C7", "%C8", "%C9", "%CA", "%CB", "%CC", "%CD", "%CE", "%CF", "%D0", "%D1", "%D2", "%D3", "%D4", "%D5", "%D6", "%D8", "%D9", "%DA", "%DB", "%DC", "%DD", "%DE", "%DF", "%E0", "%E1", "%E2", "%E3", "%E4", "%E5", "%E6", "%E7", "%E8", "%E9", "%EA", "%EB", "%EC", "%ED", "%EE", "%EF", "%F0", "%F1", "%F2", "%F3", "%F4", "%F5", "%F6", "%F7", "%F8", "%F9", "%FA", "%FB", "%FC", "%FD", "%FE", "%FF" };

        public static string EncodeSymbol(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            List<string> symbols = new List<string>();
            List<string> encodedSymbols = new List<string>();

            for (int i = 0; i < chars.Length; i++)
            {
                if (!IsNumber(chars[i]) && !IsChar(chars[i]))
                {
                    symbols.Add(chars[i]);
                    encodedSymbols.Add(encoded[i]);
                }
            }

            return Encode(text, symbols, encodedSymbols);
        }

        private static bool IsNumber(string c)
        {
            if (string.IsNullOrEmpty(c))
                return false;

            return '0' <= c[0] && c[0] <= '9';
        }

        private static bool IsChar(string c)
        {
            if (string.IsNullOrEmpty(c))
                return false;

            char ch = c[0];

            return ('a' <= ch && ch <= 'z') || ('A' <= ch && ch <= 'Z');
        }

        public static string Encode(string text)
        {
            return Encode(text, new List<string>(chars), new List<string>(encoded));
        }

        private static string Encode(string text, List<string> originals, List<string> encodedString)
        {
            StringBuilder sbText = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];

                int index = originals.FindIndex(s => s[0] == c);
                if (index >= 0)
                {
                    sbText.Append(encodedString[index]);
                }
                else
                {
                    sbText.Append(c);
                }
            }

            return sbText.ToString();
        }

        public static string Decode(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            for (int i = 0; i < encoded.Length; i++)
            {
                text = text.Replace(encoded[i], chars[i]);
            }

            return text;
        }

        public static string DecodeEncodedNonAsciiCharacters(string value)
        {
            var html = value;
          
            return Regex.Replace(
                html,
                @"\\u(?<Value>[a-zA-Z0-9]{4})",
                m => ((char)int.Parse(m.Groups["Value"].Value, NumberStyles.HexNumber)).ToString());
        }

        public static string DecodeSlash(string html)
        {
            do
            {
                var start = html;

                html = html.Replace("\\/", "/");
                html = html.Replace("\\\"", "\"");
                html = html.Replace("///", "/");
                html = html.Replace("\\\\u", "\\u");
                html = html.Remove("\\n");

                if (start == html)
                    break;

            } while (true);

            return html;
        }

        public static string DecodeHtmlString(string html)
        {
            html = Domainer.GetRawUrl(html);
            html = DecodeEncodedNonAsciiCharacters(html);
            html = Domainer.GetRawUrl(html);
            html = DecodeEncodedNonAsciiCharacters(html);

            html = html.Replace("\\/", "/");
            html = html.Replace("\\\"", "\"");
            html = html.Remove("\\n");

            return html;
        }
    }
}