using System;
using System.Text;

namespace KSS.Patterns.WebAutomation
{
    public class SiteStructureChangedException : Exception
    {
        public SiteStructureChangedException(string url, string element)
        {
            Url = url;
            Element = element;
        }

        public SiteStructureChangedException(string url, string element, string content) : this(url, element, content, null)
        {

        }

        public SiteStructureChangedException(string url, string element, string content, string html)
        {
            Url = url;
            Element = element;
            Content = content;
            Html = html;
        }

        public string Url { get; set; }
        public string Element { get; set; }
        public string Content { get; set; }
        public string Html { get; set; }

        public override string Message
        {
            get
            {
                StringBuilder sbMessage = new StringBuilder();
                sbMessage.AppendFormat("'{0}' : '{1}'", Url, Element);
                sbMessage.AppendLine(string.Format("Content: {0}", Content));

                return sbMessage.ToString();
            }
        }

        public SiteStructureChangedException()
        {
        }
    }
}