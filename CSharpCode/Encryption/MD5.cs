using System.Linq;

namespace CwCodeLib.Encryption
{
    public static class MD5
    {
        public static string GetHashString(string input)
        {
            return string.Join("", new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(System.Text.Encoding.UTF8.GetBytes(input)).Select(b => b.ToString("X2")));
        }
    }
}