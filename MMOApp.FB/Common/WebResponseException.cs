using System;
using System.Runtime.Serialization;

namespace KSS.Patterns.Web
{
    public enum WebResponseErrorCode
    {
        General,
        NullResponse,
        NoFootprint
    }

    public class WebResponseException : Exception
    {
        public WebResponseErrorCode ErrorCode { get; set; }

        public override string Message
        {
            get
            {
                string message = base.Message;
                if (string.IsNullOrEmpty(message))
                {
                    message = ErrorCode.ToString();
                }
                else
                {
                    message += " Error Code: " + ErrorCode;
                }

                return message;
            }
        }

        public WebResponseException()
        {
        }

        public WebResponseException(string message) : base(message)
        {
        }

        public WebResponseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WebResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}