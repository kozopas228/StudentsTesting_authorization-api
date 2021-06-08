using System.Security.Cryptography;
using System.Text;

namespace Authorization_Services
{
    public class Hashing
    {
        public static string GetHashString(string text)
        {
            var bytes = Encoding.Unicode.GetBytes(text);

            var csp = new MD5CryptoServiceProvider();

            var byteHash = csp.ComputeHash(bytes);

            var hash = string.Empty;

            foreach (var b in byteHash)
                hash += $"{b:x2}";

            return hash;
        }
    }
}
