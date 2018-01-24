using System.Linq;
using System.Text;

namespace CwCodeLib.Encryption
{
    public static class SHA512
    {
        public static string GetHashString(string input)
        {
            var hasher = System.Security.Cryptography.SHA512.Create();
            return string.Join("", hasher.ComputeHash(Encoding.UTF8.GetBytes(input)).Select(b => b.ToString("X2")));
        }
    }
}