using System;

namespace IMH.Domain
{
    public class Account : BaseEntity
    {
        
        public string UserId { get; set; }

        
        public string Username { get; set; }

        
        public string Url { get; set; }

        
        public string Email { get; set; }

        public string Identity { get { return Email ?? UserId; } }
        
        public string FirstName { get; set; }

        
        public string LastName { get; set; }

        
        public string FullName { get; set; }

        
        public string NameSet { get; set; }

        
        public string Password { get; set; }

        
        public string OldPassword { get; set; }

        
        public DateTime? DOB { get; set; }

        
        public bool? Gender { get; set; }

        
        public string Phone { get; set; }

        
        public string Description { get; set; }

        
        public string StreetAddress { get; set; }

        
        public string City { get; set; }

        
        public string State { get; set; }

        
        public string ZipCode { get; set; }

        public bool IsSameName(Account another)
        {
            return FirstName == another.FirstName && LastName == another.LastName;
        }
    }
}