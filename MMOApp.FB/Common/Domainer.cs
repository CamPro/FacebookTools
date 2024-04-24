using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Linq;
using KSS.Patterns.Extensions;

namespace KSS.Patterns.Web
{
    public static class Domainer
    {
        public static string GetDomain(string url, bool keepScheme, bool removeWWW)
        {
            Uri uri = new Uri(url);
            string domain = uri.Host;

            if (removeWWW)
            {
                List<string> segments = domain.Split('.').ToList();
                if (segments[0].Equals("www", StringComparison.InvariantCultureIgnoreCase))
                {
                    segments.RemoveAt(0);
                    domain = Join(segments);
                }
            }

            if (keepScheme)
            {
                domain = $"{uri.Scheme}://{domain}";
            }

            return domain;
        }
        public static string GetUrlWithoutQueryString(string url)
        {
            Uri uri = new Uri(url);
            string path = $"{uri.Scheme}{Uri.SchemeDelimiter}{uri.Authority}{uri.AbsolutePath}";

            return path;
        }
        public static string GetRawUrl(string url)
        {
            return UrlDecode(HtmlDecode(UrlDecode(HtmlDecode(url))));
        }
        public static string UrlEncode(string url)
        {
            return UrlEncode(url, true);
        }
        public static string UrlEncode(string url, bool keepHttp)
        {
            if (url.IsNullOrWhiteSpace())
                return url;

            const string http = "http://";
            if (keepHttp && url.StartsWith(http))
            {
                //return http + HttpUtility.UrlEncode(url.Remove(0, http.Length).ToUpperInvariant());
                return http + HttpUtility.UrlEncode(url.Remove(0, http.Length));
            }

            return HttpUtility.UrlEncode(url);
        }

        public static string UrlEncodeSeparateParams(string url)
        {
            var encodedUrl = new StringBuilder();
            var link = GetUrlWithoutQueryString(url);
            encodedUrl.Append(link + "?");

            var uri = new Uri(url);

            bool firstQuery = true;
            var queries = HttpUtility.ParseQueryString(uri.Query);
            foreach (var key in queries.Keys)
            {
                var name = key.ToString();
                if (!firstQuery)
                {
                    encodedUrl.Append("&");
                }

                encodedUrl.AppendFormat("{0}={1}", name, UrlEncode(queries[name]));
                firstQuery = false;
            }

            return encodedUrl.ToString();
        }
        public static string GetQueryValueFromUrl(string url, string queryName)
        {
            var uri = new Uri(url);
            var value = GetQueryValueFromQueryString(uri.Query, queryName);

            return value;
        }
        /// <summary>
        /// Get query value in query part, the text start with ? mark.
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="queryName"></param>
        /// <returns></returns>
        public static string GetQueryValueFromQueryString(string queryString, string queryName)
        {
            var queries = HttpUtility.ParseQueryString(queryString);
            var value = queries.Get(queryName);

            return value;
        }
        public static Dictionary<string, string> ParseQueryString(string queryString)
        {
            var parameters = new Dictionary<string, string>();
            var queries = HttpUtility.ParseQueryString(queryString);
            foreach (string name in queries.Keys)
            {
                parameters.Add(name, queries[name]);
            }

            return parameters;
        }
        public static Dictionary<string, string> ParseQueryStringFromUrl(string url)
        {
            var uri = new Uri(url);
            return ParseQueryString(uri.Query);
        }
        public static string BuildUrlQuery(Dictionary<string, string> queryParams, bool encode = true)
        {
            return BuildUrlQuery(queryParams.ToList(), encode);
        }
        public static string BuildUrlQuery(List<KeyValuePair<string, string>> queryParams, bool encode = true)
        {
            var sbQueries = new StringBuilder();
            foreach (var query in queryParams)
            {
                var key = query.Key;
                var value = query.Value;

                if (sbQueries.Length > 0)
                    sbQueries.Append("&");

                if (encode)
                    sbQueries.Append(UrlEncode(key) + "=" + UrlEncode(value));
                else
                    sbQueries.Append(key + "=" + value);
            }

            return sbQueries.ToString();
        }

        private static string Join(IEnumerable<string> segments)
        {
            StringBuilder sbUrl = new StringBuilder();
            foreach (string segment in segments)
            {
                if (sbUrl.Length > 0)
                    sbUrl.Append(".");

                sbUrl.Append(segment);
            }

            return sbUrl.ToString();
        }
        public static string HtmlDecode(string url)
        {
            if (url.IsNullOrWhiteSpace())
                return url;

            string decode = HttpUtility.HtmlDecode(url);

            if (decode != url)
                return HtmlDecode(decode);

            return decode;
        }
        public static string UrlDecode(string url)
        {
            if (url.IsNullOrWhiteSpace())
                return url;

            string decode = HttpUtility.UrlDecode(url);

            if (decode != url)
                return UrlDecode(decode);

            return decode;
        }


        public static string BuildFullLink(string domain, string url)
        {
            url = HtmlDecode(url);

            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
            {
                if (!url.StartsWith("/"))
                    url = "/" + url;

                url = domain + url;
            }

            return url;
        }
    }
}