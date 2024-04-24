using IMH.Domain.Facebook;
using KSS.Patterns.Extensions;
using KSS.Patterns.Web;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MMOApp.FB.BLL
{
    public class AppData
    {
        public void InitDataFolders()
        {
            var cfolder = GetAssemblyDirectory();
            var fdata = Path.Combine(cfolder, "data");

            if (!Directory.Exists(fdata))
                Directory.CreateDirectory(fdata);

            var fphotos = Path.Combine(fdata, "photos");
            if (!Directory.Exists(fphotos))
                Directory.CreateDirectory(fphotos);
        }

        public string GetDataFolder()
        {
            var cfolder = GetAssemblyDirectory();
            var fdata = Path.Combine(cfolder, "data");

            if (!Directory.Exists(fdata))
                Directory.CreateDirectory(fdata);

            return fdata;
        }

        public string GetViaFile()
        {
            var fdata = GetDataFolder();
            var vfile = Path.Combine(fdata, "accounts_via.txt");

            return vfile;
        }

        public string GetViaCPFile()
        {
            var fdata = GetDataFolder();

            var vfile = Path.Combine(fdata, "accounts_via_cp.txt");

            return vfile;
        }

        public string GetViewerFile()
        {
            var fdata = GetDataFolder();

            var vfile = Path.Combine(fdata, "accounts_viewers.txt");

            return vfile;
        }

        public string GetViaTFile()
        {
            var fdata = GetDataFolder();

            var vfile = Path.Combine(fdata, "accounts_via_trusted.txt");

            return vfile;
        }

        public string GetFriendsFolder()
        {
            var fdata = GetDataFolder();
            var ffolder = Path.Combine(fdata, "friends");

            if (!Directory.Exists(ffolder))
                Directory.CreateDirectory(ffolder);

            return ffolder;
        }

        public string GetFriendsFile(string userId)
        {
            var folder = GetFriendsFolder();
            var file = Path.Combine(folder, userId + ".txt");
            return file;
        }

        public string GetPhotoFolder(string userId)
        {
            var fdata = GetDataFolder();
            var pfolder = Path.Combine(fdata, "friends_photo");
            var ufolder = Path.Combine(pfolder, userId);

            if (!Directory.Exists(ufolder))
                Directory.CreateDirectory(ufolder);

            return ufolder;
        }

        public string GetPhotoFile(string userId, string friendId)
        {
            var pfolder = GetPhotoFolder(userId);
            var pfile = Path.Combine(pfolder, friendId + ".txt");
            return pfile;
        }

        public string GetUserInfoFolder()
        {
            var fdata = GetDataFolder();
            var ffolder = Path.Combine(fdata, "users");

            if (!Directory.Exists(ffolder))
                Directory.CreateDirectory(ffolder);

            return ffolder;
        }

        public string GetUserInfoFile(string userId)
        {
            var folder = GetUserInfoFolder();
            var file = Path.Combine(folder, $"{userId}.txt");

            return file;
        }

        public void SaveVia(List<FacebookUser> users)
        {
            var vfile = GetViaFile();

            SaveUsers(users, vfile);
        }

        public void SaveViaCP(List<FacebookUser> cpusers)
        {
            var vfile = GetViaCPFile();

            SaveUsers(cpusers, vfile);
        }

        public void SaveViaTrusted(List<FacebookUser> tusers)
        {
            var vfile = GetViaTFile();

            SaveUsers(tusers, vfile);
        }

        public void SaveUserInfo(FacebookUser user)
        {
            var file = GetUserInfoFile(user.UserId);
            SaveUser(user, file);
        }

        public FacebookUser LoadUserInfo(string userId)
        {
            var file = GetUserInfoFile(userId);
            var user = LoadUser(file);
            return user;
        }

        public List<FacebookUser> LoadVia()
        {
            var vfile = GetViaFile();
            var users = LoadUsers(vfile);
            return users;
        }

        public List<FacebookUser> LoadViaCP()
        {
            var vfile = GetViaCPFile();
            var cpusers = LoadUsers(vfile);
            return cpusers;
        }

        public List<FacebookUser> LoadViaT()
        {
            var vfile = GetViaCPFile();
            var tusers = LoadUsers(vfile);
            return tusers;
        }

        public List<FacebookUser> LoadViewers()
        {
            var file = GetViewerFile();
            var users = LoadUsers(file);
            return users;
        }

        public void SaveFriends(string userId, List<FacebookUser> friends)
        {
            var file = GetFriendsFile(userId);
            var content = JsonConvert.SerializeObject(friends);
            File.WriteAllText(file, content);
        }

        public List<FacebookUser> LoadFriends(string userId)
        {
            var file = GetFriendsFile(userId);
            var content = ReadFile(file);
            var friends = content.NotNullOrWhiteSpace() ? JsonConvert.DeserializeObject<List<FacebookUser>>(content) : null;

            return friends;
        }

        public void SaveUsers(List<FacebookUser> users, string file)
        {
            var content = JsonConvert.SerializeObject(users);
            File.WriteAllText(file, content);
        }

        public List<FacebookUser> LoadUsers(string file)
        {
            var content = File.ReadAllText(file);
            var users = JsonConvert.DeserializeObject<List<FacebookUser>>(content);
            if (users != null)
                users.ForEach(u => u.UserAgent = WebFactory.DefaultUserAgent);

            return users;
        }


        public void SaveUser(FacebookUser user, string file)
        {
            var content = JsonConvert.SerializeObject(user);
            File.WriteAllText(file, content);
        }

        public FacebookUser LoadUser(string file)
        {
            if (!File.Exists(file))
                return null;

            var content = File.ReadAllText(file);
            var user = JsonConvert.DeserializeObject<FacebookUser>(content);
            user.UserAgent = WebFactory.DefaultUserAgent;

            return user;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId">Id of user need to query photo</param>
        /// <param name="friendId">Id of friend in which user is saved</param>
        /// <returns></returns>
        public List<FacebookPhoto> LoadPhotos(string userId, string friendId)
        {
            var file = GetPhotoFile(friendId, userId);
            if (!File.Exists(file))
                return null;

            var content = File.ReadAllText(file);
            var photos = JsonConvert.DeserializeObject<List<FacebookPhoto>>(content);

            return photos;
        }

        public string ReadFile(string file)
        {
            if (File.Exists(file))
                return File.ReadAllText(file);

            return null;
        }

        public string GetAssemblyDirectory()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }



        public bool DoesFriendPhotosExists(string userId, string friendId)
        {
            var file = GetPhotoFile(userId, friendId);
            var exists = File.Exists(file);

            return exists;
        }

        public void SaveFriendPhotos(string userId, string friendId, List<FacebookPhoto> photos)
        {
            var file = GetPhotoFile(userId, friendId);
            var content = JsonConvert.SerializeObject(photos);

            File.WriteAllText(file, content);
        }
    }
}
