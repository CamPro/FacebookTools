namespace IMH.Domain.Facebook
{
    public class FacebookPhoto : BaseEntity
    {
        public string PhotoId { get; set; }

        public string AlbumId { get; set; }

        public int Width { get; set; }
                
        public int Height { get; set; }
                
        public string Url { get; set; }        

        /// <summary>
        /// Url to photo post
        /// </summary>
        public string DetailUrl { get; set; }

        /// <summary>
        /// Url to photo source
        /// </summary>
        public string FullPhotolUrl { get; set; }

        
        public string Description { get; set; }
    }
}