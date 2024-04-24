
namespace IMH.Domain
{
    public class AppVerification : BaseEntity
    {
        
        public long AppId { get; set; }

        
        public string AppSId { get; set; }

        
        public string AppName { get; set; }

        
        public long AccountId { get; set; }

        
        public string UserId { get; set; }

        
        public string Username { get; set; }

        
        public string UserFullName { get; set; }

        
        public string Email { get; set; }

        
        public string Permissions { get; set; }

        public bool Blocked { get; set; }
        public bool Active { get; set; }

        
        public string Proxy { get; set; }

        
        public string UserAgent { get; set; }

        public AppVerification()
        {
            Active = true;
            Blocked = false;
        }
    }
}