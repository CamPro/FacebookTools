using MMOApp.FB.Common;

namespace KSS.Patterns.Web
{
    public class WebFactory
    {
        public const string DefaultUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";

        public static xNetWeb CreatexNetWeb()
        {
            return new xNetWeb();
        }

        //public static SocksWeb CreateSocksWeb()
        //{
        //    return new SocksWeb();
        //}
    }
}