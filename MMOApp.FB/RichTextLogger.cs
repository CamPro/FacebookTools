using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using KSS.Patterns.Extensions;
using KSS.Patterns.Logging;

namespace MMOApp.FB
{
    public class RichTextLogger
    {
        readonly RichTextBox textbox;
        readonly ConcurrentQueue<string> logContent = new ConcurrentQueue<string>();
        private int logCount;

        private readonly Dictionary<Type, List<string>> trimmedExceptions;

        public bool Logging { get; set; }

        public int DeleteAfterLog { get; set; }

        public RichTextLogger(RichTextBox textbox)
        {
            if (textbox == null) throw new ArgumentNullException(nameof(textbox));
            logCount = 0;

            this.textbox = textbox;
            Logging = true;

            trimmedExceptions = new Dictionary<Type, List<string>>();

            textbox.MouseClick += TextboxOnMouseClick;
        }

        private void TextboxOnMouseClick(object sender, MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Right)
                Logging = !Logging;
        }
        public void AppendLog(string message, Exception e)
        {
            AppendLog(message, string.Empty, e);
        }

        public void AppendLog(string message, string prefix, Exception e)
        {
            if (!message.IsNullOrWhiteSpace())
                AppendLog(message, prefix, Color.Red);

            AppendLog(e);
        }

        public void AppendLog(Exception e, string prefix = "")
        {
            if (IsExceptionDetailTrimmed(e))
            {
                AppendLog(e.Message, prefix, Color.Red);
            }
            else
            {
                AppendLog(e.ToString(), prefix, Color.Red);
            }
        }

        public void LogExceptionMessge(Exception e, string prefix = "")
        {
            AppendLog(GetExceptionMessage(e), prefix, Color.Red);
        }

        private bool IsExceptionDetailTrimmed(Exception e)
        {
            var exceptionType = e.GetType();
            if (trimmedExceptions.ContainsKey(exceptionType))
            {
                var messages = trimmedExceptions[exceptionType];
                return (messages == null) || (messages.Any(m => m.Contains(e.Message)));
            }

            var ge = e as AggregateException;
            if (ge != null)
            {
                if (ge.InnerExceptions.Any(IsExceptionDetailTrimmed))
                    return true;
            }
            else
            {
                if (e.InnerException != null && IsExceptionDetailTrimmed(e.InnerException))
                    return true;
            }

            return false;
        }

        public void AppendLog(string content, string prefix = "")
        {
            AppendLog(content, prefix, Color.Black);
        }

        public void AppendLog(string content, Color color)
        {
            AppendLog(content, null, color);
        }

        public void AppendLog(string content, string prefix, Color color)
        {
            if (Logging)
            {
                var mainForm = GetMainForm();
                mainForm.SafeInvoke(() =>
                {
                    if (DeleteAfterLog > 0 && logCount > DeleteAfterLog)
                    {
                        ClearLog();
                        logCount = 0;
                    }

                    if (!prefix.IsNullOrWhiteSpace())
                        textbox.AppendText(prefix, color);

                    textbox.AppendText(content + "\r\n", color);
                    textbox.SelectionStart = textbox.Text.Length;
                    textbox.ScrollToCaret();
                    logCount++;
                });
            }
        }

        public string GetLog()
        {
            var mainForm = GetMainForm();
            return mainForm.SafeInvoke(() => textbox.Text);
        }

        public void WriteToLogStream(string log)
        {
            logContent.Enqueue(log);
        }

        public void WriteLogStream()
        {
            Task.Run(() =>
            {
                var sbLog = new StringBuilder();

                do
                {
                    string log;
                    while (logContent.TryDequeue(out log))
                    {
                        sbLog.AppendLine(log);
                        //AppendLog(log);
                    }

                    if (sbLog.Length > 0)
                    {
                        sbLog.Remove(sbLog.Length - 2, 2);

                        AppendLog(sbLog.ToString());
                        sbLog.Clear();
                    }

                    Thread.Sleep(100);

                } while (true);
            });
        }

        public void ClearLog()
        {
            var mainForm = GetMainForm();
            mainForm.SafeInvoke(() => textbox.Text = string.Empty);
        }

        private Form GetMainForm()
        {
            var mainForm = textbox.FindForm();
            return mainForm;
        }

        private string GetExceptionMessage(Exception e)
        {
            var ex = GetInner(e);
            return ex.Message;
        }

        private Exception GetInner(Exception e)
        {
            Exception inner = e;
            do
            {
                if (inner.InnerException != null)
                    inner = inner.InnerException;
                else
                    break;

            } while (true);

            return inner;
        }

        public Color GetLogColor(LogMode mode)
        {
            Color color;

            switch (mode)
            {
                case LogMode.None:
                case LogMode.Runtime:
                case LogMode.Debug:
                    color = Color.Black;
                    break;

                case LogMode.Warning:
                    color = Color.Blue;
                    break;

                case LogMode.Error:
                    color = Color.Red;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return color;
        }

        public void AppendLog(LogContent content, string prefix = "")
        {
            Color color = GetLogColor(content.Mode);

            string message = content.Message;
            if (content.Exception != null)
                message += " : " + GetExceptionMessage(content.Exception);

            AppendLog(message, prefix, color);
        }

        public void TrimExceptionDetail<T>(params string[] messages) where T : Exception
        {
            TrimExceptionDetail(typeof(T), messages);
        }

        public void TrimExceptionDetail(Type exceptionType, params string[] messages)
        {
            if (!trimmedExceptions.ContainsKey(exceptionType))
                trimmedExceptions.Add(exceptionType, messages != null ? messages.ToList() : null);
        }

        public string GetCurrentLog()
        {
            var mainForm = GetMainForm();
            return mainForm.SafeInvoke(() => textbox.Text);
        }
    }
}