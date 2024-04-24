using IMH.Domain.Net;
using System;
using System.Net;

namespace KSS.Patterns.Web
{
    public class WebEventArgs : EventArgs
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public Proxy Proxy { get; set; }
        public string Content { get; set; }
        public Exception Exception { get; set; }
        public HttpWebRequest Request { get; set; }
    }
}