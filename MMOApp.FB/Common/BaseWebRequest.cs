using HtmlAgilityPack;
using KSS.Patterns.Extensions;
using KSS.Patterns.Web;
using KSS.Patterns.WebAutomation;
using MMOApp.FB.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarketDragon.Automation.Common
{
    public abstract class BaseWebRequest
    {
        protected readonly IWeb web;
        protected readonly bool useSocks;
        protected HtmlParser hparser;
        protected string currentUrl;

        public BaseWebRequest() : this(null, null)
        {
        }

        public BaseWebRequest(List<KCookie> cookies, string userAgent, int timeout = 30 * 1000) : this(cookies, userAgent, null, null, null, false, timeout)
        {
        }

        public BaseWebRequest(List<KCookie> cookies, string userAgent, string proxy, bool useSocks = false, int timeout = 30 * 1000) : this(cookies, userAgent, proxy, null, null, useSocks, timeout)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cookies">Account cookies</param>
        /// <param name="userAgent">PC user agent, can be empty</param>
        /// <param name="useSocks"></param>
        /// <param name="proxy"></param>
        public BaseWebRequest(List<KCookie> cookies, string userAgent, string proxy, string proxyUsername = null, string proxyPassword = null, bool useSocks = false, int timeout = 30 * 1000)
        {
            this.useSocks = useSocks;
            hparser = new HtmlParser();

            var dweb = CreatexNetWeb(cookies, userAgent);
            if (!proxy.IsNullOrWhiteSpace())
            {
                dweb.Proxy = proxy;
                dweb.ProxyUsername = proxyUsername;
                dweb.ProxyPassword = proxyPassword;
            }

            web = dweb;

            //PreserveHeaders(web);
            web.SetTimeout(timeout);
        }

        private void PreserveHeaders(IWeb web)
        {
            web.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
            web.AddHeader("accept-language", "en-US,en");
            web.AddHeader("upgrade-insecure-requests", "1");
        }

        public void AddHeader(string key, string value)
        {
            if (key.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(key));
            if (value.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(value));

            web.AddHeader(key, value);
        }

        public bool IsUsingSocksWeb => useSocks;

        public void SetCookies(List<KCookie> cookies)
        {
            web.ClearCookies();
            AddCookies(cookies);
        }

        public void AddCookies(List<KCookie> cookies)
        {
            web.AddCookies(cookies.Select(c => c.ToCookie()));
        }

        public void AddCookie(KCookie cookie)
        {
            web.AddCookies(new[] { cookie.ToCookie() });
        }

        public void SetUserAgent(string userAgent)
        {
            web.UserAgent = userAgent;
        }

        public List<KCookie> GetCookies()
        {
            var cookies = web.GetCookies().Select(KCookie.FromCookie).ToList();
            return cookies;
        }

        public void SetProxy(string proxy)
        {
            SetProxy(proxy, null, null);
        }

        public void SetProxy(string proxy, string username, string password)
        {
            web.Proxy = proxy;
            web.ProxyUsername = username;
            web.ProxyPassword = password;
        }

        public void SetProxy(IMH.Domain.Net.Proxy proxy)
        {
            SetProxy(proxy.Address, proxy.Username, proxy.Password);
        }

        public string GetUserAgent()
        {
            return web.UserAgent;
        }

        public void Reset()
        {
            web.ClearCookies();
            web.UserAgent = string.Empty;
        }

        public string Get(string url)
        {
            return web.GetResponse(url);
        }

        public string Get(string url, string referer)
        {
            var creferer = web.Referer;

            web.Referer = referer;
            var content = web.Get(url);

            web.Referer = creferer;

            return content;
        }

        public string Post(string url,  Dictionary<string, string> parameters, string footprint = "", bool acceptEmpty = true)
        {
            return web.Post(url, parameters, footprint, acceptEmpty);
        }

        public string Post(string url, string footprint = "", bool acceptEmpty = true)
        {
            return web.Post(url, new Dictionary<string, string>(), footprint, acceptEmpty);
        }

        public string AutoRedirect(string response)
        {
            return AutoRedirect(web, response);
        }

        public string AutoRedirect(IWeb web, string response)
        {
            var redirectUrl = string.Empty;

            var redirectedUrls = new List<string>();

            do
            {
                if (redirectedUrls.Contains(redirectUrl))
                    throw new InfinitedRedirectException(redirectUrl);

                if (!redirectUrl.IsNullOrWhiteSpace())
                {
                    redirectUrl = Domainer.HtmlDecode(redirectUrl.Trim());
                    redirectUrl = Domainer.BuildFullLink(GetCurrentDomain(), redirectUrl);
                    web.Referer = GetCurrentUrl();
                    response = web.GetResponse(redirectUrl);
                    redirectedUrls.Add(redirectUrl);
                }

                redirectUrl = web.GetRedirectUrl();
                if (!redirectUrl.IsNullOrWhiteSpace())
                    continue;

                // link tag
                var r = HtmlHelper.Root(response);
                var link = r.Child("link", "rel", "redirect");
                if (link != null)
                {
                    redirectUrl = link.Href();
                    if (!redirectUrl.IsNullOrWhiteSpace())
                        continue;
                }

                // meta refresh
                redirectUrl = r.HtmlMetaRefreshUrl();
                if (!redirectUrl.IsNullOrWhiteSpace())
                    continue;

                // js window.location.replace
                var token = "window.location.replace(\"";
                var index1 = response.IndexOf(token);
                if (index1 >= 0)
                {
                    index1 += token.Length;
                    var index2 = response.IndexOf("\"", index1);
                    redirectUrl = response.Substring(index1, index2 - index1);
                    redirectUrl = redirectUrl.Remove("\\");

                    if (!redirectUrl.IsNullOrWhiteSpace())
                        continue;
                }

                // js window.location.href
                var token2 = "window.location.href=\"";
                var index3 = response.IndexOf(token2);
                if (index3 >= 0)
                {
                    index3 += token2.Length;
                    var index4 = response.IndexOf("\"", index3);
                    redirectUrl = response.Substring(index3, index4 - index3);
                    redirectUrl = redirectUrl.Remove("\\");
                }

            } while (!redirectUrl.IsNullOrWhiteSpace());



            return response;
        }

        public string GetCurrentUrl()
        {
            return web.GetCurrentUrl() ?? currentUrl;
        }

        public string GetCurrentDomain()
        {
            //throw new NotImplementedException();

            var url = GetCurrentUrl();
            if (url.IsNullOrWhiteSpace())
                return null;

            return Domainer.GetDomain(url, true, false);
        }

        public void SetCurrentUrl(string url)
        {
            currentUrl = url;
        }

        public string GetCurrentContent()
        {
            return web.GetCurrentContent();
        }

        /// <summary>
        /// Get submit url of a form in current page source.
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        protected string GetFormUrl(HtmlNode form, string defaultDomain = null)
        {
            var action = Domainer.HtmlDecode(form.Action());
            return GetFullUrl(action, defaultDomain);
        }

        public string GetFullUrl(string urlPart)
        {
            return GetFullUrl(urlPart, null);
        }

        protected string GetFullUrl(string urlPart, string defaultDomain = null)
        {
            if (urlPart.StartsWith("fb://"))
                urlPart = urlPart.Remove("fb://");

            var submitUrl = Domainer.BuildFullLink(GetCurrentDomain() ?? defaultDomain, urlPart);

            return submitUrl;
        }
        

        /// <summary>
        /// Create DirectWeb object with setting cookies & userAgent
        /// </summary>
        /// <param name="cookies"></param>
        /// <param name="userAgent"></param>
        /// <returns></returns>
        public static xNetWeb CreatexNetWeb(List<KCookie> cookies, string userAgent)
        {
            var web = WebFactory.CreatexNetWeb();

            if (cookies != null)
                web.AddCookies(cookies.Select(c => c.ToCookie()));

            if (!userAgent.IsNullOrWhiteSpace())
                web.UserAgent = userAgent;

            return web;
        }

        protected string DoPost(HtmlNode form, Dictionary<string, string> queries)
        {
            var submitUrl = GetFormUrl(form);
            web.Referer = GetCurrentUrl();
            var content = web.Post(submitUrl, queries);

            return content;
        }
    }
}