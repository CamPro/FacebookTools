using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMH.Domain.Facebook
{
    public class CheckpointPhotosOfFriendsData
    {
        public List<string> FriendPhotosUrls { get; set; }
        public List<Bitmap> FriendPhotos { get; set; }
        public List<string> FriendNames { get; set; }

        public CheckpointPhotosOfFriendsData()
        {
            FriendPhotosUrls = new List<string>();
            FriendNames = new List<string>();
        }
    }
}