using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace addFriend_FB
{
    public partial class Form1 : Form
    {
        private int interval = 0;

        private int waittime = 0;

        private Random random = new Random();

        public Form1()
        {
            InitializeComponent();
            lbResult.Text = string.Empty;
            lbOk.Text = string.Empty;
            lbNot.Text = string.Empty;
        }

        private string methodGet(string url, string Inputcookies, string userAgent)
        {
            string result;
            try
            {
                CookieContainer cookieContainer = new CookieContainer();
                string[] array = Inputcookies.Split(';');
                string[] array2 = array;
                foreach (string text in array2)
                {
                    string[] array3 = text.Trim().Split('=');
                    try
                    {
                        Cookie cookie = new Cookie(array3[0].Trim(), array3[1].Trim(), "/", ".facebook.com");
                        cookieContainer.Add(cookie);
                    }
                    catch (Exception ex)
                    {
                        result = ex.Message;
                    }
                }
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.UserAgent = userAgent;
                httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                httpWebRequest.CookieContainer = cookieContainer;
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream stream = httpWebResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(stream);
                result = streamReader.ReadToEnd();
            }
            catch (Exception ex2)
            {
                result = ex2.Message;
            }
            return result;
        }

        private bool Ketban(string uid, string cookie, string userAgent)
        {
            try
            {
                string text = "";
                string url = $"https://m.facebook.com/{uid}";
                string text2 = methodGet(url, cookie, userAgent);
                if (text2.Contains("/a/mobile/friends/profile_add_friend.php?subjectid="))
                {
                    string url2 = "https://mbasic.facebook.com/profile.php?id=" + uid;
                    string input = methodGet(url2, cookie, userAgent);
                    string[] array = Regex.Split(input, "profile_add_friend\\.php\\?subjectid=(.*?)\"");
                    url2 = "https://m.facebook.com/a/mobile/friends/profile_add_friend.php?subjectid=" + array[1].Replace("&amp;", "&");
                    text = methodGet(url2, cookie, userAgent);
                }
                if (text.Contains("Đã yêu cầu") || text.Contains("Bạn bè"))
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void Crossthread(Action action)
        {
            try
            {
                if (base.InvokeRequired)
                {
                    Invoke((MethodInvoker)delegate
                    {
                        action();
                    });
                }
                else
                {
                    action();
                }
            }
            catch
            {
            }
        }

        private int intTime(int length)
        {
            return Convert.ToInt32(new string((from s in Enumerable.Repeat("0123456789", length)
                                               select s[random.Next(s.Length)]).ToArray()));
        }

        private async void runKetban(string[] listUid)
        {
            randomTime rTime = new randomTime();
            Crossthread(delegate
            {
                lbResult.Text = "Đang gửi yêu cầu kết bạn";
            });
            interval = 0;
            bool isflag = true;
            int ok = 0;
            int not = 0;
            string[] array = listUid;
            foreach (string uid in array)
            {
                rTime.Time = intTime(2);
                waittime = rTime.Time * 100;
                isflag = true;
                await Task.Factory.StartNew(delegate
                {
                    while (isflag)
                    {
                        Crossthread(delegate
                        {
                            lbResult.Text = $"Đang gửi lời mời kết bạn tiếp theo sau: ({Convert.ToInt32((waittime - interval) / 100).ToString()})";
                        });
                        Thread.Sleep(1);
                        if (interval % waittime == 0)
                        {
                            Crossthread(delegate
                            {
                                lbResult.Text = $"Đang gửi lời mời kết bạn tới: {uid}";
                            });
                            if (Ketban(uid, txtCookies.Text, txtUserAgent.Text))
                            {
                                Crossthread(delegate
                                {
                                    lbResult.Text = $"Đã gửi lời mời kết bạn tới: {uid}";
                                });
                                ok++;
                                Crossthread(delegate
                                {
                                    lbOk.Text = $"Thành công: {ok}/{listUid.Length.ToString()}";
                                });
                                interval = 0;
                                isflag = false;
                            }
                            else
                            {
                                Crossthread(delegate
                                {
                                    lbResult.Text = $"Chưa gửi được lời mời kết bạn tới: {uid}";
                                });
                                not++;
                                Crossthread(delegate
                                {
                                    lbNot.Text = $"Thất bại: {not}/{listUid.Length.ToString()}";
                                });
                                interval = 0;
                                isflag = false;
                            }
                        }
                        interval++;
                        if (interval >= waittime)
                        {
                            interval = 0;
                        }
                    }
                });
            }
            interval = 0;
            waittime = 0;
        }

        private void BtnRun_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCookies.Text))
            {
                MessageBox.Show("Bạn phải nhập cookie của tài khoản", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                txtCookies.Focus();
            }
            else if (string.IsNullOrEmpty(txtUserAgent.Text))
            {
                MessageBox.Show("Bạn phải nhập UserAgent thường xuyên đăng nhập của tài khoản", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                txtUserAgent.Focus();
            }
            else if (!string.IsNullOrEmpty(txtListUid.Text))
            {
                string[] array = txtListUid.Text.Split('\n');
                lbOk.Text = "0/" + array.Length;
                runKetban(array);
            }
            else
            {
                MessageBox.Show("Không có uid nào trong danh sách", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                txtListUid.Focus();
            }
        }

    }

    public class randomTime
    {
        private int _time = 0;

        public int Time
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
                if (_time < 10)
                {
                    _time = 10;
                }
                if (_time > 40)
                {
                    _time = 40;
                }
            }
        }
    }

}
