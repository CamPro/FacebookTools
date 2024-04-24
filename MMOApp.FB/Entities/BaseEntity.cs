using System;

namespace IMH.Domain
{
    [Serializable]
    public abstract class BaseEntity
    {
        public string ServiceCode { get; set; }
        
        public DateTime DateCreated { get; set; }
        
        public DateTime DateUpdated { get; set; }

        protected BaseEntity()
        {
            DateCreated = DateTime.Now;
        }
    }
}