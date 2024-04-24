using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSS.Patterns.WebAutomation
{
    public abstract class AccountException : Exception
    {
        public string UserId { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }

        protected AccountException(string message, string userId, string username)
            : base(message)
        {
            UserId = userId;
            Username = username;
        }

        protected AccountException(string username)
            : this(username, string.Empty)
        {
        }

        protected AccountException(string username, string password)
        {
            Username = username;
            Password = password;
        }

        protected AccountException()
        {
        }
    }
}