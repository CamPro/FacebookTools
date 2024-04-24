using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace KSS.Patterns.Extensions
{
    public static class StringExtensions
    {
        private static char[] numbers;

        public static int ToInt32(this string numberString, int defaultValue)
        {
            int value;
            return int.TryParse(numberString, out value) ? value : defaultValue;
        }
        public static Int64 ToInt64(this string numberString, Int64 defaultValue)
        {
            Int64 value;
            return Int64.TryParse(numberString, out value) ? value : defaultValue;
        }

        public static double ToDouble(this string numberString, double defautlValue)
        {
            double value;
            return double.TryParse(numberString, out value) ? value : defautlValue;
        }
        public static bool ToBoolean(this string boolString, bool defaultValue)
        {
            bool value;
            return bool.TryParse(boolString, out value) ? value : defaultValue;
        }
        public static int ToNumber(this string word)
        {
            return int.Parse(word);
        }
        public static DateTime ToDateTime(this string word)
        {
            return DateTime.Parse(word);
        }

        public static string ToBase64(this string text)
        {
            var data = Encoding.UTF8.GetBytes(text);
            var base64Text = Convert.ToBase64String(data);
            return base64Text;
        }
        public static string FromBase64(this string base64Text)
        {
            var data = Convert.FromBase64String(base64Text);
            var text = Encoding.UTF8.GetString(data);
            return text;
        }


        public static int GetWordCount(this string paragraph)
        {
            MatchCollection collection = Regex.Matches(paragraph, @"[\S]+");

            return collection.Count;
        }
        public static List<string> GetWords(this string content)
        {
            return content.Split(' ').Where(w => !w.IsNullOrWhiteSpace()).ToList();
        }

        public static bool IsOneWordNumber(this string word)
        {
            return !string.IsNullOrEmpty(word) && GetWordCount(word) == 1 && StartWithNumber(word);
        }
        public static bool StartWithNumber(this string word)
        {
            return !string.IsNullOrEmpty(word) && word[0].IsNumber();
        }
        public static bool ContainsNumber(this string word)
        {
            if (string.IsNullOrEmpty(word)) return false;

            InitNumbers();

            return word.IndexOfAny(numbers) >= 0;
        }

        private static void InitNumbers()
        {
            if (numbers == null)
            {
                List<char> numberChars = new List<char>();
                for (int i = '0'; i <= '9'; i++)
                {
                    numberChars.Add((char)i);
                }
                numbers = numberChars.ToArray();
            }
        }
        public static bool OnlyContainsNumber(this string word)
        {
            if (string.IsNullOrEmpty(word)) return false;

            InitNumbers();

            var allNumber = word.All(c => c.IsNumber());

            return allNumber;
        }
        public static bool IsNumber(this string word)
        {
            int value;
            return int.TryParse(word, out value);
        }

        public static bool Contains(this string content, string value, StringComparison comparison)
        {
            if (content == null) throw new ArgumentNullException("content");
            if (value == null) throw new ArgumentNullException("value");

            var idx = content.IndexOf(value, comparison);

            return (idx >= 0);
        }
        public static bool ContainsAny(this string content, IEnumerable<string> values)
        {
            return ContainsAny(content, values, StringComparison.InvariantCultureIgnoreCase);
        }
        public static bool ContainsAny(this string content, IEnumerable<string> values, StringComparison comparison)
        {
            var contains = values.Any(v => content.Contains(v, comparison));
            return contains;
        }
        public static bool ContainsAll(this string content, IEnumerable<string> values, StringComparison comparison)
        {
            var contains = values.All(v => content.Contains(v, comparison));
            return contains;
        }

        /// <summary>
        /// Determines whether the specified text contains any character outside a range.
        /// </summary>
        /// <param name="text">The text to check</param>
        /// <param name="characters">The range characters.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// content
        /// or
        /// characters
        /// </exception>
        public static bool ContainsOut(this string text, IEnumerable<char> characters)
        {
            if (text == null) throw new ArgumentNullException("text");
            if (characters == null) throw new ArgumentNullException("characters");

            var outside = text.Any(c => !characters.Contains(c));

            return outside;
        }

        public static string SuperTrim(this string word)
        {
            word = word.Trim();
            word = string.Join(" ", word.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

            return word;
        }

        public static string TrimAlsoNewLineTab(this string world)
        {
            if (world == null)
                return null;

            world = SuperTrim(world);

            int index = -1;
            for (int i = 0; i < world.Length; i++)
            {
                var c = world[i];
                if (c == '\r' || c == '\n' || c == '\t')
                    index = i;
                else
                    break;
            }

            if (index >= 0)
                world = world.SubstringToEnd(index + 1);


            index = -1;
            for (int i = world.Length - 1; i > 0; i--)
            {
                var c = world[i];
                if (c == '\r' || c == '\n' || c == '\t')
                    index = i;
                else
                    break;
            }

            if (index >= 0)
                world = world.Substring(0, index);

            return world;
        }

        public static string GetShort(this string text, int length)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            int len = Math.Min(length, text.Length);
            return text.Substring(0, len) + ((len < text.Length) ? "..." : string.Empty);
        }

        public static string[] GetLines(this string text)
        {
            text = text.Replace("\r", string.Empty);
            return text.Split('\n');
        }

        public static string Join(this string[] lines)
        {
            return Join(lines, string.Empty);
        }
        public static string Join(this string[] lines, string separator, bool joinLine = true)
        {
            if (lines == null || lines.Length <= 0)
                return string.Empty;

            StringBuilder text = new StringBuilder();
            for (int i = 0; i < lines.Length; i++)
            {
                if (joinLine)
                    text.AppendLine(lines[i]);
                else
                    text.Append(lines[i]);

                if (i < lines.Length - 1 && !string.IsNullOrEmpty(separator))
                {
                    if (joinLine)
                        text.AppendLine(separator);
                    else
                        text.Append(separator);
                }
            }

            return text.ToString();
        }

        public static string SubstringSlideUntilDifferent(this string content, int start)
        {
            int length = content.Length;

            if (string.IsNullOrWhiteSpace(content) || start >= length - 1)
                return string.Empty;

            char startChar = content[start];
            char nextChar = startChar;
            int index = start;
            while (nextChar == startChar)
            {
                index++;
                if (index >= length)
                    break;

                nextChar = content[index];
            }

            return index < length
                ? content.Substring(index, length - index)
                : string.Empty;
        }
        public static string SubstringToEnd(this string content, int start)
        {
            return content.Substring(start, content.Length - start);
        }
        public static int CountContaining(this string content, string value)
        {
            int count = 0;
            int index = -1;
            do
            {
                index++;
                index = content.IndexOf(value, index);
                if (index >= 0)
                    count++;
            } while (index > 0);

            return count;
        }

        public static string Reverse(this string content)
        {
            return new string(Enumerable.Reverse(content).ToArray());
        }

        public static string First(this string content, int length)
        {
            int count = Math.Min(content.Length, length);
            return content.Substring(0, count);
        }
        public static string Last(this string content, int length)
        {
            if (length <= 0)
                return string.Empty;

            int count = Math.Min(content.Length, length);
            return content.Substring(content.Length - count, length);
        }
        public static string GetRemaining(this string content, int from)
        {
            return Last(content, content.Length - from);
        }

        public static string Slide(this string content, string firstToken, string secondToken)
        {
            int index1 = content.IndexOf(firstToken, StringComparison.InvariantCultureIgnoreCase);
            if (index1 < 0)
                return string.Empty;

            index1 += firstToken.Length;
            int index2 = content.IndexOf(secondToken, index1, StringComparison.InvariantCultureIgnoreCase);
            if (index2 < 0)
                return string.Empty;

            return content.Substring(index1, index2 - index1);
        }

        public static string GetNumberString(this string content, int from)
        {
            int index = from;
            int length = content.Length;
            while (index < length && content[index].IsNumber())
            {
                index++;
            }

            string number = content.Substring(@from, index - @from);

            return number;
        }

        public static int IndexCount(this string content, string search, int count)
        {
            int index = -1;
            int start = 0;

            for (int i = 0; i < count; i++)
            {
                index = content.IndexOf(search, start);
                if (index < 0)
                    return index;

                if (i < count - 1)
                    start = index + search.Length;
            }

            return index;
        }
        public static int LastIndexCount(this string content, string search, int count = 1)
        {
            int index = content.Length;

            for (int i = 0; i < count; i++)
            {
                index = content.LastIndexOf(search, index - 1, index);
                if (index < 0)
                    return index;
            }

            return index;
        }

        public static bool IsNullOrEmpty(this string content)
        {
            return string.IsNullOrEmpty(content);
        }
        public static bool IsNullOrWhiteSpace(this string content)
        {
            return string.IsNullOrWhiteSpace(content);
        }
        public static bool NotNullOrWhiteSpace(this string content)
        {
            return !string.IsNullOrWhiteSpace(content);
        }

        public static string Remove(this string content, string removedString)
        {
            return content.Replace(removedString, string.Empty);
        }
        public static string Remove(this string content, string[] removedStrings)
        {
            return removedStrings.Aggregate(content, Remove);
        }
        public static string RemoveInvisibleChars(this string content)
        {
            var result = new string(content.Where(c => !char.IsControl(c)).ToArray());

            return result;
        }
        public static string RemoveNonTextOrNumberChars(this string content)
        {
            if (content == null)
                return null;

            var result = new string(content.Where(c => c.IsNumber() || c.IsText()).ToArray());

            return result;
        }
        public static string RemoveNonNumberChars(this string content)
        {
            if (content == null)
                return null;

            var result = new string(content.Where(c => c.IsNumber()).ToArray());

            return result;
        }
        public static string RemoveNewLine(this string content)
        {
            if (content == null)
                return null;

            return Remove(content, new[] { "\r", "\n" });
        }
        public static string RemoveSpace(this string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return string.Empty;

            while (text.IndexOf(" ") >= 0)
            {
                text = text.Replace(" ", string.Empty);
            }

            return text;
        }
        public static string RemoveFirstChars(this string text, int amount = 1)
        {
            if (string.IsNullOrWhiteSpace(text)) return string.Empty;

            int charsToRemove = Math.Min(text.Length, amount);
            var result = text.Substring(charsToRemove, text.Length - charsToRemove);

            return result;
        }
        public static string RemoveLastChars(this string text, int amount = 1)
        {
            if (string.IsNullOrWhiteSpace(text)) return string.Empty;

            int charsToRemove = Math.Min(text.Length, amount);
            var result = text.Substring(0, text.Length - charsToRemove);

            return result;
        }
        public static string RemoveNonLatinChars(this string text)
        {
            var sbText = new StringBuilder();
            foreach (var c in text)
            {
                if (c < 'a' || c > 'Z' || c != ' ' || !c.IsNumber())
                    continue;

                sbText.Append(c);
            }

            return sbText.ToString();
        }

        public static string Replace(this string content, string oldValue, string newValue, int startIndex)
        {
            string replace = content.Substring(startIndex + 1).Replace(oldValue, newValue);
            content = content.Remove(startIndex + 1) + replace;

            return content;
        }
        public static string Replace(this string content, string oldValue, string newValue, StringComparison comparison)
        {
            StringBuilder sb = new StringBuilder();

            int previousIndex = 0;
            int index = content.IndexOf(oldValue, comparison);
            while (index != -1)
            {
                sb.Append(content.Substring(previousIndex, index - previousIndex));
                sb.Append(newValue);
                index += oldValue.Length;

                previousIndex = index;
                index = content.IndexOf(oldValue, index, comparison);
            }
            sb.Append(content.Substring(previousIndex));

            return sb.ToString();
        }
        public static string UpperCaseFirstChar(this string content)
        {
            if (content.IsNullOrWhiteSpace())
                return content;

            var lower = content.ToLowerInvariant();
            var result = lower[0].ToString().ToUpperInvariant() + lower.Substring(1);

            return result;
        }
        public static string UpperCaseEachWord(this string content)
        {
            var parts = content.ToLowerInvariant().Split(' ');
            for (int i = 0; i < parts.Length; i++)
            {
                if (!parts[i].IsNullOrWhiteSpace())
                    parts[i] = UpperCaseFirstChar(parts[i]);
            }

            return String.Join(" ", parts);
        }

        public static string StripHtml(this string content)
        {
            if (content.IsNullOrWhiteSpace())
                return content;

            return Regex.Replace(content, "<.*?>", string.Empty);
        }
    }
}