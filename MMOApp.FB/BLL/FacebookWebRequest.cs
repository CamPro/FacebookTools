using HtmlAgilityPack;
using IMH.Domain.Facebook;
using KSS.Patterns.Environment;
using KSS.Patterns.Extensions;
using KSS.Patterns.Web;
using KSS.Patterns.WebAutomation;
using MarketDragon.Automation.Common;
using MarketDragon.Automation.Social.Facebook;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace MMOApp.FB.BLL
{
    public class FacebookWebRequest : BaseWebRequest
    {

        private static Dictionary<string, FacebookWebRequest> cacheRequests;

        private readonly FacebookParser parser;
        private readonly FacebookUrlParser urlParser;

        private readonly string[] desktopUserAgents;

        public FacebookWebRequest() : this(null, null)
        {
        }

        public FacebookWebRequest(List<KCookie> cookies, string userAgent, int timeout = 30 * 1000) : this(cookies, userAgent, null, null, null, false, timeout)
        {
        }

        public FacebookWebRequest(List<KCookie> cookies, string userAgent, string proxy, bool useSocks = false, int timeout = 30 * 1000) : this(cookies, userAgent, proxy, null, null, useSocks, timeout)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cookies">Account cookies</param>
        /// <param name="userAgent">PC user agent, can be empty</param>
        /// <param name="useSocks"></param>
        /// <param name="proxy"></param>
        public FacebookWebRequest(List<KCookie> cookies, string userAgent, string proxy, string proxyUsername = null, string proxyPassword = null, bool useSocks = false, int timeout = 30 * 1000) :
            base(cookies, userAgent, proxy, proxyUsername, proxyPassword, useSocks, timeout)
        {
            desktopUserAgents = new[]
            {
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36"
            };

            parser = new FacebookParser();
            urlParser = new FacebookUrlParser();
        }

        public static FacebookWebRequest Create(FacebookUser account, bool? noscript = null)
        {
            if (account.Cookies == null)
                return null;

            //if (account.IsSingleProxyConnection && !account.Proxy.IsNullOrWhiteSpace())
            //{
            //    if (cacheRequests == null)
            //        cacheRequests = new Dictionary<string, FacebookWebRequest>();

            //    if (cacheRequests.ContainsKey(account.Proxy))
            //        return cacheRequests[account.Proxy];
            //}

            //account.CleanCookies();
            var cookies = account.GetCookies(noscript);
            var proxy = account.UseProxy.HasValue && account.UseProxy.Value && !account.Proxy.IsNullOrWhiteSpace() ? account.Proxy : null;
            var isSocks = !proxy.IsNullOrWhiteSpace() && account.IsSockProxy.HasValue && account.IsSockProxy.Value;

            var request = Create(cookies, account.UserAgent, proxy, account.ProxyUsername, account.ProxyPassword, isSocks);

            //if (account.IsSingleProxyConnection && !account.Proxy.IsNullOrWhiteSpace())
            //{
            //    cacheRequests.Add(account.Proxy, request);
            //}

            return request;
        }

        public static FacebookWebRequest Create(List<KCookie> cookies, string userAgent)
        {
            return Create(cookies, userAgent, null, false);
        }

        public static FacebookWebRequest Create(List<KCookie> cookies, string userAgent, string proxy = null, bool useSocks = false)
        {
            return Create(cookies, userAgent, proxy, null, null, useSocks);
        }

        public static FacebookWebRequest Create(List<KCookie> cookies, string userAgent, string proxy = null, string proxyUsername = null, string proxyPassword = null, bool useSocks = false)
        {
            MakeSureFacebookCookies(cookies);
            return new FacebookWebRequest(cookies, userAgent, proxy, proxyUsername, proxyPassword, useSocks);
        }

        public static void MakeSureFacebookCookies(List<KCookie> cookies)
        {
            var domain = ".facebook.com";
            foreach (var cookie in cookies)
            {
                if (cookie.Host.IsNullOrWhiteSpace())
                    cookie.Host = domain;

                if (cookie.RawHost.IsNullOrWhiteSpace())
                    cookie.RawHost = domain;
            }
        }

        public string MBasicPasswordArquisitionSubmit(string pageSource, string password)
        {
            var root = HtmlHelper.Root(pageSource);
            var form = parser.FindForm(root, "passwordacquisitionqp");
            if (form == null)
                throw new SiteStructureChangedException(GetCurrentUrl(), "password_form", pageSource);

            var queries = parser.FormGetElementsBySibling(form);
            queries.AddOrUpdate("password", password);
            var submitUrl = GetFormUrl(form);
            var content = web.Post(submitUrl, queries);

            return content;
        }


        /// <summary>
        /// Sumit login form.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Login(string email, string password)
        {
            var content = SubmitLogin(email, password);

            var errorMessage = parser.MobileLoginErrorMessage(content);

            if (!errorMessage.IsNullOrWhiteSpace() || web.GetCurrentUrl().Contains("facebook.com/reg/"))
                throw new LoginException("Facebook", email, password, errorMessage, LoginException.Error.WrongUsernameOrPassword);

            if (parser.IsMobileLoginPage(content))
                throw new LoginException("Facebook", email, password, "Cannot login to Facebook");

            if (parser.MobileIsLocked(content))
                throw new AccountBlockedException(email);

            if (parser.IsDisabled(content))
                throw new AccountSuspendedException(email);

            if (parser.IsCheckPoint(content))
                throw new AccountBlockedException(email);

            return true;
        }

        public string SubmitLogin(string email, string password)
        {
            var content = web.Get(FBCommon.MBASIC_HOME_URL);
            content = AutoRedirect(content);

            if (!parser.IsMobileLoginPage(content))
                return null;

            var root = HtmlHelper.Root(content);
            var form = parser.MobileLoginGetForm(root);
            if (form == null)
                throw new SiteStructureChangedException(FBCommon.MBASIC_HOME_URL, "Login form");

            var submitUrl = GetFormUrl(form);
            var queries = parser.FormGetElementsBySibling(form);
            queries["email"] = email;
            queries["pass"] = password;
            var content2 = web.Post(submitUrl, queries);

            return content2;
        }


        /// <summary>
        /// Try to unblock account.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pageSource"></param>
        /// <returns></returns>
        public string MBasicTryUnblock(FacebookUser account,
                                        string pageSource,
                                        Func<string, string> solvePhotoUpload,
                                        Func<CheckpointPhotosOfFriendsData, string> solveFriendsPhotos,
                                        Func<string, List<string>> solveTrustedContacts,
                                        Action updateAccount)
        {
            pageSource = SubmitCheckPointFormUntilCanntGo(pageSource);

            SaveFile($"{account.UserId}_before_unlock", pageSource);

            if (parser.MBasicIsCheckpointVerificationMethodSelectionPage(pageSource))
            {
                if (parser.MBasicCheckpointMethodHasIdentifyPhotosOfFriends(pageSource) && solveFriendsPhotos != null)
                {
                    pageSource = MBasicSubmitChooseVerificaionMethodPhotosOfFriends(pageSource);
                    pageSource = MBasicUnBlockPhotosOfFriendsCheckPoint(account, pageSource, solveFriendsPhotos);
                }
            }
            else if (parser.MBasicIsFriendSelectionPage(pageSource) && solveFriendsPhotos != null)
            {
                pageSource = MBasicUnBlockPhotosOfFriendsCheckPoint(account, pageSource, solveFriendsPhotos);
            }

            if (parser.MBasicIsCheckpointPhotoUpload(pageSource))
            {
                pageSource = solvePhotoUpload(pageSource);
            }

            SaveFile($"{account.UserId}_after_unlock", pageSource);

            if (parser.MBasicIsPasswordChangingPage(pageSource))
            {
                pageSource = MBasicChangePassword(account, pageSource);
                pageSource = SubmitCheckPointFormUntilCanntGo(pageSource);
                updateAccount?.Invoke();
            }

            SaveFile($"{account.UserId}_after_change_pass", pageSource);

            return pageSource;
        }

        private string MBasicSubmitChooseVerificaionMethodPhotosOfFriends(string pageSource)
        {
            pageSource = MBasicSubmitChooseVerificaionMethod(pageSource, "3"); // photos of friends

            // skip mediate pages
            pageSource = SubmitCheckPointFormUntilCanntGo(pageSource);

            return pageSource;
        }

        private string MBasicSubmitChooseVerificaionMethod(string pageSource, string method)
        {
            Action<Dictionary<string, string>> removeLogout = qr =>
            {
                var lgout = qr.Keys.FirstOrDefault(k => k.Contains("logout"));
                if (lgout != null)
                    qr.Remove(lgout);
            };

            // choose verify method
            var isMethodSelect = parser.MBasicIsCheckpointVerificationMethodSelectionPage(pageSource);
            if (!isMethodSelect)
                return pageSource;

            var root = HtmlHelper.Root(pageSource);
            var form = MBasicGetCheckpointForm(root);
            var queries = parser.FormGetElementsBySibling(form);
            queries.AddOrUpdate("verification_method", method);
            removeLogout(queries);

            var submitUrl = GetFormUrl(form);
            pageSource = web.Post(submitUrl, queries);

            return pageSource;
        }

        public string MBasicUnBlockPhotosOfFriendsCheckPoint(FacebookUser account, string pageSource, Func<CheckpointPhotosOfFriendsData, string> solveFriendsPhotos)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            string content = pageSource;
            var connHelper = new ConnectionHelper();

            // solve the name
            int skipped = 0;
            int maxSkip = 2;

            do
            {

                var data = connHelper.TryRequest(() => MBasicParsePhotosOfFriendsCheckpointData(content));
                if (data == null)
                    break;

                var result = solveFriendsPhotos(data);

                var unknown = result.IsNullOrWhiteSpace();
                if (unknown)
                    skipped++;

                var random = unknown && skipped > maxSkip;
                content = connHelper.TryRequest(() => MBasicSubmitFriendNameAtCheckPoint(content, result, random));

            } while (true);

            content = ConfirmLastLogin(content);
            content = SubmitCheckPointFormUntilCanntGo(content);

            return content;
        }

        void SaveFile(string name, string content)
        {
            var folder = Path.Combine(EnvironmentUtils.GetAssemblyDirectory(), "log/html");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var random = new Random();
            var file = Path.Combine(folder, name + "_" + random.AdvancedNextString(5) + ".html");
            File.WriteAllText(file, content);
        }

        private string ConfirmLastLogin(string pageSource)
        {
            var root = HtmlHelper.Root(pageSource);
            var form = parser.FindForm(root, "login/checkpoint");

            if (form == null)
                return pageSource;

            var queries = parser.FormGetElementsBySibling(form);
            queries.RemoveIfContains("submit[No]");

            var lgout = queries.Keys.FirstOrDefault(k => k.Contains("logout"));
            if (lgout != null)
                queries.Remove(lgout);


            var url = GetFormUrl(form);
            var content = web.Post(url, queries);

            return content;
        }

        private CheckpointPhotosOfFriendsData MBasicParsePhotosOfFriendsCheckpointData(string pageSource)
        {
            var photos = new List<string>();
            Action<CheckpointPhotosOfFriendsData> addFriendPhoto = dt => photos.Add(dt.FriendPhotosUrls.First());

            // photo 1
            var data = parser.MBasicParsePhotosOfFriendsCheckPointData(pageSource);
            if (data == null)
                return null;

            addFriendPhoto(data);

            // photo 2
            var root = HtmlHelper.Root(pageSource);
            var form = parser.FindForm(root, "checkpoint/block") ?? parser.FindForm(root, "login/checkpoint");
            var url = GetFormUrl(form);
            var queries = parser.FormGetElementsBySibling(form);
            queries.RemoveIfContains("submit[Continue]");
            queries.RemoveIfContains("submit[Continue][go_to_0]");
            queries.RemoveIfContains("submit[Continue][go_to_2]");
            var content = web.Post(url, queries);
            data = parser.MBasicParsePhotosOfFriendsCheckPointData(content);
            addFriendPhoto(data);

            // photo 3
            root = HtmlHelper.Root(content);
            form = parser.FindForm(root, "checkpoint/block") ?? parser.FindForm(root, "login/checkpoint");
            url = GetFormUrl(form);
            queries = parser.FormGetElementsBySibling(form);
            queries.RemoveIfContains("submit[Continue]");
            queries.RemoveIfContains("submit[Continue][go_to_0]");
            queries.RemoveIfContains("submit[Continue][go_to_1]");
            content = web.Post(url, queries);
            data = parser.MBasicParsePhotosOfFriendsCheckPointData(content);
            addFriendPhoto(data);

            data.FriendPhotosUrls.Clear();
            data.FriendPhotosUrls.AddRange(photos);

            var dweb = web.Clone() as IWeb;
            data.FriendPhotos = new List<Bitmap>();
            data.FriendPhotosUrls.ForEach(photoUrl =>
            {
                var photo = dweb.Download(photoUrl);
                var bitmap = Image.FromStream(new MemoryStream(photo)) as Bitmap;
                data.FriendPhotos.Add(bitmap);
            });

            return data;
        }

        private string MBasicSubmitFriendNameAtCheckPoint(string content, string name, bool randomResult = false)
        {
            var root = HtmlHelper.Root(content);
            var form = parser.FindForm(root, "checkpoint/block") ?? parser.FindForm(root, "login/checkpoint");
            var url = GetFormUrl(form);
            var queries = parser.FormGetElementsBySibling(form);
            queries.RemoveIfContains("submit[Continue][go_to_0]");
            queries.RemoveIfContains("submit[Continue][go_to_1]");
            queries.RemoveIfContains("submit[Continue][go_to_2]");

            var answerNode = root.ChildByName("answer_choices");
            var nameValue = name.IsNullOrWhiteSpace() ? "-1" : answerNode.SearchOptionValeByText(name);

            // select random 1 answer
            if (randomResult)
            {
                var options = answerNode.Options();
                options.RemoveAll(n => n.Value() == "-1");
                nameValue = options.PickRandomOne().Value();
            }

            queries.AddOrUpdate("answer_choices", nameValue);

            content = web.Post(url, queries);

            return content;
        }

        /// <summary>
        /// Submit form until got checkpoint selection page or cannot go to diffrent page.
        /// </summary>
        /// <param name="pageSource"></param>
        /// <returns></returns>
        public string SubmitCheckPointFormUntilCanntGo(string pageSource)
        {
            string action = null;
            string title = null;
            do
            {
                if (parser.MBasicIsCheckpointVerificationMethodSelectionPage(pageSource) ||
                    parser.MBasicIsCheckpointPhotoUpload(pageSource) ||
                    parser.MBasicIsFriendSelectionPage(pageSource) ||
                    parser.MBasicIsTrustedContactCodeSubmissionPage(pageSource))
                    break;

                pageSource = SubmitCheckPointForm(pageSource);
                if (pageSource.IsNullOrWhiteSpace())
                    break;

                var root = HtmlHelper.Root(pageSource);
                var form = MBasicGetCheckpointForm(root);
                if (form == null)
                {
                    break;
                }

                var queries = parser.FormGetElementsBySibling(form);

                // cannot break phone check point
                if (queries.ContainsKey("contact_point") || queries.ContainsKey("captcha_response") || queries.ContainsKey("password_new"))
                    break;

                var url = form.Action();
                var titleNode = root.Child("title", true, true, true);
                if (action == url && title == titleNode.InnerText.Trim())
                    break;

                action = url;
                title = titleNode.InnerText.Trim();

            } while (true);

            return pageSource;
        }

        public string SubmitCheckPointForm(string pageSource)
        {
            var root = HtmlHelper.Root(pageSource);
            var form = MBasicGetCheckpointForm(root);
            if (form != null)
            {
                var queries = parser.FormGetElementsBySibling(form);
                var lgout = queries.Keys.FirstOrDefault(k => k.Contains("logout"));
                if (lgout != null)
                    queries.Remove(lgout);

                queries.RemoveIfContains("submit[_footer]");
                queries.RemoveIfContains("submit[Back]");

                if (queries.ContainsKey("HackedCleanupEmails[]"))
                    queries["HackedCleanupEmails[]"] = Domainer.HtmlDecode(queries["HackedCleanupEmails[]"]);

                var url = GetFormUrl(form);
                pageSource = web.Post(url, queries);
                return pageSource;
            }

            return null;
        }

        private HtmlNode MBasicGetCheckpointForm(HtmlNode root)
        {
            return parser.FindForm(root, "/checkpoint");
        }

        public string MBasicSubmitPhotoUpload(string pageSource, byte[] photo)
        {
            var root = HtmlHelper.Root(pageSource);
            var form = parser.FindForm(root, "checkpoint/block");
            var queries = parser.FormGetElementsBySibling(form);
            queries.RemoveIfContains("photo_input");

            var url = GetFormUrl(form, FBCommon.MBASIC_HOME_URL);

            var content = web.PostMultipart(url, queries, photo, "photo_input", Guid.NewGuid() + ".jpg");

            return content;
        }

        private string MBasicChangePassword(FacebookUser account, string pageSource)
        {
            Action<Dictionary<string, string>> removeLogout = qr =>
            {
                var lgout = qr.Keys.FirstOrDefault(k => k.Contains("logout"));
                if (lgout != null)
                    qr.Remove(lgout);
            };

            var root = HtmlHelper.Root(pageSource);
            var form = MBasicGetCheckpointForm(root);
            var queries = parser.FormGetElementsBySibling(form);
            removeLogout(queries);

            var random = new Random();
            var newPass = random.AdvancedNextString(10);
            queries.RemoveIfContains("submit[Back]");

            if (pageSource.Contains("new_password"))
            {
                queries.AddOrUpdate("old_password", account.Password);
                queries.AddOrUpdate("new_password", newPass);
                queries.AddOrUpdate("new_password_confirm", newPass);
            }
            else
            {
                queries.AddOrUpdate("password_old", account.Password);
                queries.AddOrUpdate("password_new", newPass);
                queries.AddOrUpdate("password_confirm", newPass);
            }

            var submitUrl = GetFormUrl(form);
            pageSource = web.Post(submitUrl, queries);

            if (pageSource.Contains("new_password"))
                throw new WrongPasswordException(account.UserId);

            account.OldPassword = account.Password;
            account.Password = newPass;
            account.Cookies = GetCookies();

            return pageSource;
        }
    }
}