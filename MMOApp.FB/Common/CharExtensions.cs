namespace KSS.Patterns.Extensions
{
    public static class CharExtensions
    {
        public static bool IsNumber(this char c)
        {
            return '0' <= c && c <= '9';
        }

        public static bool IsNaN(this char c)
        {
            return !IsNumber(c);
        }

        public static bool IsText(this char c)
        {
            return ('a' <= c && c <= 'z') || ('A' <= c && c <= 'Z');
        }
    }
}