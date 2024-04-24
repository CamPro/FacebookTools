using fb.spam3.admin;
using IMH.Domain.Facebook;
using KSS.Patterns.Extensions;
using KSS.Patterns.Logging;
using KSS.Patterns.Web;
using KSS.Patterns.WebAutomation;
using MarketDragon.Automation.Common;
using MarketDragon.Automation.Social.Facebook;
using MMOApp.FB.BLL;
using MMOApp.FB.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xNet;

namespace MMOApp.FB
{
    public partial class MainForm : Form
    {
        public enum CloneStatus
        {
            New,
            Live,
            CheckPoint,
            Die
        }

        #region fields

        const int THREADS_BACKUP = 1; // 10
        const int THREADS_BACKUP_PHOTOS = 10; // 10

        RichTextLogger logger;
        RichTextLogger loggervia;
        RichTextLogger loggervialive;
        RichTextLogger loggerviat;
        RichTextLogger loggerviacp;

        Business business;
        List<FacebookUser> users; // via
        List<FacebookUser> tusers; // trusted 
        List<FacebookUser> cpusers; // cpoint
        AppData data;

        List<FacebookUser> viewers;

        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

        void InitControls()
        {
            logger = new RichTextLogger(txtLog);
            loggervia = new RichTextLogger(txtAccounts);
            loggervialive = new RichTextLogger(txtAccountsLive);
            loggerviat = new RichTextLogger(txtAccountsTrusted);
            loggerviacp = new RichTextLogger(txtAccountsCP);
        }

        #region log

        private void LogEmpty()
        {
            Log(string.Empty);
        }

        private void Log(IEnumerable<string> content)
        {
            var sbContent = new StringBuilder();
            content.ForEach(c => sbContent.AppendLine(c));
            Log(sbContent.ToString());
        }

        private void Log(string content)
        {
            Log(content, string.Empty);
        }

        private void Log(string content, string prefix)
        {
            Log(content, Color.Black, prefix);
        }

        private void Log(string content, Color color, string prefix = "")
        {
            logger.AppendLog(content, prefix, color);
        }

        private void Log(Exception e, string prefix = "")
        {
            if (e is WebException)
            {
                Log("Web connection exception", Color.Red);
            }
            else
            {
                logger.AppendLog(e, prefix);
                //logger.LogExceptionMessge(e);
            }
        }

        private void Log(string content, Exception e)
        {
            if (e == null) { Log(content); }
            else { Log(e); }
        }


        private void Log(string content, LogMode mode)
        {
            Log(content, mode, string.Empty);
        }

        private void Log(string content, LogMode mode, string prefix)
        {
            var color = Color.Black;
            if (mode == LogMode.Error)
                color = Color.Red;

            Log(content, color, prefix);
        }

        private void Log(string content, LogMode mode, Exception exception)
        {
            Log(content, mode);

            if (exception != null)
                Log(exception);
        }

        void Log(FacebookUser user)
        {
            Log(GetLog(user));
        }

        private void LogWebException()
        {
            Log("Web connection exception", Color.Red);
        }

        private void LogError(string content)
        {
            Log(content, Color.Red);
        }

        private void LogBlocked(FacebookUser user)
        {
            Log($"Blocked : {GetLog(user)}", Color.Red);
        }

        private void DoLog(int threadIdx, string content)
        {
            DoLog(threadIdx, content, Color.Black);
        }

        private void DoLog(int threadIdx, string content, Color color)
        {
            string message = $"{content}";
            Log(message, color, $"[T-{threadIdx}] ");
        }

        private string ThreadLog(int threadIdx)
        {
            return $"[T-{threadIdx}]";
        }

        private string GetLog(FacebookUser user)
        {
            if (user == null) { return string.Empty; }

            return $"[{user.UserId ?? user.Username}] [{user.FullName}]";
        }

        private void LogLine()
        {
            Log("--------------------------------------------------------------");
        }

        private Task Run(Action action)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    Log(e);
                }
            });

            return task;
        }

        private void Run<T>(Func<T> action)
        {
            Run((Action)(() => action()));
        }

        /// <summary>
        /// Try request many times if error.
        /// </summary>
        /// <param name="func"></param>
        void TryRequest(Action func)
        {
            int maxRequestTry = 3;
            int tries = 0;
            do
            {
                try
                {
                    tries++;
                    func();
                    break;
                }
                catch (Exception e)
                {
                    if (!CatchRequestException(e))
                        throw;
                }
            } while (tries < maxRequestTry);
        }

        T TryRequest<T>(Func<T> func)
        {
            int maxRequestTry = 3;
            int tries = 0;

            do
            {
                try
                {
                    tries++;
                    return func();
                }
                catch (Exception e)
                {
                    if (!CatchRequestException(e))
                        throw;
                }
            } while (tries < maxRequestTry);

            return default(T);
        }

        bool CatchRequestException(Exception e)
        {
            if (e is WebException)
                return true;

            if (e is AggregateException)
            {
                var ae = e as AggregateException;
                ae.Flatten();

                foreach (var ine in ae.InnerExceptions)
                {
                    if (CatchRequestException(ine))
                        return true;
                }
            }

            if (e.InnerException != null)
                return CatchRequestException(e.InnerException);

            return false;
        }

        #endregion

        #region common

        private FacebookWebRequest CreateWeb(FacebookUser account, bool noscript = false)
        {
            if (account.Cookies == null)
                return null;

            return FacebookWebRequest.Create(account, noscript);
        }

        private FacebookWebRequest CreateWeb(List<KCookie> cookies, string userAgent, string proxy = null, bool isSocks = false)
        {
            return FacebookWebRequest.Create(cookies, userAgent, proxy, isSocks);
        }

        private FacebookPrivateAPI CreateApi(FacebookUser user)
        {
            return FacebookPrivateAPI.Create(user);
        }

        bool CheckAlive(FacebookUser user)
        {
            var api = new FacebookPrivateAPI(user);

            string error;
            bool alive = true;
            try
            {
                alive = api.CheckAlive();
            }
            catch (AccessTokenBlockedException)
            {
                alive = false;
            }

            user.Blocked = !alive;
            if (user.Blocked)
            {
                LogBlocked(user);
            }

            return alive;
        }

        #endregion

        #region  services

        private List<FacebookUser> ServiceGetNewVia()
        {
            // get new
            var url = ConfigurationManager.AppSettings["Service_Via_New"];
            var susers = ServiceGetAccs(url);
            return susers;
        }

        private List<FacebookUser> ServiceGetAllVia()
        {
            // get new
            var url = ConfigurationManager.AppSettings["Service_Via_All"];
            var susers = ServiceGetAccs(url);
            return susers;
        }

        private List<FacebookUser> ServiceGetViaCheckpoint()
        {
            var url = ConfigurationManager.AppSettings["Service_Via_CP"];
            var susers = ServiceGetAccs(url);
            return susers;
        }

        private List<FacebookUser> ServiceGetViaTrusted()
        {
            var url = ConfigurationManager.AppSettings["Service_Via_Trusted"];
            var susers = ServiceGetAccs(url);
            return susers;
        }

        private List<FacebookUser> ServiceGetAccs(string url)
        {
            var web = WebFactory.CreatexNetWeb();
            var content = web.Get(url);
            var idx = content.IndexOf('[');
            if (idx < 0)
                return new List<FacebookUser>();

            content = content.Substring(idx, content.Length - idx);

            var gusers = new List<FacebookUser>();

            dynamic result = JsonConvert.DeserializeObject(content);
            foreach (var item in result)
            {
                var id = (string)item.uid;
                if (id.IsNullOrWhiteSpace())
                    continue;

                var basestring = (string)item.base_string;
                var model = new FBModel();
                model.Parse(basestring);

                //if (basestring.IsNullOrWhiteSpace())
                //    continue;

                var user = new FacebookUser
                {
                    UserId = id,
                    Email = model.email,
                    CookiesText = model.cookie,
                    iPhoneVerification = new FacebookVerification { AccessToken = model.token },
                    Password = model.pwd,
                    UserAgent = WebFactory.DefaultUserAgent
                };

                gusers.Add(user);
            }

            foreach (var user in gusers)
            {
                if (user.CookiesText.NotNullOrWhiteSpace())
                    user.AddCookies(user.CookiesText);
            }

            return gusers;
        }

        private void ServiceSetCheckPoint(string userId)
        {
            var url = ConfigurationManager.AppSettings["Service_Via_Set_Checkpoint"];
            url = $"{url}?uid={userId}";

            var web = WebFactory.CreatexNetWeb();
            var content = web.Get(url);
        }

        private void ServiceSubmitLive(FacebookUser user)
        {
            var serviceUrl = ConfigurationManager.AppSettings["Service_Via_Sumbmit_Live"];
            var model = UserToModel(user);
            var basestring = model.ToBaseString();

            var url = $"{serviceUrl}?uid={user.UserId}&base_string={basestring}&token={user.GetToken().AccessToken}";
            var web = WebFactory.CreatexNetWeb();
            var content = web.Get(url);
        }

        private FBModel UserToModel(FacebookUser user)
        {
            var model = new FBModel
            {
                uid = user.UserId,
                email = user.Email,
                pwd = user.Password,
                cookie = user.GetLoginCookieString(),
                token = user.GetToken().AccessToken
            };

            return model;
        }

        #endregion

        #region data

        List<FacebookUser> ParseAccs(string content)
        {
            var qusers = from line in content.GetLines()
                         where line.NotNullOrWhiteSpace()
                         let parts = line.Split('|')
                         let identity = parts[0].Split(':')[1].Trim()
                         let pass = parts[1].Split(':')[1].Trim()
                         let token = parts[2].Split(':')[1].Trim()
                         let cookies = parts[3].Split(':')[1].Trim()
                         select new FacebookUser
                         {
                             UserId = identity,
                             Password = pass,
                             iPhoneVerification = new FacebookVerification { AccessToken = token },
                             CookiesText = cookies
                         };

            var users = qusers.ToList();
            foreach (var user in users)
            {
                if (user.UserId.Contains("@"))
                {
                    user.Email = user.UserId;
                    user.UserId = null;
                }

                user.AddCookies(user.CookiesText);
            }

            return users;
        }

        string SelectFile()
        {
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
                return dlg.FileName;

            return null;
        }

        string OpenFile()
        {
            var file = SelectFile();
            if (file.IsNullOrWhiteSpace() || !File.Exists(file))
                return null;

            var content = File.ReadAllText(file);
            return content;
        }

        List<FacebookUser> LoadAccountsFromFile()
        {
            var content = OpenFile();
            if (content.NotNullOrWhiteSpace())
            {
                var users = ParseAccs(content);
                return users;
            }

            return null;
        }

        void LoadViaFromFile()
        {
            var fusers = LoadAccountsFromFile();
            if (fusers != null)
            {
                users = fusers;
                data.SaveVia(users);
            }
        }

        #endregion

        #region func

        private List<FacebookUser> LoadAccounts(string content)
        {
            var lines = content.GetLines();
            var qusers = from line in lines
                         where line.NotNullOrWhiteSpace()
                         let segments = line.Split('|')
                         let id = segments[0]
                         let email = segments[1]
                         let pass = segments[2]
                         let cookies = segments[3]
                         let token = segments[4]
                         let user = new FacebookUser
                         {
                             UserId = id,
                             Email = email,
                             Password = pass,
                             iPhoneVerification = new FacebookVerification { AccessToken = token },
                             CookiesText = cookies
                         }
                         select user;

            var users = qusers.ToList();
            users.ForEach(u =>
            {
                u.AddCookies(u.CookiesText);
                u.UserAgent = WebFactory.DefaultUserAgent;
            });


            return users;
        }

        void Init()
        {
            data = new AppData();
            business = new Business();

            viewers = data.LoadViewers();

            users = data.LoadVia();

            ShowViaOnUI();

            //Test();

            //business = new Business();

            //var file = @"D:\temp\addmem_vi.txt";
            //var content = File.ReadAllText(file);

            //txtAccounts.Text = content;
            //users = LoadAccounts();
        }

        void GetNewAccsFromService()
        {
            users = ServiceGetNewVia();
            data.SaveVia(users);
            ShowViaOnUI();
        }

        void GetAllAccsFromService()
        {
            users = ServiceGetAllVia();
            data.SaveVia(users);
            ShowViaOnUI();
        }

        void GetAccsCPFromService()
        {
            cpusers = ServiceGetViaCheckpoint();
            data.SaveViaCP(cpusers);
            ShowViaOnUI(cpusers, txtAccountsCP);
        }

        void SubmitUnlockedAccount(FacebookUser user)
        {

        }


        #endregion

        #region  backup

        void Backup()
        {
            var qusers = users.ToConcurrentQueue();

            Action doBackup = () =>
            {
                FacebookUser user;

                do
                {
                    if (qusers.IsEmpty || !qusers.TryDequeue(out user))
                        break;

                    try
                    {
                        if (!CheckAlive(user))
                        {
                            ServiceSetCheckPoint(user.UserId);
                            loggerviacp.AppendLog(GetLog(user));
                            continue;
                        }

                        Backup(user);

                        loggervialive.AppendLog(GetLog(user));
                    }
                    catch /*(WebResponseException)*/
                    {
                        LogError("Web exception");
                        qusers.Enqueue(user);
                    }

                } while (true);
            };

            var tasks = new List<Task>();
            for (int i = 0; i < THREADS_BACKUP; i++)
            {
                var task = Task.Run(doBackup);
                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());

            Log("ALL BACKUP DONE!");
        }

        void BackupProfile()
        {
            var viewer = viewers.PickRandomOne();
            //var qusers = users.Where(u => u.UserId == "100012960852389").ToConcurrentQueue();
            var qusers = users.ToConcurrentQueue();


            Action doBackup = () =>
            {
                FacebookUser user;

                do
                {
                    if (qusers.IsEmpty || !qusers.TryDequeue(out user))
                        break;

                    try
                    {
                        //if (!CheckAlive(user))
                        //{
                        //    ServiceSetCheckPoint(user.UserId);
                        //    loggerviacp.AppendLog(GetLog(user));
                        //    continue;
                        //}

                        BackUpInfo(user, viewer);

                        loggervialive.AppendLog(GetLog(user));
                    }
                    catch /*(WebResponseException)*/
                    {
                        LogError("Web exception");
                        qusers.Enqueue(user);
                    }

                } while (true);
            };

            var tasks = new List<Task>();
            for (int i = 0; i < THREADS_BACKUP; i++)
            {
                var task = Task.Run(doBackup);
                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());

            Log("ALL BACKUP DONE!");

        }

        void Backup(FacebookUser user)
        {
            LogLine();
            Log(user);

            BackUpInfo(user);
            BackUpFriends(user);
            BackupFriendsPhotos(user);

            ServiceSubmitLive(user);

            Log($"Backup DONE: {GetLog(user)}");
        }

        void BackUpInfo(FacebookUser user)
        {
            var api = new FacebookPrivateAPI(user);
            var info = api.UserGetFullInfoFQL(user.UserId);

            if (info != null)
                data.SaveUserInfo(info);
        }

        void BackUpInfo(FacebookUser user, FacebookUser viewer)
        {
            var api = new FacebookPrivateAPI(viewer ?? user);

            var info = api.UserGetFullInfoFQL(user.UserId);

            if (info != null)
                data.SaveUserInfo(info);
        }


        void BackUpFriends(FacebookUser user)
        {
            var api = new FacebookPrivateAPI(user);

            var friends = api.GetFriendListFQL(user.UserId);
            data.SaveFriends(user.UserId, friends);

            user.TotalFriends = friends.Count;
        }

        void BackupFriendsPhotos(FacebookUser user, bool forceUpdate = false)
        {
            var friends = data.LoadFriends(user.UserId).ToConcurrentQueue();

            Func<bool> doBackup = () =>
            {
                FacebookUser friend = null;

                try
                {
                    if (friends.IsEmpty || !friends.TryDequeue(out friend))
                        return false;

                    if (data.DoesFriendPhotosExists(user.UserId, friend.UserId) && !forceUpdate)
                        return true;

                    BackupFriendsPhotos(user, friend);
                }
                catch (HttpException)
                {
                    LogWebException();
                    friends.Enqueue(friend);
                }

                return true;
            };

            var tasks = new List<Task>();
            for (int i = 0; i < THREADS_BACKUP_PHOTOS; i++)
            {
                var task = Task.Run(() => { while (doBackup()) { } });
                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());
        }

        void BackupFriendsPhotos(FacebookUser user, FacebookUser friend)
        {
            string error;
            var api = CreateApi(user);

            var connHelper = new ConnectionHelper();

            var photos = connHelper.TryRequest(() => api.GetPhotosHasUserTagged(friend.UserId, out error));

            data.SaveFriendPhotos(user.UserId, friend.UserId, photos);
        }

        #endregion

        #region  unlock

        private void UnlockCheckpoint(FacebookUser user)
        {
            business.CheckAlive(user, true, null, Log);
        }

        private void UnlockAccounts()
        {
            UnlockAccounts(users);
        }

        private void UnlockCPAccounts()
        {
            UnlockAccounts(cpusers.Shuffle().ToList());
        }

        private void UnlockAccounts(List<FacebookUser> lockedUsers)
        {
            if (lockedUsers == null || lockedUsers.Count <= 0)
            {
                LogError("Khong co account de unlock");
                return;
            }

            var lusers = lockedUsers.ToList();

            foreach (var user in lusers)
            {
                LogLine();
                Log(GetLog(user));

                var alive = CheckAlive(user);

                if (alive)
                {
                    var veri = business.GetiPhoneToken(user);
                }

                if (!alive)
                {
                    UnlockCheckpoint(user);

                    if (user.Blocked)
                    {
                        AddAccountLogUI(user, loggerviacp);
                        LogError($"Cannot Unlock {GetLog(user)}");
                    }
                    else
                    {
                        var veri = business.GetiPhoneToken(user);
                        data.SaveVia(users);

                        if (veri != null)
                        {
                            // send to service
                            ServiceSubmitLive(user);

                            AddAccountLogUI(user, loggervialive);

                            Log($"Unlock sucessfully {GetLog(user)}", Color.Green);
                        }
                        else
                        {
                            LogError("Cannot get token");
                        }
                    }
                }
            }
        }

        #endregion

        #region handlers               

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            Run(UnlockAccounts);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitControls();

            Init();
        }

        private void btnBackupData_Click(object sender, EventArgs e)
        {
            Run(Backup);
        }

        private void btnLoadAccs_Click(object sender, EventArgs e)
        {
            LoadViaFromFile();
        }

        private void btnGetVia_Click(object sender, EventArgs e)
        {
            Run(GetNewAccsFromService);
        }

        private void btnGetAllVia_Click(object sender, EventArgs e)
        {
            Run(GetAllAccsFromService);
        }

        private void btnGetViaTrusted_Click(object sender, EventArgs e)
        {

        }

        private void btnGetViaCP_Click(object sender, EventArgs e)
        {
            Run(GetAccsCPFromService);
        }

        private void btnUnlockCPAccounts_Click(object sender, EventArgs e)
        {
            Run(UnlockCPAccounts);
        }

        private void btnBackUpProfile_Click(object sender, EventArgs e)
        {
            Run(BackupProfile);
        }

        #endregion

        #region ui

        void ShowViaOnUI()
        {
            ShowViaOnUI(users, txtAccounts);
        }

        void ShowViaOnUI(List<FacebookUser> uusers, RichTextBox rtbox)
        {
            var sb = new StringBuilder();
            foreach (var user in uusers)
            {
                sb.AppendLine(GetAccountDisplay(user));
            }

            this.SafeInvoke(() =>
            {
                rtbox.Text = sb.ToString();
                Log($"Accounts loaded: {uusers.Count}");
            });
        }

        string GetAccountDisplay(FacebookUser user)
        {
            return $"{user.UserId} - {user.Email} - {user.FullName}";
        }

        void AddAccountLogUI(FacebookUser user, RichTextLogger logger)
        {
            logger.AppendLog(GetAccountDisplay(user));
        }

        void Test()
        {
            var parser = new FacebookParser();
            var content = File.ReadAllText(@"D:\temp\debug.html");
            var root = HtmlHelper.Root(content);
            var form = parser.FindForm(root, "checkpoint");
            var queries = parser.FormGetElementsBySibling(form);
        }

        #endregion


    }
}