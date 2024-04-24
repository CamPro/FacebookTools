using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Net;
using System.Net.Http;
using System.Collections.Specialized;

namespace regmail
{
     public class RegMail
     {

          DataGridViewRow r;
          ChromeOptions options = new ChromeOptions();
          ChromeDriver driver;
          public event EventHandler OnComplete;
          int chromeid;

          List<string> listOfUserAgent = File.ReadAllLines("useragent_IOS.txt").ToList().Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
          List<string> listOfFirstName = File.ReadAllLines("firstname.txt").ToList().Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
          List<string> listOfLastName = File.ReadAllLines("lastname.txt").ToList().Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();

          public RegMail()
          {
               
               try
               {

                    this.r = r;
                    chromeid = 0;

                    string sProfile = string.Format("UserData_{0}", "vunguyen41427@gmail.com");
                    //string sProfile = string.Format("UserData_{0}", "khanhdang701407@gmail.com");
                    string ChromePath = string.Format("{0}\\ChromeProfile.Subs\\{1}", Application.StartupPath, sProfile);



                    //options.AddArgument("no-sandbox");
                    string sUserAgent = listOfUserAgent.RandomList(3).FirstOrDefault().ToString();
                    if (!string.IsNullOrEmpty(sUserAgent))
                    {
                         //options.AddArgument(string.Format("user-agent={0}", sUserAgent));
                    }
                    //options.AddArgument(string.Format("user-data-dir={0}", ChromePath));
                    //options.AddArgument(string.Format("profile-directory={0}", sProfile));
                    options.BinaryLocation = @"D:\project.mobile.facebook\portableapp\GoogleChromePortable0/GoogleChromePortable.exe";

                    var service = ChromeDriverService.CreateDefaultService();
                    service.LogPath = "chromedriver.log";
                    service.EnableVerboseLogging = true;
                    service.HideCommandPromptWindow = true;

                    driver = new ChromeDriver(service, options, TimeSpan.FromMinutes(3));
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    if (driver != null)
                    {
                         driver.Manage().Cookies.DeleteAllCookies();
                    }
               }
               catch (Exception _ex)
               {
                    File.WriteAllText("log.txt", string.Format("{0}\t{1}\t{2}", DateTime.Now.ToString(), "youtubeRuner", _ex.ToString()));
                    Console.WriteLine(_ex.ToString());
               }
          }


          public RegMail(DataGridViewRow r, int chrome_id)
          {
               try
               {
                    this.r = r;
                    chromeid = chrome_id;

                    string sProfile = string.Format("UserData_{0}", "vunguyen41427@gmail.com");
                    //string sProfile = string.Format("UserData_{0}", "khanhdang701407@gmail.com");
                    string ChromePath = string.Format("{0}\\ChromeProfile.Subs\\{1}", Application.StartupPath, sProfile);



                    //options.AddArgument("no-sandbox");
                    string sUserAgent = listOfUserAgent.RandomList(3).FirstOrDefault().ToString();
                    if (!string.IsNullOrEmpty(sUserAgent))
                    {
                         options.AddArgument(string.Format("user-agent={0}", sUserAgent));
                    }
                    options.AddArgument(string.Format("user-data-dir={0}", ChromePath));
                    options.AddArgument(string.Format("profile-directory={0}", sProfile));
                    options.BinaryLocation = Application.StartupPath + string.Format("/GoogleChromePortable{0}/GoogleChromePortable.exe", chromeid);

                    var service = ChromeDriverService.CreateDefaultService();
                    service.LogPath = "chromedriver.log";
                    service.EnableVerboseLogging = true;
                    service.HideCommandPromptWindow = true;

                    driver = new ChromeDriver(service, options, TimeSpan.FromMinutes(3));
               }
               catch (Exception _ex)
               {
                    File.WriteAllText("log.txt", string.Format("{0}\t{1}\t{2}", DateTime.Now.ToString(), "youtubeRuner", _ex.ToString()));
                    Console.WriteLine(_ex.ToString());
               }
          }


          public RegMail(DataGridViewRow r, int chrome_id, dynamic dproxy)
          {
               try
               {
                    this.r = r;
                    chromeid = chrome_id;

                    string sProfile = string.Format("User Data {0}", chromeid);
                    string ChromePath = string.Format("{0}\\ChromeProfile\\{1}", Application.StartupPath, sProfile);
                    options.AddArgument("ignore-certificate-errors");
                    if (dproxy.protocol.ToString().Equals("http"))
                    {
                         options.AddArguments(string.Format("proxy-server={0}:{1}", dproxy.ip, dproxy.port));
                    }
                    else if (dproxy.protocol.ToString().Equals("socks4"))
                    {
                         options.AddArguments(string.Format("proxy-server=socks4://{0}:{1}", dproxy.ip, dproxy.port));
                    }
                    else
                    {
                         options.AddArguments(string.Format("proxy-server=socks5://{0}:{1}", dproxy.ip, dproxy.port));

                    }


                    //options.AddArgument("no-sandbox");

                    options.AddArgument(string.Format("user-data-dir={0}", ChromePath));
                    options.BinaryLocation = Application.StartupPath + string.Format("/GoogleChromePortable{0}/GoogleChromePortable.exe", chromeid);

                    var service = ChromeDriverService.CreateDefaultService();
                    service.LogPath = "chromedriver.log";
                    service.EnableVerboseLogging = true;
                    service.HideCommandPromptWindow = true;

                    driver = new ChromeDriver(service, options, TimeSpan.FromMinutes(3));
               }
               catch (Exception _ex)
               {
                    File.WriteAllText("log.txt", string.Format("{0}\t{1}\t{2}", DateTime.Now.ToString(), "youtubeRuner", _ex.ToString()));
                    Console.WriteLine(_ex.ToString());
               }
          }
          public RegMail(DataGridViewRow r, string chrome_folder, dynamic dproxy)
          {
               try
               {
                    this.r = r;
                    chromeid = 0;

                    string sProfile = string.Format("User Data {0}", chromeid);
                    string ChromePath = string.Format("{0}\\ChromeProfile\\{1}", Application.StartupPath, sProfile);
                    options.AddArgument("ignore-certificate-errors");
                    if (dproxy.protocol.ToString().Equals("http"))
                    {
                         options.AddArguments(string.Format("proxy-server={0}:{1}", dproxy.ip, dproxy.port));
                    }
                    else if (dproxy.protocol.ToString().Equals("socks4"))
                    {
                         options.AddArguments(string.Format("proxy-server=socks4://{0}:{1}", dproxy.ip, dproxy.port));
                    }
                    else
                    {
                         options.AddArguments(string.Format("proxy-server=socks5://{0}:{1}", dproxy.ip, dproxy.port));

                    }


                    //options.AddArgument("no-sandbox");

                    options.AddArgument(string.Format("user-data-dir={0}", ChromePath));
                    options.BinaryLocation = Application.StartupPath + string.Format("/GoogleChromePortable{0}/GoogleChromePortable.exe", chromeid);

                    var service = ChromeDriverService.CreateDefaultService();
                    service.LogPath = "chromedriver.log";
                    service.EnableVerboseLogging = true;
                    service.HideCommandPromptWindow = true;

                    driver = new ChromeDriver(service, options, TimeSpan.FromMinutes(3));
               }
               catch (Exception _ex)
               {
                    File.WriteAllText("log.txt", string.Format("{0}\t{1}\t{2}", DateTime.Now.ToString(), "youtubeRuner", _ex.ToString()));
                    Console.WriteLine(_ex.ToString());
               }
          }


          private static string sendHttpRequest(string url, NameValueCollection values, NameValueCollection files = null)
          {
               string boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");
               // The first boundary
               byte[] boundaryBytes = System.Text.Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
               // The last boundary
               byte[] trailer = System.Text.Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
               // The first time it itereates, we need to make sure it doesn't put too many new paragraphs down or it completely messes up poor webbrick
               byte[] boundaryBytesF = System.Text.Encoding.ASCII.GetBytes("--" + boundary + "\r\n");

               // Create the request and set parameters
               HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
               request.ContentType = "multipart/form-data; boundary=" + boundary;
               request.Method = "POST";
               request.KeepAlive = true;
               request.Credentials = System.Net.CredentialCache.DefaultCredentials;

               // Get request stream
               Stream requestStream = request.GetRequestStream();

               foreach (string key in values.Keys)
               {
                    // Write item to stream
                    byte[] formItemBytes = System.Text.Encoding.UTF8.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}", key, values[key]));
                    requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                    requestStream.Write(formItemBytes, 0, formItemBytes.Length);
               }

               if (files != null)
               {
                    foreach (string key in files.Keys)
                    {
                         if (File.Exists(files[key]))
                         {
                              int bytesRead = 0;
                              byte[] buffer = new byte[2048];
                              byte[] formItemBytes = System.Text.Encoding.UTF8.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n", key, files[key]));
                              requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                              requestStream.Write(formItemBytes, 0, formItemBytes.Length);

                              using (FileStream fileStream = new FileStream(files[key], FileMode.Open, FileAccess.Read))
                              {
                                   while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                                   {
                                        // Write file content to stream, byte by byte
                                        requestStream.Write(buffer, 0, bytesRead);
                                   }

                                   fileStream.Close();
                              }
                         }
                    }
               }

               // Write trailer and close stream
               requestStream.Write(trailer, 0, trailer.Length);
               requestStream.Close();

               using (StreamReader reader = new StreamReader(request.GetResponse().GetResponseStream()))
               {
                    return reader.ReadToEnd();
               };
          }



          public void StartRegHotmail()
          {

               //r.Cells["status"].Value = "running..";
               //r.Cells["startDate"].Value = DateTime.Now;

               try
               {
                    

                    string hotmail_url = "https://signup.live.com/signup";
                    string proton_mail = "https://mail.protonmail.com/create/new?language=en";
                    int sleeptime = 2 * 1000;


                    string firstname = listOfFirstName.RandomList(1).FirstOrDefault();
                    string lastname = listOfLastName.RandomList(1).FirstOrDefault();
                    string fullname = string.Format("{0}{1}{2}", firstname, lastname, new Random().Next(100000, 999999));
                    string password = string.Format("{0}{1}@{2}", firstname, lastname, new Random().Next(100000, 999999));
                    string email = string.Format("{0}@hotmail.com", fullname);

                    WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 60));
                    // create a hotmail
                    {
                         
                         driver.Navigate().GoToUrl(hotmail_url);
                         if (driver != null)
                         {
                              driver.Manage().Cookies.DeleteAllCookies();

                         }
                         driver.Navigate().GoToUrl(hotmail_url);

                         IWebElement clickableElement = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("MemberName")));
                         if (driver.Url.Contains("error"))
                         {
                              return;
                         }
                         IWebElement MemberName = driver.FindElement(By.Id("MemberName"));
                         MemberName.SendKeys(email);
                         Thread.Sleep(sleeptime);

                         ////IWebElement domainLabel = driver.FindElement(By.Id("domainLabel"));
                         //////domainLabel.Click();
                         ////Thread.Sleep(sleeptime);

                         ////IWebElement domain1 = driver.FindElement(By.Id("domain1"));
                         //////domain1.Click();
                         ////Thread.Sleep(sleeptime);


                         IWebElement iSignupAction = driver.FindElement(By.Id("iSignupAction"));
                         iSignupAction.Click();
                         Thread.Sleep(sleeptime);

                         IWebElement PasswordInput = driver.FindElement(By.Id("PasswordInput"));
                         PasswordInput.SendKeys(password);


                         iSignupAction = driver.FindElement(By.Id("iSignupAction"));
                         iSignupAction.Click();
                         Thread.Sleep(sleeptime);

                         IWebElement FirstName = driver.FindElement(By.Id("FirstName"));
                         FirstName.SendKeys(firstname);
                         Thread.Sleep(sleeptime);

                         IWebElement LastName = driver.FindElement(By.Id("LastName"));
                         LastName.SendKeys(lastname);
                         Thread.Sleep(sleeptime);

                         iSignupAction = driver.FindElement(By.Id("iSignupAction"));
                         iSignupAction.Click();
                         Thread.Sleep(sleeptime);




                         //driver.FindElement(By.XPath(".//*[@id='BirthMonth']/form/select[1]/option[3]")).Click();

                         var education = driver.FindElement(By.Name("BirthMonth"));
                         //create select element object 
                         var selectElement = new SelectElement(education);
                         string randomMonth = new Random().Next(1, 11).ToString();
                         selectElement.SelectByValue(randomMonth);

                         IWebElement BirthDay = driver.FindElement(By.Id("BirthDay"));
                         string randomDay = new Random().Next(1, 28).ToString();
                         BirthDay.SendKeys(randomDay);
                         Thread.Sleep(sleeptime);

                         IWebElement BirthYear = driver.FindElement(By.Id("BirthYear"));
                         string randomYear = new Random().Next(1970, 1990).ToString();
                         BirthYear.SendKeys(randomYear);



                         //optionlist = driver1.find_element_by_id('BirthMonth').find_elements_by_tag_name("option")
                         //optionlist[random.randint(1, 11)].click()
                         Thread.Sleep(sleeptime);

                         iSignupAction = driver.FindElement(By.Id("iSignupAction"));
                         iSignupAction.Click();
                         Thread.Sleep(sleeptime);
                         Thread.Sleep(5*1000);


                        
                         clickableElement = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#hipTemplateContainer img")));
                     
                         string imageUrl = driver.FindElement(By.CssSelector("#hipTemplateContainer img")).GetAttribute("src");
                         Console.WriteLine("image -> {0}", imageUrl);
                         Thread.Sleep(sleeptime);

                         try
                         {
                              WebClient client = new WebClient();
                              byte[] response = client.DownloadData(imageUrl);
                              File.WriteAllBytes("img1.jpg", response);
                              
                              NameValueCollection reqparm = new NameValueCollection();
                              NameValueCollection files = new NameValueCollection();
                              reqparm.Add("function", "picture2");
                              reqparm.Add("username", "thanhnambeo");
                              reqparm.Add("password", "4affhvpna");
                              reqparm.Add("pict_type", "0");
                              files.Add("pict", "img1.jpg");

                              string r = "";
                              for (int i=0; i<3;i++)
                              {
                                   
                                   
                                   try
                                   {
                                        r = sendHttpRequest("http://poster.de-captcher.com", reqparm, files);
                                        Console.WriteLine("r  -> {0}", r);
                                        string captcher = r.Split('|').ToList().Last().Replace(" ", string.Empty); ;
                                        Console.WriteLine("captcha  -> {0}", captcher);
                                        if (string.IsNullOrEmpty(captcher) || captcher.Contains("-7")==true)
                                        {
                                             continue;
                                        }
                                        IWebElement captcha = driver.FindElement(By.CssSelector("#hipTemplateContainer input"));
                                        captcha.SendKeys(captcher);
                                        Thread.Sleep(sleeptime);

                                        iSignupAction = driver.FindElement(By.Id("iSignupAction"));
                                        iSignupAction.Click();
                                        Thread.Sleep(5 * 1000);

                                        IWebElement body12345 = new WebDriverWait(driver, TimeSpan.FromSeconds(15)).Until(ExpectedConditions.ElementIsVisible(By.TagName("body")));
                                        string src1 = driver.PageSource;
                                        if (src1.Contains("The characters didn't match the picture. Please try again."))
                                        {
                                             continue;
                                        }
                                        break;
                                   }
                                   catch(Exception _ex)
                                   {

                                   }
                                   
                              }                             

                              //Thread.Sleep(sleeptime);
                             

                              Thread.Sleep(5*1000);                              
                              IWebElement body12 = new WebDriverWait(driver, TimeSpan.FromSeconds(15)).Until(ExpectedConditions.ElementIsVisible(By.TagName("body")));
                              
                              Thread.Sleep(sleeptime);

                              string src = driver.PageSource;
                              if (src.Contains("Phone number") && src.Contains("Country code") && src.Contains("Add security info"))
                              {
                                   return;
                              }

                              for (int i = 0; i < 6; i++)
                              {
                                   try
                                   {
                                        if (!driver.Url.Contains("https://outlook.live.com"))
                                        {
                                             driver.Navigate().GoToUrl("https://outlook.live.com/owa/");
                                        }
                                        iSignupAction = driver.FindElement(By.ClassName("nextButton"));
                                        if (iSignupAction != null)
                                        {
                                             iSignupAction.Click();
                                             Thread.Sleep(sleeptime);
                                        }
                                   }
                                   catch (Exception _ex)
                                   {
                                        //Console.WriteLine(_ex.ToString());
                                   }
                              }

                              IWebElement primaryButton = driver.FindElement(By.ClassName("primaryButton"));
                              if (primaryButton != null)
                              {
                                   primaryButton.Click();
                                   Thread.Sleep(sleeptime);
                              }
                              Thread.Sleep(sleeptime);
                              


                         }
                         catch (Exception ex)
                         {

                         }

                         Thread.Sleep(sleeptime);
                    }

                    driver.ExecuteScript("window.open();");
                    driver.SwitchTo().Window(driver.WindowHandles[1]);
                    driver.Navigate().GoToUrl(proton_mail);
                    // create a hotmail
                    Thread.Sleep(sleeptime);

                    {
                         IWebElement ifullname = driver.FindElement(By.Id("username"));
                         ifullname.SendKeys(fullname);
                         Thread.Sleep(sleeptime);

                         IWebElement ipassword = driver.FindElement(By.Id("password"));
                         ipassword.SendKeys(password);
                         Thread.Sleep(sleeptime);

                         IWebElement ipasswordc = driver.FindElement(By.Id("passwordc"));
                         ipasswordc.SendKeys(password);
                         Thread.Sleep(sleeptime);

                         IWebElement notificationEmail = driver.FindElement(By.Id("notificationEmail"));
                         notificationEmail.SendKeys(email);
                         Thread.Sleep(sleeptime);

                         IWebElement iSignupAction = driver.FindElement(By.ClassName("pm_button primary large signUpProcess-btn-create disabled-if-network-activity"));
                         iSignupAction.Click();
                         Thread.Sleep(sleeptime);

                         //IWebElement w_selectEmail = new WebDriverWait(driver, TimeSpan.FromSeconds(15)).Until(ExpectedConditions.ElementIsVisible(By.Id("id-signup-radio-email")));
                         //IWebElement selectEmail = driver.FindElement(By.Id("id-signup-radio-email"));
                         //selectEmail.Click();
                         //Thread.Sleep(sleeptime);


                         IWebElement w_selectEmail = new WebDriverWait(driver, TimeSpan.FromSeconds(15)).Until(ExpectedConditions.ElementIsVisible(By.Id("emailVerification")));

                         IWebElement emailVerification = driver.FindElement(By.Id("emailVerification"));
                         emailVerification.SendKeys(email);
                         Thread.Sleep(sleeptime);

                         IWebElement codeVerificator = driver.FindElement(By.ClassName("codeVerificator-btn-send"));
                         codeVerificator.Click();
                         Thread.Sleep(sleeptime);

                         driver.SwitchTo().Window(driver.WindowHandles[0]);
                         driver.Navigate().GoToUrl("https://outlook.live.com/mail/inbox");
                         Thread.Sleep(sleeptime);
                         Thread.Sleep(10 * 1000);
                         for (int i = 0; i < 5; i++)
                         {
                              try
                              {
                                   IWebElement Tab1 = driver.FindElement(By.CssSelector("button[id*='-Tab1"));
                                   Tab1.Click();
                                   break;
                              }
                              catch (Exception _ex)
                              {

                              }
                              try
                              {
                                   IWebElement Tab1 = driver.FindElement(By.Id("id__66"));
                                   Tab1.Click();
                                   //break;
                              }
                              catch (Exception _ex)
                              {

                              }


                         }
                         IWebElement verifycode = driver.FindElement(By.CssSelector("div[aria-label*='Your Proton verification code is: ']"));
                         string vercode=verifycode.GetAttribute("aria-label").ToString().GetLast(6);
                         Console.WriteLine("proton vercode -->{0}", vercode);


                         driver.SwitchTo().Window(driver.WindowHandles[1]);

                         IWebElement codeValue = driver.FindElement(By.Id("codeValue"));
                         codeValue.SendKeys(vercode);
                         Thread.Sleep(sleeptime);

                         IWebElement completeSetup = driver.FindElement(By.ClassName("humanVerification-completeSetup-create"));
                         completeSetup.Click();

                      
                         IWebElement clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("confirmModalBtn")));

                         IWebElement confirmModalBtn = driver.FindElement(By.Id("confirmModalBtn"));
                         confirmModalBtn.Click();


                         File.AppendAllText("account.txt", string.Format("{0}|{1}{2}", fullname, password, Environment.NewLine) );

                    }

                    //r.Cells["duration"].Value = string.Format("{0} seconds", Math.Ceiling((double)sleeptime / 1000));           
               }
               catch (Exception _ex)
               {
                    File.WriteAllText("log.txt", string.Format("{0}\t{1}\t{2}", DateTime.Now.ToString(), "StartYoutube", _ex.ToString()));
                    Console.WriteLine(_ex.ToString());
                   
               }
               finally
               {
                   
                    OnComplete?.Invoke(this, null);
                    if (driver != null)
                    {
                         driver.Manage().Cookies.DeleteAllCookies();
                         driver.Close();
                         //driver.Manage().Cookies.DeleteAllCookies();
                         driver.Quit();
                    }
               }


          }

     }
}
