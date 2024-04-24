using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using reg.dcom.driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace reg._.dcom.driver
{
     public partial class Form1 : Form
     {
          public Form1()
          {
               InitializeComponent();
          }

          private void button1_Click(object sender, EventArgs e)
          {

             


              

          }

          private string getProxy()
          {
               string ret = "";
               try {
                    WebClient webClient = new WebClient();
                    dynamic result = JObject.Parse(webClient.DownloadString("https://api.getproxylist.com/proxy?allowsHttps=1&country[]=US&protocol=http"));
                    Console.WriteLine(result);

                    Console.WriteLine(result.ip);
                    Console.WriteLine(result.port);
                    ret = string.Format("{0}:{1}", result.ip, result.port);
               }
               catch(Exception _ex)
               {

               }
               return ret;
          }
          void reg_webdriver(int i)
          {

               try
               {
                    ChromeOptions options = new ChromeOptions();
                    //options.AddArgument("--disable-extensions");
                    //string userAgent002 = "Mozilla/5.0 (Linux; U; Android 4.2.2; en-us; GT-I9060 Build/JDQ39) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Mobile Safari/534.30 [FB_IAB/FB4A;FBAV/118.0.0.22.79;]";
                    //string userAgent002 = Properties.Resources.useragent.Replace("\r\n", "|").Split('|').ToList().RandomList(1).FirstOrDefault(); 
                    string userAgent002 = "Mozilla/5.0 (Linux; Android 7.0; PLUS Build/NRD90M) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.98 Mobile Safari/537.36";
                    //options.AddArgument($"--user-agent={userAgent002}");

                    options.AddArguments("--incognito");
                    
                    //options.AddArgument("--user-data-dir=" + pathDes + "/");
                    string sPxy = getProxy();
                    if (string.IsNullOrEmpty(sPxy))
                    {
                         return;
                    }
                    string sProxy = string.Format("--proxy-server={0}", sPxy);
                    string ChromePath = string.Format("D:\\ChromeProfile\\User Data {0}", i);
                    options.AddArgument(sProxy);
                    options.AddArgument(string.Format("user-data-dir={0}", ChromePath));

                    var service = ChromeDriverService.CreateDefaultService();
                    service.LogPath = "chromedriver.log";
                    service.EnableVerboseLogging = true;

                    service.HideCommandPromptWindow = true;
                    //LoggingPreferences logPrefs = new LoggingPreferences();
                    options.BinaryLocation = Application.StartupPath + "/GoogleChromePortable/GoogleChromePortable.exe";
                    ChromeDriver driver = new ChromeDriver(service, options);


                    try
                    {
                         string captcha_url = "";
                         string pagesource = "";
                         string pageurl = "";
                         bool isSubmit = true;


                         //driver.Navigate().GoToUrl("http://www.google.com");

                         //string url = "http://www.asiamovie.info/getip.php";
                         //driver.Navigate().GoToUrl(url);

                         //Thread.Sleep(10 * 1000);
                         string youtube_link = "https://www.youtube.com/watch?v=aiOEwVtgWQs";
                         driver.Navigate().GoToUrl(youtube_link);
                         ////Thread.Sleep(new Random().Next(500, 2000));
                         pagesource = driver.PageSource;


                       

                         int sleeptime = Convert.ToInt32(GetRandomNumber(2.0, 3.0) * 60 * 1000);
                         Console.WriteLine("sleep-time: {0}", sleeptime);
                         table.Rows.Add(table.Rows.Count, youtube_link, "David", DateTime.Now);
                         Thread.Sleep(sleeptime);


                    }
                    catch (Exception _ex)
                    {
                         Console.WriteLine(_ex.ToString());
                    }
                    finally
                    {

                         driver.Manage().Cookies.DeleteAllCookies();
                         driver.Close();
                    }
               }
               catch (Exception _ex)
               {
                    Console.WriteLine(_ex.ToString());
               }
               return ;
          }
          private double GetRandomNumber(double minimum, double maximum)
          {
               Random random = new Random();
               return random.NextDouble() * (maximum - minimum) + minimum;
          }
          private void tbarStartAutoView_Click(object sender, EventArgs e)
          {
               Task.Run(() =>
               {


                    Parallel.For(0, (int)5, new ParallelOptions { MaxDegreeOfParallelism = 2}, k => { reg_webdriver(k); });


               });
          }
          static DataTable GetTable()
          {
               // Here we create a DataTable with four columns.
               DataTable table = new DataTable();
               table.Columns.Add("STT", typeof(int));
               table.Columns.Add("Link", typeof(string));
               table.Columns.Add("Country", typeof(string));
               table.Columns.Add("IP", typeof(string));
               table.Columns.Add("Date", typeof(DateTime));

               // Here we add five DataRows.
               table.Rows.Add(1, "Indocin", "US", "David", DateTime.Now);
               table.Rows.Add(2, "Enebrel", "UK", "David", DateTime.Now);
               table.Rows.Add(3, "Hydralazine", "JP", "Christoff", DateTime.Now);
               table.Rows.Add(4, "Combivent", "FR", "Janet", DateTime.Now);
               table.Rows.Add(5, "Dilantin", "VN", "Melanie", DateTime.Now);
               return table;
          }

          static DataTable GetTable2()
          {
               // Here we create a DataTable with four columns.
               DataTable table = new DataTable();
               table.Columns.Add("STT", typeof(int));
               table.Columns.Add("youtube_link", typeof(string));
               table.Columns.Add("Patient", typeof(string));
               table.Columns.Add("Date", typeof(DateTime));

               // Here we add five DataRows.
               table.Rows.Add(1, "Indocin", "David", DateTime.Now);
               table.Rows.Add(2, "Enebrel", "Sam", DateTime.Now);
               table.Rows.Add(3, "Hydralazine", "Christoff", DateTime.Now);
               table.Rows.Add(4, "Combivent", "Janet", DateTime.Now);
               table.Rows.Add(5, "Dilantin", "Melanie", DateTime.Now);
               return table;
          }


          DataTable table = new DataTable();
          private void Form1_Load(object sender, EventArgs e)
          {
               table = GetTable();
               dataGridView1.DataSource = table;

               DataTable table2 = GetTable2();
               dataGridView2.DataSource = table2;

               this.tabControl1.SelectedTab = this.tabControl1.TabPages["about"];
          }

          private void toolStripLabel1_Click(object sender, EventArgs e)
          {
               System.Diagnostics.Process.Start("https://www.facebook.com/vinguyet6666");
          }

          private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
          {
               System.Diagnostics.Process.Start("https://www.facebook.com/vinguyet6666");
          }
     }
}
