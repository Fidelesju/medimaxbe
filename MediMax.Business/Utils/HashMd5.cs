using System.Security.Cryptography;
using System.Text;

namespace MediMax.Business.Utils
{
    public class HashMd5
    {
        static string keyMD5 { get; set; } = "E(H+MbQeThWmZq4t7w!z%C&F)J@NcRfUjXn2r5u8x/A?D(G-KaPdSgVkYp3s6v9y";
        public string EncryptMD5(string text)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

            tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(keyMD5));
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform transform = tdes.CreateEncryptor();
            byte[] textBytes = UTF8Encoding.UTF8.GetBytes(text);
            byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
            return Convert.ToBase64String(bytes, 0, bytes.Length);
        }

        public string DecryptMD5(string cipher)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

            tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(keyMD5));
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform transform = tdes.CreateDecryptor();

            byte[] cipherBytes = Convert.FromBase64String(cipher);
            byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
            return UTF8Encoding.UTF8.GetString(bytes);
        }
    }
}