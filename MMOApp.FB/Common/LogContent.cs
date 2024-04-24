using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSS.Patterns.Logging
{
    public class LogContent
    {
        public string Message { get; set; }
        public LogMode Mode { get; set; }
        public Exception Exception { get; set; }

        public LogContent(string message, LogMode mode = LogMode.Runtime)
            : this(message, mode, null)
        {
        }

        public LogContent(string message, LogMode mode, Exception exception)
        {
            Message = message;
            Mode = mode;
            Exception = exception;
        }
    }
}