using System;

namespace KSS.Patterns.WebAutomation
{
    public class AccountSuspendedException : Exception
    {
        public string Username { get; private set; }
        public string Password { get; private set; }

        public AccountSuspendedException(string message, string username, string password)
            : base(message)
        {
            Username = username;
            Password = password;
        }

        public AccountSuspendedException(string username)
            : this(username, string.Empty)
        {
        }

        public AccountSuspendedException(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public AccountSuspendedException()
        {
        }
    }
}