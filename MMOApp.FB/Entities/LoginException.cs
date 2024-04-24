using System;

namespace MarketDragon.Automation.Common
{
    public class LoginException : Exception
    {
        public enum Error
        {
            Unknown,
            WrongUsernameOrPassword,
            VerificationRequired,
            UserNotExist,
            AccountDisabled
        }

        public string Service { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Error ErrorCode { get; set; }

        private readonly string message;

        public LoginException(string service, string username, string password)
            : this(service, username, password, string.Empty)
        {

        }

        public LoginException(string service, string username, string password, string message, Error code = Error.WrongUsernameOrPassword)
        {
            Service = service;
            Username = username;
            Password = password;
            this.message = message;

            ErrorCode = code;
        }

        public override string Message
        {
            get
            {
                if (!string.IsNullOrEmpty(message))
                    return message;

                return base.Message;
            }
        }
    }
}