using IMH.Domain.Facebook;
using KSS.Patterns.Extensions;
using KSS.Patterns.ImageProcessing;
using KSS.Patterns.Logging;
using KSS.Patterns.Web;
using KSS.Patterns.WebAutomation;
using MarketDragon.Automation.Common;
using MarketDragon.Automation.Social.Facebook;
using MMOApp.FB.BLL;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MMOApp.FB.Common
{
    public class Business
    {
        private AppData appData;

        public Business()
        {
            appData = new AppData();
        }


        private const string DEFAULT_USER_AGENT = WebFactory.DefaultUserAgent;

        private FacebookWebRequest CreateWeb(FacebookUser account, bool? noscript = null)
        {
            return FacebookWebRequest.Create(account, noscript);
        }

        private FacebookWebRequest CreateWeb(List<KCookie> cookies, string userAgent)
        {
            return FacebookWebRequest.Create(cookies, userAgent);
        }


        public void SetUserAgent(FacebookUser account)
        {
            account.UserAgent = WebFactory.DefaultUserAgent;
        }

        public void BlockAccount(FacebookUser account, bool active = true)
        {
            account.Block();

            if (!active)
                account.Disable();
        }

        public void DisableAccount(FacebookUser account)
        {
            account.Disable();
        }

        public bool CheckAlive(FacebookUser account, bool autoUnlock = true, FacebookUser viewer = null, Action<string, LogMode, Exception> log = null)
        {
            account.CleanCookies();
            account.CheckGetUserIdFromCookies();

            var fweb = CreateWeb(account, false) ?? new FacebookWebRequest(null, account.UserAgent);

            var url = FBCommon.MBASIC_HOME_URL;
            var content = fweb.Get(url);
            content = fweb.AutoRedirect(content);

            var parser = new FacebookParser();

            var qpLink = HtmlHelper.Root(content).ChildrenLink().FirstOrDefault(l => l.Href().Contains("/qp/action/close/?qp_id="));
            if (qpLink != null)
            {
                var qpUrl = FBCommon.BuildMobileBasicLink(qpLink.Href());
                content = fweb.Get(qpUrl);
                content = fweb.AutoRedirect(content);
            }

            var isEmailConfirm = parser.IsEmailConfirmPage(content);
            var isPasswordArquisition = parser.IsPasswordArquisition(content);
            var isLogin = parser.IsMobileLoginPage(content);
            var isCheckpoint = parser.IsCheckPoint(content);
            var isLocked = parser.MobileIsLocked(content);
            var isWizard = parser.IsWizardPage(content);
            var english = parser.IsMBasicEnglishInterface(content);


            if (isEmailConfirm || isWizard)
                return true;

            if (isPasswordArquisition)
            {
                content = fweb.MBasicPasswordArquisitionSubmit(content, account.Password);
            }

            if ((isLocked || isCheckpoint) && autoUnlock)
            {
                var success = UnlockAccount(account, viewer, content, fweb.GetCurrentUrl(), log);
                if (success)
                {
                    isLocked = isCheckpoint = isLogin = false;
                    account.Cookies = fweb.GetCookies();
                }
            }

            if (!isLogin && !isCheckpoint && !isLocked)
            {
                content = fweb.Get(FBCommon.MBASIC_HOME_URL);

                string userid, username;
                parser.MBasicParseUserIdentity(content, out userid, out username);
                if (userid.IsNullOrWhiteSpace())
                {
                    account.UserAgent = null;
                    SetUserAgent(account);
                    return CheckAlive(account, autoUnlock, viewer);
                }

                account.Active = true;
                account.Blocked = false;
                account.UserId = userid;
                account.Username = username;
                account.Cookies = fweb.GetCookies();
                account.SetNoScriptCookie(false);

                return true;
            }

            if (isCheckpoint || isLocked)
            {
                account.CheckPointType = parser.DefineCheckPointType(content);
                if (account.CheckPointType == CheckPointType.Disabled)
                {
                    DisableAccount(account);
                }
                else
                {
                    BlockAccount(account);
                }

                return false;
            }


            try
            {
                var success = fweb.Login(account.Email ?? account.UserId, account.Password);

                if (!success)
                    return false;
            }
            catch (Exception e)
            {
                account.Cookies = fweb.GetCookies();

                if (e is LoginException)
                {
                    account.CheckPointType = CheckPointType.Disabled;
                    DisableAccount(account);
                }
                else if (e is AccountBlockedException)
                {
                    var success = autoUnlock && UnlockAccount(account, viewer, log);
                    if (success)
                    {
                        account.Cookies = fweb.GetCookies();
                        return true;
                    }
                    else
                    {
                        BlockAccount(account);
                    }
                }
                else if (e is AccountSuspendedException)
                {
                    BlockAccount(account, false);
                }

                return false;
            }

            account.Active = true;
            account.Blocked = false;
            account.Cookies = fweb.GetCookies();
            account.CheckGetUserIdFromCookies();

            return true;
        }

        /// <summary>
        /// Unlock an user from dob and friends' photos checkpoint.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="viewer">User that looking for friends' photos.</param>
        /// <returns></returns>
        public bool UnlockAccount(FacebookUser account, FacebookUser viewer, Action<string, LogMode, Exception> log = null)
        {
            var fweb = CreateWeb(account, true) ?? new FacebookWebRequest(null, account.UserAgent);
            fweb.SetUserAgent(DEFAULT_USER_AGENT); // make sure using mbasic interface


            var url = FBCommon.MBASIC_HOME_URL;
            var content = fweb.Get(url);
            content = fweb.AutoRedirect(content);

            return UnlockAccount(account, viewer, content, fweb.GetCurrentUrl(), log);
        }

        public bool UnlockAccount(FacebookUser account, FacebookUser viewer, string content, string currentUrl, Action<string, LogMode, Exception> log = null)
        {
            var fweb = CreateWeb(account, true) ?? new FacebookWebRequest(null, account.UserAgent);
            fweb.SetCurrentUrl(currentUrl);
            fweb.SetUserAgent(DEFAULT_USER_AGENT); // make sure using mbasic interface

            var parser = new FacebookParser();

            var isLogin = parser.IsMobileLoginPage(content);
            if (isLogin)
            {
                log?.Invoke("Trying login", LogMode.Debug, null);
                content = fweb.SubmitLogin(account.Identity, account.Password);
            }

            isLogin = parser.IsMobileLoginPage(content);
            if (isLogin)
            {
                log?.Invoke("Cannot login", LogMode.Error, null);
                return false;
            }

            log?.Invoke("Trying unlock account", LogMode.Debug, null);
            fweb.MBasicTryUnblock(account,
                                content,
                                (pageSource) => SolveCheckointPhotoUpload(account, pageSource, log),
                                data => SolvePhotosOfFriends(account, data, log),
                                null,
                                null);

            log?.Invoke("Unlock all done, trying to login...", LogMode.Debug, null);

            var url = FBCommon.MBASIC_HOME_URL;
            content = fweb.Get(url);
            content = fweb.AutoRedirect(content);

            isLogin = parser.IsMobileLoginPage(content);
            var isCheckpoint = parser.IsCheckPoint(content);
            var isLocked = parser.MobileIsLocked(content);

            return !isLogin && !isCheckpoint && !isLocked;
        }

        private string SolveCheckointPhotoUpload(FacebookUser account, string pageSource, Action<string, LogMode, Exception> log = null)
        {
            log?.Invoke("Unlock by upload user photo", LogMode.Debug, null);

            var fweb = CreateWeb(account);

            var photoUrl = GetPhoto();

            log?.Invoke($"Upload photo  : [{photoUrl}] ...", LogMode.Debug, null);

            var photo = DownloadFile(photoUrl);

            var content = fweb.MBasicSubmitPhotoUpload(pageSource, photo);

            if (content.Contains("submit[OK]"))
                log?.Invoke($"Photo uploaded!", LogMode.Debug, null);

            return content;
        }

        /// <summary>
        /// Find name of friends in photos
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string SolvePhotosOfFriends(FacebookUser account, CheckpointPhotosOfFriendsData data, Action<string, LogMode, Exception> log = null)
        {
            //if (viewer == null)
            //    throw new ArgumentNullException(nameof(viewer));

            if (log != null)
            {
                var names = string.Join("], [", data.FriendNames);
                log($"Trying to solve friends' photos: [{names}]", LogMode.Debug, null);
            }

            log?.Invoke("All captcha photos downloaded, now comparing...", LogMode.Debug, null);

            string result = null;
            var userId = account.UserId;
            var friends = appData.LoadFriends(userId);
            if (friends == null || friends.Count <= 0)
            {
                log?.Invoke("No friends backup", LogMode.Error, null);
                return null;
            }

            Action<List<Bitmap>, string> solveByBackupPhotos = (captchaBitmaps, name) =>
            {
                // debug - save all photos
                //var folder = Path.Combine(@"D:\temp\fb_fr_captcha", name);
                //if (!Directory.Exists(folder))
                //    Directory.CreateDirectory(folder);

                var nfriends = friends.Where(fr => fr.FullName == name).ToList();
                foreach (var friend in nfriends)
                {
                    if (result.NotNullOrWhiteSpace()) break;

                    var friendId = friend.UserId;

                    var photos = appData.LoadPhotos(friendId, userId);
                    if (photos == null || photos.Count <= 0)
                        continue;

                    bool downloadDone = false;
                    int downloadThreads = Math.Min(photos.Count, 20);

                    var photoUrls = photos.Select(p => p.FullPhotolUrl).Where(url => url.NotNullOrWhiteSpace()).ToConcurrentQueue();
                    var photoBytes = new ConcurrentQueue<byte[]>();

                    Task.Run(() =>
                    {
                        var dtasks = new List<Task>();
                        for (int i = 0; i < downloadThreads; i++)
                        {
                            var dtask = Task.Run(() =>
                            {
                                do
                                {
                                    if (result.NotNullOrWhiteSpace())
                                        break;

                                    string photoUrl;
                                    if (photoUrls.IsEmpty || !photoUrls.TryDequeue(out photoUrl))
                                        break;

                                    try
                                    {
                                        var pbytes = DownloadFile(photoUrl);
                                        if (pbytes.Length > 0)
                                        {
                                            photoBytes.Enqueue(pbytes);
                                            //Console.WriteLine("Donwload photo: " + photoUrl);
                                        }
                                    }
                                    catch (WebException)
                                    {
                                        photoUrls.Enqueue(photoUrl);
                                    }
                                    catch (Exception)
                                    {

                                    }

                                } while (true);
                            });
                            dtasks.Add(dtask);
                        }

                        Task.WaitAll(dtasks.ToArray());
                        downloadDone = true;
                    });


                    // comparing photos
                    do
                    {
                        if (result.NotNullOrWhiteSpace() || (photoBytes.IsEmpty && downloadDone))
                            break;

                        byte[] aphotoBytes;
                        if (photoBytes.IsEmpty || !photoBytes.TryDequeue(out aphotoBytes))
                        {
                            Thread.Sleep(1000);
                            continue;
                        }

                        // do compare
                        using (var frBitmap = ImageProcessor.ToImage(aphotoBytes).ToBitmap())
                        {
                            foreach (var captcha in captchaBitmaps)
                            {
                                if (result.NotNullOrWhiteSpace()) break;

                                using (var mfrphoto = ImageProcessor.ResizeImage(frBitmap, captcha.Size))
                                {
                                    var similari = ImageProcessor.CompareSimilar(mfrphoto, captcha);

                                    // FOUND
                                    if (similari >= 90)
                                        result = name;
                                }
                            }
                        }

                    } while (true);
                }
            };




            var friendNames = data.FriendNames;

            var tasks = new List<Task>();
            foreach (var friendName in friendNames)
            {
                var name = friendName;

                // single thread
                solveByBackupPhotos(data.FriendPhotos, name);
                if (result.NotNullOrWhiteSpace()) break;

                // multiple threads
                //var ccphtos = data.FriendPhotos.Select(p => p.Clone() as Bitmap).ToList();
                //var task = Task.Run(() =>
                //{
                //    solveByBackupPhotos(ccphtos, name);
                //});
                //tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());

            if (log != null)
            {
                if (result.IsNullOrWhiteSpace())
                    log("Cannot solve captcha", LogMode.Error, null);
                else
                    log($"Captcha solve success: [{result}]", LogMode.Debug, null);
            }

            return result;
        }

        public byte[] DownloadPhoto()
        {
            var photoUrl = GetPhoto();
            var data = DownloadFile(photoUrl);
            return data;
        }

        public byte[] DownloadFile(string fileUrl)
        {
            //var client = new WebClient();
            //var data = client.DownloadData(fileUrl);

            var web = WebFactory.CreatexNetWeb();
            web.UserAgent = WebFactory.DefaultUserAgent;
            var data = web.Download(fileUrl);

            return data;
        }

        public string GetPhoto()
        {
            var url = "http://api.facespam.net/getimage.php";

            var web = new xNetWeb();
            var content = web.Get(url);

            return content;
        }

        public FacebookVerification GetiPhoneToken(FacebookUser account)
        {
            var api = new FacebookPrivateAPI();

            if (account.IsUsingProxy())
                api.SetProxy(account.Proxy, account.ProxyUsername, account.ProxyPassword);

            var connHelper = new ConnectionHelper();
            FacebookVerification verification = null;
            try
            {
                verification = connHelper.TryRequest(() => api.VerifyApp(account.Identity, account.Password));
                account.UserId = verification.UserId;
            }
            catch (LoginException le)
            {
                if (le.ErrorCode == LoginException.Error.VerificationRequired || le.ErrorCode == LoginException.Error.WrongUsernameOrPassword)
                {
                    BlockAccount(account);
                    return null;
                }

                if (le.ErrorCode == LoginException.Error.AccountDisabled)
                {
                    DisableAccount(account);
                    return null;
                }
            }

            return verification;
        }
    }
}