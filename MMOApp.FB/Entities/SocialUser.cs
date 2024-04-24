using KSS.Patterns.Extensions;

namespace IMH.Domain
{
    public class SocialUser : IMHAccount
    {
        
        public bool DefaultProfile { get; set; }

        
        public string ProfileImage { get; set; }

        
        public string ProfileImageUrl { get; set; }

        
        public bool DefaultProfileImage { get; set; }

        
        public string Location { get; set; }

        
        public string Language { get; set; }

        
        public string TimeZone { get; set; }

        public bool IsOnline { get; set; }

        public bool HasProfileImage()
        {
            return !ProfileImage.IsNullOrWhiteSpace();
        }
    }
}