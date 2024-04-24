using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using KSS.Patterns.Extensions;

namespace KSS.Patterns.WebAutomation
{
    [Serializable]
    public class KCookie
    {
        public DateTime CreationTime { get; set; }

        public long Expiry { get; set; }

        public string Host { get; set; }

        public bool IsDomain { get; set; }

        public bool IsHttpOnly { get; set; }

        public bool IsSecure { get; set; }

        public bool IsSession { get; set; }

        public long LastAccess { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public string RawHost { get; set; }

        public string Value { get; set; }

        public Cookie ToCookie()
        {
            //var cookie = new Cookie(Name, Value, Path, RawHost);

            var cookie = new Cookie(Name, Value, Path, RawHost)
            {
                Domain = Host,
                HttpOnly = IsHttpOnly,
                Secure = IsSecure,
                Expired = false
            };

            if (Expiry > 0)
            {
                try
                {
                    cookie.Expires = Expiry.KFromUnixTime();
                }
                catch (Exception)
                {
                }
            }

            return cookie;
        }

        public static KCookie FromCookie(Cookie cookie)
        {
            return new KCookie
            {
                CreationTime = cookie.TimeStamp,
                Expiry = cookie.Expires.KToUnixTime(),
                Host = cookie.Domain,
                //IsDomain = cookie.HttpOnly,
                IsHttpOnly = cookie.HttpOnly,
                IsSecure = cookie.Secure,
                //IsSession = cookie.,
                Name = cookie.Name,
                Path = cookie.Path,
                RawHost = cookie.Domain,
                Value = cookie.Value
            };
        }
    }
}
