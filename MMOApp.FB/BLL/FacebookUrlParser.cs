using KSS.Patterns.Extensions;
using KSS.Patterns.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMOApp.FB.BLL
{
    public class FacebookUrlParser
    {        
        public void ParseUserLink( string link, out string userId, out string username)
        {
            if (string.IsNullOrWhiteSpace(link))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(link));

            var url = Domainer.HtmlDecode(link);

            Uri uri;
            if (!Uri.TryCreate(url, UriKind.Absolute, out uri))
                throw new ArgumentException("Invalid link.", nameof(link));

            //var domain = "facebook.com";
            //var domain = Domainer.GetRootDomain(url, false, true);
            //if (!domain.Equals("facebook.com", StringComparison.InvariantCultureIgnoreCase) && !domain.Equals("fb.com", StringComparison.InvariantCultureIgnoreCase))
            //    throw new ArgumentException("The link is not Facebook domain.", nameof(link));

            userId = username = string.Empty;

            if (url.Contains("profile.php?"))
                userId = Domainer.GetQueryValueFromUrl(url, "id");

            if (userId.IsNullOrWhiteSpace())
                userId = Domainer.GetQueryValueFromUrl(url, "uid");

            // url only contains User Id or Username
            if (userId.IsNullOrWhiteSpace())
                username = uri.LocalPath.Remove("/");

            if (username.Equals("profile.php"))
                username = null;
        }
        
        public void ParsePostLink(string link, out string postId)
        {
            link = Domainer.HtmlDecode(link);

            postId = ParseTopLevelPostIdFromUrl(link);
            if (!postId.IsNullOrWhiteSpace())
                return;

            if (link.Contains("story.php"))
            {
                postId = Domainer.GetQueryValueFromUrl(link, "story_fbid");
            }
            else if (link.Contains("view=permalink"))
            {
                postId = Domainer.GetQueryValueFromUrl(link, "id");
            }
            else if (link.Contains("/photos/"))
            {
                link = Domainer.UrlDecode(link);
                postId = ParseTopLevelPostIdFromUrl(link);
                if (postId.IsNullOrWhiteSpace())
                {
                    var index = link.IndexOf("?");
                    postId = link.Substring(0, index).Split('/').Last(s => !s.IsNullOrWhiteSpace());
                }
            }
            else if (link.Contains("photo.php"))
            {
                postId = ParseTopLevelPostIdFromUrl(link);
                if (postId.IsNullOrWhiteSpace())
                    postId = Domainer.GetQueryValueFromUrl(link, "fbid");
            }
            else if (link.Contains("/albums/"))
            {
                if (postId.IsNullOrWhiteSpace())
                {
                    var index = link.IndexOf("?");
                    if (index > 0)
                        link = link.Substring(0, index);

                    postId = link.Split('/').Last(s => !s.IsNullOrWhiteSpace());
                }
            }
            else if (link.Contains("story"))
            {
                postId = Domainer.GetQueryValueFromUrl(link, "hash");
            }
            else if (link.Contains("/posts/"))
            {
                var token = "/posts/";
                var start = link.IndexOf(token);
                start += token.Length;
                var end = link.IndexOf("/", start);
                postId = link.Substring(start, end - start - 1);
            }
            else
            {
                throw new Exception("Unsupport post url : " + link);
            }
        }
        
        private string ParseTopLevelPostIdFromUrl(string url)
        {
            url = Domainer.UrlDecode(url);
            var token = "top_level_post_id.";
            var start = url.IndexOf(token);
            if (start > 0)
            {
                start += token.Length;
                var index2 = url.IndexOf(":", start);
                var index3 = url.IndexOf("#", start);
                var end = (index2 > 0 && index3 > 0) ? Math.Min(index2, index3) : Math.Max(index2, index3);
                var id = end > 0 ? url.Substring(start, end - start) : url.Substring(start);
                return id;
            }

            return null;
        }
    }
}
