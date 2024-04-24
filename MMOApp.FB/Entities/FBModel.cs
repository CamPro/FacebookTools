using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace fb.spam3.admin
{
    public class FBModel
    {
        public int id { set; get; }
        public int idx { set; get; }

        public int takecare_time { set; get; } = 0;

        public bool isAddFriendRandom { set; get; } = false;
        public bool isUpAvatar { set; get; } = false;

        public string uid { set; get; }
        public string email { set; get; }
        public string pwd { set; get; }
        public string token { set; get; }
        public string cookie { set; get; }
        public string basestring { set; get; }
        public int count_friend { set; get; }
        public int count_group { set; get; }

        public DateTime startdate { set; get; }
        public DateTime curentdate { set; get; } = DateTime.Now;
        public bool checkpoint { set; get; }
        public bool hasAvatar { set; get; }
        public int friend_today { set; get; }
        public int group_today { set; get; }
        public int like_post_today { set; get; }
        public int like_page_today { set; get; }
        public int request_friend_today { set; get; }
        public int confirm_friend_today { set; get; }
        public int join_group_today { set; get; }
        public int run_timer { set; get; }
        public int checkpoint_timer { set; get; }
        public int flag { set; get; }
        public string useragent { set; get; }

        public List<string> listMySpammer = new List<string>();


        public List<string> listPost = new List<string>();

        public List<string> listSSH = new List<string>();

        public List<string> listFriend = new List<string>();

        public List<string> listSuggest = new List<string>();

        public List<string> listFollow = new List<string>();
        public List<string> listMyPage = new List<string>();


        public List<string> listGroup = new List<string>();
        public List<string> listMyGroup = new List<string>();

        public List<string> listPageLike = new List<string>();
        public List<string> listLike = new List<string>();
        public List<string> listComment = new List<string>();

        public List<string> listPakes = new List<string>();

        public List<string> listPhoto = new List<string>();

        public List<string> listCommentFriend = new List<string>();

        public void Parse(string line)
        {
            if (string.IsNullOrEmpty(line)) return;
            //Task.Factory.StartNew(() =>
            //{
            try
            {
                string[] acdetail = line.Split('|');
                //DataRow newCustomersRow = _mDB.NewRow();
                int iPos = 0;
                foreach (string item in acdetail)
                {
                    if (string.IsNullOrEmpty(item)) continue;

                    if (item.isEmailAddress()) email = item;
                    else if (item.isUID()) uid = item;
                    else if (item.isToken()) token = item;
                    else if (item.isCoookie()) cookie = item;
                    else pwd = item;
                }
            }
            catch (Exception _ex)
            {

            }
        }
        public bool isUpdateInfo { set; get; }

        public string ToBaseString()
        {
            var bs = $"{uid}|{email}|{pwd}|{cookie}|{token}";
            return bs;
        }
    }

    public static class StringExtensions
    {
        public static string Left(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            maxLength = Math.Abs(maxLength);

            return (value.Length <= maxLength
                   ? value
                   : value.Substring(0, maxLength)
                   );
        }

        public static bool isCoookie(this string item)
        {
            if (item.Contains("c_user=10") && item.Contains("xs="))
            {
                return true;
            }
            return false;
        }
        public static bool isToken(this string item)
        {
            if (item.Left(5).Contains("EAA"))
            {
                return true;
            }
            return false;
        }
        public static bool isUID(this string item)
        {
            if (item.Left(4).Contains("1000"))
            {
                return true;
            }
            return false;
        }

        public static bool isEmailAddress(this string emailAddress)
        {
            bool MethodResult = false;

            try
            {
                MailAddress m = new MailAddress(emailAddress);

                MethodResult = m.Address == emailAddress;

            }
            catch //(Exception ex)
            {
                //ex.HandleException();

            }

            return MethodResult;

        }

        //if (IsValidEmailAddress(item)) email = item;



    }

}
