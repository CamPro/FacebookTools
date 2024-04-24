using System;

namespace KSS.Patterns.WebAutomation
{
    public class AccountBlockedException : AccountException
    {
        public AccountBlockedException() : base()
        {

        }

        public AccountBlockedException(string message, string userId, string username) : base(message, userId, username)
        {

        }

        public AccountBlockedException(string username) : base(username)
        {

        }

        public AccountBlockedException(string username, string password) : base(username, password)
        {

        }
    }
}