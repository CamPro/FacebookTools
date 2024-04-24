using System;
using System.Collections.Generic;
using System.Linq;
using KSS.Patterns.Cloning;
using KSS.Patterns.Extensions;
using KSS.Patterns.WebAutomation;
using IMH.Domain.Net;

namespace IMH.Domain.Facebook
{
    [Serializable]
    public class FacebookUser : SocialUser
    {
        public event Action<FacebookUser> FriendRequestSent;


        public string Country { get; set; }


        public FacebookVerification AndroidVerification { get; set; }
        
        public FacebookVerification iPhoneVerification { get; set; }

        public string ProfilePhotoPageUrl { get; set; }


        public string CoverPhoto { get; set; }
        
        public string CoverPhotoUrl { get; set; }

        public string Locale { get; set; }

        public string Religion { get; set; }

        public CheckPointType CheckPointType { get; set; }


        /// <summary>
        /// Trusted friends, used to unlock checkpoint
        /// </summary>

        public List<string> TrustedFriends { get; set; }

        public string LoginCookies => GetLoginCookieString();


        public bool? CanntUnlock { get; set; }


        public int TotalFriends { get; set; }        

        public List<string> FriendIds { get; set; }
        



        public bool IsSameUser(FacebookUser user)
        {
            return IsSameUser(user.UserId, user.Username);
        }

        public bool IsSameUser(string userId, string username)
        {
            var sameUserId = !UserId.IsNullOrWhiteSpace() && UserId == userId;
            var sameUsername = !Username.IsNullOrWhiteSpace() && Username == username;

            return sameUserId || sameUsername;
        }


        public void Block()
        {
            Blocked = true;
        }

        public void Disable()
        {
            Active = false;
        }
        

        /// <summary>
        /// Remove non Facebook cookies.
        /// </summary>
        public void CleanCookies()
        {
            CleanCookies(Cookies);
        }

        public List<KCookie> GetLoginCookies()
        {
            if (Cookies == null || Cookies.Count <= 0)
                return null;

            var c_user = Cookies.FirstOrDefault(c => c.Name == "c_user");
            var xs = Cookies.FirstOrDefault(c => c.Name == "xs");

            if (c_user == null || xs == null)
                return null;

            var cookies = new List<KCookie> { c_user, xs };
            return cookies;
        }

        public string GetLoginCookieString()
        {
            if (Cookies == null || Cookies.Count <= 0)
                return null;

            var c_user = Cookies.FirstOrDefault(c => c.Name == "c_user");
            var xs = Cookies.FirstOrDefault(c => c.Name == "xs");

            if (c_user == null || xs == null)
                return null;

            var cookiesString = $"c_user={c_user.Value}; xs={xs.Value}";

            return cookiesString;
        }

        public void AddCookies(string cookieText)
        {
            if (Cookies == null)
                Cookies = new List<KCookie>();

            var segments = cookieText.Split(';');
            foreach (var segment in segments)
            {
                var pair = segment.Trim().Split('=');
                if (pair.Length != 2)
                    continue;

                var name = pair[0];
                var value = pair[1];

                if (name == "c_user")
                    UserId = value;

                var cookie = new KCookie
                {
                    Host = ".facebook.com",
                    IsSecure = true,
                    Path = "/",
                    RawHost = ".facebook.com",
                    Expiry = DateTimeExtensions.KToUnixTime(DateTime.Now.AddYears(10)),
                    Name = name,
                    Value = value
                };

                Cookies.Add(cookie);
            }
        }

        public void AddCookies(List<KCookie> cookies)
        {
            if (Cookies == null)
                Cookies = new List<KCookie>();

            Cookies.AddRange(cookies);
        }

        public static void CleanCookies(List<KCookie> cookies)
        {
            var removed_cookies = new[] { "m_pixel_ratio", "wd", "noscript", "m_ts", "m_user", "reg_fb_gate", "reg_fb_ref", "csm", "s", "sb", "lu" };
            //var removed_cookies = new[] { "pnl_data" };
            if (cookies != null)
            {
                cookies.RemoveAll(c => removed_cookies.Contains(c.Name));
                cookies.RemoveAll(c => !c.Host.Contains("facebook.com"));
            }
        }

        public void SetNoScriptCookie(bool noscript = true)
        {
            if (Cookies == null)
                Cookies = new List<KCookie>();

            if (noscript && Cookies.All(c => c.Name != "noscript"))
            {
                var cookie = GetNoScriptCookie();
                Cookies.Add(cookie);
            }

            if (!noscript && Cookies != null)
            {
                Cookies.RemoveAll(c => c.Name == "noscript");
            }
        }

        /// <summary>
        /// Get clone cookies from user.
        /// </summary>
        /// <returns></returns>
        public List<KCookie> GetCookies(bool? noscript = null)
        {
            if (Cookies == null)
                Cookies = new List<KCookie>();

            var cookies = Cloner.Clone(Cookies);

            if (noscript.HasValue)
            {
                if (noscript.Value && cookies.All(c => c.Name != "noscript"))
                {
                    var cookie = GetNoScriptCookie();
                    cookies.Add(cookie);
                }

                if (!noscript.Value)
                {
                    cookies.RemoveAll(c => c.Name == "noscript");
                }
            }

            return cookies;
        }

        public string CheckGetUserIdFromCookies()
        {
            if (UserId.NotNullOrWhiteSpace())
                return UserId;

            if (Cookies == null || Cookies.Count <= 0)
                return null;

            var c_user = Cookies.Find(c => c.Name == "c_user");
            if (c_user == null)
                return null;

            UserId = c_user.Value;
            return UserId;
        }

        public bool IsNoScript()
        {
            if (Cookies == null)
                return false;

            var noscript = Cookies.FirstOrDefault(c => c.Name == "noscript");
            return noscript != null && noscript.Value == "1";
        }

        public void VerifyCookies()
        {
            // set domain value for all cookies
            if (Cookies != null)
            {
                Cookies.ForEach(c =>
                {
                    if (c.Host.IsNullOrWhiteSpace())
                        c.Host = ".facebook.com";
                });
            }
        }

        public static KCookie GetNoScriptCookie()
        {
            var cookie = new KCookie
            {
                Name = "noscript",
                Value = "1",
                Path = "/",
                IsSession = true,
                CreationTime = DateTime.Now,
                Host = ".facebook.com",
                RawHost = ".facebook.com"
            };

            return cookie;
        }








        /// <summary>
        /// Get simple version of current user, with some identity properties.
        /// </summary>
        /// <returns></returns>
        public FacebookUser GetIdentityUser()
        {
            return new FacebookUser()
            {
                UserId = UserId,
                Username = Username,
                FullName = FullName
            };
        }

        /// <summary>
        /// Check whether user has identity information : userid or username
        /// </summary>
        /// <returns></returns>
        public bool HasIdentity()
        {
            return !UserId.IsNullOrWhiteSpace() || !Username.IsNullOrWhiteSpace();
        }
        
        public FacebookVerification GetToken()
        {
            return iPhoneVerification ?? AndroidVerification;
        }

        public bool HasToken()
        {
            return GetToken() != null;
        }

        public bool HasCoverImage()
        {
            return !CoverPhoto.IsNullOrWhiteSpace();
        }

        public List<KCookie> GetTokenCookies()
        {
            var veri = GetToken();
            return veri?.Cookies;
        }

        public bool HasTokenCookies()
        {
            var cookies = GetTokenCookies();
            return cookies != null;
        }

        public string GetTokenUserAgent()
        {
            var veri = GetToken();
            var userAgent = veri?.UserAgent;
            if (userAgent.NotNullOrWhiteSpace())
            {
                userAgent = EnsureUSUserAgent(userAgent);
                veri.UserAgent = userAgent;
            }

            return userAgent;
        }

        public bool HasTokenUserAgent()
        {
            var ua = GetTokenUserAgent();
            return !ua.IsNullOrWhiteSpace();
        }

        public bool UseTokenUserAgent()
        {
            if (HasTokenUserAgent())
            {
                UserAgent = GetTokenUserAgent();
                UserAgent = EnsureUSUserAgent(UserAgent);

                return true;
            }

            return false;
        }

        private string EnsureUSUserAgent(string userAgent)
        {
            if (userAgent.NotNullOrWhiteSpace())
            {
                var langs = new[] { "th_TH", "zh_TW", "fr_FR", "tr_TR", "id_ID", "zh_CN", "th_TH", "es_LA" };
                langs.ForEach(lang => userAgent = userAgent.Replace(lang, "en_US"));
            }

            return userAgent;
        }

        public bool UseTokenCookies()
        {
            if (HasTokenCookies())
            {
                Cookies = GetTokenCookies();
                return true;
            }

            return false;
        }








        
        public FacebookUser()
        {
            Active = true;
            Blocked = false;

            //if (WifiName.IsNullOrWhiteSpace())
            //    WifiName = $"{FullName}'s Wifi";

            //if (DeviceName.IsNullOrWhiteSpace())
            //    DeviceName = FullName;

            //if (WifiBSSID.IsNullOrWhiteSpace())
            //    WifiBSSID = KSS.Patterns.Network.NetworkUtilities.GetRandomMacAddress();
        }


        public void AddFriendIds(string friendId)
        {
            if (FriendIds == null)
                FriendIds = new List<string>();

            if (FriendIds.Contains(friendId))
                return;

            FriendIds.Add(friendId);
        }

        public void RemoveFriendId(string friendId)
        {
            if (FriendIds == null)
                return;

            FriendIds.Remove(friendId);
        }

        #region Trusted Friends

        public bool HasTrustedFriends()
        {
            return TrustedFriends != null && TrustedFriends.Count > 0;
        }

        public void AddTrustedFriend(string friendId)
        {
            if (TrustedFriends == null)
                TrustedFriends = new List<string>();

            if (!TrustedFriends.Contains(friendId))
                TrustedFriends.Add(friendId);
        }

        public void RemoveTrustedFriend(string friendId)
        {
            if (TrustedFriends != null && TrustedFriends.Contains(friendId))
                TrustedFriends.Remove(friendId);
        }

        public void ClearTrustedFriends()
        {
            TrustedFriends = null;
        }

        public bool IsTrustedFriend(string friendId)
        {
            var isFriend = TrustedFriends != null && TrustedFriends.Contains(friendId);
            return isFriend;
        }

        public bool AreTrustedFriends(List<string> friendIds)
        {
            var areFriend = friendIds.All(IsTrustedFriend);
            return areFriend;
        }

        #endregion

    }
}