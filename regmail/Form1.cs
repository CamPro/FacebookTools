using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace regmail
{
     public partial class Form1 : Form
     {
          public Form1()
          {
               InitializeComponent();

           
          }

          private void _close()
          {
               try {
                    foreach (var process in Process.GetProcessesByName("chromedriver"))
                         process.Kill();
                    foreach (var process in Process.GetProcessesByName("Chrome"))
                         process.Kill();
               }
               catch(Exception _ex)
               {

               }
          }
          private void tbarRun_Click(object sender, EventArgs e)
          {
               isRunning = true;
               var y = new RegMail();
               y.OnComplete += (s1, e1) =>
               {
                    //r.Cells["status"].Value = "completed";
                    //r.Cells["endDate"].Value = DateTime.Now;
               };
               y.StartRegHotmail();
               Thread.Sleep(5000);
               _close();

               isRunning = false;
          }

          System.Timers.Timer aTimer = new System.Timers.Timer();
          bool isRunning = false;
          private void Form1_Load(object sender, EventArgs e)
          {

              
               aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
               aTimer.Interval = 30*1000;
               aTimer.Enabled = true;

               //var client = new WebClient();
               //client.Proxy = new WebProxy("zproxy.lum-superproxy.io:22225");
               //client.Proxy.Credentials = new NetworkCredential("lum-customer-hl_2fd5a055-zone-zone1", "ms64nh1yppcw");
               //Console.WriteLine(client.DownloadString("http://lumtest.com/myip.json"));


          }
          private void OnTimedEvent(object source, ElapsedEventArgs e)
          {
               if (!isRunning)
               {

                    toolStrip1.BeginInvoke((Action)(() =>
                    {

                         tbarRun.PerformClick();
                    }));


                    
               }
          }


          private void toolStripButton1_Click(object sender, EventArgs e)
          {
               foreach (var process in Process.GetProcessesByName("Chrome"))
                    process.Kill();
          }
     }
}
