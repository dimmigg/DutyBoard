using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DutyBoard_Models.Extensions
{
    public static class StringExtensions
    {
        public static string Encryption(this string originalText)
        {
            if (string.IsNullOrEmpty(originalText))
                return "";

            var initVecB = Encoding.ASCII.GetBytes("b7aoSuDitOz1hYr#");
            var sol = Encoding.ASCII.GetBytes("Chepa");
            var originalTextB = Encoding.UTF8.GetBytes(originalText);

            var passDer = new PasswordDeriveBytes("pass", sol, "SHA1", 2);
            var keyBytes = passDer.GetBytes(32);
            var rij = new RijndaelManaged();
            rij.Mode = CipherMode.CBC;

            byte[] cipherTextBytes = null;

            using (var encryptor = rij.CreateEncryptor(keyBytes, initVecB))
            {
                using (var memStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(originalTextB, 0, originalTextB.Length);
                        cryptoStream.FlushFinalBlock();
                        cipherTextBytes = memStream.ToArray();
                        memStream.Close();
                        cryptoStream.Close();
                    }
                }
            }

            rij.Clear();
            return Convert.ToBase64String(cipherTextBytes);
        }

        public static string Decryption(this string cryptText)
        {
            if (string.IsNullOrEmpty(cryptText))
                return "";

            var initVecB = Encoding.ASCII.GetBytes("b7aoSuDitOz1hYr#");
            var sol = Encoding.ASCII.GetBytes("Chepa");
            var cipherTextBytes = Convert.FromBase64String(cryptText);

            var passDer = new PasswordDeriveBytes("pass", sol, "SHA1", 2);
            var keyBytes = passDer.GetBytes(32);

            var rij = new RijndaelManaged();
            rij.Mode = CipherMode.CBC;

            var plainTextBytes = new byte[cipherTextBytes.Length];
            var byteCount = 0;

            using (var decryptor = rij.CreateDecryptor(keyBytes, initVecB))
            {
                using (var mSt = new MemoryStream(cipherTextBytes))
                {
                    using (var cryptoStream = new CryptoStream(mSt, decryptor, CryptoStreamMode.Read))
                    {
                        byteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                        mSt.Close();
                        cryptoStream.Close();
                    }
                }
            }

            rij.Clear();
            return Encoding.UTF8.GetString(plainTextBytes, 0, byteCount);
        }

    }
}
