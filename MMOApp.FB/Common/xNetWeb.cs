using KSS.Patterns.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using KSS.Patterns.Extensions;
using xNet;

namespace MMOApp.FB.Common
{
    public class xNetWeb : IWeb
    {
        protected const int SECOND = 1000;
        protected const int DEFAULT_TIMEOUT = 30 * SECOND;

        protected int timeout = DEFAULT_TIMEOUT;

        private readonly Dictionary<string, string> headers = new Dictionary<string, string>();

        protected readonly Dictionary<string, object> lastSessionValues;
        protected readonly Dictionary<string, string> lastSessionHeaders; // not yet supported

        protected readonly List<Cookie> cookies;

        protected string currentUrl;
        protected string currentContent;
        protected string redirectUrl;

        public string Name { get; set; }
        public bool AutoRedirect { get; set; }
        public string UserAgent { get; set; }
        public string Referer { get; set; }
        public string Proxy { get; set; }
        public string ProxyUsername { get; set; }
        public string ProxyPassword { get; set; }

        public string Host { get; set; }

        public event EventHandler<WebEventArgs> Request;
        public event EventHandler<WebEventArgs> Response;
        public event EventHandler<WebEventArgs> Exception;

        public xNetWeb()
        {
            AutoRedirect = true;

            cookies = new List<Cookie>();
            lastSessionValues = new Dictionary<string, object>();
            lastSessionHeaders = new Dictionary<string, string>();
        }

        public void SetTimeout(int to)
        {
            timeout = to;
        }

        public T GetLastSessionValue<T>(string name)
        {
            return lastSessionValues.ContainsKey(name) ? (T)lastSessionValues[name] : default(T);
        }

        public void AddHeader(string key, string value)
        {
            if (headers.ContainsKey(key))
                headers[key] = value;
            else
                headers.Add(key, value);
        }

        public void ClearHeaders()
        {
            headers.Clear();
        }

        public CookieCollection GetCookieCollection()
        {
            var cookieColl = new CookieCollection();

            cookies.ForEach(c => cookieColl.Add(c));

            return cookieColl;
        }

        public List<Cookie> GetCookies()
        {
            return new List<Cookie>(cookies);
        }

        public void AddCookie(Cookie cookie)
        {
            cookies.Add(cookie);
        }

        public void AddCookie(string domain, string path, string name, string value)
        {
            AddCookie(new Cookie(name, value, path, domain));
        }

        public void AddCookies(CookieCollection addingCookies)
        {
            addingCookies.Cast<Cookie>().ForEach(c => cookies.Add(c));
        }

        public void AddCookies(IEnumerable<Cookie> addingCookies)
        {
            addingCookies.ForEach(AddCookie);
        }

        public void ClearCookies()
        {
            cookies.Clear();
        }

        public string GetCurrentUrl()
        {
            return currentUrl;
        }

        public string GetCurrentDomain()
        {
            throw new NotImplementedException();
        }

        public string GetRedirectUrl()
        {
            return redirectUrl;
        }

        public string GetCurrentContent()
        {
            return currentContent;
        }

        public Dictionary<string, string> GetLastSessionHeaders()
        {
            return lastSessionHeaders;
        }


        public string Get(string url, string footprint = "", bool acceptEmpty = true)
        {
            using (var request = new HttpRequest())
            {
                BeginRequest(request, url);

                var response = SendRequest(() => request.Get(url), url);

                EndRequest(response, url);
            }

            return currentContent;
        }

        public string GetResponse(string url, Dictionary<string, string> parameters, string footprint = "", bool acceptEmpty = true)
        {
            return PostMultipart(url, parameters, footprint, acceptEmpty);
        }

        public string GetResponse(string url, string parameters = "", string footprint = "", bool acceptEmpty = true)
        {
            var response = parameters.IsNullOrWhiteSpace()
                ? Get(url, footprint, acceptEmpty)
                : Post(url, Domainer.ParseQueryString(parameters), footprint, acceptEmpty);

            return response;
        }

        public string Post(string url, List<KeyValuePair<string, string>> parameters, string footprint = "", bool acceptEmpty = true)
        {
            using (var request = new HttpRequest())
            {
                BeginRequest(request, url);

                var response = SendRequest(() =>
                {
                    parameters.ForEach(item => request.AddParam(item.Key, item.Value));
                    return request.Post(url);
                }, url);

                EndRequest(response, url);
            }

            return currentContent;
        }

        public string Post(string url, Dictionary<string, string> parameters, string footprint = "", bool acceptEmpty = true)
        {
            return Post(url, parameters.ToList(), footprint, acceptEmpty);
        }

        public string PostJson(string url, string json, string footprint = "", bool acceptEmpty = true)
        {
            throw new NotImplementedException();
        }

        public string PostMultipart(string url, Dictionary<string, string> parameters, string footprint = "", bool acceptEmpty = true)
        {
            using (var request = new HttpRequest())
            {
                BeginRequest(request, url);

                var response = SendRequest(() =>
                {
                    parameters.ForEach(item => request.AddField(item.Key, item.Value));
                    return request.Post(url);
                }, url);

                EndRequest(response, url);
            }

            return currentContent;
        }

        public string PostMultipart(string url, Dictionary<string, string> parameters, IEnumerable<UploadFile> files, string footprint = "", bool acceptEmpty = true)
        {
            using (var request = new HttpRequest())
            {
                BeginRequest(request, url);

                var response = SendRequest(() =>
                {
                    parameters.ForEach(item => request.AddField(item.Key, item.Value));
                    files.ForEach(file => request.AddFile(file.Name, file.FileName, file.Data));
                    return request.Post(url);
                }, url);

                EndRequest(response, url);
            }

            return currentContent;
        }

        public string PostMultipart(string url, Dictionary<string, string> parameters, byte[] data, string name, string filename, string footprint = "", bool acceptEmpty = true)
        {
            using (var request = new HttpRequest())
            {
                BeginRequest(request, url);

                var response = SendRequest(() =>
                {
                    parameters.ForEach(item => request.AddField(item.Key, item.Value));
                    request.AddFile(name, filename, data);
                    return request.Post(url);
                }, url);

                EndRequest(response, url);
            }

            return currentContent;
        }


        private void BeginRequest(HttpRequest request, string url)
        {
            request.Cookies = new CookieDictionary();

            foreach (var cookie in cookies)
            {
                request.Cookies.Add(cookie.Name, cookie.Value);
            }

            request.AllowAutoRedirect = AutoRedirect;

            if (!UserAgent.IsNullOrWhiteSpace())
                request.UserAgent = UserAgent;

            if (!Referer.IsNullOrWhiteSpace())
                request.Referer = Referer;

            foreach (var key in headers.Keys)
            {
                request.AddHeader(key, headers[key]);
            }

            WebEventArgs args = new WebEventArgs
            {
                Name = Name,
                Url = url
            };

            OnRequest(args);
        }

        private void EndRequest(HttpResponse response, string url)
        {
            WebEventArgs args = new WebEventArgs
            {
                Name = Name,
                Url = url
            };


            var content = response.ToString();

            currentUrl = response.Address.AbsoluteUri;
            Referer = currentUrl;

            // update cookies
            var rcookies = ConvertCookies(response.Cookies);
            cookies.Clear();
            cookies.AddRange(rcookies);

            args.Content = content;
            OnReponse(args);

            currentContent = content;
        }

        private byte[] DownloadRequest(HttpResponse response, string url)
        {
            WebEventArgs args = new WebEventArgs
            {
                Name = Name,
                Url = url
            };

            var content = response.ToBytes();

            currentUrl = response.Address.AbsoluteUri;
            Referer = currentUrl;

            // update cookies
            var rcookies = ConvertCookies(response.Cookies);
            cookies.Clear();
            cookies.AddRange(rcookies);

            OnReponse(args);

            return content;
        }

        private HttpResponse SendRequest(Func<HttpResponse> send, string url)
        {
            WebEventArgs args = new WebEventArgs
            {
                Name = Name,
                Url = url
            };

            try
            {
                return send();
            }
            catch (Exception e)
            {
                args.Exception = e;
                OnException(args);
                throw new WebResponseException($"Error while trying to request url: '{url}'", e) { ErrorCode = WebResponseErrorCode.General };
            }
        }

        private List<Cookie> ConvertCookies(CookieDictionary dicCookies)
        {
            var ccookies = new List<Cookie>();
            foreach (var name in dicCookies.Keys)
            {
                var value = dicCookies[name];
                var cookie = new Cookie(name, value);
                ccookies.Add(cookie);
            }

            return ccookies;
        }


        protected virtual void OnRequest(WebEventArgs args)
        {
            OnEvent(Request, args);
        }

        protected virtual void OnReponse(WebEventArgs args)
        {
            OnEvent(Response, args);
        }

        protected virtual void OnException(WebEventArgs args)
        {
            OnEvent(Exception, args);
        }

        private void OnEvent(EventHandler<WebEventArgs> e, WebEventArgs a)
        {
            if (e != null)
            {
                e(this, a);
            }
        }

        public byte[] Download(string url)
        {
            byte[] data = null;

            using (var request = new HttpRequest())
            {
                BeginRequest(request, url);

                var response = SendRequest(() => request.Get(url), url);

                data = DownloadRequest(response, url);
            }

            return data;
        }

        public object Clone()
        {
            var web = new xNetWeb
            {
                Name = Name,
                AutoRedirect = AutoRedirect,
                UserAgent = UserAgent,
                Referer = Referer,
                Proxy = Proxy,
                ProxyUsername = ProxyUsername,
                ProxyPassword = ProxyPassword,
                Host  = Host
            };

            web.AddCookies(GetCookies());

            if (headers != null)
                headers.ForEach(h => web.AddHeader(h.Key, h.Value));

            web.SetTimeout(timeout);            

            return web;
        }
    }
}