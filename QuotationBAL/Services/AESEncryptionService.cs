using QuotationModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuotationBAL.Services
{
    public class AESEncryptionService
    {
        public String Encryptpassworddata(UserMasterModel user)
        {
            string data = user.EncryptedPassword;
            string key = GenerateRandomKey();
            string encryptedData = EncryptData(data, key);
            user.EncryptedKey = EncryptKey(key);
            return encryptedData;

            /*string decryptedData = DecryptData(encryptedData, key);
            Console.WriteLine("Decrypted Data: " + decryptedData);*/
        }

        public String Decryptpassworddata(UserMasterModel user)
        {
            string data = user.EncryptedPassword;
            string DKey = user.EncryptedKey;
            string DecryptedKey = DecryptKey(DKey);
            string encryptedData = EncryptData(data, DecryptedKey);
            return encryptedData;

            /*string decryptedData = DecryptData(encryptedData, key);
            Console.WriteLine("Decrypted Data: " + decryptedData);*/
        }

        public static string EncryptKey(string data)
        {
            StringBuilder encryptedText = new StringBuilder();

            foreach (char c in data)
            {
                char encryptedChar = (char)(c + 1); // Shift each character by 1 position
                encryptedText.Append(encryptedChar);
            }

            return encryptedText.ToString();
        }
        public static string DecryptKey(string encryptedData)
        {
            StringBuilder decryptedText = new StringBuilder();

            foreach (char c in encryptedData)
            {
                char decryptedChar = (char)(c - 1); // Reverse the shift by 1 position
                decryptedText.Append(decryptedChar);
            }

            return decryptedText.ToString();
        }
        public static string EncryptData(string data, string key)
        {
            byte[] encryptedBytes;

            using (AesManaged aes = new AesManaged())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = new byte[16]; // Fixed IV

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (var ms = new System.IO.MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                        cs.Write(dataBytes, 0, dataBytes.Length);
                        cs.FlushFinalBlock();
                    }

                    encryptedBytes = ms.ToArray();
                }
            }

            // Convert to Base64 string for easier storage or transmission
            string encryptedData = Convert.ToBase64String(encryptedBytes);
            return encryptedData;
        }

        public static string DecryptData(string encryptedData, string key)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedData);
            byte[] decryptedBytes;

            using (AesManaged aes = new AesManaged())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = new byte[16]; // Fixed IV

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (var ms = new System.IO.MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
                    {
                        cs.Write(encryptedBytes, 0, encryptedBytes.Length);
                        cs.FlushFinalBlock();
                    }

                    decryptedBytes = ms.ToArray();
                }
            }

            string decryptedData = Encoding.UTF8.GetString(decryptedBytes);
            return decryptedData;
        }
        public static string GenerateRandomKey()
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] keyBytes = new byte[16]; // Assuming key length is 32 bytes (256 bits)
                rng.GetBytes(keyBytes);
                string key = Convert.ToBase64String(keyBytes);
                return key;
            }
        }
    }
}

