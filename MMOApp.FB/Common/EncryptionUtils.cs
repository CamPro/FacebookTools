using System.Security.Cryptography;
using System.Text;

namespace KSS.Patterns.Encryption
{
    public class EncryptionUtils
    {
        public static string GetMd5Sum(string str, bool unicode = true)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = unicode ? Encoding.UTF8.GetBytes(str) : Encoding.ASCII.GetBytes(str);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}