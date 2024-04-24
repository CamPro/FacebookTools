using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketDragon.Automation.Common
{
    public class InfinitedRedirectException : Exception
    {
        public string RedirectUrl { get; set; }

        public InfinitedRedirectException()
        {

        }

        public InfinitedRedirectException(string redirectUrl)
        {
            RedirectUrl = redirectUrl;
        }

        public InfinitedRedirectException(string redirectUrl, string message) : base(message)
        {
            RedirectUrl = redirectUrl;
        }
    }
}