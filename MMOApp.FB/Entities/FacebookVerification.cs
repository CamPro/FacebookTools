using KSS.Patterns.WebAutomation;
using System.Collections.Generic;

namespace IMH.Domain.Facebook
{
    public class FacebookVerification : AppVerification
    {
        public string AccessToken { get; set; }

        
        public string SessionKey { get; set; }

        
        public string Secret { get; set; }

        
        public string DeviceId { get; set; }

        
        public string MachineId { get; set; }

        
        public string UserStorageKey { get; set; }

        
        public List<KCookie> Cookies { get; set; }

        
        public string Longitude { get; set; }

        
        public string Latitude { get; set; }
    }
}