using IMH.Domain.Facebook;
using KSS.Patterns.Encode;
using KSS.Patterns.Encryption;
using KSS.Patterns.Extensions;
using KSS.Patterns.Web;
using KSS.Patterns.WebAutomation;
using MarketDragon.Automation.Common;
using MarketDragon.Automation.Social.Facebook;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;

namespace MMOApp.FB.BLL
{
    public class FacebookPrivateAPI
    {
        public const string SERVICE_URL = "https://graph.facebook.com/restserver.php";
        public const int DEFAULT_TIMEOUT = 120; // in sec

        private FacebookVerification verification;
        private string accessToken;
        private FacebookUser user;
        private int timeout = DEFAULT_TIMEOUT;

        public string UserId { get; private set; }
        public string Proxy { get; set; }
        public string ProxyUsername { get; set; }
        public string ProxyPassword { get; set; }
        public bool IsSocksProxy { get; set; }
        public string ServerIP { get; set; }
        public FacebookUser User { get { return user; } }

        public FacebookPrivateAPI() { }

        public FacebookPrivateAPI(FacebookUser account) : this(account.GetToken())
        {
            user = account;
            if (account.IsUsingProxy())
                SetProxy(account.Proxy, account.ProxyUsername, account.ProxyPassword);

            if (UserId.IsNullOrWhiteSpace())
                UserId = user.UserId;
        }

        public FacebookPrivateAPI(FacebookVerification verification)
        {
            this.verification = verification;
            UserId = verification.UserId;
            accessToken = verification.AccessToken;
        }

        public static FacebookPrivateAPI Create(FacebookUser account)
        {
            return new FacebookPrivateAPI(account);
        }

        public void SetProxy(string proxy, string username = null, string password = null, bool isSocks = false)
        {
            Proxy = proxy;
            ProxyUsername = username;
            ProxyPassword = password;
            IsSocksProxy = isSocks;
        }

        private string BuildSignature(Dictionary<string, string> urlParams, string apiSecret)
        {
            var param = urlParams.OrderBy(u => u.Key).ToDictionary(u => u.Key, u => u.Value);

            var sbParam = new StringBuilder();
            param.ForEach(d => sbParam.Append(d.Key + "=" + d.Value));
            sbParam.Append(apiSecret);

            var sig = EncryptionUtils.GetMd5Sum(sbParam.ToString());

            return sig.ToLowerInvariant();
        }

        /// <summary>
        /// Check whether account get blocked and throw exception base on api result.
        /// </summary>
        /// <param name="content"></param>
        private void CheckAccountBlocked(string content)
        {
            try
            {
                dynamic result = JsonConvert.DeserializeObject(content);
                if (result.error.type == "OAuthException")
                {
                    throw new AccountBlockedException((string)result.error.message, UserId, null);
                }
            }
            catch (AccountBlockedException)
            {
                throw;
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Set request timeout in second.
        /// </summary>
        /// <param name="timeout"></param>
        public void SetTimeout(int timeout)
        {
            this.timeout = timeout;
        }


        public FacebookVerification VerifyApp(string email, string password)
        {
            var param = new Dictionary<string, string>
            {
                {"api_key", "3e7c78e35a76a9299309885393b02d97"},
                {"credentials_type" , "password"},
                {"email", email},
                {"format", "JSON"},
                {"generate_machine_id" , "1"},
                {"generate_session_cookies" , "1"},
                {"locale", "en_us"},
                {"method", "auth.login"},
                {"password", password},
                {"return_ssl_resources", "0"},
                {"v", "1.0"},
            };

            // sign data
            param["sig"] = BuildSignature(param, "c1e620fa708a1d5696fb991c1bde5662");

            var web = WebFactory.CreatexNetWeb();
            if (!Proxy.IsNullOrWhiteSpace())
            {
                web.Proxy = Proxy;
                web.ProxyUsername = ProxyUsername;
                web.ProxyPassword = ProxyPassword;
            }

            var userAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 10_1_1 like Mac OS X) AppleWebKit/602.2.14 (KHTML, like Gecko) Mobile/14B100 [FBAN/FBIOS;FBAV/75.0.0.48.61;FBBV/45926345;FBRV/0;FBDV/iPhone7,2;FBMD/iPhone;FBSN/iOS;FBSV/10.1.1;FBSS/2;FBCR/TelenorMyanmar;FBID/phone;FBLC/en_US;FBOP/5]";
            web.UserAgent = userAgent;
            //var response = web.GetResponse(SERVICE_URL, param);

            var query = Domainer.BuildUrlQuery(param);
            var url = SERVICE_URL + "?" + query;
            var response = web.Get(url);

            dynamic result = JsonConvert.DeserializeObject(response);
            var error_msg = string.Empty;
            try
            {
                error_msg = result.error_msg;
            }
            catch (Exception) { }

            if (!error_msg.IsNullOrWhiteSpace())
            {
                LoginException.Error error = LoginException.Error.Unknown;
                var code = result.error_code;

                if (code == "405" || code == "407" || code == "401")
                    error = LoginException.Error.VerificationRequired;

                if (code == "400")
                    error = LoginException.Error.WrongUsernameOrPassword;

                if (code == "10")
                    error = LoginException.Error.AccountDisabled;

                if (error != LoginException.Error.Unknown)
                    throw new LoginException("Facebook", email, password, "Cannot login to Facebook", error);

                throw new AuthenticationException((string)result.error_msg);
            }

            var random = new Random();
            var deviceId = Guid.NewGuid().ToString().ToLowerInvariant();

            var veri = new FacebookVerification
            {
                UserId = result.uid,
                SessionKey = result.session_key,
                Secret = result.secret,
                AccessToken = result.access_token,
                MachineId = result.machine_id,
                DeviceId = deviceId,
                UserAgent = userAgent,
                Active = true,
                Blocked = false
            };

            try
            {
                foreach (dynamic cookie in result.session_cookies)
                {
                    if (veri.Cookies == null)
                        veri.Cookies = new List<KCookie>();

                    var wcookie = new KCookie
                    {
                        Name = cookie.name,
                        Value = cookie.value,
                        Expiry = long.Parse((string)cookie.expires_timestamp),
                        Host = cookie.domain,
                        Path = ((string)cookie.path).Remove(@"\"),
                        IsSecure = bool.Parse((string)cookie.secure)
                    };

                    veri.Cookies.Add(wcookie);
                }
            }
            catch (Exception e) { }

            try
            {
                veri.UserStorageKey = result.user_storage_key;
            }
            catch (Exception) { }

            return veri;
        }


        private IWeb CreateOAuthWeb()
        {
            return CreateOAuthWeb(verification?.AccessToken ?? accessToken, Proxy, ProxyUsername, ProxyPassword);
        }

        private IWeb CreateOAuthWeb(string accessToken, string proxy, string proxyUsername = null, string proxyPassword = null, bool isSocks = false)
        {
            var web = CreateWeb(proxy, proxyUsername, proxyPassword, isSocks);

            if (accessToken.NotNullOrWhiteSpace())
                web.AddHeader("authorization", "OAuth " + accessToken);

            string user_agent = "Dalvik/1.6.0 (Linux; U; Android 5.1.1; Google Nexus 4 - 5.1.1 - API 16 - 768x1280 Build/JRO03S) [FBAN/FB4A;FBAV/50.0.0.10.54;FBPN/com.facebook.katana;FBLC/en_US;FBBV/16053538;FBCR/Android;FBMF/Genymotion;FBBD/generic;FBDV/Google Nexus 4 - 4.1.1 - API 16 - 768x1280;FBSV/4.1.1;FBCA/x86:armeabi-v7a;FBDM/{density=2.0,width=768,height=1184};FB_FW/1;]";
            web.UserAgent = (verification != null) ? (verification.UserAgent ?? user_agent) : user_agent;

            return web;
        }

        private IWeb CreateWeb()
        {
            return CreateWeb(Proxy, ProxyUsername, ProxyPassword, IsSocksProxy);
        }

        private IWeb CreateWeb(string proxy, string proxyUsername = null, string proxyPassword = null, bool isSocks = false)
        {
            if (isSocks)
                throw new NotImplementedException();

            //var web = IsSocksProxy ? (IWeb)WebFactory.CreateSocksWeb() : WebFactory.CreatexNetWeb();
            var web = WebFactory.CreatexNetWeb();
            web.SetTimeout(timeout * 1000);

            if (!proxy.IsNullOrWhiteSpace())
            {
                web.Proxy = proxy;
                web.ProxyUsername = proxyUsername;
                web.ProxyPassword = proxyPassword;
            }

            return web;
        }

        private string GenerateGuid()
        {
            return Guid.NewGuid().ToString().ToLowerInvariant();
        }

        private void ThrowIfException(dynamic result)
        {
            string errorMessage = GetExceptionMessage(result);

            if (errorMessage.NotNullOrWhiteSpace())
                throw new Exception(errorMessage);
        }

        private string GetExceptionMessage(dynamic result)
        {
            Func<object, string> getOneError = data =>
            {
                dynamic error = data;
                var hasMessage = DynamicHelper.Has(() => error.message);
                var isError1 = (DynamicHelper.Has(() => error.severity) && error.severity == "CRITICAL");
                var isError2 = (DynamicHelper.Has(() => error.error_subcode) && DynamicHelper.Has(() => error.error_data));
                var isError = hasMessage && (isError1 || isError2);

                if (isError)
                {
                    var message = new StringBuilder($"[{error.message}]");

                    if (DynamicHelper.Has(() => error.summary))
                        message.Append($" [{error.summary}]");

                    if (DynamicHelper.Has(() => error.description))
                        message.Append($" [{error.description}]");

                    if (DynamicHelper.Has(() => error.debug_info))
                        message.Append($" [{error.debug_info}]");

                    if (DynamicHelper.Has(() => error.error_user_title))
                        message.Append($" [{error.error_user_title}]");

                    if (DynamicHelper.Has(() => error.error_user_msg))
                        message.Append($" [{error.error_user_msg}]");

                    return message.ToString();
                }

                return null;
            };

            Func<object, string> getError = data =>
            {
                dynamic d = data;
                var error = DynamicHelper.TryGet(() => d.error, null);
                if (error != null)
                {
                    return getOneError(error);
                }

                return null;
            };

            Func<object, string> getBodyError = data =>
            {
                dynamic d = data;
                var error = DynamicHelper.TryGet(() => result[0][1].body.error, null);
                if (error != null)
                {
                    return getOneError(error);
                }

                return null;
            };

            Func<object, string> getErrors = data =>
            {
                dynamic d = data;
                var errors = DynamicHelper.TryGet(() => d.errors, null);
                var message = errors != null ? ((IEnumerable<object>)errors).Select(getOneError).FirstOrDefault(err => err.NotNullOrWhiteSpace()) : null;

                return message;
            };

            Func<object, string> getFqlError = data =>
            {
                dynamic d = data;
                var errorCode = DynamicHelper.TryGet(() => d.error_code, null);
                var message = DynamicHelper.TryGet(() => d.error_msg, null);
                return message;
            };


            string errorMessage = getError(result) ?? getErrors(result) ?? getFqlError(result) ?? getBodyError(result);

            if (!errorMessage.IsNullOrWhiteSpace())
            {
                var tokens = new[]
                {
                    "validating access token",
                    "The user is enrolled in a blocking",
                    "validating access token"
                };

                if (tokens.Any(errorMessage.Contains))
                    throw new AccessTokenBlockedException();

                var tokens2 = new[]
                {
                    "The action attempted has been deemed abusive or is otherwise disallowed"
                };

                if (tokens2.Any(errorMessage.Contains))
                    throw new ActionDisallowedException();

                return errorMessage;
            }

            return null;
        }

        //private string DecodeText(string text)
        //{
        //    text = HtmlEncoder.DecodeEncodedNonAsciiCharacters(text);

        //    return text;
        //}

        private string FormatUrl(string url)
        {
            url = url.Replace("\\/", "/");
            return url;
        }





        public object RunFQL(string fql, string friendlyName = null, string callerClass = null)
        {
            var param = new Dictionary<string, string>
            {
                {"query", fql},
                {"format", "JSON"},
                {"locale", "en_US"},
                {"client_country_code", "US"},
                {"method", "fql.query"}
            };

            if (friendlyName.NotNullOrWhiteSpace())
                param.AddOrUpdate("fb_api_req_friendly_name", friendlyName);

            if (callerClass.NotNullOrWhiteSpace())
                param.AddOrUpdate("fb_api_caller_class", callerClass);

            var url = "https://api.facebook.com/method/fql.query";
            var web = CreateOAuthWeb();
            var content = web.Post(url, param);
            object result = JsonConvert.DeserializeObject(content);

            return result;
        }

        public List<FacebookPhoto> GetPhotosHasUserTagged(string userId, out string errorMessage)
        {
            var query = $"select owner,src_big,src_big_width,src_big_height,link,object_id,album_object_id from photo where pid in (select pid from photo_tag where subject = \"{userId}\")";
            dynamic result = RunFQL(query);

            errorMessage = GetExceptionMessage(result);
            if (errorMessage.NotNullOrWhiteSpace())
                return null;

            var photos = new List<FacebookPhoto>();
            foreach (var item in result)
            {
                var photo = new FacebookPhoto
                {
                    PhotoId = item.object_id,
                    FullPhotolUrl = item.src_big,
                    Width = item.src_big_width,
                    Height = item.src_big_height,
                    AlbumId = item.album_object_id,
                    DetailUrl = item.link
                };

                photos.Add(photo);
            }

            return photos;
        }

        public List<FacebookUser> GetFriendListFQL(string userId)
        {
            var query = $"select uid, name, friend_count from user where uid in (select uid2 from friend where uid1={userId})";
            var users = GetUsersFQL(query);

            return users;
        }

        public List<FacebookUser> GetUsersFQL(string query)
        {
            var data = RunFQL(query);

            ThrowIfException(data);

            var users = FQLParseUsers(data);

            return users;
        }



        /// <summary>
        /// Parse a user json data returned by calling FQL query table [user]
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private FacebookUser FQLParseUser(object data)
        {
            dynamic ddata = data;
            var user = new FacebookUser
            {
                UserId = ddata.uid,
                FullName = ddata.name,

                Email = ddata.email,
                TotalFriends = ddata.friend_count == null ? 0 : (int)ddata.friend_count,
                Url = ddata.profile_url,
                ProfileImageUrl = ddata.pic_big
            };


            return user;
        }

        private List<FacebookUser> FQLParseUsers(object data)
        {
            var users = ((IEnumerable<dynamic>)data).Select(FQLParseUser).ToList();
            return users;
        }

        public bool CheckAlive()
        {
            //var web = CreateOAuthWeb();
            var web = WebFactory.CreatexNetWeb();

            string message = string.Empty;

            try
            {
                var content = web.Get($"https://graph.facebook.com/me?access_token={verification.AccessToken}");
                dynamic result = JsonConvert.DeserializeObject(content);
                try
                {
                    var uid = (string)result.id;
                    if (uid.NotNullOrWhiteSpace())
                        return true;

                    //message = (string)result.error.message;
                    //if (message.Contains("The user is enrolled in a blocking") || message.Contains("Error validating access token"))
                    //    return false; // use blocked
                }
                catch (Exception)
                {
                    return false;
                }
            }
            catch (WebResponseException e)
            {
                return false;
            }

            return true;
        }

        public bool CheckAliveFQL(out string errorMessage)
        {
            errorMessage = null;

            try
            {
                var alive = CheckAliveFQL(UserId, out errorMessage);
                return alive;
            }
            catch (AccessTokenBlockedException)
            {
            }

            return false;
        }

        /// <summary>
        /// Check user alive using FQL.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool CheckAliveFQL(string userId, out string errorMessage)
        {
            var ids = CheckAliveFQL(new List<string> { userId }, out errorMessage);
            var alive = ids != null && ids.Contains(userId);

            return alive;
        }

        /// <summary>
        /// Check users alive using FQL.
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public List<string> CheckAliveFQL(List<string> userIds, out string errorMessage)
        {
            var ids = string.Join("\",\"", userIds);
            var query = $"select uid from user where uid in (\"{ids}\")";
            var result = RunFQL(query);

            errorMessage = GetExceptionMessage(result);
            if (errorMessage.NotNullOrWhiteSpace())
                return null;

            var liveIds = DynamicHelper.TryGet(() => ((IEnumerable<dynamic>)result).Select(i => (string)i.uid).ToList(), new List<string>());

            return liveIds;
        }

        public FacebookUser UserGetFullInfoFQL(string userId)
        {
            var query = $"select uid, first_name, last_name, name, locale, pic_big, pic_cover, profile_url, timezone, religion, birthday, sex, verified, friend_count, email from user where uid = {userId}";

            dynamic data = RunFQL(query);

            try
            {
                var user = FQLParseFullUser(data[0]);
                return user;
            }
            catch (Exception)
            {
            }

            return null;
        }

        private FacebookUser FQLParseFullUser(object data)
        {
            dynamic ddata = data;
            var user = new FacebookUser
            {
                UserId = ddata.uid,
                FullName = ddata.name,
                FirstName = ddata.first_name,
                LastName = ddata.last_name,

                Email = ddata.email,
                DOB = ddata.birthday == null ? null : DateTime.Parse(ddata.birthday),
                TotalFriends = ddata.friend_count == null ? 0 : (int)ddata.friend_count,
                Url = ddata.profile_url,
                ProfileImageUrl = ddata.pic_big,
                CoverPhotoUrl = ddata.pic_cover.source,
                Locale = ddata.locale,
                TimeZone = ddata.timezone,
                Religion = ddata.religion,
                Verified = ddata.verified == null ? false : (bool)ddata.verified
            };

            // gender
            var sgender = ddata.sex;
            if (sgender == "male")
                user.Gender = true;
            else if (sgender == "female")
                user.Gender = false;

            // username
            var username = user.Url;
            if (!username.Contains("?") && !username.Contains("="))
            {
                user.Username = username.Split('/').Last();
            }

            return user;
        }

    }
}