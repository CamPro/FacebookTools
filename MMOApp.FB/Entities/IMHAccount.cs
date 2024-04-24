using System.Collections.Generic;
using KSS.Patterns.Extensions;
using IMH.Domain.Net;
using KSS.Patterns.WebAutomation;

namespace IMH.Domain
{
    public class IMHAccount : Account
    {
        
        public long VPId { get; set; }

        
        public string Proxy { get; set; }

        
        public string ProxyUsername { get; set; }

        
        public string ProxyPassword { get; set; }

        
        public bool? UseProxy { get; set; }

        
        public bool? IsSockProxy { get; set; }

        
        public string UserAgent { get; set; }

        
        public string Profile { get; set; }

        
        public string MetaData { get; set; }

        
        public List<KCookie> Cookies { get; set; }

        public string CookiesText { get; set; }
        
        
        public bool PVA { get; set; }
        
        public bool Verified { get; set; }

        
        public string RegistrationIPAddress { get; set; }

        
        public string RegistrationCountryCode { get; set; }

        public string RegistrationCountryName { get; set; }

        public string RegistrationCountryPhoneCode { get; set; }

        public bool Blocked { get; set; }

        public bool Active { get; set; }

        public bool IsUsingProxy()
        {
            return UseProxy.HasValue && UseProxy.Value;
        }

        public bool HasProxy()
        {
            return !Proxy.IsNullOrWhiteSpace();
        }

        public bool IsUsingSockProxy()
        {
            return IsSockProxy.HasValue && IsSockProxy.Value;
        }

        public virtual void SetProxy(Proxy proxy)
        {
            Proxy = proxy.Address;
            ProxyUsername = proxy.Username;
            ProxyPassword = proxy.Password;
            IsSockProxy = proxy.IsSocks;
            UseProxy = true;
        }

        public virtual void ClearProxy()
        {
            Proxy = null;
            ProxyUsername = null;
            ProxyPassword = null;
            IsSockProxy = false;
            UseProxy = false;
        }

        public IMHAccount()
        {
            Active = true;
            Blocked = false;
        }
    }
}