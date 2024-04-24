using System;
using System.IO;
using System.Reflection;

namespace KSS.Patterns.Environment
{
    public class EnvironmentUtils
    {
        static public string GetAssemblyDirectory()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
    }
}