using System;
using System.Web;
using KSS.Patterns.Web;
using System.Linq;
using IMH.Domain.Facebook;

namespace MarketDragon.Automation.Social.Facebook
{
    public static class FBCommon
    {
        public const string HOME_URL = "https://web.facebook.com/";

        public const string MBASIC_HOME_URL = "https://mbasic.facebook.com";

        public const string MOBILE_HOME_URL = "https://mbasic.facebook.com";

        public static string[] GetFacebookDomains()
        {
            return new[] { "https://facebook.com", "https://www.facebook.com", "https://m.facebook.com" };
        }

        public static string BuildMobileBasicLink(string url)
        {
            return BuildFullLink(MOBILE_HOME_URL, url);
        }

        public static string BuildMobileLink(string url)
        {
            return BuildFullLink(MOBILE_HOME_URL, url);
        }

        public static string BuildFacebookLink(string url)
        {
            return BuildFullLink(HOME_URL, url);
        }

        public static string BuildFullLink(string domain, string url)
        {
            url = Domainer.HtmlDecode(url);

            if (url.StartsWith("fb://"))
            {
                url = url.Replace("fb://", domain);
            }

            var prefixes = new[] { "http://", "https://", "fb://" };

            if (prefixes.All(pre => !url.StartsWith(pre)))
            {
                if (!url.StartsWith("/"))
                    url = "/" + url;

                url = domain + url;
            }


            return url;
        }

        public static string GetQueryParam(string url, string queryParam)
        {
            var fullUrl = BuildMobileLink(url);
            var uri = new Uri(fullUrl);

            var paramValue = HttpUtility.ParseQueryString(uri.Query).Get(queryParam);

            return paramValue;
        }

        public static bool MobileRegistrationIsConfirmEmailPage(string url)
        {
            return !IsCheckPointUrl(url) && url.Contains("confirmemail.php");
        }

        public static bool IsCheckPointUrl(string url)
        {
            return url.Contains("facebook.com/checkpoint/block");
        }

        public static string MobileGetProfileUrl(string userId)
        {
            var url = $"https://m.facebook.com/profile.php?id={userId}";
            return url;
        }
    }
}