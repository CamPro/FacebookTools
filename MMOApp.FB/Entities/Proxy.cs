using System;

namespace IMH.Domain.Net
{
    public partial class Proxy : BaseEntity
    {
        public string Address { get; set; }

        
        public string Rank { get; set; }

        /// <summary>
        /// Time in seconds to connect to proxy.
        /// </summary>
        
        public int Ping { get; set; }

        public bool IsPrivate { get; set; }

        public bool Active { get; set; }

        public bool IsSocks { get; set; }

        
        public string Provider { get; set; }

        
        public string ProviderUrl { get; set; }
        
        
        public string IPRange { get; set; }

        
        public string GeoId { get; set; }

        
        public string CountryIsoCode { get; set; }

        
        public string CountryName { get; set; }

        
        public string CityName { get; set; }

        
        public string TimeZone { get; set; }

        
        public string Username { get; set; }

        
        public string Password { get; set; }

        
        public int Port { get; set; }

        /// <summary>
        /// Whether this proxy can be use to reg FB account.
        /// </summary>
        
        public bool? CanRegFB { get; set; }

        /// <summary>
        /// Facebook : last time use this proxy to access Facebook.
        /// </summary>
        
        public DateTime? LastFBUse { get; set; }

        /// <summary>
        /// Facebook : last time use this proxy to make registration on Facebook.
        /// </summary>
        
        public DateTime? LastFBReg { get; set; }

        
        
        public string SshScanError { get; set; }

        /// <summary>
        /// Time need to connet & login to ssh, in second.
        /// </summary>
        
        public int SshScanTime { get; set; }

        
        public DateTime? DateRegistration { get; set; }

        
        public DateTime? DateExpired { get; set; }
        

        public string Text { get; set; }
    }
}