using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSS.Patterns.WebAutomation
{
    public class WrongPasswordException : Exception
    {
        public string Username { get; set; }

        public WrongPasswordException()
        {
        }

        public WrongPasswordException(string username)
        {
            Username = username;
        }
    }
}
