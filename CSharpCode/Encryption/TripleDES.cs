using System;
using System.Text;

namespace CwCodeLib.Encryption
{
    public class TripleDES
    {
        public TripleDES(byte[] key, byte[] iv)
        {
            this.Key = key;
            this.IV = iv;
        }

        public byte[] Key { get; set; }

        public byte[] IV { get; set; }

        public string Encrypt(string plaintext)
        {
            System.Security.Cryptography.TripleDESCryptoServiceProvider cipher = new System.Security.Cryptography.TripleDESCryptoServiceProvider();
            cipher.BlockSize = 64;
            string cipherText;
            using (System.IO.MemoryStream cipherTextStream = new System.IO.MemoryStream())
            {
                using (System.Security.Cryptography.CryptoStream cryptoStream = new System.Security.Cryptography.CryptoStream(cipherTextStream, cipher.CreateEncryptor(this.Key, this.IV), System.Security.Cryptography.CryptoStreamMode.Write))
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(plaintext);
                    cryptoStream.Write(bytes, 0, bytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherText = Convert.ToBase64String(cipherTextStream.ToArray());
                }
            }

            return cipherText;
        }

        public string Decrypt(string ciphertext)
        {
            System.Security.Cryptography.TripleDESCryptoServiceProvider cipher = new System.Security.Cryptography.TripleDESCryptoServiceProvider();
            cipher.BlockSize = 64;
            string plaintext;
            using (System.IO.MemoryStream plaintextStream = new System.IO.MemoryStream())
            {
                using (System.Security.Cryptography.CryptoStream cryptoStream = new System.Security.Cryptography.CryptoStream(plaintextStream, cipher.CreateDecryptor(this.Key, this.IV), System.Security.Cryptography.CryptoStreamMode.Write))
                {
                    byte[] bytes = Convert.FromBase64String(ciphertext);
                    cryptoStream.Write(bytes, 0, bytes.Length);
                    cryptoStream.FlushFinalBlock();
                    plaintext = Encoding.UTF8.GetString(plaintextStream.ToArray());
                }
            }

            return plaintext;
        }

        public static byte[] GenerateKey()
        {
            System.Security.Cryptography.TripleDESCryptoServiceProvider cipher = new System.Security.Cryptography.TripleDESCryptoServiceProvider();
            cipher.GenerateKey();
            return cipher.Key;
        }

        public static byte[] GenerateIV()
        {
            System.Security.Cryptography.TripleDESCryptoServiceProvider cipher = new System.Security.Cryptography.TripleDESCryptoServiceProvider();
            cipher.GenerateIV();
            return cipher.IV;
        }
    }
}