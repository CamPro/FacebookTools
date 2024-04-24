using System;
using System.Collections.Generic;
using System.Net;

namespace KSS.Patterns.Web
{
    public interface IWeb : ICloneable
    {
        string Name { get; set; }
        string Host { get; set; }
        string UserAgent { get; set; }
        string Referer { get; set; }
        string Proxy { get; set; }
        string ProxyUsername { get; set; }
        string ProxyPassword { get; set; }

        event EventHandler<WebEventArgs> Request;
        event EventHandler<WebEventArgs> Response;
        event EventHandler<WebEventArgs> Exception;

        /// <summary>
        /// Set request timeout as milisecond
        /// </summary>
        /// <param name="to"></param>
        void SetTimeout(int to);

        void AddHeader(string key, string value);
        CookieCollection GetCookieCollection();
        List<Cookie> GetCookies();
        void AddCookie(Cookie cookie);
        void AddCookie(string domain, string path, string name, string value);
        void AddCookies(CookieCollection addingCookies);
        void AddCookies(IEnumerable<Cookie> addingCookies);
        void ClearCookies();
        string GetCurrentUrl();
        string GetRedirectUrl();
        string GetCurrentContent();
        Dictionary<string, string> GetLastSessionHeaders();
        T GetLastSessionValue<T>(string name);

        /// <summary>
        /// Get a request. If parameters available, do POST form.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <param name="footprint"></param>
        /// <param name="acceptEmpty"></param>
        /// <returns></returns>
        string GetResponse(string url, string parameters = "", string footprint = "", bool acceptEmpty = true);

        /// <summary>
        /// Get a request. If parameters available, do POST multi part form.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <param name="footprint"></param>
        /// <param name="acceptEmpty"></param>
        /// <returns></returns>
        string GetResponse(string url, Dictionary<string, string> parameters, string footprint = "", bool acceptEmpty = true);

        string Get(string url, string footprint = "", bool acceptEmpty = true);
        string Post(string url,  Dictionary<string, string> parameters, string footprint = "", bool acceptEmpty = true);
        string Post(string url,  List<KeyValuePair<string, string>> parameters, string footprint = "", bool acceptEmpty = true);
        string PostMultipart(string url,  Dictionary<string, string> parameters, string footprint = "", bool acceptEmpty = true);
        string PostMultipart(string url,  Dictionary<string, string> parameters, byte[] data, string name, string filename, string footprint = "", bool acceptEmpty = true);
        string PostMultipart(string url,  Dictionary<string, string> parameters, IEnumerable<UploadFile> files, string footprint = "", bool acceptEmpty = true);
        string PostJson(string url, string json, string footprint = "", bool acceptEmpty = true);
        byte[] Download(string url);
    }
}